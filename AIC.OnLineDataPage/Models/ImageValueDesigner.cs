using AIC.Core;
using AIC.Domain;
using DiagramDesigner;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace AIC.OnLineDataPage.Models
{
    public class ImageValueDesigner : DesignerItemViewModelBase
    {
        private string ImageViewModelID = BitConverter.ToString(SHA1Managed.Create().ComputeHash(Encoding.UTF8.GetBytes("ImageViewModel"))).Replace("-", string.Empty);
        private IDisposable propertyChangedSubscription;
        private IDisposable connectorsChangedSubscription;
        private ObservableCollection<CinchMenuItem> menuOptions;
        public ImageValueDesigner()
            : base(BitConverter.ToString(SHA1Managed.Create().ComputeHash(Encoding.UTF8.GetBytes("ImageViewModel"))).Replace("-", string.Empty))
        {
            menuOptions = new ObservableCollection<CinchMenuItem>();
            AddItemCommand = new SimpleCommand(ExecuteAddItemCommand);
            ImageSwitchCommand = new SimpleCommand(ExecuteImageSwitchCommand);

            BuildMenuOptions();
            propertyChangedSubscription = WhenPropertyChanged.Where(o => o.ToString() == "Left" || o.ToString() == "Top" || o.ToString() == "ItemWidth" || o.ToString() == "ItemHeight").Subscribe(ChangeImageElement);
            connectorsChangedSubscription = WhenConnectorsChanged.Subscribe(OnConnectorsChanged);  
        }

        private void BuildMenuOptions()
        {
            //if (Directory.Exists(@"C:\AIC\设备图"))
            string dir = @LocalAddress.GifDir;

            if (Directory.Exists(dir))
            {               
                var equipmentImages = Directory.GetFiles(dir).Select(Path.GetFileName);
                foreach (var item in equipmentImages)
                {
                    CinchMenuItem menuItem = new CinchMenuItem();
                    menuItem.Text = item;
                    menuItem.Command = ImageSwitchCommand;
                    menuItem.CommandParameter = item;
                    menuOptions.Add(menuItem);
                }
            }
        }

        private void ChangeImageElement(string propertyName)
        {
            switch(propertyName)
            {
                case "Left":
                    ImageDesignElement.Left = Left;
                    break;
                case "Top":
                    ImageDesignElement.Top = Top;
                    break;
                case "ItemWidth":
                    ImageDesignElement.Width = ItemWidth;
                    break;
                case "ItemHeight":
                    ImageDesignElement.Height = ItemHeight;
                    break;
                default:
                    break;
            }   
        }

        private void OnConnectorsChanged(NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                if (args.NewItems.Count > 0)
                {
                    foreach (var item in args.NewItems)
                    {
                        var connector = item as FullyCreatedConnectorInfo;
                        ConnectorElement connectorElement = new ConnectorElement();
                        connectorElement.ParentID = ImageViewModelID;
                        connectorElement.XRatio = connector.XRatio;
                        connectorElement.YRatio = connector.YRatio;
                        connectorElement.Orientation = connector.Orientation.ToString();
                        connector.DesignElement = connectorElement;
                        ImageDesignElement.Connectors.Add(connectorElement);
                    }
                }
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                if (args.OldItems.Count > 0)
                {
                    foreach (var item in args.OldItems)
                    {
                        var connector = item as FullyCreatedConnectorInfo;
                        var connectorElements = ImageDesignElement.Connectors.Where(o => o.XRatio == connector.XRatio && o.YRatio == o.YRatio).ToArray();
                        foreach (var connectorElement in connectorElements)
                        {
                            ImageDesignElement.Connectors.Remove(connectorElement);
                        }
                    }
                }
            }
            else if (args.Action == NotifyCollectionChangedAction.Reset)
            {
                ImageDesignElement.Connectors.Clear();
            }
        }


        public SimpleCommand AddItemCommand { get; private set; }
        public SimpleCommand ImageSwitchCommand { get; private set; }
        public IEnumerable<CinchMenuItem> MenuOptions { get { return menuOptions; } }

        private void ExecuteAddItemCommand(object parameter)
        {
            FullyCreatedConnectorInfo connector = new FullyCreatedConnectorInfo(this, ConnectorOrientation.Top);
            MouseButtonEventArgs mosueArg = ((EventToCommandArgs)parameter).EventArgs as MouseButtonEventArgs;
            var position = mosueArg.GetPosition(((EventToCommandArgs)parameter).Sender as IInputElement);
            connector.XRatio = (position.X - ConnectorInfoBase.ConnectorWidth / 2) / connector.DataItem.ItemWidth;
            connector.YRatio = (position.Y - ConnectorInfoBase.ConnectorHeight / 2) / connector.DataItem.ItemHeight;
            AddConnector(connector);
        }
        private void ExecuteImageSwitchCommand(object parameter)
        {
            string image = parameter as string;
            //string filePath = string.Format(@"C:\AIC\设备图\{0}", image);

            string dir = @LocalAddress.GifDir + @"\{0}";
            string filePath = string.Format(dir, image);
            if (File.Exists(filePath))
            {
                SelectedImage = filePath;
                ImageDesignElement.BackgroundImage = SelectedImage;      
            }
        }        

        #region DesignElement
        private ImageDesignElement imageDesignElement = new ImageDesignElement();
        public ImageDesignElement ImageDesignElement
        {
            get { return imageDesignElement; }
            set
            {
                if (imageDesignElement != value)
                {
                    if (propertyChangedSubscription!=null)
                    {
                        propertyChangedSubscription.Dispose();
                    }
                    if (connectorsChangedSubscription != value)
                    {
                        connectorsChangedSubscription.Dispose();
                    }

                    ClearConnectors();
                    imageDesignElement = value;
                    if (value != null)
                    {
                        Top = value.Top;
                        Left = value.Left;
                        ItemWidth = value.Width;
                        ItemHeight = value.Height;
                        string dir = @LocalAddress.GifDir + @"\{0}";
                        string image = string.Format(dir, value.BackgroundImage.Split('\\').Last());
                        if (File.Exists(image))
                        {
                            SelectedImage = image;
                        }
                        else
                        {
                            SelectedImage = "../Images/Null.gif";
                        }
                        foreach (var connectorElement in value.Connectors)
                        {
                            ConnectorOrientation orientation=ConnectorOrientation.Top;
                            Enum.TryParse(connectorElement.Orientation, out orientation);
                            FullyCreatedConnectorInfo connector=new FullyCreatedConnectorInfo(this,orientation);
                            connector.DesignElement = connectorElement;
                            AddConnector(connector);
                        }
                        propertyChangedSubscription = WhenPropertyChanged.Where(o => o.ToString() == "Left" || o.ToString() == "Top" || o.ToString() == "ItemWidth" || o.ToString() == "ItemHeight").Subscribe(ChangeImageElement);
                        connectorsChangedSubscription = WhenConnectorsChanged.Subscribe(OnConnectorsChanged);
                    }
                }
            }
        }
        #endregion

        private bool shouldInsertAnchor;
        public bool ShouldInsertAnchor
        {
            get { return shouldInsertAnchor; }
            set
            {
                if (shouldInsertAnchor != value)
                {
                    shouldInsertAnchor = value;
                    NotifyChanged("ShouldInsertAnchor");
                }
            }
        }

        #region SelectedImage
        private string selectedImage = "../Images/Null.gif";
        public string SelectedImage
        {
            get { return selectedImage; }
            set
            {
                if (selectedImage != value)
                {
                    if (value != null)
                    {
                        selectedImage = value;
                    }
                    else
                    {
                        selectedImage = "../Images/Null.gif";
                    }
                    NotifyChanged("SelectedImage");
                }
            }
        }
        #endregion
    }
}

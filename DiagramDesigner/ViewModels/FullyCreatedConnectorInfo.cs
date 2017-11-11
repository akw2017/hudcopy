using AIC.Domain;
using System;
using System.Collections.Generic;

namespace DiagramDesigner
{
    public class FullyCreatedConnectorInfo : ConnectorInfoBase
    {
        private bool showConnectors = false;
        private List<CinchMenuItem> menuOptions;

        public FullyCreatedConnectorInfo(DesignerItemViewModelBase dataItem, ConnectorOrientation orientation)
            : base(orientation)
        {
            this.DataItem = dataItem;

            menuOptions = new List<CinchMenuItem>();
            MenuItemCommand = new SimpleCommand(ExecuteMenuItemCommand);
            DeleteCommand = new SimpleCommand(ExecuteDeleteCommand);
            BuildMenuOptions();
           
        }

        private ConnectorElement designElement;
        public ConnectorElement DesignElement
        {
            get { return designElement; }
            set
            {
                if (designElement != value)
                {
                    designElement = value;
                    if (value != null)
                    {
                        XRatio = value.XRatio;
                        YRatio = value.YRatio;
                        ConnectorOrientation connectorOrientation = ConnectorOrientation.Top;
                        Enum.TryParse(value.Orientation, out connectorOrientation);
                        Orientation = connectorOrientation;
                    }
                }
            }
        }

        public void ExecuteMenuItemCommand(object arg)
        {
            Orientation = (ConnectorOrientation)arg;
            if(DesignElement!=null)
            {
                DesignElement.Orientation = Orientation.ToString();
            }
        }

        public void ExecuteDeleteCommand(object arg)
        {
            DataItem.RemoveConnector(this);

        }

        private void BuildMenuOptions()
        {
            menuOptions.Clear();
            var orientation = new CinchMenuItem("方向");
            var top = new CinchMenuItem("上");
            top.Command = MenuItemCommand;
            top.CommandParameter = ConnectorOrientation.Top;
            var bottom = new CinchMenuItem("下");
            bottom.Command = MenuItemCommand;
            bottom.CommandParameter = ConnectorOrientation.Bottom;
            var left = new CinchMenuItem("左");
            left.Command = MenuItemCommand;
            left.CommandParameter = ConnectorOrientation.Left;
            var right = new CinchMenuItem("右");
            right.Command = MenuItemCommand;
            right.CommandParameter = ConnectorOrientation.Right;
            orientation.Children.Add(top);
            orientation.Children.Add(bottom);
            orientation.Children.Add(left);
            orientation.Children.Add(right);

            var delete = new CinchMenuItem("删除");
            delete.Command = DeleteCommand;

            menuOptions.Add(orientation);
            menuOptions.Add(delete);
        }


        public DesignerItemViewModelBase DataItem { get; private set; }

        public bool ShowConnectors
        {
            get
            {
                return showConnectors;
            }
            set
            {
                if (showConnectors != value)
                {
                    showConnectors = value;
                    NotifyChanged("ShowConnectors");
                }
            }
        }

        private double xRatio;
        public double XRatio
        {
            get{ return xRatio;}
            set
            {
                if (xRatio != value)
                {
                    xRatio = value;
                    NotifyChanged("XRatio");
                }
            }
        }

        private double yRatio;
        public double YRatio
        {
            get { return yRatio; }
            set
            {
                if (yRatio != value)
                {
                    yRatio = value;
                    NotifyChanged("YRatio");
                }
            }
        }

        public SimpleCommand DeleteCommand { get; private set; }
        public SimpleCommand MenuItemCommand { get; private set; }

        public IEnumerable<CinchMenuItem> MenuOptions { get { return menuOptions; } }
    }
}

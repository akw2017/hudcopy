using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Collections.Specialized;

namespace DiagramDesigner
{
    public abstract class DesignerItemViewModelBase : SelectableDesignerItemViewModelBase
    {
     
        private bool showConnectors = false;
        private ObservableCollection<FullyCreatedConnectorInfo> connectors = new ObservableCollection<FullyCreatedConnectorInfo>();

        public DesignerItemViewModelBase(string id) : base(id)
        {

        }

        public IObservable<NotifyCollectionChangedEventArgs> WhenConnectorsChanged
        {
            get
            {
                return Observable
                    .FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                        h => this.connectors.CollectionChanged += h,
                        h => this.connectors.CollectionChanged -= h)
                    .Select(x => x.EventArgs);
            }
        }

        public bool ShowConnectors
        {
            get { return showConnectors; }
            set
            {
                if (showConnectors != value)
                {
                    showConnectors = value;
                    foreach (var connector in connectors)
                    {
                        connector.ShowConnectors = value;
                    }
                    NotifyChanged("ShowConnectors");
                }
            }
        }

        private double left;
        public double Left
        {
            get { return left; }
            set
            {
                if (left != value)
                {
                    left = value;
                    NotifyChanged("Left");
                }
            }
        }

        private double top;
        public double Top
        {
            get { return top; }
            set
            {
                if (top != value)
                {
                    top = value;
                    NotifyChanged("Top");
                }
            }
        }

        private double itemWidth = 300;
        public double ItemWidth
        {
            get { return itemWidth; }
            set
            {
                if (itemWidth != value)
                {
                    itemWidth = value;
                    NotifyChanged("ItemWidth");
                }
            }
        }

        private double itemHeight = 200;
        public double ItemHeight
        {
            get { return itemHeight; }
            set
            {
                if (itemHeight != value)
                {
                    itemHeight = value;
                    NotifyChanged("ItemHeight");
                }
            }
        }

        private bool isDragConnectionOver;
        public bool IsDragConnectionOver
        {
            get { return isDragConnectionOver; }
            set
            {
                if (isDragConnectionOver != value)
                {
                    isDragConnectionOver = value;
                    NotifyChanged("IsDragConnectionOver");
                }
            }
        }

        public void AddConnector(FullyCreatedConnectorInfo connector)
        {
            if (!connectors.Contains(connector))
            {
                connectors.Add(connector);
            }
        }

        public void RemoveConnector(FullyCreatedConnectorInfo connector)
        {
            if (connectors.Contains(connector))
            {
                connectors.Remove(connector);
            }
        }

        public void ClearConnectors()
        {
            connectors.Clear();
        }

        public IEnumerable<FullyCreatedConnectorInfo> Connectors { get { return connectors; } }
    }
}

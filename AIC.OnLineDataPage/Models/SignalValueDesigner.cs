
using AIC.Core.SignalModels;
using AIC.Domain;
using DiagramDesigner;
using System;
using System.Reactive.Linq;

namespace AIC.OnLineDataPage.Models
{
    public class SignalValueDesigner : DesignerItemViewModelBase
    {
        private IDisposable propertyChangedSubscription;
        public SignalValueDesigner(BaseAlarmSignal sg):base(sg.Guid.GetHashCode().ToString())
        {
            TopConnector = new FullyCreatedConnectorInfo(this, ConnectorOrientation.Top) { XRatio = 0.5, YRatio = 0 };
            BottomConnector = new FullyCreatedConnectorInfo(this, ConnectorOrientation.Bottom) { XRatio = 0.5, YRatio = 1 };
            LeftConnector = new FullyCreatedConnectorInfo(this, ConnectorOrientation.Left) { XRatio = 0, YRatio = 0.5 };
            RightConnector = new FullyCreatedConnectorInfo(this, ConnectorOrientation.Right) { XRatio = 1, YRatio = 0.5 };

            AddConnector(TopConnector);
            AddConnector(BottomConnector);
            AddConnector(LeftConnector);
            AddConnector(RightConnector);

            Signal = sg;
            propertyChangedSubscription = WhenPropertyChanged.Where(o => o.ToString() == "Left" || o.ToString() == "Top" || o.ToString() == "ItemWidth" || o.ToString() == "ItemHeight").Subscribe(ChangeImageElement);
        }

        private void ChangeImageElement(string propertyName)
        {
            switch (propertyName)
            {
                case "Top":
                    DesignElement.Top = Top;
                    break;
                case "Left":
                    DesignElement.Left = Left;
                    break;
                case "ItemWidth":
                    DesignElement.Width = ItemWidth;
                    break;
                case "ItemHeight":
                    DesignElement.Height = ItemHeight;
                    break;
            }
        }

        public FullyCreatedConnectorInfo TopConnector { get; private set; }
        public FullyCreatedConnectorInfo BottomConnector { get; private set; }
        public FullyCreatedConnectorInfo LeftConnector{ get; private set; }
        public FullyCreatedConnectorInfo RightConnector{ get; private set; }

        private DesignElement designElement;
        public DesignElement DesignElement
        {
            get { return designElement; }
            set
            {
                if (designElement != value)
                {
                    if (propertyChangedSubscription != null)
                    {
                        propertyChangedSubscription.Dispose();
                    }
                    designElement = value;
                    if (value != null)
                    {
                        Top = value.Top;
                        Left = value.Left;
                        ItemWidth = value.Width;
                        ItemHeight = value.Height;
                        propertyChangedSubscription = WhenPropertyChanged.Where(o => o.ToString() == "Left" || o.ToString() == "Top" || o.ToString() == "ItemWidth" || o.ToString() == "ItemHeight").Subscribe(ChangeImageElement);
                    }
                }
            }
        }

        private BaseAlarmSignal signal;
        public BaseAlarmSignal Signal
        {
            get { return signal; }
            set
            {
                if (signal != value)
                {
                    signal = value;
                    NotifyChanged("Signal");
                }
            }
        }

       
    }
}

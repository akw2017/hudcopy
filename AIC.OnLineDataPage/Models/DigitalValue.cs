using AIC.Domain;
using Prism.Commands;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace AIC.OnLineMonitor.Models
{
    public class DigitalValue : ValueBase
    {
        private IDisposable propertyChangedSubscription;

        public DigitalValue(DigitalSignal sg)
            : base(sg != null ? sg.ChannelId.ToString() : string.Empty)
        {
            Signal = sg;
            propertyChangedSubscription = WhenPropertyChanged.Where(o => o.ToString() == "Left" || o.ToString() == "Top" || o.ToString() == "ItemWidth" || o.ToString() == "ItemHeight").Subscribe(ChangeImageElement);
            //ShowPropertyCommand = new DelegateCommand<object>(ShowProperty, CanShowProperty);
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

        #region Public Property

        #region Signal
        private DigitalSignal signal;
        public override SignalBase Signal
        {
            get { return signal; }
            set
            {
                if (signal != value)
                {
                    signal = (DigitalSignal)value;
                    NotifyChanged("Signal");
                }
            }
        }
        #endregion Signal

        #region DesignElement
        private DigitalElement designElement;
        public override DesignElement DesignElement
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
                    designElement = (DigitalElement)value;
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
        #endregion

        #endregion

        protected override void ItemIsSelected()
        {
            if (Signal != null)
            {
                //-EventAggregatorService.Instance.EventAggregator.GetEvent<SignalValueSelectedEvent>().Publish(Signal);
            }
        }

        public DelegateCommand<Object> ShowPropertyCommand { get; set; }
    }
}

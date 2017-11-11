using AIC.Core.Events;
using AIC.Core.SignalModels;
using AIC.Domain;
using Akka.Actor;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using Prism.Mvvm;
using System;

namespace AIC.OnLineDataPage.ViewModels.SubViewModels
{
    public delegate void SignalChangedHandler();
    public abstract class ChartViewModelBase : BindableBase
    {
        private SubscriptionToken subscriptionToken;
        private IEventAggregator _eventAggregator;
        public event EventHandler Closed;
        public event SignalChangedHandler SignalChanged;

        protected ChartViewModelBase(BaseAlarmSignal signal)
        {
            SetSignal(signal);
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();          
        }

        public void Subscribe(Action<object> action)
        {
            SignalBroadcastingEvent signalProcessedEvent = _eventAggregator.GetEvent<SignalBroadcastingEvent>();
            if (subscriptionToken != null)
            {
                signalProcessedEvent.Unsubscribe(subscriptionToken);
            }
            subscriptionToken = signalProcessedEvent.Subscribe(action, ThreadOption.UIThread, true, Filter);
        }

        protected virtual bool Filter(object message)
        {
            return IsUpdated;
        }

        public bool IsUpdated { get; set; }

        public void Unsubscribe()
        {
            SignalBroadcastingEvent signalProcessedEvent = _eventAggregator.GetEvent<SignalBroadcastingEvent>();
            if (subscriptionToken != null)
            {
                signalProcessedEvent.Unsubscribe(subscriptionToken);
                subscriptionToken = null;
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
                    OnPropertyChanged(() => Signal);
                }
            }
        }

        public virtual void SetSignal(BaseAlarmSignal sg)
        {
            try
            {
                RemoveProcessor();
                Signal = sg;
                AddProcessor();
                RaisedSiganlChanged();
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-设置信号", ex));
            }
        }

        public void RaisedSiganlChanged()
        {
            var handler = SignalChanged;
            if (handler != null)
            {
                handler();
            }
        }

        public virtual void AddProcessor()
        {
        }

        public virtual void RemoveProcessor()
        {
        }

        public void Close()
        {
            Unsubscribe();
            RemoveProcessor();
            var handler = Closed;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}

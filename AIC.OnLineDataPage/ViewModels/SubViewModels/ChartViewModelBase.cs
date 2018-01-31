using AIC.Core.Events;
using AIC.Core.SignalModels;
using AIC.CoreType;
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
        public event EventHandler Opened;
        public event SignalChangedHandler SignalChanged;

        protected ChartViewModelBase(BaseAlarmSignal sg)
        {
            Signal = sg;
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();          
        }

        protected ChartViewModelBase(BaseAlarmSignal sg, bool isfilter, SignalPreProccessType signalPreType)
        {
            IsFilter = isfilter;
            SignalPreProccessType = signalPreType;
            Signal = sg;
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }

        public void Subscribe(Action<object> action)
        {
            SignalBroadcastingEvent signalProcessedEvent = _eventAggregator.GetEvent<SignalBroadcastingEvent>();
            if (subscriptionToken != null)
            {
                signalProcessedEvent.Unsubscribe(subscriptionToken);
            }
            subscriptionToken = signalProcessedEvent.Subscribe(action, ThreadOption.UIThread, true);
        }

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

        private bool isFilter;
        public bool IsFilter
        {
            get { return isFilter; }
            set
            {
                if (isFilter != value)
                {
                    ChangeFilter(isFilter, value);
                    isFilter = value;
                    OnPropertyChanged("IsFilter");
                }
            }
        }

        private SignalPreProccessType signalPreProccessType;
        public SignalPreProccessType SignalPreProccessType
        {
            get { return signalPreProccessType; }
            set
            {
                if (signalPreProccessType != value)
                {
                    ChangeProcessor(signalPreProccessType, value);
                    signalPreProccessType = value;
                    OnPropertyChanged(() => SignalPreProccessType);
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

        public virtual void SetSignal(BaseAlarmSignal sg, SignalPreProccessType signalPreType, bool isfilter)
        {
            signalPreProccessType = signalPreType;//注意大小写，用小写，避免执行ChangeProcessor
            isFilter = isfilter;
            SetSignal(sg);
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

        public virtual void ChangeProcessor(SignalPreProccessType oldsignalPreType, SignalPreProccessType newsignalPreTyp)
        {
            RaisedSiganlChanged();
        }

        public virtual void ChangeFilter(bool oldisFilter, bool newisFilter)
        {
            RaisedSiganlChanged();
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

        public void Open()
        {
            AddProcessor();
            var handler = Opened;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}

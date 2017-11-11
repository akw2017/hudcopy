using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Events
{
    public class EventAggregatorService
    {
        private readonly IEventAggregator _eventAggregator;
        private static EventAggregatorService instance = new EventAggregatorService();
        public EventAggregatorService()
        {
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }
        public static EventAggregatorService Instance { get { return instance; } }

        public IEventAggregator EventAggregator { get { return _eventAggregator; } }
    }
}

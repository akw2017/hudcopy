using AIC.Core.Events;
using AIC.ServiceInterface;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace AIC.LocalConfiguration
{
    public class LocalConfigurationModule : IModule
    {
        private readonly ILocalConfiguration _localConfiguration;
        private readonly IEventAggregator _eventAggregator;

        public LocalConfigurationModule(ILocalConfiguration localConfiguration, IEventAggregator eventAggregator)
        {
            _localConfiguration = localConfiguration;
            _eventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            _localConfiguration.Initialize();
        }
    }
}
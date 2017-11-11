using AIC.ServiceInterface;
using ModuleTracking;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace AIC.SignalCache
{
    public class SignalCacheModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly ISignalCache _signalCache;
        private readonly IEventAggregator _eventAggregator;
        private readonly IModuleTracker _moduleTracker;

        public SignalCacheModule(RegionManager regionManager, 
                                 ISignalCache signalCache,
                                 IEventAggregator eventAggregator,
                                 IModuleTracker moduleTracker)
        {
            _regionManager = regionManager;
            _signalCache = signalCache;
            _eventAggregator = eventAggregator;
            _moduleTracker = moduleTracker;
            _moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleSignalCache);
        }

        public void Initialize()
        {
            try
            {
                _signalCache.Initialize();
                _moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleSignalCache);
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ModuleInitializeExceptionEvent>().Publish(ex);
            }
        }
    }
}
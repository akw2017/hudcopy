using ModuleTracking;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace AIC.Database
{
    public class DatabaseModule : IModule
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IModuleTracker _moduleTracker;
        private readonly IUnitOfWork _unitOfWork;

        public DatabaseModule(IUnitOfWork unitOfWork, 
                              IEventAggregator eventAggregator,
                              IModuleTracker moduleTracker)
        {
            _unitOfWork = unitOfWork;
            _eventAggregator = eventAggregator;
            _moduleTracker = moduleTracker;
            _moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleDatabase);
        }

        public void Initialize()
        {
            try
            {
                //_unitOfWork.Initialize();
                _moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleDatabase);
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ModuleInitializeExceptionEvent>().Publish(ex);
            }
           
        }
    }
}
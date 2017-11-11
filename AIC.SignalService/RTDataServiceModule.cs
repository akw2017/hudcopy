using Prism.Modularity;
using Prism.Regions;
using System;

namespace AIC.RTDataService
{
    public class RTDataServiceModule : IModule
    {
        IRegionManager _regionManager;

        public RTDataServiceModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            
        }
    }
}
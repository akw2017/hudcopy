using Prism.Modularity;
using Prism.Regions;
using System;

namespace AIC.ServiceInterface
{
    public class ServiceInterfaceModule : IModule
    {
        IRegionManager _regionManager;

        public ServiceInterfaceModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            
        }
    }
}
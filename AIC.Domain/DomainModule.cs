using Prism.Modularity;
using Prism.Regions;
using System;

namespace AIC.Domain
{
    public class DomainModule : IModule
    {
        IRegionManager _regionManager;

        public DomainModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
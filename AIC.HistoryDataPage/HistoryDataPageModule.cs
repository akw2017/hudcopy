using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DataPage
{
    public class HistoryDataPageModule : IModule
    {
        private readonly IUnityContainer _container;
        IRegionManager _regionManager;

        public HistoryDataPageModule(RegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {                  

        }
    }
}

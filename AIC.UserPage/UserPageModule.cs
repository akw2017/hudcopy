using AIC.Core;
using AIC.UserPage.Views;
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

namespace AIC.UserPage
{
    public class UserPageModule : IModule
    {
        private readonly IUnityContainer _container;
        IRegionManager _regionManager;

        public UserPageModule(RegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<UserSetView>();  
        }
    }
}

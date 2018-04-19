using AIC.Core;
using AIC.HomePage.Views;
using AICDiagnose.Views;
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

namespace AIC.HomePage
{
    public class AICDiagnoseModule : IModule
    {
        private readonly IUnityContainer _container;
        IRegionManager _regionManager;

        public AICDiagnoseModule(RegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<TabView>();
            _container.RegisterTypeForNavigation<LoginView>();
            _container.RegisterTypeForNavigation<HomeMapView>();
            _container.RegisterTypeForNavigation<ServerSetView>(); 
            _container.RegisterTypeForNavigation<MainRegionView>();
            _regionManager.RegisterViewWithRegion(RegionNames.DiagnoseMainRegionRegion, typeof(DiagnoseMainRegionView));
            _regionManager.RegisterViewWithRegion(RegionNames.MainBodyRegion, typeof(LoginView));

        }
    }
}

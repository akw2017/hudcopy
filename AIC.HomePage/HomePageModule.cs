using AIC.Core;
using AIC.HomePage.Views;
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
using Wpf.CloseTabControl;

namespace AIC.HomePage
{
    public class HomePageModule : IModule
    {
        private readonly IUnityContainer _container;
        IRegionManager _regionManager;

        public HomePageModule(RegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<HomeView>();
            _container.RegisterTypeForNavigation<LoginView>();
            _container.RegisterTypeForNavigation<MapView>();
            _container.RegisterTypeForNavigation<ServerSetView>(); 
            _container.RegisterTypeForNavigation<MainRegionView>();
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegionRegion, typeof(MainRegionView));           
            _regionManager.RegisterViewWithRegion(RegionNames.HomeViewMainRegion, typeof(LoginView));

            //首页默认打开
            IRegion region = this._regionManager.Regions["MainTabRegion"];
            Object viewObj = ServiceLocator.Current.GetInstance<HomeView>();
            ICloseable view = viewObj as ICloseable;
            if (view != null)
            {
                view.Closer.RequestClose += () => region.Remove(view);
            }
            region.Add(view, "首页");
            region.Activate(view);

        }
    }
}

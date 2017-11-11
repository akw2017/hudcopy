using AIC.Core;
using AIC.OnLineDataPage.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System;

namespace AIC.OnLineMonitor
{
    public class OnLineDataPageModule : IModule
    {
        private readonly IUnityContainer _container;
        IRegionManager _regionManager;

        public OnLineDataPageModule(RegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            //_container.RegisterTypeForNavigation<OnLineEquipmentView>();
            //_container.RegisterTypeForNavigation<OnLineMonitorView>();
            //_container.RegisterTypeForNavigation<OnLineTileView>();
            //_container.RegisterTypeForNavigation<OnLineListView>();

            //_regionManager.RegisterViewWithRegion(RegionNames.OnLineMainRegion, typeof(OnLineEquipmentView));
            //_regionManager.RegisterViewWithRegion(RegionNames.MainTabRegion, typeof(OnLineMonitorView));
          
        }
    }
}
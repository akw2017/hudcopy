using AIC.Core;
using AIC.DiagnosePage.Views;
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

namespace AIC.DiagnosePage
{
    public class DiagnosePageModule : IModule
    {
        private readonly IUnityContainer _container;
        IRegionManager _regionManager;

        public DiagnosePageModule(RegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<EditMachComponentView>();
            _container.RegisterTypeForNavigation<EditShaftComponentView>();
            _container.RegisterTypeForNavigation<NullView>();
        }
    }
}

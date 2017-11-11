using AIC.PDA.ViewModels;
using AIC.PDA.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System;

namespace AIC.PDA
{
    public class PDAModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;
        public PDAModule(IUnityContainer container, RegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<NullCardView>();
            _container.RegisterTypeForNavigation<AnalogInCardView>();
            _container.RegisterTypeForNavigation<DigitTachometerCardView>();
            _container.RegisterTypeForNavigation<EddyCurrentDisplacementCardView>();
            _container.RegisterTypeForNavigation<EddyCurrentKeyPhaseCardView>();
            _container.RegisterTypeForNavigation<EddyCurrentTachometerCardView>();
            _container.RegisterTypeForNavigation<IEPECardView>();
            _container.RegisterTypeForNavigation<RelayCardView>();
            _container.RegisterTypeForNavigation<DivFreView>();

            _container.RegisterTypeForNavigation<NullChannelView>();
            _container.RegisterTypeForNavigation<AnalogInChannelView>();
            _container.RegisterTypeForNavigation<DigitTachometerChannelView>();
            _container.RegisterTypeForNavigation<EddyCurrentDisplacementChannelView>();
            _container.RegisterTypeForNavigation<EddyCurrentKeyPhaseChannelView>();
            _container.RegisterTypeForNavigation<EddyCurrentTachometerChannelView>();
            _container.RegisterTypeForNavigation<IEPEChannelView>();
            _container.RegisterTypeForNavigation<RelayChannelView>();

            _container.RegisterTypeForNavigation<CardParameterAllocateView>();

            //_regionManager.RegisterViewWithRegion("MainRegion", typeof(PDAManageView));
            _regionManager.RegisterViewWithRegion("ChannelTreeRegion", typeof(ChannelTreeView));
            _regionManager.RegisterViewWithRegion("PDARegion", typeof(PDAEditorView));
            _regionManager.RegisterViewWithRegion("PDAContentRegion", typeof(PDAParameterView));
            
        }
    }
}
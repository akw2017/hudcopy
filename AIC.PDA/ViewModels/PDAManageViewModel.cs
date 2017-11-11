using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows.Input;

namespace AIC.PDA.ViewModels
{
    public class PDAManageViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private static Uri pdaEditorView = new Uri("PDAEditorView", UriKind.RelativeOrAbsolute);
        private static Uri cardParameterAllocateView = new Uri("CardParameterAllocateView", UriKind.RelativeOrAbsolute);
        public PDAManageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string para)
        {
            if(para== "PDAEditorView")
            {
                _regionManager.RequestNavigate("PDARegion", pdaEditorView);
            }
            else if(para == "CardParameterAllocateView")
            {
                _regionManager.RequestNavigate("PDARegion", cardParameterAllocateView);
            }
        }

        public ICommand NavigateCommand { get; }
    }
}

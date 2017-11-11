using AIC.Core.SignalModels;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.OnLineDataPage.Controllers
{
    public class PropertyController : IPropertyController
    {
        private IRegionManager _regionManager;
      
        public PropertyController(IRegionManager regionManager)
        {
            this._regionManager = regionManager;
            EditCommand = new DelegateCommand<BaseWaveSignal>(OnEditExecuted);
        }

        void OnEditExecuted(BaseWaveSignal parameter)
        {
            //StartEdit(parameter);
        }

        //private void StartEdit(BaseWaveSignal parameter)
        //{
        //    IRegion region = this._regionManager.Regions[RegionNames.PropertyRegion];
        //    var propertyViewModel = region.GetView("PropertyViewModel");
        //    if (propertyViewModel == null)
        //    {
        //        propertyViewModel = ServiceLocator.Current.GetInstance<PropertyViewModel>();
        //        region.Add(propertyViewModel, "PropertyViewModel");

        //        PropertyViewModel viewModel = propertyViewModel as PropertyViewModel;
        //        viewModel.Signal = parameter;
        //        viewModel.CloseViewRequested += delegate
        //        {
        //            region.Remove(viewModel);

        //        };
        //        region.Activate(propertyViewModel);
        //    }
        //    else
        //    {
        //        PropertyViewModel viewModel = propertyViewModel as PropertyViewModel;
        //        viewModel.Signal = parameter;
        //        region.Activate(viewModel);
        //    }
        //}

        #region ILMCommandController Members

        public DelegateCommand<BaseWaveSignal> EditCommand { get; private set; }

        #endregion
    }
}

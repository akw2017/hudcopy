using AIC.CoreType;

using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using Prism.Regions;
using System.Windows.Input;
using Prism.Mvvm;
using AIC.Domain;
using AIC.ServiceInterface;

namespace AIC.PDA.ViewModels
{
    public class DivFreViewModel : BindableBase,INavigationAware
    {
        public DivFreViewModel()
        {
            
        }


        private DivFreWrapper divFre;
        public DivFreWrapper DivFre
        {
            get { return divFre; }
            set
            {
                SetProperty(ref divFre, value);
            }
        }
                                                     
        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            DivFre = navigationContext.Parameters["DivFre"] as DivFreWrapper;
            //if (model != null)
            //{
            //    if (DivFre == null || DivFre.Model != model)
            //    {
            //        DivFre = new DivFreWrapper(model);
            //    }
            //}
            //else
            //{
            //    DivFre = null;
            //}
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion
    }
}

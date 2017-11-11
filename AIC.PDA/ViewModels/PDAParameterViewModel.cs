using AIC.Domain;
using AIC.ServiceInterface;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.PDA.ViewModels
{
    public class PDAParameterViewModel : BindableBase, INavigationAware
    {
        private readonly IPDAService _pdaService;
        public PDAParameterViewModel(IPDAService pdaService)
        {
            _pdaService = pdaService;
            SaveCommand = new DelegateCommand<object>(Save, CanSave);
            ResetCommand = new DelegateCommand<object>(Reset, CanReset);
           
        }

        private void Save(object obj)
        {
            if (PDA != null)
            {
                PDA.AcceptChanges();
                _pdaService.UpdatePDA(PDA.IP);
                InvalidateCommands();
            }
        }
        private bool CanSave(object arg)
        {
            if (PDA == null) return false;
            return PDA.IsChanged && PDA.IsValid;
        }

        private void Reset(object obj)
        {
            PDA.RejectChanges();
        }
        private bool CanReset(object arg)
        {
            if (PDA == null) return false;
            return PDA.IsChanged;
        }

        private void InvalidateCommands()
        {
            SaveCommand.RaiseCanExecuteChanged();
            ResetCommand.RaiseCanExecuteChanged();
        }

        private PDABaseModelWrapper pda;
        public PDABaseModelWrapper PDA
        {
            get { return pda; }
            set
            {
                if (pda != null)
                {
                    pda.PropertyChanged -= Card_PropertyChanged;
                }
                SetProperty(ref pda, value);
                if (pda != null)
                {
                    pda.PropertyChanged += Card_PropertyChanged;
                }
            }
        }

        private void Card_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PDA.IsChanged)
                || e.PropertyName == nameof(PDA.IsValid)
                || e.PropertyName == "")
            {
                InvalidateCommands();
            }
        }

        public DelegateCommand<object> SaveCommand { get; }
        public DelegateCommand<object> ResetCommand { get; }
    

        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var model = navigationContext.Parameters["PDA"] as PDABaseModel;
            if (model != null)
            {
                if (PDA == null || PDA.Model != model)
                {
                    PDA = new PDABaseModelWrapper(model);
                }
            }
            else
            {
                PDA = null;
            }
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

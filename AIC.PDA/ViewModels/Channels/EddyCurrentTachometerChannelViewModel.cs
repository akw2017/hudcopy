using AIC.CoreType;
using AIC.Domain;
using AIC.ServiceInterface;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AIC.PDA.ViewModels
{
    public class EddyCurrentTachometerChannelViewModel : BindableBase, INavigationAware, IConfirmNavigationRequest
    {

        private readonly IPDAService _pdaService;
        public EddyCurrentTachometerChannelViewModel(IPDAService pdaService)
        {
            _pdaService = pdaService;
            SaveCommand = new DelegateCommand<object>(Save, CanSave);
            ResetCommand = new DelegateCommand<object>(Reset, CanReset);
            ConfirmationRequest = new InteractionRequest<IConfirmation>();
        }

        private void Save(object obj)
        {
            if (Channel != null)
            {
                Channel.AcceptChanges();
                _pdaService.UpdateChannel(Channel.Model);
                InvalidateCommands();
            }
        }
        private bool CanSave(object arg)
        {
            if (Channel == null) return false;
            return Channel.IsChanged && Channel.IsValid;
        }

        private void Reset(object obj)
        {
            Channel.RejectChanges();
        }
        private bool CanReset(object arg)
        {
            if (Channel == null) return false;
            return Channel.IsChanged;
        }

        private void InvalidateCommands()
        {
            SaveCommand.RaiseCanExecuteChanged();
            ResetCommand.RaiseCanExecuteChanged();
        }


        private EddyCurrentTachometerChannelWrapper channel;
        public EddyCurrentTachometerChannelWrapper Channel
        {
            get { return channel; }
            set
            {
                if (channel != null)
                {
                    channel.PropertyChanged -= Channel_PropertyChanged;
                }
                SetProperty(ref channel, value);
                if (channel != null)
                {
                    channel.PropertyChanged += Channel_PropertyChanged;
                }
            }
        }

        private void Channel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Channel.IsChanged)
                || e.PropertyName == nameof(Channel.IsValid)
                || e.PropertyName == "")
            {
                InvalidateCommands();
            }
        }

        public DelegateCommand<object> SaveCommand { get; }
        public DelegateCommand<object> ResetCommand { get; }
        public InteractionRequest<IConfirmation> ConfirmationRequest { get; private set; }

        #region IConfirmNavigationRequest
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (Channel == null) return;
            bool result = true;
            if (Channel.IsChanged)
            {
                ConfirmationRequest.Raise(new Confirmation() { Content = "有未提交的修改操作,是否要保存修改", Title = "修改未提交" }, confirm =>
                {
                    if (confirm.Confirmed)
                    {
                        result = true;
                        Save(null);
                    }
                    else
                    {
                        result = false;
                        Channel.RejectChanges();
                    }
                });
            }
            continuationCallback(result);
        }
        #endregion

        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var model = navigationContext.Parameters["Channel"] as EddyCurrentTachometerChannelModel;
            if (model != null)
            {
                if (Channel == null || Channel.Model != model)
                {
                    Channel = new EddyCurrentTachometerChannelWrapper(model);
                }
            }
            else
            {
                Channel = null;
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

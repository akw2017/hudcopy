using AIC.CoreType;
using AIC.Domain;
using AIC.ServiceInterface;
using MoreLinq;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AIC.PDA.ViewModels
{
    public class EddyCurrentKeyPhaseChannelViewModel : BindableBase, INavigationAware, IConfirmNavigationRequest
    {

        private readonly IPDAService _pdaService;
        public EddyCurrentKeyPhaseChannelViewModel(IPDAService pdaService)
        {
            _pdaService = pdaService;
            SaveCommand = new DelegateCommand<object>(Save, CanSave);
            ResetCommand = new DelegateCommand<object>(Reset, CanReset);
            SettingTriggerChannelCommand = new DelegateCommand<object>(SettingTriggerChannel);
            SaveTriggerChannelCommand = new DelegateCommand<object>(SaveTriggerChannel);
            ClearTriggerChannelCommand = new DelegateCommand<object>(ClearTriggerChannel);
            ConfirmationRequest = new InteractionRequest<IConfirmation>();
        }

        private void ClearTriggerChannel(object obj)
        {
            var channel = obj as TriggerChannel;
            if (channel != null)
            {
                channel.IP = string.Empty;
                channel.CardNum = string.Empty;
                channel.ChannelNum = string.Empty;
            }
        }

        private void SaveTriggerChannel(object obj)
        {
            if (Channel != null)
            {
                _pdaService.UpdatePDA(Channel.ChannelId.IP);
            }
        }

        private void SettingTriggerChannel(object obj)
        {
            var triggerChannel = obj as TriggerChannel;
            if (triggerChannel != null)
            {
                if (Channel != null)
                {
                    var channelId = Channel.ChannelId;
                    TriggerChannels
                        .Where(o => o.IP == channelId.IP && o.CardNum == channelId.CardNum && o.ChannelNum == channelId.ChannelNum)
                        .ForEach(t => t.Clear());
                    triggerChannel.IP = channelId.IP;
                    triggerChannel.CardNum = channelId.CardNum;
                    triggerChannel.ChannelNum = channelId.ChannelNum;
                }
            }
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

        private IEnumerable<TriggerChannel> triggerChannels;
        public IEnumerable<TriggerChannel> TriggerChannels
        {
            get { return triggerChannels; }
            set
            {
                if (SetProperty(ref triggerChannels, value))
                    OnPropertyChanged(nameof(IsTriggerChannel));
            }
        }

        public bool IsTriggerChannel => CheckTriggerChannel();

        public bool CheckTriggerChannel()
        {
            if (TriggerChannels != null && Channel != null)
            {
                return TriggerChannels
                    .Where(o => o.IP == Channel.ChannelId.IP && o.CardNum == Channel.ChannelId.CardNum && o.ChannelNum == Channel.ChannelId.ChannelNum)
                    .SingleOrDefault() != null;
            }
            return false;
        }

        private EddyCurrentKeyPhaseChannelWrapper channel;
        public EddyCurrentKeyPhaseChannelWrapper Channel
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
        public DelegateCommand<object> SettingTriggerChannelCommand { get; }
        public DelegateCommand<object> SaveTriggerChannelCommand { get; }
        public DelegateCommand<object> ClearTriggerChannelCommand { get; }
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
            var model = navigationContext.Parameters["Channel"] as EddyCurrentKeyPhaseChannelModel;
            if (model != null)
            {
                if (Channel == null || Channel.Model != model)
                {
                    Channel = new EddyCurrentKeyPhaseChannelWrapper(model);
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

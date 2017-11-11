using AIC.CoreType;
using AIC.Domain;
using AIC.PDA.Events;
using AIC.ServiceInterface;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Linq;
using Prism.Interactivity.InteractionRequest;

namespace AIC.PDA.ViewModels
{
    public class EddyCurrentDisplacementCardViewModel : BindableBase, INavigationAware, IConfirmNavigationRequest
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IPDAService _pdaService;
        
        public EddyCurrentDisplacementCardViewModel(IEventAggregator eventAggregator,IPDAService pdaService)
        {
            _eventAggregator = eventAggregator;
            _pdaService = pdaService;
            SaveCommand = new DelegateCommand<object>(Save, CanSave);
            ResetCommand = new DelegateCommand<object>(Reset, CanReset);
            _eventAggregator.GetEvent<TriggerChannelChangedEvent>().Subscribe(OnTriggerChannelChanged);
            ConfirmationRequest = new InteractionRequest<IConfirmation>();
        }

        private void OnTriggerChannelChanged(object obj)
        {
            GetTriggerNum();
        }

        private void GetTriggerNum()
        {
            if(Card!=null)
            {
                TriggerChannels = _pdaService.GetTriggerChannels(Card.CardId.IP);
                if (TriggerChannels != null)
                {
                    SelectedTriggerChannel = TriggerChannels.Where(o => o.CardNum == Card.TriggerCardNum && o.ChannelNum == Card.TriggerChannelNum).SingleOrDefault();
                }
                else
                {
                    SelectedTriggerChannel = null;
                }
            }
        }

        private IEnumerable<TriggerChannel> triggerChannels;
        public IEnumerable<TriggerChannel> TriggerChannels
        {
            get { return triggerChannels; }
            set { SetProperty(ref triggerChannels, value); }
        }

        private TriggerChannel selectedTriggerChannel;
        public TriggerChannel SelectedTriggerChannel
        {
            get { return selectedTriggerChannel; }
            set {
                if(SetProperty(ref selectedTriggerChannel, value))
                {
                    Card.TriggerCardNum = value != null ? value.CardNum : string.Empty;
                    Card.TriggerChannelNum = value != null ? value.ChannelNum : string.Empty;
                }
            }
        }

        private void Save(object obj)
        {
            if (Card != null)
            {
                Card.AcceptChanges();
                _pdaService.UpdateCard(Card.Model);
                InvalidateCommands();
            }
        }
        private bool CanSave(object arg)
        {
            if (Card == null) return false;
            return Card.IsChanged && Card.IsValid;
        }

        private void Reset(object obj)
        {
            Card.RejectChanges();
        }
        private bool CanReset(object arg)
        {
            if (Card == null) return false;
            return Card.IsChanged;
        }

        private void InvalidateCommands()
        {
            SaveCommand.RaiseCanExecuteChanged();
            ResetCommand.RaiseCanExecuteChanged();
        }


        private EddyCurrentDisplacementCardWrapper card;
        public EddyCurrentDisplacementCardWrapper Card
        {
            get { return card; }
            set
            {
                if (card != null)
                {
                    card.PropertyChanged -= Card_PropertyChanged;
                }
                SetProperty(ref card, value);
                if (card != null)
                {
                    card.PropertyChanged += Card_PropertyChanged;
                }
            }
        }

        private void Card_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Card.IsChanged)
                || e.PropertyName == nameof(Card.IsValid)
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
            if (Card == null) return;
            bool result = true;
            if (Card.IsChanged)
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
                        Card.RejectChanges();
                    }
                });
            }
            continuationCallback(result);
        }
        #endregion

        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var model = navigationContext.Parameters["Card"] as EddyCurrentDisplacementCardModel;
            if (model != null)
            {
                if (Card == null || Card.Model != model)
                {
                    Card = new EddyCurrentDisplacementCardWrapper(model);
                    GetTriggerNum();
                }
            }
            else
            {
                Card = null;
                TriggerChannels = null;
                SelectedTriggerChannel = null;
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

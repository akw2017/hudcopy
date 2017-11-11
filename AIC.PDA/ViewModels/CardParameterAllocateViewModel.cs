using AIC.Domain;
using AIC.PDA.Events;

using AIC.ServiceInterface;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AIC.PDA.ViewModels
{
    public class CardParameterAllocateViewModel : BindableBase
    {
        private readonly IPDAService _pdaService;
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<BaseCardModel> cardCollection;

        private ObservableCollection<BaseCardModel> allocatedCardCollection;
        public CardParameterAllocateViewModel(IEventAggregator eventAggregator, IPDAService pdaService)
        {
            _eventAggregator = eventAggregator;
            _pdaService = pdaService;
            cardCollection = new ObservableCollection<BaseCardModel>();
            cardCollection.AddRange(_pdaService.GetCards());

            allocatedCardCollection = new ObservableCollection<BaseCardModel>();

            CopyCardParameterCommand = new DelegateCommand<object>(CopyCardParameter, CanCopyCardParameter);
          //  AllocateCard = cardCollection.First();
          //  allocatedCardCollection.CollectionChanged += AllocatedCardCollection_CollectionChanged;

            _eventAggregator.GetEvent<SelectedCardChangedEvent>().Subscribe(OnSelectedCardChanged);
        }


        private void CopyCardParameter(object obj)
        {
            var list = new List<BaseCardModel>();
            foreach (var item in allocatedCardCollection)
            {
                if (item.GetType() == AllocateCard.GetType())
                {
                    if (item.Count == AllocateCard.Count)
                    {
                        list.Add(item);
                    }
                }
            }  

            foreach (var card in list)
            {
                AllocateCard.CopyTo(card);
                _pdaService.UpdateCard(card);
                foreach (var channel in AllocateCard.Channels)
                {
                    var targetChannel = card.Channels.Where(o => o.ChannelId.ChannelNum == channel.ChannelId.ChannelNum).SingleOrDefault();
                    if(targetChannel != null)
                    {
                        channel.CopyTo(targetChannel);
                        _pdaService.UpdateChannel(targetChannel);
                    }
                }
            }
        }
        

        private bool CanCopyCardParameter(object arg)
        {
            return AllocateCard != null;
        }

        private void AllocatedCardCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
        }

        private void OnSelectedCardChanged(Tuple<string, string> arg)
        {
            SelectedCard = cardCollection.Where(o => o.CardId.IP == arg.Item1 && o.CardId.CardNum == arg.Item2).SingleOrDefault();
        }

        public DelegateCommand<object> CopyCardParameterCommand { get; private set; }

        public IEnumerable<BaseCardModel> Cards { get { return cardCollection; } }
        public IEnumerable<BaseCardModel> AllocatedCards { get { return allocatedCardCollection; } }

        private BaseCardModel selectedCard;
        public BaseCardModel SelectedCard
        {
            get { return selectedCard; }
            set
            {
                if (selectedCard != value)
                {
                    selectedCard = value;
                    OnPropertyChanged("SelectedCard");
                }
            }
        }

        private BaseCardModel allocateCard;
        public BaseCardModel AllocateCard
        {
            get { return allocateCard; }
            set
            {
                if (allocateCard != value)
                {
                    if(allocateCard!=null)
                    {
                        if (!cardCollection.Contains(allocateCard) && !allocatedCardCollection.Contains(allocateCard))
                        {
                            cardCollection.Add(allocateCard);
                        }
                    }
                    allocateCard = value;
                    OnPropertyChanged("AllocateCard");
                    CopyCardParameterCommand.RaiseCanExecuteChanged();
                }
            }
        }
        
    }
}

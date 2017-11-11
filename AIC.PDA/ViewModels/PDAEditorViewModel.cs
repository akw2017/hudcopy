using AIC.Domain;
using AIC.PDA.Events;
using AIC.PDA.Models;

using AIC.ServiceInterface;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AIC.PDA.ViewModels
{
    public class PDAEditorViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPDAService _pdaService;
        private ObservableCollection<PDABaseModel> pdaCollection;

        private static Uri pdaParameterView = new Uri("PDAParameterView", UriKind.RelativeOrAbsolute);

        private static Uri nullCardView = new Uri("NullCardView", UriKind.Relative);
        private static Uri analogInCardView = new Uri("AnalogInCardView", UriKind.RelativeOrAbsolute);
        private static Uri digitTachometerCardView = new Uri("DigitTachometerCardView", UriKind.RelativeOrAbsolute);
        private static Uri eddyCurrentDisplacementCardView = new Uri("EddyCurrentDisplacementCardView", UriKind.RelativeOrAbsolute);
        private static Uri eddyCurrentKeyPhaseCardView = new Uri("EddyCurrentKeyPhaseCardView", UriKind.RelativeOrAbsolute);
        private static Uri eddyCurrentTachometerCardView = new Uri("EddyCurrentTachometerCardView", UriKind.RelativeOrAbsolute);
        private static Uri iepeCardView = new Uri("IEPECardView", UriKind.RelativeOrAbsolute);
        private static Uri relayCardView = new Uri("RelayCardView", UriKind.RelativeOrAbsolute);

        private static Uri nullChannelView = new Uri("NullChannelView", UriKind.Relative);
        private static Uri analogInChannelView = new Uri("AnalogInChannelView", UriKind.RelativeOrAbsolute);
        private static Uri digitTachometerChannelView = new Uri("DigitTachometerChannelView", UriKind.RelativeOrAbsolute);
        private static Uri eddyCurrentDisplacementChannelView = new Uri("EddyCurrentDisplacementChannelView", UriKind.RelativeOrAbsolute);
        private static Uri eddyCurrentKeyPhaseChannelView = new Uri("EddyCurrentKeyPhaseChannelView", UriKind.RelativeOrAbsolute);
        private static Uri eddyCurrentTachometerChannelView = new Uri("EddyCurrentTachometerChannelView", UriKind.RelativeOrAbsolute);
        private static Uri iepeChannelView = new Uri("IEPEChannelView", UriKind.RelativeOrAbsolute);
        private static Uri relayChannelView = new Uri("RelayChannelView", UriKind.RelativeOrAbsolute);


        private static Uri vibrationChannelView = new Uri("VibrationChannelView", UriKind.Relative);
        private static Uri analogChannelView = new Uri("AnalogChannelView", UriKind.Relative);
    
        public PDAEditorViewModel(IRegionManager regionManager, IEventAggregator eventAggregator,IPDAService pdaService)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _pdaService = pdaService;
            pdaCollection = new ObservableCollection<PDABaseModel>(_pdaService.GetPDAs());
            DownloadingCommand = new DelegateCommand<object>(Downloading);
            _eventAggregator.GetEvent<SelectedChannelChangedEvent>().Subscribe(OnSelectedChannelChanged);
        }

        private void Downloading(object obj)
        {
            var pdaModel = obj as PDABaseModel;
            if (pdaModel != null)
            {
                var data = GenerateDownloadingData(pdaModel);
                //string serverIP = ServerAddress.CTLAddress.Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(':')[0];
                //int serverPort = int.Parse(ServerAddress.CTLAddress.Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(':')[1]);
                //var command = new IncentiveCommand()
                //{
                //    IP = TriggerIP,
                //    Port = TriggerPort,
                //    ServerIP = serverIP,
                //    ServerPort = serverPort
                //};

                // var result = await Task.Run(() => (string)WCFCaller<IStorageManagement>.ExecuteMethod(ServerAddress.CTLAddress, "SendCommandFromWCFToPDA2", command));
                //WCFCaller<IStorageManagement>.ExecuteMethod(ServerAddress.CTLAddress, "SendCommandFromWCFToPDA2", command)
                //return Result.Ok(result);
            }
        }

        public DownloadingData GenerateDownloadingData(PDABaseModel pdaModel)
        {
            DownloadingData data = new DownloadingData();
            data.SetPDA(pdaModel.PDA);

            foreach (var card in pdaModel.Cards)
            {
                if (card is AnalogInCardModel)
                {
                    data.Add(((AnalogInCardModel)card).Card);
                    foreach (var channel in card.Channels.OfType<AnalogInChannelModel>())
                    {
                        data.Add(channel.Channel);
                    }
                }
                else if (card is DigitTachometerCardModel)
                {
                    data.Add(((DigitTachometerCardModel)card).Card);
                    foreach (var channel in card.Channels.OfType<DigitTachometerChannelModel>())
                    {
                        data.Add(channel.Channel);
                    }
                }
                else if (card is EddyCurrentDisplacementCardModel)
                {
                    data.Add(((EddyCurrentDisplacementCardModel)card).Card);
                    foreach (var channel in card.Channels.OfType<EddyCurrentDisplacementChannelModel>())
                    {
                        data.Add(channel.Channel);
                    }
                }
                else if (card is EddyCurrentKeyPhaseCardModel)
                {
                    data.Add(((EddyCurrentKeyPhaseCardModel)card).Card);
                    foreach (var channel in card.Channels.OfType<EddyCurrentKeyPhaseChannelModel>())
                    {
                        data.Add(channel.Channel);
                    }
                }
                else if (card is EddyCurrentTachometerCardModel)
                {
                    data.Add(((EddyCurrentTachometerCardModel)card).Card);
                    foreach (var channel in card.Channels.OfType<EddyCurrentTachometerChannelModel>())
                    {
                        data.Add(channel.Channel);
                    }
                }
                else if (card is IEPECardModel)
                {
                    data.Add(((IEPECardModel)card).Card);
                    foreach (var channel in card.Channels.OfType<IEPEChannelModel>())
                    {
                        data.Add(channel.Channel);
                    }
                }
                else if (card is RelayCardModel)
                {
                    data.Add(((RelayCardModel)card).Card);
                    foreach (var channel in card.Channels.OfType<RelayChannelModel>())
                    {
                        data.Add(channel.Channel);
                    }
                }
            }
            return data;
        }

        private void OnSelectedChannelChanged(ChannelIdentity id)
        {
            SelectedPDA = pdaCollection.Where(o => o.IP == id.IP).Single();
            SelectedCard = SelectedPDA.Cards.Where(o => o.CardId.CardNum == id.CardNum).Single();
            //SelectedChannel= SelectedIEPECard.
            if (SelectedCard is IEPECardModel)
            {
                SelectedChannel = SelectedCard.Channels.Where(o => o.ChannelId.IP == id.IP && o.ChannelId.CardNum == id.CardNum && o.ChannelId.ChannelNum == id.ChannelNum).Single();
            }
            else if (SelectedCard is AnalogInCardModel)
            {
                SelectedChannel = SelectedCard.Channels.Where(o => o.ChannelId.IP == id.IP && o.ChannelId.CardNum == id.CardNum && o.ChannelId.ChannelNum == id.ChannelNum).Single();
            }
        }

        public IEnumerable<PDABaseModel> PDAs { get { return pdaCollection; } }

        public ICommand DownloadingCommand { get; }

        private PDABaseModel selectedPDA;
        public PDABaseModel SelectedPDA
        {
            get { return selectedPDA; }
            set
            {
                if (selectedPDA != value)
                {
                    selectedPDA = value;
                    OnPropertyChanged("SelectedPDA");
                }
            }
        }

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
                    SelectedChannel = null;
                }
            }
        }

        private BaseChannelModel selectedChannel;
        public BaseChannelModel SelectedChannel
        {
            get { return selectedChannel; }
            set
            {
                if (selectedChannel != value)
                {
                    selectedChannel = value;
                    OnPropertyChanged("SelectedChannel");
                    NavigateToChannel(value);
                }
            }
        }

        private bool? showAsCard=true;
        public bool? ShowAsCard
        {
            get { return showAsCard; }
            set { SetProperty(ref showAsCard, value); }
        }

        private State state;
        public State State
        {
            get { return state; }
            set { SetProperty(ref state, value); }
        }
        
        public void ChangeStateToCard()
        {
            State = State.Card;
        }

        public void ChangeStateToChannel()
        {
            State = State.Channel;
           // NavigateCard(SelectedCard);
        }

        public void NavigateToPDA()
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("PDA", SelectedPDA);
            _regionManager.RequestNavigate("PDAContentRegion", pdaParameterView, navigationParameters);
        }

        public void  NavigateToCard()
        {
            if (SelectedCard == null || SelectedCard == BaseCardModel.Null)
            {
                _regionManager.RequestNavigate("CardContentRegion", nullCardView);
            }
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("Card", SelectedCard);
            if (SelectedCard is AnalogInCardModel)
            {
                _regionManager.RequestNavigate("CardContentRegion", analogInCardView, navigationParameters);
            }
            else if (SelectedCard is DigitTachometerCardModel)
            {
                _regionManager.RequestNavigate("CardContentRegion", digitTachometerCardView, navigationParameters);
            }
            else if (SelectedCard is EddyCurrentDisplacementCardModel)
            {
                _regionManager.RequestNavigate("CardContentRegion", eddyCurrentDisplacementCardView, navigationParameters);
            }
            else if (SelectedCard is EddyCurrentKeyPhaseCardModel)
            {
                _regionManager.RequestNavigate("CardContentRegion", eddyCurrentKeyPhaseCardView, navigationParameters);
            }
            else if (SelectedCard is EddyCurrentTachometerCardModel)
            {
                _regionManager.RequestNavigate("CardContentRegion", eddyCurrentTachometerCardView, navigationParameters);
            }
            else if (SelectedCard is IEPECardModel)
            {
                _regionManager.RequestNavigate("CardContentRegion", iepeCardView, navigationParameters);
            }
            else if (SelectedCard is RelayCardModel)
            {
                _regionManager.RequestNavigate("CardContentRegion", relayCardView, navigationParameters);
            }
        }

        private void NavigateToChannel(BaseChannelModel channel)
        {
            if (channel == null)
            {
                _regionManager.RequestNavigate("ChannelContentRegion", nullChannelView);
                return;
            }

            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("Channel", channel);
            if (channel is AnalogInChannelModel)
            {
                _regionManager.RequestNavigate("ChannelContentRegion", analogInChannelView, navigationParameters);
            }
            else if (channel is DigitTachometerChannelModel)
            {
                _regionManager.RequestNavigate("ChannelContentRegion", digitTachometerChannelView, navigationParameters);
            }
            else if (channel is EddyCurrentDisplacementChannelModel)
            {
                _regionManager.RequestNavigate("ChannelContentRegion", eddyCurrentDisplacementChannelView, navigationParameters);
            }
            else if (channel is EddyCurrentKeyPhaseChannelModel)
            {
                _regionManager.RequestNavigate("ChannelContentRegion", eddyCurrentKeyPhaseChannelView, navigationParameters);
            }
            else if (channel is EddyCurrentTachometerChannelModel)
            {
                _regionManager.RequestNavigate("ChannelContentRegion", eddyCurrentTachometerChannelView, navigationParameters);
            }
            else if (channel is IEPEChannelModel)
            {
                _regionManager.RequestNavigate("ChannelContentRegion", iepeChannelView, navigationParameters);
            }
            else if (channel is RelayChannelModel)
            {
                _regionManager.RequestNavigate("ChannelContentRegion", relayChannelView, navigationParameters);
            }
        }
    }

    public enum State
    {
        PDA,
        Card,
        Channel
    }
}

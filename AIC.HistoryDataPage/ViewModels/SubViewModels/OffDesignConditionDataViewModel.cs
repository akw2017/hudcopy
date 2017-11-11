using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using AIC.CoreType;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Commands;
using AIC.HistoryDataPage.Models;

namespace AIC.HistoryDataPage.ViewModels
{
    //public class OffDesignConditionObject
    //{
    //    private event EventHandler<ChannelDataChangedEventArgs> channelDataChanged;
    //    public ChannelToken ChannelToken { get; set; }
    //    public void RaiseChannelDataChanged(IEnumerable<ChannelToken> tokens)
    //    {
    //        if (channelDataChanged != null)
    //        {
    //            channelDataChanged(this, new ChannelDataChangedEventArgs(tokens));
    //        }
    //    }
    //}

    public class OffDesignConditionDataViewModel : HistoricalDataViewModel
    {
        private  ObservableCollection<ChannelToken> contractsCollection;
        //htzk123
        //private DataModelProvider.DataModelProvider _dataModelProvider;

        //public OffDesignConditionDataViewModel(DataModelProvider.DataModelProvider dataModelProvider)
        //{
        //    this._dataModelProvider = dataModelProvider;

        //    contractsCollection = new ObservableCollection<ChannelToken>();
        //    DisplayMode = SignalDisplayType.OffDesignCondition;
        //}

        public OffDesignConditionDataViewModel()
        {
            contractsCollection = new ObservableCollection<ChannelToken>();
            DisplayMode = SignalDisplayType.OffDesignCondition;
        }

        public void AddChannel(ChannelToken channel)
        {
            if (!contractsCollection.Contains(channel))
            {
                contractsCollection.Add(channel);
            }
            
        }
        public void RemoveChannel(ChannelToken channel)
        {
            if (channel is BaseWaveChannelToken)
            {
                ((BaseWaveChannelToken)channel).RaiseDisposed();
            }
            else if (channel is DivFreChannelToken)
            {
                ((DivFreChannelToken)channel).RaiseDisposed();
            }

            if (contractsCollection.Contains(channel))
            {
                contractsCollection.Remove(channel);
            }
        }
        public void ChannelDataChanged(IEnumerable<ChannelToken> channelTokens)
        {
            foreach(var channel in channelTokens)
            {
                if (channel is BaseWaveChannelToken)
                {
                    ((BaseWaveChannelToken)channel).RaiseDataChanged();
                }
                else if (channel is DivFreChannelToken)
                {
                    ((DivFreChannelToken)channel).RaiseDataChanged();
                }
            }
        }

        private ChannelToken selectedOffCondition;
        public ChannelToken SelectedOffCondition
        {
            get { return selectedOffCondition; }
            set {
                if (selectedOffCondition != value)
                {
                    selectedOffCondition = value;
                    OnPropertyChanged("SelectedOffCondition");
                }
            }
        }

        public IEnumerable<ChannelToken> Channels { get { return contractsCollection; } }

        public DelegateCommand<object> CalculateFittingCurveCommand { get; private set; }
        public IAsyncCommand MediatorFittingCoeffCommand { get; private set; }
        
    }
}

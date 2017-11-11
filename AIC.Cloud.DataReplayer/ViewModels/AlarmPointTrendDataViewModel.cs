using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;

using Microsoft.Practices.Prism.ViewModel;

using AIC.CoreType;
using AIC.Cloud.Domain;
using Arction.WPF.LightningChartUltimate;
using System.Collections.ObjectModel;
using AIC.Server.Storage.Contract;
using AIC.Cloud.Database;
using Microsoft.Practices.Prism.Commands;
using AIC.Server.Common;
using Newtonsoft.Json;
using AIC.Cloud.Applications;
using System.Threading.Tasks.Dataflow;
using System.Threading.Tasks;
using Nito.AsyncEx;
using System.Threading;
using AIC.Cloud.Applications.Services;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using AIC.Cloud.Applications.Events;
//using Nito.AsyncEx;
//using Nito.AsyncEx;

namespace AIC.Cloud.DataReplayer.ViewModels
{
    public class SnapshotDataChangedEventArgs : EventArgs
    {
        private SnapshotAMSContract2 _contract;

        public SnapshotDataChangedEventArgs(SnapshotAMSContract2 contract)
        {
            _contract = contract;
        }
        public SnapshotAMSContract2 Contract { get { return _contract; } }
    }

    public class AlarmPointTrendDataViewModel : HistoricalDataViewModel
    {   
        private event EventHandler<SnapshotDataChangedEventArgs> snapshotDataChanged;
        private event EventHandler showTimeOrFrequency;
        private ObservableCollection<VibrationChannelToken> contractsCollection;

        public AlarmPointTrendDataViewModel()
        {
            DisplayMode = SignalDisplayType.AlarmPointTrend;
            ShowGraphCommand = AsyncCommand.Create(OnShowGraph);
            contractsCollection = new ObservableCollection<VibrationChannelToken>();            
        }

        public void AddChannel(VibrationChannelToken channel)
        {
            if (!contractsCollection.Contains(channel))
            {
                contractsCollection.Add(channel);
            }
        }

        public void RemoveChannel(VibrationChannelToken channel)
        {
            if (contractsCollection.Contains(channel))
            {
                contractsCollection.Remove(channel);
            }
        }

        public async Task ChangeSnapshotData(IEnumerable<VibrationChannelToken> tokens)
        {
            if (SelecetedSeriesToken == null)
            {
                SelecetedSeriesToken = contractsCollection.FirstOrDefault();
            }
            if (SelecetedSeriesToken != null)
            {
                if (SelecetedSeriesToken.CurrentIndex != -1)
                {
                    string ipAddress = ServerAddress.CTLAddress.Split('/')[2].Split(':')[0];
                    VInfoTableAMSContract vInfoAMS = SelecetedSeriesToken.DataContracts[SelecetedSeriesToken.CurrentIndex];
                    LinqWhereHelper helper = new LinqWhereHelper();
                    helper.AddCondition("ChannelGlobalIndex", "=", SelecetedSeriesToken.Channel.ChannelGlobalIndex);
                    helper.AddCondition("STIME", "=", vInfoAMS.Date);
                    var database = await DatabaseComponent.Instance;
                    var contracts = await Task.Run(() => database.QuerySnapshot(ipAddress, helper, vInfoAMS.Date));
                    var contact = contracts.SingleOrDefault();
                    RaiseSnapshotDataChanged(contact);
                }
            }
        }

        private async Task OnShowGraph()
        {
            try
            {
                string ipAddress = ServerAddress.CTLAddress.Split('/')[2].Split(':')[0];
                LinqWhereHelper helper = new LinqWhereHelper();
                var dateTime = SelecetedSeriesToken.DataContracts[SelecetedSeriesToken.CurrentIndex].Date;
                helper.AddCondition("ChannelGlobalIndex", "=", SelecetedSeriesToken.Channel.ChannelGlobalIndex);
                helper.AddCondition("STIME", "=", dateTime);
                var response = await Task.Run(() => SocketCaller.ExecuteMethod(ipAddress, 39997, "Snapshot_Query", 1, true, null, helper.ToString(), dateTime, helper.Values));
                if (!response[0].StartsWith("#"))
                {
                    var contracts = JsonConvert.DeserializeObject<List<SnapshotContract2>>(response[0]);
                    if (contracts != null)
                    {
                        CurrentSnapshotContract = contracts[0];
                    }
                    else
                    {
                        throw new Exception("SnapshotContract2 反序列化失败 NULL");
                    }
                }
                else
                {
                    throw new Exception(response[0]);
                }

                if (CurrentSnapshotContract != null)
                {
                    RaiseShowTimeOrFrequency();
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-报警点趋势", ex));
            }
        }

        public void RaiseShowTimeOrFrequency()
        {
            if (showTimeOrFrequency != null)
            {
                showTimeOrFrequency(this, EventArgs.Empty);
            }
        }
        public void RaiseSnapshotDataChanged(SnapshotAMSContract2 contact)
        {
            if (snapshotDataChanged != null)
            {
                snapshotDataChanged(this, new SnapshotDataChangedEventArgs(contact));
            }
        }

        public IObservable<SnapshotAMSContract2> WhenSnapshotDataChanged
        {
            get
            {
                return Observable
                    .FromEventPattern<SnapshotDataChangedEventArgs>(
                        h => this.snapshotDataChanged += h,
                        h => this.snapshotDataChanged -= h)
                    .Select(x => x.EventArgs.Contract);
            }
        }
        public IObservable<object> WhenShowTimeOrFrequency
        {
            get
            {
                return Observable
                    .FromEventPattern(
                        h => this.showTimeOrFrequency += h,
                        h => this.showTimeOrFrequency -= h);
            }
        }

        private VibrationChannelToken selecetedSeriesToken;
        public VibrationChannelToken SelecetedSeriesToken
        {
            get { return selecetedSeriesToken; }
            set
            {
                if (selecetedSeriesToken != value)
                {
                    selecetedSeriesToken = value;
                    OnPropertyChanged(() => SelecetedSeriesToken);
                    CurrentSnapshotContract = null;
                }
            }
        }
        public SnapshotContract2 CurrentSnapshotContract { get; set; }
        public IAsyncCommand ShowGraphCommand { get; set; }
        public IEnumerable<VibrationChannelToken> ContractsCollection { get { return contractsCollection; } }
    }
}

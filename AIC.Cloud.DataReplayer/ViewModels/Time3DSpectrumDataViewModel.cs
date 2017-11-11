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
using System.Threading.Tasks.Dataflow;
using System.Threading.Tasks;
using System.Threading;
using Nito.AsyncEx;

namespace AIC.Cloud.DataReplayer.ViewModels
{
    public class Time3DSpectrumDataViewModel : HistoricalDataViewModel
    {
        private ObservableCollection<VibrationChannelToken> contractsCollection;
        public Time3DSpectrumDataViewModel()
        {
            contractsCollection = new ObservableCollection<VibrationChannelToken>();
            DisplayMode = SignalDisplayType.Time3DSpectrum;
        }

        private async Task PastVibrationDatas(IEnumerable<VibrationChannelToken> tokens)
        {
            if (tokens == null) return;
            var tcs = new TaskCompletionSource();
            ValueAsync = NotifyTaskCompletion.Create(tcs.Task);
            try
            {
                foreach (var token in tokens)
                {
                    if (!contractsCollection.Contains(token))
                    {
                        contractsCollection.Add(token);
                    }
                }
                if (SelecetedChannel == null)
                {
                    SelecetedChannel = contractsCollection.FirstOrDefault();
                }

                if (SelecetedChannel != null)
                {
                    if (SelecetedChannel.VData == null)
                    {
                        
                    }
                    else
                    {
                        
                    }
                }
                tcs.SetResult();
                await ValueAsync.TaskCompleted;
            }
            catch (Exception e)
            {
                tcs.SetException(e);
            }
        }
        public void RemoveChannel(VibrationChannelToken channel)
        {
            if (contractsCollection.Contains(channel))
            {
                contractsCollection.Remove(channel);
            }
        }

        private INotifyTaskCompletion valueAsync;
        public INotifyTaskCompletion ValueAsync
        {
            get { return valueAsync; }
            set
            {
                valueAsync = value;
                OnPropertyChanged(() => ValueAsync);
            }
        }

        private VibrationChannelToken selecetedChannel;
        public VibrationChannelToken SelecetedChannel
        {
            get { return selecetedChannel; }
            set
            {
                if (selecetedChannel != value)
                {
                    selecetedChannel = value;
                    OnPropertyChanged(() => SelecetedChannel);
                }
            }
        }

        private int sizeX = 100;
        public int SizeX
        {
            get { return sizeX; }
            set
            {
                if (sizeX != value)
                {
                    sizeX = value;
                    OnPropertyChanged("SizeX");
                }
            }
        }

        private int sizeY = 100;
        public int SizeY
        {
            get { return sizeY; }
            set
            {
                if (sizeY != value)
                {
                    sizeY = value;
                    OnPropertyChanged("SizeY");
                }
            }
        }

        public IEnumerable<VibrationChannelToken> Channels { get { return contractsCollection; } }

        #region Property ShowDetail
        private bool showDetail = true;
        public bool ShowDetail
        {
            get { return showDetail; }
            set
            {
                if (showDetail != value)
                {
                    showDetail = value;
                    this.OnPropertyChanged(() => ShowDetail);
                }
            }
        }
        #endregion
    }
}

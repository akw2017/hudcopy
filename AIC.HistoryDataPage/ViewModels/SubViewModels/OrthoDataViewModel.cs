using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using AIC.CoreType;
using Nito.AsyncEx;
using System.Threading;
using Prism.Commands;
using AIC.HistoryDataPage.Models;
using AIC.Core.Events;

namespace AIC.HistoryDataPage.ViewModels
{
    public class OrthoDataChangedEventArgs : EventArgs
    {
        private double[] _xArray;
        private double[] _yArray;

        public OrthoDataChangedEventArgs(double[] xArray, double[] yArray)
        {
            _xArray = xArray;
            _yArray = yArray;
        }

        public double[] XArray { get { return _xArray; } }
        public double[] YArray { get { return _yArray; } }
    }

    public class OrthoDataViewModel : HistoricalDataViewModel
    {
        private ObservableCollection<BaseWaveChannelToken> xChannelCollection = new ObservableCollection<BaseWaveChannelToken>();
        private ObservableCollection<ChannelDistributionContract> yChannelCollection = new ObservableCollection<ChannelDistributionContract>();
        private event EventHandler<OrthoDataChangedEventArgs> orthoDataChanged;
      
        public OrthoDataViewModel()
        {
            DisplayMode = SignalDisplayType.Ortho;
            SelecetXChannelCommand = new DelegateCommand<object>(SelecetXChannel);
           
        }

        public void AddChannel(ChannelToken channel)
        {
            if (!xChannelCollection.Contains(channel))
            {
                xChannelCollection.Add(channel as BaseWaveChannelToken);
            }
        }

        public void RemoveChannel(ChannelToken channel)
        {
            if (xChannelCollection.Contains(channel))
            {
                xChannelCollection.Remove(channel as BaseWaveChannelToken);
            }
        }

        public async Task ChangeOrthoData(IEnumerable<BaseWaveChannelToken> tokens)
        {
            double[] xArray = new double[0];
            double[] yArray = new double[0];
            try
            {
                if (SelecetedXChannel == null)
                {
                    SelecetedXChannel = xChannelCollection.FirstOrDefault();
                }

                if (SelecetedXChannel != null && SelecetedYChannel != null)
                {
                    if (SelecetedXChannel.VData != null && SelecetedXChannel.VData.Waveform != null)
                    {
                        xArray = SelecetedXChannel.VData.Waveform;
                        //htzk123
                        //int globalIndex = SelecetedYChannel.ChannelGlobalIndex;
                        //Guid saveLab = SelecetedXChannel.VData.SaveLab;
                        //LinqWhereHelper helper = new LinqWhereHelper();
                        //helper.AddCondition("SaveLab", "=", saveLab);
                        //string ipAddress = ServerAddress.CTLAddress.Split('/')[2].Split(':')[0];
                        //var database = await DatabaseComponent.Instance;
                        //var contracts = await Task.Run(() => database.QueryVInfoTable(ipAddress, globalIndex, helper, SelecetedXChannel.VData.STIME, SelecetedXChannel.VData.STIME));
                        //if (contracts.Length == 1)
                        //{
                        //    var yVInfo = contracts.Single();
                        //    int length = yVInfo.VData.Length / 4;
                        //    if (xArray.Length != length)
                        //    {
                        //        throw new Exception(string.Format("X向与Y向数组长度不等,X:{0},Y:{1}", xArray.Length, length));
                        //    }
                        //    else
                        //    {
                        //        yArray = new double[length];
                        //        for (int i = 0; i < length; i++)
                        //        {
                        //            yArray[i] = BitConverter.ToSingle(yVInfo.VData, i * 4);
                        //        }
                        //    }
                        //}
                        //else if (contracts.Length > 0)
                        //{
                        //    throw new Exception(string.Format("存在多个Y轴数据,count:{0}", contracts.Length));
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-轴心轨迹", ex));
            }
            finally
            {
                RaiseOrthoDataChanged(xArray, yArray);
            }
        }

        public void RaiseOrthoDataChanged(double[] xArray, double[] yArray)
        {
            if (orthoDataChanged != null)
            {
                orthoDataChanged(this, new OrthoDataChangedEventArgs(xArray, yArray));
            }
        }

        public IObservable<OrthoDataChangedEventArgs> WhenOrthoDataChanged
        {
            get
            {
                return Observable
                    .FromEventPattern<OrthoDataChangedEventArgs>(
                        h => this.orthoDataChanged += h,
                        h => this.orthoDataChanged -= h)
                    .Select(x => x.EventArgs);
            }
        }


        private async void SelecetXChannel(object args)
        {
            await SelecetXChannelAsync(args);
        }

        private async Task SelecetXChannelAsync(object args)
        {
            //htzk123
            //var database = await DatabaseComponent.Instance;
            //if (database == null) return;
            yChannelCollection.Clear();
            if (SelecetedXChannel != null)
            {
                if (SelecetedXChannel.VData != null)
                {
                    string[] indexStrArray = SelecetedXChannel.VData.RelatedChannelGlobalIndex.Split(',');
                    foreach (var indexstr in indexStrArray)
                    {
                        int index = -1;
                        if (Int32.TryParse(indexstr, out index))
                        {
                            //htzk123
                            //var contract = database.ChannelContracts.Where(o => o.ChannelGlobalIndex == index).SingleOrDefault();
                            //if (contract != null)
                            //{
                            //    yChannelCollection.Add(contract);
                            //}
                        }
                    }
                    SelecetedYChannel = yChannelCollection.FirstOrDefault();
                }
            }
        }

        private BaseWaveChannelToken selecetedXChannel;
        public BaseWaveChannelToken SelecetedXChannel
        {
            get { return selecetedXChannel; }
            set
            {
                if (selecetedXChannel != value)
                {
                    selecetedXChannel = value;
                    OnPropertyChanged(() => SelecetedXChannel);
                }
            }
        }

        private ChannelDistributionContract selecetedYChannel;
        public ChannelDistributionContract SelecetedYChannel
        {
            get { return selecetedYChannel; }
            set
            {
                if (selecetedYChannel != value)
                {
                    selecetedYChannel = value;
                    OnPropertyChanged(() => SelecetedYChannel);
                }
            }
        }

        public IEnumerable<BaseWaveChannelToken> XChannels { get { return xChannelCollection; } }
        public IEnumerable<ChannelDistributionContract> YChannels { get { return yChannelCollection; } }
        public DelegateCommand<object> GenerateYChannelsCommand { get; private set; }
        public DelegateCommand<object> SelecetXChannelCommand { get; private set; }
    }
}

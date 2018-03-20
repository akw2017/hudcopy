using AIC.Core.Models;
using AIC.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIC.Core.ControlModels;
using AIC.Core.OrganizationModels;
using AIC.ServiceInterface;
using AIC.Core;
using System.Windows.Input;
using System.Reactive.Linq;
using System.Threading;
using AIC.Resources.Models;
using System.Windows;
using AIC.M9600.Common.SlaveDB.Generated;
using AIC.Core.DataModels;
using AIC.Core.Helpers;
using AIC.HistoryDataPage.Models;
using AIC.CoreType;
using AIC.M9600.Common.DTO.Device;
using AIC.MatlabMath;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Media;
using Arction.Wpf.Charting;

namespace AIC.HistoryDataPage.ViewModels
{
    public class HistoryEventAlarmTrendViewModel : BindableBase
    {
        
        private readonly IEventAggregator _eventAggregator;       
        private readonly IOrganizationService _organizationService;
        private readonly IDatabaseComponent _databaseComponent;
        private readonly ICardProcess _cardProcess;
        private readonly IHardwareService _hardwareService;

        public HistoryEventAlarmTrendViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, IDatabaseComponent databaseComponent, ICardProcess cardProcess, IHardwareService hardwareService)
        {           
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _databaseComponent = databaseComponent;
            _cardProcess = cardProcess;
            _hardwareService = hardwareService;

            Initialization = InitializeAsync();
        }

        #region 属性与字段

        private ObservableCollection<HistoricalDataViewModel> historicalDataCollection = new ObservableCollection<HistoricalDataViewModel>();
        public IEnumerable<HistoricalDataViewModel> HistoricalDatas { get { return historicalDataCollection; } }

        private ViewModelStatus _status = ViewModelStatus.None;
        public ViewModelStatus Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public string waitinfo;
        public string WaitInfo
        {
            get
            {
                return waitinfo;
            }
            set
            {
                waitinfo = value;
                OnPropertyChanged("WaitInfo");
            }
        }

        private double itemWidth = 500;
        public double ItemWidth
        {
            get { return itemWidth; }
            set
            {
                if (itemWidth != value)
                {
                    itemWidth = value;
                    OnPropertyChanged("ItemWidth");
                    amsReplayVM.ItemWidth = value;
                    timeDomainVM.ItemWidth = value;
                    frequencyDomainVM.ItemWidth = value;
                    powerSpectrumVM.ItemWidth = value;
                    powerSpectrumDensityVM.ItemWidth = value;
                    orthoDataVM.ItemWidth = value;
                    offDesignConditionVM.ItemWidth = value;
                    orderAnalysisVM.ItemWidth = value;
                    time3DSpectrumVM.ItemWidth = value;
                    rpm3DSpectrumVM.ItemWidth = value;
                    alarmPointTrendVM.ItemWidth = value;
                }
            }
        }

        private double itemHeight = 300;
        public double ItemHeight
        {
            get { return itemHeight; }
            set
            {
                if (itemHeight != value)
                {
                    itemHeight = value;
                    OnPropertyChanged("ItemHeight");
                    amsReplayVM.ItemHeight = value;
                    timeDomainVM.ItemHeight = value;
                    frequencyDomainVM.ItemHeight = value;
                    powerSpectrumVM.ItemHeight = value;
                    powerSpectrumDensityVM.ItemHeight = value;
                    orthoDataVM.ItemHeight = value;
                    offDesignConditionVM.ItemHeight = value;
                    orderAnalysisVM.ItemHeight = value;
                    time3DSpectrumVM.ItemHeight = value;
                    rpm3DSpectrumVM.ItemHeight = value;
                    alarmPointTrendVM.ItemHeight = value;
                }
            }
        }
        #endregion

        #region 私有变量
        private RMSReplayDataViewModel amsReplayVM;
        private TimeDomainDataViewModel timeDomainVM;
        private FrequencyDomainDataViewModel frequencyDomainVM;
        private PowerSpectrumDataViewModel powerSpectrumVM;
        private PowerSpectrumDensityDataViewModel powerSpectrumDensityVM;
        private OrthoDataViewModel orthoDataVM;
        private OffDesignConditionDataViewModel offDesignConditionVM;
        private OrderAnalysisDataViewModel orderAnalysisVM;
        private Time3DSpectrumDataViewModel time3DSpectrumVM;
        private RPM3DSpectrumDataViewModel rpm3DSpectrumVM;
        private AlarmPointTrendDataViewModel alarmPointTrendVM;
        private SynchronizationContext uiContext = SynchronizationContext.Current;
        private Func<IEnumerable<BaseWaveChannelToken>, Task> trackTask { get; set; }
        private bool isTrackRunning;
        private List<Color> ColorList = new List<Color>();
        #endregion

        #region 初始化
        public Task Initialization { get; private set; }
        public async Task InitializeAsync()
        {
            try
            {
                amsReplayVM = new RMSReplayDataViewModel(true);
                amsReplayVM.Title = "趋势";
                amsReplayVM.ItemWidth = ItemWidth;
                amsReplayVM.ItemHeight = ItemHeight;              

                timeDomainVM = new TimeDomainDataViewModel();
                timeDomainVM.Title = "时域";
                timeDomainVM.ItemWidth = ItemWidth;
                timeDomainVM.ItemHeight = ItemHeight;

                frequencyDomainVM = new FrequencyDomainDataViewModel();
                frequencyDomainVM.Title = "频域";
                frequencyDomainVM.ItemWidth = ItemWidth;
                frequencyDomainVM.ItemHeight = ItemHeight;

                powerSpectrumVM = new PowerSpectrumDataViewModel();
                powerSpectrumVM.Title = "功率谱";
                powerSpectrumVM.ItemWidth = ItemWidth;
                powerSpectrumVM.ItemHeight = ItemHeight;

                powerSpectrumDensityVM = new PowerSpectrumDensityDataViewModel();
                powerSpectrumDensityVM.Title = "功率谱密度";
                powerSpectrumDensityVM.ItemWidth = ItemWidth;
                powerSpectrumDensityVM.ItemHeight = ItemHeight;

                orthoDataVM = new OrthoDataViewModel();
                orthoDataVM.Title = "轴心轨迹";
                orthoDataVM.ItemWidth = ItemWidth;
                orthoDataVM.ItemHeight = ItemHeight;

                //offDesignConditionVM = new OffDesignConditionDataViewModel(_dataModelProvider);//htzk123
                offDesignConditionVM = new OffDesignConditionDataViewModel();//htzk123
                offDesignConditionVM.Title = "变工况拟合";
                offDesignConditionVM.ItemWidth = ItemWidth;
                offDesignConditionVM.ItemHeight = ItemHeight;

                alarmPointTrendVM = new AlarmPointTrendDataViewModel();
                alarmPointTrendVM.Title = "报警点趋势";
                alarmPointTrendVM.ItemWidth = ItemWidth;
                alarmPointTrendVM.ItemHeight = ItemHeight;

                orderAnalysisVM = new OrderAnalysisDataViewModel();
                orderAnalysisVM.Title = "阶次分析";
                orderAnalysisVM.ItemWidth = ItemWidth;
                orderAnalysisVM.ItemHeight = ItemHeight;

                time3DSpectrumVM = new Time3DSpectrumDataViewModel();
                time3DSpectrumVM.Title = "时间三维谱";
                time3DSpectrumVM.ItemWidth = ItemWidth;
                time3DSpectrumVM.ItemHeight = ItemHeight;

                rpm3DSpectrumVM = new RPM3DSpectrumDataViewModel();
                rpm3DSpectrumVM.Title = "转速三维谱";
                rpm3DSpectrumVM.ItemWidth = ItemWidth;
                rpm3DSpectrumVM.ItemHeight = ItemHeight;

                trackTask = AMSTrackChanged;

                amsReplayVM.WhenTrackChanged.Sample(TimeSpan.FromMilliseconds(500)).ObserveOn(uiContext).Subscribe(RaiseTrackChanged);

                amsReplayVM.IsVisible = true;
                timeDomainVM.IsVisible = true;
                frequencyDomainVM.IsVisible = true;

                historicalDataCollection.Add(amsReplayVM);
                historicalDataCollection.Add(alarmPointTrendVM);
                historicalDataCollection.Add(timeDomainVM);
                historicalDataCollection.Add(frequencyDomainVM);
                historicalDataCollection.Add(powerSpectrumVM);
                historicalDataCollection.Add(powerSpectrumDensityVM);
                historicalDataCollection.Add(orthoDataVM);
                historicalDataCollection.Add(offDesignConditionVM);
                historicalDataCollection.Add(orderAnalysisVM);
                historicalDataCollection.Add(time3DSpectrumVM);
                historicalDataCollection.Add(rpm3DSpectrumVM);

            }
            catch (Exception e)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-初始化异常", e));
            }            
        }

        private async void RaiseTrackChanged(IEnumerable<BaseWaveChannelToken> tokens)
        {
            if (trackTask == null)
            {
                return;
            }

            if (isTrackRunning)
            {
                return;
            }

            try
            {
                // we're running it now
                isTrackRunning = true;
                await trackTask.Invoke(tokens);
            }
            catch (Exception)
            {
            }
            finally
            {
                // allow it to run again
                isTrackRunning = false;
            }
        }
        private async Task AMSTrackChanged(IEnumerable<BaseWaveChannelToken> tokens)
        {
            try
            {
                if (tokens == null) return;

                var unValidTokens = tokens.Where(o => o.CurrentIndex == -1);
                foreach (var token in unValidTokens)
                {
                    token.VData = null;
                }

                var validTokens = tokens.Where(o => o.CurrentIndex != -1).ToArray();
              
                if (validTokens.Length == 0) return;

                //var globalIndexes = validTokens.Select(o => o.DataContracts[o.CurrentIndex].ChannelGlobalIndex).ToArray();
                //var ids = validTokens.Select(o => o.DataContracts[o.CurrentIndex].id).ToArray();
                //var date = validTokens.Select(o => o.DataContracts[o.CurrentIndex].ACQDatetime).First();              

                List<IWaveformData> result = new List<IWaveformData>();
                foreach (var token in validTokens)
                {
                    if (token is BaseDivfreChannelToken)
                    {
                        var divtoken = token as BaseDivfreChannelToken;

                        List<D_WirelessVibrationSlot_Waveform> data = null;
                        if (divtoken.CurrentIndex != -1 && (divtoken.DataContracts[divtoken.CurrentIndex] as IBaseDivfreSlot).IsValidWave.Value == true)//修正拖动太快，CurrentIndex一直在变
                        {
                            data = await _databaseComponent.GetHistoryData<D_WirelessVibrationSlot_Waveform>(divtoken.IP, divtoken.Guid, new string[] { "WaveData", "SampleFre", "SamplePoint", "WaveUnit" }, divtoken.DataContracts[divtoken.CurrentIndex].ACQDatetime.AddSeconds(-1), divtoken.DataContracts[divtoken.CurrentIndex].ACQDatetime.AddSeconds(20), "(RecordLab = @0)", new object[] { divtoken.DataContracts[divtoken.CurrentIndex].RecordLab });
                        }
                        else
                        {
                            token.VData = null;
                        }
                        if (data != null && data.Count > 0)
                        {
                            result.Add(ClassCopyHelper.AutoCopy<D_WirelessVibrationSlot_Waveform, WirelessVibrationSlotData_Waveform>(data[0]));
                        }
                        else
                        {
                            token.VData = null;
                        }
                    }    
                }

                await Task.Run(() => Parallel.For(0, result.Count, i =>
                {      
                    VibrationData vdata = new VibrationData();
                    vdata.Waveform = Algorithm.ByteToSingle(result[i].WaveData);
                    vdata.SampleFre = result[i].SampleFre ?? 0;
                    vdata.SamplePoint = result[i].SamplePoint ?? 0;
                    vdata.Unit = result[i].WaveUnit;

                    var paras = Algorithm.CalculatePara(vdata.Waveform);
                    if (paras != null)
                    {
                        vdata.RMSValue = paras[0];
                        vdata.PeakValue = paras[1];
                        vdata.PeakPeakValue = paras[2];
                        vdata.Slope = paras[3];
                        vdata.Kurtosis = paras[4];
                        vdata.KurtosisValue = paras[5];
                        vdata.WaveIndex = paras[6];
                        vdata.PeakIndex = paras[7];
                        vdata.ImpulsionIndex = paras[8];
                        vdata.RootAmplitude = paras[9];
                        vdata.ToleranceIndex = paras[10];
                    }

                    double sampleFre = vdata.SampleFre;
                    if (vdata.Trigger == TriggerType.Angle)
                    {
                        if (vdata.RPM > 0 && vdata.TeethNumber > 0)
                        {
                            sampleFre = vdata.RPM * vdata.TeethNumber / 60;
                        }
                    }

                    int length = (int)(vdata.SamplePoint / 2.56) + 1;
                    if (vdata.Frequency == null || vdata.Frequency.Length != length)
                    {
                        vdata.Frequency = new double[length];
                    }
                    double frequencyInterval = sampleFre / vdata.SamplePoint;
                    for (int j = 0; j < length; j++)
                    {
                        vdata.Frequency[j] = frequencyInterval * j;
                    }
                    var output = Algorithm.Instance.FFT2AndPhaseAction(vdata.Waveform, vdata.SamplePoint);
                    if (output != null)
                    {
                        vdata.Amplitude = output[0].Take(length).ToArray();
                        vdata.Phase = output[1].Take(length).ToArray();
                    }
                    validTokens[i].VData = vdata;
                }));              
                
                timeDomainVM.ChangeChannelData(tokens);
                frequencyDomainVM.ChangeChannelData(tokens);               
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-TrackChanged", ex));
            }
        }
        #endregion 

        public async Task AddData(ItemTreeItemViewModel item, DateTime start, DateTime end)
        {      

            string selectedip = item.ServerIP;

            #region 测点
            if (item != null)
            {
                try
                {
                    WaitInfo = "获取数据中";
                    Status = ViewModelStatus.Querying;
                    if (item.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
                    {
                       
                        var result = await _databaseComponent.GetHistoryData<D_WirelessVibrationSlot>(selectedip, item.T_Item.Guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade", "IsValidWave", "RecordLab", "RPM" }, start, end, null, null);
                        result = result.Where(p => p.IsValidWave == true).ToList();
                        if (result == null || result.Count == 0)
                        {
                            return;
                        }
                        BaseDivfreChannelToken channeltoken = new BaseDivfreChannelToken()
                        {
                            DisplayName = item.BaseAlarmSignal.DeviceItemName,
                            IP = selectedip,
                            Guid = item.T_Item.Guid,
                            DataContracts = result.Select(p => ClassCopyHelper.AutoCopy<D_WirelessVibrationSlot, D1_WirelessVibrationSlot>(p) as IBaseAlarmSlot).ToList(),
                        };
                        foreach (var color in DefaultColors.SeriesForBlackBackgroundWpf)
                        {
                            if (!ColorList.Contains(color))
                            {
                                ColorList.Add(color);
                                channeltoken.SolidColorBrush = new SolidColorBrush(color);
                                break;
                            }
                        }
                        amsReplayVM.AddChannel(channeltoken);
                        timeDomainVM.AddChannel(channeltoken);
                        frequencyDomainVM.AddChannel(channeltoken);
                        powerSpectrumVM.AddChannel(channeltoken);
                        powerSpectrumDensityVM.AddChannel(channeltoken);
                        alarmPointTrendVM.AddChannel(channeltoken);
                        orthoDataVM.AddChannel(channeltoken);

                        offDesignConditionVM.AddChannel(channeltoken);
                    }
                    else if (item.T_Item.ItemType == (int)ChannelType.WirelessScalarChannelInfo)
                    {                       
                        var result = await _databaseComponent.GetHistoryData<D_WirelessScalarSlot>(selectedip, item.T_Item.Guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade" }, start, end, null, null);
                        if (result == null || result.Count == 0)
                        {
                            return;
                        }
                        BaseAlarmChannelToken channeltoken = new BaseAlarmChannelToken()
                        {
                            DisplayName = item.BaseAlarmSignal.DeviceItemName,
                            IP = selectedip,
                            Guid = item.T_Item.Guid,
                            DataContracts = result.Select(p => ClassCopyHelper.AutoCopy<D_WirelessScalarSlot, D1_WirelessScalarSlot>(p) as IBaseAlarmSlot).ToList(),
                        };
                        foreach (var color in DefaultColors.SeriesForBlackBackgroundWpf)
                        {
                            if (!ColorList.Contains(color))
                            {
                                ColorList.Add(color);
                                channeltoken.SolidColorBrush = new SolidColorBrush(color);
                                break;
                            }
                        }
                        amsReplayVM.AddChannel(channeltoken);
                        timeDomainVM.IsVisible = false;
                        frequencyDomainVM.IsVisible = false;
                    }
                }
                catch (Exception ex)
                {
                    _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-测点查询", ex));
                }
                finally
                {
                    Status = ViewModelStatus.None;
                }
            }
            #endregion
        }

    }
  
}

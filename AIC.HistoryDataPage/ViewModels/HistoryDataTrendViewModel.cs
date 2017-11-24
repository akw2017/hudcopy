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
using AIC.Core.SignalModels;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Data;
using AIC.CoreType;
using AIC.MatlabMath;
using Wpf.PageNavigationControl;
using System.Windows;
using AIC.Core.ExCommand;
using AIC.HistoryDataPage.Models;
using AIC.M9600.Common.SlaveDB.Generated;
using AIC.Core.Helpers;
using AIC.Core.DataModels;
using Arction.Wpf.Charting;
using System.Windows.Media;
using System.Reactive.Linq;
using System.Threading;
using AIC.Resources.Models;
using AIC.HistoryDataPage.ViewModels;
using AIC.M9600.Common.DTO.Device;

namespace AIC.OnLineDataPage.ViewModels
{
    public delegate void SignalAddHandler(SignalToken token, DateTime time, int size);
    public delegate void SignalRemovedHandler(SignalToken token);
    public delegate void SignalRefreshHandler(IEnumerable<SignalToken> tokens, DateTime time, int size, bool refresh = false);
    public delegate void SignalShowChangedHandler(SignalToken token);
    public delegate void SignalSelectedHandler(SignalToken token);

    class HistoryDataTrendViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;       
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        private readonly IDatabaseComponent _databaseComponent;

        public event SignalAddHandler SignalAdded;
        public event SignalRemovedHandler SignalRemoved;
        public event SignalRefreshHandler SignalRefresh;
        public event SignalShowChangedHandler SignalShowChanged;
        public event SignalSelectedHandler SignalSelected;

        public HistoryDataTrendViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess, IDatabaseComponent databaseComponent)
        {           
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _signalProcess = signalProcess;
            _cardProcess = cardProcess;
            _databaseComponent = databaseComponent;
            WhenTrackChanged.Sample(TimeSpan.FromMilliseconds(500)).ObserveOn(SynchronizationContext.Current).Subscribe(RaiseTrackChanged);

            InitTree();
            InitPager();
            Initialization = InitializeAsync();
        }
        #region 属性与字段   
        private ObservableCollection<OrganizationTreeItemViewModel> _organizationTreeItems;
        public ObservableCollection<OrganizationTreeItemViewModel> OrganizationTreeItems
        {
            get { return _organizationTreeItems; }
            set
            {
                _organizationTreeItems = value;
                OnPropertyChanged("OrganizationTreeItems");
            }
        }

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

        private ObservableCollection<SignalToken> addedSignals = new ObservableCollection<SignalToken>();
        public IEnumerable<SignalToken> AddedSignals { get { return addedSignals; } }
        public List<string> UnitCategory
        {
            get
            {
                return new List<string>()
                {
                    "m/s^2", "mm/s", "um", "Pa", "RPM", "°C", "Unit"
                };
            }
        }

        private int timeSize;
        public int TimeSize
        {
            get
            {
                return timeSize;
            }
            set
            {
                timeSize = value;
                OnPropertyChanged("TimeSize");
            }
        }

        private List<int> timeSizeList;
        public List<int> TimeSizeList
        {
            get
            {
                return timeSizeList;
            }
            set
            {
                timeSizeList = value;
                OnPropertyChanged("TimeSizeList");
            }
        }

        private int totalPoint;
        public int TotalPoint
        {
            get
            {
                return totalPoint;
            }
            set
            {
                totalPoint = value;
                OnPropertyChanged("TotalPoint");
            }
        }

        private DateTime currentTime;
        public DateTime CurrentTime
        {
            get
            {
                return currentTime;
            }
            set
            {
                currentTime = value;
                OnPropertyChanged("CurrentTime");
            }
        }

        private string chartFileName;
        public string ChartFileName
        {
            get
            {
                return chartFileName;
            }
            set
            {
                chartFileName = value;
                OnPropertyChanged("ChartFileName");
            }
        }

        private ChartFileData chartFile;
        public ChartFileData ChartFile
        {
            get
            {
                return chartFile;
            }
            set
            {
                chartFile = value;
                OnPropertyChanged("ChartFile");
            }
        }

        private ObservableCollection<ChartFileData> chartFileCategory = new ObservableCollection<ChartFileData>();
        public IEnumerable<ChartFileData> ChartFileCategory { get { return chartFileCategory; } }

        private event EventHandler<TrendTrackChangedEventArgs> trackChanged;
        public IObservable<IEnumerable<BaseWaveSignalToken>> WhenTrackChanged
        {
            get
            {
                return Observable
                    .FromEventPattern<TrendTrackChangedEventArgs>(
                        h => this.trackChanged += h,
                        h => this.trackChanged -= h)
                   .Select(x => x.EventArgs.Tokens);// .Select(x => x.EventArgs.Tokens).Throttle(TimeSpan.FromMilliseconds(500));
            }
        }

        private List<Color> ColorList = new List<Color>();
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
        #endregion

        #region 命令
        private ICommand doubleClickAddDataCommand;
        public ICommand DoubleClickAddDataCommand
        {
            get
            {
                return this.doubleClickAddDataCommand ?? (this.doubleClickAddDataCommand = new DelegateCommand<object>(para => this.AddData(para)));
            }
        }

        private ICommand selectedTreeChangedComamnd;
        public ICommand SelectedTreeChangedComamnd
        {
            get
            {
                return this.selectedTreeChangedComamnd ?? (this.selectedTreeChangedComamnd = new DelegateCommand<object>(para => this.SelectedTreeChanged(para)));
            }
        }

        private ICommand selectedDataGridChangedComamnd;
        public ICommand SelectedDataGridChangedComamnd
        {
            get
            {
                return this.selectedDataGridChangedComamnd ?? (this.selectedDataGridChangedComamnd = new DelegateCommand<object>(para => this.SelectedDataGridChanged(para)));
            }
        }

        private DelegateCommand<object> currentTimeChangedComamnd;
        public DelegateCommand<object> CurrentTimeChangedComamnd
        {
            get
            {
                return this.currentTimeChangedComamnd ?? (this.currentTimeChangedComamnd = new DelegateCommand<object>(value => this.CurrentTimeChanged(value)));
            }
        }

        private ICommand checkedCommand;
        public ICommand CheckedCommand
        {
            get
            {
                return this.checkedCommand ?? (this.checkedCommand = new DelegateCommand<object>(para => this.Checked(para)));
            }
        }

        private ICommand unCheckedCommand;
        public ICommand UnCheckedCommand
        {
            get
            {
                return this.unCheckedCommand ?? (this.unCheckedCommand = new DelegateCommand<object>(para => this.UnChecked(para)));
            }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return this.deleteCommand ?? (this.deleteCommand = new DelegateCommand<object>(para => this.Delete(para)));
            }
        }

        private ICommand saveChartFileCommand;
        public ICommand SaveChartFileCommand
        {
            get
            {
                return this.saveChartFileCommand ?? (this.saveChartFileCommand = new DelegateCommand<object>(para => this.SaveChartFile(para)));
            }
        }

        private ICommand loadChartFileCommand;
        public ICommand LoadChartFileCommand
        {
            get
            {
                return this.loadChartFileCommand ?? (this.loadChartFileCommand = new DelegateCommand<object>(para => this.LoadChartFile(para)));
            }
        }
        #endregion

        #region 管理树
        private void InitTree()
        { 
            OrganizationTreeItems = _organizationService.OrganizationTreeItems;
            //TreeExpanded();
        }

        private void TreeExpanded()
        {
            foreach (var first in OrganizationTreeItems)
            {
                first.IsExpanded = true;
                foreach (var second in first.Children)
                {
                    second.IsExpanded = true;
                    foreach (var third in second.Children)
                    {
                        third.IsExpanded = true;
                    }
                }
            }
        }
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

                //trackTask = AMSTrackChanged;

                //amsReplayVM.WhenTrackChanged.Sample(TimeSpan.FromMilliseconds(500)).ObserveOn(uiContext).Subscribe(RaiseTrackChanged);

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
        
        #endregion 

        #region 数据
        private async void AddData(object para)
        {
            if (addedSignals.Count >= 8)
            {
                return;
            }
            ItemTreeItemViewModel itemTree = para as ItemTreeItemViewModel;
            if (itemTree != null && itemTree.T_Item != null)
            {
                if (addedSignals.Select(o => o.Guid).Contains(itemTree.T_Item.Guid)) return;
                string selectedip = itemTree.ServerIP;
                #region WirelessVibrationChannelInfo
                if (itemTree.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
                {
                    BaseDivfreSignalToken signaltoken = new BaseDivfreSignalToken()
                    {
                        DisplayName = itemTree.BaseAlarmSignal.DeviceItemName,
                        IP = selectedip,
                        Guid = itemTree.T_Item.Guid,   
                        ItemType = 12,                    
                        BaseAlarmSignal = itemTree.BaseAlarmSignal,
                        UpperLimit = 10,
                        LowerLimit = 0,   
                    };

                    var result = await _databaseComponent.GetHistoryData<D_WirelessVibrationSlot>(selectedip, itemTree.T_Item.Guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade", "IsValidWave", "RecordLab", "RPM" }, CurrentTime.AddMinutes(0 - TimeSize), CurrentTime.AddMinutes(TimeSize * 2), null, null);

                    if (result != null && result.Count > 0)
                    {
                        double maxValue =  Math.Round(result.Select(p => p.Result.Value).Max(), 1);
                        double minValue = Math.Round(result.Select(p => p.Result.Value).Min(), 1);                      
                        signaltoken.UpperLimit = maxValue + 5;
                        signaltoken.LowerLimit = minValue - 5;
                        WirelessVibrationDataPartition(result, signaltoken, CurrentTime, TimeSize);
                        signaltoken.Unit = result[0].Unit;
                    }
                   
                    foreach (var color in  DefaultColors.SeriesForBlackBackgroundWpf)
                    {
                        if (!ColorList.Contains(color))
                        {
                            ColorList.Add(color);
                            signaltoken.SolidColorBrush = new SolidColorBrush(color);
                            break;
                        }
                    }

                    addedSignals.Add(signaltoken);
                    signaltoken.Index = addedSignals.IndexOf(signaltoken) + 1;
                    if (SignalAdded != null)
                    {
                        SignalAdded(signaltoken, CurrentTime, TimeSize);
                    }

                    signaltoken.PreviousDatas = new List<IBaseDivfreSlot>();
                    signaltoken.NextDatas = new List<IBaseDivfreSlot>();
                    string sql = null;
                    object[] unit = null;
                    if (signaltoken.Unit != null)
                    {
                        sql = "Unit = @0";
                        unit = new object[] { signaltoken.Unit };
                    }
                    await LoadWirelessVibrationData(signaltoken.ItemType, signaltoken.Guid, signaltoken.IP, signaltoken.PreviousDatas, CurrentTime.AddMinutes(0 - TimeSize * 3 / 2), CurrentTime.AddMinutes(0 - TimeSize), sql, unit);
                    await LoadWirelessVibrationData(signaltoken.ItemType, signaltoken.Guid, signaltoken.IP, signaltoken.NextDatas, CurrentTime.AddMinutes(TimeSize * 2), CurrentTime.AddMinutes(TimeSize * 5 / 2), sql, unit);

                    signaltoken.ChannelToken = SignalConvertToChannelToken(signaltoken);

                    timeDomainVM.AddChannel(signaltoken.ChannelToken);
                    frequencyDomainVM.AddChannel(signaltoken.ChannelToken);
                    powerSpectrumVM.AddChannel(signaltoken.ChannelToken);
                    powerSpectrumDensityVM.AddChannel(signaltoken.ChannelToken);
                    alarmPointTrendVM.AddChannel(signaltoken.ChannelToken);
                    orthoDataVM.AddChannel(signaltoken.ChannelToken);

                    offDesignConditionVM.AddChannel(signaltoken.ChannelToken);
                }
                #endregion
                #region WirelessScalarChannelInfo
                else if (itemTree.T_Item.ItemType == (int)ChannelType.WirelessScalarChannelInfo)
                {
                    BaseAlarmSignalToken signaltoken = new BaseAlarmSignalToken()
                    {
                        DisplayName = itemTree.BaseAlarmSignal.DeviceItemName,
                        IP = selectedip,
                        Guid = itemTree.T_Item.Guid,
                        ItemType = 11,
                        BaseAlarmSignal = itemTree.BaseAlarmSignal,
                        UpperLimit = 10,
                        LowerLimit = 0,
                    };

                    var result = await _databaseComponent.GetHistoryData<D_WirelessScalarSlot>(selectedip, itemTree.T_Item.Guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade"}, CurrentTime.AddMinutes(0 - TimeSize), CurrentTime.AddMinutes(TimeSize * 2), null, null);

                    if (result != null && result.Count > 0)
                    {
                        double maxValue = Math.Round(result.Select(p => p.Result.Value).Max(), 1);
                        double minValue = Math.Round(result.Select(p => p.Result.Value).Min(), 1);
                        signaltoken.UpperLimit = maxValue + 5;
                        signaltoken.LowerLimit = minValue - 5;
                        WirelessScalarDataPartition(result, signaltoken, CurrentTime, TimeSize);
                        signaltoken.Unit = result[0].Unit;
                    }

                    foreach (var color in DefaultColors.SeriesForBlackBackgroundWpf)
                    {
                        if (!ColorList.Contains(color))
                        {
                            ColorList.Add(color);
                            signaltoken.SolidColorBrush = new SolidColorBrush(color);
                            break;
                        }
                    }

                    addedSignals.Add(signaltoken);
                    signaltoken.Index = addedSignals.IndexOf(signaltoken) + 1;
                    if (SignalAdded != null)
                    {
                        SignalAdded(signaltoken, CurrentTime, TimeSize);
                    }

                    signaltoken.PreviousDatas = new List<IBaseAlarmSlot>();
                    signaltoken.NextDatas = new List<IBaseAlarmSlot>();
                    string sql = null;
                    object[] unit = null;
                    if (signaltoken.Unit != null)
                    {
                        sql = "Unit = @0";
                        unit = new object[] { signaltoken.Unit };
                    }
                    await LoadWirelessScalarData(signaltoken.ItemType, signaltoken.Guid, signaltoken.IP, signaltoken.PreviousDatas, CurrentTime.AddMinutes(0 - TimeSize * 3 / 2), CurrentTime.AddMinutes(0 - TimeSize), sql, unit);
                    await LoadWirelessScalarData(signaltoken.ItemType, signaltoken.Guid, signaltoken.IP, signaltoken.NextDatas, CurrentTime.AddMinutes(TimeSize * 2), CurrentTime.AddMinutes(TimeSize * 5 / 2), sql, unit);
                }
                #endregion
            }
        }

        private readonly SemaphoreSlim lazyLoadinglocker = new SemaphoreSlim(1);

        private async void PageData(RoutedPropertyChangedEventArgs<TrendNavigationEventArgs> args)
        {
            await lazyLoadinglocker.WaitAsync();
            try
            {
                var newtime = args.NewValue.CurrectTime;
                var newsize = args.NewValue.TimeSize;
                var oldtime = args.OldValue.CurrectTime;
                var oldsize = args.OldValue.TimeSize;

                if (newsize == oldsize && newtime == oldtime)
                {
                    await PageRefreshData(newtime, newsize); //return;
                }
                else if (newsize == oldsize && newtime == oldtime.AddMinutes(newsize / 2))
                {
                    await PageDownData(newtime, newsize);
                }
                else if (newsize == oldsize && newtime == oldtime.AddMinutes(0 - newsize / 2))
                {
                    await PageUpData(newtime, newsize);
                }
                else
                {
                    await PageRefreshData(newtime, newsize);
                }
            }
            finally
            {
                lazyLoadinglocker.Release();
            }
        }

        private async Task PageDownData(DateTime time, int size)
        {
            //向后翻页
            List<Task> lttask = new List<Task>();
            foreach (var signal in addedSignals)
            {
                if (signal is BaseDivfreSignalToken)
                {
                    var signaltoken = signal as BaseDivfreSignalToken;
                    signaltoken.PreviousDatas = signaltoken.FirstDatas;
                    signaltoken.FirstDatas = signaltoken.SecondDatas;
                    signaltoken.SecondDatas = signaltoken.ThirdDatas;
                    signaltoken.ThirdDatas = signaltoken.FourthDatas;
                    signaltoken.FourthDatas = signaltoken.FifthDatas;
                    signaltoken.FifthDatas = signaltoken.SixthDatas;
                    signaltoken.SixthDatas = signaltoken.NextDatas;
                    signaltoken.NextDatas = new List<IBaseDivfreSlot>();
                    string sql = null;
                    object[] unit = null;
                    if (signaltoken.Unit != null)
                    {
                        sql = "Unit = @0";
                        unit = new object[] { signaltoken.Unit };
                    }
                    lttask.Add(LoadWirelessVibrationData(signaltoken.ItemType, signaltoken.Guid, signaltoken.IP, signaltoken.NextDatas, time.AddMinutes(size * 2), time.AddMinutes(size * 5 / 2), sql, unit));
                }
                else if (signal is BaseAlarmSignalToken)
                {
                    var signaltoken = signal as BaseAlarmSignalToken;
                    signaltoken.PreviousDatas = signaltoken.FirstDatas;
                    signaltoken.FirstDatas = signaltoken.SecondDatas;
                    signaltoken.SecondDatas = signaltoken.ThirdDatas;
                    signaltoken.ThirdDatas = signaltoken.FourthDatas;
                    signaltoken.FourthDatas = signaltoken.FifthDatas;
                    signaltoken.FifthDatas = signaltoken.SixthDatas;
                    signaltoken.SixthDatas = signaltoken.NextDatas;
                    signaltoken.NextDatas = new List<IBaseAlarmSlot>();
                    string sql = null;
                    object[] unit = null;
                    if (signaltoken.Unit != null)
                    {
                        sql = "Unit = @0";
                        unit = new object[] { signaltoken.Unit };
                    }
                    lttask.Add(LoadWirelessScalarData(signaltoken.ItemType, signaltoken.Guid, signaltoken.IP, signaltoken.NextDatas, time.AddMinutes(size * 2), time.AddMinutes(size * 5 / 2), sql, unit));
                }
            }
            if (SignalRefresh != null)
            {
                SignalRefresh(addedSignals, time, size);
            }
            await Task.WhenAll(lttask.ToArray());
        }

        private async Task PageUpData(DateTime time, int size)
        {
            //向前翻页
            List<Task> lttask = new List<Task>();
            foreach (var signal in addedSignals)
            {
                if (signal is BaseDivfreSignalToken)
                {
                    var signaltoken = signal as BaseDivfreSignalToken;
                    signaltoken.NextDatas = signaltoken.SixthDatas;
                    signaltoken.SixthDatas = signaltoken.FifthDatas;
                    signaltoken.FifthDatas = signaltoken.FourthDatas;
                    signaltoken.FourthDatas = signaltoken.ThirdDatas;
                    signaltoken.ThirdDatas = signaltoken.SecondDatas;
                    signaltoken.SecondDatas = signaltoken.FirstDatas;
                    signaltoken.FirstDatas = signaltoken.PreviousDatas;
                    signaltoken.PreviousDatas = new List<IBaseDivfreSlot>();
                    string sql = null;
                    object[] unit = null;
                    if (signaltoken.Unit != null)
                    {
                        sql = "Unit = @0";
                        unit = new object[] { signaltoken.Unit };
                    }
                    lttask.Add(LoadWirelessVibrationData(signaltoken.ItemType, signaltoken.Guid, signaltoken.IP, signaltoken.PreviousDatas, time.AddMinutes(0 - size * 3 / 2), time.AddMinutes(0 - size), sql, unit));
                }
                else if (signal is BaseAlarmSignalToken)
                {
                    var signaltoken = signal as BaseAlarmSignalToken;
                    signaltoken.NextDatas = signaltoken.SixthDatas;
                    signaltoken.SixthDatas = signaltoken.FifthDatas;
                    signaltoken.FifthDatas = signaltoken.FourthDatas;
                    signaltoken.FourthDatas = signaltoken.ThirdDatas;
                    signaltoken.ThirdDatas = signaltoken.SecondDatas;
                    signaltoken.SecondDatas = signaltoken.FirstDatas;
                    signaltoken.FirstDatas = signaltoken.PreviousDatas;
                    signaltoken.PreviousDatas = new List<IBaseAlarmSlot>();
                    string sql = null;
                    object[] unit = null;
                    if (signaltoken.Unit != null)
                    {
                        sql = "Unit = @0";
                        unit = new object[] { signaltoken.Unit };
                    }
                    lttask.Add(LoadWirelessScalarData(signaltoken.ItemType, signaltoken.Guid, signaltoken.IP, signaltoken.PreviousDatas, time.AddMinutes(0 - size * 3 / 2), time.AddMinutes(0 - size),sql, unit));
                }
            }
            if (SignalRefresh != null)
            {
                SignalRefresh(addedSignals, time, size);
            }
            await Task.WhenAll(lttask.ToArray());
        }

        private async Task PageRefreshData(DateTime time, int size)
        {           
            List<Task> lttask = new List<Task>();
            foreach (var signal in addedSignals)
            {
                if (signal is BaseDivfreSignalToken)
                {
                    var signaltoken = signal as BaseDivfreSignalToken;
                    signaltoken.PreviousDatas = new List<IBaseDivfreSlot>();
                    signaltoken.FirstDatas = new List<IBaseDivfreSlot>();
                    signaltoken.SecondDatas = new List<IBaseDivfreSlot>();
                    signaltoken.ThirdDatas = new List<IBaseDivfreSlot>();
                    signaltoken.FourthDatas =  new List<IBaseDivfreSlot>();
                    signaltoken.FifthDatas = new List<IBaseDivfreSlot>();
                    signaltoken.SixthDatas = new List<IBaseDivfreSlot>();
                    signaltoken.NextDatas = new List<IBaseDivfreSlot>();
                    string sql = null;
                    object[] unit = null;
                    if (signaltoken.Unit != null)
                    {
                        sql = "Unit = @0";
                        unit = new object[] { signaltoken.Unit };
                    }
                    var result = await _databaseComponent.GetHistoryData<D_WirelessVibrationSlot>(signaltoken.IP, signaltoken.Guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade", "IsValidWave", "RecordLab", "RPM" }, time.AddMinutes(0 - size / 2), time.AddMinutes(size * 3 / 2), sql, unit);

                    if (result != null && result.Count > 0)
                    {
                        double maxValue = Math.Round(result.Select(p => p.Result.Value).Max(), 1);
                        double minValue = Math.Round(result.Select(p => p.Result.Value).Min(), 1);                      
                        signaltoken.UpperLimit = maxValue + 5;
                        signaltoken.LowerLimit = minValue - 5;
                        WirelessVibrationDataPartition(result, signaltoken, time, size);
                    }

                    signaltoken.PreviousDatas = new List<IBaseDivfreSlot>();
                    signaltoken.NextDatas = new List<IBaseDivfreSlot>();
                    lttask.Add(LoadWirelessVibrationData(signaltoken.ItemType, signaltoken.Guid, signaltoken.IP, signaltoken.PreviousDatas, time.AddMinutes(0 - size), time.AddMinutes(0 - size / 2), sql, unit));
                    lttask.Add(LoadWirelessVibrationData(signaltoken.ItemType, signaltoken.Guid, signaltoken.IP, signaltoken.NextDatas, time.AddMinutes(size * 3 / 2), time.AddMinutes(size * 2), sql, unit));
                }
                if (signal is BaseAlarmSignalToken)
                {
                    var signaltoken = signal as BaseAlarmSignalToken;
                    signaltoken.PreviousDatas = new List<IBaseAlarmSlot>();
                    signaltoken.FirstDatas = new List<IBaseAlarmSlot>();
                    signaltoken.SecondDatas = new List<IBaseAlarmSlot>();
                    signaltoken.ThirdDatas = new List<IBaseAlarmSlot>();
                    signaltoken.FourthDatas = new List<IBaseAlarmSlot>();
                    signaltoken.FifthDatas = new List<IBaseAlarmSlot>();
                    signaltoken.SixthDatas = new List<IBaseAlarmSlot>();
                    signaltoken.NextDatas = new List<IBaseAlarmSlot>();
                    string sql = null;
                    object[] unit = null;
                    if (signaltoken.Unit != null)
                    {
                        sql = "Unit = @0";
                        unit = new object[] { signaltoken.Unit };
                    }
                    var result = await _databaseComponent.GetHistoryData<D_WirelessScalarSlot>(signaltoken.IP, signaltoken.Guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade" }, time.AddMinutes(0 - size / 2), time.AddMinutes(size * 3 / 2), sql, unit);

                    if (result != null && result.Count > 0)
                    {
                        double maxValue = Math.Round(result.Select(p => p.Result.Value).Max(), 1);
                        double minValue = Math.Round(result.Select(p => p.Result.Value).Min(), 1);
                        signaltoken.UpperLimit = maxValue + 5;
                        signaltoken.LowerLimit = minValue - 5;
                        WirelessScalarDataPartition(result, signaltoken, time, size);
                    }

                    signaltoken.PreviousDatas = new List<IBaseAlarmSlot>();
                    signaltoken.NextDatas = new List<IBaseAlarmSlot>();
                    lttask.Add(LoadWirelessScalarData(signaltoken.ItemType, signaltoken.Guid, signaltoken.IP, signaltoken.PreviousDatas, time.AddMinutes(0 - size), time.AddMinutes(0 - size / 2), sql, unit));
                    lttask.Add(LoadWirelessScalarData(signaltoken.ItemType, signaltoken.Guid, signaltoken.IP, signaltoken.NextDatas, time.AddMinutes(size * 3 / 2), time.AddMinutes(size * 2), sql, unit));
                }
            }
            if (SignalRefresh != null)
            {
                SignalRefresh(addedSignals, time, size, true);
            }
            await Task.WhenAll(lttask.ToArray());
        }

        private void WirelessVibrationDataPartition(List<D_WirelessVibrationSlot> result, BaseDivfreSignalToken token, DateTime time, int size)
        {
            token.FirstDatas = result.Where(p => p.ACQDatetime <= time.AddMinutes(0 - size / 2)).Select(p => ClassCopyHelper.AutoCopy<D_WirelessVibrationSlot, D1_WirelessVibrationSlot>(p) as IBaseDivfreSlot).ToList();
            token.SecondDatas = result.Where(p => p.ACQDatetime > time.AddMinutes(0 - size / 2) && p.ACQDatetime <= time).Select(p => ClassCopyHelper.AutoCopy<D_WirelessVibrationSlot, D1_WirelessVibrationSlot>(p) as IBaseDivfreSlot).ToList();
            token.ThirdDatas = result.Where(p => p.ACQDatetime > time && p.ACQDatetime <= time.AddMinutes(size / 2)).Select(p => ClassCopyHelper.AutoCopy<D_WirelessVibrationSlot, D1_WirelessVibrationSlot>(p) as IBaseDivfreSlot).ToList();
            token.FourthDatas = result.Where(p => p.ACQDatetime > time.AddMinutes(size / 2) && p.ACQDatetime <= time.AddMinutes(size)).Select(p => ClassCopyHelper.AutoCopy<D_WirelessVibrationSlot, D1_WirelessVibrationSlot>(p) as IBaseDivfreSlot).ToList();
            token.FifthDatas = result.Where(p => p.ACQDatetime > time.AddMinutes(size) && p.ACQDatetime <= time.AddMinutes(size * 3 / 2)).Select(p => ClassCopyHelper.AutoCopy<D_WirelessVibrationSlot, D1_WirelessVibrationSlot>(p) as IBaseDivfreSlot).ToList();
            token.SixthDatas = result.Where(p => p.ACQDatetime > time.AddMinutes(size * 3 / 2) && p.ACQDatetime <= time.AddMinutes(size * 2)).Select(p => ClassCopyHelper.AutoCopy<D_WirelessVibrationSlot, D1_WirelessVibrationSlot>(p) as IBaseDivfreSlot).ToList();
        }

        private void WirelessScalarDataPartition(List<D_WirelessScalarSlot> result, BaseAlarmSignalToken token, DateTime time, int size)
        {
            token.FirstDatas = result.Where(p => p.ACQDatetime <= time.AddMinutes(0 - size / 2)).Select(p => ClassCopyHelper.AutoCopy<D_WirelessScalarSlot, D1_WirelessScalarSlot>(p) as IBaseAlarmSlot).ToList();
            token.SecondDatas = result.Where(p => p.ACQDatetime > time.AddMinutes(0 - size / 2) && p.ACQDatetime <= time).Select(p => ClassCopyHelper.AutoCopy<D_WirelessScalarSlot, D1_WirelessScalarSlot>(p) as IBaseAlarmSlot).ToList();
            token.ThirdDatas = result.Where(p => p.ACQDatetime > time && p.ACQDatetime <= time.AddMinutes(size / 2)).Select(p => ClassCopyHelper.AutoCopy<D_WirelessScalarSlot, D1_WirelessScalarSlot>(p) as IBaseAlarmSlot).ToList();
            token.FourthDatas = result.Where(p => p.ACQDatetime > time.AddMinutes(size / 2) && p.ACQDatetime <= time.AddMinutes(size)).Select(p => ClassCopyHelper.AutoCopy<D_WirelessScalarSlot, D1_WirelessScalarSlot>(p) as IBaseAlarmSlot).ToList();
            token.FifthDatas = result.Where(p => p.ACQDatetime > time.AddMinutes(size) && p.ACQDatetime <= time.AddMinutes(size * 3 / 2)).Select(p => ClassCopyHelper.AutoCopy<D_WirelessScalarSlot, D1_WirelessScalarSlot>(p) as IBaseAlarmSlot).ToList();
            token.SixthDatas = result.Where(p => p.ACQDatetime > time.AddMinutes(size * 3 / 2) && p.ACQDatetime <= time.AddMinutes(size * 2)).Select(p => ClassCopyHelper.AutoCopy<D_WirelessScalarSlot, D1_WirelessScalarSlot>(p) as IBaseAlarmSlot).ToList();
        }

        private async Task LoadWirelessVibrationData(int itemtype, Guid guid, string ip, List<IBaseDivfreSlot> datas, DateTime start, DateTime end, string sql = null, object[] unit = null)
        {
            if (itemtype == 12)
            {
                var result = await _databaseComponent.GetHistoryData<D_WirelessVibrationSlot>(ip, guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade", "IsValidWave", "RecordLab", "RPM" }, start, end, sql, unit);
                if (result != null)
                {
                    datas.AddRange(result.Select(p => ClassCopyHelper.AutoCopy<D_WirelessVibrationSlot, D1_WirelessVibrationSlot>(p) as IBaseDivfreSlot).ToList());
                }
            }
        }

        private async Task LoadWirelessScalarData(int itemtype, Guid guid, string ip, List<IBaseAlarmSlot> datas, DateTime start, DateTime end, string sql = null, object[] unit = null)
        {
            if (itemtype == 11)
            {
                var result = await _databaseComponent.GetHistoryData<D_WirelessScalarSlot>(ip, guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade" }, start, end, sql, unit);
                datas = result.Select(p => ClassCopyHelper.AutoCopy<D_WirelessScalarSlot, D1_WirelessScalarSlot>(p) as IBaseAlarmSlot).ToList();
            }
        }

        private void Checked(object para)
        {
            if (SignalShowChanged != null)
            {
                SignalShowChanged(para as SignalToken);
            }
        }

        private void UnChecked(object para)
        {
            if (SignalShowChanged != null)
            {
                SignalShowChanged(para as SignalToken);
            }
        }

        private void Delete(object para)
        {
            SignalToken token = para as SignalToken;
            if (token != null)
            {
                addedSignals.Remove(token);
                foreach(var signal in addedSignals)
                {
                    signal.Index = addedSignals.IndexOf(signal) + 1;
                }

                ColorList.Remove(token.SolidColorBrush.Color);
                if (SignalRemoved != null)
                {
                    SignalRemoved(token);
                }
                amsReplayVM.RemoveChannel(token.ChannelToken);
                alarmPointTrendVM.RemoveChannel(token.ChannelToken);
                timeDomainVM.RemoveChannel(token.ChannelToken);
                frequencyDomainVM.RemoveChannel(token.ChannelToken);
                powerSpectrumVM.RemoveChannel(token.ChannelToken);
                powerSpectrumDensityVM.RemoveChannel(token.ChannelToken);
                orthoDataVM.RemoveChannel(token.ChannelToken);
                time3DSpectrumVM.RemoveChannel(token.ChannelToken);
                offDesignConditionVM.RemoveChannel(token.ChannelToken);
            }
        }

        public void TrackChanged(IEnumerable<BaseWaveSignalToken> tokens)
        {
            if (trackChanged != null)
            {
                trackChanged(this, new TrendTrackChangedEventArgs(tokens));
            }
        }

        private async void RaiseTrackChanged(IEnumerable<BaseWaveSignalToken> tokens)
        {
            if (isTrackRunning)
            {
                return;
            }

            try
            {
                // we're running it now
                isTrackRunning = true;
                await AMSTrackChanged(tokens);
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

        private async Task AMSTrackChanged(IEnumerable<BaseWaveSignalToken> tokens)
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
                    if (token is BaseDivfreSignalToken)
                    {
                        var divtoken = token as BaseDivfreSignalToken;

                        List<D_WirelessVibrationSlot_Waveform> data = null;
                        if (divtoken.CurrentIndex != -1 && divtoken.DataContracts[divtoken.CurrentIndex].IsValidWave.Value == true)//修正拖动太快，CurrentIndex一直在变
                        {
                            data = await _databaseComponent.GetHistoryData<D_WirelessVibrationSlot_Waveform>(divtoken.IP, divtoken.Guid, new string[] { "WaveData", "SampleFre", "SamplePoint", "WaveUnit" }, divtoken.DataContracts[divtoken.CurrentIndex].ACQDatetime.AddSeconds(-1), divtoken.DataContracts[divtoken.CurrentIndex].ACQDatetime.AddSeconds(20), "(RecordLab = @0)", new object[] { divtoken.DataContracts[divtoken.CurrentIndex].RecordLab });
                        }
                        else
                        {
                            token.VData = null;
                            (divtoken.ChannelToken as BaseDivfreChannelToken).VData = null;
                        }
                        if (data != null && data.Count > 0)
                        {
                            result.Add(ClassCopyHelper.AutoCopy<D_WirelessVibrationSlot_Waveform, WirelessVibrationSlotData_Waveform>(data[0]));
                        }
                        else
                        {
                            token.VData = null;
                            (divtoken.ChannelToken as BaseDivfreChannelToken).VData = null;
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
                    (validTokens[i].ChannelToken as BaseDivfreChannelToken).VData = vdata;
                }));

                timeDomainVM.ChangeChannelData(tokens.Select(p => p.ChannelToken as BaseWaveChannelToken));
                frequencyDomainVM.ChangeChannelData(tokens.Select(p => p.ChannelToken as BaseWaveChannelToken));
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-TrackChanged", ex));
            }
        }
        #endregion

        #region 选中
        private void SelectedTreeChanged(object para)
        {
            ItemTreeItemViewModel itemTree = para as ItemTreeItemViewModel;
            if (itemTree != null && itemTree.T_Item != null)
            {
                var token = addedSignals.Where(p => p.Guid == itemTree.T_Item.Guid).FirstOrDefault();
                SelectedDataGridChanged(token);
            }
        }

        private void SelectedDataGridChanged(object para)
        {
            SignalToken token = para as SignalToken;
            if (token != null)
            {
                if (SignalSelected != null)
                {
                    SignalSelected(token);
                }
            }
        }
        #endregion

        #region 翻页控件

        private void InitPager()
        {
            TimeSizeList = new List<int>();
            TimeSizeList.Add(10);
            TimeSizeList.Add(20);
            TimeSizeList.Add(30);
            TimeSizeList.Add(60);
            TimeSizeList.Add(120);
            TimeSizeList.Add(240);
            TimeSizeList.Add(360);
            TimeSizeList.Add(720);
            TimeSizeList.Add(1440);
            TimeSize = TimeSizeList[2];
        }

        private void CurrentTimeChanged(object value)
        {
            RoutedPropertyChangedEventArgs<TrendNavigationEventArgs> args = ((ExCommandParameter)value).EventArgs as RoutedPropertyChangedEventArgs<TrendNavigationEventArgs>;
            if (args != null)
            {
                PageData(args);
            }
        }

        #endregion

        #region 保存chart文件
        private void SaveChartFile(object para)
        {
            if (ChartFileName == null || ChartFileName == "")
            {
#if XBAP
                MessageBox.Show("请填写名称","提示",MessageBoxButton.OK,MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请填写名称", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }
            var file = chartFileCategory.Where(p => p.Name == ChartFileName).FirstOrDefault();
            if (file != null)
            {
                if (ChartFile == file)
                {
                    ChartFile.ListGuid = addedSignals.Select(p => p.Guid).ToList();
                }
                else
                {
#if XBAP
                    MessageBox.Show("趋势图重名，请重新填写名称","提示",MessageBoxButton.OK,MessageBoxImage.Warning);
#else
                    Xceed.Wpf.Toolkit.MessageBox.Show("趋势图重名，请重新填写名称", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                }
            }
            else
            {
                ChartFileData data = new ChartFileData()
                {
                    Name = ChartFileName,
                    ListGuid = addedSignals.Select(p => p.Guid).ToList(),
                };
                chartFileCategory.Add(data);
            }


        }
        private void LoadChartFile(object para)
        {           
            if (ChartFile != null && ChartFile.ListGuid != null)
            {
                var guids = addedSignals.Select(p => p.Guid).ToList();
                if (guids.All(ChartFile.ListGuid.Contains) && guids.Count == ChartFile.ListGuid.Count)
                {
                    return;
                }
                for (int i = addedSignals.Count - 1; i >= 0; i--)
                {
                    Delete(addedSignals[i]);
                }
                foreach (var guid in ChartFile.ListGuid)
                {
                    var itemTree = _organizationService.ItemTreeItems.Where(p => p.T_Item.Guid == guid).FirstOrDefault();
                    if (itemTree != null)
                    {
                        AddData(itemTree);
                    }
                }
            }
        }
        #endregion

        private ChannelToken SignalConvertToChannelToken(SignalToken token)
        {
            if (token is BaseDivfreSignalToken)
            {
                BaseDivfreSignalToken signaltoken = token as BaseDivfreSignalToken;
                BaseDivfreChannelToken channeltoken = new BaseDivfreChannelToken()
                {
                    DisplayName = signaltoken.DisplayName,
                    IP = signaltoken.IP,
                    Guid = signaltoken.Guid,
                    DataContracts = signaltoken.DataContracts,
                    SolidColorBrush = signaltoken.SolidColorBrush,
                };
                return channeltoken;
            }
            else if (token is BaseWaveSignalToken)
            {
                BaseWaveSignalToken signaltoken = token as BaseWaveSignalToken;
                BaseWaveChannelToken channeltoken = new BaseWaveChannelToken()
                {
                    DisplayName = signaltoken.DisplayName,
                    IP = signaltoken.IP,
                    Guid = signaltoken.Guid,
                    DataContracts = signaltoken.DataContracts,
                    SolidColorBrush = signaltoken.SolidColorBrush,
                };
                return channeltoken;
            }
            else
            {
                ChannelToken channeltoken = new ChannelToken()
                {
                    DisplayName = token.DisplayName,
                    IP = token.IP,
                    Guid = token.Guid,
                    SolidColorBrush = token.SolidColorBrush,
                };
                return channeltoken;
            }
        }

    }
}

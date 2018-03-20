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
    class HistoryDataDiagramViewModel : BindableBase
    {
        
        private readonly IEventAggregator _eventAggregator;       
        private readonly IOrganizationService _organizationService;
        private readonly IDatabaseComponent _databaseComponent;
        private readonly ICardProcess _cardProcess;
        private readonly IHardwareService _hardwareService;

        public HistoryDataDiagramViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, IDatabaseComponent databaseComponent, ICardProcess cardProcess, IHardwareService hardwareService)
        {           
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _databaseComponent = databaseComponent;
            _cardProcess = cardProcess;
            _hardwareService = hardwareService;

            InitTree();
            Initialization = InitializeAsync();
        }

        #region 管理树
        private void InitTree()
        { 
            OrganizationTreeItems = _organizationService.OrganizationTreeItems;
            RecycledTreeItems = _organizationService.RecycledTreeItems; ;
            SelectedTreeItem = _cardProcess.GetSelectedTree(OrganizationTreeItems);//可能无效了
            TreeExpanded();
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

        private ObservableCollection<OrganizationTreeItemViewModel> _recycledTreeItems;
        public ObservableCollection<OrganizationTreeItemViewModel> RecycledTreeItems
        {
            get { return _recycledTreeItems; }
            set
            {
                _recycledTreeItems = value;
                OnPropertyChanged("RecycledTreeItems");
            }
        }

        private ObservableCollection<HistoricalDataViewModel> historicalDataCollection = new ObservableCollection<HistoricalDataViewModel>();
        public IEnumerable<HistoricalDataViewModel> HistoricalDatas { get { return historicalDataCollection; } }

        private HistoricalDataViewModel selectedHistoricalData;
        public HistoricalDataViewModel SelectedHistoricalData
        {
            get { return selectedHistoricalData; }
            set
            {
                if (value == null)
                {
                    if (historicalDataCollection.Count != 0)
                    {
                        return;
                    }
                }
                if (selectedHistoricalData != value)
                {
                    if (selectedHistoricalData != null)
                    {
                        selectedHistoricalData.IsSelected = false;
                    }
                    selectedHistoricalData = value;
                    if (selectedHistoricalData != null)
                    {
                        selectedHistoricalData.IsSelected = true;
                    }

                    OnPropertyChanged("SelectedHistoricalData");
                }
            }
        }

        private ObservableCollection<ChannelToken> addedChannels = new ObservableCollection<ChannelToken>();
        public IEnumerable<ChannelToken> AddedChannels { get { return addedChannels; } }

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

        private int channelRecordLength = 50000;
        public int ChannelRecordLength
        {
            get { return channelRecordLength; }
            set
            {
                if (value != channelRecordLength)
                {
                    channelRecordLength = value;
                    OnPropertyChanged("ChannelRecordLength");
                }
            }
        }

        private bool allowNormal = true;
        public bool AllowNormal
        {
            get { return allowNormal; }
            set
            {
                if (allowNormal != value)
                {
                    allowNormal = value;
                    OnPropertyChanged(() => AllowNormal);
                }
            }
        }

        private bool allowPreWarning = true;
        public bool AllowPreWarning
        {
            get { return allowPreWarning; }
            set
            {
                if (allowPreWarning != value)
                {
                    allowPreWarning = value;
                    OnPropertyChanged(() => AllowPreWarning);
                }
            }
        }

        private bool allowWarning = true;
        public bool AllowWarning
        {
            get { return allowWarning; }
            set
            {
                if (allowWarning != value)
                {
                    allowWarning = value;
                    OnPropertyChanged(() => AllowWarning);
                }
            }
        }

        private bool allowDanger = true;
        public bool AllowDanger
        {
            get { return allowDanger; }
            set
            {
                if (allowDanger != value)
                {
                    allowDanger = value;
                    OnPropertyChanged(() => AllowDanger);
                }
            }
        }

        private bool allowInvalid = true;
        public bool AllowInvalid
        {
            get { return allowInvalid; }
            set
            {
                if (allowInvalid != value)
                {
                    allowInvalid = value;
                    OnPropertyChanged(() => AllowInvalid);
                }
            }
        }

        private double? peakValue;
        public double? PeakValue
        {
            get { return peakValue; }
            set
            {
                if (peakValue != value)
                {
                    peakValue = value;
                    this.OnPropertyChanged(() => PeakValue);
                }
            }
        }

        private double? peakPeakValue;
        public double? PeakPeakValue
        {
            get { return peakPeakValue; }
            set
            {
                if (peakPeakValue != value)
                {
                    peakPeakValue = value;
                    this.OnPropertyChanged(() => PeakPeakValue);
                }
            }
        }

        private DateTime? startTime = DateTime.Now.Subtract(TimeSpan.FromHours(1));
        public DateTime? StartTime
        {
            get { return startTime; }
            set
            {
                if (value != startTime)
                {
                    startTime = value;                   
                    this.OnPropertyChanged("StartTime");
                }
            }
        }

        private DateTime? endTime = DateTime.Now;
        public DateTime? EndTime
        {
            get { return endTime; }
            set
            {
                if (value != endTime)
                {
                    endTime = value;                  
                    this.OnPropertyChanged("EndTime");
                }
            }
        }

        private ChannelToken selectedChannel;
        public ChannelToken SelectedChannel
        {
            get { return selectedChannel; }
            set
            {
                if (selectedChannel != value)
                {
                    selectedChannel = value;
                    this.OnPropertyChanged(() => this.SelectedChannel);
                }
            }
        }

        private AIC.CoreType.Unit unitFilter = AIC.CoreType.Unit.Velocity;
        public AIC.CoreType.Unit UnitFilter
        {
            get { return unitFilter; }
            set
            {
                if (unitFilter != value)
                {
                    unitFilter = value;
                    OnPropertyChanged(() => this.UnitFilter);
                }
            }
        }

        private AIC.CoreType.TriggerType triggerFilter = AIC.CoreType.TriggerType.Auto;
        public AIC.CoreType.TriggerType TriggerFilter
        {
            get { return triggerFilter; }
            set
            {
                if (triggerFilter != value)
                {
                    triggerFilter = value;
                    OnPropertyChanged(() => this.TriggerFilter);
                }
            }
        }

        private bool isMulticursor;
        public bool IsMulticursor//多关标模式
        {
            get { return isMulticursor; }
            set
            {
                if (isMulticursor != value)
                {
                    isMulticursor = value;
                    OnPropertyChanged(() => this.IsMulticursor);
                    if (ShowTimeDomain)//多光标新增
                    {
                        timeDomainVM2.IsVisible = value;                      
                    }
                    else
                    {
                        timeDomainVM2.IsVisible = false;
                    }
                    if (ShowFrequencyDomain)//多光标新增
                    {
                        frequencyDomainVM2.IsVisible = value;
                    }
                    else
                    {
                        frequencyDomainVM2.IsVisible = false;
                    }
                    if (ShowPowerSpectrum)//多光标新增
                    {
                        powerSpectrumVM2.IsVisible = value;
                    }
                    else
                    {
                        powerSpectrumVM2.IsVisible = false;
                    }
                    if (ShowPowerSpectrumDensity)//多光标新增
                    {
                        powerSpectrumDensityVM2.IsVisible = value;
                    }
                    else
                    {
                        powerSpectrumDensityVM2.IsVisible = false;
                    }
                    if (isMulticursor)
                    {
                        timeDomainVM.ItemWidth = ItemWidth / 2 - 8;
                        frequencyDomainVM.ItemWidth = ItemWidth / 2 - 8;
                        powerSpectrumVM.ItemWidth = ItemWidth / 2 - 8;
                        powerSpectrumDensityVM.ItemWidth = ItemWidth / 2 - 8;
                        timeDomainVM2.ItemWidth = ItemWidth / 2 - 8;//多光标新增
                        frequencyDomainVM2.ItemWidth = ItemWidth / 2 - 8;//多光标新增
                        powerSpectrumVM2.ItemWidth = ItemWidth / 2 - 8;//多光标新增
                        powerSpectrumDensityVM2.ItemWidth = ItemWidth / 2 - 8;//多光标新增                      
                    }
                    else
                    {
                        timeDomainVM.ItemWidth = ItemWidth;
                        frequencyDomainVM.ItemWidth = ItemWidth;
                        powerSpectrumVM.ItemWidth = ItemWidth;
                        powerSpectrumDensityVM.ItemWidth = ItemWidth;
                        timeDomainVM2.ItemWidth = ItemWidth;//多光标新增
                        frequencyDomainVM2.ItemWidth = ItemWidth;//多光标新增
                        powerSpectrumVM2.ItemWidth = ItemWidth;//多光标新增
                        powerSpectrumDensityVM2.ItemWidth = ItemWidth;//多光标新增                       
                    }
                    amsReplayVM.CursorlChannged(isMulticursor);
                }
            }
        }

        private double itemWidth = 450;
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
                    if (isMulticursor)
                    {
                        timeDomainVM.ItemWidth = value / 2 - 8;
                        frequencyDomainVM.ItemWidth = value / 2 - 8;
                        powerSpectrumVM.ItemWidth = value / 2 - 8;
                        powerSpectrumDensityVM.ItemWidth = value / 2 - 8;
                        timeDomainVM2.ItemWidth = value / 2 - 8;//多光标新增
                        frequencyDomainVM2.ItemWidth = value / 2 - 8;//多光标新增
                        powerSpectrumVM2.ItemWidth = value / 2 - 8;//多光标新增
                        powerSpectrumDensityVM2.ItemWidth = value / 2 - 8;//多光标新增                      
                    }
                    else
                    {
                        timeDomainVM.ItemWidth = value;
                        frequencyDomainVM.ItemWidth = value;
                        powerSpectrumVM.ItemWidth = value;
                        powerSpectrumDensityVM.ItemWidth = value;
                        timeDomainVM2.ItemWidth = value;//多光标新增
                        frequencyDomainVM2.ItemWidth = value;//多光标新增
                        powerSpectrumVM2.ItemWidth = value;//多光标新增
                        powerSpectrumDensityVM2.ItemWidth = value;//多光标新增                       
                    }
                    orthoDataVM.ItemWidth = value;
                    offDesignConditionVM.ItemWidth = value;
                    orderAnalysisVM.ItemWidth = value;
                    time3DSpectrumVM.ItemWidth = value;
                    rpm3DSpectrumVM.ItemWidth = value;
                    distributionMapVM.ItemWidth = value;
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
                    timeDomainVM2.ItemHeight = value;//多光标新增
                    frequencyDomainVM2.ItemHeight = value;//多光标新增
                    powerSpectrumVM2.ItemHeight = value;//多光标新增
                    powerSpectrumDensityVM2.ItemHeight = value;//多光标新增
                    orthoDataVM.ItemHeight = value;
                    offDesignConditionVM.ItemHeight = value;
                    orderAnalysisVM.ItemHeight = value;
                    time3DSpectrumVM.ItemHeight = value;
                    rpm3DSpectrumVM.ItemHeight = value;
                    distributionMapVM.ItemHeight = value;
                    alarmPointTrendVM.ItemHeight = value;

                }
            }
        }

        private bool isComposing;
        public bool IsComposing
        {
            get { return isComposing; }
            set
            {
                if (isComposing != value)
                {
                    isComposing = value;
                    OnPropertyChanged("IsComposing");
                }
            }
        }

        private bool showAMS;
        public bool ShowAMS
        {
            get { return showAMS; }
            set
            {
                if (showAMS != value)
                {
                    showAMS = value;
                    OnPropertyChanged("ShowAMS");
                    amsReplayVM.IsVisible = value;
                }
            }
        }

        private bool showTimeDomain;
        public bool ShowTimeDomain
        {
            get { return showTimeDomain; }
            set
            {
                if (showTimeDomain != value)
                {
                    showTimeDomain = value;
                    OnPropertyChanged("ShowTimeDomain");
                    timeDomainVM.IsVisible = value;
                    if (IsMulticursor)
                    {
                        timeDomainVM2.IsVisible = value;
                        timeDomainVM.ItemWidth = ItemWidth / 2 - 8;
                        timeDomainVM2.ItemWidth = ItemWidth / 2 - 8;
                    }
                    else
                    {
                        timeDomainVM2.IsVisible = false;
                        timeDomainVM.ItemWidth = ItemWidth;
                        timeDomainVM2.ItemWidth = ItemWidth;
                    }
                }
            }
        }

        private bool showFrequencyDomain;
        public bool ShowFrequencyDomain
        {
            get { return showFrequencyDomain; }
            set
            {
                if (showFrequencyDomain != value)
                {
                    showFrequencyDomain = value;
                    OnPropertyChanged("ShowFrequencyDomain");
                    frequencyDomainVM.IsVisible = value;
                    if (IsMulticursor)
                    {
                        frequencyDomainVM2.IsVisible = value;
                        frequencyDomainVM.ItemWidth = ItemWidth / 2 - 8;
                        frequencyDomainVM2.ItemWidth = ItemWidth / 2 - 8;
                    }
                    else
                    {
                        frequencyDomainVM2.IsVisible = false;
                        frequencyDomainVM.ItemWidth = ItemWidth;
                        frequencyDomainVM2.ItemWidth = ItemWidth;
                    }
                }
            }
        }

        private bool showPowerSpectrum;
        public bool ShowPowerSpectrum
        {
            get { return showPowerSpectrum; }
            set
            {
                if (showPowerSpectrum != value)
                {
                    showPowerSpectrum = value;
                    OnPropertyChanged("ShowPowerSpectrum");
                    powerSpectrumVM.IsVisible = value;
                    if (IsMulticursor)
                    {
                        powerSpectrumVM2.IsVisible = value;
                        powerSpectrumVM.ItemWidth = ItemWidth / 2 - 8;
                        powerSpectrumVM2.ItemWidth = ItemWidth / 2 - 8;
                    }
                    else
                    {
                        powerSpectrumVM2.IsVisible = false;
                        powerSpectrumVM.ItemWidth = ItemWidth;
                        powerSpectrumVM2.ItemWidth = ItemWidth;
                    }
                }
            }
        }

        private bool showPowerSpectrumDensity;
        public bool ShowPowerSpectrumDensity
        {
            get { return showPowerSpectrumDensity; }
            set
            {
                if (showPowerSpectrumDensity != value)
                {
                    showPowerSpectrumDensity = value;
                    OnPropertyChanged("ShowPowerSpectrumDensity");
                    powerSpectrumDensityVM.IsVisible = value;
                    if (IsMulticursor)
                    {
                        powerSpectrumDensityVM2.IsVisible = value;
                        powerSpectrumDensityVM.ItemWidth = ItemWidth / 2 - 8;
                        powerSpectrumDensityVM2.ItemWidth = ItemWidth / 2 - 8;
                    }
                    else
                    {
                        powerSpectrumDensityVM2.IsVisible = false;
                        powerSpectrumDensityVM.ItemWidth = ItemWidth;
                        powerSpectrumDensityVM2.ItemWidth = ItemWidth;
                    }
                }
            }
        }

        private bool showOrtho;
        public bool ShowOrtho
        {
            get { return showOrtho; }
            set
            {
                if (showOrtho != value)
                {
                    showOrtho = value;
                    OnPropertyChanged("ShowOrtho");
                    orthoDataVM.IsVisible = value;
                }
            }
        }

        private bool showOffCondition;
        public bool ShowOffCondition
        {
            get { return showOffCondition; }
            set
            {
                if (showOffCondition != value)
                {
                    showOffCondition = value;
                    OnPropertyChanged("ShowOffCondition");
                    offDesignConditionVM.IsVisible = value;
                }
            }
        }

        private bool showOrderAnalysis;
        public bool ShowOrderAnalysis
        {
            get { return showOrderAnalysis; }
            set
            {
                if (showOrderAnalysis != value)
                {
                    showOrderAnalysis = value;
                    OnPropertyChanged("ShowOrderAnalysis");
                    orderAnalysisVM.IsVisible = value;
                }
            }
        }

        private bool showTime3DSpectrum;
        public bool ShowTime3DSpectrum
        {
            get { return showTime3DSpectrum; }
            set
            {
                if (showTime3DSpectrum != value)
                {
                    showTime3DSpectrum = value;
                    OnPropertyChanged("ShowTime3DSpectrum");
                    time3DSpectrumVM.IsVisible = value;
                }
            }
        }

        private bool showRPM3DSpectrum;
        public bool ShowRPM3DSpectrum
        {
            get { return showRPM3DSpectrum; }
            set
            {
                if (showRPM3DSpectrum != value)
                {
                    showRPM3DSpectrum = value;
                    OnPropertyChanged("ShowRPM3DSpectrum");
                    rpm3DSpectrumVM.IsVisible = value;
                }
            }
        }

        private bool showAlarmPointTrend;
        public bool ShowAlarmPointTrend
        {
            get { return showAlarmPointTrend; }
            set
            {
                if (showAlarmPointTrend != value)
                {
                    showAlarmPointTrend = value;
                    OnPropertyChanged("ShowAlarmPointTrend");
                    alarmPointTrendVM.IsVisible = value;
                }
            }
        }

        private bool showDistributionMap;
        public bool ShowDistributionMap
        {
            get { return showDistributionMap; }
            set
            {
                if (showDistributionMap != value)
                {
                    showDistributionMap = value;
                    OnPropertyChanged("ShowDistributionMap");
                    distributionMapVM.IsVisible = value;
                }
            }
        }

        private bool allowRPMFilter;
        public bool AllowRPMFilter
        {
            get { return allowRPMFilter; }
            set
            {
                if (allowRPMFilter != value)
                {
                    allowRPMFilter = value;
                    OnPropertyChanged("AllowRPMFilter");
                }
            }
        }

    

        private double upRPMFilter;
        public double UpRPMFilter
        {
            get { return upRPMFilter; }
            set
            {
                if (upRPMFilter != value)
                {
                    upRPMFilter = value;
                    OnPropertyChanged("UpRPMFilter");
                }
            }
        }

        private double downRPMFilter;
        public double DownRPMFilter
        {
            get { return downRPMFilter; }
            set
            {
                if (downRPMFilter != value)
                {
                    downRPMFilter = value;
                    OnPropertyChanged("DownRPMFilter");
                }
            }
        }

       

        public List<string> UnitCategory
        {
            get
            {
                return _databaseComponent.UnitCategory;
            }
        }

        private string unit;
        public string Unit
        {
            get { return unit; }
            set
            {
                if (unit != value)
                {
                    unit = value;
                    OnPropertyChanged("Unit");
                }
            }
        }

        private OrganizationTreeItemViewModel selectedTreeItem;
        public OrganizationTreeItemViewModel SelectedTreeItem
        {
            get { return selectedTreeItem; }
            set
            {
                if (selectedTreeItem != value)
                {
                    selectedTreeItem = value;
                    OnPropertyChanged("SelectedTreeItem");
                }
            }
        }
        #endregion

        #region 命令
        private ICommand selectedTreeChangedComamnd;
        public ICommand SelectedTreeChangedComamnd
        {
            get
            {
                return this.selectedTreeChangedComamnd ?? (this.selectedTreeChangedComamnd = new DelegateCommand<object>(para => this.SelectedTreeChanged(para)));
            }
        }
        private ICommand addDataCommand;
        public ICommand AddDataCommand
        {
            get
            {
                return this.addDataCommand ?? (this.addDataCommand = new DelegateCommand<object>(para => this.AddData(para)));
            }
        }
        private ICommand doubleClickAddDataCommand;
        public ICommand DoubleClickAddDataCommand
        {
            get
            {
                return this.doubleClickAddDataCommand ?? (this.doubleClickAddDataCommand = new DelegateCommand<object>(para => this.DoubleClickAddData(para)));
            }
        }
        private ICommand refreshDataCommand;
        public ICommand RefreshDataCommand
        {
            get
            {
                return this.refreshDataCommand ?? (this.refreshDataCommand = new DelegateCommand<object>(para => this.RefreshData(para)));
            }
        }
        private ICommand clearDataCommand;
        public ICommand ClearDataCommand
        {
            get
            {
                return this.clearDataCommand ?? (this.clearDataCommand = new DelegateCommand<object>(para => this.ClearData(para)));
            }
        }

        private ICommand removeDataCommand;
        public ICommand RemoveDataCommand
        {
            get
            {
                return this.removeDataCommand ?? (this.removeDataCommand = new DelegateCommand<object>(para => this.RemoveData(para)));
            }
        }
        #endregion

        #region 私有变量
        private RMSReplayDataViewModel amsReplayVM;
        private TimeDomainDataViewModel timeDomainVM;
        private FrequencyDomainDataViewModel frequencyDomainVM;
        private PowerSpectrumDataViewModel powerSpectrumVM;
        private PowerSpectrumDensityDataViewModel powerSpectrumDensityVM;
        private TimeDomainDataViewModel timeDomainVM2;//多光标新增
        private FrequencyDomainDataViewModel frequencyDomainVM2;//多光标新增
        private PowerSpectrumDataViewModel powerSpectrumVM2;//多光标新增
        private PowerSpectrumDensityDataViewModel powerSpectrumDensityVM2;//多光标新增
        private OrthoDataViewModel orthoDataVM;
        private OffDesignConditionDataViewModel offDesignConditionVM;
        private OrderAnalysisDataViewModel orderAnalysisVM;
        private Time3DSpectrumDataViewModel time3DSpectrumVM;
        private RPM3DSpectrumDataViewModel rpm3DSpectrumVM;
        private AlarmPointTrendDataViewModel alarmPointTrendVM;
        private DistributionMapViewModel distributionMapVM;
        private SynchronizationContext uiContext = SynchronizationContext.Current;
        private Func<IEnumerable<BaseWaveChannelToken>, Task> trackTask { get; set; }
        private bool isTrackRunning;
        private Func<IEnumerable<BaseWaveChannelToken>, Task> track2Task { get; set; }//多光标新增
        private bool isTrack2Running;//多光标新增
        private List<Color> ColorList = new List<Color>();
        #endregion

        #region 初始化
        public Task Initialization { get; private set; }
        public async Task InitializeAsync()
        {
            try
            {
                amsReplayVM = new RMSReplayDataViewModel();
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

                timeDomainVM2 = new TimeDomainDataViewModel();
                timeDomainVM2.Title = "时域";
                timeDomainVM2.ItemWidth = ItemWidth;
                timeDomainVM2.ItemHeight = ItemHeight;

                frequencyDomainVM2 = new FrequencyDomainDataViewModel();
                frequencyDomainVM2.Title = "频域";
                frequencyDomainVM2.ItemWidth = ItemWidth;
                frequencyDomainVM2.ItemHeight = ItemHeight;

                powerSpectrumVM2 = new PowerSpectrumDataViewModel();
                powerSpectrumVM2.Title = "功率谱";
                powerSpectrumVM2.ItemWidth = ItemWidth;
                powerSpectrumVM2.ItemHeight = ItemHeight;

                powerSpectrumDensityVM2 = new PowerSpectrumDensityDataViewModel();
                powerSpectrumDensityVM2.Title = "功率谱密度";
                powerSpectrumDensityVM2.ItemWidth = ItemWidth;
                powerSpectrumDensityVM2.ItemHeight = ItemHeight;

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

                distributionMapVM = new DistributionMapViewModel();
                distributionMapVM.Title = "分布图";
                distributionMapVM.ItemWidth = ItemWidth;
                distributionMapVM.ItemHeight = ItemHeight;

                ShowAMS = true;
                ShowAlarmPointTrend = false;
                ShowTimeDomain = true;
                ShowFrequencyDomain = true;
                ShowOrtho = false;
                ShowOffCondition = false;
                ShowOrderAnalysis = false;
                ShowTime3DSpectrum = false;
                ShowRPM3DSpectrum = false;
                ShowDistributionMap = false;

                trackTask = AMSTrackChanged;
                track2Task = AMSTrack2Changed;

                amsReplayVM.WhenTrackChanged.Sample(TimeSpan.FromMilliseconds(500)).ObserveOn(uiContext).Subscribe(RaiseTrackChanged);
                amsReplayVM.WhenTrack2Changed.Sample(TimeSpan.FromMilliseconds(500)).ObserveOn(uiContext).Subscribe(RaiseTrack2Changed);

                historicalDataCollection.Add(amsReplayVM);
                //historicalDataCollection.Add(alarmPointTrendVM);
                historicalDataCollection.Add(timeDomainVM);
                historicalDataCollection.Add(timeDomainVM2);//多光标新增
                historicalDataCollection.Add(frequencyDomainVM);
                historicalDataCollection.Add(frequencyDomainVM2);//多光标新增
                historicalDataCollection.Add(powerSpectrumVM);
                historicalDataCollection.Add(powerSpectrumVM2);//多光标新增
                historicalDataCollection.Add(powerSpectrumDensityVM);                    
                historicalDataCollection.Add(powerSpectrumDensityVM2);//多光标新增
                historicalDataCollection.Add(orthoDataVM);
                historicalDataCollection.Add(offDesignConditionVM);
                historicalDataCollection.Add(orderAnalysisVM);
                historicalDataCollection.Add(time3DSpectrumVM);
                historicalDataCollection.Add(rpm3DSpectrumVM);
                historicalDataCollection.Add(distributionMapVM);             
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

                //进行2次筛选，避免获取数据过程中，删除了数据
                var updatetokens = tokens.Intersect(AddedChannels.OfType<BaseWaveChannelToken>());

                if (ShowTimeDomain)
                {
                    timeDomainVM.ChangeChannelData(updatetokens);
                }
                if (ShowFrequencyDomain)
                {
                    frequencyDomainVM.ChangeChannelData(updatetokens);
                }
                if (ShowPowerSpectrum)
                {
                    powerSpectrumVM.ChangeChannelData(updatetokens);
                }
                if (ShowPowerSpectrumDensity)
                {
                    powerSpectrumDensityVM.ChangeChannelData(updatetokens);
                }
                if (ShowAlarmPointTrend)
                {
                    //await alarmPointTrendVM.ChangeSnapshotData(updatetokens);
                }
                if (ShowOrtho)
                {
                    //await orthoDataVM.ChangeOrthoData(updatetokens);
                }              
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-TrackChanged", ex));
            }
        }

        private async void RaiseTrack2Changed(IEnumerable<BaseWaveChannelToken> tokens)
        {
            if (track2Task == null)
            {
                return;
            }

            if (isTrack2Running)
            {
                return;
            }

            try
            {
                // we're running it now
                isTrack2Running = true;
                await track2Task.Invoke(tokens);
            }
            catch (Exception)
            {
            }
            finally
            {
                // allow it to run again
                isTrack2Running = false;
            }
        }
        private async Task AMSTrack2Changed(IEnumerable<BaseWaveChannelToken> tokens)
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

                //进行2次筛选，避免获取数据过程中，删除了数据
                var updatetokens = tokens.Intersect(AddedChannels.OfType<BaseWaveChannelToken>());

                if (ShowTimeDomain)
                {
                    timeDomainVM2.ChangeChannelData(updatetokens);
                }
                if (ShowFrequencyDomain)
                {
                    frequencyDomainVM2.ChangeChannelData(updatetokens);
                }
                if (ShowPowerSpectrum)
                {
                    powerSpectrumVM2.ChangeChannelData(updatetokens);
                }
                if (ShowPowerSpectrumDensity)
                {
                    powerSpectrumDensityVM2.ChangeChannelData(updatetokens);
                }
                if (ShowAlarmPointTrend)
                {
                    //await alarmPointTrendVM.ChangeSnapshotData(updatetokens);
                }
                if (ShowOrtho)
                {
                    //await orthoDataVM.ChangeOrthoData(updatetokens);
                }
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-TrackChanged", ex));
            }
        }
        #endregion

        private void SelectedTreeChanged(object para)
        {
            SelectedTreeItem = para as ItemTreeItemViewModel;
            if (SelectedTreeItem is ItemTreeItemViewModel)
            {
                var itemTree = SelectedTreeItem as ItemTreeItemViewModel;
                if (itemTree.BaseAlarmSignal != null)
                {
                    if (itemTree.BaseAlarmSignal.Unit != null)
                    {
                        Unit = itemTree.BaseAlarmSignal.Unit;
                    }
                }
            }
        }
        private void DoubleClickAddData(object para)
        {
            SelectedTreeItem = para as ItemTreeItemViewModel;
            if (SelectedTreeItem is ItemTreeItemViewModel)
            {
                AddData(para);
            }
        }
        private async void AddData(object para)
        {
            var item = SelectedTreeItem as ItemTreeItemViewModel;
            if (item == null)
            {
#if XBAP
                MessageBox.Show("请选中要查询的测点", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请选中要查询的测点", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }

            if (Unit == null || Unit == "")
            {
#if XBAP
                MessageBox.Show("请选择要查询的测点的数据类型", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请选择要查询的测点的数据类型", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }

            if (addedChannels.Count >= 16)
            {
                return;
            }

            string conditionWave;
            string conditionAlarm;
            object[] objectWave;
            object[] objectAlarm;
            ConditionClass.GetConditionStr(out conditionWave, out conditionAlarm, out objectWave, out objectAlarm,  AllowNormal, AllowPreWarning, AllowWarning, AllowDanger, AllowInvalid, AllowRPMFilter,Unit, DownRPMFilter, UpRPMFilter);

            string selectedip = SelectedTreeItem.ServerIP;

            #region 分频            
            var divfre = SelectedTreeItem as DivFreTreeItemViewModel;
            if (divfre != null)
            {
                try
                {
                    WaitInfo = "获取数据中";
                    Status = ViewModelStatus.Querying;

                    var item_parent = divfre.Parent as ItemTreeItemViewModel;
                    var divfreinfo = divfre.T_DivFreInfo;
                    if (divfreinfo == null)
                    {
                        return;
                    }

                    var channel = _cardProcess.GetChannel(_hardwareService.ServerTreeItems, item_parent.T_Item);
                    if (channel == null || channel.IChannel == null)
                    {
                        return;
                    }

                    if (addedChannels.OfType<DivFreChannelToken>().Select(o => o.DivFreChannel).Contains(divfreinfo)) return;
                    var result = await _databaseComponent.GetHistoryData<D1_DivFreInfo>(selectedip, divfreinfo.Guid, null, StartTime.Value, EndTime.Value, null, null);
                    if (result == null || result.Count == 0)
                    {
                        return;
                    }

                    List<IBaseDivfreSlot> slotdata = null;
                    ChannelType channelType = ChannelType.None;

                    switch (item_parent.T_Item.ItemType)
                    {
                        case (int)ChannelType.IEPEChannelInfo:
                            {
                                channelType = ChannelType.IEPEChannelInfo;
                                var resultslot = await _databaseComponent.GetHistoryData<D_IEPESlot>(selectedip, item_parent.T_Item.Guid, null, StartTime.Value, EndTime.Value, null, null);
                                if (resultslot.Count == 0)
                                {
                                    return;
                                }
                                slotdata = resultslot.Select(p => p as IBaseDivfreSlot).ToList();
                                break;
                            }
                        case (int)ChannelType.EddyCurrentDisplacementChannelInfo:
                            {
                                channelType = ChannelType.EddyCurrentDisplacementChannelInfo;
                                var resultslot = await _databaseComponent.GetHistoryData<D_EddyCurrentDisplacementSlot>(selectedip, item_parent.T_Item.Guid, null, StartTime.Value, EndTime.Value, null, null);
                                if (resultslot.Count == 0)
                                {
                                    return;
                                }
                                slotdata = resultslot.Select(p => p as IBaseDivfreSlot).ToList();
                                break;
                            }
                        case (int)ChannelType.WirelessVibrationChannelInfo:
                            {
                                channelType = ChannelType.WirelessVibrationChannelInfo;
                                var resultslot = await _databaseComponent.GetHistoryData<D_WirelessVibrationSlot>(selectedip, item_parent.T_Item.Guid, null, StartTime.Value, EndTime.Value, null, null);
                                if (resultslot.Count == 0)
                                {
                                    return;
                                }
                                slotdata = resultslot.Select(p => p as IBaseDivfreSlot).ToList();
                                break;
                            }
                        default: return;
                    }

                    if (slotdata == null)
                    {
                        return;
                    }

                    DivFreChannelToken channeltoken = new DivFreChannelToken()
                    {
                        DisplayName = divfre.Parent.Name + "_" + divfre.Name,
                        IP = selectedip,
                        Guid = item_parent.T_Item.Guid,
                        DivFreChannel = divfreinfo,
                        DataContracts = result,
                        SlotDataContracts = slotdata,
                        ChannelType = channelType,
                    };

                    amsReplayVM.AddChannel(channeltoken);
                    offDesignConditionVM.AddChannel(channeltoken);

                    addedChannels.Add(channeltoken);
                }
                catch (Exception ex)
                {
                    _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-分频查询", ex));
                }
                finally
                {
                    Status = ViewModelStatus.None;
                }
                return;
            }
            #endregion

            #region 测点
            if (item != null && item.T_Item != null && item.T_Item.ItemType != 0)
            {
                if (addedChannels.Select(o => o.Guid).Contains(item.T_Item.Guid)) return;
                try
                {
                    WaitInfo = "获取数据中";
                    Status = ViewModelStatus.Querying;
                    SubAddData(item, conditionWave, conditionAlarm, objectWave, objectAlarm);
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
            else
            {
#if XBAP
                MessageBox.Show("该测点没绑定或无信息", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("该测点无信息", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }
            #endregion
        }

        private async void SubAddData(ItemTreeItemViewModel item, string conditionWave, string conditionAlarm, object[] objectWave, object[] objectAlarm)
        {
            List<IBaseAlarmSlot> result = await GetData(item.ServerIP, item.T_Item.Guid, item.T_Item.ItemType, conditionWave, conditionAlarm, objectWave, objectAlarm);

            if (result == null || result.Count == 0)
            {
#if XBAP
                MessageBox.Show("没有数据，请重新选择条件", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("没有数据，请重新选择条件", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }

            if (item.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
            {
                BaseDivfreChannelToken channeltoken = new BaseDivfreChannelToken()
                {
                    DisplayName = (item.BaseAlarmSignal != null) ? item.BaseAlarmSignal.DeviceItemName : item.FullName,
                    IP = item.ServerIP,
                    Guid = item.T_Item.Guid,
                    ItemType = item.T_Item.ItemType,
                    DataContracts = result,
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
                timeDomainVM2.AddChannel(channeltoken);//多光标新增
                frequencyDomainVM2.AddChannel(channeltoken);//多光标新增
                powerSpectrumVM2.AddChannel(channeltoken);//多光标新增
                powerSpectrumDensityVM2.AddChannel(channeltoken);//多光标新增
                //alarmPointTrendVM.AddChannel(channeltoken);
                //orthoDataVM.AddChannel(channeltoken);
                //offDesignConditionVM.AddChannel(channeltoken);
                distributionMapVM.AddChannel(channeltoken);
                addedChannels.Add(channeltoken);
            }
            else if (item.T_Item.ItemType == (int)ChannelType.WirelessScalarChannelInfo)
            {             
                BaseAlarmChannelToken channeltoken = new BaseAlarmChannelToken()
                {
                    DisplayName = (item.BaseAlarmSignal != null) ? item.BaseAlarmSignal.DeviceItemName : item.FullName,
                    IP = item.ServerIP,
                    Guid = item.T_Item.Guid,
                    ItemType = item.T_Item.ItemType,
                    DataContracts = result,
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
                distributionMapVM.AddChannel(channeltoken);
                addedChannels.Add(channeltoken);
            }
        }

        private async Task<List<IBaseAlarmSlot>> GetData(string serverip, Guid guid, int itemtype, string conditionWave, string conditionAlarm, object[] objectWave, object[] objectAlarm)
        {
            List<IBaseAlarmSlot> result = new List<IBaseAlarmSlot>();
            if (itemtype == (int)ChannelType.WirelessVibrationChannelInfo)
            {
                result = await _databaseComponent.GetUniformHistoryData(itemtype, serverip, guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade", "IsValidWave", "RecordLab", "RPM" }, StartTime.Value, EndTime.Value, conditionWave, objectWave);

            }
            else if (itemtype == (int)ChannelType.WirelessScalarChannelInfo)
            {
                result = await _databaseComponent.GetUniformHistoryData(itemtype, serverip, guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade" }, StartTime.Value, EndTime.Value, conditionAlarm, objectAlarm);
            }

            return result;
        }

        private async void RefreshData(object para)
        {
            string conditionWave;
            string conditionAlarm;
            object[] objectWave;
            object[] objectAlarm;
            ConditionClass.GetConditionStr(out conditionWave, out conditionAlarm, out objectWave, out objectAlarm, AllowNormal, AllowPreWarning, AllowWarning, AllowDanger, AllowInvalid, AllowRPMFilter, Unit, DownRPMFilter, UpRPMFilter);

            try
            {
                WaitInfo = "刷新数据中";
                Status = ViewModelStatus.Querying;

                var vibratonChannels = addedChannels.OfType<BaseDivfreChannelToken>().ToArray();
                if (vibratonChannels.Length > 0)
                {
                    foreach (var item in vibratonChannels)
                    {
                        List<IBaseAlarmSlot> result = await GetData(item.IP, item.Guid, item.ItemType, conditionWave, conditionAlarm, objectWave, objectAlarm);
                        item.DataContracts = result;                        
                    }
                }
                var alarmChannels = addedChannels.OfType<BaseAlarmChannelToken>().ToArray();
                if (alarmChannels.Length > 0)
                {
                    foreach (var item in alarmChannels)
                    {
                        List<IBaseAlarmSlot> result = await GetData(item.IP, item.Guid, item.ItemType, conditionWave, conditionAlarm, objectWave, objectAlarm);
                        item.DataContracts = result;
                    }
                }

                #region 分频
                var divfreChannels = addedChannels.OfType<DivFreChannelToken>().ToArray();
                if (divfreChannels.Length > 0)
                {
                    foreach (var item in divfreChannels)
                    {
                        var result = await _databaseComponent.GetHistoryData<D1_DivFreInfo>(item.IP, item.DivFreChannel.Guid, null, StartTime.Value, EndTime.Value, null, null);
                        List<IBaseDivfreSlot> slotdata = null;
                        ChannelType channelType = item.ChannelType;

                        switch (channelType)
                        {
                            case ChannelType.IEPEChannelInfo:
                                {                                  
                                    var resultslot = await _databaseComponent.GetHistoryData<D_IEPESlot>(item.IP, item.Guid, null, StartTime.Value, EndTime.Value, null, null);
                                    if (resultslot.Count == 0)
                                    {
                                        continue;
                                    }
                                    slotdata = resultslot.Select(p => p as IBaseDivfreSlot).ToList();
                                    break;
                                }
                            case ChannelType.EddyCurrentDisplacementChannelInfo:
                                {                                   
                                    var resultslot = await _databaseComponent.GetHistoryData<D_EddyCurrentDisplacementSlot>(item.IP, item.Guid, null, StartTime.Value, EndTime.Value, null, null);
                                    if (resultslot.Count == 0)
                                    {
                                        continue;
                                    }
                                    slotdata = resultslot.Select(p => p as IBaseDivfreSlot).ToList();
                                    break;
                                }
                            case ChannelType.WirelessVibrationChannelInfo:
                                {                                  
                                    var resultslot = await _databaseComponent.GetHistoryData<D_WirelessVibrationSlot>(item.IP, item.Guid, null, StartTime.Value, EndTime.Value, null, null);
                                    if (resultslot.Count == 0)
                                    {
                                        continue;
                                    }
                                    slotdata = resultslot.Select(p => p as IBaseDivfreSlot).ToList();
                                    break;
                                }
                            default: continue;
                        }
                       
                        item.DataContracts = result;
                        item.SlotDataContracts = slotdata;
                    }
                }
                #endregion

                amsReplayVM.ChannelDataChanged(addedChannels);
                distributionMapVM.ChangeChannelData(addedChannels);
                offDesignConditionVM.ChannelDataChanged(addedChannels);
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-通道数据跟新失败", ex));               
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }
        private void ClearData(object para)
        {
            ColorList.Clear();
            foreach (var token in addedChannels)
            {                
                amsReplayVM.RemoveChannel(token);
                //alarmPointTrendVM.RemoveChannel(token);
                timeDomainVM.RemoveChannel(token);
                frequencyDomainVM.RemoveChannel(token);
                powerSpectrumVM.RemoveChannel(token);
                powerSpectrumDensityVM.RemoveChannel(token);
                timeDomainVM2.RemoveChannel(token);//多光标新增
                frequencyDomainVM2.RemoveChannel(token);//多光标新增
                powerSpectrumVM2.RemoveChannel(token);//多光标新增
                powerSpectrumDensityVM2.RemoveChannel(token);//多光标新增
                orthoDataVM.RemoveChannel(token);
                time3DSpectrumVM.RemoveChannel(token);
                offDesignConditionVM.RemoveChannel(token);
                distributionMapVM.RemoveChannel(token);
            }
            addedChannels.Clear();
        }
        private void RemoveData(object para)
        {
            ChannelToken token = para as ChannelToken;
            if (addedChannels.Contains(token))
            {
                ColorList.Remove(token.SolidColorBrush.Color);

                addedChannels.Remove(token);
                amsReplayVM.RemoveChannel(token);
                //alarmPointTrendVM.RemoveChannel(token);
                timeDomainVM.RemoveChannel(token);
                frequencyDomainVM.RemoveChannel(token);
                powerSpectrumVM.RemoveChannel(token);
                powerSpectrumDensityVM.RemoveChannel(token);
                timeDomainVM2.RemoveChannel(token);//多光标新增
                frequencyDomainVM2.RemoveChannel(token);//多光标新增
                powerSpectrumVM2.RemoveChannel(token);//多光标新增
                powerSpectrumDensityVM2.RemoveChannel(token);//多光标新增
                orthoDataVM.RemoveChannel(token);
                time3DSpectrumVM.RemoveChannel(token);
                offDesignConditionVM.RemoveChannel(token);
                distributionMapVM.RemoveChannel(token);
            }
        }     

        public void Close()
        {
            foreach (var viewmodel in this.HistoricalDatas)
            {
                viewmodel.Close();
            }
        }
    }
  
}

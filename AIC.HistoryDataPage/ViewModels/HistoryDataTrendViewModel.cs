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
        public IObservable<IEnumerable<SignalToken>> WhenTrackChanged
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
                ColorList.Remove(token.SolidColorBrush.Color);
                if (SignalRemoved != null)
                {
                    SignalRemoved(token);
                }
            }
        }

        public void TrackChanged(IEnumerable<SignalToken> tokens)
        {
            if (trackChanged != null)
            {
                trackChanged(this, new TrendTrackChangedEventArgs(tokens));
            }
        }

        private void RaiseTrackChanged(IEnumerable<SignalToken> tokens)
        {

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

    }
}

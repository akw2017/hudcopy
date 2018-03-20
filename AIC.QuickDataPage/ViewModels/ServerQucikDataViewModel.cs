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
using System.Diagnostics;
using System.Threading;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;
using AIC.QuickDataPage.Views;
using Microsoft.Practices.ServiceLocation;
using Wpf.CloseTabControl;
using LiveCharts.Configurations;
using LiveCharts.Helpers;
using LiveCharts.Defaults;
using AIC.Core.Helpers;

namespace AIC.QuickDataPage.ViewModels
{
    class ServerQucikDataViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        private readonly ILocalConfiguration _localConfiguration;
        private readonly IRegionManager _regionManager;
        private readonly ILoginUserService _loginUserService;

        public ServerQucikDataViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess, ILocalConfiguration localConfiguration, IRegionManager regionManager, ILoginUserService loginUserService)
        {
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _signalProcess = signalProcess;
            _cardProcess = cardProcess;
            _localConfiguration = localConfiguration;
            _regionManager = regionManager;
            _loginUserService = loginUserService;

            _signalProcess.DailyChanged += DailyChanged;
            ServerInfoList = _localConfiguration.LoginServerInfoList;

            readDataTimer.Tick += new EventHandler(timeCycle);
            readDataTimer.Interval = new TimeSpan(0, 0, 0, 1);
            readDataTimer.Start();
        }
        #region 属性与字段
        public IEnumerable<ServerInfo> ServerInfoList { get; set; }

        private ServerInfo serverInfo;
        public ServerInfo ServerInfo
        {
            get { return serverInfo; }
            set
            {
                if (serverInfo != value)
                {
                    serverInfo = value;
                    OnPropertyChanged("ServerInfo");
                }
            }
        }

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

        private ObservableCollection<DeviceTreeItemViewModel> _deviceTreeItems;
        public ObservableCollection<DeviceTreeItemViewModel> DeviceTreeItems
        {
            get { return _deviceTreeItems; }
            set
            {
                _deviceTreeItems = value;
                OnPropertyChanged("DeviceTreeItems");
            }
        }

        private double? runningDays;
        public double? RunningDays
        {
            get { return runningDays; }
            set
            {
                if (runningDays != value)
                {
                    runningDays = value;
                    OnPropertyChanged("RunningDays");
                }
            }
        }

        private int itemNormalNumber;
        public int ItemNormalNumber
        {
            get { return itemNormalNumber; }
            set
            {
                if (itemNormalNumber != value)
                {
                    itemNormalNumber = value;
                    OnPropertyChanged("ItemNormalNumber");
                }
            }
        }

        private int itemAlarmNumber;
        public int ItemAlarmNumber
        {
            get { return itemAlarmNumber; }
            set
            {
                if (itemAlarmNumber != value)
                {
                    itemAlarmNumber = value;
                    OnPropertyChanged("ItemAlarmNumber");
                }
            }
        }

        private double itemAlarmRate;
        public double ItemAlarmRate
        {
            get { return itemAlarmRate; }
            set
            {
                if (itemAlarmRate != value)
                {
                    itemAlarmRate = value;
                    OnPropertyChanged("ItemAlarmRate");
                }
            }
        }

        private int deviceNormalNumber;
        public int DeviceNormalNumber
        {
            get { return deviceNormalNumber; }
            set
            {
                if (deviceNormalNumber != value)
                {
                    deviceNormalNumber = value;
                    OnPropertyChanged("DeviceNormalNumber");
                }
            }
        }

        private int deviceAlarmNumber;
        public int DeviceAlarmNumber
        {
            get { return deviceAlarmNumber; }
            set
            {
                if (deviceAlarmNumber != value)
                {
                    deviceAlarmNumber = value;
                    OnPropertyChanged("DeviceAlarmNumber");
                }
            }
        }

        private double deviceAlarmRate;
        public double DeviceAlarmRate
        {
            get { return deviceAlarmRate; }
            set
            {
                if (deviceAlarmRate != value)
                {
                    deviceAlarmRate = value;
                    OnPropertyChanged("DeviceAlarmRate");
                }
            }
        }


        private BaseAlarmSignal firstDangerItem;
        public BaseAlarmSignal FirstDangerItem
        {
            get { return firstDangerItem; }
            set
            {
                if (firstDangerItem != value)
                {
                    firstDangerItem = value;
                    OnPropertyChanged("FirstDangerItem");
                }
            }
        }

        private BaseAlarmSignal secondDangerItem;
        public BaseAlarmSignal SecondDangerItem
        {
            get { return secondDangerItem; }
            set
            {
                if (secondDangerItem != value)
                {
                    secondDangerItem = value;
                    OnPropertyChanged("SecondDangerItem");
                }
            }
        }

        private BaseAlarmSignal thirdDangerItem;
        public BaseAlarmSignal ThirdDangerItem
        {
            get { return thirdDangerItem; }
            set
            {
                if (thirdDangerItem != value)
                {
                    thirdDangerItem = value;
                    OnPropertyChanged("ThirdDangerItem");
                }
            }
        }

        private SeriesCollection pieSeries;
        public SeriesCollection PieSeries
        {
            get { return pieSeries; }
            set
            {

                if (pieSeries != value)
                {
                    pieSeries = value;
                    OnPropertyChanged("PieSeries");
                }
            }
        }

        public Func<double, string> PieFormatter { get; set; }

        private SeriesCollection stackedSeries;
        public SeriesCollection StackedSeries
        {
            get { return stackedSeries; }
            set
            {
                if (stackedSeries != value)
                {
                    stackedSeries = value;
                    OnPropertyChanged("StackedSeries");
                }
            }
        }

        private string[] stackedLabels;
        public string[] StackedLabels
        {
            get { return stackedLabels; }
            set
            {
                if (stackedLabels != value)
                {
                    stackedLabels = value;
                    OnPropertyChanged("StackedLabels");
                }
            }
        }

        private SeriesCollection trendSeries;
        public SeriesCollection TrendSeries
        {
            get { return trendSeries; }
            set
            {
                if (trendSeries != value)
                {
                    trendSeries = value;
                    OnPropertyChanged("TrendSeries");
                }
            }
        }

        private string[] trendLabels;
        public string[] TrendLabels
        {
            get { return trendLabels; }
            set
            {
                if (trendLabels != value)
                {
                    trendLabels = value;
                    OnPropertyChanged("TrendLabels");
                }
            }
        }

        public Func<double, string> TrendFormatter { get; set; }

        private SeriesCollection columnSeries;
        public SeriesCollection ColumnSeries
        {
            get { return columnSeries; }
            set
            {
                if (columnSeries != value)
                {
                    columnSeries = value;
                    OnPropertyChanged("ColumnSeries");
                }
            }
        }

        private string[] columnLabels;
        public string[] ColumnLabels
        {
            get { return columnLabels; }
            set
            {
                if (columnLabels != value)
                {
                    columnLabels = value;
                    OnPropertyChanged("ColumnLabels");
                }
            }
        }

        public Func<double, string> ColumnFormatter { get; set; }

        private int normalCount;
        public int NormalCount
        {
            get { return normalCount; }
            set
            {
                if (normalCount != value)
                {
                    normalCount = value;
                    this.OnPropertyChanged("NormalCount");
                }
            }
        }
        private int preAlarmCount;
        public int PreAlarmCount
        {
            get { return preAlarmCount; }
            set
            {
                if (preAlarmCount != value)
                {
                    preAlarmCount = value;
                    this.OnPropertyChanged("PreAlarmCount");
                }
            }
        }
        private int alarmCount;
        public int AlarmCount
        {
            get { return alarmCount; }
            set
            {
                if (alarmCount != value)
                {
                    alarmCount = value;
                    this.OnPropertyChanged("AlarmCount");
                }
            }
        }
        private int dangerCount;
        public int DangerCount
        {
            get { return dangerCount; }
            set
            {
                if (dangerCount != value)
                {
                    dangerCount = value;
                    this.OnPropertyChanged("DangerCount");
                }
            }
        }

        private int abnormalCount;
        public int AbnormalCount
        {
            get { return abnormalCount; }
            set
            {
                if (abnormalCount != value)
                {
                    abnormalCount = value;
                    this.OnPropertyChanged("AbnormalCount");
                }
            }
        }

        private int unConnectCount;
        public int UnConnectCount
        {
            get { return unConnectCount; }
            set
            {
                if (unConnectCount != value)
                {
                    unConnectCount = value;
                    this.OnPropertyChanged("UnConnectCount");
                }
            }
        }
        #endregion

        #region 命令

        private ICommand selectedServerChangedComamnd;
        public ICommand SelectedServerChangedComamnd
        {
            get
            {
                return this.selectedServerChangedComamnd ?? (this.selectedServerChangedComamnd = new DelegateCommand<object>(para => this.SelectedServerChanged(para)));
            }
        }

        private ICommand gotoCommand;
        public ICommand GotoCommand
        {
            get
            {
                return this.gotoCommand ?? (this.gotoCommand = new DelegateCommand<object>(para => this.Goto(para)));
            }
        }
        #endregion

        #region 管理树
        public void Init(ServerInfo serverinfo)
        {          
            ServerInfo = serverinfo;
            OrganizationTreeItems = new ObservableCollection<OrganizationTreeItemViewModel>(_organizationService.OrganizationTreeItems.Where(p => p.ServerIP == ServerInfo.IP));
            selectedsignals = _signalProcess.Signals.OfType<BaseAlarmSignal>().Where(p => p.ServerIP == ServerInfo.IP).ToList();
            DeviceTreeItems = new ObservableCollection<DeviceTreeItemViewModel>(_cardProcess.GetDevices(OrganizationTreeItems));
            DailyChanged();
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

        private List<BaseAlarmSignal> selectedsignals;


        public void SelectedServerChanged(object para)
        {
            if (para is ServerInfo)
            {
                Init(para as ServerInfo);
                timeCycle(null, null);
            }
        }

        private System.Windows.Threading.DispatcherTimer readDataTimer = new System.Windows.Threading.DispatcherTimer();
        private void timeCycle(object sender, EventArgs e)
        {
            #region 更新前三报警
            selectedsignals = selectedsignals.OrderByDescending(p => p.Low8Alarm).ThenByDescending(p => p.PercentResult).ToList();
            if (selectedsignals == null || selectedsignals.Count == 0)
            {
                FirstDangerItem = null;
                SecondDangerItem = null;
                ThirdDangerItem = null;
                return;
            }
            if (selectedsignals.Count >= 1)
            {
                FirstDangerItem = selectedsignals[0];
            }
            else
            {
                FirstDangerItem = null;
            }
            if (selectedsignals.Count >= 2)
            {
                SecondDangerItem = selectedsignals[1];
            }
            else
            {
                SecondDangerItem = null;
            }
            if (selectedsignals.Count >= 3)
            {
                ThirdDangerItem = selectedsignals[2];
            }
            else
            {
                ThirdDangerItem = null;
            }

            NormalCount = selectedsignals.Where(o => (o.DelayAlarmGrade == AlarmGrade.HighNormal || o.DelayAlarmGrade == AlarmGrade.LowNormal)).Count();
            PreAlarmCount = selectedsignals.Where(o => (o.DelayAlarmGrade == AlarmGrade.HighPreAlarm || o.DelayAlarmGrade == AlarmGrade.LowPreAlarm)).Count();
            AlarmCount = selectedsignals.Where(o => (o.DelayAlarmGrade == AlarmGrade.HighAlarm || o.DelayAlarmGrade == AlarmGrade.LowAlarm)).Count();
            DangerCount = selectedsignals.Where(o => (o.DelayAlarmGrade == AlarmGrade.HighDanger || o.DelayAlarmGrade == AlarmGrade.LowDanger)).Count();
            AbnormalCount = selectedsignals.Where(o => (o.DelayAlarmGrade == AlarmGrade.Abnormal)).Count();
            UnConnectCount = selectedsignals.Where(o => (o.DelayAlarmGrade == AlarmGrade.DisConnect)).Count();

            ItemNormalNumber = NormalCount + PreAlarmCount + AlarmCount + DangerCount;
            ItemAlarmNumber = PreAlarmCount + AlarmCount + DangerCount;
            ItemAlarmRate = (ItemNormalNumber == 0) ? 0 : (double)ItemAlarmNumber / ItemNormalNumber;

            DeviceNormalNumber = DeviceTreeItems.Where(p => p.IsRunning == true).Count();
            DeviceAlarmNumber = DeviceTreeItems.Where(p => p.Alarm  == AlarmGrade.HighPreAlarm || p.Alarm == AlarmGrade.LowPreAlarm
                                      ||  p.Alarm == AlarmGrade.HighAlarm || p.Alarm == AlarmGrade.HighAlarm
                                      || p.Alarm == AlarmGrade.HighDanger || p.Alarm == AlarmGrade.LowDanger).Count();
            DeviceAlarmRate = (DeviceNormalNumber == 0) ? 0 :  (double)DeviceAlarmNumber / DeviceNormalNumber;

            if (PieSeries == null)
            {
                InitPieSerices();
            }
            else
            {
                UpdatePieSerices();
            }
            #endregion
        }

        private void InitPieSerices()
        {
            PieSeries = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "无效",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(AbnormalCount) },
                    Fill = new SolidColorBrush(Color.FromRgb(0xff, 0x69, 0x84)),//粉色  
                },
                new PieSeries
                {
                    Title = "正常",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(NormalCount) },
                    Fill = new SolidColorBrush(Color.FromRgb(0x00, 0x80, 0x00)),//绿色 
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "预警",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(PreAlarmCount) },
                    Fill = new SolidColorBrush(Color.FromRgb(0xff, 0xff, 0x00)),//黄色 
                },
                new PieSeries
                {
                    Title = "警告",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(AlarmCount) },
                    Fill = new SolidColorBrush(Color.FromRgb(0xff, 0xa5, 0x00)),//橙色     
                },
                new PieSeries
                {
                    Title = "危险",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(DangerCount) },
                    Fill = new SolidColorBrush(Color.FromRgb(0xff, 0x00, 0x00)),//红色
                },              
                new PieSeries
                {
                    Title = "断线",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(UnConnectCount) },
                    Fill = new SolidColorBrush(Color.FromRgb(0x8B, 0x00, 0x00)),//黄色   
                },
            };
        }

        private void UpdatePieSerices()
        {
            if ((PieSeries[0].Values[0] as ObservableValue).Value != AbnormalCount)
            {
                PieSeries[0] = new PieSeries
                {
                    Title = "无效",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(AbnormalCount) },
                    Fill = new SolidColorBrush(Color.FromRgb(0xff, 0x69, 0x84)),//粉色  
                    DataLabels = (AbnormalCount > 0) ? true : false,
                };
            }
            if ((PieSeries[1].Values[0] as ObservableValue).Value != NormalCount)
            {
                PieSeries[1] = new PieSeries
                {
                    Title = "正常",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(NormalCount) },
                    Fill = new SolidColorBrush(Color.FromRgb(0x00, 0x80, 0x00)),//绿色 
                    DataLabels = (NormalCount > 0) ? true : false,
                };
            }
            if ((PieSeries[2].Values[0] as ObservableValue).Value != PreAlarmCount)
            {
                PieSeries[2] = new PieSeries
                {
                    Title = "预警",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(PreAlarmCount) },
                    Fill = new SolidColorBrush(Color.FromRgb(0xff, 0xff, 0x00)),//黄色 
                    DataLabels = (PreAlarmCount > 0) ? true : false,
                };
            }
            if ((PieSeries[3].Values[0] as ObservableValue).Value != AlarmCount)
            {
                PieSeries[3] = new PieSeries
                {
                    Title = "警告",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(AlarmCount) },
                    Fill = new SolidColorBrush(Color.FromRgb(0xff, 0xa5, 0x00)),//橙色 
                    DataLabels = (AlarmCount > 0) ? true : false,
                };
            }
            if ((PieSeries[4].Values[0] as ObservableValue).Value != DangerCount)
            {
                PieSeries[4] = new PieSeries
                {
                    Title = "危险",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(DangerCount) },
                    Fill = new SolidColorBrush(Color.FromRgb(0xff, 0x00, 0x00)),//红色
                    DataLabels = (DangerCount > 0) ? true : false,
                };
            }
            if ((PieSeries[5].Values[0] as ObservableValue).Value != UnConnectCount)
            {
                PieSeries[5] = new PieSeries
                {
                    Title = "断线",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(UnConnectCount) },
                    Fill = new SolidColorBrush(Color.FromRgb(0x8B, 0x00, 0x00)),//黄色
                    DataLabels = (UnConnectCount > 0) ? true : false,
                };
            }
        }
        private void DailyChanged()
        {
            if (_signalProcess.RunningDays != null && _signalProcess.RunningDays.Count > 0)
            {
                RunningDays = _signalProcess.RunningDays.Where(p => p.Key == ServerInfo.IP).Select(p => p.Value).FirstOrDefault();
            }

            var statisticalInfo = _signalProcess.StatisticalInformation;
            if (statisticalInfo == null || !statisticalInfo.ContainsKey(ServerInfo.IP))
            {
                return;
            }

            System.Windows.Application.Current.Dispatcher.Invoke((Action)(() =>//调用线程必须为 STA
            {
            TrendSeries = new SeriesCollection
                {
                    new StackedAreaSeries
                    {
                        Title = "预警点数",
                        Values = new ChartValues<int> { },
                        Fill = new SolidColorBrush(Color.FromRgb(0xff, 0xff, 0x00)),//黄色                        
                    },
                    new StackedAreaSeries
                    {
                        Title = "警告点数",
                        Values = new ChartValues<int> { },
                        Fill = new SolidColorBrush(Color.FromRgb(0xff, 0xa5, 0x00)),//橙色                        
                    },
                    new StackedAreaSeries
                    {
                        Title = "危险点数",
                        Values = new ChartValues<int> { },
                        Fill = new SolidColorBrush(Color.FromRgb(0xff, 0x00, 0x00)),//红色
                    },
                };
            TrendFormatter = value => value.ToString("0");

            var serverInfoList = new List<AlarmServerInfo>();
            var selectedguids = selectedsignals.Select(p => p.Guid).ToArray();
            var selectStatisticalInfo = statisticalInfo[ServerInfo.IP].Where(p => (selectedsignals == null || selectedguids.Contains(p.T_Item_Guid))).ToArray();

            if (selectStatisticalInfo != null)
            {
                //统计总报警
                {
                    List<Tuple<DateTime, int, int, int, int>> tuple = new List<Tuple<DateTime, int, int, int, int>>();
                    var daySelectStatisticalInfo = selectStatisticalInfo.OrderBy(p => p.ACQDatetime).GroupBy(p => p.ACQDatetime.Value.Date);
                    foreach (var daysignals in daySelectStatisticalInfo)
                    {
                        var allNumber = daysignals.Count();
                        var dangerNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 4).Count();
                        var alarmNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 3).Count();
                        var prealarmNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 2).Count();
                        tuple.Add(new Tuple<DateTime, int, int, int, int>(daysignals.FirstOrDefault().ACQDatetime.Value.Date, allNumber, dangerNumber, alarmNumber, prealarmNumber));
                    }

                    TrendLabels = tuple.Select(p => p.Item1.ToString("MM/dd")).ToArray();
                    TrendSeries[2].Values.AddRange(tuple.Select(p => p.Item3 as object));
                    TrendSeries[1].Values.AddRange(tuple.Select(p => p.Item4 as object));
                    TrendSeries[0].Values.AddRange(tuple.Select(p => p.Item5 as object));
                }

                foreach (var device in DeviceTreeItems)
                {
                    var itemguids = device.Children.OfType<ItemTreeItemViewModel>().Where(p => p.BaseAlarmSignal != null).Select(p => p.BaseAlarmSignal.Guid).ToArray();
                    var vibrationguids = device.Children.OfType<ItemTreeItemViewModel>().Where(p => p.BaseAlarmSignal != null && p.BaseAlarmSignal is BaseWaveSignal).Select(p => p.BaseAlarmSignal.Guid).ToArray();
                    List<Tuple<DateTime, int, int, int, int, bool>> tuple = new List<Tuple<DateTime, int, int, int, int, bool>>();
                    var daySelectStatisticalInfo = selectStatisticalInfo.Where(p => itemguids.Contains(p.T_Item_Guid)).OrderBy(p => p.ACQDatetime).GroupBy(p => p.ACQDatetime.Value.Date);
                    foreach (var daysignals in daySelectStatisticalInfo)
                    {
                        var allNumber = daysignals.Count();
                        var dangerNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 4).Count();
                        var alarmNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 3).Count();
                        var prealarmNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 2).Count();
                        int runitem = daysignals.Where(p => vibrationguids.Contains(p.T_Item_Guid) && (p.AlarmGrade & 0xff) >= 1).Count();
                        var running = (runitem >= vibrationguids.Count() - runitem);//运行判断
                        tuple.Add(new Tuple<DateTime, int, int, int, int, bool>(daysignals.FirstOrDefault().ACQDatetime.Value.Date, allNumber, dangerNumber, alarmNumber, prealarmNumber, running));
                    }
                    AlarmServerInfo serverinfo = new AlarmServerInfo();
                    serverinfo.Name = device.Name;
                    if (tuple.Count != 0)
                    {
                        serverinfo.AlarmRate = tuple.Select(p => (p.Item2 == 0) ? 0 : (double)(p.Item3 + p.Item4) / p.Item2).Average();
                        if (tuple.Select(p => (p.Item2 == 0) ? 0 : (p.Item3)).Average() > 0)
                        {
                            serverinfo.AlarmGrade = 4;
                        }
                        else if (tuple.Select(p => (p.Item2 == 0) ? 0 : (p.Item4)).Average() > 0)
                        {
                            serverinfo.AlarmGrade = 3;
                        }
                        else if (tuple.Select(p => (p.Item2 == 0) ? 0 : (p.Item5)).Average() > 0)
                        {
                            serverinfo.AlarmGrade = 2;
                        }
                        else
                        {
                            serverinfo.AlarmGrade = 1;
                        }
                        serverinfo.DangerNumber = tuple.Select(p => p.Item3).Sum();
                        serverinfo.AlarmNumber = tuple.Select(p => p.Item4).Sum();
                        serverinfo.PreAlarmNumber = tuple.Select(p => p.Item5).Sum();
                        serverinfo.DateRunning = tuple.Select(p => new Tuple<DateTime, bool>(p.Item1, p.Item6)).ToList();//取每一天的运行状态  
                    }
                    serverInfoList.Add(serverinfo);
                }

                var alarmserverInfoList = serverInfoList.Where(p => (p.DangerNumber > 0 || p.AlarmNumber > 0 || p.PreAlarmNumber > 0));

                StackedSeries = new SeriesCollection
                    {
                        new StackedColumnSeries
                        {
                            Title = "预警点数",
                            Values = new ChartValues<ObservableValue>(alarmserverInfoList.Select(p => new ObservableValue(p.PreAlarmNumber))),
                            Fill = new SolidColorBrush(Color.FromRgb(0xff, 0xff, 0x00)),//黄色   
                            DataLabels = true
                        },
                        new StackedColumnSeries
                        {
                            Title = "警告点数",
                            Values = new ChartValues<ObservableValue>(alarmserverInfoList.Select(p => new ObservableValue(p.AlarmNumber))),
                            Fill = new SolidColorBrush(Color.FromRgb(0xff, 0xa5, 0x00)),//橙色 
                            DataLabels = true
                        },
                        new StackedColumnSeries
                        {
                            Title = "危险点数",
                            Values = new ChartValues<ObservableValue>(alarmserverInfoList.Select(p => new ObservableValue(p.DangerNumber))),
                            Fill = new SolidColorBrush(Color.FromRgb(0xff, 0x00, 0x00)),//红色
                            DataLabels = true
                        }
                    };
                    StackedLabels = alarmserverInfoList.Select(p => p.Name).ToArray();

                    var allcount = serverInfoList.Count();
                    var runcount =  serverInfoList.Where(p => p.DateRunning != null).SelectMany(p => p.DateRunning.Where(run => run.Item2 == true)).GroupBy(p => p.Item1, (key, group) => new { Key = key, Value = group }).Select(p => p.Value.Count());
                    var stopcount = runcount.Select(p => (allcount - p));

                    ColumnSeries = new SeriesCollection
                            {
                                new ColumnSeries
                                {
                                    Title = "设备运行台数",
                                    Values = new ChartValues<ObservableValue>(runcount.Select(p => new ObservableValue(p))),
                                    Fill = new SolidColorBrush(Color.FromRgb(0x00, 0x80, 0x00)), //绿色     
                                    DataLabels = true
                                },
                                 new ColumnSeries
                                {
                                    Title = "设备停止台数",
                                      Values = new ChartValues<ObservableValue>(stopcount.Select(p => new ObservableValue(p))),
                                    Fill = new SolidColorBrush(Color.FromRgb(0xff, 0x00, 0x00)),//红色
                                    DataLabels = true
                                },
                            };
                    ColumnLabels = serverInfoList.Where(p => p.DateRunning != null).SelectMany(p => p.DateRunning.Where(run => run.Item2 == true).Select(time => time.Item1.ToString("MM/dd"))).Distinct().ToArray();
                    ColumnFormatter = value => value.ToString("0");
                }

            }));
        }

        private void Goto(object para)
        {
            if (para is BaseAlarmSignal)//测点
            {
                ItemQucikDataView view = _loginUserService.GotoTab<ItemQucikDataView>("MenuItemQucikData") as ItemQucikDataView;
                if (view != null)
                {
                    view.GotoItem(ServerInfo, para as BaseAlarmSignal);
                }
            }
            else if (para is DeviceTreeItemViewModel)//设备名
            {
                DeviceQucikDataView view = _loginUserService.GotoTab<DeviceQucikDataView>("MenuServerQucikData") as DeviceQucikDataView;
                if (view != null)
                {
                    view.GotoDevice(ServerInfo, para as DeviceTreeItemViewModel);
                }
            }
        }
    }


}

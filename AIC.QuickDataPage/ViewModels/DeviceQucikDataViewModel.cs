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

namespace AIC.QuickDataPage.ViewModels
{
     class DeviceQucikDataViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        private readonly ILocalConfiguration _localConfiguration;
        private readonly IRegionManager _regionManager;
        private readonly ILoginUserService _loginUserService;

        public DeviceQucikDataViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess, ILocalConfiguration localConfiguration, IRegionManager regionManager, ILoginUserService loginUserService)
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
            Init(null, null);

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

        private OrganizationTreeItemViewModel _organizationTreeItem;
        public OrganizationTreeItemViewModel OrganizationTreeItem
        {
            get { return _organizationTreeItem; }
            set
            {
                if (_organizationTreeItem != value)
                {
                    _organizationTreeItem = value;                   
                    OnPropertyChanged("OrganizationTreeItem");
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

        private SeriesCollection seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get { return seriesCollection; }
            set
            {
                if (seriesCollection != value)
                {
                    seriesCollection = value;
                    OnPropertyChanged("SeriesCollection");
                }
            }
        }

        private string[] labels;
        public string[] Labels
        {
            get { return labels; }
            set
            {
                if (labels != value)
                {
                    labels = value;
                    OnPropertyChanged("Labels");
                }
            }
        }

        public Func<double, string> YFormatter { get; set; }

        private ObservableCollection<AlarmObjectInfo> alarmServerInfoList;
        public ObservableCollection<AlarmObjectInfo> AlarmObjectInfoList
        {
            get { return alarmServerInfoList; }
            set
            {
                if (alarmServerInfoList != value)
                {
                    alarmServerInfoList = value;
                    OnPropertyChanged("AlarmObjectInfoList");
                }
            }
        }

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
        private ICommand selectedTreeChangedComamnd;
        public ICommand SelectedTreeChangedComamnd
        {
            get
            {
                return this.selectedTreeChangedComamnd ?? (this.selectedTreeChangedComamnd = new DelegateCommand<object>(para => this.SelectedTreeChanged(para)));
            }
        }

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
        public void Init(ServerInfo serverinfo, DeviceTreeItemViewModel device)
        {
            if (serverinfo != null)
            {
                ServerInfo = serverinfo;
            }
            else
            {
                ServerInfo = _localConfiguration.ServerInfoList.Where(p => p.IP == _loginUserService.LoginInfo.ServerInfo.IP).FirstOrDefault();
            }
            OrganizationTreeItems = new ObservableCollection<OrganizationTreeItemViewModel>(_organizationService.OrganizationTreeItems.Where(p => p.ServerIP == ServerInfo.IP));
            OrganizationTreeItem = device;
            if (OrganizationTreeItem == null)
            {
                DeviceTreeItems = new ObservableCollection<DeviceTreeItemViewModel>(_cardProcess.GetDevices(OrganizationTreeItems));
            }
            else
            {
                DeviceTreeItems = new ObservableCollection<DeviceTreeItemViewModel> { device };
            }
            selectedsignals = _signalProcess.Signals.OfType<BaseAlarmSignal>().Where(p => p.ServerIP == ServerInfo.IP).ToList();
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

        private void SelectedServerChanged(object para)
        {
            if (para is ServerInfo)
            {
                Init(para as ServerInfo, null);
                timeCycle(null, null);
            }
        }

        private void SelectedTreeChanged(object para)
        {
            if (para is OrganizationTreeItemViewModel)
            {
                OrganizationTreeItem = para as OrganizationTreeItemViewModel;
                selectedsignals = _cardProcess.GetItems(para as OrganizationTreeItemViewModel).Where(p => p.BaseAlarmSignal != null).Select(p => p.BaseAlarmSignal).ToList();
                DeviceTreeItems = new ObservableCollection<DeviceTreeItemViewModel>(_cardProcess.GetDevices(para as OrganizationTreeItemViewModel));
                DailyChanged();
                timeCycle(null, null);
            }
        }

        private System.Windows.Threading.DispatcherTimer readDataTimer = new System.Windows.Threading.DispatcherTimer();
        private void timeCycle(object sender, EventArgs e)
        {
            #region 更新前三报警
            if (selectedsignals == null)
            {
                return;
            }
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
            #endregion
        }

        private void DailyChanged()
        {
            var statisticalInfo = _signalProcess.StatisticalInformation;
            if (statisticalInfo == null || !statisticalInfo.ContainsKey(ServerInfo.IP))
            {
                return;
            }

            System.Windows.Application.Current.Dispatcher.Invoke((Action)(() =>//调用线程必须为 STA
            {
                var selectedguids = selectedsignals.Select(p => p.Guid).ToArray();              
                var tuple = _signalProcess.GetStatisticalAlarmNumber(ServerInfo.IP, selectedguids);
                DrawSeriesCollection(tuple);

                var serverInfoList = _signalProcess.GetStatisticalAlarmAlarmRate(ServerInfo.IP, DeviceTreeItems.ToArray());
                AlarmObjectInfoList = new ObservableCollection<AlarmObjectInfo>(serverInfoList);
            }));
        }

        private void DrawSeriesCollection(List<Tuple<DateTime, int, int, int, int>> tuple)
        {
            if (tuple == null) return;
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "预警点数",
                    Values = new ChartValues<int> { },
                    Stroke = new SolidColorBrush(Color.FromRgb(0xff, 0xff, 0x00)),//黄色                        
                },
                new LineSeries
                {
                    Title = "警告点数",
                    Values = new ChartValues<int> { },
                    Stroke = new SolidColorBrush(Color.FromRgb(0xff, 0xa5, 0x00)),//橙色                        
                },
                new LineSeries
                {
                    Title = "危险点数",
                    Values = new ChartValues<int> { },
                    Stroke = new SolidColorBrush(Color.FromRgb(0xff, 0x00, 0x00)),//红色
                },
            };
            YFormatter = value => value.ToString("0");
            Labels = tuple.Select(p => p.Item1.ToString("MM/dd")).ToArray();
            SeriesCollection[2].Values.AddRange(tuple.Select(p => p.Item3 as object));
            SeriesCollection[1].Values.AddRange(tuple.Select(p => p.Item4 as object));
            SeriesCollection[0].Values.AddRange(tuple.Select(p => p.Item5 as object));
        }

        private void Goto(object para)
        {
            if (para is BaseAlarmSignal)//测点
            {
                ItemQucikDataView view = _loginUserService.GotoTab<ItemQucikDataView>("MenuItemQucikData") as ItemQucikDataView;
                if (view != null)
                {
                    view.GotoItem(_loginUserService.GetServerInfo((para as BaseAlarmSignal).ServerIP), para as BaseAlarmSignal);
                }
            }
            else if (para is string)//设备名
            {
                var device = DeviceTreeItems.Where(p => p.Name == para as string).FirstOrDefault();
                SelectedTreeChanged(device);
            }
        }
    }
}

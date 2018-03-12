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

            _signalProcess.StatisticalInformationDataChanged += StatisticalInformationDataChanged;   
            InitTree();

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

        private ObservableCollection<AlarmServerInfo> alarmServerInfoList;
        public ObservableCollection<AlarmServerInfo> AlarmServerInfoList
        {
            get { return alarmServerInfoList; }
            set
            {
                if (alarmServerInfoList != value)
                {
                    alarmServerInfoList = value;
                    OnPropertyChanged("AlarmServerInfoList");
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
        private void InitTree()
        {
            ServerInfoList = _localConfiguration.LoginServerInfoList;
            ServerInfo = _loginUserService.GotoServerInfo;
            OrganizationTreeItems = new ObservableCollection<OrganizationTreeItemViewModel>(_organizationService.OrganizationTreeItems.Where(p => p.ServerIP == ServerInfo.IP));
            DeviceTreeItems = new ObservableCollection<DeviceTreeItemViewModel>(_cardProcess.GetDevices(OrganizationTreeItems));
            selectedsignals = _signalProcess.Signals.OfType<BaseAlarmSignal>().Where(p => p.ServerIP == ServerInfo.IP).ToList();
            StatisticalInformationDataChanged();
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
                InitTree();
                timeCycle(null, null);
            }
        }

        public void SelectedTreeChanged(object para)
        {
            if (para is OrganizationTreeItemViewModel)
            {
                OrganizationTreeItem = para as OrganizationTreeItemViewModel;
                selectedsignals = _cardProcess.GetItems(para as OrganizationTreeItemViewModel).Where(p => p.BaseAlarmSignal != null).Select(p => p.BaseAlarmSignal).ToList();
                DeviceTreeItems = new ObservableCollection<DeviceTreeItemViewModel>(_cardProcess.GetDevices(para as OrganizationTreeItemViewModel));
                StatisticalInformationDataChanged();
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
            #endregion
        }

        private void StatisticalInformationDataChanged()
        {
            var statisticalInfo = _signalProcess.StatisticalInformation;
            if (statisticalInfo == null || !statisticalInfo.ContainsKey(ServerInfo.IP))
            {
                return;
            }

            System.Windows.Application.Current.Dispatcher.Invoke((Action)(() =>//调用线程必须为 STA
            {
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
                        Title = "报警点数",
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
                Labels = new string[] { };
                YFormatter = value => value.ToString("0");

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

                        Labels = tuple.Select(p => p.Item1.ToString("MM/dd")).ToArray();
                        SeriesCollection[2].Values.AddRange(tuple.Select(p => p.Item3 as object));
                        SeriesCollection[1].Values.AddRange(tuple.Select(p => p.Item4 as object));
                        SeriesCollection[0].Values.AddRange(tuple.Select(p => p.Item5 as object));
                    }

                    foreach (var device in DeviceTreeItems)
                    {
                        var itemguids = device.Children.OfType<ItemTreeItemViewModel>().Where(p => p.BaseAlarmSignal != null).Select(p => p.BaseAlarmSignal.Guid).ToArray();
                        List<Tuple<DateTime, int, int, int, int>> tuple = new List<Tuple<DateTime, int, int, int, int>>();
                        var daySelectStatisticalInfo = selectStatisticalInfo.Where(p => itemguids.Contains(p.T_Item_Guid)).OrderBy(p => p.ACQDatetime).GroupBy(p => p.ACQDatetime.Value.Date);
                        foreach (var daysignals in daySelectStatisticalInfo)
                        {
                            var allNumber = daysignals.Count();
                            var dangerNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 4).Count();
                            var alarmNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 3).Count();
                            var prealarmNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 2).Count();
                            tuple.Add(new Tuple<DateTime, int, int, int, int>(daysignals.FirstOrDefault().ACQDatetime.Value.Date, allNumber, dangerNumber, alarmNumber, prealarmNumber));
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
                        }
                        serverInfoList.Add(serverinfo);
                        serverInfoList = serverInfoList.OrderByDescending(p => p.AlarmRate).ToList();
                        serverInfoList.ForEach(p => p.Index = serverInfoList.IndexOf(p) + 1);
                    }
                }
                AlarmServerInfoList = new ObservableCollection<AlarmServerInfo>(serverInfoList);
            }));
        }

        private void Goto(object para)
        {
            if (para is BaseAlarmSignal)//测点
            {
                _loginUserService.SetGotoServerInfo((para as BaseAlarmSignal).ServerIP);
                _loginUserService.SetGotoSignal(para as BaseAlarmSignal);
                ItemQucikDataView view = _loginUserService.GotoTab<ItemQucikDataView>("MenuItemQucikData") as ItemQucikDataView;
                if (view != null)
                {
                    view.GotoItem(para as BaseAlarmSignal);
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

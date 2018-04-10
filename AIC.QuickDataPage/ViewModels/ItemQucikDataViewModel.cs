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
using LiveCharts.Defaults;
using LiveCharts.Configurations;

namespace AIC.QuickDataPage.ViewModels
{
     class ItemQucikDataViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        private readonly ILocalConfiguration _localConfiguration;
        private readonly ILoginUserService _loginUserService;

        public ItemQucikDataViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess, ILocalConfiguration localConfiguration, ILoginUserService loginUserService)
        {
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _signalProcess = signalProcess;
            _cardProcess = cardProcess;
            _localConfiguration = localConfiguration;
            _loginUserService = loginUserService;

            _signalProcess.DailyChanged += DailyChanged;
            ServerInfoList = _localConfiguration.LoginServerInfoList;
            Init(null, null);
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

        private ObservableCollection<ItemTreeItemViewModel> _itemTreeItems;
        public ObservableCollection<ItemTreeItemViewModel> ItemTreeItems
        {
            get { return _itemTreeItems; }
            set
            {
                _itemTreeItems = value;
                OnPropertyChanged("ItemTreeItems");
            }
        }

        private ItemTreeItemViewModel _firstItemTreeItem;
        public ItemTreeItemViewModel FirstItemTreeItem
        {
            get { return _firstItemTreeItem; }
            set
            {
                if (_firstItemTreeItem != value)
                {
                    if (_firstItemTreeItem != null)
                    {
                        RemoveProcessorTrend(_firstItemTreeItem.BaseAlarmSignal);//去除趋势保存
                    }
                    _firstItemTreeItem = value;
                    if (_firstItemTreeItem != null)
                    {
                        AddProcessorTrend(_firstItemTreeItem.BaseAlarmSignal);//添加趋势保存
                    }
                    OnPropertyChanged("FirstItemTreeItem");
                    OnPropertyChanged("FirstItemInfo");
                }
            }
        }

        public string FirstItemInfo
        {
            get
            {
                if (FirstItemTreeItem != null && FirstItemTreeItem.T_Item != null)
                {
                    return "测点名称:" + FirstItemTreeItem.Name + "\r\n" +
                        "测点类型:" + FirstItemTreeItem.T_Item.GetHardWaveType() + "\r\n" +
                        "测点地址:" + FirstItemTreeItem.T_Item.GetHardWaveInfo() + "\r\n" +
                        "测点报警:" + ((FirstItemTreeItem.BaseAlarmSignal == null) ? "" : FirstItemTreeItem.BaseAlarmSignal.AlarmLimitString);
                }
                else
                {
                    return null;
                }
            }
        }

        private BaseAlarmSignal AlarmSignal { get; set; }

        private StatisticalInformationData firstDangerItem = new StatisticalInformationData();
        public StatisticalInformationData FirstDangerItem
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

        private StatisticalInformationData secondDangerItem = new StatisticalInformationData();
        public StatisticalInformationData SecondDangerItem
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

        private StatisticalInformationData thirdDangerItem = new StatisticalInformationData();
        public StatisticalInformationData ThirdDangerItem
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

        //第一个
        private SeriesCollection statisticalSeries;
        public SeriesCollection StatisticalSeries
        {
            get { return statisticalSeries; }
            set
            {
                if (statisticalSeries != value)
                {
                    statisticalSeries = value;
                    OnPropertyChanged("StatisticalSeries");
                }
            }
        }

        private string[] statisticalLabels;
        public string[] StatisticalLabels
        {
            get { return statisticalLabels; }
            set
            {
                if (statisticalLabels != value)
                {
                    statisticalLabels = value;
                    OnPropertyChanged("StatisticalLabels");
                }
            }
        }

        public Func<double, string> StatisticalFormatter { get; set; }

        //第二个
        public Func<double, string> TrendFormatter { get; set; }

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
        public void Init(ServerInfo serverinfo, BaseAlarmSignal sg)
        {
            if (serverinfo != null)
            {
                ServerInfo = serverinfo;
            }
            else
            {
                ServerInfo = _localConfiguration.ServerInfoList.Where(p => p.IP == _loginUserService.LoginInfo.ServerInfo.IP).FirstOrDefault();
            }
            AlarmSignal = sg;
            OrganizationTreeItems = new ObservableCollection<OrganizationTreeItemViewModel>(_organizationService.OrganizationTreeItems.Where(p => p.ServerIP == ServerInfo.IP));
            ItemTreeItems = new ObservableCollection<ItemTreeItemViewModel>(_cardProcess.GetItems(OrganizationTreeItems));
            FirstItemTreeItem = ItemTreeItems.Where(p => p.BaseAlarmSignal == AlarmSignal).FirstOrDefault();//默认测点
            if (FirstItemTreeItem != null)
            {
                ItemTreeItems = new ObservableCollection<ItemTreeItemViewModel> { FirstItemTreeItem };//默认只选择该测点
            }
            else
            {
                FirstItemTreeItem = new ItemTreeItemViewModel() { BaseAlarmSignal = new BaseAlarmSignal(new Guid()) };//避免为空
            }

            FirstDangerItem = new StatisticalInformationData();
            SecondDangerItem = new StatisticalInformationData();
            ThirdDangerItem = new StatisticalInformationData();
            DailyChanged();
            //TreeExpanded();

            InitTrendSeries();
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

        #region 选择控制
        public void SelectedServerChanged(object para)
        {
            if (para is ServerInfo)
            {
                AlarmSignal = null;
                Init(para as ServerInfo, AlarmSignal);
            }
        }
        public void SelectedTreeChanged(object para)
        {
            if (para is OrganizationTreeItemViewModel)
            {                
                ItemTreeItems = new ObservableCollection<ItemTreeItemViewModel>(_cardProcess.GetItems(para as OrganizationTreeItemViewModel));
                if (para is ItemTreeItemViewModel)
                {
                    FirstItemTreeItem = para as ItemTreeItemViewModel;
                }
                else
                {
                    FirstItemTreeItem = new ItemTreeItemViewModel() { BaseAlarmSignal = new BaseAlarmSignal(new Guid()) };//避免界面不显示
                }
                DailyChanged();
                ClearTrendSeries();
            }
        }
        #endregion

        #region 统计更新
        private void DailyChanged()
        {
            var statisticalInfo = _signalProcess.StatisticalInformation;
            if (statisticalInfo == null || !statisticalInfo.ContainsKey(ServerInfo.IP))
            {
                return;
            }

            var itemguids = ItemTreeItems.Where(p => p.BaseAlarmSignal != null).Select(p => p.BaseAlarmSignal.Guid).ToArray();
            if (FirstItemTreeItem == null || FirstItemTreeItem.BaseAlarmSignal == null || FirstItemTreeItem.BaseAlarmSignal.Guid == new Guid())//如果没有默认测点
            {
                //匹配出最大的测点   
                var firstStatisticalInfo = statisticalInfo[ServerInfo.IP].Where(p => itemguids.Contains(p.T_Item_Guid)).OrderByDescending(p => p.Low8Alarm).ThenByDescending(p => p.PercentResult).FirstOrDefault();
                if (firstStatisticalInfo != null)
                {
                    FirstItemTreeItem = ItemTreeItems.Where(p => p.BaseAlarmSignal.Guid == firstStatisticalInfo.T_Item_Guid).FirstOrDefault();
                }
            }

            if (FirstItemTreeItem == null || FirstItemTreeItem.BaseAlarmSignal == null)
            {
                FirstItemTreeItem = new ItemTreeItemViewModel() { BaseAlarmSignal = new BaseAlarmSignal(new Guid()) };//避免界面不显示
                return;
            }

            System.Windows.Application.Current.Dispatcher.Invoke((Action)(() =>//调用线程必须为 STA
            {
                var statisticalItem = statisticalInfo[ServerInfo.IP].Where(p => p.T_Item_Guid == FirstItemTreeItem.BaseAlarmSignal.Guid).OrderByDescending(p => p.Low8Alarm).ThenByDescending(p => p.PercentResult).ToList();
                if (statisticalItem == null || statisticalItem.Count == 0)
                {
                    return;
                }
                if (statisticalItem.Count >= 1)
                {
                    FirstDangerItem = statisticalItem[0];
                }
                else
                {
                    FirstDangerItem = new StatisticalInformationData();
                }
                if (statisticalItem.Count >= 2)
                {
                    SecondDangerItem = statisticalItem[1];
                }
                else
                {
                    SecondDangerItem = new StatisticalInformationData();
                }
                if (statisticalItem.Count >= 3)
                {
                    ThirdDangerItem = statisticalItem[2];
                }
                else
                {
                    ThirdDangerItem = new StatisticalInformationData();
                }

                var tuple = _signalProcess.GetStatisticalAlarmNumber(ServerInfo.IP, itemguids);
                DrawStatisticalSeries(tuple);

                var serverInfoList = _signalProcess.GetStatisticalAlarmAlarmRate(ServerInfo.IP, ItemTreeItems.ToArray());
                AlarmObjectInfoList = new ObservableCollection<AlarmObjectInfo>(serverInfoList);
            }));
        }

        private void DrawStatisticalSeries(List<Tuple<DateTime, int, int, int, int>> tuple)
        {
            if (tuple == null) return;
            StatisticalSeries = new SeriesCollection
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
            StatisticalLabels = new string[] { };
            StatisticalFormatter = value => value.ToString("0");
            
            StatisticalLabels = tuple.Select(p => p.Item1.ToString("MM/dd")).ToArray();
            StatisticalSeries[2].Values.AddRange(tuple.Select(p => p.Item3 as object));
            StatisticalSeries[1].Values.AddRange(tuple.Select(p => p.Item4 as object));
            StatisticalSeries[0].Values.AddRange(tuple.Select(p => p.Item5 as object));
        }
        #endregion

        #region 趋势更新
        private void AddProcessorTrend(BaseAlarmSignal Signal)
        {
            if (Signal is BaseAlarmSignal)
            {
                Signal.AddProcessorTrend();
            }
        }

        public void RemoveProcessorTrend(BaseAlarmSignal Signal)
        {
            if (Signal is BaseAlarmSignal)
            {
                Signal.RemoveProcessorTrend();
            }
        }

        private void InitTrendSeries()
        {
            _eventAggregator.GetEvent<SignalBroadcastingEvent>().Subscribe(SignalBroadcastingEvent, ThreadOption.UIThread);//<!--昌邑石化-->
            var dayConfig = Mappers.Xy<DateModel>().X(dayModel => (double)dayModel.DateTime.Ticks / TimeSpan.FromSeconds(1).Ticks).Y(dayModel => dayModel.Value)
                .Fill(dayModel => AlarmGradeColor(dayModel.AlarmGrade))
                .Stroke(dayModel => AlarmGradeColor(dayModel.AlarmGrade));

            TrendSeries = new SeriesCollection(dayConfig)
            {
                new LineSeries
                {
                    Values = new ChartValues<DateModel>
                    {
                        //new DateModel
                        //{
                        //    DateTime = System.DateTime.Now,
                        //    Value = 5
                        //},                       
                    },
                    Fill = Brushes.Transparent,
                    Title = "采集值"
                },
            };

            TrendFormatter = value => new System.DateTime((long)(value * TimeSpan.FromSeconds(1).Ticks)).ToString("MM-dd HH:mm");
        }

        private SolidColorBrush AlarmGradeColor(AlarmGrade grade)
        {
            switch (grade)
            {
                case AlarmGrade.Invalid: return null;
                case AlarmGrade.HighNormal:
                case AlarmGrade.LowNormal: return null;
                case AlarmGrade.HighPreAlarm:
                case AlarmGrade.LowPreAlarm: return new SolidColorBrush(Color.FromRgb(0xff, 0xff, 0x00));//黄色
                case AlarmGrade.HighAlarm:
                case AlarmGrade.LowAlarm: return new SolidColorBrush(Color.FromRgb(0xff, 0xa5, 0x00));//橙色
                case AlarmGrade.HighDanger:
                case AlarmGrade.LowDanger: return new SolidColorBrush(Color.FromRgb(0xff, 0x00, 0x00));//红色
                case AlarmGrade.DisConnect: return null;
                default: return null;
            }
        }
        private void ClearTrendSeries()
        {
            TrendSeries[0].Values.Clear();
        }

        private void SignalBroadcastingEvent(object obj)
        {           
            if (FirstItemTreeItem.BaseAlarmSignal != null && FirstItemTreeItem.BaseAlarmSignal.TrendData != null)
            {
                var oldpoints = (TrendSeries[0].Values as ChartValues<DateModel>);
                var olddatetimes = oldpoints.Select(p => p.DateTime);
                var trenddata = FirstItemTreeItem.BaseAlarmSignal.TrendData;
                var trenddatetimes = trenddata.Select(p => p.ACQDateTime);

                foreach (var point in oldpoints)//删除历史数据
                {
                    if (trenddatetimes.Contains(point.DateTime) || FirstItemTreeItem.BaseAlarmSignal.ACQDatetime == point.DateTime)
                    {
                        continue;
                    }
                    else
                    {
                        TrendSeries[0].Values.Remove(point);
                    }
                }
                TrendSeries[0].Values.AddRange(trenddata.Where(p => !olddatetimes.Contains(p.ACQDateTime)).Select(p => new DateModel { DateTime = p.ACQDateTime, Value = p.Result, AlarmGrade = (AlarmGrade)(p.AlarmGrade & 0x00ffff00) }));//添加历史数据
                if (FirstItemTreeItem.BaseAlarmSignal.Result != null && (TrendSeries[0].Values as ChartValues<DateModel>).Where(p => p.DateTime == FirstItemTreeItem.BaseAlarmSignal.ACQDatetime).Count() == 0)//添加实时数据
                {
                    TrendSeries[0].Values.Add(new DateModel { DateTime = FirstItemTreeItem.BaseAlarmSignal.ACQDatetime.Value, Value = FirstItemTreeItem.BaseAlarmSignal.Result.Value, AlarmGrade = FirstItemTreeItem.BaseAlarmSignal.AlarmGrade });
                }
            }
        }
        #endregion

        private void Goto(object para)
        {
            if (para is BaseAlarmSignal)//测点
            {
                Init(ServerInfo, para as BaseAlarmSignal);
            }
        }
    }

    public class DateModel
    {
        public System.DateTime DateTime { get; set; }
        public double Value { get; set; }

        public AlarmGrade AlarmGrade { get; set; }
    }
}

using AIC.Core.Models;
using AIC.Core.Events;
using AIC.HomePage.Menus;
using AIC.ServiceInterface;
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
using AIC.Resources.Models;
using AIC.Core.ControlModels;
using AIC.Core.OrganizationModels;
using AIC.Core.SignalModels;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Threading;
using System.Windows.Media;
using AIC.HomePage.Models;
using Microsoft.Practices.ServiceLocation;
using Wpf.CloseTabControl;
using AIC.OnLineDataPage.Views;
using AIC.QuickDataPage.Views;

namespace AIC.HomePage.ViewModels
{
    class HomeMapViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILocalConfiguration _localConfiguration;
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        private readonly ILoginUserService _loginUserService;

        public delegate void ShowMapAlarm();
        public event ShowMapAlarm ShowMapAlarmChanged;
        public delegate void ShowMapServer();
        public event ShowMapServer ShowMapServerChanged;

        public HomeMapViewModel(ILocalConfiguration localConfiguration, IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess, ILoginUserService loginUserService)
        {
            _localConfiguration = localConfiguration;
            _loginUserService = loginUserService;
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _signalProcess = signalProcess;
            _cardProcess = cardProcess;

            _signalProcess.StatisticalInformationDataChanged += StatisticalInformationDataChanged;

            ServerInfoList = _localConfiguration.LoginServerInfoList;
            ServerInfo = _loginUserService.LoginInfo.ServerInfo;

            readDataTimer.Tick += new EventHandler(timeCycle);
            readDataTimer.Interval = new TimeSpan(0, 0, 0, 1);
            readDataTimer.Start();
        } 

        #region 属性与字段
        private ServerInfo serverInfo;
        public ServerInfo ServerInfo
        {
            get { return serverInfo; }
            set
            {
                serverInfo = value;
                OnPropertyChanged("ServerInfo");
            }
        }

        public IEnumerable<ServerInfo> ServerInfoList { get; set; }

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
        private ICommand selectedServerChangedComamnd;
        public ICommand SelectedServerChangedComamnd
        {
            get
            {
                return this.selectedServerChangedComamnd ?? (this.selectedServerChangedComamnd = new DelegateCommand(() => this.SelectedServerChanged()));
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

        private List<BaseAlarmSignal> selectedsignals;
        public void SelectedServerChanged()
        {
            selectedsignals = _signalProcess.Signals.OfType<BaseAlarmSignal>().Where(p => p.ServerIP == ServerInfo.IP).ToList();
            timeCycle(null, null);
            if (ShowMapServerChanged != null)
            {
                ShowMapServerChanged();
                StatisticalInformationDataChanged();
            }
        }

        private System.Windows.Threading.DispatcherTimer readDataTimer = new System.Windows.Threading.DispatcherTimer();
        private void timeCycle(object sender, EventArgs e)
        {
            #region 更新前三报警
            if (selectedsignals == null)
            {
                selectedsignals = _signalProcess.Signals.OfType<BaseAlarmSignal>().OrderByDescending(p => p.Low8Alarm).ThenByDescending(p => p.PercentResult).ToList();
            }
            else
            {
                selectedsignals = selectedsignals.OrderByDescending(p => p.Low8Alarm).ThenByDescending(p => p.PercentResult).ToList();
            }
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

            bool update = false;
            foreach (var server in _localConfiguration.LoginServerInfoList)
            {
                var signals = _signalProcess.Signals.OfType<BaseAlarmSignal>().Where(p => p.ServerIP == server.IP).OrderByDescending(p => p.Low8Alarm).ThenByDescending(p => p.PercentResult).GroupBy(p => p.Low8Alarm, (key, group) => new { Key = key, Value = group }).FirstOrDefault();
                if (signals != null)
                {
                    if (server.AlarmGrade != signals.Value.FirstOrDefault().DelayAlarmGrade || server.AlarmCount != signals.Value.Count())
                    {
                        server.AlarmGrade = signals.Value.FirstOrDefault().DelayAlarmGrade;
                        server.AlarmCount = signals.Value.Count();
                        update = true;
                    }
                }
            }

            if (update == true && ShowMapAlarmChanged != null)
            {
                ShowMapAlarmChanged();
            }
        }

        private void StatisticalInformationDataChanged()
        {
            var statisticalresult = _signalProcess.ServerLevelStatisticalResult;
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

                if (statisticalresult != null && statisticalresult.ContainsKey(ServerInfo.IP))
                {
                    var tuple = statisticalresult[ServerInfo.IP];

                    Labels = tuple.Select(p => p.Item1.ToString("MM/dd")).ToArray();
                    SeriesCollection[2].Values.AddRange(tuple.Select(p => p.Item3 as object));
                    SeriesCollection[1].Values.AddRange(tuple.Select(p => p.Item4 as object));
                    SeriesCollection[0].Values.AddRange(tuple.Select(p => p.Item5 as object));

                    foreach (var serverkey in statisticalresult)
                    {
                        AlarmServerInfo serverinfo = new AlarmServerInfo();
                        serverinfo.Name = ServerInfoList.Where(p => p.IP == serverkey.Key).Select(p => p.Name).FirstOrDefault();
                        serverinfo.AlarmRate = serverkey.Value.Select(p => (p.Item2 == 0) ? 0 : (double)(p.Item3 + p.Item4) / p.Item2).Average();
                        if (serverkey.Value.Select(p => (p.Item2 == 0) ? 0 : (p.Item3)).Average() > 0)
                        {
                            serverinfo.AlarmGrade = 4;
                        }
                        else if (serverkey.Value.Select(p => (p.Item2 == 0) ? 0 : (p.Item4)).Average() > 0)
                        {
                            serverinfo.AlarmGrade = 3;
                        }
                        else if (serverkey.Value.Select(p => (p.Item2 == 0) ? 0 : (p.Item5)).Average() > 0)
                        {
                            serverinfo.AlarmGrade = 2;
                        }
                        else
                        {
                            serverinfo.AlarmGrade = 1;
                        }
                        serverInfoList.Add(serverinfo);
                    }
                    serverInfoList = serverInfoList.OrderByDescending(p => p.AlarmRate).ToList();
                    serverInfoList.ForEach(p => p.Index = serverInfoList.IndexOf(p) + 1);
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
                }
            }
            else if (para is string)//服务器
            {
                _loginUserService.SetGotoServerInfo(para as string);
                DeviceQucikDataView view = _loginUserService.GotoTab<DeviceQucikDataView>("MenuDeviceQucikData") as DeviceQucikDataView;
                if (view != null)
                {
                    view.GotoServer(_loginUserService.GotoServerInfo);
                }
            }
        }
    }
}

using AIC.Core;
using AIC.Core.Events;
using AIC.Core.Helpers;
using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.SignalModels;
using AIC.Core.UserManageModels;
using AIC.CoreType;
using AIC.DeviceDataPage.Views;
using AIC.DiagnosePage.Views;
using AIC.HistoryDataPage.Views;
using AIC.HomePage.Menus;
using AIC.HomePage.Models;
using AIC.HomePage.Views;
using AIC.NetWorksPage.Views;
using AIC.OnLineDataPage.Views;
using AIC.PDAPage.Views;
using AIC.QuickDataPage.Views;
using AIC.Resources.Models;
using AIC.Resources.Views;
using AIC.ServiceInterface;
using AIC.UserPage.Views;
using MahApps.Metro;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Media;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Wpf.CloseTabControl;

namespace AIC.HomePage.ViewModels
{
    class MainRegionViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILocalConfiguration _localConfiguration;
        private readonly ILoginUserService _loginUserService;
        private readonly IUserManageService _userManageService;
        private readonly ISignalProcess _signalProcess;

        private static Uri onLineMonitorView = new Uri("OnLineDataPageView", UriKind.Relative);
        private static Uri pdaManageView = new Uri("PDAManageView", UriKind.Relative);
        private static Uri loginView = new Uri("LoginView", UriKind.Relative);
        //private static Uri mapView = new Uri("HomeMapView", UriKind.Relative);
        private static Uri tabView = new Uri("TabView", UriKind.Relative);

        public MainRegionViewModel(ILocalConfiguration localConfiguration, IRegionManager regionManager, IEventAggregator eventAggregator, ILoginUserService loginUserService, IUserManageService userManageService, ISignalProcess signalProcess)
        {
            _localConfiguration = localConfiguration;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _loginUserService = loginUserService;
            _userManageService = userManageService;
            _signalProcess = signalProcess;

            ThemeManager.AddAppTheme("BaseGray", new Uri("pack://application:,,,/AIC.Resources;component/Styles/BaseGray.xaml"));
            ThemeManager.AddAccent("BlueGray", new Uri("pack://application:,,,/AIC.Resources;component/Styles/BlueGray.xaml"));

            Accent expectedAccent = ThemeManager.Accents.First(x => x.Name == "BlueGray");
            AppTheme expectedTheme = ThemeManager.GetAppTheme("BaseGray");
            ThemeManager.ChangeAppStyle(Application.Current, expectedAccent, expectedTheme);

            // create accent color menu items for the demo
            this.AccentColors = ThemeManager.Accents
                                            .Select(a => new AccentColorMenuData() { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush })
                                            .ToList();

            // create metro theme color menu items for the demo
            this.AppThemes = ThemeManager.AppThemes
                                           .Select(a => new AppThemeMenuData() { Name = a.Name, BorderColorBrush = a.Resources["BlackColorBrush"] as Brush, ColorBrush = a.Resources["WhiteColorBrush"] as Brush })
                                           .ToList();

            MenuManageList = _loginUserService.MenuManageList;
            ExceptionModel = _loginUserService.ExceptionModel;
            CustomSystemException = _loginUserService.CustomSystemException;

            readDataTimer.Tick += new EventHandler(timeCycle);
            readDataTimer.Interval = new TimeSpan(0, 0, 0, 1);
            readDataTimer.Start();

            WhenPropertyChanged.Where(o => o.ToString() == "Alarm").Subscribe(OnAlarmGradeChanged);
            WhenPropertyChanged.Where(o => o.ToString() == "SystemDate").Sample(TimeSpan.FromSeconds(10)).Subscribe(OnSystemDateChanged);

            _eventAggregator.GetEvent<LoginEvent>().Subscribe(LoginFinishEvent);
            _eventAggregator.GetEvent<CustomSystemEvent>().Subscribe(CustomSystemHappenEvent, ThreadOption.UIThread);//<!--昌邑石化-->

            WhenSlideChanged.Throttle(TimeSpan.FromMilliseconds(500)).ObserveOn(SynchronizationContext.Current).Subscribe(RaiseSlideChanged);

            InitLanguage();

            this.GCTimer.Interval = TimeSpan.FromMinutes(10); //垃圾释放定时器 我定为每十分钟释放一次，大家可根据需要修改
            this.GCTimer.Start();
            this.EventsRegistion();    // 注册事件

        }

        #region 字段和属性
        private ObservableCollection<ExceptionModel> ExceptionModel;

        private ObservableCollection<T1_SystemEvent> CustomSystemException;

        private MenuManageList menuManageList;
        public MenuManageList MenuManageList
        {
            get { return menuManageList; }
            set
            {
                menuManageList = value;
                OnPropertyChanged("MenuManageList");
            }
        }
        public List<AccentColorMenuData> AccentColors { get; set; }
        public List<AppThemeMenuData> AppThemes { get; set; }

        private string loginUser = "";
        public string LoginUser
        {
            get { return loginUser; }
            set
            {
                loginUser = value;
                OnPropertyChanged("LoginUser");
            }
        }

        private string nowTime;
        public string NowTime
        {
            get { return nowTime; }
            set
            {
                nowTime = value;
                OnPropertyChanged("NowTime");
            }
        }

        private bool pause;
        public bool Pause
        {
            get { return pause; }
            set
            {
                pause = value;
                OnPropertyChanged("Pause");
                _signalProcess.Suspend(value);
            }
        }

        private bool chineseChecked = true;
        public bool ChineseChecked
        {
            get { return chineseChecked; }
            set
            {
                if (chineseChecked != value)
                {
                    if (EnglishChecked == false && value == false)
                    {
                        return;
                    }
                    chineseChecked = value;
                    if (chineseChecked == true)
                    {
                        EnglishChecked = false;
                        SetAsChinese();                       
                    }
                    OnPropertyChanged("ChineseChecked");
                }
            }
        }
        private bool englishChecked;
        public bool EnglishChecked
        {
            get { return englishChecked; }
            set
            {
                if (englishChecked != value)
                {
                    if (ChineseChecked == false && value == false)
                    {
                        return;
                    }
                    englishChecked = value;
                    if (englishChecked == true)
                    {
                        ChineseChecked = false;
                        SetAsEnglish();                       
                    }
                    OnPropertyChanged("EnglishChecked");
                }     
            }
        }

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

        public string waitinfo = "用户登录中...";
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

        private Visibility loginVisibility = Visibility.Collapsed;
        public Visibility LoginVisibility
        {
            get
            {
                return loginVisibility;
            }
            set
            {
                loginVisibility = value;
                OnPropertyChanged("LoginVisibility");
            }
        }

        private int count;
        public int Count
        {
            get { return count; }
            set
            {
                if (count != value)
                {
                    count = value;
                    this.OnPropertyChanged(() => this.Count);
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
                    this.OnPropertyChanged(() => this.NormalCount);
                }
            }
        }
        private int preAlertCount;
        public int PreAlertCount
        {
            get { return preAlertCount; }
            set
            {
                if (preAlertCount != value)
                {
                    preAlertCount = value;
                    this.OnPropertyChanged(() => this.PreAlertCount);
                }
            }
        }
        private int alertCount;
        public int AlertCount
        {
            get { return alertCount; }
            set
            {
                if (alertCount != value)
                {
                    alertCount = value;
                    this.OnPropertyChanged(() => this.AlertCount);
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
                    this.OnPropertyChanged(() => this.DangerCount);
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
                    this.OnPropertyChanged(() => this.AbnormalCount);
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
                    this.OnPropertyChanged(() => this.UnConnectCount);
                }
            }
        }
        private bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                if (isConnected != value)
                {
                    isConnected = value;
                    this.OnPropertyChanged(() => this.IsConnected);
                }
            }
        }

        private AlarmGrade alarm;
        public AlarmGrade Alarm
        {
            get { return alarm; }
            set
            {
                if (alarm != value)
                {
                    alarm = value;
                    OnPropertyChanged(() => this.Alarm);
                }
            }
        }

        private ObservableCollection<BaseAlarmSignal> dangerList;
        public ObservableCollection<BaseAlarmSignal> DangerList
        {
            get { return dangerList; }
            set
            {
                if (dangerList != value)
                {
                    dangerList = value;
                    OnPropertyChanged("DangerList");
                }
            }
        }

        private ObservableCollection<BaseAlarmSignal> totalDangerList;
        public ObservableCollection<BaseAlarmSignal> TotalDangerList
        {
            get { return totalDangerList; }
            set
            {
                if (totalDangerList != value)
                {
                    totalDangerList = value;
                    OnPropertyChanged("TotalDangerList");
                }
            }
        }       

        private bool mute;
        public bool Mute
        {
            get { return mute; }
            set
            {
                mute = value;
                this.OnPropertyChanged(() => this.Mute);
                OnAlarmGradeChanged(null);
            }
        }

        private Visibility exceptListVisibility = Visibility.Collapsed;
        public Visibility ExceptListVisibility
        {
            get
            {
                return exceptListVisibility;
            }
            set
            {
                exceptListVisibility = value;
                OnPropertyChanged("ExceptListVisibility");
            }
        }

        public DateTime? historyplayTime = DateTime.Now;
        public DateTime? HistoryPlayTime
        {
            get
            {
                return historyplayTime;
            }
            set
            {
                historyplayTime = value;
                OnPropertyChanged("HistoryPlayTime");
            }
        }

        public float historyModeActualSpeedUpRatio = 1;
        public float HistoryModeActualSpeedUpRatio
        {
            get
            {
                return historyModeActualSpeedUpRatio;
            }
            set
            {
                historyModeActualSpeedUpRatio = value;
                OnPropertyChanged("HistoryModeActualSpeedUpRatio");
            }
        }

        private bool speedPause = false;
        public bool SpeedPause
        {
            get
            {
                return speedPause;
            }
            set
            {
                speedPause = value;
                OnPropertyChanged("SpeedPause");
            }
        }

        private float historyplayProcess = 0;
        public float HistoryPlayProcess
        {
            get
            {
                return historyplayProcess;
            }
            set
            {
                historyplayProcess = value;
                OnPropertyChanged("HistoryPlayProcess");
            }
        }

        private bool isShowHistoryplay = false;
        public bool IsShowHistoryPlay
        {
            get
            {
                return isShowHistoryplay;
            }
            set
            {
                isShowHistoryplay = value;
                OnPropertyChanged("IsShowHistoryPlay");
            }
        }

        private bool isHistoryRrackingMode = false;
        public bool IsHistoryRrackingMode
        {
            get
            {
                return isHistoryRrackingMode;
            }
            set
            {
                isHistoryRrackingMode = value;
                OnPropertyChanged("IsHistoryRrackingMode");
            }
        }

        private string historyModeName = "历史跟踪模式";
        public string HistoryModeName
        {
            get
            {
                return historyModeName;
            }
            set
            {
                historyModeName = value;
                OnPropertyChanged("HistoryModeName");
            }
        }

        private string systemDate;
        private string SystemDate
        {
            get
            {
                return systemDate;
            }
            set
            {
                if (systemDate != value)
                {
                    systemDate = value;
                    OnPropertyChanged("SystemDate");
                }
            }
        }
        public IObservable<string> WhenPropertyChanged
        {
            get
            {
                return Observable
                    .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                        h => this.PropertyChanged += h,
                        h => this.PropertyChanged -= h)
                    .Select(x => x.EventArgs.PropertyName);
            }
        }

        #endregion

        #region 命令
        private ICommand logoutCommand;
        public ICommand LogoutCommand
        {
            get
            {
                return this.logoutCommand ?? (this.logoutCommand = new DelegateCommand(() => this.Logout()));
            }
        }

        private ICommand navigateCommand;
        public ICommand NavigateCommand
        {
            get
            {
                return this.navigateCommand ?? (this.navigateCommand = new DelegateCommand<object>(para => this.Navigate(para)));
            }
        }

        private ICommand editPwdCommand;
        public ICommand EditPwdCommand
        {
            get
            {
                return this.editPwdCommand ?? (this.editPwdCommand = new DelegateCommand<object>(para => this.EditPwd()));
            }
        }        

        private ICommand lockCommand;
        public ICommand LockCommand
        {
            get
            {
                return this.lockCommand ?? (this.lockCommand = new DelegateCommand(() => this.Lock()));
            }
        }

        private ICommand unLockCommand;
        public ICommand UnLockCommand
        {
            get
            {
                return this.unLockCommand ?? (this.unLockCommand = new DelegateCommand<object>(para => this.UnLock(para)));
            }
        }

        private ICommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                return this.exitCommand ?? (this.exitCommand = new DelegateCommand(() => this.Exit()));
            }
        }

        private ICommand aboutCommand;
        public ICommand AboutCommand
        {
            get
            {
                return this.aboutCommand ?? (this.aboutCommand = new DelegateCommand(() => this.About()));
            }
        }

        private ICommand aboutVersionCommmad;
        public ICommand AboutVersionCommmad
        {
            get
            {
                return this.aboutVersionCommmad ?? (this.aboutVersionCommmad = new DelegateCommand(() => this.AboutVersion()));
            }
        }

        private ICommand systemLogCommand;
        public ICommand SystemLogCommand
        {
            get
            {
                return this.systemLogCommand ?? (this.systemLogCommand = new DelegateCommand(() => this.SystemLog()));
            }
        }

        private ICommand screenShotFolderCommand;
        public ICommand ScreenShotFolderCommand
        {
            get
            {
                return this.screenShotFolderCommand ?? (this.screenShotFolderCommand = new DelegateCommand(() => this.ScreenShotFolder()));
            }
        }

        private ICommand abnormalListCommand;
        public ICommand AbnormalListCommand
        {
            get
            {
                return this.abnormalListCommand ?? (this.abnormalListCommand = new DelegateCommand(() => this.AbnormalList()));
            }
        }

        private ICommand feedbackCommand;
        public ICommand FeedbackCommand
        {
            get
            {
                return this.feedbackCommand ?? (this.feedbackCommand = new DelegateCommand(() => this.Feedback()));
            }
        }

        private ICommand alarmListCommmad;
        public ICommand AlarmListCommmad
        {
            get
            {
                return this.alarmListCommmad ?? (this.alarmListCommmad = new DelegateCommand(() => this.AlarmList()));
            }
        }

        private ICommand exceptListCommmad;
        public ICommand ExceptListCommmad
        {
            get
            {
                return this.exceptListCommmad ?? (this.exceptListCommmad = new DelegateCommand(() => this.ExceptList()));
            }
        }

        private ICommand settingCommmad;
        public ICommand SettingCommmad
        {
            get
            {
                return this.settingCommmad ?? (this.settingCommmad = new DelegateCommand(() => this.Setting()));
            }
        }

        private ICommand historyQuickPlaySettingCommmad;
        public ICommand HistoryQuickPlaySettingCommmad
        {
            get
            {
                return this.historyQuickPlaySettingCommmad ?? (this.historyQuickPlaySettingCommmad = new DelegateCommand(() => this.HistoryQuickPlaySetting()));
            }
        }
        

        private ICommand dangerListClickCommmad;
        public ICommand DangerListClickCommmad
        {
            get
            {
                return this.dangerListClickCommmad ?? (this.dangerListClickCommmad = new DelegateCommand<object>(para => this.DangerListClick(para)));
            }
        }
        private ICommand dangerListDetailCommmad;
        public ICommand DangerListDetailCommmad
        {
            get
            {
                return this.dangerListDetailCommmad ?? (this.dangerListDetailCommmad = new DelegateCommand(() => this.DangerListDetail()));
            }
        }

        private ICommand decreaseSpeedCommand;
        public ICommand DecreaseSpeedCommand
        {
            get
            {
                return this.decreaseSpeedCommand ?? (this.decreaseSpeedCommand = new DelegateCommand(() => this.DecreaseSpeed()));
            }
        }

        private ICommand increaseSpeedCommand;
        public ICommand IncreaseSpeedCommand
        {
            get
            {
                return this.increaseSpeedCommand ?? (this.increaseSpeedCommand = new DelegateCommand(() => this.IncreaseSpeed()));
            }
        }

        private ICommand pauseSpeedCommand;
        public ICommand PauseSpeedCommand
        {
            get
            {
                return this.pauseSpeedCommand ?? (this.pauseSpeedCommand = new DelegateCommand(() => this.PauseSpeed()));
            }
        }

        private ICommand histotyProcessChangedCommand;
        public ICommand HistotyProcessChangedCommand
        {
            get
            {
                return this.histotyProcessChangedCommand ?? (this.histotyProcessChangedCommand = new DelegateCommand<object>(para => this.HistotyProcessChanged(para)));
            }
        }

        private ICommand closeWindowsCommand;
        public ICommand CloseWindowsCommand
        {
            get
            {
                return this.closeWindowsCommand ?? (this.closeWindowsCommand = new DelegateCommand(() => this.CloseWindows()));
            }
        }

        private event EventHandler<EventArgs> trackChanged;
        public IObservable<EventArgs> WhenSlideChanged
        {
            get
            {
                return Observable
                    .FromEventPattern<EventArgs>(
                        h => this.trackChanged += h,
                        h => this.trackChanged -= h)
                   .Select(x => x.EventArgs);
            }
        }
        #endregion

        #region 登录      

        private void LoginFinishEvent(LoginInfo loginInfo)//登录消息
        {
            OpenFirstTab();

            //_eventAggregator.GetEvent<ServerMarkEvent>().Publish(_localConfiguration.ServerInfoList);

            LoginUser = String.Format((string)Application.Current.Resources["strUser"]
                + ": {0}  IP: {1}  "
                + (string)Application.Current.Resources["strRole"]
                + "：{2}", loginInfo.UserName, loginInfo.ServerInfo.IP, loginInfo.ServerInfo.Permission);

            LoginVisibility = Visibility.Visible;
        }

        private void Logout()
        {
#if XBAP
            MessageBoxResult result = MessageBox.Show((string)Application.Current.Resources["strLogoutSystem"], (string)Application.Current.Resources["strLogout"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#else
            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["strLogoutSystem"], (string)Application.Current.Resources["strLogout"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#endif
            if (result == MessageBoxResult.OK)
            {
                _regionManager.RequestNavigate(RegionNames.MainBodyRegion, loginView);

                _loginUserService.SetUserLogout();

                LoginUser = null;

                _loginUserService.CloseTabs();

                LoginVisibility = Visibility.Collapsed;
            }
        }

        private void OpenFirstTab()
        {
            _regionManager.RequestNavigate(RegionNames.MainBodyRegion, tabView);
            ////首页默认打开
            _loginUserService.GotoTab<HomeMapView>("首页");
        }      

        private void EditPwd()
        {
            if (PwdEditManage.Enter(_loginUserService.LoginInfo))
            {
#if XBAP
                MessageBox.Show("密码修改成功!", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("密码修改成功!", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
#endif
            }
        }
        private void Lock()
        {
            //如果没有登录
            if (_loginUserService.GetUserLoginStatus() == false)
            {
                return;
            }
            if (LockManage.Lock(_loginUserService.LoginInfo))
            {
                _loginUserService.LockTabs();
            }
        }

        private void UnLock(object para)
        {
            string name = para as string;

            //如果没有登录
            if (_loginUserService.GetUserLoginStatus() == false)
            {
                return;
            }
            if (LockManage.UnLock(_loginUserService.LoginInfo))
            {
                _loginUserService.UnLockTabs(name);
            }
        }

        private void Exit()
        {
#if XBAP
            MessageBoxResult result = MessageBox.Show((string)Application.Current.Resources["strExitSystem"], (string)Application.Current.Resources["strExit"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#else
            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["strExitSystem"], (string)Application.Current.Resources["strExit"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#endif
            if (result == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }
        #endregion

        #region 菜单管理
        private async void Navigate(object para)
        {
            string viewName = para as string;
            if (viewName == null)
            {
                return;
            }         

            if (viewName == "MenuServerSetting")
            {
                _loginUserService.GotoTab<ServerSetView>(viewName);
            }
            else if (viewName == "MenuCollectorSetting")
            {
                _loginUserService.GotoTab<PDASystemManageView>(viewName);
            }
            else if (viewName == "MenuUserManage")
            {
                _loginUserService.GotoTab<UserSetView>(viewName);
            }
            else if (viewName == "MenuRoleManage")
            {
                _loginUserService.GotoTab<RoleSetView>(viewName);
            }
            else if (viewName == "MenuMenuManage")
            {
                _loginUserService.GotoTab<MenuSetView>(viewName);
            }
            else if (viewName == "MenuOrganizationManage")
            {
                _loginUserService.GotoTab<OrganizationPrivilegeSetView>(viewName);
            }
            else if (viewName == "MenuManageLog")
            {
                _loginUserService.GotoTab<OperateRecord>(viewName);
            }
            else if (viewName == "MenuOnlineDataOverview")
            {
                _loginUserService.GotoTab<OnlineDataOverviewView>(viewName);
            }
            else if (viewName == "MenuOnlineDataList")
            {
                _loginUserService.GotoTab<OnlineDataListView>(viewName);
            }
            else if (viewName == "MenuOnlineDataTile")
            {
                _loginUserService.GotoTab<OnlineDataTileView>(viewName);
            }
            else if (viewName == "MenuOnlineDataDiagram")
            {
                _loginUserService.GotoTab<OnlineDataDiagramView>(viewName);
            }
            else if (viewName == "MenuHistoryDataList")
            {
                _loginUserService.GotoTab<HistoryDataListView>(viewName);
            }
            else if (viewName == "MenuHistoryDataDiagram")
            {
                _loginUserService.GotoTab<HistoryDataDiagramView>(viewName);
            }
            else if (viewName == "MenuHistoryData")
            {
                _loginUserService.GotoTab<HistoryDataDiagramView>(viewName);
            }
            else if (viewName == "MenuOnlineDataDiagnosis")
            {
                _loginUserService.GotoTab<OnlineDataDiagnosisView>(viewName);
            }
            else if (viewName == "MenuOnlineDataStatistics")
            {
                _loginUserService.GotoTab<OnlineDataStatisticsView>(viewName);
            }
            else if (viewName == "MenuHistoryDataStatistics")
            {
                _loginUserService.GotoTab<HistoryDataStatisticsView>(viewName);
            }
            else if (viewName == "MenuSystemEventList")
            {
                _loginUserService.GotoTab<HistoryEventListView>(viewName);
            }
            else if (viewName == "MenuDataTrendChart")
            {
                _loginUserService.GotoTab<HistoryDataTrendView>(viewName);
            }
            else if (viewName == "MenuDeviceRunTime")
            {
                _loginUserService.GotoTab<DeviceRunTimeListView>(viewName);
            }
            else if (viewName == "MenuDeviceRunAnalyze")
            {
                _loginUserService.GotoTab<DeviceRunAnalyzeListView>(viewName);
            }
            else if (viewName == "MenuDeviceHourlyData")
            {
                _loginUserService.GotoTab<DeviceHourlyDataView>(viewName);
            }
            else if (viewName == "MenuExportDBData")
            {
                _loginUserService.GotoTab<ExportDBDataView>(viewName);
            }
            else if (viewName == "MenuImportDBData")
            {
                _loginUserService.GotoTab<ImportDBDataView>(viewName);
            }
            else if (viewName == "MenuFilterDBData")
            {
                _loginUserService.GotoTab<FilterDBDataView>(viewName);
            }
            else if (viewName == "MenuServerQucikData")
            {
                _loginUserService.GotoTab<ServerQucikDataView>(viewName);
            }
            else if (viewName == "MenuDeviceQucikData")
            {
                _loginUserService.GotoTab<DeviceQucikDataView>(viewName);
            }
            else if (viewName == "MenuItemQucikData")
            {
                _loginUserService.GotoTab<ItemQucikDataView>(viewName);
            }
            else if (viewName == "MenuBPNetWorks")
            {
                _loginUserService.GotoTab<BPNetWorksView>(viewName);
            }
            else if (viewName == "MenuSOMNetWorks")
            {
                _loginUserService.GotoTab<SOMNetWorksView>(viewName);
            }
            else if (viewName == "MenuDeviceFaultDiagnose")
            {
                _loginUserService.GotoTab<DeviceFaultDiagnoseView>(viewName);
            }
            else if (viewName == "MenuRefreshData")
            {
                Status = ViewModelStatus.Querying;
                WaitInfo = "刷新中";
                _loginUserService.CloseTabs(false);
                MenuManageList.Dictionary.Values.ToList().ForEach(p => p.Visibility = Visibility.Collapsed);
                await _loginUserService.SetUserLogin();
                Status = ViewModelStatus.None;
                PopupWindow win = new PopupWindow("提示", "刷新完成！！！");
                win.Show();
                await _loginUserService.LazyLoading();
                return;
            }
            else if (viewName == "test")
            {
                TestWin win = new TestWin();
                win.Show();
                return;
            }            
        }
        #endregion

        #region 中英文管理
        private void InitLanguage()
        {
            if (LocalSetting.IsEnglishLanguage == true)
            {
                EnglishChecked = true;
            }
        }

        private void SetAsChinese()
        {
            List<ResourceDictionary> dictionaryList = new List<ResourceDictionary>();
            foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
            {
                dictionaryList.Add(dictionary);
            }
            string requestedCulture = @"/AIC.Resources;component/Themes/zh-cn.xaml";
            ResourceDictionary resourceDictionary = dictionaryList.Where(d => d.Source != null && d.Source.OriginalString.Equals(requestedCulture)).FirstOrDefault();
            Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            TabLanguageShift();
        }
        private void SetAsEnglish()
        {
            List<ResourceDictionary> dictionaryList = new List<ResourceDictionary>();
            foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
            {
                dictionaryList.Add(dictionary);
            }
            string requestedCulture = @"/AIC.Resources;component/Themes/en-us.xaml";
            ResourceDictionary resourceDictionary = dictionaryList.Where(d => d.Source != null && d.Source.OriginalString.Equals(requestedCulture)).FirstOrDefault();
            Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            TabLanguageShift();
        }
        private void TabLanguageShift()
        {
            _loginUserService.TabLanguageShift();
            try
            {
                LocalSetting.IsEnglishLanguage = EnglishChecked;
                LocalSetting.SetAppSetting("IsEnglishLanguage", EnglishChecked.ToString());    
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("设置错误", ex));
            }
        }
        #endregion

        #region 其它
        private void About()
        {
            AboutWin win = new AboutWin();
            win.ShowDialog();
        }

        private void SystemLog()
        {
            //string dir = System.Environment.CurrentDirectory + "\\Log";
            // string dir = System.Windows.Forms.Application.StartupPath + "\\Log";
            string dir = System.AppDomain.CurrentDomain.BaseDirectory + "Log";
            System.Diagnostics.Process.Start("explorer.exe", Path.GetFullPath(dir));
        }

        private void ScreenShotFolder()
        {
            //string dir = @LocalSetting.ScreenShotDir;
            string dir = System.AppDomain.CurrentDomain.BaseDirectory + "ScreenShot";
            System.Diagnostics.Process.Start("explorer.exe", Path.GetFullPath(dir));
        }

        private void Feedback()
        {
            SendEmailWin win = new SendEmailWin();
            win.ShowDialog();
        }

        private void AbnormalList()
        {
            ShowAbnormalWin win = new ShowAbnormalWin(ExceptionModel);
            win.ShowDialog();
        }

        private void AlarmList()
        {
            //ShowAlarmWin win = new ShowAlarmWin();
            //win.ShowDialog();
        }

        private void ExceptList()
        {
            ShowAbnormalWin win = new ShowAbnormalWin(ExceptionModel);
            win.ShowDialog();
        }

        private void Setting()
        {
            DefaultSettingWin win = new DefaultSettingWin();
            win.ShowDialog();
        }

        private void HistoryQuickPlaySetting()
        {
            HistoryQuickPlayWin win = new HistoryQuickPlayWin();
            win.ShowDialog();
        }

        private void AboutVersion()
        {
            VersionWin win = new VersionWin();
            win.ShowDialog();
        }

        private void CloseWindows()
        {
            _loginUserService.CloseTabs(false);
        }
        #endregion

        #region 数据
        private System.Windows.Threading.DispatcherTimer readDataTimer = new System.Windows.Threading.DispatcherTimer();

        private float historyplayCount = LocalSetting.HistoryModeDBCallInterval;
        private readonly SemaphoreSlim locker = new SemaphoreSlim(1);

        private async void timeCycle(object sender, EventArgs e)
        {
            NowTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            IsShowHistoryPlay = LocalSetting.IsHistoryMode;
            IsHistoryRrackingMode = LocalSetting.IsHistoryRrackingMode;

            if (_loginUserService.LoginInfo.LoginStatus == true)
            {
                await locker.WaitAsync();
                try
                {
                    if (LocalSetting.IsHistoryMode == true)
                    {                      
                        historyplayCount++;
                        if (historyplayCount >= LocalSetting.HistoryModeDBCallInterval)
                        {
                            historyplayCount = 0;

                            if (LocalSetting.IsHistoryRrackingMode == true)
                            {
                                HistoryModeName = "历史跟踪模式";
                                HistoryPlayTime = DateTime.Now;
                                HistoryModeActualSpeedUpRatio = 1;
                            }
                            else
                            {
                                HistoryModeName = "历史快放模式";
                                if (HistoryPlayTime == null)
                                {
                                    HistoryPlayTime = LocalSetting.HistoryModeStartTime;
                                }
                                if (SpeedPause == false)
                                {
                                    if (HistoryPlayTime >= LocalSetting.HistoryModeEndTime)//循环播放
                                    {
                                        HistoryPlayTime = LocalSetting.HistoryModeStartTime;
                                    }

                                    HistoryModeActualSpeedUpRatio = LocalSetting.HistoryModeSpeedUpRatio * LocalSetting.HistoryModeDBCallInterval;
                                    HistoryPlayTime = HistoryPlayTime.Value.AddSeconds(HistoryModeActualSpeedUpRatio);
                                    HistoryPlayProcess = (HistoryPlayTime.Value - LocalSetting.HistoryModeStartTime).Ticks * 100 / (LocalSetting.HistoryModeEndTime - LocalSetting.HistoryModeStartTime).Ticks;
                                }
                            }
                            IsConnected = await _signalProcess.GetSignalData(HistoryPlayTime.Value, true);
                        }
                        SystemDate = (HistoryPlayTime ?? LocalSetting.HistoryModeStartTime).ToString("yyyy-MM-dd");//用于刷新统计
                    }
                    else
                    {
                        HistoryPlayTime = LocalSetting.HistoryModeStartTime;
                        IsConnected = await _signalProcess.GetSignalData(DateTime.Now, false);
                        SystemDate = DateTime.Now.ToString("yyyy-MM-dd");//用于刷新统计
                    }
                    if (IsConnected == true)
                    {
                        _eventAggregator.GetEvent<SignalBroadcastingEvent>().Publish(null);
                    }
                    SetStatus();
                }
                finally
                {
                    locker.Release();
                }
            }
            else
            {
                InitStatus();
            }

            if (ExceptionModel.Count > 0)
            {
                ExceptListVisibility = Visibility.Visible;
            }
            else
            {
                ExceptListVisibility = Visibility.Collapsed;
            }

          
        }

        private void InitStatus()
        {
            Count = 0;
            NormalCount = 0;
            PreAlertCount = 0;
            AlertCount = 0;
            DangerCount = 0;
            AbnormalCount = 0;
            UnConnectCount = 0;
            Alarm = AlarmGrade.HighNormal;
            IsConnected = false;
            DangerList = new ObservableCollection<BaseAlarmSignal>();
            HistoryPlayTime = LocalSetting.HistoryModeStartTime;
        }

        private void SetStatus()
        {
            var signals = _signalProcess.Signals.OfType<BaseAlarmSignal>();
            Count = signals.Count();
            NormalCount = signals.Where(o => (o.DelayAlarmGrade == AlarmGrade.HighNormal || o.DelayAlarmGrade == AlarmGrade.LowNormal)).Count();
            PreAlertCount = signals.Where(o =>  (o.DelayAlarmGrade == AlarmGrade.HighPreAlarm || o.DelayAlarmGrade == AlarmGrade.LowPreAlarm)).Count();
            AlertCount = signals.Where(o => (o.DelayAlarmGrade == AlarmGrade.HighAlarm || o.DelayAlarmGrade == AlarmGrade.LowAlarm)).Count();
            DangerCount = signals.Where(o => (o.DelayAlarmGrade == AlarmGrade.HighDanger || o.DelayAlarmGrade == AlarmGrade.LowDanger)).Count();
            AbnormalCount = signals.Where(o =>  (o.DelayAlarmGrade == AlarmGrade.Abnormal)).Count();
            UnConnectCount = signals.Where(o =>  (o.DelayAlarmGrade == AlarmGrade.DisConnect)).Count();
            if (DangerCount > 0)
            {
                Alarm = AlarmGrade.HighDanger;
            }
            else if (AlertCount > 0)
            {
                Alarm = AlarmGrade.HighAlarm;
            }
            else if (PreAlertCount > 0)
            {
                Alarm = AlarmGrade.HighPreAlarm;
            }
            else
            {
                Alarm = AlarmGrade.HighNormal;
            }
            DangerList = new ObservableCollection<BaseAlarmSignal>(signals.Where(o => o.AlarmAck == false && o.IsConnected == true && (o.DelayAlarmGrade == AlarmGrade.HighDanger || o.DelayAlarmGrade == AlarmGrade.LowDanger)).Take(3));
            TotalDangerList = new ObservableCollection<BaseAlarmSignal>(signals.Where(o => o.IsConnected == true && (o.DelayAlarmGrade == AlarmGrade.HighDanger || o.DelayAlarmGrade == AlarmGrade.LowDanger)));
        }

        private void DecreaseSpeed()
        {            
            HistoryPlayTime = HistoryPlayTime.Value.AddSeconds(0 - HistoryModeActualSpeedUpRatio);            
            HistoryPlayProcess = (HistoryPlayTime.Value - LocalSetting.HistoryModeStartTime).Ticks * 100 / (LocalSetting.HistoryModeEndTime - LocalSetting.HistoryModeStartTime).Ticks;
            if (SpeedPause == false)
            {
                HistoryPlayTime = HistoryPlayTime.Value.AddSeconds(0 - HistoryModeActualSpeedUpRatio);
            }
            if (trackChanged != null)
            {
                trackChanged(this, null);
            }
        }

        private void IncreaseSpeed()
        {
            HistoryPlayTime = HistoryPlayTime.Value.AddSeconds(HistoryModeActualSpeedUpRatio);
            HistoryPlayProcess = (HistoryPlayTime.Value - LocalSetting.HistoryModeStartTime).Ticks * 100 / (LocalSetting.HistoryModeEndTime - LocalSetting.HistoryModeStartTime).Ticks;
            if (SpeedPause == false)
            {
                HistoryPlayTime.Value.AddSeconds(0 - HistoryModeActualSpeedUpRatio);
            }
            if (trackChanged != null)
            {
                trackChanged(this, null);
            }
        }

        private void PauseSpeed()
        {
            SpeedPause = !SpeedPause;
        }

        private void HistotyProcessChanged(object para)
        {
            if (para is double)
            {
                var value = (double)para;
                if (SpeedPause == false)
                {
                    HistoryPlayTime = LocalSetting.HistoryModeStartTime + new TimeSpan((long)(value * (LocalSetting.HistoryModeEndTime - LocalSetting.HistoryModeStartTime).Ticks / 100));
                    HistoryPlayTime = HistoryPlayTime.Value.AddSeconds(0 - HistoryModeActualSpeedUpRatio);
                }
                else
                {
                    HistoryPlayTime = LocalSetting.HistoryModeStartTime + new TimeSpan((long)(value * (LocalSetting.HistoryModeEndTime - LocalSetting.HistoryModeStartTime).Ticks / 100));
                }
                if (trackChanged != null)
                {
                    trackChanged(this, null);
                }
            }
        }

        private void RaiseSlideChanged(EventArgs args)
        {           
            historyplayCount = LocalSetting.HistoryModeDBCallInterval;
        }

        private void DangerListClick(object para)
        {
            BaseAlarmSignal sg = para as BaseAlarmSignal;
            AlarmAckWin win = new Views.AlarmAckWin(sg);
            win.Show();
        }

        private void DangerListDetail()
        {
            AlarmAckListWin win = new AlarmAckListWin(TotalDangerList);
            win.Show();
        }

        private SoundPlayer player = new System.Media.SoundPlayer();
        private void OnAlarmGradeChanged(string propertyName)//报警改变
        {
            if (Mute == true)//静音
            {
                player.Stop();
                return;
            }
            if (Alarm == AlarmGrade.HighDanger || Alarm == AlarmGrade.LowDanger)
            {
                player.SoundLocation = System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Danger.wav";
                player.PlayLooping();
            }
            else if (Alarm == AlarmGrade.HighAlarm || Alarm == AlarmGrade.LowAlarm)
            {
                player.SoundLocation = System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Waring.wav";
                player.PlayLooping();
            }
            else
            {
                player.Stop();
            }
        }
        #endregion

        #region 统计数据
        private void OnSystemDateChanged(string propertyName)//刷新统计数据
        {
            if (_loginUserService.LoginInfo.LoginStatus == true)
            {
                try
                {
                    var date = Convert.ToDateTime(SystemDate);
                    _signalProcess.GetDailyMedianData(date.AddDays(-7), date);
                    _signalProcess.GetRunningDays(date);
                }
                catch { }
            }
        }
        #endregion

        #region 事件

        private List<double> heightoffsets = new List<double>();
        private const double constantheightoffset = 60;
        private object threadLock = new object();

        private void CustomSystemHappenEvent(Tuple<string, T1_SystemEvent> ex)
        {
            if (ex != null)
            {
                ex.Item2.id = (CustomSystemException.LastOrDefault() ?? new T1_SystemEvent()).id + 1;
                CustomSystemException.Add(ex.Item2);

                //保留当天100条数据
                if (CustomSystemException.Count >= 100)
                {
                    CustomSystemException.RemoveAt(0);
                }

                var olds = CustomSystemException.Where(p => p.EventTime.Date != ex.Item2.EventTime.Date).ToList();
                if (olds != null)
                {
                    for (int i = 0; i < olds.Count; i++)
                    {
                        CustomSystemException.Remove(olds[i]);
                    }
                }
                _loginUserService.AddSystemEvent(ex.Item1, ex.Item2);
            }
            //if (heightoffsets.Count >= 5)//避免太多弹出窗口//htzk123，弹出窗口太多，废弃
            //{
            //    return;
            //}

            //lock (threadLock)
            //{
            //    if (Mute == false)//静音
            //    {
            //        playsound();
            //    }
            //    string title = GetEnumDescription.GetDescription((CustomSystemType)ex.Type) + "事件发生";
            //    string content = ex.EventTime.ToString("yyyy-MM-dd HH:mm:ss") + "   " + ex.Remarks;
            //    double heightoffset = 0;
            //    while (true)
            //    {
            //        if (heightoffsets.Contains(heightoffset))
            //        {
            //            heightoffset += constantheightoffset;
            //        }
            //        else
            //        {
            //            heightoffsets.Add(heightoffset);
            //            break;
            //        }
            //    }

            //    TaskbarNotifier taskbarnotifier = new TaskbarNotifier(title, content, heightoffset);
            //    taskbarnotifier.ThisClosed += Taskbarnotifier_ThisClosed;
            //    taskbarnotifier.Show();
            //}
        }

        private void Taskbarnotifier_ThisClosed(double heightoffset)
        {
            heightoffsets.Remove(heightoffset);
        }

        private void playsound()
        {
            string soundName = System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\msg.wav";
            PlaySound.Play(soundName);
        }
        #endregion

        #region 清理垃圾
        System.Windows.Threading.DispatcherTimer GCTimer = new System.Windows.Threading.DispatcherTimer();

        public void EventsRegistion()
        {
            this.GCTimer.Tick += new EventHandler(OnGarbageCollection);
        }

        public void EventDeregistration()
        {
            this.GCTimer.Tick -= new EventHandler(OnGarbageCollection);
        }

        void OnGarbageCollection(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        #endregion
    }
}

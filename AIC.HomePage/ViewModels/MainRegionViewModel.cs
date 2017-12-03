using AIC.Core;
using AIC.Core.Events;
using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.Core.SignalModels;
using AIC.Core.UserManageModels;
using AIC.CoreType;
using AIC.DeviceDataPage.Views;
using AIC.HistoryDataPage.Views;
using AIC.HomePage.Menus;
using AIC.HomePage.Models;
using AIC.HomePage.Views;
using AIC.OnLineDataPage.Views;
using AIC.PDAPage.Views;
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
        private static Uri mapView = new Uri("MapView", UriKind.Relative);

        public MainRegionViewModel(ILocalConfiguration localConfiguration, IRegionManager regionManager, IEventAggregator eventAggregator, ILoginUserService loginUserService, IUserManageService userManageService, ISignalProcess signalProcess)
        {
            _localConfiguration = localConfiguration;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _loginUserService = loginUserService;
            _userManageService = userManageService;
            _signalProcess = signalProcess;

            ThemeManager.AddAppTheme("BaseGray", new Uri("pack://application:,,,/AIC.Resources;component/Styles/BaseGray.xaml"));

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

            _eventAggregator.GetEvent<LoginEvent>().Subscribe(LoginFinishEvent);
            _eventAggregator.GetEvent<CustomSystemEvent>().Subscribe(CustomSystemHappenEvent, ThreadOption.UIThread);//<!--昌邑石化-->

            WhenSlideChanged.Throttle(TimeSpan.FromMilliseconds(500)).ObserveOn(SynchronizationContext.Current).Subscribe(RaiseSlideChanged);

            LoginManage.LoginChanged += LoginManage_LoginChanged;
        }
     

        #region 字段和属性
        private ObservableCollection<ExceptionModel> ExceptionModel;

        private ObservableCollection<CustomSystemException> CustomSystemException;

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
                    chineseChecked = value;
                    if (chineseChecked == true)
                    {
                        SetAsChinese();
                        EnglishChecked = false;
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
                    englishChecked = value;
                    if (englishChecked == true)
                    {
                        SetAsEnglish();
                        ChineseChecked = false;
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

        private Visibility login1Visibility = Visibility.Collapsed;
        public Visibility Login1Visibility
        {
            get
            {
                return login1Visibility;
            }
            set
            {
                login1Visibility = value;
                OnPropertyChanged("Login1Visibility");
            }
        }

        private Visibility login2Visibility = Visibility.Collapsed;
        public Visibility Login2Visibility
        {
            get
            {
                return login2Visibility;
            }
            set
            {
                login2Visibility = value;
                OnPropertyChanged("Login2Visibility");
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
        public DateTime? HistoryplayTime
        {
            get
            {
                return historyplayTime;
            }
            set
            {
                historyplayTime = value;
                OnPropertyChanged("HistoryplayTime");
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
        public float HistoryplayProcess
        {
            get
            {
                return historyplayProcess;
            }
            set
            {
                historyplayProcess = value;
                OnPropertyChanged("HistoryplayProcess");
            }
        }

        private bool isShowHistoryplay = false;
        public bool IsShowHistoryplay
        {
            get
            {
                return isShowHistoryplay;
            }
            set
            {
                isShowHistoryplay = value;
                OnPropertyChanged("IsShowHistoryplay");
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
        private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return this.loginCommand ?? (this.loginCommand = new DelegateCommand(() => this.Login()));
            }
        }
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
            _regionManager.RequestNavigate(RegionNames.HomeViewMainRegion, mapView);

            _eventAggregator.GetEvent<ServerMarkEvent>().Publish(_localConfiguration.ServerInfoList);

            LoginUser = String.Format((string)Application.Current.Resources["strUser"]
                + ": {0}  IP: {1}  "
                + (string)Application.Current.Resources["strRole"]
                + "：{2}", loginInfo.UserName, loginInfo.ServerInfo.IP, loginInfo.ServerInfo.Permission);

            LoginVisibility = Visibility.Visible;
            if (MenuManageList.MenuServerSetting.Visibility == Visibility.Visible || MenuManageList.MenuCollectorSetting.Visibility == Visibility.Visible || MenuManageList.MenuEquipmentSetting.Visibility == Visibility.Visible)
            {
                Login1Visibility = Visibility.Visible;
            }
            if (MenuManageList.MenuOnlineDataDiagram.Visibility == Visibility.Visible || MenuManageList.MenuHistoricalData.Visibility == Visibility.Visible || MenuManageList.MenuAlarmData.Visibility == Visibility.Visible)
            {
                Login2Visibility = Visibility.Visible;
            }
        }

        private void Login()
        {
            //如果没有登录
            if (_loginUserService.GetUserLoginStatus() == false)
            {               
                LoginManage.Enter(_loginUserService.DefaultLoginServer());               
            }
            else
            {
#if XBAP
                MessageBox.Show("请退出后再登陆！！！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请退出后再登陆！！！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
            }
        }

        private async void LoginManage_LoginChanged(bool result)
        {
            try
            {
                if (result)
                {
                    Status = ViewModelStatus.Querying;
                    //把登录配置保存到本地
                    _localConfiguration.WriteServerInfo(_localConfiguration.ServerInfoList);
                    await _loginUserService.SetUserLogin();
                    _eventAggregator.GetEvent<LoginEvent>().Publish(_loginUserService.LoginInfo);
                    Status = ViewModelStatus.None;
                    //加载后续数据   
                    await _loginUserService.LazyLoading();
                }
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("登录异常", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        private void Logout()
        {
            _regionManager.RequestNavigate(RegionNames.HomeViewMainRegion, loginView);

            _loginUserService.SetUserLogout();

            LoginUser = null;

            CloseTabs();

            LoginVisibility = Visibility.Collapsed;
            Login1Visibility = Visibility.Collapsed;
            Login2Visibility = Visibility.Collapsed;
        }

        private void CloseTabs()
        {
            //关闭除主页外其他视图
            IRegion region = this._regionManager.Regions["MainTabRegion"];
            var views = region.Views.ToList();
            for (int i = views.Count - 1; i >= 0; i--)
            {
                var viewObj = views[i];
                ICloseable view = viewObj as ICloseable;
                if (view.Closer.Visibility == Visibility.Visible)
                {
                    region.Remove(view);
                }
            }
        }

        private void EditPwd()
        {
            if (PwdEditManage.Enter(_loginUserService.LoginInfo))
            {
                IRegion region = this._regionManager.Regions["MainTabRegion"];
                var views = region.Views.ToList();
                for (int i = 0; i < views.Count; i++)
                {
                    var viewObj = views[i];
                    ICloseable view = viewObj as ICloseable;
                    if (view.Closer.Visibility == Visibility.Visible)
                    {
                        view.Closer.LockVisibility = Visibility.Visible;
                    }
                }
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
                IRegion region = this._regionManager.Regions["MainTabRegion"];
                var views = region.Views.ToList();
                for (int i = 0; i < views.Count; i++)
                {
                    var viewObj = views[i];
                    ICloseable view = viewObj as ICloseable;
                    if (view.Closer.Visibility == Visibility.Visible)
                    {
                        view.Closer.LockVisibility = Visibility.Visible;
                    }
                }
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
                IRegion region = this._regionManager.Regions["MainTabRegion"];
                if (name == null)
                {
                    var views = region.Views.ToList();
                    for (int i = 0; i < views.Count; i++)
                    {
                        var viewObj = views[i];
                        ICloseable view = viewObj as ICloseable;
                        if (view.Closer.Visibility == Visibility.Visible)
                        {
                            view.Closer.LockVisibility = Visibility.Collapsed;
                        }
                    }
                }
                else
                {
                    var views = region.Views.ToList();
                    for (int i = 0; i < views.Count; i++)
                    {
                        var viewObj = views[i];
                        ICloseable view = viewObj as ICloseable;
                        if (view.Closer.Visibility == Visibility.Visible && view.Closer.Title == name)
                        {
                            view.Closer.LockVisibility = Visibility.Collapsed;
                            break;
                        }
                    }
                }
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
            if (!this._regionManager.Regions.ContainsRegionWithName("MainTabRegion"))
            {
                return;
            }
            IRegion region = this._regionManager.Regions["MainTabRegion"];
            if (region.GetView(viewName) != null)
            {
                region.Activate(region.GetView(viewName));
                return;
            }
            object viewObj = null;
            if (viewName == "MenuServerSetting")
            {
                viewObj = ServiceLocator.Current.GetInstance<ServerSetView>();
            }
            //else if (viewName == "MenuOnlineData")
            //{
            //    viewObj = ServiceLocator.Current.GetInstance<OnLineDataPageView>();
            //}
            else if (viewName == "MenuCollectorSetting")
            {
                viewObj = ServiceLocator.Current.GetInstance<PDASystemManageView>();
            }
            else if (viewName == "MenuUserManage")
            {
                viewObj = ServiceLocator.Current.GetInstance<UserSetView>();
            }
            else if (viewName == "MenuRoleManage")
            {
                viewObj = ServiceLocator.Current.GetInstance<RoleSetView>();
            }
            else if (viewName == "MenuMenuManage")
            {
                viewObj = ServiceLocator.Current.GetInstance<MenuSetView>();
            }
            else if (viewName == "MenuOrganizationManage")
            {
                viewObj = ServiceLocator.Current.GetInstance<OrganizationPrivilegeSetView>();
            }
            else if (viewName == "MenuManageLog")
            {
                viewObj = ServiceLocator.Current.GetInstance<OperateRecord>();
            }
            else if (viewName == "MenuOnlineDataOverview")
            {
                viewObj = ServiceLocator.Current.GetInstance<OnlineDataOverviewView>();
            }
            else if (viewName == "MenuOnlineDataList")
            {
                viewObj = ServiceLocator.Current.GetInstance<OnlineDataListView>();
            }
            else if (viewName == "MenuOnlineDataTile")
            {
                viewObj = ServiceLocator.Current.GetInstance<OnlineDataTileView>();
            }
            else if (viewName == "MenuOnlineDataDiagram")
            {
                viewObj = ServiceLocator.Current.GetInstance<OnlineDataDiagramView>();
            }
            else if (viewName == "MenuHistoryDataList")
            {
                viewObj = ServiceLocator.Current.GetInstance<HistoryDataListView>();
            }
            else if (viewName == "MenuHistoryDataDiagram")
            {
                viewObj = ServiceLocator.Current.GetInstance<HistoryDataDiagramView>();
            }
            else if (viewName == "MenuHistoryData")
            {
                viewObj = ServiceLocator.Current.GetInstance<HistoryDataDiagramView>();
            }
            else if (viewName == "MenuOnlineDataDiagnosis")
            {
                viewObj = ServiceLocator.Current.GetInstance<OnlineDataDiagnosisView>();
            }
            else if (viewName == "MenuOnlineDataStatistics")
            {
                viewObj = ServiceLocator.Current.GetInstance<OnlineDataStatisticsView>();
            }
            else if (viewName == "MenuHistoryDataStatistics")
            {
                viewObj = ServiceLocator.Current.GetInstance<HistoryDataStatisticsView>();
            }
            else if (viewName == "MenuHistoryEventList")
            {
                viewObj = ServiceLocator.Current.GetInstance<HistoryEventListView>();
            }
            else if (viewName == "MenuHistoryDataTrend")
            {
                viewObj = ServiceLocator.Current.GetInstance<HistoryDataTrendView>();
            }
            else if (viewName == "MenuEquipmentRunTime")
            {
                viewObj = ServiceLocator.Current.GetInstance<DeviceRunStatusListView>();
            }
            else if (viewName == "MenuRefreshData")
            {
                Status = ViewModelStatus.Querying;
                WaitInfo = "刷新中";
                CloseTabs();
                MenuManageList.Dictionary.Values.ToList().ForEach(p => p.Visibility = Visibility.Collapsed);
                await _loginUserService.SetUserLogin();
                Status = ViewModelStatus.None;
                await _loginUserService.LazyLoading();                
                return;
            }
            else if (viewName == "test")
            {
                TestWin win = new TestWin();
                win.Show();
                return;
            }
            else
            {
                return;
            }
            ICloseable view = viewObj as ICloseable;
            if (view != null)
            {
                view.Closer.RequestClose += () => region.Remove(view);
            }
            region.Add(view, viewName);
            region.Activate(view);
        }
        #endregion

        #region 中英文管理
        private void SetAsChinese()
        {
            List<ResourceDictionary> dictionaryList = new List<ResourceDictionary>();
            foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
            {
                dictionaryList.Add(dictionary);
            }
            string requestedCulture = @"/AIC.Resources;component/Themes/zh-cn.xaml";
            ResourceDictionary resourceDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedCulture));
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
            ResourceDictionary resourceDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedCulture));
            Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            TabLanguageShift();
        }
        private void TabLanguageShift()
        {
            IRegion region = this._regionManager.Regions["MainTabRegion"];
            var views = region.Views.ToList();
            for (int i = 0; i < views.Count; i++)
            {
                var viewObj = views[i];
                ICloseable view = viewObj as ICloseable;
                switch (view.Closer.Title)
                {
                    case "Home":
                    case "首页": view.Closer.Title = (string)Application.Current.Resources["tabFirst"]; break;
                    case "ServerSetting":
                    case "服务器管理": view.Closer.Title = (string)Application.Current.Resources["menuServerSetting"]; break;
                    case "CollectorSetting":
                    case "数采器管理": view.Closer.Title = (string)Application.Current.Resources["menuCollectorSetting"]; break;
                    case "UserManage":
                    case "用户管理": view.Closer.Title = (string)Application.Current.Resources["menuUserManage"]; break;
                    case "RoleManage":
                    case "角色管理": view.Closer.Title = (string)Application.Current.Resources["menuRoleManage"]; break;
                    case "SettingLog":
                    case "管理日志": view.Closer.Title = (string)Application.Current.Resources["menuManageLog"]; break;
                }
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
            string dir = System.AppDomain.CurrentDomain.BaseDirectory + "\\Log";
            System.Diagnostics.Process.Start("explorer.exe", Path.GetFullPath(dir));
        }

        private void ScreenShotFolder()
        {
            string dir = @LocalSetting.ScreenShotDir;
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
            ShowAlarmWin win = new ShowAlarmWin();
            win.ShowDialog();
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
        #endregion

        #region 数据
        private System.Windows.Threading.DispatcherTimer readDataTimer = new System.Windows.Threading.DispatcherTimer();

        private bool firstshow = false;
        private float historyplayCount = LocalSetting.HistoryModeDBCallInterval;
        private readonly SemaphoreSlim lazyLoadinglocker = new SemaphoreSlim(1);

        private async void timeCycle(object sender, EventArgs e)
        {
            NowTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            IsShowHistoryplay = LocalSetting.IsHistoryMode;
            IsHistoryRrackingMode = LocalSetting.IsHistoryRrackingMode;

            if (_loginUserService.LoginInfo.LoginStatus == true)
            {
                await lazyLoadinglocker.WaitAsync();
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
                                HistoryplayTime = DateTime.Now;
                                HistoryModeActualSpeedUpRatio = 1;
                            }
                            else
                            {
                                HistoryModeName = "历史快放模式";
                                if (HistoryplayTime == null)
                                {
                                    HistoryplayTime = LocalSetting.HistoryModeStartTime;
                                }
                                if (SpeedPause == false)
                                {
                                    if (HistoryplayTime >= LocalSetting.HistoryModeEndTime)//循环播放
                                    {
                                        HistoryplayTime = LocalSetting.HistoryModeStartTime;
                                    }

                                    HistoryModeActualSpeedUpRatio = LocalSetting.HistoryModeSpeedUpRatio * LocalSetting.HistoryModeDBCallInterval;
                                    HistoryplayTime = HistoryplayTime.Value.AddSeconds(HistoryModeActualSpeedUpRatio);
                                    HistoryplayProcess = (HistoryplayTime.Value - LocalSetting.HistoryModeStartTime).Ticks * 100 / (LocalSetting.HistoryModeEndTime - LocalSetting.HistoryModeStartTime).Ticks;
                                }
                            }
                            IsConnected = await _signalProcess.GetSignalData(HistoryplayTime.Value, true);
                            if (IsConnected == true)
                            {
                                _eventAggregator.GetEvent<SignalBroadcastingEvent>().Publish(null);
                            }
                            SetStatus();
                        }
                    }
                    else
                    {
                        HistoryplayTime = null;
                        IsConnected = await _signalProcess.GetSignalData(DateTime.Now, false);
                        if (IsConnected == true)
                        {
                            _eventAggregator.GetEvent<SignalBroadcastingEvent>().Publish(null);
                        }
                        SetStatus();
                    }
                }
                finally
                {
                    lazyLoadinglocker.Release();
                }
            }
            else
            {
                InitStatus();
            }

            if (firstshow == false)
            {
                firstshow = true;
                Login();
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
        }

        private void SetStatus()
        {
            var signals = _signalProcess.Signals.OfType<BaseAlarmSignal>();
            Count = signals.Count();
            NormalCount = signals.Where(o => o.IsConnected == true && (o.DelayAlarmGrade == AlarmGrade.HighNormal || o.DelayAlarmGrade == AlarmGrade.LowNormal)).Count();
            PreAlertCount = signals.Where(o => o.IsConnected == true && (o.DelayAlarmGrade == AlarmGrade.HighPreAlert || o.DelayAlarmGrade == AlarmGrade.LowPreAlert)).Count();
            AlertCount = signals.Where(o => o.IsConnected == true && (o.DelayAlarmGrade == AlarmGrade.HighAlert || o.DelayAlarmGrade == AlarmGrade.LowAlert)).Count();
            DangerCount = signals.Where(o => o.IsConnected == true && (o.DelayAlarmGrade == AlarmGrade.HighDanger || o.DelayAlarmGrade == AlarmGrade.LowDanger)).Count();
            AbnormalCount = signals.Where(o => o.IsConnected == true && (o.DelayAlarmGrade == AlarmGrade.Abnormal)).Count();
            UnConnectCount = signals.Where(o => o.IsConnected == false).Count();
            if (DangerCount > 0)
            {
                Alarm = AlarmGrade.HighDanger;
            }
            else if (AlertCount > 0)
            {
                Alarm = AlarmGrade.HighAlert;
            }
            else if (PreAlertCount > 0)
            {
                Alarm = AlarmGrade.HighPreAlert;
            }
            else
            {
                Alarm = AlarmGrade.HighNormal;
            }
            DangerList = new ObservableCollection<BaseAlarmSignal>(signals.Where(o => o.AlarmAck == false && o.IsConnected == true && (o.DelayAlarmGrade == AlarmGrade.HighDanger || o.DelayAlarmGrade == AlarmGrade.LowDanger)).Take(5));
            TotalDangerList = new ObservableCollection<BaseAlarmSignal>(signals.Where(o => o.IsConnected == true && (o.DelayAlarmGrade == AlarmGrade.HighDanger || o.DelayAlarmGrade == AlarmGrade.LowDanger)));
        }

        private void DecreaseSpeed()
        {            
            HistoryplayTime = HistoryplayTime.Value.AddSeconds(0 - HistoryModeActualSpeedUpRatio);            
            HistoryplayProcess = (HistoryplayTime.Value - LocalSetting.HistoryModeStartTime).Ticks * 100 / (LocalSetting.HistoryModeEndTime - LocalSetting.HistoryModeStartTime).Ticks;
            if (SpeedPause == false)
            {
                HistoryplayTime = HistoryplayTime.Value.AddSeconds(0 - HistoryModeActualSpeedUpRatio);
            }
            if (trackChanged != null)
            {
                trackChanged(this, null);
            }
        }

        private void IncreaseSpeed()
        {
            HistoryplayTime = HistoryplayTime.Value.AddSeconds(HistoryModeActualSpeedUpRatio);
            HistoryplayProcess = (HistoryplayTime.Value - LocalSetting.HistoryModeStartTime).Ticks * 100 / (LocalSetting.HistoryModeEndTime - LocalSetting.HistoryModeStartTime).Ticks;
            if (SpeedPause == false)
            {
                HistoryplayTime.Value.AddSeconds(0 - HistoryModeActualSpeedUpRatio);
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
                    HistoryplayTime = LocalSetting.HistoryModeStartTime + new TimeSpan((long)(value * (LocalSetting.HistoryModeEndTime - LocalSetting.HistoryModeStartTime).Ticks / 100));
                    HistoryplayTime = HistoryplayTime.Value.AddSeconds(0 - HistoryModeActualSpeedUpRatio);
                }
                else
                {
                    HistoryplayTime = LocalSetting.HistoryModeStartTime + new TimeSpan((long)(value * (LocalSetting.HistoryModeEndTime - LocalSetting.HistoryModeStartTime).Ticks / 100));
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
            //for (int i = 0; i < 5; i++)
            //{
            //    CustomSystemHappenEvent(new Core.Models.CustomSystemException()
            //    {
            //        Type = 201,
            //        Degree = 1,
            //        EventTime = DateTime.Now,
            //        Remarks = "ceshi",
            //        T_Item_Guid = Guid.NewGuid(),
            //        T_Item_Type = 12,
            //    });
            //}
        }

        private SoundPlayer player = new System.Media.SoundPlayer();
        private void OnAlarmGradeChanged(string propertyName)
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
            else if (Alarm == AlarmGrade.HighAlert || Alarm == AlarmGrade.LowAlert)
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

        #region 事件

        private List<double> heightoffsets = new List<double>();
        private const double constantheightoffset = 60;
        private object threadLock = new object();

        private void CustomSystemHappenEvent(CustomSystemException ex)
        {
            CustomSystemException.Add(ex);    
            if (CustomSystemException.Count > 1000)
            {
                for (int i = 0; i < 100; i++)
                {
                    CustomSystemException.RemoveAt(0);
                }
            }     

            if (heightoffsets.Count >= 5)//避免太多弹出窗口
            {
                return;
            }

            lock (threadLock)
            {
                if (Mute == false)//静音
                {
                    playsound();
                }
                string title = GetEnumDescription.GetDescription((CustomSystemType)ex.Type) + "事件发生";
                string content = ex.EventTime.ToString("yyyy-MM-dd HH:mm:ss") + "   " + ex.Remarks;
                double heightoffset = 0;
                while (true)
                {
                    if (heightoffsets.Contains(heightoffset))
                    {
                        heightoffset += constantheightoffset;
                    }
                    else
                    {
                        heightoffsets.Add(heightoffset);
                        break;
                    }
                }

                TaskbarNotifier taskbarnotifier = new TaskbarNotifier(title, content, heightoffset);
                taskbarnotifier.ThisClosed += Taskbarnotifier_ThisClosed;
                taskbarnotifier.Show();
            }
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
    }
}

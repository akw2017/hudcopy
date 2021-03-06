﻿using AIC.Core;
using AIC.Core.Events;
using AIC.Core.Models;
using AIC.Core.SignalModels;
using AIC.Core.UserManageModels;
using AIC.CoreType;
using AIC.HistoryDataPage.Views;
using AIC.CYSHPage.Menus;
using AIC.CYSHPage.Models;
using AIC.CYSHPage.Views;
using AIC.CYSHPage.Views;
using AIC.PDAPage.Views;
using AIC.Resources.Models;
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
using System.IO;
using System.Linq;
using System.Media;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Wpf.CloseTabControl;

namespace AIC.CYSHPage.ViewModels
{
    class MainRegionViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILocalConfiguration _localConfiguration;
        private readonly ILoginUserService _loginUserService;
        private readonly IUserManageService _userManageService;
        private readonly ISignalProcess _signalProcess;      

              
        private static Uri loginView = new Uri("LoginView", UriKind.Relative);
        private static Uri cYSHDataListView = new Uri("CYSHDataListView", UriKind.Relative);

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

            readDataTimer.Tick += new EventHandler(timeCycle);
            readDataTimer.Interval = new TimeSpan(0, 0, 0, 1);
            readDataTimer.Start();

            WhenPropertyChanged.Where(o => o.ToString() == "Alarm").Subscribe(OnAlarmGradeChanged);

            _eventAggregator.GetEvent<LoginEvent>().Subscribe(LoginFinishEvent);

            getTree();
        }      
     
        #region 登录      

        private void LoginFinishEvent(LoginInfo loginInfo)//登录消息
        {
            _regionManager.RequestNavigate(RegionNames.HomeViewMainRegion, cYSHDataListView);

           // _eventAggregator.GetEvent<ServerMarkEvent>().Publish(_localConfiguration.ServerInfoList);

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
       
        private async void Login()
        {
            //如果没有登录
            if (_loginUserService.GetUserLoginStatus() == false)
            {
                if (new LoginManage().Login(_loginUserService.DefaultLoginServer()))
                {
                    Status = ViewModelStatus.Querying;
                    //把登录配置保存到本地
                    _localConfiguration.WriteServerInfo(_localConfiguration.ServerInfoList);
                    await _loginUserService.SetUserLogin();
                    _eventAggregator.GetEvent<LoginEvent>().Publish(_loginUserService.LoginInfo);                   
                    Status = ViewModelStatus.None;
                }
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("请退出后再登陆！！！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                //if (LoginManage.Login(_loginUserService.LoginInfo))
                //{
                //    _loginUserService.SetUserLogin();
                //    _eventAggregator.GetEvent<LoginEvent>().Publish(_loginUserService.LoginInfo);
                //    //把登录配置保存到本地
                //    _localConfiguration.WriteServerInfo(_localConfiguration.ServerInfoList);
                //}
            }
        }

        private void Logout()
        {
            _regionManager.RequestNavigate(RegionNames.HomeViewMainRegion, loginView);

            _loginUserService.SetUserLogout();

            LoginUser = null;

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

            Login1Visibility = Visibility.Collapsed;
            Login1Visibility = Visibility.Collapsed;
            Login2Visibility = Visibility.Collapsed;
        }

        private void Lock()
        {
            //如果没有登录
            if (_loginUserService.GetUserLoginStatus() == false)
            {
                return;
            }
            if (new LockManage().Lock(_loginUserService.LoginInfo))
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
            string str = para as string;

            //如果没有登录
            if (_loginUserService.GetUserLoginStatus() == false)
            {
                return;
            }
            if (new LockManage().UnLock(_loginUserService.LoginInfo))
            {
                IRegion region = this._regionManager.Regions["MainTabRegion"];
                if (str == null)
                {
                    var views = region.ActiveViews.ToList();
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
                        if (view.Closer.Visibility == Visibility.Visible)
                        {
                            view.Closer.LockVisibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void Exit()
        {
            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["strExitSystem"], (string)Application.Current.Resources["strExit"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
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
                //viewObj = ServiceLocator.Current.GetInstance<ServerSetView>();
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
                //viewObj = ServiceLocator.Current.GetInstance<OnlineDataOverviewView>();
            }
            else if (viewName == "MenuOnlineDataList")
            {
                viewObj = ServiceLocator.Current.GetInstance<CYSHDataListView>();
            }
            else if (viewName == "MenuOnlineDataTile")
            {
                //viewObj = ServiceLocator.Current.GetInstance<OnlineDataTileView>();
            }
            else if (viewName == "MenuOnlineDataDiagram")
            {
                //viewObj = ServiceLocator.Current.GetInstance<OnlineDataDiagramView>();
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
            else if (viewName == "MenuRefreshData")
            {
                Status = ViewModelStatus.Querying;
                WaitInfo = "刷新中";
                await _loginUserService.SetUserLogin();
                Status = ViewModelStatus.None;
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
           
        }

        private void SystemLog()
        {
            string dir = System.Environment.CurrentDirectory + "\\Log";
            System.Diagnostics.Process.Start("explorer.exe ", Path.GetFullPath(dir));
        }

        private void Feedback()
        {
           
        }

        private void AbnormalList()
        {
           
        }
        #endregion

        #region 字段和属性
        private ObservableCollection<ExceptionModel> ExceptionModel;

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

        private ObservableCollection<PropertyNodeItem> catalogTreeItems;
        public ObservableCollection<PropertyNodeItem> CatalogTreeItems
        {
            get { return catalogTreeItems; }
            set
            {
                catalogTreeItems = value;
                OnPropertyChanged("CatalogTreeItems");
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

        private ICommand systemLogCommand;
        public ICommand SystemLogCommand
        {
            get
            {
                return this.systemLogCommand ?? (this.systemLogCommand = new DelegateCommand(() => this.SystemLog()));
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

        public ICommand selectTreeChangeComamnd;
        public ICommand SelectTreeChangeComamnd
        {
            get
            {
                return this.selectTreeChangeComamnd ?? (this.selectTreeChangeComamnd = new DelegateCommand<object>(value => this.treeSelected(value)));
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
        private int preWarningCount;
        public int PreWarningCount
        {
            get { return preWarningCount; }
            set
            {
                if (preWarningCount != value)
                {
                    preWarningCount = value;
                    this.OnPropertyChanged(() => this.PreWarningCount);
                }
            }
        }
        private int warningCount;
        public int WarningCount
        {
            get { return warningCount; }
            set
            {
                if (warningCount != value)
                {
                    warningCount = value;
                    this.OnPropertyChanged(() => this.WarningCount);
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
        #endregion

        private System.Windows.Threading.DispatcherTimer readDataTimer = new System.Windows.Threading.DispatcherTimer();

        private bool firstshow = false;

        private async void timeCycle(object sender, EventArgs e)
        {
            NowTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (_loginUserService.LoginInfo.LoginStatus == true)
            {
                IsConnected = await _signalProcess.GetSignalData();
                if (IsConnected == true)
                {
                    _eventAggregator.GetEvent<TestEvent>().Publish(null);
                }
                SetStatus();
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
            PreWarningCount = 0;
            WarningCount = 0;           
            DangerCount = 0;
            AbnormalCount = 0;
            UnConnectCount = 0;
            Alarm = AlarmGrade.HighNormal;
            IsConnected = false;
        }

        private void SetStatus()
        {
            var signals = _signalProcess.Signals.OfType<BaseAlarmSignal>();
            Count = signals.Count();
            NormalCount = signals.Where(o => o.IsConnected == true && (o.DelayAlarmGrade == AlarmGrade.HighNormal || o.DelayAlarmGrade == AlarmGrade.LowNormal)).Count();
            PreWarningCount = signals.Where(o => o.IsConnected == true && (o.DelayAlarmGrade == AlarmGrade.HighPreAlert || o.DelayAlarmGrade == AlarmGrade.LowPreAlert)).Count();
            WarningCount = signals.Where(o => o.IsConnected == true && (o.DelayAlarmGrade == AlarmGrade.HighAlert || o.DelayAlarmGrade == AlarmGrade.LowAlert)).Count();
            DangerCount = signals.Where(o => o.IsConnected == true && (o.DelayAlarmGrade == AlarmGrade.HighDanger || o.DelayAlarmGrade == AlarmGrade.LowDanger)).Count();
            AbnormalCount = signals.Where(o => o.IsConnected == true && (o.DelayAlarmGrade == AlarmGrade.Abnormal)).Count();
            UnConnectCount = signals.Where(o => o.IsConnected == false).Count();
            if (DangerCount > 0)
            {
                Alarm = AlarmGrade.HighDanger;
            }
            else if (WarningCount > 0)
            {
                Alarm = AlarmGrade.HighAlert;
            }
            else if (PreWarningCount > 0)
            {
                Alarm = AlarmGrade.HighPreAlert;
            }
            else
            {
                Alarm = AlarmGrade.HighNormal;
            }
            //IsConnected = true;
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


        private void AlarmList()
        {
           
        }

        private void ExceptList()
        {
           
        }

        private void Setting()
        {
            
        }

        private void treeSelected(object value)
        {
            var node = value as PropertyNodeItem;
            if (node == null)
            {
                return;
            }
            switch (node.Name)
            {
                //case "数据总况": _regionManager.RequestNavigate("MainRegion", dataSummaryView); TreeDeSelected(TempTreeItems); break;
                //case "缺勤统计": _regionManager.RequestNavigate("MainRegion", AbsenceResultView); TreeDeSelected(TempTreeItems); break;
                //case "漏检统计": _regionManager.RequestNavigate("MainRegion", missingCountView); TreeDeSelected(TempTreeItems); break;
                //case "临时测量数据查询": _regionManager.RequestNavigate("MainRegion", tempMeasureView); TreeDeSelected(TreeItems); break;
                default: break;
            }
        }

        private void getTree()
        {
            CatalogTreeItems = new ObservableCollection<PropertyNodeItem>();
            PropertyNodeItem node1 = new PropertyNodeItem("检测数据");
            node1.IsExpanded = true;     
            PropertyNodeItem node11 = new PropertyNodeItem("数据列表");
            node11.IsExpanded = true;
            CatalogTreeItems.Add(node1);
            node1.AddChild(node11);
        }
    }
}

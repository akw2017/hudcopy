using AIC.Core;
using AIC.Core.Events;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.Core.UserManageModels;
using AIC.DeviceDataPage.Views;
using AIC.DiagnosePage.Views;
using AIC.HistoryDataPage.Views;
using AIC.HomePage.Menus;
using AIC.HomePage.Models;
using AIC.HomePage.ViewModels;
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
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AICDiagnose.ViewModels
{
    class DiagnoseMainRegionViewModel : BindableBase
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

        public DiagnoseMainRegionViewModel(ILocalConfiguration localConfiguration, IRegionManager regionManager, IEventAggregator eventAggregator, ILoginUserService loginUserService, IUserManageService userManageService, ISignalProcess signalProcess)
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

            readDataTimer.Tick += new EventHandler(timeCycle);
            readDataTimer.Interval = new TimeSpan(0, 0, 0, 1);
            readDataTimer.Start();

            _eventAggregator.GetEvent<LoginEvent>().Subscribe(LoginFinishEvent);
            _eventAggregator.GetEvent<DeviceDiagnoseEvent>().Subscribe(DeviceDiagnoseOpen);//<!--诊断-->


            InitLanguage();

            this.GCTimer.Interval = TimeSpan.FromMinutes(10); //垃圾释放定时器 我定为每十分钟释放一次，大家可根据需要修改
            this.GCTimer.Start();
            this.EventsRegistion();    // 注册事件

        }

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

        private ICommand closeWindowsCommand;
        public ICommand CloseWindowsCommand
        {
            get
            {
                return this.closeWindowsCommand ?? (this.closeWindowsCommand = new DelegateCommand(() => this.CloseWindows()));
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

        private async void Logout()
        {
#if XBAP
            MessageBoxResult result = MessageBox.Show((string)Application.Current.Resources["strLogoutSystem"], (string)Application.Current.Resources["strLogout"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#else
            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["strLogoutSystem"], (string)Application.Current.Resources["strLogout"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#endif
            if (result == MessageBoxResult.OK)
            {
                Status = ViewModelStatus.Querying;
                WaitInfo = "注销数据中";
                await _loginUserService.SetUserLogout();
                LoginUser = null;
                _loginUserService.CloseTabs();
                LoginVisibility = Visibility.Collapsed;
                _regionManager.RequestNavigate(RegionNames.MainBodyRegion, loginView);
                Status = ViewModelStatus.None;
                return;
            }
        }

        private void OpenFirstTab()
        {
            _regionManager.RequestNavigate(RegionNames.MainBodyRegion, tabView);
            ////首页默认打开
            _loginUserService.GotoTab<DeviceFaultDiagnoseView>("诊断首页");
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
            else if (viewName == "MenuEditDeviceComponents")
            {
                _loginUserService.GotoTab<EditDeviceComponentsView>(viewName);
            }
            else if (viewName == "MenuEditDeviceTemplate")
            {
                _loginUserService.GotoTab<EditDeviceTemplateView>(viewName);
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

        private void timeCycle(object sender, EventArgs e)
        {
            NowTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (ExceptionModel.Count > 0)
            {
                ExceptListVisibility = Visibility.Visible;
            }
            else
            {
                ExceptListVisibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region 诊断
        private void DeviceDiagnoseOpen(Tuple<DateTime, DeviceTreeItemViewModel> data)
        {
            if (MenuManageList.MenuDeviceFaultDiagnose.Visibility != Visibility.Visible)
            {
#if XBAP
                MessageBox.Show("请确定自己的权限是否可以使用诊断", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请确定自己的权限是否可以使用诊断", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }
            if (data != null && data.Item1 != null && data.Item2 != null)
            {
                DeviceFaultDiagnoseView view = _loginUserService.GotoTab<DeviceFaultDiagnoseView>("MenuDeviceFaultDiagnose") as DeviceFaultDiagnoseView;
                if (view != null)
                {
                    view.GotoDevice(data.Item2, data.Item1);
                }
            }
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

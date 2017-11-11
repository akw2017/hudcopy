using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using System;
using Prism.Regions;
using AIC.Core;
using Microsoft.Practices.ServiceLocation;

using AIC.OnLineMonitor.Views;
using AIC.PDA.Views;
using Wpf.CloseTabControl;
using AIC.Menus;
using MahApps.Metro;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media;
using Prism.Events;
using AIC.Core.Models;
using AIC.Core.Events;
using AIC.HomePage.Views;
using AIC.Resources.Models;

namespace AIC.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        private static Uri onLineMonitorView = new Uri("OnLineMonitorView", UriKind.Relative);
        private static Uri pdaManageView = new Uri("PDAManageView", UriKind.Relative);
        private static Uri loginView = new Uri("LoginView", UriKind.Relative);
        public ShellViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;            

            // create accent color menu items for the demo
            this.AccentColors = ThemeManager.Accents
                                            .Select(a => new AccentColorMenuData() { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush })
                                            .ToList();

            // create metro theme color menu items for the demo
            this.AppThemes = ThemeManager.AppThemes
                                           .Select(a => new AppThemeMenuData() { Name = a.Name, BorderColorBrush = a.Resources["BlackColorBrush"] as Brush, ColorBrush = a.Resources["WhiteColorBrush"] as Brush })
                                           .ToList();


            _eventAggregator.GetEvent<LoginEvent>().Subscribe(Login);           
        }

        private void Login(LoginInfo user)
        {
            LoginUser = String.Format("当前用户: {0}  服务器IP: {1}  级别：{2}", user.User, user.Info.IP, user.Level);
            //_regionManager.RequestNavigate(RegionNames.HomeViewMainRegion, mapView);
            SystemMenus = GetSystemMenus();
            HardMenus = GetHardMenus();
            DataMenus = GetDataMenus();
        }

        private void Logout()
        {            
            _regionManager.RequestNavigate(RegionNames.HomeViewMainRegion, loginView);           
        }

        private RootMenuViewModel GetSystemMenus()
        {
            RootMenu root = new RootMenu("系统");
            root.MenuItems.Add(new MenuItem("菜单管理", NavigateCommand, IconAddress.Application_Edit_Icon));
            root.MenuItems.Add(new MenuItem("角色管理", NavigateCommand, IconAddress.Users_Icon));
            root.MenuItems.Add(new MenuItem("用户管理", NavigateCommand, IconAddress.User_Icon));
            root.MenuItems.Add(new MenuItem("操作日志", NavigateCommand, IconAddress.Page_Error_Icon));
            RootMenuViewModel rootMenuViewModel = new RootMenuViewModel(root);
            return rootMenuViewModel;
        }

        private RootMenuViewModel GetHardMenus()
        {
            RootMenu root = new RootMenu("硬件设置");
            root.MenuItems.Add(new MenuItem("服务器管理", NavigateCommand, IconAddress.Database_Gear_Icon));
            root.MenuItems.Add(new MenuItem("数采器管理", NavigateCommand, IconAddress.Brick_Edit_Icon));
            root.MenuItems.Add(new MenuItem("设备树管理", NavigateCommand, IconAddress.Chart_Organisation_Icon));          
            RootMenuViewModel rootMenuViewModel = new RootMenuViewModel(root);
            return rootMenuViewModel;
        }

        private RootMenuViewModel GetDataMenus()
        {
            RootMenu root = new RootMenu("数据监测");
            root.MenuItems.Add(new MenuItem("在线数据", NavigateCommand, IconAddress.Database_Refresh_Icon));
            root.MenuItems.Add(new MenuItem("历史数据", NavigateCommand, IconAddress.Database_Save_Icon));
            root.MenuItems.Add(new MenuItem("报警数据", NavigateCommand, IconAddress.Database_Error_Icon));
            root.MenuItems.Add(new MenuItem("设备运行监测", NavigateCommand, IconAddress.Database_Go_Icon));
            root.MenuItems.Add(new MenuItem("设备运行分析", NavigateCommand, IconAddress.Database_Lightning_Icon));
            RootMenuViewModel rootMenuViewModel = new RootMenuViewModel(root);
            return rootMenuViewModel;
        }

        //private void Navigate(string obj)
        //{
        //    if (obj == "OnLineMonitor")
        //    {
        //        _regionManager.RequestNavigate(RegionNames.MainTabRegion, onLineMonitorView);
        //    }
        //    else if (obj == "PDAManage")
        //    {
        //        _regionManager.RequestNavigate(RegionNames.MainTabRegion, pdaManageView);
        //    }
        //}

        private void Navigate(object para)
        {
            string viewName = para as string;
            if(viewName == null)
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
                return;
            }
            object viewObj = null;
            switch (viewName)
            {
                case "OnLineMonitor":
                    viewObj = ServiceLocator.Current.GetInstance<OnLineMonitorView>();
                    break;
                case "PDAManage":
                    viewObj = ServiceLocator.Current.GetInstance<PDAManageView>();
                    break;
                case "Servers":
                    viewObj = ServiceLocator.Current.GetInstance<ServerSetView>();
                    break;
                default:
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

        public List<AccentColorMenuData> AccentColors { get; set; }
        public List<AppThemeMenuData> AppThemes { get; set; }
        public RootMenuViewModel SystemMenus { get; set; }
        public RootMenuViewModel HardMenus { get; set; }
        public RootMenuViewModel DataMenus { get; set; }

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

        private string _title = "第二代在线";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public ICommand LoginCommand { get; }
        public ICommand logoutCommand;
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

    }
}

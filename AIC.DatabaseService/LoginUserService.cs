﻿using AIC.Core;
using AIC.Core.ControlModels;
using AIC.Core.Events;
using AIC.Core.Helpers;
using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.SignalModels;
using AIC.Core.UserManageModels;
using AIC.CoreType;
using AIC.DatabaseService.Models;
using AIC.M9600.Common.MasterDB.Generated;
using AIC.Resources.Views;
using AIC.ServiceInterface;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Wpf.CloseTabControl;

namespace AIC.DatabaseService
{
    public class LoginUserService : ILoginUserService
    {
        private readonly ILocalConfiguration _localConfiguration;
        private readonly IHardwareService _hardwareService;
        private readonly IOrganizationService _organizationService;
        private readonly IUserManageService _userManageService;
        private readonly ICardProcess _cardProcess;
        private readonly ISignalProcess _signalProcess;
        private readonly IDatabaseComponent _databaseComponent;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        public LoginInfo LoginInfo { get; set; }  
        public MenuManageList MenuManageList { get; set; }
        public ObservableCollection<ExceptionModel> ExceptionModel { get; private set; }
        public ObservableCollection<T1_SystemEvent> CustomSystemException { get; private set; }

        public LoginUserService(ILocalConfiguration localConfiguration, IHardwareService hardwareService, IOrganizationService organizationService, IUserManageService userManageService, ICardProcess cardProcess, ISignalProcess signalProcess, IDatabaseComponent databaseComponent, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _localConfiguration = localConfiguration;
            _hardwareService = hardwareService;
            _organizationService = organizationService;
            _userManageService = userManageService;
            _cardProcess = cardProcess;
            _signalProcess = signalProcess;
            _databaseComponent = databaseComponent;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            MenuManageList = new MenuManageList();
            ExceptionModel = new ObservableCollection<ExceptionModel>();
            CustomSystemException = new ObservableCollection<T1_SystemEvent>();

            _eventAggregator.GetEvent<ThrowExceptionEvent>().Subscribe(ManageException, ThreadOption.UIThread);           
        }

        public void Initialize()
        {
            LoginInfo = new LoginInfo("", "", new ServerInfo());
        }

        #region 登录管理
        public bool GetUserLoginStatus()
        {
            return LoginInfo.LoginStatus;
        }

        public async Task SetUserLogin()
        {
            foreach (var server in _localConfiguration.LoginServerInfoList)
            {
                _databaseComponent.InitDatabase(server.IP);
                List<Task> lttask = new List<Task>();
                lttask.Add(_databaseComponent.LoadUserData(server.IP));
                lttask.Add(_databaseComponent.LoadRoleData(server.IP));
                lttask.Add(_databaseComponent.LoadMenuData(server.IP));
                lttask.Add(_databaseComponent.LoadDeviceData(server.IP));
                lttask.Add(_databaseComponent.LoadOrganizationData(server.IP));
                lttask.Add(_databaseComponent.LoadItemData(server.IP));
                lttask.Add(_databaseComponent.LoadOrganizationPrivilegeData(server.IP));
                //lttask.Add(_databaseComponent.LoadHardwave(item.IP));//改为延时加载
                await Task.WhenAll(lttask.ToArray());
            }
          
            string mainserverip = LoginInfo.ServerInfo.IP;
            _databaseComponent.SetMainServerIp(mainserverip);

            _organizationService.SetDivFres();

            LoginInfo.LoginStatus = true;
            if (LoginInfo.UserName == "superadmin" && LoginInfo.Password == "superadmin")
            {
                MenuManageList.Dictionary.Values.ToList().ForEach(p => p.Visibility = Visibility.Visible);
                _organizationService.InitOrganizations(true);
            }        
            else if (LoginInfo.ServerInfo.Permission == "超级管理员" || LoginInfo.ServerInfo.Permission == "superadmin")
            {
                MenuManageList.Dictionary.Values.ToList().ForEach(p => p.Visibility = Visibility.Visible);
                _organizationService.InitOrganizations(true);
            }
            else//菜单权限=主服务器权限
            { 
                var user = (from p in _databaseComponent.GetUserData(mainserverip) where p.Name == LoginInfo.UserName select p).FirstOrDefault();
                if (user != null)
                {
                    LoginInfo.UserCode = user.Code;

                    var menu = (from p in _databaseComponent.GetMenuData(mainserverip) where p.Guid == user.T_Menu_Guid select p).ToList();
                    foreach (var submenu in menu)
                    {
                        if (MenuManageList.Dictionary.Keys.Contains(submenu.InternalNumber))
                        {
                            MenuManageList.Dictionary[submenu.InternalNumber].Visibility = Visibility.Visible;
                        }                       
                    }

                    _organizationService.SetUserOrganizationPrivilege(user.T_OrganizationPrivilege_Guid);
                    _organizationService.InitOrganizations(false);
                }
            }

            //_hardwareService.InitServers();//改为延时加载
            _signalProcess.InitSignals();
            //AddOperateRecord(UserOperateType.Login);  //登录记录取消
        }

        private readonly SemaphoreSlim locker = new SemaphoreSlim(1);
        public async Task LazyLoading()//延时加载
        {
            await locker.WaitAsync();
            try
            {
                foreach (var server in _localConfiguration.LoginServerInfoList)
                {
                    List<Task> lttask = new List<Task>();
                    lttask.Add(_databaseComponent.LoadHardwave(server.IP));
                    lttask.Add(_databaseComponent.GetMeasureUnit(server.IP));
                    await Task.WhenAll(lttask.ToArray());
                }

                _hardwareService.InitServers(_localConfiguration.LoginServerInfoList);
                _signalProcess.LazyInitSignals();
            }
            finally
            {
                locker.Release();
            }
        }
        public async Task AwaitLazyLoading()
        {
            await locker.WaitAsync();
            try
            { }
            finally
            {
                locker.Release();
            }
        }
        public async Task SetUserLogout()//注销
        {
            await locker.WaitAsync();
            try
            {
                LoginInfo.ClearLoginInfo();
                _databaseComponent.ClearDatabase();
                //重新获取本地配置文件
                _localConfiguration.ReadServerInfo();

                //_eventAggregator.GetEvent<ServerChangedEvent>().Publish(_localConfiguration.ServerInfoList);
                MenuManageList.Dictionary.Values.ToList().ForEach(p => p.Visibility = Visibility.Collapsed);
            }
            finally
            {
                locker.Release();
            }
        }

        public LoginInfo InitLoginServer(ServerInfo serverinfo)
        {
            return TestInitAdmin(serverinfo);
        }
        public LoginInfo DefaultLoginServer()
        {
            ServerInfo defaultserverinfo = null;

            defaultserverinfo = _localConfiguration.LoginServerInfoList.FirstOrDefault();
            if (defaultserverinfo == null)
            {
                if (_localConfiguration.ServerInfoList.Count > 0)
                {
                    defaultserverinfo = _localConfiguration.ServerInfoList[0];
                }
                else
                {
                    defaultserverinfo = new ServerInfo();
                }
            }
            return TestInitAdmin(defaultserverinfo);
        }
        private LoginInfo TestInitAdmin(ServerInfo serverinfo)
        {    
            LoginInfo.SetLoginInfo(serverinfo.UserName, serverinfo.UserPwd, "", serverinfo);
            return LoginInfo;
        }
        #endregion

        #region 操作记录
        public void AddOperateRecord(string ip, UserOperateType operateType)
        {         
            T1_OperateRecord LM_OperateRecord = new T1_OperateRecord()
            {               
                T_User_Name = LoginInfo.UserName,
                T_User_Code = LoginInfo.UserCode,
                OperateTime = DateTime.Now,
                OperateType = (short)operateType,
            };

            _databaseComponent.Add<T_OperateRecord>(ip, LM_OperateRecord);
        }

        public async Task<List<T1_OperateRecord>> GetOperateRecord(string ip, DateTime start, DateTime end, string name, UserOperateType operateType)
        {
            List<T_OperateRecord> list = new List<T_OperateRecord>();
            if (name.Trim() == "" && operateType == UserOperateType.None)
            {
                list = await _databaseComponent.Query<T_OperateRecord>(ip, null, "(OperateTime >= @0 and OperateTime <= @1)", new object[] { start, end, });
             }
            else if (name.Trim() != "" && operateType == UserOperateType.None)
            {
                list = await _databaseComponent.Query<T_OperateRecord>(ip, null, "((OperateTime >= @0 and OperateTime <= @1) and T_User_Name like '%'+ @2+ '%')", new object[] { start, end, name });
            }
            else if (name.Trim() == "" && operateType != UserOperateType.None)
            {
                list = await _databaseComponent.Query<T_OperateRecord>(ip, null, "((OperateTime >= @0 and OperateTime <= @1) and UserOperateType = @2)", new object[] { start, end, ((short)operateType) });
            }
            else
            {
                list = await _databaseComponent.Query<T_OperateRecord>(ip, null, "((OperateTime >= @0 and OperateTime <= @1) and T_User_Name like '%'+ @2+ '%' and UserOperateType = @3)", new object[] { start, end, name, ((short)operateType).ToString() });
            }
            return list.Select(p => ClassCopyHelper.AutoCopy<T_OperateRecord, T1_OperateRecord>(p)).ToList();
        }
        #endregion

        #region 系统事件
        public async Task AddSystemEvent(string ip, T1_SystemEvent exception)
        {
            if (LocalSetting.IsHistoryMode == true)//历史模式不存事件
            {
                return;
            }
            string hostName = System.Net.Dns.GetHostName();//本机名 
            System.Net.IPAddress[] addressList = System.Net.Dns.GetHostAddresses(hostName);//会返回所有地址，包括IPv4和IPv6 
          
            if (addressList.Select(p => p.ToString()).Contains(ip) || ip == "127.0.0.1" || LocalSetting.IsEventServer == true)//运行在服务器上，或者事件服务器
            {
                await _databaseComponent.Add<T_SystemEvent>(ip, exception);
            }
        }

        public async Task<List<T1_SystemEvent>> GetSystemEvent(string ip, DateTime start, DateTime end, string name, CustomSystemType systemtype)
        {
            List<T_SystemEvent> list = new List<T_SystemEvent>();
            if (name.Trim() == "" && systemtype == CustomSystemType.None)
            {
                list = await _databaseComponent.Query<T_SystemEvent>(ip, null, "(EventTime >= @0 and EventTime <= @1)", new object[] { start, end, });
            }
            else if (name.Trim() != "" && systemtype == CustomSystemType.None)
            {
                list = await _databaseComponent.Query<T_SystemEvent>(ip, null, "((EventTime >= @0 and EventTime <= @1) and Remarks like '%'+ @2+ '%')", new object[] { start, end, name });
            }
            else if (name.Trim() == "" && systemtype != CustomSystemType.None)
            {
                list = await _databaseComponent.Query<T_SystemEvent>(ip, null, "((EventTime >= @0 and EventTime <= @1) and Type = @2)", new object[] { start, end, ((int)systemtype) });
            }
            else
            {
                list = await _databaseComponent.Query<T_SystemEvent>(ip, null, "((EventTime >= @0 and EventTime <= @1) and Remarks like '%'+ @2+ '%' and Type = @3)", new object[] { start, end, name, ((short)systemtype).ToString() });
            }
            return list.Select(p => ClassCopyHelper.AutoCopy<T_SystemEvent, T1_SystemEvent>(p)).ToList();
        }
        #endregion

        #region 异常
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private void ManageException(Tuple<string, Exception> tuple)
        {
            try
            {
                if (tuple == null) return;
                var ex = tuple.Item2;
                while (ex != null)
                {
                    var exceptionModel = BuildExceptionModel(tuple);
                    lock (ExceptionModel)
                    {
                        if (ExceptionModel.Count > 100)
                        {
                            ExceptionModel.Clear();
                        }                   
                        ExceptionModel.Add(exceptionModel);
                    }
                    log.Error(exceptionModel.Target, tuple.Item2);
                    ex = ex.InnerException;
                    if (ex != null)
                    {
                        tuple = Tuple.Create<string, Exception>(ex.Source, ex);
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("未处理的严重异常", e);
            }
        }

        public void ClearException()
        {
            lock (ExceptionModel)
            {
                ExceptionModel.Clear();
            }
        }

        private ExceptionModel BuildExceptionModel(Tuple<string, Exception> tuple)
        {
            var ex = tuple.Item2;
            string data = string.Empty;
            if (ex.Data.Keys.Count > 0)
            {
                StringBuilder result = new StringBuilder();
                foreach (object key in ex.Data.Keys)
                {
                    if (key != null && ex.Data[key] != null)
                    {
                        result.AppendLine(key.ToString() + " = " + ex.Data[key].ToString());
                    }
                }

                if (result.Length > 0) result.Length = result.Length - 1;
                data = result.ToString();
                return new ExceptionModel(tuple.Item1, ex.GetType().ToString(), ex.Message, ex.StackTrace, data, ex.TargetSite.ToString(), ex.Source);
            }
            else
            {
                string stackTrace  = ex.StackTrace ?? "";
                string targetSite = (ex.TargetSite == null) ? "" : ex.TargetSite.ToString();
                string source = ex.Source ?? "";                
                return new ExceptionModel(tuple.Item1, ex.GetType().ToString(), ex.Message, stackTrace, "", targetSite, source);
            }

        }
        #endregion

        #region 调转管理

        public object GotoTab<T>(string viewName)
        {
            if (!this._regionManager.Regions.ContainsRegionWithName("MainTabRegion"))
            {
                return null;
            }

            IRegion region = this._regionManager.Regions["MainTabRegion"];
            if (region.GetView(viewName) != null)
            {
                region.Activate(region.GetView(viewName));
                return region.GetView(viewName);
            }

            Object viewObj = ServiceLocator.Current.GetInstance<T>();
            ICloseable view = viewObj as ICloseable;
            if (view != null)
            {
                view.Closer.RequestClose += () =>
                {
                    var disposable = view as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                    region.Remove(view);
                };
            }
            region.Add(view, viewName);
            region.Activate(view);
            return viewObj;
        }
        public void TabLanguageShift()
        {
            if (this._regionManager.Regions.ContainsRegionWithName("MainTabRegion"))
            {
                IRegion region = this._regionManager.Regions["MainTabRegion"];
                var views = region.Views.ToList();
                for (int i = 0; i < views.Count; i++)
                {
                    var viewObj = views[i];
                    ICloseable view = viewObj as ICloseable;
                    view.Closer.Title = (string)Application.Current.Resources[view.Closer.TitleResourceName];
                }
            }
        }
        public void CloseTabs(bool firstTabClosed = true)
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
                else if (firstTabClosed == true)
                {
                    region.Remove(view);
                }
            }
        }
        public void LockTabs()
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

        public void UnLockTabs(string name)
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

        public ServerInfo GetServerInfo(string servername)
        {
            var serverinfo = _localConfiguration.ServerInfoList.Where(p => p.Name == servername).FirstOrDefault();
            if (serverinfo == null)
            {
                serverinfo = _localConfiguration.ServerInfoList.Where(p => p.IP == servername).FirstOrDefault();
                if (serverinfo == null)
                {
                    serverinfo = _localConfiguration.ServerInfoList.FirstOrDefault();
                }
            }
            return serverinfo;
        }
        #endregion
    }
}

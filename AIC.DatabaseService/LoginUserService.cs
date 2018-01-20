using AIC.Core;
using AIC.Core.ControlModels;
using AIC.Core.Events;
using AIC.Core.Helpers;
using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.UserManageModels;
using AIC.CoreType;
using AIC.DatabaseService.Models;
using AIC.M9600.Common.MasterDB.Generated;
using AIC.Resources.Views;
using AIC.ServiceInterface;
using Prism.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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

        public LoginInfo LoginInfo { get; set; }  
        public MenuManageList MenuManageList { get; set; }
        public ObservableCollection<ExceptionModel> ExceptionModel { get; private set; }
        public ObservableCollection<T1_SystemEvent> CustomSystemException { get; private set; }
        public LoginUserService(ILocalConfiguration localConfiguration, IHardwareService hardwareService, IOrganizationService organizationService, IUserManageService userManageService, ICardProcess cardProcess, ISignalProcess signalProcess, IDatabaseComponent databaseComponent, IEventAggregator eventAggregator)
        {
            _localConfiguration = localConfiguration;
            _hardwareService = hardwareService;
            _organizationService = organizationService;
            _userManageService = userManageService;
            _cardProcess = cardProcess;
            _signalProcess = signalProcess;
            _databaseComponent = databaseComponent;
            _eventAggregator = eventAggregator;

            MenuManageList = new MenuManageList();
            ExceptionModel = new ObservableCollection<ExceptionModel>();
            CustomSystemException = new ObservableCollection<T1_SystemEvent>();

            _eventAggregator.GetEvent<ThrowExceptionEvent>().Subscribe(ManageException);           
        }

        public void Initialize()
        {
            var serverinfolist = _localConfiguration.ServerInfoList ?? new List<ServerInfo>();
            LoginInfo = new LoginInfo("", "", new ServerInfo(), serverinfolist);
        }

        #region 登录管理
        public bool GetUserLoginStatus()
        {
            return LoginInfo.LoginStatus;
        }

        public async Task SetUserLogin()
        {         
            var servers = _localConfiguration.ServerInfoList.Where(p => p.LoginResult == true);

            foreach (var item in servers.Distinct(EqualityHelper<ServerInfo>.CreateComparer(p => p.IP)))
            {
                _databaseComponent.InitDatabase(item.IP);
                List<Task> lttask = new List<Task>();
                lttask.Add(_databaseComponent.LoadUserData(item.IP));
                lttask.Add(_databaseComponent.LoadRoleData(item.IP));
                lttask.Add(_databaseComponent.LoadMenuData(item.IP));
                lttask.Add(_databaseComponent.LoadDeviceData(item.IP));
                lttask.Add(_databaseComponent.LoadOrganizationData(item.IP));
                lttask.Add(_databaseComponent.LoadItemData(item.IP));
                lttask.Add(_databaseComponent.LoadOrganizationPrivilegeData(item.IP));
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
            //AddOperateRecord(OperateType.Login);  //登录记录取消
        }

        private readonly SemaphoreSlim locker = new SemaphoreSlim(1);
        public async Task LazyLoading()//延时加载
        {
            await locker.WaitAsync();
            try
            {
                var servers = _localConfiguration.ServerInfoList.Where(p => p.LoginResult == true);
                foreach (var item in servers.Distinct(EqualityHelper<ServerInfo>.CreateComparer(p => p.IP)))
                {
                    List<Task> lttask = new List<Task>();
                    lttask.Add(_databaseComponent.LoadHardwave(item.IP));
                    lttask.Add(_databaseComponent.GetMeasureUnit(item.IP));
                    await Task.WhenAll(lttask.ToArray());
                }
                _hardwareService.InitServers();
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
        public void SetUserLogout()//注销
        {
            LoginInfo.ClearLoginInfo();
            _databaseComponent.ClearDatabase();
            //重新获取本地配置文件
            _localConfiguration.ReadServerInfo();
            LoginInfo.ServerInfoList = _localConfiguration.ServerInfoList ?? new List<ServerInfo>();
            _eventAggregator.GetEvent<ServerChangedEvent>().Publish(_localConfiguration.ServerInfoList);
            MenuManageList.Dictionary.Values.ToList().ForEach(p => p.Visibility = Visibility.Collapsed);
        }

        public LoginInfo InitLoginServer(ServerInfo serverinfo)
        {
            return TestInitAdmin(serverinfo);
        }
        public LoginInfo DefaultLoginServer()
        {
            ServerInfo defaultserverinfo = null;

            defaultserverinfo = (from p in LoginInfo.ServerInfoList where p.IsLogin == true select p).FirstOrDefault();
            if (defaultserverinfo == null)
            {
                if (LoginInfo.ServerInfoList.Count > 0)
                {
                    defaultserverinfo = LoginInfo.ServerInfoList[0];
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
        public void AddOperateRecord(string ip, OperateType operateType)
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

        public async Task<List<T1_OperateRecord>> GetOperateRecord(string ip, DateTime start, DateTime end, string name, OperateType operateType)
        {
            List<T_OperateRecord> list = new List<T_OperateRecord>();
            if (name.Trim() == "" && operateType == OperateType.None)
            {
                list = await _databaseComponent.Query<T_OperateRecord>(ip, null, "(OperateTime >= @0 and OperateTime <= @1)", new object[] { start, end, });
             }
            else if (name.Trim() != "" && operateType == OperateType.None)
            {
                list = await _databaseComponent.Query<T_OperateRecord>(ip, null, "((OperateTime >= @0 and OperateTime <= @1) and T_User_Name like '%'+ @2+ '%')", new object[] { start, end, name });
            }
            else if (name.Trim() == "" && operateType != OperateType.None)
            {
                list = await _databaseComponent.Query<T_OperateRecord>(ip, null, "((OperateTime >= @0 and OperateTime <= @1) and OperateType = @2)", new object[] { start, end, ((short)operateType) });
            }
            else
            {
                list = await _databaseComponent.Query<T_OperateRecord>(ip, null, "((OperateTime >= @0 and OperateTime <= @1) and T_User_Name like '%'+ @2+ '%' and OperateType = @3)", new object[] { start, end, name, ((short)operateType).ToString() });
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


    }
}

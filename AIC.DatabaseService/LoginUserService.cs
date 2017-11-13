using AIC.Core;
using AIC.Core.ControlModels;
using AIC.Core.Events;
using AIC.Core.Helpers;
using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.UserManageModels;
using AIC.DatabaseService.Models;
using AIC.M9600.Common.MasterDB.Generated;
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
                //lttask.Add(_databaseComponent.LoadHardwave(item.IP));
                await Task.WhenAll(lttask.ToArray());
            }
          
            string serverip = LoginInfo.ServerInfo.IP;
            _databaseComponent.MainServerIp = serverip;

            _organizationService.T_Organization = _databaseComponent.T_Organization;
            //_organizationService.T_OrganizationPrivilege = _databaseComponent.T_OrganizationPrivilege;
            _organizationService.T_Item = _databaseComponent.T_Item;
            _organizationService.T_DivFreInfo.Clear();
            foreach (var rootcard in _databaseComponent.T_RootCard)
            {
                _organizationService.T_DivFreInfo.Add(rootcard.Key, rootcard.Value.T_DivFreInfo);
            }
            _userManageService.T_Menu = _databaseComponent.T_Menu;
            _userManageService.T_User = _databaseComponent.T_User;
            _userManageService.T_Role = _databaseComponent.T_Role;
            _userManageService.T_OrganizationPrivilege = _databaseComponent.T_OrganizationPrivilege;            
           
            _hardwareService.T_RootCard = _databaseComponent.T_RootCard;   

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
            else
            { 
                var user = (from p in _userManageService.T_User[serverip] where p.Name == LoginInfo.UserName select p).FirstOrDefault();
                if (user != null)
                {
                    LoginInfo.UserCode = user.Code;

                    var menu = (from p in _userManageService.T_Menu[serverip] where p.Guid == user.T_Menu_Guid select p).ToList();
                    foreach (var submenu in menu)
                    {
                        switch (submenu.InternalNumber)
                        {
                            case 0: MenuManageList.MenuUserManage.Visibility = Visibility.Visible; break;
                            case 1: MenuManageList.MenuRoleManage.Visibility = Visibility.Visible; break;
                            case 2: MenuManageList.MenuMenuManage.Visibility = Visibility.Visible; break;
                            case 3: MenuManageList.MenuOrganizationManage.Visibility = Visibility.Visible; break;
                            case 4: MenuManageList.MenuManageLog.Visibility = Visibility.Visible; break;
                            case 5: MenuManageList.MenuServerSetting.Visibility = Visibility.Visible; break;
                            case 6: MenuManageList.MenuCollectorSetting.Visibility = Visibility.Visible; break;
                            case 7: MenuManageList.MenuEquipmentSetting.Visibility = Visibility.Visible; break;
                            case 8: MenuManageList.MenuOnlineData.Visibility = Visibility.Visible; break;
                            case 9: MenuManageList.MenuHistoricalData.Visibility = Visibility.Visible; break;
                            case 10: MenuManageList.MenuAlarmData.Visibility = Visibility.Visible; break;
                            case 11: MenuManageList.MenuRunningMonitor.Visibility = Visibility.Visible; break;
                            case 12: MenuManageList.MenuRunningAnalyze.Visibility = Visibility.Visible; break;
                            case 13: MenuManageList.MenuOnlineDataList.Visibility = Visibility.Visible; break;
                            case 14: MenuManageList.MenuOnlineDataTile.Visibility = Visibility.Visible; break;
                            case 15: MenuManageList.MenuOnlineDataDiagram.Visibility = Visibility.Visible; break;
                            case 16: MenuManageList.MenuOnlineDataOverview.Visibility = Visibility.Visible; break;
                            case 17: MenuManageList.MenuHistoryDataList.Visibility = Visibility.Visible; break;
                            case 18: MenuManageList.MenuHistoryDataDiagram.Visibility = Visibility.Visible; break;
                        }
                    }
                    _organizationService.T_OrganizationPrivilege.Clear();
                    foreach (var organizationPrivilege in _databaseComponent.T_OrganizationPrivilege)
                    {
                        var userorganizationPrivilege = (from p in organizationPrivilege.Value where p.Guid == user.T_OrganizationPrivilege_Guid select p).ToList();
                        _organizationService.T_OrganizationPrivilege.Add(organizationPrivilege.Key, userorganizationPrivilege);
                    }
                    _organizationService.InitOrganizations(false);
                }
            }

            //_hardwareService.InitServers();//改为延时加载
            _signalProcess.InitSignals();
            //AddOperateRecord(OperateType.Login);  
        }

        private readonly SemaphoreSlim lazyLoadinglocker = new SemaphoreSlim(1);
        public async Task LazyLoading()
        {
            await lazyLoadinglocker.WaitAsync();
            try
            {
                var servers = _localConfiguration.ServerInfoList.Where(p => p.LoginResult == true);
                foreach (var item in servers.Distinct(EqualityHelper<ServerInfo>.CreateComparer(p => p.IP)))
                {
                    List<Task> lttask = new List<Task>();
                    lttask.Add(_databaseComponent.LoadHardwave(item.IP));
                    await Task.WhenAll(lttask.ToArray());
                }
                _hardwareService.InitServers();
                _signalProcess.LazyInitSignals();
            }
            finally
            {
                lazyLoadinglocker.Release();
            }
        }
        public async Task AwaitLazyLoading()
        {
            await lazyLoadinglocker.WaitAsync();
            try
            { }
            finally
            {
                lazyLoadinglocker.Release();
            }
        }
        public void SetUserLogout()
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

        public void AddOperateRecord(OperateType operateType)
        {         
            T1_OperateRecord LM_OperateRecord = new T1_OperateRecord()
            {               
                T_User_Name = LoginInfo.UserName,
                T_User_Code = LoginInfo.UserCode,
                OperateTime = DateTime.Now,
                OperateType = (short)operateType,
            };

            _databaseComponent.Add<T_OperateRecord>(LoginInfo.ServerInfo.IP, LM_OperateRecord);
        }

        public async Task<List<T1_OperateRecord>> GetOperateRecord(DateTime start, DateTime end, string name, OperateType operateType)
        {
            List<T_OperateRecord> list = new List<T_OperateRecord>();
            if (name.Trim() == "" && operateType == OperateType.None)
            {
                list = await _databaseComponent.Query<T_OperateRecord>(LoginInfo.ServerInfo.IP, null, "(OperateTime >= @0 or OperateTime <= @1)", new object[] { start, end, });
             }
            else if (name.Trim() != "" && operateType == OperateType.None)
            {
                list = await _databaseComponent.Query<T_OperateRecord>(LoginInfo.ServerInfo.IP, null, "((OperateTime >= @0 or OperateTime <= @1) and T_User_Name like '%'+ @2+ '%')", new object[] { start, end, name });
            }
            else if (name.Trim() == "" && operateType != OperateType.None)
            {
                list = await _databaseComponent.Query<T_OperateRecord>(LoginInfo.ServerInfo.IP, null, "((OperateTime >= @0 or OperateTime <= @1) and OperateType = @2)", new object[] { start, end, ((short)operateType) });
            }
            else
            {
                list = await _databaseComponent.Query<T_OperateRecord>(LoginInfo.ServerInfo.IP, null, "((OperateTime >= @0 or OperateTime <= @1) and T_User_Name like '%'+ @2+ '%' and OperateType = @3)", new object[] { start, end, name, ((short)operateType).ToString() });
            }
            return list.Select(p => ClassCopyHelper.AutoCopy<T_OperateRecord, T1_OperateRecord>(p)).ToList();
        }
               
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
                    if (ExceptionModel.Count > 100)
                    {
                        lock (ExceptionModel)
                        {
                            ExceptionModel.Clear();
                        }
                    }
                    ExceptionModel.Add(exceptionModel);
                    
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
    }
}

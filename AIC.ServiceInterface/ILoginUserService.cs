using AIC.Core.ControlModels;
using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.SignalModels;
using AIC.Core.UserManageModels;
using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.ServiceInterface
{
    public interface ILoginUserService
    {
        LoginInfo LoginInfo { get; set; }        
        MenuManageList MenuManageList { get; set; }
        ObservableCollection<ExceptionModel> ExceptionModel { get; }
        ObservableCollection<T1_SystemEvent> CustomSystemException { get; }
        void Initialize();

        LoginInfo InitLoginServer(ServerInfo serverinfo);
        LoginInfo DefaultLoginServer();

        bool GetUserLoginStatus();
        Task SetUserLogin();
        void SetUserLogout();
        Task LazyLoading();
        Task AwaitLazyLoading();
        void AddOperateRecord(string ip, OperateType operateType);
        Task<List<T1_OperateRecord>> GetOperateRecord(string ip, DateTime start, DateTime end, string name, OperateType operateType);
        void ClearException();
        Task AddSystemEvent(string ip, T1_SystemEvent exception);
        Task<List<T1_SystemEvent>> GetSystemEvent(string ip, DateTime start, DateTime end, string name, CustomSystemType systemtype);

        object GotoTab<T>(string viewName);
        void TabLanguageShift();
        void CloseTabs(bool firstTabClosed = true);
        void LockTabs();
        void UnLockTabs(string name);
        ServerInfo GetServerInfo(string servername);
    }
}

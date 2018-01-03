using AIC.Core.ControlModels;
using AIC.Core.LMModels;
using AIC.Core.Models;
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
        ObservableCollection<CustomSystemException> CustomSystemException { get; }
        void Initialize();

        LoginInfo InitLoginServer(ServerInfo serverinfo);
        LoginInfo DefaultLoginServer();

        bool GetUserLoginStatus();
        Task SetUserLogin();
        void SetUserLogout();
        Task LazyLoading();
        Task AwaitLazyLoading();
        void AddOperateRecord(OperateType operateType);
        Task<List<T1_OperateRecord>> GetOperateRecord(string ip, DateTime start, DateTime end, string name, OperateType operateType);
    }
}

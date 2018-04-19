using AIC.Core.DataModels;
using AIC.Core.HardwareModels;
using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.M9600.Common.DTO.Web;
using AIC.M9600.Common.SlaveDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.ServiceInterface
{
    public interface IDatabaseComponent
    {
        Dictionary<string, T1_RootCard> T_RootCard { get; }
        Dictionary<string, List<T1_Organization>> T_Organization { get; }
        Dictionary<string, List<T1_Device>> T_Device { get; }
        Dictionary<string, List<T1_Item>> T_Item { get; }
        Dictionary<string, List<T1_User>> T_User { get; }
        Dictionary<string, List<T1_Role>> T_Role { get; }
        Dictionary<string, List<T1_Menu>> T_Menu { get; }
        Dictionary<string, List<T1_OrganizationPrivilege>> T_OrganizationPrivilege { get; }
        List<string> UnitCategory { get; }
        string MainServerIp { get; }
        void InitDatabase(string ip);
        void ClearDatabase();
        List<string> GetServerIPCategory();
        List<T1_User> GetUserData(string ip);
        List<T1_Role> GetRoleData(string ip);
        List<T1_Menu> GetMenuData(string ip);
        List<T1_Device> GetDeviceData(string ip);
        List<T1_Organization> GetOrganizationData(string ip);
        List<T1_Item> GetItemData(string ip);
        List<T1_OrganizationPrivilege> GetOrganizationPrivilegeData(string ip);
        T1_RootCard GetRootCard(string ip);       
        void SetMainServerIp(string ip);   
        Task<List<T1_User>> LoadUserData(string ip);
        Task<List<T1_Role>> LoadRoleData(string ip);
        Task<List<T1_Menu>> LoadMenuData(string ip);
        Task<List<T1_Device>> LoadDeviceData(string ip);
        Task<List<T1_Organization>> LoadOrganizationData(string ip);
        Task<List<T1_Item>> LoadItemData(string ip);
        Task<List<T1_OrganizationPrivilege>> LoadOrganizationPrivilegeData(string ip);
        Task<List<T1_DivFreInfo>> LoadDivFreData(string ip);

        Task<T1_RootCard> LoadHardwave(string ip);    
        Task<bool> UploadHardwave(string ip, T1_RootCard rootcard);
        Task<bool> DeleteHardwave(string ip, T1_RootCard rootcard);

        Task<long[]> Add<T>(string ip, ICollection<T> objs);
        Task<long> Add<T>(string ip, T obj);
        Task<List<T>> Query<T>(string ip, ICollection<string> columns, string condition, object[] args);
        //Task<List<T>> Query<T>(string ip, string condition, object[] args);
        Task<bool> Modify<T>(string ip, ICollection<string> columns, ICollection<T> objs);
        Task<bool> Modify<T>(string ip, ICollection<string> columns, T obj);
        Task<bool> Delete<T>(string ip, ICollection<object> ids);
        Task<bool> Delete<T>(string ip, long id);      
      
        Task<bool> Complex(string ip, IDictionary<string, ICollection<object>> addObjs,
          IDictionary<string, Tuple<ICollection<string>, ICollection<object>>> modifyObjs,
          IDictionary<string, Tuple<string, ICollection<object>>> deleteObjs);
        Task<RootCard> ComplexWithJson(string serverip, string ip, string json, IDictionary<string, ICollection<object>> addObjs,
        IDictionary<string, Tuple<ICollection<string>, ICollection<object>>> modifyObjs,
        IDictionary<string, Tuple<string, ICollection<object>>> deleteObjs);

        Task<Tuple<T1_Role, T1_User>> UserLogin(string ip, string user, string pwd);
        Task<string> UserPing(string ip);
        Task GetMeasureUnit(string ip);

        Task<Dictionary<string, LatestSampleData>> GetLatestData();
        Task<List<T>> GetHistoryData<T>(string ip, Guid guid, string[] columns, DateTime startTime, DateTime endTime, string condition, object[] args);
        Task<List<T>> GetHistoryData<T>(string ip, Guid[] guids, string[] columns, DateTime startTime, DateTime endTime, string condition, object[] args, IProgress<double> progress = null);
        Task<LatestSampleData> GetHistoryData(string ip, Dictionary<Guid, string> itemGuids, DateTime startTime, DateTime endTime);
        Task<List<T>> GetHistoryWaveformData<T>(string ip, Dictionary<Guid, Tuple<Guid, DateTime>> recordLabs, IProgress<double> process = null);
        Task<Dictionary<Guid, Dictionary<string, double>>> GetStatisticsData(string ip, HashSet<Guid> guidlist);
        Task<Dictionary<Guid, List<D_SlotStatistic>>> GetDailyStatisticsData(string ip, HashSet<Guid> guidlist, DateTime startTime, DateTime endTime);

        Task<List<T>> GetDailyMedianData<T>(string ip, string table, DateTime startTime, DateTime endTime, string columns = "*");
        Task<Dictionary<string, List<T>>> GetDailyMedianData<T>(DateTime startTime, DateTime endTime, string columns = "*");

        Task<List<IBaseAlarmSlot>> GetUniformHistoryData(int itemType, string ip, Guid guid, string[] columns, DateTime startTime, DateTime endTime, string condition, object[] args);
    }
}

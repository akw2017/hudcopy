using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.Core.UserManageModels;
using AIC.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DatabaseService
{
    public class UserManageService : IUserManageService
    {
        private readonly IOrganizationService _organizationService;
        //public Dictionary<string, List<T1_Role>> T_Role { get; set; }
        //public Dictionary<string, List<T1_User>> T_User { get; set; }
        //public Dictionary<string, List<T1_Menu>> T_Menu { get; set; }     
        //public Dictionary<string, List<LM_OperateRecord>> T_OperateRecord { get; set; }
        //public Dictionary<string, List<T1_OrganizationPrivilege>> T_OrganizationPrivilege { get; set; }
        //public List<ObservableCollection<OrganizationTreeItemViewModel>> OrganizationPrivilegeTreeItemViewModel { get; set; }
        //private Dictionary<string, List<T1_Organization>> T_Organization { get; set; }
        public UserManageService(IOrganizationService organizationService)
        {
            _organizationService = organizationService;

            //T_Role = new Dictionary<string, List<T1_Role>>();
            //T_User = new Dictionary<string, List<T1_User>>();
            //T_Menu = new Dictionary<string, List<T1_Menu>>();
            //T_OperateRecord = new Dictionary<string, List<LM_OperateRecord>>();
            //T_OrganizationPrivilege = new Dictionary<string, List<T1_OrganizationPrivilege>>();
            //T_Organization = _organizationService.T_Organization;
        }

        public void InitUserManage()
        {
            //if (T_Role.Count == 0)
            //{
            //    getRole("192.168.0.210");
            //}
            //if (T_Menu.Count == 0)
            //{
            //    getMenu("192.168.0.210");
            //}
            //if (T_OrganizationPrivilege.Count == 0)
            //{
            //    getOrganizationPrivilege("192.168.0.210");
            //}
            //if (T_User.Count == 0)
            //{
            //    getUser("192.168.0.210");
            //}
        }

        private void getRole(string ip)
        {
            //T_Role[ip].Clear();
            //T_Role[ip].Add(new T1_Role() { id = 1, Name = "管理员", Guid = Guid.NewGuid(), Is_Admin = true, Is_SuperAdmin = false, Sort_No = 0, Is_Disabled = false });
            //T_Role[ip].Add(new T1_Role() { id = 2, Name = "工程师", Guid = Guid.NewGuid(), Is_Admin = false, Is_SuperAdmin = false, Sort_No = 1, Is_Disabled = false });
            //T_Role[ip].Add(new T1_Role() { id = 3, Name = "操作员", Guid = Guid.NewGuid(), Is_Admin = false, Is_SuperAdmin = false, Sort_No = 2, Is_Disabled = false });
        }

        private void getMenu(string ip)
        {
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();

            //T_Menu[ip].Clear();
            //T_Menu[ip].Add(new T1_Menu() { id = 1, Name = "菜单1", InternalNumber = 0, ShowText = "用户管理", Guid = guid1 });
            //T_Menu[ip].Add(new T1_Menu() { id = 2, Name = "菜单1", InternalNumber = 1, ShowText = "角色管理", Guid = guid1 });
            //T_Menu[ip].Add(new T1_Menu() { id = 3, Name = "菜单1", InternalNumber = 2, ShowText = "菜单管理", Guid = guid1 });
            //T_Menu[ip].Add(new T1_Menu() { id = 4, Name = "菜单1", InternalNumber = 3, ShowText = "组织管理", Guid = guid1 });
            //T_Menu[ip].Add(new T1_Menu() { id = 5, Name = "菜单1", InternalNumber = 4, ShowText = "操作日志", Guid = guid1 });
            //T_Menu[ip].Add(new T1_Menu() { id = 6, Name = "菜单1", InternalNumber = 5, ShowText = "服务器管理", Guid = guid1 });
            //T_Menu[ip].Add(new T1_Menu() { id = 7, Name = "菜单1", InternalNumber = 6, ShowText = "数采器管理", Guid = guid1 });
            //T_Menu[ip].Add(new T1_Menu() { id = 8, Name = "菜单1", InternalNumber = 7, ShowText = "设备管理", Guid = guid1 });
            //T_Menu[ip].Add(new T1_Menu() { id = 9, Name = "菜单1", InternalNumber = 8, ShowText = "在线数据", Guid = guid1 });
            //T_Menu[ip].Add(new T1_Menu() { id = 10, Name = "菜单1", InternalNumber = 9, ShowText = "历史数据", Guid = guid1 });
            //T_Menu[ip].Add(new T1_Menu() { id = 11, Name = "菜单1", InternalNumber = 10, ShowText = "报警数据", Guid = guid1 });
            //T_Menu[ip].Add(new T1_Menu() { id = 19, Name = "菜单1", InternalNumber = 13, ShowText = "在线数据列表", Guid = guid1 });
            //T_Menu[ip].Add(new T1_Menu() { id = 20, Name = "菜单1", InternalNumber = 14, ShowText = "在线频谱分析", Guid = guid1 });
            //T_Menu[ip].Add(new T1_Menu() { id = 21, Name = "菜单1", InternalNumber = 15, ShowText = "在线数据图表", Guid = guid1 });
            //T_Menu[ip].Add(new T1_Menu() { id = 22, Name = "菜单1", InternalNumber = 16, ShowText = "在线数据总貌", Guid = guid1 });

            //T_Menu[ip].Add(new T1_Menu() { id = 12, Name = "菜单2", InternalNumber = 0, ShowText = "用户管理", Guid = guid2 });
            //T_Menu[ip].Add(new T1_Menu() { id = 13, Name = "菜单2", InternalNumber = 1, ShowText = "角色管理", Guid = guid2 });
            //T_Menu[ip].Add(new T1_Menu() { id = 14, Name = "菜单2", InternalNumber = 2, ShowText = "菜单管理", Guid = guid2 });
            //T_Menu[ip].Add(new T1_Menu() { id = 15, Name = "菜单2", InternalNumber = 3, ShowText = "组织管理", Guid = guid2 });
            //T_Menu[ip].Add(new T1_Menu() { id = 16, Name = "菜单2", InternalNumber = 4, ShowText = "操作日志", Guid = guid2 });

            //T_Menu[ip].Add(new T1_Menu() { id = 17, Name = "菜单3", InternalNumber = 0, ShowText = "用户管理", Guid = guid3 });
            //T_Menu[ip].Add(new T1_Menu() { id = 18, Name = "菜单3", InternalNumber = 1, ShowText = "角色管理", Guid = guid3 });
        }

        private void getOrganizationPrivilege(string ip)
        {
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();

            //T_OrganizationPrivilege[ip].Clear();
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 1, Name = "组织1", Guid = guid1, T_Organization_Guid = T_Organization[ip][0].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 2, Name = "组织1", Guid = guid1, T_Organization_Guid = T_Organization[ip][1].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 3, Name = "组织1", Guid = guid1, T_Organization_Guid = T_Organization[ip][2].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 4, Name = "组织1", Guid = guid1, T_Organization_Guid = T_Organization[ip][3].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 5, Name = "组织1", Guid = guid1, T_Organization_Guid = T_Organization[ip][4].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 6, Name = "组织1", Guid = guid1, T_Organization_Guid = T_Organization[ip][8].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 7, Name = "组织1", Guid = guid1, T_Organization_Guid = T_Organization[ip][12].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 8, Name = "组织1", Guid = guid1, T_Organization_Guid = T_Organization[ip][16].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 9, Name = "组织1", Guid = guid1, T_Organization_Guid = T_Organization[ip][17].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 10, Name = "组织1", Guid = guid1, T_Organization_Guid = T_Organization[ip][18].Guid });
           

            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 11, Name = "组织2", Guid = guid2, T_Organization_Guid = T_Organization[ip][0].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 12, Name = "组织2", Guid = guid2, T_Organization_Guid = T_Organization[ip][1].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 13, Name = "组织2", Guid = guid2, T_Organization_Guid = T_Organization[ip][2].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 14, Name = "组织2", Guid = guid2, T_Organization_Guid = T_Organization[ip][3].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 15, Name = "组织2", Guid = guid2, T_Organization_Guid = T_Organization[ip][4].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 16, Name = "组织2", Guid = guid2, T_Organization_Guid = T_Organization[ip][8].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 17, Name = "组织2", Guid = guid2, T_Organization_Guid = T_Organization[ip][12].Guid });
            

            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 18, Name = "组织3", Guid = guid3, T_Organization_Guid = T_Organization[ip][0].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 19, Name = "组织3", Guid = guid3, T_Organization_Guid = T_Organization[ip][1].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 20, Name = "组织3", Guid = guid3, T_Organization_Guid = T_Organization[ip][2].Guid });
            //T_OrganizationPrivilege[ip].Add(new T1_OrganizationPrivilege() { id = 21, Name = "组织3", Guid = guid3, T_Organization_Guid = T_Organization[ip][3].Guid });
            
        }

        private void getUser(string ip)
        {
            //T_User[ip].Clear();
            //T_User[ip].Add(new T1_User() { id = 1, Name = "admin", Alias_Name = "管理员甲", Password = "admin", T_Role_Guid = T_Role[ip][0].Guid, T_Menu_Guid = T_Menu[ip][0].Guid, T_OrganizationPrivilege_Guid = T_OrganizationPrivilege[ip][0].Guid, Is_Disabled = false, Is_Inside = false });
            //T_User[ip].Add(new T1_User() { id = 2, Name = "engineer", Alias_Name = "工程师乙", Password = "engineer", T_Role_Guid = T_Role[ip][1].Guid, T_Menu_Guid = T_Menu[ip][11].Guid, T_OrganizationPrivilege_Guid = T_OrganizationPrivilege[ip][10].Guid, Is_Disabled = false, Is_Inside = false });
            //T_User[ip].Add(new T1_User() { id = 3, Name = "operator", Alias_Name = "操作员丙", Password = "operator", T_Role_Guid = T_Role[ip][2].Guid, T_Menu_Guid = T_Menu[ip][16].Guid, T_OrganizationPrivilege_Guid = T_OrganizationPrivilege[ip][17].Guid, Is_Disabled = false, Is_Inside = false });
        }

        //public void AddOperateRecord(string ip, string name, string code, UserOperateType operateType)
        //{
        //    int max = T_OperateRecord.Count;
        //    T_OperateRecord[ip].Add(new LM_OperateRecord()
        //    {
        //        id = max + 1,
        //        T_User_Name = name,
        //        T_User_Code = code,
        //        OperateTime = DateTime.Now,
        //        UserOperateType = (short)operateType,
        //    });
        //}       

    }
}

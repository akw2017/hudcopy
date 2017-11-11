using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public class T1_UserTable
    {
        public List<T1_Organization> T_Organization { get; set; }
        public List<T1_Device> T_Device { get; set; }
        public List<T1_Item> T_Item { get; set; }
        public List<T1_User> T_User { get; set; }
        public List<T1_Role> T_Role { get; set; }
        public List<T1_Menu> T_Menu { get; set; }
        public List<T1_OrganizationPrivilege> T_OrganizationPrivilege { get; set; }     

        public T1_UserTable()
        {
            T_Organization = new List<T1_Organization>();
            T_Device = new List<T1_Device>();
            T_Item = new List<T1_Item>();
            T_User = new List<T1_User>();
            T_Role = new List<T1_Role>();
            T_Menu = new List<T1_Menu>();
            T_OrganizationPrivilege = new List<T1_OrganizationPrivilege>();        
        }
    }
}

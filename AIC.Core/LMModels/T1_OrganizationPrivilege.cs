using AIC.Core.UserManageModels;
using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public class T1_OrganizationPrivilege : T_OrganizationPrivilege, IAnobject, Iid
    {
        //public int id { get; set; }//自增ID
        //public string Name { get; set; }//名称 
        //public Guid Guid { get; set; }//名称 
        //public string Code { get; set; }//代号 
        //public string T_Organization_Code { get; set; }//代号
        //public Guid T_Organization_Guid { get; set; }//Guid      
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //浅拷贝
        public T1_OrganizationPrivilege ShallowClone()
        {
            return this.Clone() as T1_OrganizationPrivilege;
        }
    }
}

using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public enum OperateType
    {
        None = -1,
        Login = 0,
        UserManage = 1,
        RoleManage = 2,
        MenuManage = 3,
        OrganizationManage = 4,
    }
    public class T1_OperateRecord : T_OperateRecord, Iid
    {
        //public int id { get; set; }//自增ID
        //public string T_User_Name { get; set; }//用户登录名
        //public string T_User_Code { get; set; }//用户代号
        //public int OperateType { get; set; }//操作类型
        //public DateTime OperateTime { get; set; }//操作时间
        //public string Remarks { get; set; }//描述
    }
}

using AIC.Core.UserManageModels;
using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public class T1_Role : T_Role, IAnobject, Iid
    {
        //public int id { get; set; }//自增ID
        //public string Name { get; set; }//角色名(职务名)
        //public Guid Guid { get; set; }//Guid
        //public string Code { get; set; }//角色代号
        //public int Sort_No { get; set; }//排序序号
        //public bool Is_Admin { get; set; }//是否管理员
        //public bool Is_SuperAdmin { get; set; }//是否管理员
        //public bool Is_Disabled { get; set; }//禁止显示       
        //public bool Is_Cooperator { get; set; }//是否协作管理员
        //public bool Is_Opened { get; set; }//是否开放
        //public bool Remarks { get; set; }//标注

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //浅拷贝
        public T1_Role ShallowClone()
        {
            return this.Clone() as T1_Role;
        }
    }
}

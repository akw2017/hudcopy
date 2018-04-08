using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.CoreType
{
    public enum UserOperateType
    {
        [Description("-")]
        None = -1,
        //[Description("登录")]
        //Login = 0,
        [Description("用户管理")]
        UserManage = 1,
        [Description("角色管理")]
        RoleManage = 2,
        [Description("菜单管理")]
        MenuManage = 3,
        [Description("组织管理")]
        OrganizationManage = 4,
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public class T1_UserAddition
    {
        public T1_User T_User { get; set; }
        public List<T1_Role> T_Role { get; set; }
        public List<T1_Menu> T_Menu { get; set; }
        public List<T1_OrganizationPrivilege> T_OrganizationPrivilege { get; set; }
    }
}

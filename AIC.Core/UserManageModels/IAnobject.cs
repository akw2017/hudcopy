using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.UserManageModels
{
    //仅仅是把有这个属性的类继承它，没有特殊含义的接口
    public interface IAnobject
    {
        string Name { get; set; }
        Guid Guid { get; set; }
        string Code { get; set; }
    }
}

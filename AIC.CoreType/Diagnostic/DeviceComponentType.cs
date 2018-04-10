using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.CoreType
{
    public enum DeviceComponentType
    {
        [Description("轴承")]
        Bearing = 0,
        [Description("皮带")]
        Belt,
        [Description("齿轮")]
        Gear,
        [Description("电机")]
        Motor,
        [Description("叶轮")]
        Impeller,
    }
}

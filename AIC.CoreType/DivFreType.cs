using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum DivFreType
    {
        [EnumDescription(EnumValue = DivFreType.RPM)]
        RPM=0,
        [EnumDescription(EnumValue = DivFreType.Custom)]
        Custom=1,
        [EnumDescription(EnumValue = DivFreType.Range)]
        Range = 2,

    }
}

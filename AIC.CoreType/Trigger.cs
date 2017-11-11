using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum TriggerType
    {
        [EnumDescription(EnumValue = TriggerType.Auto)]
        Auto = 0,
        [EnumDescription(EnumValue = TriggerType.RPM)]
        RPM,
        [EnumDescription(EnumValue = TriggerType.Angle)]
        Angle,
        [EnumDescription(EnumValue = TriggerType.Cycle)]
        Cycle,
    }
}

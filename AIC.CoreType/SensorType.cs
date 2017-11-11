using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum SensorType
    {
        [EnumDescription(EnumValue = SensorType.Acceleration)]
        Acceleration=0,
        [EnumDescription(EnumValue = SensorType.Speed)]
        Speed,
        [EnumDescription(EnumValue = SensorType.Displacement)]
        Displacement
    }
}

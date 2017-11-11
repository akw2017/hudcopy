using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum Unit
    {
        [EnumDescription(EnumValue = Unit.Acceleration)]
        Acceleration=0,
        [EnumDescription(EnumValue = Unit.Velocity)]
        Velocity,
        [EnumDescription(EnumValue = Unit.Displacement)]
        Displacement,  
        [EnumDescription(EnumValue = Unit.Tempature)]
        Tempature,
        [EnumDescription(EnumValue = Unit.Pressure)]
        Pressure,
        [EnumDescription(EnumValue = Unit.RPM)]
        RPM ,
        [EnumDescription(EnumValue = Unit.Unit)]
        Unit
    }
}

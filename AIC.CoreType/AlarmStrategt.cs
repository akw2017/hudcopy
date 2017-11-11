using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum AlarmStrategt
    {
        [EnumDescription(EnumValue = AlarmStrategt.Normal)]
        Normal=0,
        [EnumDescription(EnumValue = AlarmStrategt.Warning)]
        Warning,
        [EnumDescription(EnumValue = AlarmStrategt.Danger)]
        Danger,
    }
}

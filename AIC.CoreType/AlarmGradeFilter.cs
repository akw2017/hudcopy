using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum AlarmGradeFilter
    {
        [EnumDescription(EnumValue = AlarmGradeFilter.Normal)]
        Normal=0,
        [EnumDescription(EnumValue = AlarmGradeFilter.Warning)]
        Warning,
        [EnumDescription(EnumValue = AlarmGradeFilter.Danger)]
        Danger,
        [EnumDescription(EnumValue = AlarmGradeFilter.WarningAndDanger)]
        WarningAndDanger,
        [EnumDescription(EnumValue = AlarmGradeFilter.All)]
        All,
    }
}

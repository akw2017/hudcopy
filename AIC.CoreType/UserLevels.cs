using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;
using System.ComponentModel;

namespace AIC.CoreType
{
    public enum UserLevels
    {
        [EnumDescription(EnumValue = UserLevels.Administrator)]
        Administrator=0,
        [EnumDescription(EnumValue = UserLevels.Engineer)]
        Engineer,
    }
}

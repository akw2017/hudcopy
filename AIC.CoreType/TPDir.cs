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
    public enum TPDir
    {
        [EnumDescription(EnumValue = TPDir.X)]
        X=0,

        [EnumDescription(EnumValue = TPDir.Y)]
        Y,

        [EnumDescription(EnumValue = TPDir.Z)]
        Z
    }
}

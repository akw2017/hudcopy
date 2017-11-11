using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum nType
    {
        [EnumDescription(EnumValue = nType.Zero)]
        Zero = 0,
        [EnumDescription(EnumValue = nType.One)]
        One,
        [EnumDescription(EnumValue = nType.Two)]
        Two,
        [EnumDescription(EnumValue = nType.Three)]
        Three,
        [EnumDescription(EnumValue = nType.Four)]
        Four
    }
}

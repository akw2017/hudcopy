using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum ServerStatusType
    {
        [EnumDescription(EnumValue = ServerStatusType.Online)]
        Online = 0,
        [EnumDescription(EnumValue = ServerStatusType.Maintenance)]
        Maintenance,
    }
}

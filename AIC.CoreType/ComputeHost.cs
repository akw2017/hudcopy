using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum ComputeHost
    {
        [EnumDescription(EnumValue = ComputeHost.Software)]
        Software = 0,
        [EnumDescription(EnumValue = ComputeHost.Hardware)]
        Hardware,
    }
}

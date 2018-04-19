using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DiagnosticBaseModels
{
    public interface IMachComponent
    {
        Guid Guid { get; set; }
        string Name { get; set; }
        IMach Component { get; set; }
        DeviceComponentType ComponentType { get; }
    }
}

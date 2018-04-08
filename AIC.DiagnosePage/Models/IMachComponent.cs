﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DiagnosePage.Models
{
    public interface IMachComponent
    {
        Guid ID { get; set; }
        string Name { get; set; }
        IMach Component { get; set; }
        DeviceComponentType ComponentType { get; }
    }
}
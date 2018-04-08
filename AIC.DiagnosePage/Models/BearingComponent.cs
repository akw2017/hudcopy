//using NullGuard;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DiagnosePage.Models
{
    public class BearingComponent : IMachComponent
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public BearingClass Component { get; set; }

        IMach IMachComponent.Component
        {
            get
            {
                return Component;
            }
            set
            {
                Component = value as BearingClass;
            }
        }

        public DeviceComponentType ComponentType
        {
            get
            {
                return DeviceComponentType.Bearing;
            }
        }
    }
}

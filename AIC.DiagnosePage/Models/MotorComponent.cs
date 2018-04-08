//using NullGuard;
using Prism.Mvvm;
using System;

namespace AIC.DiagnosePage.Models
{
    public class MotorComponent : IMachComponent
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public MotorClass Component { get; set; }
        IMach IMachComponent.Component
        {
            get
            {
                return Component;
            }
            set
            {
                Component = value as MotorClass;
            }
        }

        public DeviceComponentType ComponentType
        {
            get
            {
                return DeviceComponentType.Motor;
            }
        }
    }
}

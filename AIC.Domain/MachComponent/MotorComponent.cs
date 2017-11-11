 //using NullGuard;
using System;

namespace AIC.Domain
{
    public class MotorComponent : IMachComponent
    {
        public Guid ID { get; set; }
        //[AllowNull]
        public string Name { get; set; }
        //[AllowNull]
        public Motor Component { get; set; }

        IMach IMachComponent.Component
        {
            get
            {
                return Component;
            }
            set
            {
                Component = value as Motor;
            }
        }
    }
}

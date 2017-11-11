//using NullGuard;
using System;

namespace AIC.Domain
{
    public class BearingComponent : IMachComponent
    {
        public Guid ID { get; set; }
        //[AllowNull]
        public string Name { get; set; }
        //[AllowNull]
        public Bearing Component { get; set; }

        IMach IMachComponent.Component
        {
            get
            {
                return Component;
            }
            set
            {
                Component = value as Bearing;
            }
        }
    }
}

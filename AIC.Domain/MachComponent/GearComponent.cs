 //using NullGuard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Domain
{
    public class GearComponent : IMachComponent
    {
        public Guid ID { get; set; }
        //[AllowNull]
        public string Name { get; set; }
        //[AllowNull]
        public Gear Component { get; set; }

        IMach IMachComponent.Component
        {
            get
            {
                return Component;
            }
            set
            {
                Component = value as Gear;
            }
        }
    //    public GearComponent Clone()
    //    {
    //        return new GearComponent()
    //        {
    //            ID = this.ID,
    //            Name = this.Name,
    //            Component = this.Component,
    //        };
    //    }

    //    IMachComponent IMachComponent.Clone()
    //    {
    //        return Clone();
    //    }
    }
}

//using NullGuard;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DiagnosePage.Models
{
    public class GearComponent : IMachComponent
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public GearClass Component { get; set; }
        IMach IMachComponent.Component
        {
            get
            {
                return Component;
            }
            set
            {
                Component = value as GearClass;
            }
        }

        public DeviceComponentType ComponentType
        {
            get
            {
                return DeviceComponentType.Gear;
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

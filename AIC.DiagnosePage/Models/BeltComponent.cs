
using Prism.Mvvm;
using System;

namespace AIC.DiagnosePage.Models
{
    public class BeltComponent : IMachComponent
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public BeltClass Component { get; set; }
        IMach IMachComponent.Component
        {
            get
            {
                return Component;
            }
            set
            {
                Component = value as BeltClass;
            }
        }

        public DeviceComponentType ComponentType
        {
            get
            {
                return DeviceComponentType.Belt;
            }
        }

        //public BeltComponent Clone()
        //{
        //    return new BeltComponent()
        //    {
        //        ID = this.ID,
        //        Name = this.Name,
        //        Component = this.Component.Clone(),
        //    };
        //}

        //IMachComponent IMachComponent.Clone()
        //{
        //    return Clone();
        //}
    }
}

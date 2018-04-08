//using NullGuard;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DiagnosePage.Models
{
    public class ImpellerComponent : IMachComponent
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public ImpellerClass Component { get; set; }
        IMach IMachComponent.Component
        {
            get
            {
                return Component;
            }
            set
            {
                Component = value as ImpellerClass;
            }
             
        }

        public DeviceComponentType ComponentType
        {
            get
            {
                return DeviceComponentType.Impeller;
            }
        }
        //public ImpellerComponent Clone()
        //{
        //    return new ImpellerComponent()
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

//using NullGuard;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DiagnosePage.Models
{
    public class ShaftComponent : BindableBase, IMachComponent
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public ShaftClass Component { get; set; }        
        IMach IMachComponent.Component
        {
            get
            {
                return Component;
            }
            set
            {
                Component = value as ShaftClass;
            }
        }

        public DeviceComponentType ComponentType
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        //public ShaftComponent Clone()
        //{
        //    return new ShaftComponent()
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

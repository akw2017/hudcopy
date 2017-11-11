 //using NullGuard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Domain
{
    public class ImpellerComponent : IMachComponent
    {
        public Guid ID { get; set; }
        //[AllowNull]
        public string Name { get; set; }
        //[AllowNull]
        public Impeller Component { get; set; }

        IMach IMachComponent.Component
        {
            get
            {
                return Component;
            }
            set
            {
                Component = value as Impeller;
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

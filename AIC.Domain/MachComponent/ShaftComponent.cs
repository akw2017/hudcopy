 //using NullGuard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Domain
{
    public class ShaftComponent : IMachComponent
    {
        public Guid ID { get; set; }
        //[AllowNull]
        public string Name { get; set; }
        //[AllowNull]
        public Shaft Component { get; set; }

        IMach IMachComponent.Component
        {
            get
            {
                return Component;
            }
            set
            {
                Component = value as Shaft;
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

//using NullGuard;
using AIC.CoreType;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  AIC.Core.DiagnosticBaseModels
{
    public class ShaftComponent : BindableBase, IMachComponent
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        private string name = "新建轴";
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public ShaftClass Component { get; set; } = new ShaftClass();       
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

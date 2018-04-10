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
    public class GearComponent :BindableBase, IMachComponent
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        private string name = "新建齿轮";
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public GearClass Component { get; set; } = new GearClass();
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

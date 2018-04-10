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
    public class ImpellerComponent :BindableBase, IMachComponent
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        private string name = "新建叶轮";
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public ImpellerClass Component { get; set; } = new ImpellerClass();
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

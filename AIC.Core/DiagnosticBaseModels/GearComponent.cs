//using NullGuard;
using AIC.CoreType;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DiagnosticBaseModels
{
    [Serializable]
    public class GearComponent : INotifyPropertyChanged, IMachComponent
    {
        public Guid Guid { get; set; } = Guid.NewGuid();

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

        private GearClass component = new GearClass();
        public GearClass Component
        {
            get { return component; }
            set
            {
                component = value;
                OnPropertyChanged("Component");
            }
        }

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

        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

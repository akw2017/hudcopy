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
    public class ImpellerComponent : INotifyPropertyChanged, IMachComponent
    {
        public Guid Guid { get; set; } = Guid.NewGuid();

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

        private ImpellerClass component = new ImpellerClass();
        public ImpellerClass Component
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

        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

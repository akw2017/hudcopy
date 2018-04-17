//using NullGuard;
using AIC.CoreType;
using Prism.Mvvm;
using System;
using System.ComponentModel;

namespace AIC.Core.DiagnosticBaseModels
{
    [Serializable]
    public class MotorComponent : INotifyPropertyChanged, IMachComponent
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        private string name = "新建电机";
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private MotorClass component = new MotorClass();
        public MotorClass Component
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
                Component = value as MotorClass;
            }
        }

        public DeviceComponentType ComponentType
        {
            get
            {
                return DeviceComponentType.Motor;
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
    }
}

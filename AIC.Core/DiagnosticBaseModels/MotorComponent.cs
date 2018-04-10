//using NullGuard;
using AIC.CoreType;
using Prism.Mvvm;
using System;

namespace  AIC.Core.DiagnosticBaseModels
{
    public class MotorComponent :BindableBase, IMachComponent
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
        public MotorClass Component { get; set; } = new MotorClass();
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
    }
}

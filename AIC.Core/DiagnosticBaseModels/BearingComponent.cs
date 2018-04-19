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
    public class BearingComponent : INotifyPropertyChanged, IMachComponent
    {
        public Guid Guid { get; set; } = Guid.NewGuid();

        private string name = "新建轴承";
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private BearingClass component = new BearingClass();
        public BearingClass Component
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
                Component = value as BearingClass;
            }
        }

        public DeviceComponentType ComponentType
        {
            get
            {
                return DeviceComponentType.Bearing;
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

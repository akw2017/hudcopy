
using AIC.CoreType;
using Prism.Mvvm;
using System;
using System.ComponentModel;

namespace AIC.Core.DiagnosticBaseModels
{
    [Serializable]
    public class BeltComponent : INotifyPropertyChanged, IMachComponent
    {
        public Guid Guid { get; set; } = Guid.NewGuid();

        private string name = "新建皮带";
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private BeltClass component = new BeltClass();
        public BeltClass Component
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
                Component = value as BeltClass;
            }
        }

        public DeviceComponentType ComponentType
        {
            get
            {
                return DeviceComponentType.Belt;
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

        //public BeltComponent Clone()
        //{
        //    return new BeltComponent()
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

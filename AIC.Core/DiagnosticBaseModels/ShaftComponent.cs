//using NullGuard;
using AIC.CoreType;
using AIC.M9600.Common.MasterDB.Generated;
using Newtonsoft.Json;
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
    public class ShaftComponent : INotifyPropertyChanged, IMachComponent
    {
        public long id { get; set; } = -1; //新增为-1
        public Guid Guid { get; set; } = Guid.NewGuid();

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

        private ShaftClass component = new ShaftClass();
        public ShaftClass Component
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
                Component = value as ShaftClass;
            }
        }

        [JsonIgnore]
        public DeviceComponentType ComponentType
        {
            get
            {
                throw new NotImplementedException();
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

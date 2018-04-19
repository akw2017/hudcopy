using AIC.Core.OrganizationModels;
using AIC.CoreType;
using AIC.M9600.Common.MasterDB.Generated;
using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DiagnosticBaseModels
{
    public class DeviceDiagnoseComponent : IMachComponent
    {
        public DeviceDiagnoseComponent()
        {

        }

        public DeviceDiagnoseComponent(IList<ItemInfo> items, Guid deviceguid, string devicename = "新建设备")
        {
            var shaftComponent = new ShaftComponent()
            {
                Component = new ShaftClass()               
            };
            shaftComponent.Component.AddBearingComponent(new BearingComponent());
            Component.UnAllotItems = new ObservableCollection<ItemInfo>(items);
            Component.AddShaftComponent(shaftComponent);
            Guid = deviceguid;
            Name = devicename;
        }

        public DeviceDiagnoseComponent(IList<ItemInfo> items, Guid deviceguid, string devicename, DeviceDiagnoseClass component): this(items, deviceguid, devicename)
        {
            if (component != null)
            {
                Component = component;
                Component.UnAllotItems = new ObservableCollection<ItemInfo>(items);
            }
        }

        public long id { get; set; } = -1; //新增为-1
        public Guid Guid { get; set; } = Guid.NewGuid();

        private string name = "新建设备";
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private DeviceDiagnoseClass component = new DeviceDiagnoseClass();
        public DeviceDiagnoseClass Component
        {
            get { return component; }
            set
            {
                component = value;
                OnPropertyChanged("Component");
            }
        }

        private ObservableCollection<ComponentNaturalFrequency> componentNaturalFrequency = new ObservableCollection<ComponentNaturalFrequency>();
        public ObservableCollection<ComponentNaturalFrequency> ComponentNaturalFrequency
        {
            get { return componentNaturalFrequency; }
            set
            {
                componentNaturalFrequency = value;
                OnPropertyChanged("ComponentNaturalFrequency");
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
                Component = value as DeviceDiagnoseClass;
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

        public static DeviceDiagnoseComponent ConvertFromDB(T_Diagnosis_Model t_model)
        {
            DeviceDiagnoseComponent model = new DeviceDiagnoseComponent();
            model.id = t_model.id;
            model.Name = t_model.Name;
            model.Guid = t_model.Guid;
            if (!string.IsNullOrWhiteSpace(t_model.Structure))
            {
                model.Component = JsonConvert.DeserializeObject<DeviceDiagnoseClass>(t_model.Structure);
            }
            else
            {
                model.Component = new DeviceDiagnoseClass();
            }
            //修复一些不进行json的字段
            foreach (var shaft in model.Component.Shafts)
            {
                shaft.Component.Parent = model.Component;
                shaft.Component.InitMachComponents();
            }
            return model;
        }

        public static T_Diagnosis_Model ConvertToDB(DeviceDiagnoseComponent model)
        {
            T_Diagnosis_Model t_model = new T_Diagnosis_Model();
            t_model.id = model.id;
            t_model.Name = model.Name;
            t_model.Guid = model.Guid;
            if (model.Component != null)
            {
                t_model.Structure = JsonConvert.SerializeObject(model.Component);//可能有问题
            }
            else
            {
                t_model.Structure = null;
            }

            return t_model;
        }
    }
}

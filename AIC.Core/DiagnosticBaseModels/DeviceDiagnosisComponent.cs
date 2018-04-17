using AIC.Core.OrganizationModels;
using AIC.CoreType;
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
    public class DeviceDiagnosisComponent : IMachComponent
    {
        public DeviceDiagnosisComponent(IList<ItemTreeItemViewModel> items, string devicename = "新建设备")
        {
            var shaftComponent = new ShaftComponent()
            {
                Component = new ShaftClass()
                {
                    MachComponents = new ObservableCollection<IMachComponent>()
                                {
                                    new BearingComponent(),
                                }
                },
            };
            Component.UnAllotItems = new ObservableCollection<ItemTreeItemViewModel>(items);
            Component.AddChild(shaftComponent);
            Name = devicename;
        }

        public DeviceDiagnosisComponent(IList<ItemTreeItemViewModel> items, string devicename, DeviceDiagnosisClass component): this(items, devicename)
        {
            Component = component;
            Component.UnAllotItems = new ObservableCollection<ItemTreeItemViewModel>(items);
        }
        public Guid ID { get; set; } = Guid.NewGuid();

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

        private DeviceDiagnosisClass component = new DeviceDiagnosisClass();
        public DeviceDiagnosisClass Component
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
                Component = value as DeviceDiagnosisClass;
            }
        }

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
    }
}

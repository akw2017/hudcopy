using AIC.Core.DiagnosticFilterModels;
using AIC.Core.OrganizationModels;
using AIC.CoreType;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AIC.Core.DiagnosticBaseModels
{
    [Serializable]
    public partial class ShaftClass : INotifyPropertyChanged, IMach
    {
        public ShaftClass()
        {
            RPMCoeff = 1;
            DefaultRPM = 6000;
            DeltaRPM = 100;
        }
        public int ID { get; set; } = -1;//新增为-1
        public Guid ShaftID { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        //是否为滑动轴承
        private bool isSlidingBearing;
        public bool IsSlidingBearing
        {
            get { return isSlidingBearing; }
            set
            {
                isSlidingBearing = value;
                OnPropertyChanged("IsSlidingBearing");
            }
        }

        //转速差
        private double deltaRPM;
        public double DeltaRPM
        {
            get { return deltaRPM; }
            set
            {
                deltaRPM = value;
                OnPropertyChanged("DeltaRPM");
            }
        }

        //默认转速
        private double defaultRPM;
        public double DefaultRPM
        {
            get { return defaultRPM; }
            set
            {
                defaultRPM = value;
                OnPropertyChanged("DefaultRPM");
            }
        }

        //转速系数，默认值为1
        private double rPMCoeff;
        public double RPMCoeff
        {
            get { return rPMCoeff; }
            set
            {
                rPMCoeff = value;
                OnPropertyChanged("RPMCoeff");
            }
        }

        public DeviceDiagnosisClass Parent { get; set; }

        private ObservableCollection<IMachComponent> machComponents = new ObservableCollection<IMachComponent>();
        public ObservableCollection<IMachComponent> MachComponents
        {
            get { return machComponents; }
            set
            {
                machComponents = value;
                OnPropertyChanged("MachComponents");
            }
        }

        private IMachComponent selectedComponent;
        public IMachComponent SelectedComponent
        {
            get { return selectedComponent; }
            set
            {
                if (selectedComponent != value)
                {
                    selectedComponent = value;
                    OnPropertyChanged("SelectedComponent");
                    if (this.Parent != null)
                    {
                        foreach (var shaft in this.Parent.Shafts)
                        {
                            if (shaft.Component.MachComponents.Contains(selectedComponent))
                            {
                                this.Parent.SelectedShaft = shaft;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private ObservableCollection<ItemTreeItemViewModel> allotItems = new ObservableCollection<ItemTreeItemViewModel>();
        public ObservableCollection<ItemTreeItemViewModel> AllotItems
        {
            get { return allotItems; }
            set
            {
                allotItems = value;
                OnPropertyChanged("AllotItems");
            }
        }

        private ItemTreeItemViewModel selectedItem;
        public ItemTreeItemViewModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                    if (this.Parent != null)
                    {
                        foreach (var shaft in this.Parent.Shafts)
                        {
                            if (shaft.Component.AllotItems.Contains(selectedItem))
                            {
                                this.Parent.SelectedShaft = shaft;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public ObservableCollection<NegationDivFreStrategy> NegationDivFreStrategies { get; set; } = new ObservableCollection<NegationDivFreStrategy>();
        public ObservableCollection<NaturalFre> NaturalFres { get; set; } = new ObservableCollection<NaturalFre>();
        public ObservableCollection<DivFreThresholdProportion> DivFreThresholdProportiones { get; set; } = new ObservableCollection<DivFreThresholdProportion>();

        private FilterType filterType;
        public FilterType FilterType
        {
            get
            {
                return filterType;
            }
            set
            {
                filterType = value;
                OnPropertyChanged("FilterType");
            }
        }
        public bool BindRPMForFilter { get; set; }
        public DgBandPassFilter DgBandPassFilter { get; set; } = new DgBandPassFilter();
        public DgHighPassFilter DgHighPassFilter { get; set; } = new DgHighPassFilter();
        public DgLowPassFilter DgLowPassFilter { get; set; } = new DgLowPassFilter();

        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public ShaftClass DeepClone()
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as ShaftClass;
            }
        }

        public ShaftClass ShallowClone()
        {
            return Clone() as ShaftClass;
        }
    }
}

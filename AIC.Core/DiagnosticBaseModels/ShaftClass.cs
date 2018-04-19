using AIC.Core.DiagnosticFilterModels;
using AIC.Core.OrganizationModels;
using AIC.CoreType;
using AIC.M9600.Common.MasterDB.Generated;
using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
        public long id { get; set; } = -1; //新增为-1
        public Guid Guid { get; set; } = Guid.NewGuid();

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

        [JsonIgnore]
        public DeviceDiagnoseClass Parent { get; set; }

        private ObservableCollection<IMachComponent> machComponents = new ObservableCollection<IMachComponent>();
        [JsonIgnore]
        public ObservableCollection<IMachComponent> MachComponents
        {
            get { return machComponents; }
            set
            {
                machComponents = value;
                OnPropertyChanged("MachComponents");
            }
        }

        public List<BearingComponent> BearingComponents { get; set; } = new List<BearingComponent>();
        public List<BeltComponent> BeltComponents { get; set; } = new List<BeltComponent>();
        public List<GearComponent> GearComponents { get; set; } = new List<GearComponent>();
        public List<MotorComponent> MotorComponents { get; set; } = new List<MotorComponent>();
        public List<ImpellerComponent> ImpellerComponents { get; set; } = new List<ImpellerComponent>();

        public void AddBearingComponent(BearingComponent child)
        {
            if (!BearingComponents.Contains(child))
            {
                BearingComponents.Add(child);
                MachComponents.Add(child);
            }
        }
        public void AddBeltComponent(BeltComponent child)
        {
            if (!BeltComponents.Contains(child))
            {
                BeltComponents.Add(child);
                MachComponents.Add(child);
            }
        }
        public void AddGearComponent(GearComponent child)
        {
            if (!GearComponents.Contains(child))
            {
                GearComponents.Add(child);
                MachComponents.Add(child);
            }
        }
        public void AddMotorComponent(MotorComponent child)
        {
            if (!MotorComponents.Contains(child))
            {
                MotorComponents.Add(child);
                MachComponents.Add(child);
            }
        }
        public void AddImpellerComponent(ImpellerComponent child)
        {
            if (!ImpellerComponents.Contains(child))
            {
                ImpellerComponents.Add(child);
                MachComponents.Add(child);
            }
        }

        public void RemoveBearingComponent(BearingComponent child)
        {
            if (BearingComponents.Contains(child))
            {
                BearingComponents.Remove(child);
                MachComponents.Remove(child);
            }
        }
        public void RemoveBeltComponent(BeltComponent child)
        {
            if (BeltComponents.Contains(child))
            {
                BeltComponents.Remove(child);
                MachComponents.Remove(child);
            }
        }
        public void RemoveGearComponent(GearComponent child)
        {
            if (GearComponents.Contains(child))
            {
                GearComponents.Remove(child);
                MachComponents.Remove(child);
            }
        }
        public void RemoveMotorComponent(MotorComponent child)
        {
            if (MotorComponents.Contains(child))
            {
                MotorComponents.Remove(child);
                MachComponents.Remove(child);
            }
        }
        public void RemoveImpellerComponent(ImpellerComponent child)
        {
            if (ImpellerComponents.Contains(child))
            {
                ImpellerComponents.Remove(child);
                MachComponents.Remove(child);
            }
        }

        public void InitMachComponents()
        {
            MachComponents.Clear();
            MachComponents.AddRange(BearingComponents);
            MachComponents.AddRange(BeltComponents);
            MachComponents.AddRange(GearComponents);
            MachComponents.AddRange(MotorComponents);
            MachComponents.AddRange(ImpellerComponents);
        }

        private IMachComponent selectedComponent;
        [JsonIgnore]
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

        private ObservableCollection<ItemInfo> allotItems = new ObservableCollection<ItemInfo>();
        public ObservableCollection<ItemInfo> AllotItems
        {
            get { return allotItems; }
            set
            {
                allotItems = value;
                OnPropertyChanged("AllotItems");
            }
        }

        private ItemInfo selectedItem;
        [JsonIgnore]
        public ItemInfo SelectedItem
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

        public static ShaftClass ConvertFromDB(T_Shaft t_shaft)
        {
            ShaftClass shaft = new ShaftClass();
            shaft.id = t_shaft.id;
            shaft.Name = t_shaft.Name;
            shaft.Guid = t_shaft.Guid ?? new Guid();
            shaft.IsSlidingBearing = t_shaft.IsSlidingBearing ?? false;
            shaft.DeltaRPM = t_shaft.DeltaRPM ?? 0;
            shaft.DefaultRPM = t_shaft.DefaultRPM ?? 0;
            shaft.RPMCoeff = t_shaft.RPMCoeff ?? 0;          
            if (!string.IsNullOrWhiteSpace(t_shaft.BearingComponentsJson))
            {
                var components = JsonConvert.DeserializeObject<List<BearingComponent>>(t_shaft.BearingComponentsJson);
                if (components != null)
                {
                    shaft.BearingComponents.AddRange(components);
                }
            }
            if (!string.IsNullOrWhiteSpace(t_shaft.BeltComponentsJson))
            {
                var components = JsonConvert.DeserializeObject<List<BeltComponent>>(t_shaft.BeltComponentsJson);
                if (components != null)
                {
                    shaft.BeltComponents.AddRange(components);
                }
            }
            if (!string.IsNullOrWhiteSpace(t_shaft.GearComponentsJson))
            {
                var components = JsonConvert.DeserializeObject<List<GearComponent>>(t_shaft.GearComponentsJson);
                if (components != null)
                {
                    shaft.GearComponents.AddRange(components);
                }
            }
            if (!string.IsNullOrWhiteSpace(t_shaft.MotorComponentsJson))
            {
                var components = JsonConvert.DeserializeObject<List<MotorComponent>>(t_shaft.MotorComponentsJson);
                if (components != null)
                {
                    shaft.MotorComponents.AddRange(components);
                }
            }
            if (!string.IsNullOrWhiteSpace(t_shaft.ImpellerComponentsJson))
            {
                var components = JsonConvert.DeserializeObject<List<ImpellerComponent>>(t_shaft.ImpellerComponentsJson);
                if (components != null)
                {
                    shaft.ImpellerComponents.AddRange(components);
                }
            }

            if (!string.IsNullOrWhiteSpace(t_shaft.AllotItemsJson))
            {
                shaft.AllotItems = JsonConvert.DeserializeObject<ObservableCollection<ItemInfo>>(t_shaft.AllotItemsJson);
            }
            if (!string.IsNullOrWhiteSpace(t_shaft.NegationDivFreStrategiesJson))
            {
                shaft.NegationDivFreStrategies = JsonConvert.DeserializeObject<ObservableCollection<NegationDivFreStrategy>>(t_shaft.NegationDivFreStrategiesJson);
            }
            if (!string.IsNullOrWhiteSpace(t_shaft.NaturalFresJson))
            {
                shaft.NaturalFres = JsonConvert.DeserializeObject<ObservableCollection<NaturalFre>>(t_shaft.NaturalFresJson);
            }
            if (!string.IsNullOrWhiteSpace(t_shaft.NaturalFresJson)) 
            {
                shaft.DivFreThresholdProportiones = JsonConvert.DeserializeObject<ObservableCollection<DivFreThresholdProportion>>(t_shaft.DivFreThresholdProportionesJson);
            }
            shaft.FilterType = (FilterType)(t_shaft.FilterType ?? 0);
            shaft.BindRPMForFilter = t_shaft.BindRPMForFilter ?? false;
            if (!string.IsNullOrWhiteSpace(t_shaft.DgBandPassFilterJson))
            {
                shaft.DgBandPassFilter = JsonConvert.DeserializeObject<DgBandPassFilter>(t_shaft.DgBandPassFilterJson);
            }
            if (!string.IsNullOrWhiteSpace(t_shaft.DgHighPassFilterJson))
            {
                shaft.DgHighPassFilter = JsonConvert.DeserializeObject<DgHighPassFilter>(t_shaft.DgHighPassFilterJson);
            }
            if (!string.IsNullOrWhiteSpace(t_shaft.DgLowPassFilterJson))
            {
                shaft.DgLowPassFilter = JsonConvert.DeserializeObject<DgLowPassFilter>(t_shaft.DgLowPassFilterJson);
            }

            //修复一些不进行json的字段
            shaft.InitMachComponents();
              
            return shaft;
        }

        public static T_Shaft ConvertToDB(ShaftClass shaft)
        {
            T_Shaft t_shaft = new T_Shaft();
            t_shaft.id = shaft.id;
            t_shaft.Name = shaft.Name;
            t_shaft.Guid = shaft.Guid;
            t_shaft.IsSlidingBearing = shaft.IsSlidingBearing;
            t_shaft.DeltaRPM = shaft.DeltaRPM;
            t_shaft.DefaultRPM = shaft.DefaultRPM;
            t_shaft.RPMCoeff = shaft.RPMCoeff;
            if (shaft.BearingComponents != null)
            {
                t_shaft.BearingComponentsJson = JsonConvert.SerializeObject(shaft.BearingComponents);
            }
            else
            {
                t_shaft.BearingComponentsJson = null;
            }
            if (shaft.BeltComponents != null)
            {
                t_shaft.BeltComponentsJson = JsonConvert.SerializeObject(shaft.BeltComponents);
            }
            else
            {
                t_shaft.BeltComponentsJson = null;
            }
            if (shaft.GearComponents != null)
            {
                t_shaft.GearComponentsJson = JsonConvert.SerializeObject(shaft.GearComponents);
            }
            else
            {
                t_shaft.GearComponentsJson = null;
            }
            if (shaft.MotorComponents != null)
            {
                t_shaft.MotorComponentsJson = JsonConvert.SerializeObject(shaft.MotorComponents);
            }
            else
            {
                t_shaft.MotorComponentsJson = null;
            }
            if (shaft.ImpellerComponents != null)
            {
                t_shaft.ImpellerComponentsJson = JsonConvert.SerializeObject(shaft.ImpellerComponents);
            }
            else
            {
                t_shaft.ImpellerComponentsJson = null;
            }
            if (shaft.AllotItems != null)
            {
                t_shaft.AllotItemsJson = JsonConvert.SerializeObject(shaft.AllotItems);
            }
            else
            {
                t_shaft.AllotItemsJson = null;
            }
            if (shaft.NegationDivFreStrategies != null)
            {
                t_shaft.NegationDivFreStrategiesJson = JsonConvert.SerializeObject(shaft.NegationDivFreStrategies);
            }
            else
            {
                t_shaft.NegationDivFreStrategiesJson = null;
            }
            if (shaft.NaturalFres != null)
            {
                t_shaft.NaturalFresJson = JsonConvert.SerializeObject(shaft.NaturalFres);
            }
            else
            {
                t_shaft.NaturalFresJson = null;
            }
            if (shaft.NaturalFres != null)
            {
                t_shaft.DivFreThresholdProportionesJson = JsonConvert.SerializeObject(shaft.DivFreThresholdProportiones);
            }
            else
            {
                t_shaft.DivFreThresholdProportionesJson = null;
            }
            t_shaft.FilterType = (int)(shaft.FilterType);
            t_shaft.BindRPMForFilter = shaft.BindRPMForFilter;
            if (shaft.DgBandPassFilter != null)
            {
                t_shaft.DgBandPassFilterJson = JsonConvert.SerializeObject(shaft.DgBandPassFilter);
            }
            else
            {
                t_shaft.DgBandPassFilterJson = null;
            }
            if (shaft.DgHighPassFilter != null)
            {
                t_shaft.DgHighPassFilterJson = JsonConvert.SerializeObject(shaft.DgHighPassFilter);
            }
            else
            {
                t_shaft.DgHighPassFilterJson = null;
            }
            if (shaft.DgLowPassFilter != null)
            {
                t_shaft.DgLowPassFilterJson = JsonConvert.SerializeObject(shaft.DgLowPassFilter);
            }
            else
            {
                t_shaft.DgLowPassFilterJson = null;
            }
            return t_shaft;
        }
    }
}

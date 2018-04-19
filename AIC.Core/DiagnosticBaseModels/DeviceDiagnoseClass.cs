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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AIC.Core.DiagnosticBaseModels
{
    [Serializable]
    public partial class DeviceDiagnoseClass : INotifyPropertyChanged, IMach, ICloneable
    {
        public DeviceDiagnoseClass()
        {
            HeadDivFreThreshold = 0.1;
            KurtosisIndexThreshold = 50;
            PeakIndexThreshold = 50;
            PulseIndexThreshold = 50;
            IsDeviceDiagnosis = true;
            FrePeakFilterInterval = 5.0;
            FreDiagnosisSetupInterval = 1;
        }
        public long id { get; set; } = -1; //新增为-1
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        private ObservableCollection<ShaftComponent> shafts = new ObservableCollection<ShaftComponent>();
        public ObservableCollection<ShaftComponent> Shafts
        {
            get { return shafts; }
            set
            {
                shafts = value;
                OnPropertyChanged("Shafts");
            }
        }

        public void InsertShaftComponent(int index, ShaftComponent child)
        {
            if (!Shafts.Contains(child))
            {
                child.Component.Parent = this;
                Shafts.Insert(index, child);
            }
        }

        public void AddShaftComponent(ShaftComponent child)
        {
            if (!Shafts.Contains(child))
            {
                child.Component.Parent = this;
                Shafts.Add(child);
            }
        }

        public void AddCShaftComponentdRange(IEnumerable<ShaftComponent> childs)
        {
            foreach (var child in childs)
            {
                if (!Shafts.Contains(child))
                {
                    child.Component.Parent = this;
                    Shafts.Add(child);
                }
            }
        }

        public void RemoveShaftComponent(ShaftComponent child)
        {
            if (Shafts.Contains(child))
            {
                child.Component.Parent = null;
                Shafts.Remove(child);
            }
        }

        public void ClearChild()
        {
            if (shafts != null)
            {
                shafts.Clear();
            }
        }

        private ShaftComponent selectedShaft;
        [JsonIgnore]
        public ShaftComponent SelectedShaft
        {
            get { return selectedShaft; }
            set
            {
                if (selectedShaft != value)
                {
                    selectedShaft = value;
                    OnPropertyChanged("SelectedShaft");
                    foreach (var shaft in shafts)
                    {
                        if (shaft != selectedShaft)
                        {
                            if (shaft.Component != null)
                            {
                                shaft.Component.SelectedComponent = null;
                                shaft.Component.SelectedItem = null;
                            }
                        }
                    }

                }
            }
        }

        //总分频门槛值，如DivFreThresholdProportionInfo.Threshold分频存在，忽略HeadDivFreThreshold，否则起作用。
        private double headDivFreThreshold;
        public double HeadDivFreThreshold
        {
            get { return headDivFreThreshold; }
            set
            {
                headDivFreThreshold = value;
                OnPropertyChanged("HeadDivFreThreshold");
            }
        }

        //=1为多个测点诊断一台设备；=0一个测点诊断一台设备。
        private bool isDeviceDiagnose;
        public bool IsDeviceDiagnosis
        {
            get { return isDeviceDiagnose; }
            set
            {
                isDeviceDiagnose = value;
                OnPropertyChanged("IsDeviceDiagnosis");
            }
        }

        //绝对平均值门槛值
        private double meanThreshold;
        public double MeanThreshold
        {
            get { return meanThreshold; }
            set
            {
                meanThreshold = value;
                OnPropertyChanged("MeanThreshold");
            }
        }

        //峭度指标阀值
        private double kurtosisIndexThreshold;
        public double KurtosisIndexThreshold
        {
            get { return kurtosisIndexThreshold; }
            set
            {
                kurtosisIndexThreshold = value;
                OnPropertyChanged("KurtosisIndexThreshold");
            }
        }

        //峰值门槛值
        private double peakThreshold;
        public double PeakThreshold
        {
            get { return peakThreshold; }
            set
            {
                peakThreshold = value;
                OnPropertyChanged("PeakThreshold");
            }
        }

        //脉冲指标阀值
        private double pulseIndexThreshold;
        public double PulseIndexThreshold
        {
            get { return pulseIndexThreshold; }
            set
            {
                pulseIndexThreshold = value;
                OnPropertyChanged("PulseIndexThreshold");
            }
        }

        //峰值指标阀值
        private double peakIndexThreshold;
        public double PeakIndexThreshold
        {
            get { return peakIndexThreshold; }
            set
            {
                peakIndexThreshold = value;
                OnPropertyChanged("PeakIndexThreshold");
            }
        }

        //有效值门槛值。
        private double rMSThreshold;
        public double RMSThreshold
        {
            get { return rMSThreshold; }
            set
            {
                rMSThreshold = value;
                OnPropertyChanged("RMSThreshold");
            }
        }

        //频谱峰值筛选间隔，默认值为5
        private double frePeakFilterInterval;
        public double FrePeakFilterInterval
        {
            get { return frePeakFilterInterval; }
            set
            {
                frePeakFilterInterval = value;
                OnPropertyChanged("FrePeakFilterInterval");
            }
        }

        //频率诊断设置间隔，默认值为1
        private double freDiagnosisSetupInterval;
        public double FreDiagnosisSetupInterval
        {
            get { return freDiagnosisSetupInterval; }
            set
            {
                freDiagnosisSetupInterval = value;
                OnPropertyChanged("FreDiagnosisSetupInterval");
            }
        }

        //是否显示故障概率
        private bool isFaultprobability;
        public bool IsFaultprobability
        {
            get { return isFaultprobability; }
            set
            {
                isFaultprobability = value;
                OnPropertyChanged("IsFaultprobability");
            }
        }

        private DiagnosisMethod diagnosisMethod;
        public DiagnosisMethod DiagnosisMethod
        {
            get { return diagnosisMethod; }
            set
            {
                diagnosisMethod = value;
                OnPropertyChanged("DiagnosisMethod");
            }
        }


        private ObservableCollection<ItemInfo> unAllotItems = new ObservableCollection<ItemInfo>();
        public ObservableCollection<ItemInfo> UnAllotItems
        {
            get { return unAllotItems; }
            set
            {
                unAllotItems = value;
                OnPropertyChanged("UnAllotItems");
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

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public DeviceDiagnoseClass DeepClone()
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as DeviceDiagnoseClass;
            }
        }

        public DeviceDiagnoseClass ShallowClone()
        {
            return Clone() as DeviceDiagnoseClass;
        }

        public static DeviceDiagnoseClass ConvertFromDB(T_DeviceDiagnose t_device)
        {
            DeviceDiagnoseClass device = new DeviceDiagnoseClass();
            device.id = t_device.id;
            device.Name = t_device.Name;
            device.Guid = t_device.Guid ?? new Guid();
            if (!string.IsNullOrWhiteSpace(t_device.ShaftsJson))
            {
                device.Shafts = JsonConvert.DeserializeObject<ObservableCollection<ShaftComponent>>(t_device.ShaftsJson);//可能有问题
                //修复一些不进行json的字段
                foreach (var shaft in device.Shafts)
                {
                    if (shaft.Component != null)
                    {
                        shaft.Component.Parent = device;
                        shaft.Component.InitMachComponents();
                    }
                }
            }
            else
            {
                device.Shafts = new ObservableCollection<ShaftComponent>();
            }
            device.HeadDivFreThreshold = t_device.HeadDivFreThreshold ?? 0;
            device.IsDeviceDiagnosis = t_device.IsDeviceDiagnosis ?? false;
            device.MeanThreshold = t_device.MeanThreshold ?? 0;
            device.KurtosisIndexThreshold = t_device.KurtosisIndexThreshold ?? 0;
            device.PeakThreshold = t_device.PeakThreshold ?? 0;
            device.PulseIndexThreshold = t_device.PulseIndexThreshold ?? 0;
            device.PeakIndexThreshold = t_device.PeakIndexThreshold ?? 0;
            device.RMSThreshold = t_device.RMSThreshold ?? 0;
            device.FrePeakFilterInterval = t_device.FrePeakFilterInterval ?? 0;
            device.FreDiagnosisSetupInterval = t_device.FreDiagnosisSetupInterval ?? 0;
            device.IsFaultprobability = t_device.IsFaultprobability ?? false;
            device.DiagnosisMethod = (DiagnosisMethod)(t_device.DiagnosisMethod ?? 0);
            if (!string.IsNullOrWhiteSpace(t_device.UnAllotItemsJson))
            {
                device.UnAllotItems = JsonConvert.DeserializeObject<ObservableCollection<ItemInfo>>(t_device.UnAllotItemsJson);
            }
            else
            {
                device.UnAllotItems = new ObservableCollection<ItemInfo>();
            }

            return device;
        }

        public static T_DeviceDiagnose ConvertToDB(DeviceDiagnoseClass device)
        {
            T_DeviceDiagnose t_device = new T_DeviceDiagnose();
            t_device.id = device.id;
            t_device.Name = device.Name;
            t_device.Guid = device.Guid;
            if (device.Shafts != null)
            {
                t_device.ShaftsJson = JsonConvert.SerializeObject(device.Shafts);//可能有问题
            }
            else
            {
                t_device.ShaftsJson = null;
            }
            t_device.HeadDivFreThreshold = device.HeadDivFreThreshold;
            t_device.IsDeviceDiagnosis = device.IsDeviceDiagnosis;
            t_device.MeanThreshold = device.MeanThreshold;
            t_device.KurtosisIndexThreshold = device.KurtosisIndexThreshold;
            t_device.PeakThreshold = device.PeakThreshold;
            t_device.PulseIndexThreshold = device.PulseIndexThreshold;
            t_device.PeakIndexThreshold = device.PeakIndexThreshold;
            t_device.RMSThreshold = device.RMSThreshold;
            t_device.FrePeakFilterInterval = device.FrePeakFilterInterval;
            t_device.FreDiagnosisSetupInterval = device.FreDiagnosisSetupInterval;
            t_device.IsFaultprobability = device.IsFaultprobability;
            t_device.DiagnosisMethod = (int)device.DiagnosisMethod;
            if (device.UnAllotItems != null)
            {
                t_device.UnAllotItemsJson = JsonConvert.SerializeObject(device.UnAllotItems);
            }
            else
            {
                t_device.UnAllotItemsJson = null;
            }

            return t_device;
        }
    }
}

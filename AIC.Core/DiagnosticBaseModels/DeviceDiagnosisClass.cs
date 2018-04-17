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
    public partial class DeviceDiagnosisClass : INotifyPropertyChanged, IMach, ICloneable
    {
        public DeviceDiagnosisClass()
        {
            HeadDivFreThreshold = 0.1;
            KurtosisIndexThreshold = 50;
            PeakIndexThreshold = 50;
            PulseIndexThreshold = 50;
            IsDeviceDiagnosis = true;
            FrePeakFilterInterval = 5.0;
            FreDiagnosisSetupInterval = 1;
        }
        public int ID { get; set; } = -1;//新增为-1
        public Guid DeviceID { get; set; }
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

        public void InsertChild(int index, ShaftComponent child)
        {
            if (!Shafts.Contains(child))
            {
                child.Component.Parent = this;
                Shafts.Insert(index, child);
            }
        }

        public void AddChild(ShaftComponent child)
        {
            if (!Shafts.Contains(child))
            {
                child.Component.Parent = this;
                Shafts.Add(child);
            }
        }

        public void AddChildRange(IEnumerable<ShaftComponent> childs)
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

        public void RemoveChild(ShaftComponent child)
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
        private bool isDeviceDiagnosis;
        public bool IsDeviceDiagnosis
        {
            get { return isDeviceDiagnosis; }
            set
            {
                isDeviceDiagnosis = value;
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


        private ObservableCollection<ItemTreeItemViewModel> unAllotItems = new ObservableCollection<ItemTreeItemViewModel>();
        public ObservableCollection<ItemTreeItemViewModel> UnAllotItems
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

        public DeviceDiagnosisClass DeepClone()
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as DeviceDiagnosisClass;
            }
        }

        public DeviceDiagnosisClass ShallowClone()
        {
            return Clone() as DeviceDiagnosisClass;
        }
    }
}

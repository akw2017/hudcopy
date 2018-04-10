using AIC.Core.OrganizationModels;
using AIC.CoreType;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AIC.Core.DiagnosticBaseModels
{
    public class DeviceDiagnosisClass : IMach
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
        public double HeadDivFreThreshold { get; set; }
        //=1为多个测点诊断一台设备；=0一个测点诊断一台设备。
        public bool IsDeviceDiagnosis { get; set; }
        //峭度指标阀值
        public double KurtosisIndexThreshold { get; set; }
        //脉冲指标阀值
        public double PulseIndexThreshold { get; set; }
        //峰值指标阀值
        public double PeakIndexThreshold { get; set; }
        //频谱峰值筛选间隔，默认值为5
        public double FrePeakFilterInterval { get; set; }
        //频率诊断设置间隔，默认值为1
        public double FreDiagnosisSetupInterval { get; set; }
        //是否显示故障概率
        public bool IsFaultprobability { get; set; }
        public DiagnosisMethod DiagnosisMethod { get; set; }


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

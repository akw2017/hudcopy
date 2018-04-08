using AIC.CoreType;
using AIC.DiagnosePage.FilterModels;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AIC.DiagnosePage.Models
{
    public class ShaftClass : BindableBase, IMach
    {
        public ShaftClass()
        {
            RPMCoeff = 1;
            DefaultRPM = 6000;
            DeltaRPM = 100;
        }
        public int ID { get; set; }
        public Guid ShaftID { get; set; }
        //[AllowNull]
        public string Name { get; set; }
        //是否为滑动轴承
        public bool IsSlidingBearing { get; set; }
        //转速差
        public double DeltaRPM { get; set; }
        //默认转速
        public double DefaultRPM { get; set; }
        //转速系数，默认值为1
        public double RPMCoeff { get; set; }

        public DeviceDiagnosisModel Parent { get; set; }

        private ObservableCollection<IMachComponent> machComponents;
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

        public List<NegationDivFreStrategy> NegationDivFreStrategies { get; set; }
        public List<NaturalFre> NaturalFres { get; set; }
        public List<DivFreThresholdProportion> DivFreThresholdProportiones { get; set; }
        public FilterType FilterType { get; set; }
        public bool BindRPMForFilter { get; set; }
        public BandPassFilterPara BandPassFilterPara { get; set; }
        public HighPassFilterPara HighPassFilterPara { get; set; }
        public LowPassFilterPara LowPassFilterPara { get; set; }
    }
}

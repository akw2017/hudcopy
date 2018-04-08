using AIC.DiagnosePage.FilterModels;
using AIC.DiagnosePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DiagnosePage.TestDatas
{
    public class ShaftClassExamples
    {
        public ShaftClass ShaftClass1 { get; set; }
        public ShaftClass ShaftClass2 { get; set; }

        public ShaftClassExamples()
        {       
            this.ShaftClass1 = new ShaftClass()
            {
                BandPassFilterPara = new BandPassFilterPara(),
                BindRPMForFilter = false,
                DefaultRPM = 6000,
                DeltaRPM = 100,
                DivFreThresholdProportiones = new List<Models.DivFreThresholdProportion>(),
                FilterType = CoreType.FilterType.BandPass,
                HighPassFilterPara = new FilterModels.HighPassFilterPara(),
                ID = 1,
                IsSlidingBearing = false,
                LowPassFilterPara = new FilterModels.LowPassFilterPara(),
                MachComponents = new System.Collections.ObjectModel.ObservableCollection<IMachComponent>()
                {
                    new BearingComponent() {Component = new BearClassExamples().BearingClass1, ID = Guid.NewGuid(), Name = "轴承1" },
                    new GearComponent() {Component = new GearClass() {TeethNumber = 3 }, ID = Guid.NewGuid(), Name = "齿轮1" },
                    new BeltComponent() {Component = new BeltClass() {BeltLength = 15, PulleyDiameter = 3 }, ID = Guid.NewGuid(), Name = "皮带1" },
                    new ImpellerComponent() {Component = new ImpellerClass() { NumberOfBlades = 2 }, ID = Guid.NewGuid(), Name = "叶轮1" },
                },
                Name = "轴1",
                NaturalFres = new List<NaturalFre>()
                {
                    new NaturalFre() {DivFre = CoreType.DivFreType.Custom, Fault="不平衡", Harm = "危害1", Mode = CoreType.NaturalFreMode.Additive, Proposal = "建议1", Value1 = 0, Value2 = 0 },
                },
                NegationDivFreStrategies = new List<NegationDivFreStrategy>(),
                RPMCoeff = 1,
                ShaftID = Guid.NewGuid(),
            };
            this.ShaftClass2 = new ShaftClass()
            {
                BandPassFilterPara = new BandPassFilterPara(),
                BindRPMForFilter = false,
                DefaultRPM = 6000,
                DeltaRPM = 100,
                DivFreThresholdProportiones = new List<Models.DivFreThresholdProportion>(),
                FilterType = CoreType.FilterType.BandPass,
                HighPassFilterPara = new FilterModels.HighPassFilterPara(),
                ID = 2,
                IsSlidingBearing = false,
                LowPassFilterPara = new FilterModels.LowPassFilterPara(),
                MachComponents = new System.Collections.ObjectModel.ObservableCollection<IMachComponent>()
                {
                    new BearingComponent() {Component = new BearClassExamples().BearingClass2, ID = Guid.NewGuid(), Name = "轴承2" },
                    new GearComponent() {Component = new GearClass() {TeethNumber = 23 }, ID = Guid.NewGuid(), Name = "齿轮2" },
                },
                Name = "轴2",
                NaturalFres = new List<NaturalFre>(),
                NegationDivFreStrategies = new List<NegationDivFreStrategy>(),
                RPMCoeff = 1,
                ShaftID = Guid.NewGuid(),
            };
        }

        public ShaftClass GetShaftClass1(DeviceDiagnosisModel device)
        {
            ShaftClass1.Parent = device;
            return ShaftClass1;
        }

        public ShaftClass GetShaftClass2(DeviceDiagnosisModel device)
        {
            ShaftClass2.Parent = device;
            return ShaftClass2;
        }
    }
}

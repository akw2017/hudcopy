﻿using AIC.Core.DiagnosticBaseModels;
using AIC.Core.DiagnosticFilterModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DatabaseService.TestDatas
{
    public static class ShaftClassExamples
    {
        public static ShaftClass ShaftClass1 { get; set; }
        public static ShaftClass ShaftClass2 { get; set; }
        public static List<ShaftClass> ShaftClassLib { get; set; } = new List<ShaftClass>();
        static ShaftClassExamples()
        {
            ShaftClass1 = new ShaftClass()
            {
                DgBandPassFilter = new DgBandPassFilter(),
                BindRPMForFilter = false,
                DefaultRPM = 6000,
                DeltaRPM = 100,
                DivFreThresholdProportiones = new ObservableCollection<DivFreThresholdProportion>(),
                FilterType = CoreType.FilterType.BandPass,
                DgHighPassFilter = new DgHighPassFilter(),
                IsSlidingBearing = false,
                DgLowPassFilter = new DgLowPassFilter(),
                BearingComponents = new List<BearingComponent>()
                {
                    new BearingComponent() {Component = BearingClassExamples.BearingClass1, Guid = Guid.NewGuid(), Name = "轴承1" },
                },
                GearComponents = new List<GearComponent>()
                {
                    new GearComponent() {Component = new GearClass() {TeethNumber = 3 }, Guid = Guid.NewGuid(), Name = "齿轮1" },
                },
                BeltComponents = new List<BeltComponent>()
                {
                    new BeltComponent() {Component = new BeltClass() {BeltLength = 15, PulleyDiameter = 3 }, Guid = Guid.NewGuid(), Name = "皮带1" },
                },
                ImpellerComponents = new List<ImpellerComponent>()
                {
                    new ImpellerComponent() {Component = new ImpellerClass() { NumberOfBlades = 2 }, Guid = Guid.NewGuid(), Name = "叶轮1" },
                },
                Name = "轴1",
                NaturalFres = new ObservableCollection<NaturalFre>()
                {
                    new NaturalFre() {DivFre = CoreType.DivFreType.Custom, Fault="不平衡", Harm = "危害1", Mode = CoreType.NaturalFreMode.Additive, Proposal = "建议1", Value1 = 1, Value2 = 0 },
                },
                NegationDivFreStrategies = new ObservableCollection<NegationDivFreStrategy>()
                {
                    new NegationDivFreStrategy() {Code = 0, Fault = "测试", RelativeX = 0.1, RelativeY = 0.1, RelativeZ = 0.1 }
                },
                RPMCoeff = 1,
                Guid = Guid.NewGuid(),
            };
            ShaftClass1.InitMachComponents();
            ShaftClass2 = new ShaftClass()
            {
                DgBandPassFilter = new DgBandPassFilter(),
                BindRPMForFilter = false,
                DefaultRPM = 6000,
                DeltaRPM = 100,
                DivFreThresholdProportiones = new ObservableCollection<DivFreThresholdProportion>(),
                FilterType = CoreType.FilterType.BandPass,
                DgHighPassFilter = new DgHighPassFilter(),
                IsSlidingBearing = false,
                DgLowPassFilter = new DgLowPassFilter(),               
                BearingComponents = new List<BearingComponent>()
                {
                     new BearingComponent() {Component = BearingClassExamples.BearingClass2, Guid = Guid.NewGuid(), Name = "轴承2" },
                },
                GearComponents = new List<GearComponent>()
                {
                     new GearComponent() {Component = new GearClass() {TeethNumber = 23 }, Guid = Guid.NewGuid(), Name = "齿轮2" },
                },
                Name = "轴2",
                NaturalFres = new ObservableCollection<NaturalFre>(),
                NegationDivFreStrategies = new ObservableCollection<NegationDivFreStrategy>(),
                RPMCoeff = 1,
                Guid = Guid.NewGuid(),
            };
            ShaftClass2.InitMachComponents();
            ShaftClassLib.Add(ShaftClass1);
            ShaftClassLib.Add(ShaftClass2);
        }
    }
}

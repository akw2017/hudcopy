using AIC.Core.DiagnosticBaseModels;
using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DatabaseService.TestDatas
{
    public static class DeviceClassExamples
    {
        public static DeviceDiagnoseClass DeviceClass1 { get; set; }
        public static DeviceDiagnoseClass DeviceClass2 { get; set; }

        public static List<DeviceDiagnoseClass> DeviceDiagnoseClassLib { get; set; } = new List<DeviceDiagnoseClass>();

        static DeviceClassExamples()
        {
            DeviceClass1 = new DeviceDiagnoseClass()
            {
                Guid = Guid.NewGuid(),
                DiagnosisMethod = DiagnosisMethod.FrequencyPeakValue,
                FreDiagnosisSetupInterval = 2,
                FrePeakFilterInterval = 5,
                HeadDivFreThreshold = 0.3,
                IsDeviceDiagnosis = true,
                IsFaultprobability = false,
                KurtosisIndexThreshold = 1,
                Name = "设备1",
                PeakIndexThreshold = 1,
                PulseIndexThreshold = 1,
                Shafts = new System.Collections.ObjectModel.ObservableCollection<ShaftComponent>(),                
            };
            DeviceClass1.AddShaftComponent(
                new ShaftComponent()
                {
                    Component = ShaftClassExamples.ShaftClass1.DeepClone(),
                    Guid = Guid.NewGuid(),
                    Name = "前轴",
                });
            DeviceClass1.AddShaftComponent(
                new ShaftComponent()
                {
                    Component = ShaftClassExamples.ShaftClass2.DeepClone(),
                    Guid = Guid.NewGuid(),
                    Name = "后轴",
                });
             DeviceClass1.AddShaftComponent(
                 new ShaftComponent()
                 {
                     Component = ShaftClassExamples.ShaftClass2.DeepClone(),
                     Guid = Guid.NewGuid(),
                     Name = "电机轴",
                 });
            DeviceClass2 = new DeviceDiagnoseClass()
            {
                Guid = Guid.NewGuid(),
                DiagnosisMethod = DiagnosisMethod.Energy,
                FreDiagnosisSetupInterval = 1,
                FrePeakFilterInterval = 5,
                HeadDivFreThreshold = 0.15,
                IsDeviceDiagnosis = true,
                IsFaultprobability = false,
                KurtosisIndexThreshold = 50,
                Name = "设备2",
                PeakIndexThreshold = 50,
                PulseIndexThreshold = 50,
                Shafts = new System.Collections.ObjectModel.ObservableCollection<ShaftComponent>()
                
            };
            DeviceClass2.AddShaftComponent(
                new ShaftComponent()
                {
                    Component = ShaftClassExamples.ShaftClass1.DeepClone(),
                    Guid = Guid.NewGuid(),
                    Name = "前轴",
                });
            DeviceDiagnoseClassLib.Add(DeviceClass1);
            DeviceDiagnoseClassLib.Add(DeviceClass2);
        }
    }
}

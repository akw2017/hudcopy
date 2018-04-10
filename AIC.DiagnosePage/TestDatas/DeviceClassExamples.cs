using AIC.Core.DiagnosticBaseModels;
using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DiagnosePage.TestDatas
{
    public static class DeviceClassExamples
    {
        public static DeviceDiagnosisClass DeviceClass1 { get; set; }
        public static DeviceDiagnosisClass DeviceClass2 { get; set; }

        public static List<DeviceDiagnosisClass> DeviceDiagnosisClassLib { get; set; } = new List<DeviceDiagnosisClass>();

        static DeviceClassExamples()
        {
            DeviceClass1 = new DeviceDiagnosisClass()
            {
                DeviceID = Guid.NewGuid(),
                DiagnosisMethod = DiagnosisMethod.FrequencyPeakValue,
                FreDiagnosisSetupInterval = 2,
                FrePeakFilterInterval = 5,
                HeadDivFreThreshold = 0.3,
                ID = 1,
                IsDeviceDiagnosis = true,
                IsFaultprobability = false,
                KurtosisIndexThreshold = 1,
                Name = "设备1",
                PeakIndexThreshold = 1,
                PulseIndexThreshold = 1,
                Shafts = new System.Collections.ObjectModel.ObservableCollection<ShaftComponent>(),                
            };
            DeviceClass1.AddChild(
                new ShaftComponent()
                {
                    Component = ShaftClassExamples.ShaftClass1,
                    ID = Guid.NewGuid(),
                    Name = "前轴",
                });
            DeviceClass1.AddChild(
                new ShaftComponent()
                {
                    Component = ShaftClassExamples.ShaftClass2,
                    ID = Guid.NewGuid(),
                    Name = "后轴",
                });
             DeviceClass1.AddChild(
                 new ShaftComponent()
                 {
                     Component = ShaftClassExamples.ShaftClass2,
                     ID = Guid.NewGuid(),
                     Name = "电机轴",
                 });
            DeviceClass2 = new DeviceDiagnosisClass()
            {
                DeviceID = Guid.NewGuid(),
                DiagnosisMethod = DiagnosisMethod.Energy,
                FreDiagnosisSetupInterval = 1,
                FrePeakFilterInterval = 5,
                HeadDivFreThreshold = 0.15,
                ID = 4,
                IsDeviceDiagnosis = true,
                IsFaultprobability = false,
                KurtosisIndexThreshold = 50,
                Name = "设备2",
                PeakIndexThreshold = 50,
                PulseIndexThreshold = 50,
                Shafts = new System.Collections.ObjectModel.ObservableCollection<ShaftComponent>()
                
            };
            DeviceClass2.AddChild(
                new ShaftComponent()
                {
                    Component = ShaftClassExamples.ShaftClass1,
                    ID = Guid.NewGuid(),
                    Name = "前轴",
                });
            DeviceDiagnosisClassLib.Add(DeviceClass1);
            DeviceDiagnosisClassLib.Add(DeviceClass2);
        }
    }
}

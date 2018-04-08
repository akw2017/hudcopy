using AIC.DiagnosePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DiagnosePage.TestDatas
{
    public class DeviceClassExamples
    {
        public DeviceDiagnosisModel DeviceClass1 { get; set; }
        public DeviceDiagnosisModel DeviceClass2 { get; set; }
        public DeviceClassExamples()
        {
            this.DeviceClass1 = new Models.DeviceDiagnosisModel()
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
            this.DeviceClass1.Shafts.Add(
                new Models.ShaftComponent()
                {
                    Component = new ShaftClassExamples().GetShaftClass1(this.DeviceClass1),
                    ID = Guid.NewGuid(),
                    Name = "前轴",
                });
            this.DeviceClass1.Shafts.Add(
                new Models.ShaftComponent()
                {
                    Component = new ShaftClassExamples().GetShaftClass2(this.DeviceClass1),
                    ID = Guid.NewGuid(),
                    Name = "后轴",
                });
             this.DeviceClass1.Shafts.Add(
                 new Models.ShaftComponent()
                 {
                     Component = new ShaftClassExamples().GetShaftClass2(this.DeviceClass1),
                     ID = Guid.NewGuid(),
                     Name = "电机轴",
                 });
            this.DeviceClass2 = new Models.DeviceDiagnosisModel()
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
            this.DeviceClass2.Shafts.Add(
                new Models.ShaftComponent()
                {
                    Component = new ShaftClassExamples().GetShaftClass1(this.DeviceClass2),
                    ID = Guid.NewGuid(),
                    Name = "前轴",
                });
        }

        public DeviceDiagnosisModel GetDeviceClass1(Object obj)
        {
            return DeviceClass1;
        }

        public DeviceDiagnosisModel GetDeviceClass2(Object obj)
        {
            return DeviceClass2;
        }
    }
}

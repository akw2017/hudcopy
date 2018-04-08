using AIC.DiagnosePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DiagnosePage.TestDatas
{
    public class BearClassExamples
    {
        public BearingClass BearingClass1 { get; set; }
        public BearingClass BearingClass2 { get; set; }
        public BearClassExamples()
        {
            this.BearingClass1 = new BearingClass()
            {
                BearingID = Guid.NewGuid(),
                BearingSeries = "滚动轴承",
                ContactAngle = 0,
                Designation = "RNNAF65856009080",
                ID=5001,
                InnerRingDiameter = 1,
                InnerRingFrequency = 15.8696,
                MaintainsFrequency = 0.470999985933304,
                NumberOfColumns = 2,
                NumberOfRoller = 30,
                OuterRingDiameter = 85,
                OuterRingFrequency = 14.1304,
                PitchDiameter = 69,
                RPM = 0,
                RollerDiameter = 4,
                RollerFrequency = 8.596,
            };
            this.BearingClass2 = new BearingClass()
            {
                BearingID = Guid.NewGuid(),
                BearingSeries = "滚动轴承",
                ContactAngle = 0,
                Designation = "RNNAF65856009080",
                ID = 5002,
                InnerRingDiameter = 1,
                InnerRingFrequency = 16.864,
                MaintainsFrequency = 0.472970008850098,
                NumberOfColumns = 2,
                NumberOfRoller = 32,
                OuterRingDiameter = 90,
                OuterRingFrequency = 15.135,
                PitchDiameter = 74,
                RPM = 0,
                RollerDiameter = 4,
                RollerFrequency = 9.2229,
            };
        }
    }
}

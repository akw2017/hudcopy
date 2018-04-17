using AIC.Core.DiagnosticBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DatabaseService.TestDatas
{
    public class MotorClassExamples
    {
        public static MotorClass MotorClass1 { get; set; }
        public static MotorClass MotorClass2 { get; set; }

        public static List<MotorClass> MotorClassLib { get; set; } = new List<MotorClass>();
        static MotorClassExamples()
        {
            MotorClass1 = new MotorClass()
            {
                Name = "测试电机1",
                MotorID = Guid.NewGuid(),
                LineFrequency = 50,
                Poles = 2,
                RotorBars = 3,
                StatorCoils = 50,
                WindingSlots = 2,
                SCRs = 3,
                MotorType = CoreType.MotorType.AC,
            };
            MotorClass2 = new MotorClass()
            {
                Name = "测试电机2",
                MotorID = Guid.NewGuid(),
                LineFrequency = 50,
                Poles = 4,
                RotorBars = 3,
                StatorCoils = 50,
                WindingSlots = 2,
                SCRs = 6,
                MotorType = CoreType.MotorType.AC,
            };
            MotorClassLib.Add(MotorClass1);
            MotorClassLib.Add(MotorClass2);
        }
    }
}

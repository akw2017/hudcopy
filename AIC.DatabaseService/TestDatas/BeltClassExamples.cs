using AIC.Core.DiagnosticBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DatabaseService.TestDatas
{
    public class BeltClassExamples
    {
        public static BeltClass BeltClass1 { get; set; }
        public static BeltClass BeltClass2 { get; set; }

        public static List<BeltClass> BeltClassLib { get; set; } = new List<BeltClass>();
        static BeltClassExamples()
        {
            BeltClass1 = new BeltClass()
            {
                Name = "测试皮带1",
                BeltID = Guid.NewGuid(),
                PulleyDiameter = 15,
                BeltLength = 3,              
            };
            BeltClass2 = new BeltClass()
            {
                Name = "测试皮带2",
                BeltID = Guid.NewGuid(),
                PulleyDiameter = 16,
                BeltLength = 3.5,
            };
            BeltClassLib.Add(BeltClass1);
            BeltClassLib.Add(BeltClass2);
        }
    }
}

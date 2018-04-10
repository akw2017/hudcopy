using AIC.Core.DiagnosticBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DiagnosePage.TestDatas
{
    public class GearClassExamples
    {
        public static GearClass GearClass1 { get; set; }
        public static GearClass GearClass2 { get; set; }

        public static List<GearClass> GearClassLib { get; set; } = new List<GearClass>();
        static GearClassExamples()
        {
            GearClass1 = new GearClass()
            {
                Name = "测试齿轮1",
                BeltID = Guid.NewGuid(),
                TeethNumber = 6,              
            };
            GearClass2 = new GearClass()
            {
                Name = "测试齿轮2",
                BeltID = Guid.NewGuid(),
                TeethNumber = 3,
            };
            GearClassLib.Add(GearClass1);
            GearClassLib.Add(GearClass2);
        }
    }
}

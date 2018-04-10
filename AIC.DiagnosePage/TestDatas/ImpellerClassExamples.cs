using AIC.Core.DiagnosticBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DiagnosePage.TestDatas
{
    public class ImpellerClassExamples
    {
        public static ImpellerClass ImpellerClass1 { get; set; }
        public static ImpellerClass ImpellerClass2 { get; set; }

        public static List<ImpellerClass> ImpellerClassLib { get; set; } = new List<ImpellerClass>();
        static ImpellerClassExamples()
        {
            ImpellerClass1 = new ImpellerClass()
            {
                Name = "测试叶轮1",
                ImpellerID = Guid.NewGuid(),
                NumberOfBlades = 15,              
            };
            ImpellerClass2 = new ImpellerClass()
            {
                Name = "测试叶轮2",
                ImpellerID = Guid.NewGuid(),
                NumberOfBlades = 16,
            };
            ImpellerClassLib.Add(ImpellerClass1);
            ImpellerClassLib.Add(ImpellerClass2);
        }
    }
}

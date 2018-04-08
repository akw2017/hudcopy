using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DiagnosePage.Models
{
    public class DivFreThresholdProportion
    {
        public DivFreType DivFre { get; set; }
        //故障名称
        public string Fault { get; set; }
        //DivFreType=0时表示倍频，如1；DivFreType=1时表示固定分频，如80；DivFreType=2时频率起始值，如40；
        public double Value1 { get; set; }
        //DivFreType=2时频率结束值，如80；其它两种分频值为此项忽略。
        public double Value2 { get; set; }
        //加权值，必须>=0
        public double Proportion { get; set; }
        //门槛值，>=0且<=100%
        public double Threshold { get; set; }
    }
}

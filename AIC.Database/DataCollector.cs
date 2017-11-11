
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIC.Database
{
    public class DataCollectorContract
    {
        public LMCommandParaTableContract LMCommand { get; set; }
        public LMHDParaTableContract LMHDPara { get; set; }
        public IEnumerable<LMVInfoTableContract> LMVInfos { get;set; } 
        public IEnumerable<LMAnInfoTableContract> LMAnalogIns { get;set; } 
    }
}

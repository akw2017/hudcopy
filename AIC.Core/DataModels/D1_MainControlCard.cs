using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DataModels
{
    public class D1_MainControlCard
    {
        public byte? ACQ_Unit_Type { get; set; } //数采器类型
        public byte? AsySyn { get; set; } //异步/同步
        public byte? MainCardCode { get; set; } //主板代码
        public byte? SynWaveCode { get; set; } //波形代码
        public bool? IsHdBypass { get; set; } //硬件旁路
        public bool? IsHdMultiplication { get; set; } //硬件倍增
    }
}

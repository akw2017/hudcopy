using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.CoreType
{
    public enum SignalPreProccessType
    {
        [Description("  ")]
        None = 0,
        [Description("包络")]
        Envelope,
        [Description("TFF")]
        TFF,
        [Description("倒频谱")]
        Cepstrum,
    }
}

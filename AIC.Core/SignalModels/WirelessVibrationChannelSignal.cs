using AIC.CoreType;
using AIC.M9600.Common.SlaveDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.SignalModels
{
    public class WirelessVibrationChannelSignal : BaseDivfreSignal
    {
        public WirelessVibrationChannelSignal(Guid guid) : base(guid)
        {          
        }

        protected override IEnumerable<SignalDisplayType> CreateSupportFunView()
        {
            var list = new List<SignalDisplayType>();
            list.Add(SignalDisplayType.AMSTrend);
            list.Add(SignalDisplayType.Value);
            list.Add(SignalDisplayType.TimeDomain);
            list.Add(SignalDisplayType.FrequencyDomain);
            list.Add(SignalDisplayType.PowerSpectrum);
            list.Add(SignalDisplayType.PowerSpectrumDensity);
            list.Add(SignalDisplayType.Time3DSpectrum);
            list.Add(SignalDisplayType.RPM3D);
            list.Add(SignalDisplayType.Bode);
            list.Add(SignalDisplayType.MultiDivFre);
            list.Add(SignalDisplayType.Nyquist);
            list.Add(SignalDisplayType.OrderAnalysis);
            list.Add(SignalDisplayType.Ortho);
            return list;
        }

        //public D_WirelessVibrationSlot D_WirelessVibrationSlot { get; set; }
    }
}

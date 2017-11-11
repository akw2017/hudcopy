using AIC.CoreType;
using AIC.M9600.Common.SlaveDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.SignalModels
{
    public class EddyCurrentKeyPhaseChannelSignal : BaseWaveSignal
    {
        public EddyCurrentKeyPhaseChannelSignal(Guid guid) : base(guid)
        {          
        }

        protected override IEnumerable<SignalDisplayType> CreateSupportFunView()
        {
            var list = new List<SignalDisplayType>();
            list.Add(SignalDisplayType.Value);
            list.Add(SignalDisplayType.AMSTrend);
            list.Add(SignalDisplayType.TimeDomain);
            return list;
        }

        //public D_EddyCurrentKeyPhaseSlot D_EddyCurrentKeyPhaseSlot { get; set; }
    }
}

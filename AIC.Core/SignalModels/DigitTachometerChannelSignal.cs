using AIC.CoreType;
using AIC.M9600.Common.SlaveDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.SignalModels
{
    public class DigitTachometerChannelSignal : BaseAlarmSignal
    {
        public DigitTachometerChannelSignal(Guid guid) : base(guid)
        {
           
        }

        protected override IEnumerable<SignalDisplayType> CreateSupportFunView()
        {
            var list = new List<SignalDisplayType>();
            list.Add(SignalDisplayType.RMSTrend);
            list.Add(SignalDisplayType.Value);
            return list;
        }

        //public D_DigitTachometerSlot D_DigitTachometerSlot { get; set; }
    }
}

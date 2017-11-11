using AIC.CoreType;
using AIC.M9600.Common.SlaveDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.SignalModels
{
    public class RelayChannelSignal : BaseAlarmSignal
    {
        public RelayChannelSignal(Guid guid) : base(guid)
        {
           
        }

        protected override IEnumerable<SignalDisplayType> CreateSupportFunView()
        {
            var list = new List<SignalDisplayType>();
            list.Add(SignalDisplayType.AMSTrend);
            list.Add(SignalDisplayType.Value);
            return list;
        }

        //public D_RelaySlot D_RelaySlot { get; set; }
    }
}

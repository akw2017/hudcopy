using AIC.CoreType;
using AIC.M9600.Common.SlaveDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.SignalModels
{
    public class EddyCurrentDisplacementChannelSignal : BaseDivfreSignal
    {
        public EddyCurrentDisplacementChannelSignal(Guid guid) : base(guid)
        {           
        }

        protected override IEnumerable<SignalDisplayType> CreateSupportFunView()
        {
            var list = new List<SignalDisplayType>();
            list.AddRange(Enum.GetValues(typeof(SignalDisplayType)).OfType<SignalDisplayType>());
            return list;
        }

        //public D_EddyCurrentDisplacementSlot D_EddyCurrentDisplacementSlot { get; set; }
        //public List<DivFreSignal> DivFreSignal { get; set; }
    }
}

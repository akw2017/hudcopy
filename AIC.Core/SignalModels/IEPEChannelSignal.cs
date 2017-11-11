using AIC.CoreType;
using AIC.M9600.Common.SlaveDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.SignalModels
{
    public class IEPEChannelSignal : BaseDivfreSignal
    {
        public IEPEChannelSignal(Guid guid) : base(guid)
        {                  
        }


        //#region 属性与字段  
        
        //public D_IEPESlot D_IEPESlot { get; set; }

        //public List<DivFreSignal> DivFreSignal { get; set; }      
        //#endregion
    }
}

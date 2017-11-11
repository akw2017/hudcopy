
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Database
{
    public class MasterContractContext
    {
        public MasterContractContext()
        {
            LMCommandParas = new ContractSet<LMCommandParaTableContract>();
        }
        public ContractSet<LMCommandParaTableContract> LMCommandParas { get; private set; }
    }
}

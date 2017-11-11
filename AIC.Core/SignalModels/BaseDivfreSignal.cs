using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.SignalModels
{
    public class BaseDivfreSignal : BaseWaveSignal
    {
        public BaseDivfreSignal(Guid guid):base(guid)
        {           
        }
      
        private ObservableCollection<DivFreSignal> divFreCollection = new ObservableCollection<DivFreSignal>();
        public IEnumerable<DivFreSignal> DivFres { get { return divFreCollection; } }

        public void RemoveDivFre(DivFreSignal divfre)
        {
            if (divFreCollection.Contains(divfre))
            {
                divFreCollection.Remove(divfre);
            }
        }

        public void AddDivFre(DivFreSignal divfre)
        {
            if (!divFreCollection.Contains(divfre))
            {
                divFreCollection.Add(divfre);
            }
        }
    }
}

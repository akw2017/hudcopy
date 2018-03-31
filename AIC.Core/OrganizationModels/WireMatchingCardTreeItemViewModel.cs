using AIC.Core.HardwareModels;
using AIC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.OrganizationModels
{
    public class WireMatchingCardTreeItemViewModel : ServerTreeItemViewModel
    {
        public WireMatchingCard WireMatchingCard { get; set; }

        public string MainControlCardIP
        {
            get
            {
                if (Parent != null && Parent is MainCardTreeItemViewModel)
                {
                    return (Parent as MainCardTreeItemViewModel).MainControlCardIP;
                }
                else
                {
                    return null;
                }
            }
        }
        public int CardNum { get; set; }
        
        public WireMatchingCardTreeItemViewModel(WireMatchingCard wireMatchingCard)
        {
            Name = wireMatchingCard.CardName;
            WireMatchingCard = wireMatchingCard;
        }
    }
}

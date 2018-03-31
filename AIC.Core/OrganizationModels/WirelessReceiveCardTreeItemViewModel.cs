using AIC.Core.HardwareModels;
using AIC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.OrganizationModels
{
    public class WirelessReceiveCardTreeItemViewModel : ServerTreeItemViewModel
    {
        public WirelessReceiveCard WirelessReceiveCard { get; set; }

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

        public string MasterIdentifier { get; set; }        

        public WirelessReceiveCardTreeItemViewModel(WirelessReceiveCard wirelessReceiveCard)
        {
            if (wirelessReceiveCard != null)
            {
                MasterIdentifier = wirelessReceiveCard.MasterIdentifier;
                Name = wirelessReceiveCard.ReceiveCardName;
            }
            else
            {
                MasterIdentifier = "";
            }
            WirelessReceiveCard = wirelessReceiveCard;
        }
    }
}

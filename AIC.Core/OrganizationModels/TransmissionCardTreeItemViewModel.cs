using AIC.Core.HardwareModels;
using AIC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.OrganizationModels
{
    public class TransmissionCardTreeItemViewModel : ServerTreeItemViewModel
    {
        public TransmissionCard TransmissionCard { get; set; }

        public string MainControlCardIP
        {
            get
            {
                if (Parent != null && Parent is WirelessReceiveCardTreeItemViewModel)
                {
                    return (Parent as WirelessReceiveCardTreeItemViewModel).MainControlCardIP;
                }
                else
                {
                    return null;
                }
            }
        }
        public string MasterIdentifier
        {
            get
            {
                if (Parent != null && Parent is WirelessReceiveCardTreeItemViewModel)
                {
                    return (Parent as WirelessReceiveCardTreeItemViewModel).MasterIdentifier;
                }
                else
                {
                    return null;
                }
            }
        }
        public string SlaveIdentifier { get; set; }

        public TransmissionCardTreeItemViewModel(TransmissionCard transmissionCard)
        {
            if (transmissionCard != null)
            {
                SlaveIdentifier = transmissionCard.SlaveIdentifier;
                Name = transmissionCard.TransmissionName;
            }
            else
            {
                SlaveIdentifier = "";
            }
            TransmissionCard = transmissionCard;
        }
    }
}

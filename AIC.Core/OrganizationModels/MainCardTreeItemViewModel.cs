using AIC.Core.HardwareModels;
using AIC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.OrganizationModels
{
    public class MainCardTreeItemViewModel : ServerTreeItemViewModel
    {
        public string MainControlCardIP { get; set; } 
        public MainControlCard MainControlCard { get; set; }
        public List<WireMatchingCard> WireMatchingCard { get; set; }
        public WirelessReceiveCard WirelessReceiveCard { get; set; }

        public MainCardTreeItemViewModel(string ip)
        {
            MainControlCardIP = ip;
        }       
    }
}

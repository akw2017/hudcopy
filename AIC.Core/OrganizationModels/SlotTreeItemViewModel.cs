using AIC.Core.HardwareModels;
using AIC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.OrganizationModels
{
    public class SlotTreeItemViewModel : ServerTreeItemViewModel
    {
        public ISlot ISlot { get; set; }

        public string MainControlCardIP
        {
            get
            {
                if (Parent != null && Parent is WireMatchingCardTreeItemViewModel)
                {
                    return (Parent as WireMatchingCardTreeItemViewModel).MainControlCardIP;
                }
                else if (Parent != null && Parent is TransmissionCardTreeItemViewModel)
                {
                    return (Parent as TransmissionCardTreeItemViewModel).MainControlCardIP;
                }
                else
                {
                    return null;
                }
            }
        }
        public int CardNum
        {
            get
            {
                if (Parent != null && Parent is WireMatchingCardTreeItemViewModel)
                {
                    return (Parent as WireMatchingCardTreeItemViewModel).CardNum;
                }
                else
                {
                    return -1;
                }
            }
        }
        public string SlaveIdentifier
        {
            get
            {
                if (Parent != null && Parent is TransmissionCardTreeItemViewModel)
                {
                    return (Parent as TransmissionCardTreeItemViewModel).SlaveIdentifier;
                }
                else
                {
                    return null;
                }
            }
        }

        public int SlotNum { get; set; }
      
        public SlotTreeItemViewModel(ISlot i_slot)
        {
            ISlot = i_slot;
            if (ISlot is IWireSlot)
            {
                Name = (i_slot as IWireSlot).SlotName;
                SlotNum = (i_slot as IWireSlot).SlotNum;
            }
            else if (ISlot is IWirelessSlot)
            {               
                SlotNum = (i_slot as IWirelessSlot).SlotNum;
            }                 
        }
    }
}

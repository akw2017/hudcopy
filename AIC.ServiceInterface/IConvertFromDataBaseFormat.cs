using AIC.Core.LMModels;
using AIC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.ServiceInterface
{
    public interface IConvertFromDataBaseFormat
    {
        MainControlCard MainControlCardConvert(T1_MainControlCard t_card);
        WireMatchingCard WireMatchingCardConvert(T1_WireMatchingCard t_card);
        WirelessReceiveCard WirelessReceiveCardConvert(T1_WirelessReceiveCard t_card);
        TransmissionCard TransmissionCardConvert(T1_TransmissionCard t_card);
        ISlot BaseSlotConvert(I_BaseSlot t_slot);
        IChannel BaseChannelConvert(I_BaseChannelInfo t_channel);
        AbstractSlotInfo AbstractSlotInfoConvert(T1_AbstractSlotInfo t_slot);
        void AbstractSlotInfoConvert(AbstractSlotInfo slot, T1_AbstractSlotInfo t_slot);
        AbstractChannelInfo AbstractChannelInfoConvert(T1_AbstractChannelInfo t_channel);
        void AbstractChannelInfoConvert(AbstractChannelInfo channel, T1_AbstractChannelInfo t_channel);
        DivFreInfo DivFreInfoConvert(T1_DivFreInfo t_divfreInfo);
    }
}

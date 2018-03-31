using AIC.Core.HardwareModels;
using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.ServiceInterface
{
    public interface IConvertToDataBaseFormat
    {
        T1_MainControlCard MainControlCardConvert(MainControlCard card, string IP);
        T1_WireMatchingCard WireMatchingCardConvert(WireMatchingCard card, string IP);
        T1_WirelessReceiveCard WirelessReceiveCardConvert(WirelessReceiveCard card, string IP);
        T1_TransmissionCard TransmissionCardConvert(TransmissionCard card, string IP, string masterIdentifier);
        I_BaseSlot BaseSlotConvert(IWireSlot i_slot, string IP, int cardNum);
        I_BaseSlot BaseSlotConvert(IWirelessSlot i_slot, string IP, string slaveIdentifier);
        I_BaseChannelInfo BaseChannelConvert(IChannel i_channel, string IP, int cardNum, int slotNum);
        I_BaseChannelInfo BaseChannelConvert(IChannel i_channel, string IP, string slaveIdentifier, int slotNum);
        T1_AbstractSlotInfo AbstractSlotInfoConvert(IWireSlot i_slot, string IP, int cardNum);
        T1_AbstractChannelInfo AbstractChannelInfoConvert(IChannel i_channel, string IP, int cardNum, int slotNum);
        T1_AbstractChannelInfo AbstractChannelInfoConvert(IChannel i_channel, string IP, string slaveIdentifier, int slotNum);
        T1_AbstractChannelInfo AbstractChannelInfoConvert(IChannel i_channel, string IP, int cardNum, int slotNum, Organization organization);
        T1_DivFreInfo DivFreInfoConvert(DivFreInfo divfreInfo, string IP, int cardNum, int slotNum, int chNum);
        T1_DivFreInfo DivFreInfoConvert(DivFreInfo divfreInfo, string IP, string slaveIdentifier, int slotNum, int chNum);
        T1_DivFreInfo DivFreInfoConvert(DivFreInfo divfreInfo, ItemTreeItemViewModel item);
        T1_Item ItemConvert(ItemTreeItemViewModel item, string serverIP, string IP, string identifier, int cardNum, int slotNum, int chNum);
        T1_Item ItemConvert(ItemTreeItemViewModel item, Organization parent_organization);
        T1_Item ItemConvert(ItemTreeItemViewModel item);
        T1_Organization OrganizationConvert(OrganizationTreeItemViewModel item, OrganizationTreeItemViewModel parent);

        void MainControlCardTempConvert(MainControlCard card, MainControlCard targetcard);
        void WireMatchingCardTempConvert(WireMatchingCard card, WireMatchingCard targetcard);
        void WirelessReceiveCardTempConvert(WirelessReceiveCard card, WirelessReceiveCard targetcard);
        void TransmissionCardTempConvert(TransmissionCard card, TransmissionCard targetcard);
        void SlotTempConvert(ISlot i_slot, ISlot i_targetslot);
        void ChannelTempConvert(IChannel i_channel, IChannel i_targetchannel);
        void DivFreInfoTempConvert(DivFreInfo divfreInfo, DivFreInfo targetdivfreInfo);

    }
}

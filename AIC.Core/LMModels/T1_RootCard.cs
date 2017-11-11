using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public class T1_RootCard
    {
        public List<T1_MainControlCard> T_MainControlCard { get; set; }
        public List<T1_WireMatchingCard> T_WireMatchingCard { get; set; }
        public List<T1_WirelessReceiveCard> T_WirelessReceiveCard { get; set; }
        public List<T1_TransmissionCard> T_TransmissionCard { get; set; }
        public List<T1_AbstractChannelInfo> T_AbstractChannelInfo { get; set; }
        public List<T1_IEPEChannelInfo> T_IEPEChannelInfo { get; set; }
        public List<T1_EddyCurrentDisplacementChannelInfo> T_EddyCurrentDisplacementChannelInfo { get; set; }
        public List<T1_EddyCurrentKeyPhaseChannelInfo> T_EddyCurrentKeyPhaseChannelInfo { get; set; }
        public List<T1_EddyCurrentTachometerChannelInfo> T_EddyCurrentTachometerChannelInfo { get; set; }
        public List<T1_DigitTachometerChannelInfo> T_DigitTachometerChannelInfo { get; set; }
        public List<T1_AnalogRransducerInChannelInfo> T_AnalogRransducerInChannelInfo { get; set; }
        public List<T1_RelayChannelInfo> T_RelayChannelInfo { get; set; }
        public List<T1_DigitRransducerInChannelInfo> T_DigitRransducerInChannelInfo { get; set; }
        public List<T1_DigitRransducerOutChannelInfo> T_DigitRransducerOutChannelInfo { get; set; }
        public List<T1_AnalogRransducerOutChannelInfo> T_AnalogRransducerOutChannelInfo { get; set; }
        public List<T1_WirelessScalarChannelInfo> T_WirelessScalarChannelInfo { get; set; }
        public List<T1_WirelessVibrationChannelInfo>T_WirelessVibrationChannelInfo { get; set; }
        public List<T1_AbstractSlotInfo> T_AbstractSlotInfo { get; set; }
        public List<T1_IEPESlot> T_IEPESlot { get; set; }
        public List<T1_EddyCurrentDisplacementSlot> T_EddyCurrentDisplacementSlot { get; set; }
        public List<T1_EddyCurrentKeyPhaseSlot> T_EddyCurrentKeyPhaseSlot { get; set; }
        public List<T1_EddyCurrentTachometerSlot> T_EddyCurrentTachometerSlot { get; set; }       
        public List<T1_DigitTachometerSlot> T_DigitTachometerSlot { get; set; }
        public List<T1_AnalogRransducerInSlot> T_AnalogRransducerInSlot { get; set; }
        public List<T1_RelaySlot> T_RelaySlot { get; set; }
        public List<T1_DigitRransducerInSlot> T_DigitRransducerInSlot { get; set; }
        public List<T1_DigitRransducerOutSlot> T_DigitRransducerOutSlot { get; set; }
        public List<T1_AnalogRransducerOutSlot> T_AnalogRransducerOutSlot { get; set; }
        public List<T1_WirelessScalarSlot> T_WirelessScalarSlot { get; set; }
        public List<T1_WirelessVibrationSlot> T_WirelessVibrationSlot { get; set; }
        public List<T1_DivFreInfo> T_DivFreInfo { get; set; }

        public T1_RootCard()
        {
            T_MainControlCard = new List<T1_MainControlCard>();
            T_WireMatchingCard = new List<T1_WireMatchingCard>();
            T_AbstractSlotInfo = new List<T1_AbstractSlotInfo>();
            T_WirelessReceiveCard = new List<T1_WirelessReceiveCard>();
            T_TransmissionCard = new List<T1_TransmissionCard>();
            T_IEPESlot = new List<T1_IEPESlot>();
            T_EddyCurrentDisplacementSlot = new List<T1_EddyCurrentDisplacementSlot>();
            T_EddyCurrentKeyPhaseSlot = new List<T1_EddyCurrentKeyPhaseSlot>();
            T_EddyCurrentTachometerSlot = new List<T1_EddyCurrentTachometerSlot>();
            T_DigitTachometerSlot = new List<T1_DigitTachometerSlot>();
            T_AnalogRransducerInSlot = new List<T1_AnalogRransducerInSlot>();
            T_RelaySlot = new List<T1_RelaySlot>();
            T_DigitRransducerInSlot = new List<T1_DigitRransducerInSlot>();
            T_DigitRransducerOutSlot = new List<T1_DigitRransducerOutSlot>();
            T_AnalogRransducerOutSlot = new List<T1_AnalogRransducerOutSlot>();
            T_WirelessScalarSlot = new List<T1_WirelessScalarSlot>();
            T_WirelessVibrationSlot = new List<T1_WirelessVibrationSlot>();
            T_AbstractChannelInfo = new List<T1_AbstractChannelInfo>();
            T_IEPEChannelInfo = new List<T1_IEPEChannelInfo>();
            T_EddyCurrentDisplacementChannelInfo = new List<T1_EddyCurrentDisplacementChannelInfo>();
            T_EddyCurrentKeyPhaseChannelInfo = new List<T1_EddyCurrentKeyPhaseChannelInfo>();
            T_EddyCurrentTachometerChannelInfo = new List<T1_EddyCurrentTachometerChannelInfo>();
            T_DigitTachometerChannelInfo = new List<T1_DigitTachometerChannelInfo>();
            T_AnalogRransducerInChannelInfo = new List<T1_AnalogRransducerInChannelInfo>();
            T_RelayChannelInfo = new List<T1_RelayChannelInfo>();
            T_DigitRransducerInChannelInfo = new List<T1_DigitRransducerInChannelInfo>();
            T_DigitRransducerOutChannelInfo = new List<T1_DigitRransducerOutChannelInfo>();
            T_AnalogRransducerOutChannelInfo = new List<T1_AnalogRransducerOutChannelInfo>();
            T_WirelessScalarChannelInfo = new List<T1_WirelessScalarChannelInfo>();
            T_WirelessVibrationChannelInfo = new List<T1_WirelessVibrationChannelInfo>();
            T_DivFreInfo = new List<T1_DivFreInfo>();
        }
    }
}

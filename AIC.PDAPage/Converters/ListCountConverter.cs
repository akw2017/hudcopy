using AIC.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AIC.PDAPage.Converters
{
    public class ListCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IList list = value as IList;
            if (list != null)
            {
                string para = parameter as string;
                if (para != null && para == "-1")
                {
                    return list.Count -1;
                }
                else
                {
                    return list.Count;
                }
            }

            int SlotNum = 0;
            WireMatchingCard card = value as WireMatchingCard;
            if (card != null )
            {
                SlotNum += (card.IEPESlot != null) ? 1 : 0;
                SlotNum += (card.EddyCurrentDisplacementSlot != null) ? 1 : 0;
                SlotNum += (card.EddyCurrentKeyPhaseSlot != null) ? 1 : 0;
                SlotNum += (card.EddyCurrentTachometerSlot != null) ? 1 : 0;
                SlotNum += (card.DigitTachometerSlot != null) ? 1 : 0;
                SlotNum += (card.AnalogRransducerInSlot != null) ? 1 : 0;
                SlotNum += (card.RelaySlot != null) ? 1 : 0;
                SlotNum += (card.DigitRransducerInSlot != null) ? 1 : 0;
                SlotNum += (card.DigitRransducerOutSlot != null) ? 1 : 0;
                SlotNum += (card.AnalogRransducerOutSlot != null) ? 1 : 0;
                return SlotNum;
            }
            TransmissionCard transmissionCard = value as TransmissionCard;
            if (transmissionCard != null)
            {
                SlotNum += (transmissionCard.WirelessScalarSlot != null) ? 1 : 0;
                SlotNum += (transmissionCard.WirelessVibrationSlot != null) ? 1 : 0;               
                return SlotNum;
            }

            if (value is IEPESlot && (value as IEPESlot).IEPEChannelInfo != null)
            {
                return (value as IEPESlot).IEPEChannelInfo.Count;
            }
            if (value is EddyCurrentDisplacementSlot && (value as EddyCurrentDisplacementSlot).EddyCurrentDisplacementChannelInfo != null)
            {
                return (value as EddyCurrentDisplacementSlot).EddyCurrentDisplacementChannelInfo.Count;
            }
            if (value is EddyCurrentKeyPhaseSlot && (value as EddyCurrentKeyPhaseSlot).EddyCurrentKeyPhaseChannelInfo != null)
            {
                return (value as EddyCurrentKeyPhaseSlot).EddyCurrentKeyPhaseChannelInfo.Count;
            }
            if (value is EddyCurrentTachometerSlot && (value as EddyCurrentTachometerSlot).EddyCurrentTachometerChannelInfo != null)
            {
                return (value as EddyCurrentTachometerSlot).EddyCurrentTachometerChannelInfo.Count;
            }
            if (value is DigitTachometerSlot && (value as DigitTachometerSlot).DigitTachometerChannelInfo != null)
            {
                return (value as DigitTachometerSlot).DigitTachometerChannelInfo.Count;
            }
            if (value is AnalogRransducerInSlot && (value as AnalogRransducerInSlot).AnalogRransducerInChannelInfo != null)
            {
                return (value as AnalogRransducerInSlot).AnalogRransducerInChannelInfo.Count;
            }
            if (value is RelaySlot && (value as RelaySlot).RelayChannelInfo != null)
            {
                return (value as RelaySlot).RelayChannelInfo.Count;
            }
            if (value is DigitRransducerInSlot && (value as DigitRransducerInSlot).DigitRransducerInChannelInfo != null)
            {
                return (value as DigitRransducerInSlot).DigitRransducerInChannelInfo.Count;
            }
            if (value is DigitRransducerOutSlot && (value as DigitRransducerOutSlot).DigitRransducerOutChannelInfo != null)
            {
                return (value as DigitRransducerOutSlot).DigitRransducerOutChannelInfo.Count;
            }
            if (value is AnalogRransducerOutSlot && (value as AnalogRransducerOutSlot).AnalogRransducerOutChannelInfo != null)
            {
                return (value as AnalogRransducerOutSlot).AnalogRransducerOutChannelInfo.Count;
            }
            if (value is WirelessScalarSlot && (value as WirelessScalarSlot).WirelessScalarChannelInfo != null)
            {
                return (value as WirelessScalarSlot).WirelessScalarChannelInfo.Count;
            }
            if (value is WirelessVibrationSlot && (value as WirelessVibrationSlot).WirelessVibrationChannelInfo != null)
            {
                return (value as WirelessVibrationSlot).WirelessVibrationChannelInfo.Count;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

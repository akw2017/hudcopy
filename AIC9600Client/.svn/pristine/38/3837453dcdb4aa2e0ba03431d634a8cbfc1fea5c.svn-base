/* Author : zhengyangyong */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIC.M9600.Common.DTO.Device;
using Newtonsoft.Json;

namespace AIC.M9600.Common.DTO.Device
{
    public class SampleData
    {
        [JsonIgnore]
        public string IP { get; set; }
        public AnalogRransducerInSlotData[] AnalogRransducerInSlot { get; set; }
        public AnalogRransducerOutSlotData[] AnalogRransducerOutSlot { get; set; }
        public DigitRransducerInSlotData[] DigitRransducerInSlot { get; set; }
        public DigitRransducerOutSlotData[] DigitRransducerOutSlot { get; set; }
        public DigitTachometerSlotData[] DigitTachometerSlot { get; set; }
        public EddyCurrentDisplacementSlotData[] EddyCurrentDisplacementSlot { get; set; }
        public EddyCurrentKeyPhaseSlotData[] EddyCurrentKeyPhaseSlot { get; set; }
        public EddyCurrentTachometerSlotData[] EddyCurrentTachometerSlot { get; set; }
        public RelaySlotData[] RelaySlot { get; set; }
        public IEPESlotData[] IEPESlot { get; set; }
        public WirelessScalarSlotData[] WirelessScalarSlot { get; set; }
        public WirelessVibrationSlotData[] WirelessVibrationSlot { get; set; }

        [JsonIgnore]
        public List<ISlotData> AllSlot
        {
            get
            {
                var allSlots = new List<ISlotData>(
                    (AnalogRransducerInSlot == null ? 0 : AnalogRransducerInSlot.Length) +
                    (AnalogRransducerOutSlot == null ? 0 : AnalogRransducerOutSlot.Length) +
                    (DigitRransducerInSlot == null ? 0 : DigitRransducerInSlot.Length) +
                    (DigitRransducerOutSlot == null ? 0 : DigitRransducerOutSlot.Length) +
                    (DigitTachometerSlot == null ? 0 : DigitTachometerSlot.Length) +
                    (EddyCurrentDisplacementSlot == null ? 0 : EddyCurrentDisplacementSlot.Length) +
                    (EddyCurrentKeyPhaseSlot == null ? 0 : EddyCurrentKeyPhaseSlot.Length) +
                    (EddyCurrentTachometerSlot == null ? 0 : EddyCurrentTachometerSlot.Length) +
                    (IEPESlot == null ? 0 : IEPESlot.Length) +
                    (RelaySlot == null ? 0 : RelaySlot.Length) +
                    (WirelessScalarSlot == null ? 0 : WirelessScalarSlot.Length) +
                    (WirelessVibrationSlot == null ? 0 : WirelessVibrationSlot.Length));

                if (AnalogRransducerInSlot != null) allSlots.AddRange(AnalogRransducerInSlot);
                if (AnalogRransducerOutSlot != null) allSlots.AddRange(AnalogRransducerOutSlot);
                if (DigitRransducerInSlot != null) allSlots.AddRange(DigitRransducerInSlot);
                if (DigitRransducerOutSlot != null) allSlots.AddRange(DigitRransducerOutSlot);
                if (DigitTachometerSlot != null) allSlots.AddRange(DigitTachometerSlot);
                if (EddyCurrentDisplacementSlot != null) allSlots.AddRange(EddyCurrentDisplacementSlot);
                if (EddyCurrentKeyPhaseSlot != null) allSlots.AddRange(EddyCurrentKeyPhaseSlot);
                if (EddyCurrentTachometerSlot != null) allSlots.AddRange(EddyCurrentTachometerSlot);
                if (IEPESlot != null) allSlots.AddRange(IEPESlot);
                if (RelaySlot != null) allSlots.AddRange(RelaySlot);
                if (WirelessScalarSlot != null) allSlots.AddRange(WirelessScalarSlot);
                if (WirelessVibrationSlot != null) allSlots.AddRange(WirelessVibrationSlot);
                return allSlots;
            }
        }

        [JsonIgnore]
        public List<ISlotData> WaveformSlot
        {
            get
            {
                var waveformSlots = new List<ISlotData>(
                    (EddyCurrentDisplacementSlot == null ? 0 : EddyCurrentDisplacementSlot.Length) +
                    (EddyCurrentKeyPhaseSlot == null ? 0 : EddyCurrentKeyPhaseSlot.Length) +
                    (EddyCurrentTachometerSlot == null ? 0 : EddyCurrentTachometerSlot.Length) +
                    (IEPESlot == null ? 0 : IEPESlot.Length) +
                    (WirelessVibrationSlot == null ? 0 : WirelessVibrationSlot.Length));

                if (EddyCurrentDisplacementSlot != null) waveformSlots.AddRange(EddyCurrentDisplacementSlot);
                if (EddyCurrentKeyPhaseSlot != null) waveformSlots.AddRange(EddyCurrentKeyPhaseSlot);
                if (EddyCurrentTachometerSlot != null) waveformSlots.AddRange(EddyCurrentTachometerSlot);
                if (IEPESlot != null) waveformSlots.AddRange(IEPESlot);
                if (WirelessVibrationSlot != null) waveformSlots.AddRange(WirelessVibrationSlot);
                return waveformSlots;
            }
        }
    }
}

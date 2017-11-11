/* Author : zhengyangyong */
using AIC.M9600.Common.DTO.Device;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.M9600.Common.DTO.Web
{
    public class LatestSampleData
    {
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
        public List<ISlotData> WaveformSlot
        {
            get
            {
                var waveformSlots = new List<ISlotData>(
                                    EddyCurrentDisplacementSlot.Length +
                                    EddyCurrentKeyPhaseSlot.Length +
                                    EddyCurrentTachometerSlot.Length +
                                    IEPESlot.Length +
                                    WirelessVibrationSlot.Length);

                waveformSlots.AddRange(EddyCurrentDisplacementSlot);
                waveformSlots.AddRange(EddyCurrentKeyPhaseSlot);
                waveformSlots.AddRange(EddyCurrentTachometerSlot);
                waveformSlots.AddRange(IEPESlot);
                waveformSlots.AddRange(WirelessVibrationSlot);
                return waveformSlots;
            }
        }
    }
}

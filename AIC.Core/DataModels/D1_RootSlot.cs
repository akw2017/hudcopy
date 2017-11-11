using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DataModels
{
    public class D1_RootSlot
    {
        public List<D1_IEPESlot> IEPESlot { get; set; }
        public List<D1_EddyCurrentDisplacementSlot> EddyCurrentDisplacementSlot { get; set; }
        public List<D1_EddyCurrentKeyPhaseSlot> EddyCurrentKeyPhaseSlot { get; set; }
        public List<D1_EddyCurrentTachometerSlot> EddyCurrentTachometerSlot { get; set; }
        public List<D1_DigitTachometerSlot> DigitTachometerSlot { get; set; }
        public List<D1_AnalogRransducerInSlot> AnalogRransducerInSlot { get; set; }
        public List<D1_RelaySlot> RelaySlot { get; set; }
        public List<D1_DigitRransducerInSlot> DigitRransducerInSlot { get; set; }
        public List<D1_DigitRransducerOutSlot> DigitRransducerOutSlot { get; set; }
        public List<D1_AnalogRransducerOutSlot> AnalogRransducerOutSlot { get; set; }
        public List<D1_WirelessScalarSlot> WirelessScalarSlot { get; set; }
        public List<D1_WirelessVibrationSlot> WirelessVibrationSlot { get; set; }
        public List<D1_WaveGeneralSlot> WaveGeneralSlot { get; set; }
        public List<D1_Waveform> Waveform { get; set; }
        public List<D1_DivFreInfo> DivFreInfo { get; set; }
        public D1_MainControlCard MainControlCard { get; set; }
    }
}

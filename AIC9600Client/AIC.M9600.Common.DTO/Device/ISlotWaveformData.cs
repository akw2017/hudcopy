/* Author : zhengyangyong */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.M9600.Common.DTO.Device
{
    public interface ISlotWaveformData
    {
        IWaveformData Waveform { get; set; }
        string WaveUnit { get; set; }
        double? SampleFre { get; set; }
        IWaveformData GenerateWaveformData();
    }
}

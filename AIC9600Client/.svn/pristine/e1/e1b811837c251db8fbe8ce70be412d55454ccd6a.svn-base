/* Author : zhengyangyong */
using AIC.M9600.Common.SlaveDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.M9600.Common.DTO.Device
{
    public interface IWaveformData
    {
        Guid T_Item_Guid { get; set; }
        string ChannelHDID { get; set; }
        DateTime ACQDatetime { get; set; }
        DateTime RegionACQDatetime { get; }
        int AlarmGrade { get; set; }
        Guid RecordLab { get; set; }
        byte[] WaveData { get; set; }
        string WaveUnit { get; set; }
        double? SampleFre { get; set; }
        int? SamplePoint { get; set; }
    }
}

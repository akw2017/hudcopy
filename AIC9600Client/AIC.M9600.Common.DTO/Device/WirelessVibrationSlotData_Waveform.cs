﻿/* Author : zhengyangyong */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIC.M9600.Common.SlaveDB.Generated;

namespace AIC.M9600.Common.DTO.Device
{
    public class WirelessVibrationSlotData_Waveform : D_WirelessVibrationSlot_Waveform, IWaveformData
    {
        public DateTime RegionACQDatetime
        {
            get { return new DateTime(ACQDatetime.Year, ACQDatetime.Month, 1); }
        }
    }
}

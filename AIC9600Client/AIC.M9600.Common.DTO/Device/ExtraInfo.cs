﻿/* Author : zhengyangyong */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.M9600.Common.DTO.Device
{
    public class ExtraInfo
    {
        public double AlarmCount { get; set; }
        public double AlarmTimeLength { get; set; }
        public double DangerCount { get; set; }
        public double DangerTimeLength { get; set; }
        public double InvalidCount { get; set; }
        public double InvalidTimeLength { get; set; }
        public double NormalCount { get; set; }
        public double NormalTimeLength { get; set; }
        public double NotOKTimeLength { get; set; }
        public double PreAlarmCount { get; set; }
        public double PreAlarmTimeLength { get; set; }
    }
}

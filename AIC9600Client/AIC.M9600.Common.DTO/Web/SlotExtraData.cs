/* Author : zhengyangyong */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.M9600.Common.DTO
{
    public class SlotExtraData
    {
        public Dictionary<string, double> StatisticsInfo { get; set; }

        public SlotExtraData()
        {
            StatisticsInfo = new Dictionary<string, double>();
        }
    }
}

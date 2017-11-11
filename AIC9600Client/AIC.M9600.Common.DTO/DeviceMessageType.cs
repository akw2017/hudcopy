/* Author : zhengyangyong */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.M9600.Common.DTO
{
    public enum DeviceMessageType : short
    {
        UploadSampleDataRequest = 10001,
        UploadSampleDataResponse = -10001,

        SyncTimeRequest = 10002,
        SyncTimeResponse = -10002,
    }
}

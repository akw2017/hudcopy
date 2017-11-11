/* Author : zhengyangyong */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.M9600.Common.DTO
{
    public class ResponseCode
    {
        //Server Side
        public static string SERVER_OK = "#OK";
        public static string SERVER_VERSION_UNMATCH = "#ServerVersionUnmatch";
        public static string SERVER_EXCEPTION = "#ServerException";
        public static string SERVER_UNKNOWN_MESSAGE_TYPE = "#ServerUnknownMessageType";

        //Client Side
        public static string CLIENT_INPUT_ERROR = "#ClientInputError";
        public static string CLIENT_EXCEPTION = "#ClientException";

        //Device Side
        public static string DEVICE_ERROR = "#DeviceError";
        public static string DEVICE_CONNECT_ERROR = "#DeviceConnectError";
    }
}

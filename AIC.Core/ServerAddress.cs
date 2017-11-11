using System;
using System.Configuration;

namespace AIC.Core
{
    public class ServerAddress
    {
        public static string CTLAddress = Convert.ToString(ConfigurationManager.AppSettings["CTLADDRESS"]);
        public static string TRDADDRESS = Convert.ToString(ConfigurationManager.AppSettings["TRDADDRESS"]);
        public static string VIDEOADDRESS = Convert.ToString(ConfigurationManager.AppSettings["VIDEOADDRESS"]);
    }
}

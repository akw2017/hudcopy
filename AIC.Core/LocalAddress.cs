using System;
using System.Configuration;

namespace AIC.Core
{
    public class LocalAddress
    {
        public static string ServerXmlDir = Convert.ToString(ConfigurationManager.AppSettings["ServerXmlDir"]);
        public static string MapHtmlUri = Convert.ToString(ConfigurationManager.AppSettings["MapHtmlUri"]);
        public static string LayoutPath = Convert.ToString(ConfigurationManager.AppSettings["LayoutPath"]);
        public static string GifDir = Convert.ToString(ConfigurationManager.AppSettings["GifDir"]);
        public static string ScreenShotDir = Convert.ToString(ConfigurationManager.AppSettings["ScreenShotDir"]); 

        public static int PDAPort = Convert.ToInt32(ConfigurationManager.AppSettings["PDAPort"]);
        public static int ServerPort = Convert.ToInt32(ConfigurationManager.AppSettings["ServerPort"]);
        
        public static int UpdateTime = Convert.ToInt32(ConfigurationManager.AppSettings["UpdateTime"]);
        public static string Version = Convert.ToString(ConfigurationManager.AppSettings["Version"]);

        public static string[] sArray = Version.Split('.');

        public readonly static ushort MajorVersion = (ushort)Convert.ToInt32(sArray[0]);
        public readonly static ushort MinorVersion = (ushort)Convert.ToInt32(sArray[1]);
        public readonly static ushort ThirdVersion = (ushort)Convert.ToInt32(sArray[2]);
        public readonly static string FourthVersion = sArray[3];
        public readonly static string FifthVersion = sArray[4];
        //public readonly static string Version = MajorVersion.ToString() + "." + MinorVersion.ToString() 
        //    + "." + ThirdVersion.ToString() + "." + FourthVersion + "." + FifthVersion;
    }
}

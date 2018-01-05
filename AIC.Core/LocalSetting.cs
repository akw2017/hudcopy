using AIC.Core.Models;
using System;
using System.Configuration;

namespace AIC.Core
{
    public class LocalSetting
    {
        //public static string ServerXmlDir = Convert.ToString(ConfigurationManager.AppSettings["ServerXmlDir"]);
        //public static string MapHtmlUri = Convert.ToString(ConfigurationManager.AppSettings["MapHtmlUri"]);
        //public static string LayoutPath = Convert.ToString(ConfigurationManager.AppSettings["LayoutPath"]);
        //public static string GifDir = Convert.ToString(ConfigurationManager.AppSettings["GifDir"]);
        //public static string ScreenShotDir = Convert.ToString(ConfigurationManager.AppSettings["ScreenShotDir"]); 

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

        public static bool IsHistoryMode = Convert.ToBoolean(ConfigurationManager.AppSettings["IsHistoryMode"]);
        public static DateTime HistoryModeStartTime = Convert.ToDateTime(ConfigurationManager.AppSettings["HistoryModeStartTime"]);
        public static DateTime HistoryModeEndTime = Convert.ToDateTime(ConfigurationManager.AppSettings["HistoryModeEndTime"]);
        public static float HistoryModeSpeedUpRatio = Convert.ToSingle(ConfigurationManager.AppSettings["HistoryModeSpeedUpRatio"]);
        public static float HistoryModeDataInterval = Convert.ToSingle(ConfigurationManager.AppSettings["HistoryModeDataInterval"]);
        public static bool IsHistoryRrackingMode = Convert.ToBoolean(ConfigurationManager.AppSettings["IsHistoryRrackingMode"]);
        public static float HistoryModeDBCallInterval = Convert.ToSingle(ConfigurationManager.AppSettings["HistoryModeDBCallInterval"]);
        /// <summary>
        /// 保存appSetting
        /// </summary>
        /// <param name="key">appSetting的KEY值</param>
        /// <param name="value">appSetting的Value值</param>
        public static void SetAppSetting(string key, string value)
        {
            // 创建配置文件对象
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings[key] != null)
            {
                // 修改
                config.AppSettings.Settings[key].Value = value;
            }
            else
            {
                // 添加
                AppSettingsSection ass = (AppSettingsSection)config.GetSection("appSettings");
                ass.Settings.Add(key, value);
            }

            // 保存修改
            config.Save(ConfigurationSaveMode.Modified);

            // 强制重新载入配置文件的连接配置节
            ConfigurationManager.RefreshSection("appSettings");         
        }
    }
}

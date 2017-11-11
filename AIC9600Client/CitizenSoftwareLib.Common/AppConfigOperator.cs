﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenSoftwareLib.Common
{
    public class AppConfigOperator
    {
        public static T GetAppConfigValue<T>(string key)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
            }
            else return default(T);
        }

        public static void SaveAppConfig(string exePath, Dictionary<string, object> keyAndValue)
        {
            System.Configuration.Configuration conf = ConfigurationManager.OpenExeConfiguration(exePath);
            foreach (KeyValuePair<string, object> kvp in keyAndValue)
            {
                conf.AppSettings.Settings.Remove(kvp.Key);
                conf.AppSettings.Settings.Add(kvp.Key, kvp.Value.ToString());
            }
            conf.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void SaveAppConfig(Dictionary<string, object> keyAndValue)
        {
            System.Configuration.Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            foreach (KeyValuePair<string, object> kvp in keyAndValue)
            {
                conf.AppSettings.Settings.Remove(kvp.Key);
                conf.AppSettings.Settings.Add(kvp.Key, kvp.Value.ToString());
            }
            conf.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}

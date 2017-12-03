using AIC.Core;
using AIC.Core.Models;
using AIC.Core.Servers;
using AIC.ServiceInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.LocalConfiguration
{
    public class LocalConfiguration : ILocalConfiguration
    {       
      
        public List<ServerInfo> ServerInfoList { get; set; }

        IXmlDataService dataService = new XmlDataService();
        public LocalConfiguration()
        {
            ServerInfoList = new List<ServerInfo>();            
        }

        public void Initialize()
        {
            try
            {
                string dir = @LocalSetting.ServerXmlDir;
                ServerInfoList = dataService.ReadXml(dir).ToList();
            }
            catch { }
            finally
            {
                if (ServerInfoList == null)
                {
                    ServerInfoList = new List<ServerInfo>();
                }
                if (ServerInfoList.Count == 0)
                {
                    ServerInfoList.Add(new ServerInfo { IP = "127.0.0.1", Name = "本地服务器" });
                }
            }

            //string config = System.AppDomain.CurrentDomain.BaseDirectory + "\\MyData\\Configuration\\config.ini";
            ////读取备份配置文件
            //if (File.Exists(config))
            //{
            //    string PDAPort = IniFileHelper.GetValue("BaseSettingInfo", "PDAPort", config);
            //    if (PDAPort != "发生错误" )
            //    {

            //    }
            //}
        }
        public void ReadServerInfo()
        {
            Initialize();
        }

        public void WriteServerInfo(IList<ServerInfo> info)
        {          
            ServerInfoList = new List<ServerInfo>(info);           
            string dir = @LocalSetting.ServerXmlDir;
            var filename = dir.Substring(dir.LastIndexOf("\\"));
            var directory = dir.Substring(0, dir.Length - filename.Length);
            if (!Directory.Exists(@directory))
            {
                Directory.CreateDirectory(@directory);
            }
            dataService.WriteXml(dir, info);
        }
    }
}

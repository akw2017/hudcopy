using AIC.Core;
using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.Core.Servers;
using AIC.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.LocalConfiguration
{
    public class LocalConfiguration : ILocalConfiguration
    {       
      
        public ObservableCollection<ServerInfo> ServerInfoList { get; set; }
        public IEnumerable<ServerInfo> LoginServerInfoList { get { return ServerInfoList.Where(p => p.LoginResult == true).Distinct(EqualityHelper<ServerInfo>.CreateComparer(p => p.IP)); } }

        IXmlDataService dataService = new XmlDataService();
        public LocalConfiguration()
        {
            ServerInfoList = new ObservableCollection<ServerInfo>();            
        }

        //string dir = @LocalSetting.ServerXmlDir;//昌邑石化
        private string dir = System.AppDomain.CurrentDomain.BaseDirectory + "MyData\\Configuration\\Servers.xml";
        public void Initialize()
        {
            try
            {
                ServerInfoList.Clear();
                ServerInfoList.AddRange(dataService.ReadServerXml(dir));
            }
            catch { }
            finally
            {
                if (ServerInfoList == null)
                {
                    ServerInfoList = new ObservableCollection<ServerInfo>();
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
            //Initialize();
        }
        public void WriteServerInfo(IList<ServerInfo> info)
        {
            var serverlist = info.Distinct(EqualityHelper<ServerInfo>.CreateComparer(p => p.IP)).ToArray();
            ServerInfoList.Clear();            
            ServerInfoList.AddRange(serverlist);  //去重        
            
            var filename = dir.Substring(dir.LastIndexOf("\\"));
            var directory = dir.Substring(0, dir.Length - filename.Length);
            if (!Directory.Exists(@directory))
            {
                Directory.CreateDirectory(@directory);
            }
            dataService.WriteServerXml(dir, ServerInfoList);
        }       
    }
}

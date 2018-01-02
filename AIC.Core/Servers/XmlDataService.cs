using AIC.Core.Models;
using AIC.Core.Servers;
using AIC.Resources.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace AIC.Core.Servers
{
    public class XmlDataService : IXmlDataService
    {
        public IList<ServerInfo> ReadServerXml(string dir)
        {
            var sw = Stopwatch.StartNew();
            List<ServerInfo> list = new List<ServerInfo>();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(dir);
                var xmlNodeList = xmlDoc.SelectSingleNode("Root").ChildNodes;
                foreach (XmlNode node in xmlNodeList)
                {
                    XmlElement element = (XmlElement)node;
                    string id = element.GetAttribute("id");
                    ServerInfo infoItem = new ServerInfo();

                    int itemId = int.MaxValue;
                    Int32.TryParse(id, out itemId);
                    infoItem.ID = itemId;

                    //infoItem.Name = element["Name"].InnerText;
                    //infoItem.IP = element["IP"].InnerText;
                    //int itemPort = 0;
                    //Int32.TryParse(element["Port"].InnerText, out itemPort);
                    //infoItem.IsCloud = (element["IsCloud"].InnerText.Trim() == "1") ? IconAddress.Weather_cloud_Icon : IconAddress.Computer_Icon;
                    //infoItem.Port = itemPort;
                    //infoItem.Factory = element["Factory"].InnerText;
                    //double itemLongitude = 0.0;
                    //Double.TryParse(element["Longitude"].InnerText, out itemLongitude);
                    //infoItem.Longitude = itemLongitude;
                    //double itemLatitude = 0.0;
                    //Double.TryParse(element["Latitude"].InnerText, out itemLatitude);
                    //infoItem.Latitude = itemLatitude;
                    //infoItem.IsLogin = (element["IsLogin"].InnerText.Trim() == "1") ? IconAddress.Tick_Icon : IconAddress.Minus_Icon;
                    //infoItem.LoginResult = IconAddress.Minus_Icon;
                    //infoItem.Permission = "无权限";
                    
                    foreach (XmlNode xmlNode in element.ChildNodes)
                    {
                        XmlElement xmlEle = (XmlElement)xmlNode;
                        if (xmlEle.Name == "Name")
                        {
                            infoItem.Name = xmlEle.InnerText;
                        }
                        if (xmlEle.Name == "IP")
                        {
                            infoItem.IP = xmlEle.InnerText;
                        }
                        if (xmlEle.Name == "Port")
                        {
                            int itemPort = 0;
                            Int32.TryParse(xmlEle.InnerText, out itemPort);
                            infoItem.Port = itemPort;
                        }
                        if (xmlEle.Name == "IsCloud")
                        {
                            infoItem.IsCloud = (xmlEle.InnerText.Trim() == "1") ? true : false;                          
                        }                        
                        if (xmlEle.Name == "Factory")
                        {
                            infoItem.Factory = xmlEle.InnerText;
                        }
                        if (xmlEle.Name == "Longitude")
                        {
                            double itemLongitude = 0.0;
                            Double.TryParse(xmlEle.InnerText, out itemLongitude);
                            infoItem.Longitude = itemLongitude;
                        }
                        if (xmlEle.Name == "Latitude")
                        {
                            double itemLatitude = 0.0;
                            Double.TryParse(xmlEle.InnerText, out itemLatitude);
                            infoItem.Latitude = itemLatitude;
                        }
                        if (xmlEle.Name == "IsLogin")
                        {
                            infoItem.IsLogin = (xmlEle.InnerText.Trim() == "1") ? true : false;                           
                        }
                        if (xmlEle.Name == "UserName")
                        {
                            infoItem.UserName = xmlEle.InnerText;
                        }
                        if (xmlEle.Name == "UserPwd")
                        {                            
                            infoItem.UserPwd = MyEncrypt.DecryptDES(xmlEle.InnerText);                            
                        }
                        if (xmlEle.Name == "IsSaveUserName")
                        {
                            infoItem.IsSaveUserName = (xmlEle.InnerText.Trim() == "1") ? true : false;
                        }
                        if (xmlEle.Name == "IsSaveUserPwd")
                        {
                            infoItem.IsSaveUserPwd = (xmlEle.InnerText.Trim() == "1") ? true : false;
                        }
                        infoItem.LoginResult = false;
                        infoItem.Permission = "无权限";
                    }
                    list.Add(infoItem);                     
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Console.WriteLine("解密消耗时间" + sw.Elapsed.ToString());
            return list;            
        }

        public void WriteServerXml(string dir, IList<ServerInfo> list)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                var arrayDataAsXElements = from c in list
                                           select
                                                new XElement
                                                ("Server", new XAttribute("id", c.ID),
                                                    new XElement("Name", c.Name),
                                                    new XElement("IP", c.IP),
                                                    new XElement("Port", c.Port),
                                                    new XElement("IsCloud", (c.IsCloud == true) ? 1 : 0),
                                                    new XElement("Factory", c.Factory),
                                                    new XElement("Longitude", c.Longitude),
                                                    new XElement("Latitude", c.Latitude),
                                                    new XElement("IsLogin", (c.IsLogin == true) ? 1 : 0),
                                                    new XElement("LoginResult", (c.LoginResult == true) ? 0 : 1),
                                                    new XElement("Permission", c.Permission),
                                                    new XElement("UserName", c.UserName),
                                                    new XElement("UserPwd", MyEncrypt.EncryptDES(c.UserPwd)),
                                                    new XElement("IsSaveUserName", (c.IsSaveUserName == true) ? 1 : 0),
                                                    new XElement("IsSaveUserPwd", (c.IsSaveUserPwd == true) ? 1 : 0)
                                                );
                XElement peopleDoc = new XElement("Root", arrayDataAsXElements);
                var doc = new XDocument(peopleDoc);
                doc.Save(dir);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            Console.WriteLine("加密消耗时间" + sw.Elapsed.ToString());
        }

        public IList<ChartFileData> ReadChartXml(string dir)
        {
            List<ChartFileData> list = new List<ChartFileData>();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(dir);
                var xmlNodeList = xmlDoc.SelectSingleNode("Root").ChildNodes;
                foreach (XmlNode node in xmlNodeList)
                {
                    ChartFileData filedata = new ChartFileData();
                    XmlElement element = (XmlElement)node;
                    string name = element.GetAttribute("name");
                    filedata.Name = name;
                    filedata.ListGuid = new List<Guid>();

                    foreach (XmlNode xmlNode in element.ChildNodes)
                    {
                        XmlElement xmlEle = (XmlElement)xmlNode;
                        filedata.ListGuid.Add(new Guid(xmlEle.InnerText));
                    }
                    list.Add(filedata);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        public void WriteChartXml(string dir, IList<ChartFileData> list)
        {
            try
            {
                List<XElement> arrayDataAsXElements = new List<XElement>();
                foreach (var item in list)
                {
                    XElement element = new XElement("Chart", new XAttribute("name", item.Name));
                    foreach(var guid in item.ListGuid)
                    {
                        element.Add(new XElement("Guid", guid));
                    }
                    arrayDataAsXElements.Add(element);
                }

                XElement peopleDoc = new XElement("Root", arrayDataAsXElements);
                var doc = new XDocument(peopleDoc);
                doc.Save(dir);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

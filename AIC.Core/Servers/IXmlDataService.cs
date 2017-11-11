using AIC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Servers
{
    public interface IXmlDataService
    {
        IList<ServerInfo> ReadXml(string dir);
        void WriteXml(string dir, IList<ServerInfo> list);
    }
}

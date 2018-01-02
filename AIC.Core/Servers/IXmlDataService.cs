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
        IList<ServerInfo> ReadServerXml(string dir);
        void WriteServerXml(string dir, IList<ServerInfo> list);
        IList<ChartFileData> ReadChartXml(string dir);
        void WriteChartXml(string dir, IList<ChartFileData> list);
    }
}

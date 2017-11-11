using AIC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.ServiceInterface
{
    public interface ILocalConfiguration
    {       
        List<ServerInfo> ServerInfoList { get; }
        void Initialize();
        void ReadServerInfo();
        void WriteServerInfo(IList<ServerInfo> info);
    }
}

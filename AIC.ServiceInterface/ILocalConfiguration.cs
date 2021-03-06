﻿using AIC.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.ServiceInterface
{
    public interface ILocalConfiguration
    {
        ObservableCollection<ServerInfo> ServerInfoList { get; }
        IEnumerable<ServerInfo> LoginServerInfoList { get; }

        void Initialize();
        void ReadServerInfo();
        void WriteServerInfo(IList<ServerInfo> info);
    }
}

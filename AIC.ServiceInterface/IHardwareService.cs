using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.ServiceInterface
{
    public interface IHardwareService
    {
        ObservableCollection<ServerTreeItemViewModel> ServerTreeItems { get; }
        void Initialize();
        void InitServers(IEnumerable<ServerInfo> serverlist);
        Task<List<ChannelTreeItemViewModel>> AddCard(string serverIP, string maincardIP, string json);
        Task<List<ChannelTreeItemViewModel>> DeleteCard(string serverIP, string maincardIP, string json);
        Task<List<ChannelTreeItemViewModel>> ForceDeleteCard(string serverIP, string maincardIP);
        void GetCardFromDatabase();
        Task<List<ChannelTreeItemViewModel>> AddTransmissionCard(string serverIP, string maincardIP, string identifier, string json);
        Task<List<ChannelTreeItemViewModel>> DeleteTransmissionCard(string serverIP, string maincardIP, string identifier, string json);
    }
}

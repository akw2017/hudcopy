using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.ServiceInterface
{
    public interface IHardwareService
    {
        ObservableCollection<ServerTreeItemViewModel> ServerTreeItems { get; set; }
        Dictionary<string, T1_RootCard> T_RootCard { get; set; }
        void Initialize();
        void InitServers();
        Task<List<ChannelTreeItemViewModel>> AddCard(string serverIP, string maincardIP, string json);
        Task<List<ChannelTreeItemViewModel>> DeleteCard(string serverIP, string maincardIP, string json);
        Task<List<ChannelTreeItemViewModel>> ForceDeleteCard(string serverIP, string maincardIP);
        void SaveCardToDatabase();
        void GetCardFromDatabase();
        Task<List<ChannelTreeItemViewModel>> AddTransmissionCard(string serverIP, string maincardIP, string identifier, string json);
        Task<List<ChannelTreeItemViewModel>> DeleteTransmissionCard(string serverIP, string maincardIP, string identifier, string json);
    }
}

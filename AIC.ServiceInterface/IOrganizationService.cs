using AIC.Core.LMModels;
using AIC.Core.OrganizationModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.ServiceInterface
{
    public interface IOrganizationService
    {
        ObservableCollection<OrganizationTreeItemViewModel> OrganizationTreeItems { get; set; }      
        ObservableCollection<OrganizationTreeItemViewModel> RecycledTreeItems { get; set; }
        List<ItemTreeItemViewModel> ItemTreeItems { get; set; }
        List<DivFreTreeItemViewModel> DivFreTreeItems { get; set; }
        Dictionary<string, List<T1_Organization>> T_Organization { get; set; }
        Dictionary<string, List<T1_OrganizationPrivilege>> T_OrganizationPrivilege { get; set; }
        Dictionary<string, List<T1_Item>> T_Item { get; set; }
        Dictionary<string, List<T1_DivFreInfo>> T_DivFreInfo { get; set; }
        void Initialize();
        void InitOrganizations(bool isadmin);
        void AddItem(ItemTreeItemViewModel item);
        void DeleteItem(ItemTreeItemViewModel item);
        void AddDivFre(DivFreTreeItemViewModel divfre);
        void DeleteDivFre(DivFreTreeItemViewModel divfre);
        void SaveOrganizationToDatabase(string ip);
        void GetOrganizationFromDatabase(string ip, bool isadmin);              
    }
}

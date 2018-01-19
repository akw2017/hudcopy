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
        ObservableCollection<OrganizationTreeItemViewModel> OrganizationTreeItems { get;  }
        ObservableCollection<OrganizationTreeItemViewModel> RecycledTreeItems { get; }
        List<ItemTreeItemViewModel> ItemTreeItems { get;  }
        List<DivFreTreeItemViewModel> DivFreTreeItems { get;  }
        Dictionary<string, List<T1_DivFreInfo>> T_DivFreInfo { get;  }
        void Initialize();
        void SetUserOrganizationPrivilege(Guid? guid);
        void InitOrganizations(bool isadmin);
        void AddItem(ItemTreeItemViewModel item);
        void DeleteItem(ItemTreeItemViewModel item);
        void AddDivFre(DivFreTreeItemViewModel divfre);
        void DeleteDivFre(DivFreTreeItemViewModel divfre);
        void SaveOrganizationToDatabase(string ip);
        void GetOrganizationFromDatabase(string ip, bool isadmin);
        void SetDivFres();
    }
}

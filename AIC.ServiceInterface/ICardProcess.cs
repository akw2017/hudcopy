using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.Core.UserManageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.ServiceInterface
{
    public interface ICardProcess
    {
        List<ISlot> GetCardSlot(ICard card);
        List<IChannel> GetSlotChannel(ISlot slot);
        List<MainCardTreeItemViewModel> GetMainCards(IList<ServerTreeItemViewModel> servers);
        IChannel GetHardwareChannel(IList<ServerTreeItemViewModel> servers, T1_Item item);      
        IChannel GetHardwareChannel(IList<WireMatchingCard> cards, int cardNum, int slotNum, int chnNum);
        IChannel GetHardwareChannel(IList<TransmissionCard> cards, string slaveIdentifier, int slotNum, int chnNum);
        ChannelTreeItemViewModel GetChannel(IList<ServerTreeItemViewModel> servers, T1_Item item);     
        DivFreInfo GetDivFreInfo(IChannel i_channel, DivFreTreeItemViewModel divfre, ItemTreeItemViewModel item);
        List<OrganizationTreeItemViewModel> GetOrganizations(IList<OrganizationTreeItemViewModel> organizations);
        List<OrganizationTreeItemViewModel> GetOrganizations(OrganizationTreeItemViewModel organization);
        List<OrganizationTreeItemViewModel> GetCopyOrganizations(OrganizationTreeItemViewModel organization, OrganizationTreeItemViewModel parentorganization);
        List<ItemTreeItemViewModel> GetItems(IList<OrganizationTreeItemViewModel> organizations);
        List<ItemTreeItemViewModel> GetItems(OrganizationTreeItemViewModel organization);
        List<DeviceTreeItemViewModel> GetDevices(IList<OrganizationTreeItemViewModel> organizations);
        List<DeviceTreeItemViewModel> GetDevices(OrganizationTreeItemViewModel organization);
        List<ChannelTreeItemViewModel> GetChannels(IList<ServerTreeItemViewModel> servers);
        ItemTreeItemViewModel RecycleRecycledItem(IList<OrganizationTreeItemViewModel> recycled, ItemTreeItemViewModel item);
        List<ItemTreeItemViewModel> RecycleRecycledItem(IList<OrganizationTreeItemViewModel> recycled, IList<ItemTreeItemViewModel> items);
        void RecycleDeleteItem(IList<OrganizationTreeItemViewModel> recycled, ItemTreeItemViewModel item);
        void GetOrganizationVisibility(IList<OrganizationTreeItemViewModel> organizations, IList<T1_OrganizationPrivilege> t_organizationPrivilege);
        List<Organization> GetParentOrganizations(OrganizationTreeItemViewModel organization);
        string GetOrganizationName(OrganizationTreeItemViewModel organization);
        string GetOrganizationServer(OrganizationTreeItemViewModel organization);
        Organization OrganizationConvert(T1_Organization t_organization);
        List<T1_Organization> GetChildOrganizations(OrganizationTreeItemViewModel organization);
        string SameNameChecked(IList<T1_Organization> organizations, OrganizationTreeItemViewModel organization, bool IsNew = false);
        List<DivFreInfo> GetDivFreInfoListWithoutTemp(DivFreIChannel ichannel);
        ItemTreeItemViewModel GetRecycledItem(string ip, ChannelTreeItemViewModel channeltree, OrganizationTreeItemViewModel recycledTreeItem);
        Organization GetNewOrganization(ItemTreeItemViewModel itemtree, ItemTreeItemViewModel recycleditemtree);
        OrganizationTreeItemViewModel GetSelectedTree(IList<OrganizationTreeItemViewModel> organizations);
    }
}

using AIC.Core.OrganizationModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.PDAPage.Models
{
    public class TreeProcess
    {
        //自动获取节点
        public static bool AutoServerTreeSelected(List<ItemTreeItemViewModel> items, ChannelTreeItemViewModel channel)//查找节点
        {
            if (items == null || items.Count == 0)
            {
                return false;
            }

            foreach (var item in items)
            {
                if (item.T_Organization == null || channel.IChannel == null || channel.IChannel.T_Item_Guid == null || channel.IChannel.T_Item_Guid == "")
                {
                    continue;
                }
                if (item.T_Organization.Guid != new Guid(channel.IChannel.T_Item_Guid))
                {
                    continue;
                }

                AutoParentOrganizationTreeExpand(item);
                item.IsSelected = true;
                return true;
            }
            return false;
        }
        public static bool AutoServerTreeSelected(ObservableCollection<OrganizationTreeItemViewModel> organizations, ChannelTreeItemViewModel channel)//查找节点
        {
            if (organizations == null || organizations.Count == 0)
            {
                return false;
            }

            foreach (var organization in organizations)
            {
                var item = organization as ItemTreeItemViewModel;
                if (item != null)
                {
                    if (item.T_Item == null || channel.IChannel == null || channel.IChannel.T_Item_Guid == null || channel.IChannel.T_Item_Guid == "")
                    {
                        continue;
                    }
                    if (item.T_Item.Guid != new Guid(channel.IChannel.T_Item_Guid))
                    {
                        continue;
                    }
                    if (item.T_Item.IP != channel.MainControlCardIP)
                    {
                        continue;
                    }

                    AutoParentOrganizationTreeExpand(item);
                    item.IsSelected = true;
                    return true;
                }
                if (AutoServerTreeSelected(organization.Children, channel))
                {
                    return true;
                }
            }
            return false;
        }

        private static void AutoParentOrganizationTreeExpand(OrganizationTreeItemViewModel organization)//自动展开节点
        {
            if (organization != null)
            {
                AutoParentOrganizationTreeExpand(organization.Parent);
                organization.IsExpanded = true;
            }
        }

        public static ChannelTreeItemViewModel AutoOrganizationTreeSelected(ObservableCollection<ServerTreeItemViewModel> servers, ItemTreeItemViewModel item)//查找节点
        {
            if (servers == null || servers.Count == 0)
            {
                return null;
            }

            foreach (var server in servers)
            {
                var channel = server as ChannelTreeItemViewModel;
                if (channel != null)
                {
                    if (item.T_Item == null || channel.IChannel == null || channel.IChannel.T_Item_Guid == null || channel.IChannel.T_Item_Guid == "")
                    {
                        continue;
                    }
                    if (item.T_Item.Guid != new Guid(channel.IChannel.T_Item_Guid))
                    {
                        continue;
                    }
                    if (item.T_Item.IP != channel.MainControlCardIP)
                    {
                        continue;
                    }

                    AutoParentServerTreeExpand(channel);
                    channel.IsSelected = true;
                    //GetServerNode(item);//得到服务器根节点
                    return channel;
                }

                var subitem = AutoOrganizationTreeSelected(server.Children, item);
                if (subitem != null)
                {
                    return subitem;
                }
            }
            return null;
        }

        private static void AutoParentServerTreeExpand(ServerTreeItemViewModel server)//自动展开节点
        {
            if (server != null)
            {
                AutoParentServerTreeExpand(server.Parent);
                server.IsExpanded = true;
            }
        }
    }
}

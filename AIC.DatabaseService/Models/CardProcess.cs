using AIC.Core.Helpers;
using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.Core.UserManageModels;
using AIC.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AIC.DatabaseService.Models
{
    public class CardProcess: ICardProcess
    {
        public List<ISlot> GetCardSlot(ICard i_card)
        {
            List<ISlot> i_slots = new List<ISlot>();
            if (i_card is WireMatchingCard)
            {
                var card = i_card as WireMatchingCard;
                if (card.IEPESlot != null)
                {
                    i_slots.Add(card.IEPESlot);
                }
                if (card.EddyCurrentDisplacementSlot != null)
                {
                    i_slots.Add(card.EddyCurrentDisplacementSlot);
                }
                if (card.EddyCurrentKeyPhaseSlot != null)
                {
                    i_slots.Add(card.EddyCurrentKeyPhaseSlot);
                }
                if (card.EddyCurrentTachometerSlot != null)
                {
                    i_slots.Add(card.EddyCurrentTachometerSlot);
                }
                if (card.DigitTachometerSlot != null)
                {
                    i_slots.Add(card.DigitTachometerSlot);
                }
                if (card.AnalogRransducerInSlot != null)
                {
                    i_slots.Add(card.AnalogRransducerInSlot);
                }
                if (card.RelaySlot != null)
                {
                    i_slots.Add(card.RelaySlot);
                }
                if (card.DigitRransducerInSlot != null)
                {
                    i_slots.Add(card.DigitRransducerInSlot);
                }
                if (card.DigitRransducerOutSlot != null)
                {
                    i_slots.Add(card.DigitRransducerOutSlot);
                }
                if (card.AnalogRransducerOutSlot != null)
                {
                    i_slots.Add(card.AnalogRransducerOutSlot);
                }
            }
            else if (i_card is TransmissionCard)
            {
                var card = i_card as TransmissionCard;
                if (card.WirelessScalarSlot != null)
                {
                    i_slots.Add(card.WirelessScalarSlot);
                }
                if (card.WirelessVibrationSlot != null)
                {
                    i_slots.Add(card.WirelessVibrationSlot);
                }
            }
            return i_slots;
        }
        public List<IChannel> GetSlotChannel(ISlot slot)
        {
            List<IChannel> Channels = new List<IChannel>();
            if (slot is IEPESlot && (slot as IEPESlot).IEPEChannelInfo != null)
            {
                foreach(var channel in (slot as IEPESlot).IEPEChannelInfo)
                {                  
                    Channels.Add(channel);
                }
               
            }
            else if (slot is EddyCurrentDisplacementSlot && (slot as EddyCurrentDisplacementSlot).EddyCurrentDisplacementChannelInfo != null)
            {
                foreach (var channel in (slot as EddyCurrentDisplacementSlot).EddyCurrentDisplacementChannelInfo)
                {                   
                    Channels.Add(channel);
                }
            }
            else if (slot is EddyCurrentKeyPhaseSlot && (slot as EddyCurrentKeyPhaseSlot).EddyCurrentKeyPhaseChannelInfo != null)
            {
                foreach (var channel in (slot as EddyCurrentKeyPhaseSlot).EddyCurrentKeyPhaseChannelInfo)
                {                 
                    Channels.Add(channel);                   
                }
            }
            else if (slot is EddyCurrentTachometerSlot && (slot as EddyCurrentTachometerSlot).EddyCurrentTachometerChannelInfo != null)
            {
                foreach (var channel in (slot as EddyCurrentTachometerSlot).EddyCurrentTachometerChannelInfo)
                {                    
                    Channels.Add(channel);
                }
            }
            else if (slot is DigitTachometerSlot && (slot as DigitTachometerSlot).DigitTachometerChannelInfo != null)
            {
                foreach (var channel in (slot as DigitTachometerSlot).DigitTachometerChannelInfo)
                {                   
                    Channels.Add(channel);
                }
            }
            else if (slot is AnalogRransducerInSlot && (slot as AnalogRransducerInSlot).AnalogRransducerInChannelInfo != null)
            {
                foreach (var channel in (slot as AnalogRransducerInSlot).AnalogRransducerInChannelInfo)
                {                   
                    Channels.Add(channel);
                }
            }
            else if (slot is RelaySlot && (slot as RelaySlot).RelayChannelInfo != null)
            {
                foreach (var channel in (slot as RelaySlot).RelayChannelInfo)
                {                    
                    Channels.Add(channel);
                }
            }
            else if (slot is DigitRransducerInSlot && (slot as DigitRransducerInSlot).DigitRransducerInChannelInfo != null)
            {
                foreach (var channel in (slot as DigitRransducerInSlot).DigitRransducerInChannelInfo)
                {                  
                    Channels.Add(channel);
                }
            }
            else if (slot is DigitRransducerOutSlot && (slot as DigitRransducerOutSlot).DigitRransducerOutChannelInfo != null)
            {
                foreach (var channel in (slot as DigitRransducerOutSlot).DigitRransducerOutChannelInfo)
                {                    
                    Channels.Add(channel);
                }
            }
            else if (slot is AnalogRransducerOutSlot && (slot as AnalogRransducerOutSlot).AnalogRransducerOutChannelInfo != null)
            {
                foreach (var channel in (slot as AnalogRransducerOutSlot).AnalogRransducerOutChannelInfo)
                {                   
                    Channels.Add(channel);
                }
            }
            else if(slot is WirelessScalarSlot && (slot as WirelessScalarSlot).WirelessScalarChannelInfo != null)
            {
                foreach (var channel in (slot as WirelessScalarSlot).WirelessScalarChannelInfo)
                {
                    Channels.Add(channel);
                }
            }
            else if (slot is WirelessVibrationSlot && (slot as WirelessVibrationSlot).WirelessVibrationChannelInfo != null)
            {
                foreach (var channel in (slot as WirelessVibrationSlot).WirelessVibrationChannelInfo)
                {
                    Channels.Add(channel);
                }
            }
            return Channels;
        }

        //将服务树中所有通道list出来
        public List<ChannelTreeItemViewModel> GetChannels(IList<ServerTreeItemViewModel> servers)
        {
            if (servers == null || servers.Count == 0)
            {
                return null;
            }
            List<ChannelTreeItemViewModel> channels = new List<ChannelTreeItemViewModel>();

            foreach (var server in servers)
            {
                if (server is ChannelTreeItemViewModel)
                {
                    channels.Add(server as ChannelTreeItemViewModel);
                }
                else
                {
                    var childs = GetChannels(server.Children);
                    if (childs != null)
                    {
                        channels.AddRange(childs);
                    }
                }
            }
            return channels;
        }

        //将服务树中指定Channel所有通道list出来
        private List<ChannelTreeItemViewModel> GetChannels(IList<ServerTreeItemViewModel> servers, WireType wiretype)
        {
            if (servers == null || servers.Count == 0)
            {
                return null;
            }
            List<ChannelTreeItemViewModel> channels = new List<ChannelTreeItemViewModel>();

            foreach (var server in servers)
            {
                //htzk123,正常需要启用
                //if (server is ServerTreeItemViewModel && server.Parent == null)
                //{
                //    if ((server as ServerTreeItemViewModel).ServerIP != serverip)
                //    {
                //        continue;
                //    }
                //}
                
                if (wiretype == WireType.Wireless)
                {
                    if (server is WireMatchingCardTreeItemViewModel)
                    {
                        continue;
                    }
                }
                else if (wiretype == WireType.Wire)
                {                    
                    if (server is WirelessReceiveCardTreeItemViewModel)
                    {
                        continue;
                    }
                }

                if (server is ChannelTreeItemViewModel)
                {
                    channels.Add(server as ChannelTreeItemViewModel);
                }                
                else
                {
                    var childs = GetChannels(server.Children, wiretype);
                    if (childs != null)
                    {
                        channels.AddRange(childs);
                    }
                }
            }
            return channels;
        }

        //将服务树中所有主板list出来
        public List<MainCardTreeItemViewModel> GetMainCards(IList<ServerTreeItemViewModel> servers)
        {
            if (servers == null || servers.Count == 0)
            {
                return null;
            }
            List<MainCardTreeItemViewModel> maincards = new List<MainCardTreeItemViewModel>();

            foreach (var server in servers)
            {
                if (server is MainCardTreeItemViewModel)
                {
                    maincards.Add(server as MainCardTreeItemViewModel);
                }
                else
                {
                    var childs = GetMainCards(server.Children);
                    if (childs != null)
                    {
                        maincards.AddRange(childs);
                    }
                }
            }
            return maincards;
        }

        //得到硬件中指定通道
        public IChannel GetHardwareChannel(IList<ServerTreeItemViewModel> servers, T1_Item item)
        {
            if (servers == null || servers.Count == 0)
            {
                return null;
            }

            foreach (var server in servers)
            {
                if (server.ServerIP == item.ServerIP) //服务器IP匹配20180306
                {
                    foreach (var maincard in server.Children)
                    {
                        if ((maincard as MainCardTreeItemViewModel).MainControlCardIP == item.IP)
                        {
                            if ((item.ItemType == (int)ChannelType.WirelessScalarChannelInfo)
                                || (item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo))
                            {
                                var cards = (maincard as MainCardTreeItemViewModel).WirelessReceiveCard.TransmissionCard;
                                return GetHardwareChannel(cards, item.SlaveIdentifier, item.SlotNum.Value, item.CHNum.Value);//htzk123ICard
                            }
                            else
                            {
                                var cards = (maincard as MainCardTreeItemViewModel).WireMatchingCard;
                                return GetHardwareChannel(cards, item.CardNum.Value, item.SlotNum.Value, item.CHNum.Value);//htzk123ICard
                            }
                        }
                    }
                }
            }

            return null;
        }

        //得到硬件中指定通道
        public IChannel GetHardwareChannel(IList<ServerTreeItemViewModel> servers, string serverip, string ip, int cardNum, int slotNum, int chnNum)
        {
            if (servers == null || servers.Count == 0)
            {
                return null;
            }

            foreach (var server in servers)
            {
                if (server.ServerIP == serverip)//服务器IP匹配20180306
                {
                    foreach (var maincard in server.Children)
                    {
                        if ((maincard as MainCardTreeItemViewModel).MainControlCardIP == ip)
                        {
                            var cards = (maincard as MainCardTreeItemViewModel).WireMatchingCard;
                            return GetHardwareChannel(cards, cardNum, slotNum, chnNum);//htzk123ICard
                        }
                    }
                }
            }

            return null;
        }

        //得到硬件中指定通道
        public IChannel GetHardwareChannel(IList<WireMatchingCard> cards, int cardNum, int slotNum, int chnNum)
        {
            var card = (from p in cards where p.CardNum == cardNum select p).FirstOrDefault();
            IChannel channel;
            if (card.IEPESlot.SlotNum == slotNum)
            {
                channel = (from p in card.IEPESlot.IEPEChannelInfo where p.CHNum == chnNum select p).FirstOrDefault();
            }
            else if (card.EddyCurrentDisplacementSlot.SlotNum == slotNum)
            {
                channel = (from p in card.EddyCurrentDisplacementSlot.EddyCurrentDisplacementChannelInfo where p.CHNum == chnNum select p).FirstOrDefault();
            }
            else if (card.EddyCurrentKeyPhaseSlot.SlotNum == slotNum)
            {
                channel = (from p in card.EddyCurrentKeyPhaseSlot.EddyCurrentKeyPhaseChannelInfo where p.CHNum == chnNum select p).FirstOrDefault();
            }
            else if (card.EddyCurrentTachometerSlot.SlotNum == slotNum)
            {
                channel = (from p in card.EddyCurrentTachometerSlot.EddyCurrentTachometerChannelInfo where p.CHNum == chnNum select p).FirstOrDefault();
            }
            else if (card.DigitTachometerSlot.SlotNum == slotNum)
            {
                channel = (from p in card.DigitTachometerSlot.DigitTachometerChannelInfo where p.CHNum == chnNum select p).FirstOrDefault();
            }
            else if (card.AnalogRransducerInSlot.SlotNum == slotNum)
            {
                channel = (from p in card.AnalogRransducerInSlot.AnalogRransducerInChannelInfo where p.CHNum == chnNum select p).FirstOrDefault();
            }
            else if (card.RelaySlot.SlotNum == slotNum)
            {
                channel = (from p in card.RelaySlot.RelayChannelInfo where p.CHNum == chnNum select p).FirstOrDefault();
            }
            else if (card.DigitRransducerInSlot.SlotNum == slotNum)
            {
                channel = (from p in card.DigitRransducerInSlot.DigitRransducerInChannelInfo where p.CHNum == chnNum select p).FirstOrDefault();
            }
            else if (card.DigitRransducerOutSlot.SlotNum == slotNum)
            {
                channel = (from p in card.DigitRransducerOutSlot.DigitRransducerOutChannelInfo where p.CHNum == chnNum select p).FirstOrDefault();
            }
            else if (card.AnalogRransducerOutSlot.SlotNum == slotNum)
            {
                channel = (from p in card.AnalogRransducerOutSlot.AnalogRransducerOutChannelInfo where p.CHNum == chnNum select p).FirstOrDefault();
            }
            else
            {
                return null;
            }

            return channel;
        }

        //得到硬件中指定通道
        public IChannel GetHardwareChannel(IList<TransmissionCard> cards, string slaveIdentifier, int slotNum, int chnNum)
        {
            var card = (from p in cards where p.SlaveIdentifier == slaveIdentifier select p).FirstOrDefault();
            IChannel channel;
            if (card.WirelessScalarSlot.SlotNum == slotNum)
            {
                channel = (from p in card.WirelessScalarSlot.WirelessScalarChannelInfo where p.CHNum == chnNum select p).FirstOrDefault();
            }
            else if (card.WirelessVibrationSlot.SlotNum == slotNum)
            {
                channel = (from p in card.WirelessVibrationSlot.WirelessVibrationChannelInfo where p.CHNum == chnNum select p).FirstOrDefault();
            }         
            else
            {
                return null;
            }

            return channel;
        }

        //查找服务树上的通道
        public ChannelTreeItemViewModel GetChannel(IList<ServerTreeItemViewModel> servers, T1_Item item)
        {
            if (servers == null || servers.Count == 0)
            {
                return null;
            }           

            if ((item.ItemType == (int)ChannelType.WirelessScalarChannelInfo)
                             || (item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo))
            {
                var channels = GetChannels(servers, WireType.Wireless);
                ChannelTreeItemViewModel channel = (from p in channels where
                                                    (p.Parent.Parent.Parent.Parent as MainCardTreeItemViewModel).MainControlCardIP == item.IP &&
                                                    (p.Parent.Parent as TransmissionCardTreeItemViewModel).SlaveIdentifier == item.SlaveIdentifier && 
                                                    (p.Parent as SlotTreeItemViewModel).SlotNum == item.SlotNum &&                                                     
                                                    p.CHNum == item.CHNum &&
                                                    p.ServerIP == item.ServerIP//服务器IP匹配20180306
                                                    select p).FirstOrDefault();
                return channel;
            }
            else
            {
                var channels = GetChannels(servers, WireType.Wire);
                ChannelTreeItemViewModel channel = (from p in channels
                                                    where (p.Parent.Parent.Parent as MainCardTreeItemViewModel).MainControlCardIP == item.IP
                                                    && (p.Parent.Parent as WireMatchingCardTreeItemViewModel).CardNum == item.CardNum
                                                    && (p.Parent as SlotTreeItemViewModel).SlotNum == item.SlotNum
                                                    && p.CHNum == item.CHNum &&
                                                    p.ServerIP == item.ServerIP//服务器IP匹配20180306
                                                    select p).FirstOrDefault();
                return channel;
            }            
        }

        //查找服务树上的通道
        private ChannelTreeItemViewModel GetChannel(IList<ServerTreeItemViewModel> servers, string ip, int cardNum, int slotNum, int chnNum)
        {
            if (servers == null || servers.Count == 0)
            {
                return null;
            }           

            var channels = from p in GetChannels(servers) select p;

            ChannelTreeItemViewModel channel = (from p in channels
                where (p.Parent.Parent.Parent as MainCardTreeItemViewModel).MainControlCardIP == ip
                && (p.Parent.Parent as WireMatchingCardTreeItemViewModel).CardNum == cardNum
                && (p.Parent as SlotTreeItemViewModel).SlotNum == slotNum
                && p.CHNum == chnNum
                select p).FirstOrDefault();

            return channel;
        }
        
        //添加分频时,根据测点和通道得到分频信息
        public DivFreInfo GetDivFreInfo(IChannel i_channel, DivFreTreeItemViewModel divfre, ItemTreeItemViewModel item)
        {
            DivFreInfo divfreinfo = null;
            if (i_channel is VibrationChannelInfo)
            {
                divfreinfo = new DivFreInfo();
                //CardCopyHelper.DivFreInfoLeftCopyToRight((i_channel as VibrationChannelInfo).DivFreInfo[0], divfreinfo);
            }           
            if (divfreinfo != null)
            {
                var divfreinfo_copy = (i_channel as VibrationChannelInfo).DivFreInfo[0];
                divfreinfo.BasedOnRPM = divfreinfo_copy.BasedOnRPM;
                divfreinfo.FixedFre = divfreinfo_copy.FixedFre;
                divfreinfo.BasedOnRange = divfreinfo_copy.BasedOnRange;
                if (divfreinfo.AlarmStrategy == null)
                {
                    divfreinfo.AlarmStrategy = new AlarmStrategy();
                }
                if (divfreinfo.AlarmStrategy.Absolute == null)
                {
                    divfreinfo.AlarmStrategy.Absolute = new AbsoluteAlarm();
                }
                if (divfreinfo.AlarmStrategy.Comparative == null)
                {
                    divfreinfo.AlarmStrategy.Comparative = new ComparativeAlarm();
                }
                divfreinfo.AlarmStrategy.Absolute.Category = divfreinfo_copy.AlarmStrategy.Absolute.Category;
                divfreinfo.AlarmStrategy.Absolute.Para = divfreinfo_copy.AlarmStrategy.Absolute.Para;
                divfreinfo.AlarmStrategy.Absolute.Mode = divfreinfo_copy.AlarmStrategy.Absolute.Mode;
                divfreinfo.AlarmStrategy.Absolute.ModeCode = divfreinfo_copy.AlarmStrategy.Absolute.ModeCode;
                divfreinfo.AlarmStrategy.Comparative.Range = divfreinfo_copy.AlarmStrategy.Comparative.Range;
                divfreinfo.AlarmStrategy.Comparative.IntevalTime = divfreinfo_copy.AlarmStrategy.Comparative.IntevalTime;
                divfreinfo.AlarmStrategy.Comparative.Percent = divfreinfo_copy.AlarmStrategy.Comparative.Percent;
                divfreinfo.AlarmStrategy.Comparative.IsAllow = divfreinfo_copy.AlarmStrategy.Comparative.IsAllow;
                divfreinfo.AlarmStrategy.Comparative.Para = divfreinfo_copy.AlarmStrategy.Comparative.Para;

                divfreinfo.Guid = divfre.T_Organization.Guid.ToString();
                divfreinfo.Code = divfre.T_Organization.Code;
                divfreinfo.Name = divfre.T_Organization.Name;
                divfreinfo.Create_Time = divfre.T_Organization.Create_Time.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                divfreinfo.Modify_Time = divfre.T_Organization.Modify_Time.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                divfreinfo.Remarks = divfre.T_Organization.Remarks;
                divfreinfo.T_Item_Guid = item.T_Item.Guid.ToString();
                divfreinfo.T_Item_Name = item.T_Item.Name;
                divfreinfo.T_Item_Code = item.T_Item.Code;
                divfreinfo.DivFreCode = 0;
                return divfreinfo;
            }
            return null;
        }

        //将组织树中所有组织list出来
        public List<OrganizationTreeItemViewModel> GetOrganizations(OrganizationTreeItemViewModel organization)
        {
            if (organization == null)
            {
                return null;
            }
            List<OrganizationTreeItemViewModel> organizationlist = new List<OrganizationTreeItemViewModel>();
            organizationlist.Add(organization);
            var childs = GetOrganizations(organization.Children);
            if (childs != null)
            {
                organizationlist.AddRange(childs);
            }        
            return organizationlist;
        }

        public List<OrganizationTreeItemViewModel> GetOrganizations(IList<OrganizationTreeItemViewModel> organizations)
        {
            if (organizations == null || organizations.Count == 0)
            {
                return null;
            }
            List<OrganizationTreeItemViewModel> organizationlist = new List<OrganizationTreeItemViewModel>();

            foreach (var organization in organizations)
            {
                organizationlist.Add(organization);

                var childs = GetOrganizations(organization.Children);
                if (childs != null)
                {
                    organizationlist.AddRange(childs);
                }
            }
            return organizationlist;
        }

        public List<OrganizationTreeItemViewModel> GetCopyOrganizations(OrganizationTreeItemViewModel organization, OrganizationTreeItemViewModel parentorganization)
        {
            if (organization == null)
            {
                return null;
            }
            List<OrganizationTreeItemViewModel> organizationlist = new List<OrganizationTreeItemViewModel>();
            organizationlist.Add(organization);
            if (parentorganization != null)
            {
                organization.T_Organization.CopyTemp(parentorganization.T_Organization);
            }
            else
            {
                organization.T_Organization.CopyTemp(null);
            }

            var childs = GetCopyOrganizations(organization.Children);
            if (childs != null)
            {
                organizationlist.AddRange(childs);
            }
            return organizationlist;
        }

        public List<OrganizationTreeItemViewModel> GetCopyOrganizations(IList<OrganizationTreeItemViewModel> organizations)
        {
            if (organizations == null || organizations.Count == 0)
            {
                return null;
            }
            List<OrganizationTreeItemViewModel> organizationlist = new List<OrganizationTreeItemViewModel>();

            foreach (var organization in organizations)
            {
                organizationlist.Add(organization);
                if (organization.Parent != null)
                {
                    organization.T_Organization.CopyTemp(organization.Parent.T_Organization.TempData);
                }
                else
                {
                    organization.T_Organization.CopyTemp(null);
                }

                var childs = GetCopyOrganizations(organization.Children);
                if (childs != null)
                {
                    organizationlist.AddRange(childs);
                }
            }
            return organizationlist;
        }

        //将组织树中所有测点list出来
        public List<ItemTreeItemViewModel> GetItems(IList<OrganizationTreeItemViewModel> organizations)
        {
            if (organizations == null || organizations.Count == 0)
            {
                return null;
            }
            List<ItemTreeItemViewModel> items = new List<ItemTreeItemViewModel>();

            foreach (var organization in organizations)
            {
                if (organization is ItemTreeItemViewModel && !(organization is DivFreTreeItemViewModel))
                {
                    items.Add(organization as ItemTreeItemViewModel);
                }
                var childs = GetItems(organization.Children);
                if (childs != null)
                {
                    items.AddRange(childs);
                }
            }
            return items;
        }

        //将组织树中所有测点list出来
        public List<ItemTreeItemViewModel> GetItems(OrganizationTreeItemViewModel organization)
        {
            if (organization == null)
            {
                return null;
            }
            List<ItemTreeItemViewModel> items = new List<ItemTreeItemViewModel>();

            if (organization is ItemTreeItemViewModel && !(organization is DivFreTreeItemViewModel))
            {
                items.Add(organization as ItemTreeItemViewModel);
            }
            var childs = GetItems(organization.Children);
            if (childs != null)
            {
                items.AddRange(childs);
            }

            return items;
        }

        //将组织树中所有设备list出来
        public List<DeviceTreeItemViewModel> GetDevices(IList<OrganizationTreeItemViewModel> organizations)
        {
            if (organizations == null || organizations.Count == 0)
            {
                return null;
            }
            List<DeviceTreeItemViewModel> items = new List<DeviceTreeItemViewModel>();

            foreach (var organization in organizations)
            {
                if (organization is DeviceTreeItemViewModel && !(organization is ItemTreeItemViewModel) && !(organization is DivFreTreeItemViewModel))
                {
                    items.Add(organization as DeviceTreeItemViewModel);
                }
                var childs = GetDevices(organization.Children);
                if (childs != null)
                {
                    items.AddRange(childs);
                }
            }
            return items;
        }
        //将组织树中所有设备list出来
        public List<DeviceTreeItemViewModel> GetDevices(OrganizationTreeItemViewModel organization)
        {
            if (organization == null)
            {
                return null;
            }
            List<DeviceTreeItemViewModel> items = new List<DeviceTreeItemViewModel>();

            if (organization is DeviceTreeItemViewModel && !(organization is ItemTreeItemViewModel) && !(organization is DivFreTreeItemViewModel))
            {
                items.Add(organization as DeviceTreeItemViewModel);
            }
            var childs = GetDevices(organization.Children);
            if (childs != null)
            {
                items.AddRange(childs);
            }

            return items;
        }

        //删除到回收站 
        public ItemTreeItemViewModel RecycleRecycledItem(IList<OrganizationTreeItemViewModel> recycled, ItemTreeItemViewModel item)
        {
            if (recycled == null && recycled.Count < 1)
            {
                return null;
            }
            ItemTreeItemViewModel recy_item = new ItemTreeItemViewModel().RecycledItemTreeItem(item);
            recycled[0].AddChild(recy_item);
            return recy_item;
        }
        public List<ItemTreeItemViewModel> RecycleRecycledItem(IList<OrganizationTreeItemViewModel> recycled, IList<ItemTreeItemViewModel> items)
        {
            if (recycled == null && recycled.Count < 1)
            {
                return null;
            }
            List<ItemTreeItemViewModel> itemlist = new List<ItemTreeItemViewModel>();
            foreach (var item in items)
            {
                itemlist.Add(RecycleRecycledItem(recycled, item));
            }
            return itemlist;
        }
        //从回收站删除
        public void RecycleDeleteItem(IList<OrganizationTreeItemViewModel> recycled, ItemTreeItemViewModel item)
        {
            if (recycled == null && recycled.Count < 1)
            {
                return;
            }
            recycled[0].RemoveChild(item);
        }

        //去除无权限的组织机构
        public void GetOrganizationVisibility(IList<OrganizationTreeItemViewModel> organizations, IList<T1_OrganizationPrivilege> t_organizationPrivilege)
        {
            if (organizations == null || organizations.Count == 0)
            {
                return;
            }

            for (int i = organizations.Count - 1; i >= 0; i--)
            {
                if (organizations[i] is ItemTreeItemViewModel || organizations[i] is DivFreTreeItemViewModel)
                {
                    continue;
                }
                var t_organization = (from p in t_organizationPrivilege where p.T_Organization_Guid == organizations[i].T_Organization.Guid select p).FirstOrDefault();
                if (t_organization == null)
                {
                    if (organizations[i].T_Organization.Level <= 0)
                    {
                        organizations.Remove(organizations[i]);
                    }
                    else
                    {
                        organizations[i].Parent.RemoveChild(organizations[i]);
                    }
                }
                else
                {
                    GetOrganizationVisibility(organizations[i].Children, t_organizationPrivilege);
                }
            }
        }
        
        //将所有父组织list出来
        public List<Organization> GetParentOrganizations(OrganizationTreeItemViewModel organization)
        {
            if (organization == null)
            {
                return null;
            }
            List<Organization> organizationlist = new List<Organization>();
            organizationlist.Add(OrganizationConvert(organization.T_Organization));
            if (organization.Parent == null)
            {
                return organizationlist;
            }

            var parent = organization.Parent;
            while (parent != null)
            {
                organizationlist.Add(OrganizationConvert(parent.T_Organization));
                parent = parent.Parent;
            }

            return organizationlist;
        }

        //将所有父组织list出来
        public string GetOrganizationName(OrganizationTreeItemViewModel organization)
        {
            if (organization == null)
            {
                return null;
            }

            if (organization.Parent == null)
            {
                return organization.Name;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(organization.Name);           
            var parent = organization.Parent;
            while (parent != null)
            {
                sb.Insert(0, parent.Name + ">");
                parent = parent.Parent;
            }

            return sb.ToString();
        }

        //将所有父组织list出来
        public string GetOrganizationServer(OrganizationTreeItemViewModel organization)
        {
            if (organization == null)
            {
                return null;
            }
          
            if (organization.Parent == null)
            {
                return organization.ServerIP;
            }

            var parent = organization.Parent;
            while (parent != null)
            {
                if (parent.Parent == null)
                {
                    break;
                }
                parent = parent.Parent;
            }

            return parent.ServerIP;
        }

        //将所有子组织list出来
        public List<T1_Organization> GetChildOrganizations(OrganizationTreeItemViewModel organization)
        {
            if (organization == null)
            {
                return null;
            }
            List<T1_Organization> organizationlist = new List<T1_Organization>();
            organizationlist.Add(organization.T_Organization);
            if (organization.Children == null || organization.Children.Count == 0)
            {
                return organizationlist;
            }
            
            foreach(var child in organization.Children)
            {
                if (child.Children != null)
                {
                    organizationlist.AddRange(GetChildOrganizations(child));
                }
            }           

            return organizationlist;
        }

        //组织机构转换
        public Organization OrganizationConvert(T1_Organization t_organization)
        {
            Organization organization = new Organization();
            organization.Name = t_organization.Name;
            organization.Code = t_organization.Code;
            organization.Guid = t_organization.Guid.ToString();
            organization.Level = t_organization.Level;
            organization.Create_Time = t_organization.Create_Time.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
            organization.Modify_Time = t_organization.Modify_Time.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
            organization.Parent_Code = t_organization.Code;
            organization.Parent_Guid = t_organization.Guid.ToString();
            organization.Parent_Level = t_organization.Level;
            return organization;
        }

        public string SameNameChecked(IList<T1_Organization> organizations, OrganizationTreeItemViewModel organization, bool IsNew = false)
        {            
            string rename = organization.Name;
            string newname = rename;
            IEnumerable<string> names;

            if (organization.T_Organization.Level == 0)
            {
                if (IsNew == false)
                {
                    names = organizations.Where(p => p.Level == 0 && p.Guid != organization.T_Organization.Guid).Select(p => p.Name);
                }
                else//特别为Copy节点准备
                {
                    names = organizations.Where(p => p.Level == 0).Select(p => p.Name);
                }
            }
            else
            {
                if (IsNew == false)
                {
                    names = organizations.Where(p => p.Parent_Guid == organization.T_Organization.Parent_Guid && p.Guid != organization.T_Organization.Guid).Select(p => p.Name);
                }
                else//特别为Copy节点准备
                {
                    names = organizations.Where(p => p.Parent_Guid == organization.T_Organization.Parent_Guid).Select(p => p.Name);
                }
            }
            while (true)
            {
                foreach (var name in names)
                {
                    if (name == rename)
                    {
                        newname = rename + "-1";
                        break;
                    }
                }
                if (newname == rename)
                {
                    return newname;
                }
                rename = newname;
            }
        }

        public List<DivFreInfo> GetDivFreInfoListWithoutTemp(DivFreIChannel ichannel)
        {
            List<DivFreInfo> divFreInfolist = new List<DivFreInfo>();
            if (ichannel == null)
            {
                return divFreInfolist;
            }
            var divfreinfos = ichannel.DivFreInfo;
            if (divfreinfos != null)
            {
                foreach (var divfreinfo in divfreinfos)
                {
                    if (divfreinfo.DivFreCode == -1)
                    {
                        continue;
                    }
                    divFreInfolist.Add(divfreinfo);
                }
            }
            return divFreInfolist;
        }

        public ItemTreeItemViewModel GetRecycledItem(string ip, ChannelTreeItemViewModel channeltree, OrganizationTreeItemViewModel recycledTreeItem)
        {
            if (channeltree.IChannel is WirelessVibrationChannelInfo || channeltree.IChannel is WirelessScalarChannelInfo)
            {
                var slaveIdentifier = (channeltree.Parent.Parent as TransmissionCardTreeItemViewModel).TransmissionCard.SlaveIdentifier;
                var slotNum = (channeltree.Parent as SlotTreeItemViewModel).SlotNum;
                var chNum = channeltree.CHNum;
                //var channelHDID = ip + "_" + slaveIdentifier + "_" + slotNum + "_" + chNum;
                var channelHDID = slaveIdentifier.PadLeft(4, '0') + "_" + slotNum + "_" + chNum + "_0" + "@" + ip;
                var recycledItem = recycledTreeItem.Children.Where(p => (p as ItemTreeItemViewModel).T_Item.ChannelHDID == channelHDID).OrderByDescending(p => (p as ItemTreeItemViewModel).T_Item.Modify_Time).Select(p => p as ItemTreeItemViewModel).FirstOrDefault();
                return recycledItem;
            }
            else
            {
                var cardNum = (channeltree.Parent.Parent as WireMatchingCardTreeItemViewModel).CardNum;
                var slotNum = (channeltree.Parent as SlotTreeItemViewModel).SlotNum;
                var chNum = channeltree.CHNum;
                //var channelHDID = ip + "_" + cardNum + "_" + slotNum + "_" + chNum;
                var channelHDID = cardNum.ToString("0000") + "_" + slotNum + "_" + chNum + "_0" + "@" + ip;
                var recycledItem = recycledTreeItem.Children.Where(p => (p as ItemTreeItemViewModel).T_Item.ChannelHDID == channelHDID).OrderByDescending(p => (p as ItemTreeItemViewModel).T_Item.Modify_Time).Select(p => p as ItemTreeItemViewModel).FirstOrDefault();
                return recycledItem;
            }            
        }

        public Organization GetNewOrganization(ItemTreeItemViewModel itemtree, ItemTreeItemViewModel recycleditemtree)
        {
            Organization organization = new Organization();
            organization.Name = itemtree.T_Organization.Name;
            organization.Code = recycleditemtree.T_Item.Code;//维持原先
            organization.Guid = recycleditemtree.T_Item.Guid.ToString();//维持原先
            organization.Level = itemtree.T_Organization.Level;
            organization.Create_Time = recycleditemtree.T_Item.Create_Time.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");//维持原先
            organization.Modify_Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            organization.Parent_Code = itemtree.T_Organization.Parent_Code;
            organization.Parent_Guid = itemtree.T_Organization.Parent_Guid.ToString();
            organization.Parent_Level = itemtree.T_Organization.Parent_Level.Value;
            return organization;
        }

        public OrganizationTreeItemViewModel GetSelectedTree(IList<OrganizationTreeItemViewModel> organizations)
        {
            if (organizations == null || organizations.Count == 0)
            {
                return null;
            }

            foreach (var organization in organizations)
            {
                if (organization.IsSelected == true)
                {
                    return organization;                  
                }

                var child = GetSelectedTree(organization.Children);
                if (child != null)
                { 
                    return child;
                }
            }
            return null;
        }
    }
}

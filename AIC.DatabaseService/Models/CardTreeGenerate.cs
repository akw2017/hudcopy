using AIC.Core.HardwareModels;
using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DatabaseService.Models
{
    public class CardTreeGenerate
    {
        public static IList<ServerTreeItemViewModel> AddCardServerTree(IList<ServerTreeItemViewModel> server_nodes, string ServerIP)
        {
            if (server_nodes == null)
            {
                server_nodes = new List<ServerTreeItemViewModel>();
            }
            //服务器
            ServerTreeItemViewModel server_node = (from p in server_nodes where p.ServerIP == ServerIP select p).FirstOrDefault();
            if (server_node == null)
            {
                server_node = new ServerTreeItemViewModel(ServerIP);
                server_node.ServerIP = ServerIP;
                server_node.IsExpanded = true;
                server_nodes.Add(server_node);
            }

            return server_nodes;
        }

        public static List<ChannelTreeItemViewModel> GetCardTree(IList<ServerTreeItemViewModel> server_nodes, RootCard root, string ServerIP, string MainControlCardIP)
        {
            List<ChannelTreeItemViewModel> channels = new List<ChannelTreeItemViewModel>();
            if (server_nodes == null)
            {
                server_nodes = new List<ServerTreeItemViewModel>();
            }

            //服务器
            ServerTreeItemViewModel server_node = (from p in server_nodes where p.ServerIP == ServerIP select p).FirstOrDefault();
            if (server_node == null)
            {
                server_node = new ServerTreeItemViewModel(ServerIP);
                server_node.ServerIP = ServerIP;
                server_node.IsExpanded = true;
                server_nodes.Add(server_node);
            }

            //主板
            MainCardTreeItemViewModel maincard_node = (from p in server_node.Children where (p as MainCardTreeItemViewModel).MainControlCardIP == MainControlCardIP select p as MainCardTreeItemViewModel).FirstOrDefault();
            if (maincard_node == null)
            {
                maincard_node = new MainCardTreeItemViewModel(MainControlCardIP);
                maincard_node.MainControlCardIP = MainControlCardIP;
                maincard_node.MainControlCard = root.MainControlCard;
                maincard_node.WireMatchingCard = root.WireMatchingCard;
                maincard_node.WirelessReceiveCard = root.WirelessReceiveCard;
                maincard_node.IsExpanded = true;

                server_node.AddChild(maincard_node);
            }
            #region 有线板卡
            if (root.WireMatchingCard != null)
            {
                foreach (var card in root.WireMatchingCard)
                {
                    WireMatchingCardTreeItemViewModel card_node = (from p in maincard_node.Children.OfType<WireMatchingCardTreeItemViewModel>() where p.WireMatchingCard.CardNum == card.CardNum select p).FirstOrDefault();
                    if (card_node == null)
                    {
                        card_node = new WireMatchingCardTreeItemViewModel(card);
                        maincard_node.AddChild(card_node);
                    }

                    channels.AddRange(GetWireMatchingCardTree(card_node, card));
                }
            }
            #endregion
            #region 无线接收卡
            if (root.WirelessReceiveCard != null)
            {
                WirelessReceiveCardTreeItemViewModel wirelesscard_node = (from p in maincard_node.Children.OfType<WirelessReceiveCardTreeItemViewModel>() where p.WirelessReceiveCard.MasterIdentifier == root.WirelessReceiveCard.MasterIdentifier select p).FirstOrDefault();
                if (wirelesscard_node == null)
                {
                    wirelesscard_node = new WirelessReceiveCardTreeItemViewModel(root.WirelessReceiveCard);
                    maincard_node.AddChild(wirelesscard_node);
                }
                if (root.WirelessReceiveCard.TransmissionCard != null)
                {
                    foreach (var card in root.WirelessReceiveCard.TransmissionCard)
                    {
                        channels.AddRange(GetWirelessReceiveCardTree(wirelesscard_node, card));
                    }
                }
            }
            #endregion

            return channels;
        }

        public static List<ChannelTreeItemViewModel> GetWireMatchingCardTree(WireMatchingCardTreeItemViewModel card_node, WireMatchingCard card)
        {
            List<ChannelTreeItemViewModel> channels = new List<ChannelTreeItemViewModel>();
            {
                var slot = card.IEPESlot;
                if (slot != null)
                {
                    SlotTreeItemViewModel slot_node = (from p in card_node.Children.OfType<SlotTreeItemViewModel>() where p.SlotNum == slot.SlotNum select p).FirstOrDefault();
                    if (slot_node == null)
                    {
                        slot_node = new SlotTreeItemViewModel(slot);
                        card_node.AddChild(slot_node);
                    }

                    foreach (var channel in slot.IEPEChannelInfo)
                    {
                        ChannelTreeItemViewModel channel_node = (from p in slot_node.Children.OfType<ChannelTreeItemViewModel>() where p.CHNum == channel.CHNum select p).FirstOrDefault();
                        if (channel_node == null)
                        {
                            channel_node = new ChannelTreeItemViewModel(channel);
                            slot_node.AddChild(channel_node);
                            channels.Add(channel_node);
                        }
                    }
                }
            }

            {
                var slot = card.EddyCurrentDisplacementSlot;
                if (slot != null)
                {
                    SlotTreeItemViewModel slot_node = (from p in card_node.Children.OfType<SlotTreeItemViewModel>() where p.SlotNum == slot.SlotNum select p).FirstOrDefault();
                    if (slot_node == null)
                    {
                        slot_node = new SlotTreeItemViewModel(slot);
                        card_node.AddChild(slot_node);
                    }

                    foreach (var channel in slot.EddyCurrentDisplacementChannelInfo)
                    {
                        ChannelTreeItemViewModel channel_node = (from p in slot_node.Children.OfType<ChannelTreeItemViewModel>() where p.CHNum == channel.CHNum select p).FirstOrDefault();
                        if (channel_node == null)
                        {
                            channel_node = new ChannelTreeItemViewModel(channel);
                            slot_node.AddChild(channel_node);
                            channels.Add(channel_node);
                        }
                    }
                }
            }

            {
                var slot = card.EddyCurrentKeyPhaseSlot;
                if (slot != null)
                {
                    SlotTreeItemViewModel slot_node = (from p in card_node.Children.OfType<SlotTreeItemViewModel>() where p.SlotNum == slot.SlotNum select p).FirstOrDefault();
                    if (slot_node == null)
                    {
                        slot_node = new SlotTreeItemViewModel(slot);
                        card_node.AddChild(slot_node);
                    }

                    foreach (var channel in slot.EddyCurrentKeyPhaseChannelInfo)
                    {
                        ChannelTreeItemViewModel channel_node = (from p in slot_node.Children.OfType<ChannelTreeItemViewModel>() where p.CHNum == channel.CHNum select p).FirstOrDefault();
                        if (channel_node == null)
                        {
                            channel_node = new ChannelTreeItemViewModel(channel);
                            slot_node.AddChild(channel_node);
                            channels.Add(channel_node);
                        }
                    }
                }
            }

            {
                var slot = card.EddyCurrentTachometerSlot;
                if (slot != null)
                {
                    SlotTreeItemViewModel slot_node = (from p in card_node.Children.OfType<SlotTreeItemViewModel>() where p.SlotNum == slot.SlotNum select p).FirstOrDefault();
                    if (slot_node == null)
                    {
                        slot_node = new SlotTreeItemViewModel(slot);
                        card_node.AddChild(slot_node);
                    }

                    foreach (var channel in slot.EddyCurrentTachometerChannelInfo)
                    {
                        ChannelTreeItemViewModel channel_node = (from p in slot_node.Children.OfType<ChannelTreeItemViewModel>() where p.CHNum == channel.CHNum select p).FirstOrDefault();
                        if (channel_node == null)
                        {
                            channel_node = new ChannelTreeItemViewModel(channel);
                            slot_node.AddChild(channel_node);
                            channels.Add(channel_node);
                        }
                    }
                }
            }

            {
                var slot = card.DigitTachometerSlot;
                if (slot != null)
                {
                    SlotTreeItemViewModel slot_node = (from p in card_node.Children.OfType<SlotTreeItemViewModel>() where p.SlotNum == slot.SlotNum select p).FirstOrDefault();
                    if (slot_node == null)
                    {
                        slot_node = new SlotTreeItemViewModel(slot);
                        card_node.AddChild(slot_node);
                    }

                    foreach (var channel in slot.DigitTachometerChannelInfo)
                    {
                        ChannelTreeItemViewModel channel_node = (from p in slot_node.Children.OfType<ChannelTreeItemViewModel>() where p.CHNum == channel.CHNum select p).FirstOrDefault();
                        if (channel_node == null)
                        {
                            channel_node = new ChannelTreeItemViewModel(channel);
                            slot_node.AddChild(channel_node);
                            channels.Add(channel_node);
                        }
                    }
                }
            }

            {
                var slot = card.AnalogRransducerInSlot;
                if (slot != null)
                {
                    SlotTreeItemViewModel slot_node = (from p in card_node.Children.OfType<SlotTreeItemViewModel>() where p.SlotNum == slot.SlotNum select p).FirstOrDefault();
                    if (slot_node == null)
                    {
                        slot_node = new SlotTreeItemViewModel(slot);
                        card_node.AddChild(slot_node);
                    }

                    foreach (var channel in slot.AnalogRransducerInChannelInfo)
                    {
                        ChannelTreeItemViewModel channel_node = (from p in slot_node.Children.OfType<ChannelTreeItemViewModel>() where p.CHNum == channel.CHNum select p).FirstOrDefault();
                        if (channel_node == null)
                        {
                            channel_node = new ChannelTreeItemViewModel(channel);
                            slot_node.AddChild(channel_node);
                            channels.Add(channel_node);
                        }
                    }
                }
            }

            {
                var slot = card.RelaySlot;
                if (slot != null)
                {
                    SlotTreeItemViewModel slot_node = (from p in card_node.Children.OfType<SlotTreeItemViewModel>() where p.SlotNum == slot.SlotNum select p).FirstOrDefault();
                    if (slot_node == null)
                    {
                        slot_node = new SlotTreeItemViewModel(slot);
                        card_node.AddChild(slot_node);
                    }

                    foreach (var channel in slot.RelayChannelInfo)
                    {
                        ChannelTreeItemViewModel channel_node = (from p in slot_node.Children.OfType<ChannelTreeItemViewModel>() where p.CHNum == channel.CHNum select p).FirstOrDefault();
                        if (channel_node == null)
                        {
                            channel_node = new ChannelTreeItemViewModel(channel);
                            slot_node.AddChild(channel_node);
                            channels.Add(channel_node);
                        }
                    }
                }
            }

            {
                var slot = card.DigitRransducerInSlot;
                if (slot != null)
                {
                    SlotTreeItemViewModel slot_node = (from p in card_node.Children.OfType<SlotTreeItemViewModel>() where p.SlotNum == slot.SlotNum select p).FirstOrDefault();
                    if (slot_node == null)
                    {
                        slot_node = new SlotTreeItemViewModel(slot);
                        card_node.AddChild(slot_node);
                    }

                    foreach (var channel in slot.DigitRransducerInChannelInfo)
                    {
                        ChannelTreeItemViewModel channel_node = (from p in slot_node.Children.OfType<ChannelTreeItemViewModel>() where p.CHNum == channel.CHNum select p).FirstOrDefault();
                        if (channel_node == null)
                        {
                            channel_node = new ChannelTreeItemViewModel(channel);
                            slot_node.AddChild(channel_node);
                            channels.Add(channel_node);
                        }
                    }
                }
            }

            {
                var slot = card.DigitRransducerOutSlot;
                if (slot != null)
                {
                    SlotTreeItemViewModel slot_node = (from p in card_node.Children.OfType<SlotTreeItemViewModel>() where p.SlotNum == slot.SlotNum select p).FirstOrDefault();
                    if (slot_node == null)
                    {
                        slot_node = new SlotTreeItemViewModel(slot);
                        card_node.AddChild(slot_node);
                    }

                    foreach (var channel in slot.DigitRransducerOutChannelInfo)
                    {
                        ChannelTreeItemViewModel channel_node = (from p in slot_node.Children.OfType<ChannelTreeItemViewModel>() where p.CHNum == channel.CHNum select p).FirstOrDefault();
                        if (channel_node == null)
                        {
                            channel_node = new ChannelTreeItemViewModel(channel);
                            slot_node.AddChild(channel_node);
                            channels.Add(channel_node);
                        }
                    }
                }
            }

            {
                var slot = card.AnalogRransducerOutSlot;
                if (slot != null)
                {
                    SlotTreeItemViewModel slot_node = (from p in card_node.Children.OfType<SlotTreeItemViewModel>() where p.SlotNum == slot.SlotNum select p).FirstOrDefault();
                    if (slot_node == null)
                    {
                        slot_node = new SlotTreeItemViewModel(slot);
                        card_node.AddChild(slot_node);
                    }

                    foreach (var channel in slot.AnalogRransducerOutChannelInfo)
                    {
                        ChannelTreeItemViewModel channel_node = (from p in slot_node.Children.OfType<ChannelTreeItemViewModel>() where p.CHNum == channel.CHNum select p).FirstOrDefault();
                        if (channel_node == null)
                        {
                            channel_node = new ChannelTreeItemViewModel(channel);
                            slot_node.AddChild(channel_node);
                            channels.Add(channel_node);
                        }
                    }
                }
            }
            return channels;
        }

        public static List<ChannelTreeItemViewModel> GetWirelessReceiveCardTree(WirelessReceiveCardTreeItemViewModel wirelesscard_node, TransmissionCard card)
        {
            List<ChannelTreeItemViewModel> channels = new List<ChannelTreeItemViewModel>();

            TransmissionCardTreeItemViewModel card_node = (from p in wirelesscard_node.Children.OfType<TransmissionCardTreeItemViewModel>() where p.SlaveIdentifier == card.SlaveIdentifier select p).FirstOrDefault();
            if (card_node == null)
            {
                card_node = new TransmissionCardTreeItemViewModel(card);
                wirelesscard_node.AddChild(card_node);
            }

            {
                var slot = card.WirelessScalarSlot;
                if (slot != null)
                {
                    SlotTreeItemViewModel slot_node = (from p in card_node.Children.OfType<SlotTreeItemViewModel>() where p.SlotNum == slot.SlotNum select p).FirstOrDefault();
                    if (slot_node == null)
                    {
                        slot_node = new SlotTreeItemViewModel(slot);
                        card_node.AddChild(slot_node);
                    }

                    foreach (var channel in slot.WirelessScalarChannelInfo)
                    {
                        ChannelTreeItemViewModel channel_node = (from p in slot_node.Children.OfType<ChannelTreeItemViewModel>() where p.CHNum == channel.CHNum select p).FirstOrDefault();
                        if (channel_node == null)
                        {
                            channel_node = new ChannelTreeItemViewModel(channel);
                            slot_node.AddChild(channel_node);
                            channels.Add(channel_node);
                        }
                    }
                }
            }

            {
                var slot = card.WirelessVibrationSlot;
                if (slot != null)
                {
                    SlotTreeItemViewModel slot_node = (from p in card_node.Children.OfType<SlotTreeItemViewModel>() where p.SlotNum == slot.SlotNum select p).FirstOrDefault();
                    if (slot_node == null)
                    {
                        slot_node = new SlotTreeItemViewModel(slot);
                        card_node.AddChild(slot_node);
                    }

                    foreach (var channel in slot.WirelessVibrationChannelInfo)
                    {
                        ChannelTreeItemViewModel channel_node = (from p in slot_node.Children.OfType<ChannelTreeItemViewModel>() where p.CHNum == channel.CHNum select p).FirstOrDefault();
                        if (channel_node == null)
                        {
                            channel_node = new ChannelTreeItemViewModel(channel);
                            slot_node.AddChild(channel_node);
                            channels.Add(channel_node);
                        }
                    }
                }
            }
            return channels;
        }

        public static T1_RootCard GenerateRootCard(RootCard root, string MainControlCardIP)
        {
            T1_RootCard t_rootCard = new T1_RootCard();
            var _convertToDataBaseFormat = new ConvertToDataBaseFormat();

            //主板

            //转成数据库格式
            var t_maincard = _convertToDataBaseFormat.MainControlCardConvert(root.MainControlCard, MainControlCardIP);
            root.MainControlCard.T_MainControlCard = t_maincard; //添加引用          
            t_rootCard.T_MainControlCard.Add(t_maincard);
            string ip = MainControlCardIP;

            #region 有线板卡
            if (root.WireMatchingCard != null)
            {
                foreach (var card in root.WireMatchingCard)
                {
                    GenerateSubWireMatchingCard(card, ip, t_rootCard);
                }
            }
            #endregion
            #region 无线接收卡
            if (root.WirelessReceiveCard != null)
            {
                //转成数据库格式
                var t_wirelessReceiveCard = _convertToDataBaseFormat.WirelessReceiveCardConvert(root.WirelessReceiveCard, MainControlCardIP);
                root.WirelessReceiveCard.T_WirelessReceiveCard = t_wirelessReceiveCard;//添加引用
                t_rootCard.T_WirelessReceiveCard.Add(t_wirelessReceiveCard);
                string masterId = t_wirelessReceiveCard.MasterIdentifier;

                if (root.WirelessReceiveCard.TransmissionCard != null)
                {
                    foreach (var card in root.WirelessReceiveCard.TransmissionCard)
                    {
                        GenerateSubTransmissionCard(card, ip, masterId, t_rootCard);
                    }
                }
            }
            #endregion


            return t_rootCard;
        }

        public static T1_RootCard GenerateTransmissionCard(RootCard root, string MainControlCardIP, string identifier)
        {
            T1_RootCard t_rootCard = new T1_RootCard();
            var _convertToDataBaseFormat = new ConvertToDataBaseFormat();

            //主板

            //转成数据库格式
            //var t_maincard = _convertToDataBaseFormat.MainControlCardConvert(root.MainControlCard, MainControlCardIP);
            //root.MainControlCard.T_MainControlCard = t_maincard; //添加引用          
            //t_rootCard.T_MainControlCard.Add(t_maincard);
            string ip = MainControlCardIP;
          
            #region 无线接收卡
            if (root.WirelessReceiveCard != null)
            {
                //转成数据库格式
                //var t_wirelessReceiveCard = _convertToDataBaseFormat.WirelessReceiveCardConvert(root.WirelessReceiveCard, MainControlCardIP);
                //root.WirelessReceiveCard.T_WirelessReceiveCard = t_wirelessReceiveCard;//添加引用
                //t_rootCard.T_WirelessReceiveCard.Add(t_wirelessReceiveCard);
                string masterId = root.WirelessReceiveCard.MasterIdentifier;

                if (root.WirelessReceiveCard.TransmissionCard != null)
                {
                    foreach (var card in root.WirelessReceiveCard.TransmissionCard)
                    {
                        if (card.SlaveIdentifier != identifier)
                        {
                            continue;
                        }
                        GenerateSubTransmissionCard(card, ip, masterId, t_rootCard);
                    }
                }
            }
            #endregion


            return t_rootCard;
        }

        private static void GenerateSubWireMatchingCard(WireMatchingCard card, string ip, T1_RootCard t_rootCard)
        {
            var _convertToDataBaseFormat = new ConvertToDataBaseFormat();
            //转成数据库格式
            var t_wireMatchingCard = _convertToDataBaseFormat.WireMatchingCardConvert(card, ip);
            card.T_WireMatchingCard = t_wireMatchingCard;//添加引用
            t_rootCard.T_WireMatchingCard.Add(t_wireMatchingCard);
            int cardnum = card.CardNum;

            {
                var slot = card.IEPESlot;
                if (slot != null)
                {
                    //转成数据库格式
                    var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.IEPESlot, ip, cardnum);
                    t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.IEPESlot, ip, cardnum);
                    t_rootCard.T_IEPESlot.Add(t_slot as T1_IEPESlot);
                    slot.T_IEPESlot = t_slot as T1_IEPESlot;//添加引用
                    slot.T_AbstractSlotInfo = t_abstractslot;//添加引用
                    int slotnum = card.IEPESlot.SlotNum;

                    foreach (var channel in slot.IEPEChannelInfo)
                    {
                        //转成数据库格式
                        var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                        I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_IEPEChannelInfo.Add(t_channel as T1_IEPEChannelInfo);
                        channel.T_IEPEChannelInfo = t_channel as T1_IEPEChannelInfo;//添加引用
                        channel.T_AbstractChannelInfo = t_abstractchannel;//添加引用
                        int chnum = channel.CHNum;

                        if (channel.DivFreInfo != null)
                        {
                            foreach (var divfreinfo in channel.DivFreInfo)
                            {
                                //转成数据库格式                              
                                var t_divfreinfo = _convertToDataBaseFormat.DivFreInfoConvert(divfreinfo, ip, cardnum, slotnum, chnum);
                                divfreinfo.T_DivFreInfo = t_divfreinfo;//添加引用
                                t_rootCard.T_DivFreInfo.Add(t_divfreinfo);
                            }
                        }
                    }
                }
            }

            {
                var slot = card.EddyCurrentDisplacementSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.EddyCurrentDisplacementSlot, ip, cardnum);
                    t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.EddyCurrentDisplacementSlot, ip, cardnum);
                    t_rootCard.T_EddyCurrentDisplacementSlot.Add(t_slot as T1_EddyCurrentDisplacementSlot);
                    slot.T_EddyCurrentDisplacementSlot = t_slot as T1_EddyCurrentDisplacementSlot;//添加引用
                    slot.T_AbstractSlotInfo = t_abstractslot;//添加引用
                    int slotnum = card.EddyCurrentDisplacementSlot.SlotNum;

                    foreach (var channel in slot.EddyCurrentDisplacementChannelInfo)
                    {
                        //转成数据库格式
                        var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                        I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_EddyCurrentDisplacementChannelInfo.Add(t_channel as T1_EddyCurrentDisplacementChannelInfo);
                        channel.T_EddyCurrentDisplacementChannelInfo = t_channel as T1_EddyCurrentDisplacementChannelInfo;//添加引用
                        channel.T_AbstractChannelInfo = t_abstractchannel;//添加引用 
                        int chnum = channel.CHNum;
                        if (channel.DivFreInfo != null)
                        {
                            foreach (var divfreinfo in channel.DivFreInfo)
                            {
                                //转成数据库格式
                                var t_divfreinfo = _convertToDataBaseFormat.DivFreInfoConvert(divfreinfo, ip, cardnum, slotnum, chnum);
                                divfreinfo.T_DivFreInfo = t_divfreinfo;//添加引用
                                t_rootCard.T_DivFreInfo.Add(t_divfreinfo);
                            }
                        }
                    }
                }
            }

            {
                var slot = card.EddyCurrentKeyPhaseSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.EddyCurrentKeyPhaseSlot, ip, cardnum);
                    t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.EddyCurrentKeyPhaseSlot, ip, cardnum);
                    t_rootCard.T_EddyCurrentKeyPhaseSlot.Add(t_slot as T1_EddyCurrentKeyPhaseSlot);
                    slot.T_EddyCurrentKeyPhaseSlot = t_slot as T1_EddyCurrentKeyPhaseSlot;//添加引用
                    slot.T_AbstractSlotInfo = t_abstractslot;//添加引用
                    int slotnum = card.EddyCurrentKeyPhaseSlot.SlotNum;

                    foreach (var channel in slot.EddyCurrentKeyPhaseChannelInfo)
                    {
                        //转成数据库格式
                        var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                        I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_EddyCurrentKeyPhaseChannelInfo.Add(t_channel as T1_EddyCurrentKeyPhaseChannelInfo);
                        channel.T_EddyCurrentKeyPhaseChannelInfo = t_channel as T1_EddyCurrentKeyPhaseChannelInfo;//添加引用
                        channel.T_AbstractChannelInfo = t_abstractchannel;//添加引用
                    }
                }
            }

            {
                var slot = card.EddyCurrentTachometerSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.EddyCurrentTachometerSlot, ip, cardnum);
                    t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.EddyCurrentTachometerSlot, ip, cardnum);
                    t_rootCard.T_EddyCurrentTachometerSlot.Add(t_slot as T1_EddyCurrentTachometerSlot);
                    slot.T_EddyCurrentTachometerSlot = t_slot as T1_EddyCurrentTachometerSlot;//添加引用
                    slot.T_AbstractSlotInfo = t_abstractslot;//添加引用
                    int slotnum = card.EddyCurrentTachometerSlot.SlotNum;

                    foreach (var channel in slot.EddyCurrentTachometerChannelInfo)
                    {
                        //转成数据库格式
                        var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                        I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_EddyCurrentTachometerChannelInfo.Add(t_channel as T1_EddyCurrentTachometerChannelInfo);
                        channel.T_EddyCurrentTachometerChannelInfo = t_channel as T1_EddyCurrentTachometerChannelInfo;//添加引用
                        channel.T_AbstractChannelInfo = t_abstractchannel;//添加引用  
                    }
                }
            }

            {
                var slot = card.DigitTachometerSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.DigitTachometerSlot, ip, cardnum);
                    t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.DigitTachometerSlot, ip, cardnum);
                    t_rootCard.T_DigitTachometerSlot.Add(t_slot as T1_DigitTachometerSlot);
                    slot.T_DigitTachometerSlot = t_slot as T1_DigitTachometerSlot;//添加引用
                    slot.T_AbstractSlotInfo = t_abstractslot;//添加引用
                    int slotnum = card.DigitTachometerSlot.SlotNum;

                    foreach (var channel in slot.DigitTachometerChannelInfo)
                    {
                        //转成数据库格式
                        var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                        I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_DigitTachometerChannelInfo.Add(t_channel as T1_DigitTachometerChannelInfo);
                        channel.T_DigitTachometerChannelInfo = t_channel as T1_DigitTachometerChannelInfo;//添加引用
                        channel.T_AbstractChannelInfo = t_abstractchannel;//添加引用      
                    }
                }
            }

            {
                var slot = card.AnalogRransducerInSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.AnalogRransducerInSlot, ip, cardnum);
                    t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.AnalogRransducerInSlot, ip, cardnum);
                    t_rootCard.T_AnalogRransducerInSlot.Add(t_slot as T1_AnalogRransducerInSlot);
                    slot.T_AnalogRransducerInSlot = t_slot as T1_AnalogRransducerInSlot;//添加引用
                    slot.T_AbstractSlotInfo = t_abstractslot;//添加引用
                    int slotnum = card.AnalogRransducerInSlot.SlotNum;

                    foreach (var channel in slot.AnalogRransducerInChannelInfo)
                    {
                        //转成数据库格式
                        var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                        I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_AnalogRransducerInChannelInfo.Add(t_channel as T1_AnalogRransducerInChannelInfo);
                        channel.T_AnalogRransducerInChannelInfo = t_channel as T1_AnalogRransducerInChannelInfo;//添加引用
                        channel.T_AbstractChannelInfo = t_abstractchannel;//添加引用 
                    }
                }
            }

            {
                var slot = card.RelaySlot;
                if (slot != null)
                {
                    //转成数据库格式
                    var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.RelaySlot, ip, cardnum);
                    t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.RelaySlot, ip, cardnum);
                    t_rootCard.T_RelaySlot.Add(t_slot as T1_RelaySlot);
                    slot.T_RelaySlot = t_slot as T1_RelaySlot;//添加引用
                    slot.T_AbstractSlotInfo = t_abstractslot;//添加引用
                    int slotnum = card.RelaySlot.SlotNum;

                    foreach (var channel in slot.RelayChannelInfo)
                    {
                        //转成数据库格式
                        var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                        I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_RelayChannelInfo.Add(t_channel as T1_RelayChannelInfo);
                        channel.T_RelayChannelInfo = t_channel as T1_RelayChannelInfo;//添加引用
                        channel.T_AbstractChannelInfo = t_abstractchannel;//添加引用
                    }
                }
            }

            {
                var slot = card.DigitRransducerInSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.DigitRransducerInSlot, ip, cardnum);
                    t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.DigitRransducerInSlot, ip, cardnum);
                    t_rootCard.T_DigitRransducerInSlot.Add(t_slot as T1_DigitRransducerInSlot);
                    slot.T_DigitRransducerInSlot = t_slot as T1_DigitRransducerInSlot;//添加引用
                    slot.T_AbstractSlotInfo = t_abstractslot;//添加引用
                    int slotnum = card.DigitRransducerInSlot.SlotNum;

                    foreach (var channel in slot.DigitRransducerInChannelInfo)
                    {
                        //转成数据库格式
                        var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                        I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_DigitRransducerInChannelInfo.Add(t_channel as T1_DigitRransducerInChannelInfo);
                        channel.T_DigitRransducerInChannelInfo = t_channel as T1_DigitRransducerInChannelInfo;//添加引用
                        channel.T_AbstractChannelInfo = t_abstractchannel;//添加引用  
                    }
                }
            }

            {
                var slot = card.DigitRransducerOutSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.DigitRransducerOutSlot, ip, cardnum);
                    t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.DigitRransducerOutSlot, ip, cardnum);
                    t_rootCard.T_DigitRransducerOutSlot.Add(t_slot as T1_DigitRransducerOutSlot);
                    slot.T_DigitRransducerOutSlot = t_slot as T1_DigitRransducerOutSlot;//添加引用
                    slot.T_AbstractSlotInfo = t_abstractslot;//添加引用
                    int slotnum = card.DigitRransducerOutSlot.SlotNum;

                    foreach (var channel in slot.DigitRransducerOutChannelInfo)
                    {
                        //转成数据库格式
                        var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                        I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_DigitRransducerOutChannelInfo.Add(t_channel as T1_DigitRransducerOutChannelInfo);
                        channel.T_DigitRransducerOutChannelInfo = t_channel as T1_DigitRransducerOutChannelInfo;//添加引用
                        channel.T_AbstractChannelInfo = t_abstractchannel;//添加引用    
                    }
                }
            }

            {
                var slot = card.AnalogRransducerOutSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.AnalogRransducerOutSlot, ip, cardnum);
                    t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.AnalogRransducerOutSlot, ip, cardnum);
                    t_rootCard.T_AnalogRransducerOutSlot.Add(t_slot as T1_AnalogRransducerOutSlot);
                    slot.T_AnalogRransducerOutSlot = t_slot as T1_AnalogRransducerOutSlot;//添加引用
                    slot.T_AbstractSlotInfo = t_abstractslot;//添加引用
                    int slotnum = card.AnalogRransducerOutSlot.SlotNum;

                    foreach (var channel in slot.AnalogRransducerOutChannelInfo)
                    {
                        //转成数据库格式
                        var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                        I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                        t_rootCard.T_AnalogRransducerOutChannelInfo.Add(t_channel as T1_AnalogRransducerOutChannelInfo);
                        channel.T_AnalogRransducerOutChannelInfo = t_channel as T1_AnalogRransducerOutChannelInfo;//添加引用
                        channel.T_AbstractChannelInfo = t_abstractchannel;//添加引用    
                    }
                }
            }
        }
        private static void GenerateSubTransmissionCard(TransmissionCard card, string ip, string masterId, T1_RootCard t_rootCard)
        {
            var _convertToDataBaseFormat = new ConvertToDataBaseFormat();
            //转成数据库格式
            var t_card = _convertToDataBaseFormat.TransmissionCardConvert(card, ip, masterId);
            card.T_TransmissionCard = t_card;//添加引用
            t_rootCard.T_TransmissionCard.Add(t_card);
            string slaveId = card.SlaveIdentifier;

            {
                var slot = card.WirelessScalarSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.WirelessScalarSlot, ip, slaveId);
                    t_rootCard.T_WirelessScalarSlot.Add(t_slot as T1_WirelessScalarSlot);
                    slot.T_WirelessScalarSlot = t_slot as T1_WirelessScalarSlot;//添加引用
                    int slotnum = card.WirelessScalarSlot.SlotNum;

                    foreach (var channel in slot.WirelessScalarChannelInfo)
                    {
                        //转成数据库格式
                        var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, slaveId, slotnum);
                        t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                        I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, slaveId, slotnum);
                        t_rootCard.T_WirelessScalarChannelInfo.Add(t_channel as T1_WirelessScalarChannelInfo);
                        channel.T_WirelessScalarChannelInfo = t_channel as T1_WirelessScalarChannelInfo;//添加引用
                        channel.T_AbstractChannelInfo = t_abstractchannel;//添加引用
                    }
                }
            }

            {
                var slot = card.WirelessVibrationSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.WirelessVibrationSlot, ip, slaveId);
                    t_rootCard.T_WirelessVibrationSlot.Add(t_slot as T1_WirelessVibrationSlot);
                    slot.T_WirelessVibrationSlot = t_slot as T1_WirelessVibrationSlot;//添加引用
                    int slotnum = card.WirelessVibrationSlot.SlotNum;

                    foreach (var channel in slot.WirelessVibrationChannelInfo)
                    {
                        //转成数据库格式
                        var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, slaveId, slotnum);
                        t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                        I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, slaveId, slotnum);
                        t_rootCard.T_WirelessVibrationChannelInfo.Add(t_channel as T1_WirelessVibrationChannelInfo);
                        channel.T_WirelessVibrationChannelInfo = t_channel as T1_WirelessVibrationChannelInfo;//添加引用
                        channel.T_AbstractChannelInfo = t_abstractchannel;//添加引用 
                        int chnum = channel.CHNum;
                        if (channel.DivFreInfo != null)
                        {
                            foreach (var divfreinfo in channel.DivFreInfo)
                            {
                                //转成数据库格式
                                var t_divfreinfo = _convertToDataBaseFormat.DivFreInfoConvert(divfreinfo, ip, slaveId, slotnum, chnum);
                                divfreinfo.T_DivFreInfo = t_divfreinfo;//添加引用
                                t_rootCard.T_DivFreInfo.Add(t_divfreinfo);
                            }
                        }
                    }
                }
            }
        }

        public static T1_RootCard FindRootCard(RootCard root, T1_RootCard r_toot, string MainControlCardIP)
        {
            T1_RootCard t_rootCard = new T1_RootCard();
            var _convertToDataBaseFormat = new ConvertToDataBaseFormat();

            //主板

            //转成数据库格式
            string ip = MainControlCardIP;
            //var t_maincard = _convertToDataBaseFormat.MainControlCardConvert(root.MainControlCard, MainControlCardIP);
            //var t_maincard = (from p in r_toot.T_MainControlCard where p.IP == ip select p).FirstOrDefault();
            var t_maincard = root.MainControlCard.T_MainControlCard;
            if (t_maincard != null)
            {
                t_rootCard.T_MainControlCard.Add(t_maincard);
            }           

            #region 有线板卡
            if (root.WireMatchingCard != null)
            {
                foreach (var card in root.WireMatchingCard)
                {
                    FindWireMatchingCard(t_rootCard, card);
                }
            }
            #endregion
            #region 无线接收卡
            if (root.WirelessReceiveCard != null)
            {
                //转成数据库格式
                //var t_wirelessReceiveCard = _convertToDataBaseFormat.WirelessReceiveCardConvert(root.WirelessReceiveCard, MainControlCardIP);
                string masterId = root.WirelessReceiveCard.MasterIdentifier;
                //var t_wirelessReceiveCard = (from p in r_toot.T_WirelessReceiveCard where p.Code == ip select p).FirstOrDefault();
                var t_wirelessReceiveCard = root.WirelessReceiveCard.T_WirelessReceiveCard;
                if (t_wirelessReceiveCard != null)
                {
                    t_rootCard.T_WirelessReceiveCard.Add(t_wirelessReceiveCard);
                }               

                if (root.WirelessReceiveCard.TransmissionCard != null)
                {
                    foreach (var card in root.WirelessReceiveCard.TransmissionCard)
                    {
                        FindTransmissionCard(t_rootCard, card);
                    }
                }
            }
            #endregion


            return t_rootCard;
        }

        public static void FindWireMatchingCard(T1_RootCard t_rootCard, WireMatchingCard card)
        {
            //转成数据库格式
            int cardnum = card.CardNum;
            //var t_wireMatchingCard = _convertToDataBaseFormat.WireMatchingCardConvert(card, ip);
            //var t_wireMatchingCard = (from p in r_toot.T_WireMatchingCard where p.Code == ip + "_" + cardnum select p).FirstOrDefault();
            var t_wireMatchingCard = card.T_WireMatchingCard;
            if (t_wireMatchingCard != null)
            {
                t_rootCard.T_WireMatchingCard.Add(t_wireMatchingCard);
            }

            {
                var slot = card.IEPESlot;
                if (slot != null)
                {
                    //转成数据库格式                            
                    int slotnum = card.IEPESlot.SlotNum;
                    //var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.IEPESlot, ip, cardnum);
                    //var t_abstractslot = (from p in r_toot.T_AbstractSlotInfo where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_abstractslot = slot.T_AbstractSlotInfo;
                    if (t_abstractslot != null)
                    {
                        t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    }
                    //I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.IEPESlot, ip, cardnum);
                    //var t_slot = (from p in r_toot.T_IEPESlot where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_slot = slot.T_IEPESlot;
                    if (t_slot != null)
                    {
                        t_rootCard.T_IEPESlot.Add(t_slot as T1_IEPESlot);
                    }

                    if (slot.IEPEChannelInfo != null)
                    {
                        foreach (var channel in slot.IEPEChannelInfo)
                        {
                            //转成数据库格式
                            int chnum = channel.CHNum;
                            //var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                            //var t_abstractchannel = (from p in r_toot.T_AbstractChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_abstractchannel = channel.T_AbstractChannelInfo;
                            if (t_abstractchannel != null)
                            {
                                t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                            }
                            //I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                            //var t_channel = (from p in r_toot.T_IEPEChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_channel = channel.T_IEPEChannelInfo;
                            if (t_channel != null)
                            {
                                t_rootCard.T_IEPEChannelInfo.Add(t_channel as T1_IEPEChannelInfo);
                            }

                            if (channel.DivFreInfo != null)
                            {
                                foreach (var divfreinfo in channel.DivFreInfo)
                                {
                                    //转成数据库格式                              
                                    //var t_divfreinfo = _convertToDataBaseFormat.DivFreInfoConvert(divfreinfo, ip, cardnum, slotnum, chnum);
                                    //htzk123分频有问题
                                    //var t_divfreinfo = (from p in r_toot.T_DivFreInfo where p.T_AbstractChannelInfo_Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                                    var t_divfreinfo = divfreinfo.T_DivFreInfo;
                                    if (t_divfreinfo != null)
                                    {
                                        t_rootCard.T_DivFreInfo.Add(t_divfreinfo);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            {
                var slot = card.EddyCurrentDisplacementSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    int slotnum = card.EddyCurrentDisplacementSlot.SlotNum;
                    //var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.EddyCurrentDisplacementSlot, ip, cardnum);
                    //var t_abstractslot = (from p in r_toot.T_AbstractSlotInfo where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_abstractslot = slot.T_AbstractSlotInfo;
                    if (t_abstractslot != null)
                    {
                        t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    }
                    //I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.EddyCurrentDisplacementSlot, ip, cardnum);
                    //var t_slot = (from p in r_toot.T_EddyCurrentDisplacementSlot where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_slot = slot.T_EddyCurrentDisplacementSlot;
                    if (t_slot != null)
                    {
                        t_rootCard.T_EddyCurrentDisplacementSlot.Add(t_slot as T1_EddyCurrentDisplacementSlot);
                    }

                    if (slot.EddyCurrentDisplacementChannelInfo != null)
                    {
                        foreach (var channel in slot.EddyCurrentDisplacementChannelInfo)
                        {
                            //转成数据库格式
                            int chnum = channel.CHNum;
                            //var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                            //var t_abstractchannel = (from p in r_toot.T_AbstractChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_abstractchannel = channel.T_AbstractChannelInfo;
                            if (t_abstractchannel != null)
                            {
                                t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                            }
                            //I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                            //var t_channel = (from p in r_toot.T_EddyCurrentDisplacementChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_channel = channel.T_EddyCurrentDisplacementChannelInfo;
                            if (t_channel != null)
                            {
                                t_rootCard.T_EddyCurrentDisplacementChannelInfo.Add(t_channel as T1_EddyCurrentDisplacementChannelInfo);
                            }

                            if (channel.DivFreInfo != null)
                            {
                                foreach (var divfreinfo in channel.DivFreInfo)
                                {
                                    //转成数据库格式
                                    //var t_divfreinfo = _convertToDataBaseFormat.DivFreInfoConvert(divfreinfo, ip, cardnum, slotnum, chnum);
                                    //htzk123分频有问题
                                    //var t_divfreinfo = (from p in r_toot.T_DivFreInfo where p.T_AbstractChannelInfo_Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                                    var t_divfreinfo = divfreinfo.T_DivFreInfo;
                                    if (t_divfreinfo != null)
                                    {
                                        t_rootCard.T_DivFreInfo.Add(t_divfreinfo);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            {
                var slot = card.EddyCurrentKeyPhaseSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    //var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.EddyCurrentKeyPhaseSlot, ip, cardnum);
                    int slotnum = card.EddyCurrentKeyPhaseSlot.SlotNum;
                    //var t_abstractslot = (from p in r_toot.T_AbstractSlotInfo where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_abstractslot = slot.T_AbstractSlotInfo;
                    if (t_abstractslot != null)
                    {
                        t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    }
                    //I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.EddyCurrentKeyPhaseSlot, ip, cardnum);
                    //var t_slot = (from p in r_toot.T_EddyCurrentKeyPhaseSlot where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_slot = slot.T_EddyCurrentKeyPhaseSlot;
                    if (t_slot != null)
                    {
                        t_rootCard.T_EddyCurrentKeyPhaseSlot.Add(t_slot as T1_EddyCurrentKeyPhaseSlot);
                    }

                    if (slot.EddyCurrentKeyPhaseChannelInfo != null)
                    {
                        foreach (var channel in slot.EddyCurrentKeyPhaseChannelInfo)
                        {
                            //转成数据库格式
                            int chnum = channel.CHNum;
                            //var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                            //var t_abstractchannel = (from p in r_toot.T_AbstractChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_abstractchannel = channel.T_AbstractChannelInfo;
                            if (t_abstractchannel != null)
                            {
                                t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                            }
                            //I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                            //var t_channel = (from p in r_toot.T_EddyCurrentKeyPhaseChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_channel = channel.T_EddyCurrentKeyPhaseChannelInfo;
                            if (t_channel != null)
                            {
                                t_rootCard.T_EddyCurrentKeyPhaseChannelInfo.Add(t_channel as T1_EddyCurrentKeyPhaseChannelInfo);
                            }
                        }
                    }
                }
            }

            {
                var slot = card.EddyCurrentTachometerSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    int slotnum = card.EddyCurrentTachometerSlot.SlotNum;
                    //var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.EddyCurrentTachometerSlot, ip, cardnum);
                    //var t_abstractslot = (from p in r_toot.T_AbstractSlotInfo where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_abstractslot = slot.T_AbstractSlotInfo;
                    if (t_abstractslot != null)
                    {
                        t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    }
                    //I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.EddyCurrentTachometerSlot, ip, cardnum);
                    //var t_slot = (from p in r_toot.T_EddyCurrentTachometerSlot where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_slot = slot.T_EddyCurrentTachometerSlot;
                    if (t_slot != null)
                    {
                        t_rootCard.T_EddyCurrentTachometerSlot.Add(t_slot as T1_EddyCurrentTachometerSlot);
                    }

                    if (slot.EddyCurrentTachometerChannelInfo != null)
                    {
                        foreach (var channel in slot.EddyCurrentTachometerChannelInfo)
                        {
                            //转成数据库格式
                            int chnum = channel.CHNum;
                            //var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                            //var t_abstractchannel = (from p in r_toot.T_AbstractChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_abstractchannel = channel.T_AbstractChannelInfo;
                            if (t_abstractchannel != null)
                            {
                                t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                            }
                            //I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                            //var t_channel = (from p in r_toot.T_EddyCurrentTachometerChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_channel = channel.T_EddyCurrentTachometerChannelInfo;
                            if (t_channel != null)
                            {
                                t_rootCard.T_EddyCurrentTachometerChannelInfo.Add(t_channel as T1_EddyCurrentTachometerChannelInfo);
                            }
                        }
                    }
                }
            }

            {
                var slot = card.DigitTachometerSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    int slotnum = card.DigitTachometerSlot.SlotNum;
                    //var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.DigitTachometerSlot, ip, cardnum);
                    //var t_abstractslot = (from p in r_toot.T_AbstractSlotInfo where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_abstractslot = slot.T_AbstractSlotInfo;
                    if (t_abstractslot != null)
                    {
                        t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    }
                    //I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.DigitTachometerSlot, ip, cardnum);
                    //var t_slot = (from p in r_toot.T_DigitTachometerSlot where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_slot = slot.T_DigitTachometerSlot;
                    if (t_slot != null)
                    {
                        t_rootCard.T_DigitTachometerSlot.Add(t_slot as T1_DigitTachometerSlot);
                    }

                    if (slot.DigitTachometerChannelInfo != null)
                    {
                        foreach (var channel in slot.DigitTachometerChannelInfo)
                        {
                            //转成数据库格式
                            int chnum = channel.CHNum;
                            //var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                            //var t_abstractchannel = (from p in r_toot.T_AbstractChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_abstractchannel = channel.T_AbstractChannelInfo;
                            if (t_abstractchannel != null)
                            {
                                t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                            }
                            //I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                            //var t_channel = (from p in r_toot.T_DigitTachometerChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_channel = channel.T_DigitTachometerChannelInfo;
                            if (t_channel != null)
                            {
                                t_rootCard.T_DigitTachometerChannelInfo.Add(t_channel as T1_DigitTachometerChannelInfo);
                            }
                        }
                    }
                }
            }

            {
                var slot = card.AnalogRransducerInSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    int slotnum = card.AnalogRransducerInSlot.SlotNum;
                    //var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.AnalogRransducerInSlot, ip, cardnum);
                    //var t_abstractslot = (from p in r_toot.T_AbstractSlotInfo where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_abstractslot = slot.T_AbstractSlotInfo;
                    if (t_abstractslot != null)
                    {
                        t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    }
                    //I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.AnalogRransducerInSlot, ip, cardnum);
                    //var t_slot = (from p in r_toot.T_AnalogRransducerInSlot where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_slot = slot.T_AnalogRransducerInSlot;
                    if (t_slot != null)
                    {
                        t_rootCard.T_AnalogRransducerInSlot.Add(t_slot as T1_AnalogRransducerInSlot);
                    }

                    if (slot.AnalogRransducerInChannelInfo != null)
                    {
                        foreach (var channel in slot.AnalogRransducerInChannelInfo)
                        {
                            //转成数据库格式
                            int chnum = channel.CHNum;
                            //var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                            //var t_abstractchannel = (from p in r_toot.T_AbstractChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_abstractchannel = channel.T_AbstractChannelInfo;
                            if (t_abstractchannel != null)
                            {
                                t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                            }
                            //I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                            //var t_channel = (from p in r_toot.T_AnalogRransducerInChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_channel = channel.T_AnalogRransducerInChannelInfo;
                            if (t_channel != null)
                            {
                                t_rootCard.T_AnalogRransducerInChannelInfo.Add(t_channel as T1_AnalogRransducerInChannelInfo);
                            }
                        }
                    }
                }
            }

            {
                var slot = card.RelaySlot;
                if (slot != null)
                {
                    //转成数据库格式
                    int slotnum = card.RelaySlot.SlotNum;
                    //var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.RelaySlot, ip, cardnum);
                    //var t_abstractslot = (from p in r_toot.T_AbstractSlotInfo where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_abstractslot = slot.T_AbstractSlotInfo;
                    if (t_abstractslot != null)
                    {
                        t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    }
                    //I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.RelaySlot, ip, cardnum);
                    //var t_slot = (from p in r_toot.T_RelaySlot where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_slot = slot.T_RelaySlot;
                    if (t_slot != null)
                    {
                        t_rootCard.T_RelaySlot.Add(t_slot as T1_RelaySlot);
                    }

                    if (slot.RelayChannelInfo != null)
                    {
                        foreach (var channel in slot.RelayChannelInfo)
                        {
                            //转成数据库格式
                            int chnum = channel.CHNum;
                            //var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                            //var t_abstractchannel = (from p in r_toot.T_AbstractChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_abstractchannel = channel.T_AbstractChannelInfo;
                            if (t_abstractchannel != null)
                            {
                                t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                            }
                            //I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                            //var t_channel = (from p in r_toot.T_RelayChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_channel = channel.T_RelayChannelInfo;
                            if (t_channel != null)
                            {
                                t_rootCard.T_RelayChannelInfo.Add(t_channel as T1_RelayChannelInfo);
                            }
                        }
                    }
                }
            }

            {
                var slot = card.DigitRransducerInSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    int slotnum = card.DigitRransducerInSlot.SlotNum;
                    //var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.DigitRransducerInSlot, ip, cardnum);
                    //var t_abstractslot = (from p in r_toot.T_AbstractSlotInfo where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_abstractslot = slot.T_AbstractSlotInfo;
                    if (t_abstractslot != null)
                    {
                        t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    }
                    //I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.DigitRransducerInSlot, ip, cardnum);
                    //var t_slot = (from p in r_toot.T_DigitRransducerInSlot where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_slot = slot.T_DigitRransducerInSlot;
                    if (t_slot != null)
                    {
                        t_rootCard.T_DigitRransducerInSlot.Add(t_slot as T1_DigitRransducerInSlot);
                    }

                    if (slot.DigitRransducerInChannelInfo != null)
                    {
                        foreach (var channel in slot.DigitRransducerInChannelInfo)
                        {
                            //转成数据库格式
                            int chnum = channel.CHNum;
                            //var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                            //var t_abstractchannel = (from p in r_toot.T_AbstractChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_abstractchannel = channel.T_AbstractChannelInfo;
                            if (t_abstractchannel != null)
                            {
                                t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                            }
                            //I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                            //var t_channel = (from p in r_toot.T_DigitRransducerInChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_channel = channel.T_DigitRransducerInChannelInfo;
                            if (t_channel != null)
                            {
                                t_rootCard.T_DigitRransducerInChannelInfo.Add(t_channel as T1_DigitRransducerInChannelInfo);
                            }
                        }
                    }
                }
            }

            {
                var slot = card.DigitRransducerOutSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    int slotnum = card.DigitRransducerOutSlot.SlotNum;
                    //var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.DigitRransducerOutSlot, ip, cardnum);
                    //var t_abstractslot = (from p in r_toot.T_AbstractSlotInfo where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_abstractslot = slot.T_AbstractSlotInfo;
                    if (t_abstractslot != null)
                    {
                        t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    }
                    //I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.DigitRransducerOutSlot, ip, cardnum);
                    //var t_slot = (from p in r_toot.T_DigitRransducerOutSlot where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_slot = slot.T_DigitRransducerOutSlot;
                    if (t_slot != null)
                    {
                        t_rootCard.T_DigitRransducerOutSlot.Add(t_slot as T1_DigitRransducerOutSlot);
                    }

                    if (slot.DigitRransducerOutChannelInfo != null)
                    {
                        foreach (var channel in slot.DigitRransducerOutChannelInfo)
                        {
                            //转成数据库格式
                            int chnum = channel.CHNum;
                            //var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                            //var t_abstractchannel = (from p in r_toot.T_AbstractChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_abstractchannel = channel.T_AbstractChannelInfo;
                            if (t_abstractchannel != null)
                            {
                                t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                            }
                            //I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                            //var t_channel = (from p in r_toot.T_DigitRransducerOutChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_channel = channel.T_DigitRransducerOutChannelInfo;
                            if (t_channel != null)
                            {
                                t_rootCard.T_DigitRransducerOutChannelInfo.Add(t_channel as T1_DigitRransducerOutChannelInfo);
                            }
                        }
                    }
                }
            }

            {
                var slot = card.AnalogRransducerOutSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    int slotnum = card.AnalogRransducerOutSlot.SlotNum;
                    //var t_abstractslot = _convertToDataBaseFormat.AbstractSlotInfoConvert(card.AnalogRransducerOutSlot, ip, cardnum);
                    //var t_abstractslot = (from p in r_toot.T_AbstractSlotInfo where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_abstractslot = slot.T_AbstractSlotInfo;
                    if (t_abstractslot != null)
                    {
                        t_rootCard.T_AbstractSlotInfo.Add(t_abstractslot);
                    }
                    //I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.AnalogRransducerOutSlot, ip, cardnum);
                    //var t_slot = (from p in r_toot.T_AnalogRransducerOutSlot where p.Code == ip + "_" + cardnum + "_" + slotnum select p).FirstOrDefault();
                    var t_slot = slot.T_AnalogRransducerOutSlot;
                    if (t_slot != null)
                    {
                        t_rootCard.T_AnalogRransducerOutSlot.Add(t_slot as T1_AnalogRransducerOutSlot);
                    }

                    if (slot.AnalogRransducerOutChannelInfo != null)
                    {
                        foreach (var channel in slot.AnalogRransducerOutChannelInfo)
                        {
                            //转成数据库格式
                            int chnum = channel.CHNum;
                            //var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, cardnum, slotnum);
                            //var t_abstractchannel = (from p in r_toot.T_AbstractChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_abstractchannel = channel.T_AbstractChannelInfo;
                            if (t_abstractchannel != null)
                            {
                                t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                            }
                            //I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, cardnum, slotnum);
                            //var t_channel = (from p in r_toot.T_AnalogRransducerOutChannelInfo where p.Code == ip + "_" + cardnum + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_channel = channel.T_AnalogRransducerOutChannelInfo;
                            if (t_channel != null)
                            {
                                t_rootCard.T_AnalogRransducerOutChannelInfo.Add(t_channel as T1_AnalogRransducerOutChannelInfo);
                            }
                        }
                    }
                }
            }
        }

        public static void FindTransmissionCard(T1_RootCard t_rootCard, TransmissionCard card)
        {
            //转成数据库格式
            string slaveId = card.SlaveIdentifier;
            //var t_card = _convertToDataBaseFormat.TransmissionCardConvert(card, ip, masterId);
            //var t_card = (from p in r_toot.T_TransmissionCard where p.Code == ip + "_" + slaveId select p).FirstOrDefault();
            var t_card = card.T_TransmissionCard;
            if (t_card != null)
            {
                t_rootCard.T_TransmissionCard.Add(t_card);
            }

            {
                var slot = card.WirelessScalarSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    int slotnum = card.WirelessScalarSlot.SlotNum;
                    //I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.WirelessScalarSlot, ip, slaveId);
                    //var t_slot = (from p in r_toot.T_WirelessScalarSlot where p.Code == ip + "_" + slaveId + "_" + slotnum select p).FirstOrDefault();
                    var t_slot = slot.T_WirelessScalarSlot;
                    if (t_slot != null)
                    {
                        t_rootCard.T_WirelessScalarSlot.Add(t_slot as T1_WirelessScalarSlot);
                    }
                    if (slot.WirelessScalarChannelInfo != null)
                    {
                        foreach (var channel in slot.WirelessScalarChannelInfo)
                        {
                            //转成数据库格式
                            int chnum = channel.CHNum;
                            //var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, slaveId, slotnum);
                            //var t_abstractchannel = (from p in r_toot.T_AbstractChannelInfo where p.Code == ip + "_" + slaveId + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_abstractchannel = channel.T_AbstractChannelInfo;
                            if (t_abstractchannel != null)
                            {
                                t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                            }
                            //I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, slaveId, slotnum);
                            //var t_channel = (from p in r_toot.T_WirelessScalarChannelInfo where p.Code == ip + "_" + slaveId + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_channel = channel.T_WirelessScalarChannelInfo;
                            if (t_channel != null)
                            {
                                t_rootCard.T_WirelessScalarChannelInfo.Add(t_channel as T1_WirelessScalarChannelInfo);
                            }
                        }
                    }
                }
            }

            {
                var slot = card.WirelessVibrationSlot;
                if (slot != null)
                {
                    //转成数据库格式
                    int slotnum = card.WirelessVibrationSlot.SlotNum;
                    //I_BaseSlot t_slot = _convertToDataBaseFormat.BaseSlotConvert(card.WirelessVibrationSlot, ip, slaveId);
                    //var t_slot = (from p in r_toot.T_WirelessVibrationSlot where p.Code == ip + "_" + slaveId + "_" + slotnum select p).FirstOrDefault();
                    var t_slot = slot.T_WirelessVibrationSlot;
                    if (t_slot != null)
                    {
                        t_rootCard.T_WirelessVibrationSlot.Add(t_slot as T1_WirelessVibrationSlot);
                    }

                    if (slot.WirelessVibrationChannelInfo != null)
                    {
                        foreach (var channel in slot.WirelessVibrationChannelInfo)
                        {
                            //转成数据库格式
                            int chnum = channel.CHNum;
                            //var t_abstractchannel = _convertToDataBaseFormat.AbstractChannelInfoConvert(channel, ip, slaveId, slotnum);
                            //var t_abstractchannel = (from p in r_toot.T_AbstractChannelInfo where p.Code == ip + "_" + slaveId + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_abstractchannel = channel.T_AbstractChannelInfo;
                            if (t_abstractchannel != null)
                            {
                                t_rootCard.T_AbstractChannelInfo.Add(t_abstractchannel);
                            }
                            //I_BaseChannelInfo t_channel = _convertToDataBaseFormat.BaseChannelConvert(channel, ip, slaveId, slotnum);
                            //var t_channel = (from p in r_toot.T_WirelessVibrationChannelInfo where p.Code == ip + "_" + slaveId + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                            var t_channel = channel.T_WirelessVibrationChannelInfo;
                            if (t_channel != null)
                            {
                                t_rootCard.T_WirelessVibrationChannelInfo.Add(t_channel as T1_WirelessVibrationChannelInfo);
                            }
                            if (channel.DivFreInfo != null)
                            {
                                foreach (var divfreinfo in channel.DivFreInfo)
                                {
                                    //转成数据库格式
                                    //var t_divfreinfo = _convertToDataBaseFormat.DivFreInfoConvert(divfreinfo, ip, slaveId, slotnum, chnum);
                                    //htzk123分频有问题
                                    //var t_divfreinfo = (from p in r_toot.T_DivFreInfo where p.T_AbstractChannelInfo_Code == ip + "_" + slaveId + "_" + slotnum + "_" + chnum select p).FirstOrDefault();
                                    var t_divfreinfo = divfreinfo.T_DivFreInfo;
                                    if (t_divfreinfo != null)
                                    {
                                        t_rootCard.T_DivFreInfo.Add(t_divfreinfo);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

      

    }
}

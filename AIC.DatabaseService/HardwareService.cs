using AIC.Core.Events;
using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.DatabaseService.Models;
using AIC.DatabaseService.Tests;
using AIC.M9600.Common.MasterDB.Generated;
using AIC.ServiceInterface;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DatabaseService
{
    public class HardwareService : IHardwareService
    {
        private readonly IConvertToDataBaseFormat _convertToDataBaseFormat;
        private readonly IConvertFromDataBaseFormat _convertFromDataBaseFormat;
        private readonly IDatabaseComponent _databaseComponent;
        private readonly ICardProcess _cardProcess;
        public ObservableCollection<ServerTreeItemViewModel> ServerTreeItems { get; private set; }
        public HardwareService(IConvertToDataBaseFormat convertToDataBaseFormat, IConvertFromDataBaseFormat convertFromDataBaseFormat, IDatabaseComponent databaseComponent, ICardProcess cardProcess)
        {
            _convertToDataBaseFormat = convertToDataBaseFormat;
            _convertFromDataBaseFormat = convertFromDataBaseFormat;
            _databaseComponent = databaseComponent;
            _cardProcess = cardProcess;

            ServerTreeItems = new ObservableCollection<ServerTreeItemViewModel>();
        }

        public void Initialize()
        {

        }

        public void InitServers(IEnumerable<ServerInfo> serverlist)
        {
            ServerTreeItems.Clear();
            foreach (var server in serverlist)
            {
                CardTreeGenerate.AddCardServerTree(ServerTreeItems, server.IP);
            }
            GetCardFromDatabase();
        }

        public async Task<List<ChannelTreeItemViewModel>> AddCard(string serverIP, string maincardIP, string json)
        {
            var root = await _databaseComponent.ComplexWithJson(serverIP, maincardIP, json, null, null, null);
            if (root != null)
            {
                if (root.MainControlCard != null)
                {
                    root.MainControlCard.ServerIP = serverIP;//htzk123，请注意此处
                }
                var t_rootcard = CardTreeGenerate.GenerateRootCard(root, maincardIP);

                if (await _databaseComponent.UploadHardwave(serverIP, t_rootcard) == true)
                {
                    var channels = CardTreeGenerate.GetCardTree(ServerTreeItems, root, serverIP, maincardIP);
                    return channels;
                }
            }

            return null;
        }

        public async Task<List<ChannelTreeItemViewModel>> DeleteCard(string serverIP, string maincardIP, string json)
        {
            if (ServerTreeItems != null && ServerTreeItems.Count > 0)
            {
                var server = (from p in ServerTreeItems where p.ServerIP == serverIP select p).FirstOrDefault();
                if (server != null && server.Children.Count > 0)
                {
                    var maincard = (from p in server.Children where (p as MainCardTreeItemViewModel).MainControlCardIP == maincardIP select p).FirstOrDefault();

                    var rootcard = new RootCard();
                    rootcard.MainControlCard = (maincard as MainCardTreeItemViewModel).MainControlCard;
                    rootcard.WirelessReceiveCard = (maincard as MainCardTreeItemViewModel).WirelessReceiveCard;
                    rootcard.WireMatchingCard = (maincard as MainCardTreeItemViewModel).WireMatchingCard;
                    T1_RootCard t_rootcard = CardTreeGenerate.FindRootCard(rootcard, _databaseComponent.GetRootCard(serverIP), maincardIP);
                    //找到所有的通道
                    var channels = _cardProcess.GetChannels(maincard.Children);
                    var deleteDic = RootToDictionary(t_rootcard);
                    var root = await _databaseComponent.ComplexWithJson(serverIP, maincardIP, json, null, null, deleteDic);
                    if (root != null)
                    {
                        server.RemoveChild(maincard);
                        return channels;
                    }
                }
            }
            return null;
        }

        public async Task<List<ChannelTreeItemViewModel>> ForceDeleteCard(string serverIP, string maincardIP)
        {
            if (ServerTreeItems != null && ServerTreeItems.Count > 0)
            {
                var server = (from p in ServerTreeItems where p.ServerIP == serverIP select p).FirstOrDefault();
                if (server != null && server.Children.Count > 0)
                {
                    var maincard = (from p in server.Children where (p as MainCardTreeItemViewModel).MainControlCardIP == maincardIP select p).FirstOrDefault();

                    var rootcard = new RootCard();
                    rootcard.MainControlCard = (maincard as MainCardTreeItemViewModel).MainControlCard;
                    rootcard.WirelessReceiveCard = (maincard as MainCardTreeItemViewModel).WirelessReceiveCard;
                    rootcard.WireMatchingCard = (maincard as MainCardTreeItemViewModel).WireMatchingCard;
                    T1_RootCard t_rootcard = CardTreeGenerate.FindRootCard(rootcard, _databaseComponent.GetRootCard(serverIP), maincardIP);
                    //找到所有的通道
                    var channels = _cardProcess.GetChannels(maincard.Children);
                    var deleteDic = RootToDictionary(t_rootcard);
                    bool result = await _databaseComponent.Complex(serverIP, null, null, deleteDic);
                    if (result == true)
                    {
                        server.RemoveChild(maincard);
                        return channels;
                    }
                }
            }
            return null;
        }

        private Dictionary<string, Tuple<string, ICollection<object>>> RootToDictionary(T1_RootCard t_rootcard)
        {
            Dictionary<string, Tuple<string, ICollection<object>>> deleteDic = new Dictionary<string, Tuple<string, ICollection<object>>>();
            if (t_rootcard.T_MainControlCard != null)
            {
                deleteDic.Add("T_MainControlCard", new Tuple<string, ICollection<object>>("id", t_rootcard.T_MainControlCard.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_WireMatchingCard != null)
            {
                deleteDic.Add("T_WireMatchingCard", new Tuple<string, ICollection<object>>("id", t_rootcard.T_WireMatchingCard.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_WirelessReceiveCard != null)
            {
                deleteDic.Add("T_WirelessReceiveCard", new Tuple<string, ICollection<object>>("id", t_rootcard.T_WirelessReceiveCard.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_TransmissionCard != null)
            {
                deleteDic.Add("T_TransmissionCard", new Tuple<string, ICollection<object>>("id", t_rootcard.T_TransmissionCard.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_AbstractChannelInfo != null)
            {
                deleteDic.Add("T_AbstractChannelInfo", new Tuple<string, ICollection<object>>("id", t_rootcard.T_AbstractChannelInfo.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_IEPEChannelInfo != null)
            {
                deleteDic.Add("T_IEPEChannelInfo", new Tuple<string, ICollection<object>>("id", t_rootcard.T_IEPEChannelInfo.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_EddyCurrentDisplacementChannelInfo != null)
            {
                deleteDic.Add("T_EddyCurrentDisplacementChannelInfo", new Tuple<string, ICollection<object>>("id", t_rootcard.T_EddyCurrentDisplacementChannelInfo.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_EddyCurrentKeyPhaseChannelInfo != null)
            {
                deleteDic.Add("T_EddyCurrentKeyPhaseChannelInfo", new Tuple<string, ICollection<object>>("id", t_rootcard.T_EddyCurrentKeyPhaseChannelInfo.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_EddyCurrentTachometerChannelInfo != null)
            {
                deleteDic.Add("T_EddyCurrentTachometerChannelInfo", new Tuple<string, ICollection<object>>("id", t_rootcard.T_EddyCurrentTachometerChannelInfo.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_DigitTachometerChannelInfo != null)
            {
                deleteDic.Add("T_DigitTachometerChannelInfo", new Tuple<string, ICollection<object>>("id", t_rootcard.T_DigitTachometerChannelInfo.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_AnalogRransducerInChannelInfo != null)
            {
                deleteDic.Add("T_AnalogRransducerInChannelInfo", new Tuple<string, ICollection<object>>("id", t_rootcard.T_AnalogRransducerInChannelInfo.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_RelayChannelInfo != null)
            {
                deleteDic.Add("T_RelayChannelInfo", new Tuple<string, ICollection<object>>("id", t_rootcard.T_RelayChannelInfo.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_DigitRransducerInChannelInfo != null)
            {
                deleteDic.Add("T_DigitRransducerInChannelInfo", new Tuple<string, ICollection<object>>("id", t_rootcard.T_DigitRransducerInChannelInfo.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_DigitRransducerOutChannelInfo != null)
            {
                deleteDic.Add("T_DigitRransducerOutChannelInfo", new Tuple<string, ICollection<object>>("id", t_rootcard.T_DigitRransducerOutChannelInfo.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_AnalogRransducerOutChannelInfo != null)
            {
                deleteDic.Add("T_AnalogRransducerOutChannelInfo", new Tuple<string, ICollection<object>>("id", t_rootcard.T_AnalogRransducerOutChannelInfo.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_WirelessScalarChannelInfo != null)
            {
                deleteDic.Add("T_WirelessScalarChannelInfo", new Tuple<string, ICollection<object>>("id", t_rootcard.T_WirelessScalarChannelInfo.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_WirelessVibrationChannelInfo != null)
            {
                deleteDic.Add("T_WirelessVibrationChannelInfo", new Tuple<string, ICollection<object>>("id", t_rootcard.T_WirelessVibrationChannelInfo.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_AbstractSlotInfo != null)
            {
                deleteDic.Add("T_AbstractSlotInfo", new Tuple<string, ICollection<object>>("id", t_rootcard.T_AbstractSlotInfo.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_IEPESlot != null)
            {
                deleteDic.Add("T_IEPESlot", new Tuple<string, ICollection<object>>("id", t_rootcard.T_IEPESlot.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_EddyCurrentDisplacementSlot != null)
            {
                deleteDic.Add("T_EddyCurrentDisplacementSlot", new Tuple<string, ICollection<object>>("id", t_rootcard.T_EddyCurrentDisplacementSlot.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_EddyCurrentKeyPhaseSlot != null)
            {
                deleteDic.Add("T_EddyCurrentKeyPhaseSlot", new Tuple<string, ICollection<object>>("id", t_rootcard.T_EddyCurrentKeyPhaseSlot.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_EddyCurrentTachometerSlot != null)
            {
                deleteDic.Add("T_EddyCurrentTachometerSlot", new Tuple<string, ICollection<object>>("id", t_rootcard.T_EddyCurrentTachometerSlot.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_DigitTachometerSlot != null)
            {
                deleteDic.Add("T_DigitTachometerSlot", new Tuple<string, ICollection<object>>("id", t_rootcard.T_DigitTachometerSlot.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_AnalogRransducerInSlot != null)
            {
                deleteDic.Add("T_AnalogRransducerInSlot", new Tuple<string, ICollection<object>>("id", t_rootcard.T_AnalogRransducerInSlot.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_RelaySlot != null)
            {
                deleteDic.Add("T_RelaySlot", new Tuple<string, ICollection<object>>("id", t_rootcard.T_RelaySlot.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_DigitRransducerInSlot != null)
            {
                deleteDic.Add("T_DigitRransducerInSlot", new Tuple<string, ICollection<object>>("id", t_rootcard.T_DigitRransducerInSlot.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_DigitRransducerOutSlot != null)
            {
                deleteDic.Add("T_DigitRransducerOutSlot", new Tuple<string, ICollection<object>>("id", t_rootcard.T_DigitRransducerOutSlot.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_AnalogRransducerOutSlot != null)
            {
                deleteDic.Add("T_AnalogRransducerOutSlot", new Tuple<string, ICollection<object>>("id", t_rootcard.T_AnalogRransducerOutSlot.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_WirelessScalarSlot != null)
            {
                deleteDic.Add("T_WirelessScalarSlot", new Tuple<string, ICollection<object>>("id", t_rootcard.T_WirelessScalarSlot.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_WirelessVibrationSlot != null)
            {
                deleteDic.Add("T_WirelessVibrationSlot", new Tuple<string, ICollection<object>>("id", t_rootcard.T_WirelessVibrationSlot.Select(p => p.id as object).ToList()));
            }
            if (t_rootcard.T_DivFreInfo != null)
            {
                deleteDic.Add("T_DivFreInfo", new Tuple<string, ICollection<object>>("id", t_rootcard.T_DivFreInfo.Select(p => p.id as object).ToList()));
            }

            return deleteDic;
        }

        public void GetCardFromDatabase()
        {
            //从数据库取出，去重复
            //假设T_RootCard已经按ServerIP分好了，并且去掉没有T_MainControlCard主板的服务器。
            foreach (var server_node in ServerTreeItems)
            {
                server_node.ClearChild();
                if (!_databaseComponent.GetServerIPCategory().Contains(server_node.ServerIP))
                {
                    continue;
                }
                var rootcard = _databaseComponent.GetRootCard(server_node.ServerIP);//(from p in T_RootCard.Values where p.T_MainControlCard[0].ServerIP == server_node.ServerIP select p).FirstOrDefault();

                foreach (var mainCard in rootcard.T_MainControlCard.OrderBy(p => p.IP))//按主板IP顺序
                {
                    MainCardTreeItemViewModel maincard_node = new MainCardTreeItemViewModel(mainCard.IP);
                    maincard_node.MainControlCardIP = mainCard.IP;
                    maincard_node.MainControlCard = _convertFromDataBaseFormat.MainControlCardConvert(mainCard);
                    //maincard_node.MainControlCard.T_MainControlCard = mainCard; //添加引用

                    #region 有限板卡
                    var t_card = (from p in rootcard.T_WireMatchingCard where p.T_MainControlCard_IP == mainCard.IP orderby p.CardNum select p).ToList();//按卡号顺序
                    if (t_card.Count > 0)
                    {
                        maincard_node.WireMatchingCard = new List<WireMatchingCard>();
                    }
                    maincard_node.IsExpanded = true;

                    server_node.AddChild(maincard_node);
                    for (int icard = 0; icard < t_card.Count; icard++)
                    {
                        var card = _convertFromDataBaseFormat.WireMatchingCardConvert(t_card[icard]);
                        //card.T_WireMatchingCard = t_card[icard];//添加引用
                        maincard_node.WireMatchingCard.Add(card);
                        WireMatchingCardTreeItemViewModel card_node = new WireMatchingCardTreeItemViewModel(card);
                        maincard_node.AddChild(card_node);

                        var t_abstractslot = (from p in rootcard.T_AbstractSlotInfo where p.T_WireMatchingCard_Code == t_card[icard].Code orderby p.SlotNum select p).ToList();//按槽号顺序
                        for (int islot = 0; islot < t_abstractslot.Count; islot++)
                        {
                            #region IEPE 
                            {
                                var t_slot = (from p in rootcard.T_IEPESlot where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code select p).FirstOrDefault();
                                if (t_slot != null)
                                {
                                    var slot = _convertFromDataBaseFormat.BaseSlotConvert(t_slot) as IEPESlot;
                                    _convertFromDataBaseFormat.AbstractSlotInfoConvert(slot, t_abstractslot[islot]);
                                    //slot.T_IEPESlot = t_slot;//添加引用
                                    //slot.T_AbstractSlotInfo = t_abstractslot[islot];//添加引用
                                    card.IEPESlot = slot;
                                    SlotTreeItemViewModel slot_node = new SlotTreeItemViewModel(slot);
                                    card_node.AddChild(slot_node);

                                    var t_abstractchannel = (from p in rootcard.T_AbstractChannelInfo where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code orderby p.CHNum select p).ToList();//按通道号顺序
                                    if (t_abstractchannel.Count > 0)
                                    {
                                        slot.IEPEChannelInfo = new List<IEPEChannelInfo>();
                                    }
                                    for (int ichannel = 0; ichannel < t_abstractchannel.Count; ichannel++)
                                    {
                                        var t_channel = (from p in rootcard.T_IEPEChannelInfo where p.T_AbstractChannelInfo_Code == t_abstractchannel[ichannel].Code select p).FirstOrDefault();
                                        if (t_channel != null)//t_channel一定不为空，除非数据不完整
                                        {
                                            var channel = _convertFromDataBaseFormat.BaseChannelConvert(t_channel) as IEPEChannelInfo;
                                            _convertFromDataBaseFormat.AbstractChannelInfoConvert(channel, t_abstractchannel[ichannel]);
                                            //channel.T_IEPEChannelInfo = t_channel;//添加引用
                                            //channel.T_AbstractChannelInfo = t_abstractchannel[ichannel];//添加引用
                                            slot.IEPEChannelInfo.Add(channel);
                                            ChannelTreeItemViewModel channel_node = new ChannelTreeItemViewModel(channel);
                                            slot_node.AddChild(channel_node);

                                            //分频
                                            var t_divfreinfo = (from p in rootcard.T_DivFreInfo where p.T_AbstractChannelInfo_Code == t_abstractchannel[ichannel].Code orderby p.Create_Time select p).ToList();
                                            if (t_divfreinfo.Count > 0)
                                            {
                                                channel.DivFreInfo = new ObservableCollection<DivFreInfo>();
                                                for (int idivfreinfo = 0; idivfreinfo < t_divfreinfo.Count; idivfreinfo++)
                                                {
                                                    var divfreinfo = _convertFromDataBaseFormat.DivFreInfoConvert(t_divfreinfo[idivfreinfo]);
                                                    //divfreinfo.T_DivFreInfo = t_divfreinfo[idivfreinfo];//添加引用                                                  
                                                    channel.DivFreInfo.Add(divfreinfo);
                                                }
                                            }
                                        }
                                    }
                                    continue;
                                }
                            }
                            #endregion
                            #region EddyCurrentDisplacement 
                            {
                                var t_slot = (from p in rootcard.T_EddyCurrentDisplacementSlot where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code select p).FirstOrDefault();
                                if (t_slot != null)
                                {
                                    var slot = _convertFromDataBaseFormat.BaseSlotConvert(t_slot) as EddyCurrentDisplacementSlot;
                                    _convertFromDataBaseFormat.AbstractSlotInfoConvert(slot, t_abstractslot[islot]);
                                    //slot.T_EddyCurrentDisplacementSlot = t_slot;//添加引用
                                    //slot.T_AbstractSlotInfo = t_abstractslot[islot];//添加引用
                                    card.EddyCurrentDisplacementSlot = slot;
                                    SlotTreeItemViewModel slot_node = new SlotTreeItemViewModel(slot);
                                    card_node.AddChild(slot_node);

                                    var t_abstractchannel = (from p in rootcard.T_AbstractChannelInfo where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code orderby p.CHNum select p).ToList();//按通道号顺序
                                    if (t_abstractchannel.Count > 0)
                                    {
                                        slot.EddyCurrentDisplacementChannelInfo = new List<EddyCurrentDisplacementChannelInfo>();
                                    }
                                    for (int ichannel = 0; ichannel < t_abstractchannel.Count; ichannel++)
                                    {
                                        var t_channel = (from p in rootcard.T_EddyCurrentDisplacementChannelInfo where p.T_AbstractChannelInfo_Code == t_abstractchannel[ichannel].Code select p).FirstOrDefault();
                                        if (t_channel != null)//t_channel一定不为空，除非数据不完整
                                        {
                                            var channel = _convertFromDataBaseFormat.BaseChannelConvert(t_channel) as EddyCurrentDisplacementChannelInfo;
                                            _convertFromDataBaseFormat.AbstractChannelInfoConvert(channel, t_abstractchannel[ichannel]);
                                            //channel.T_EddyCurrentDisplacementChannelInfo = t_channel;//添加引用
                                            //channel.T_AbstractChannelInfo = t_abstractchannel[ichannel];//添加引用
                                            slot.EddyCurrentDisplacementChannelInfo.Add(channel);
                                            ChannelTreeItemViewModel channel_node = new ChannelTreeItemViewModel(channel);
                                            slot_node.AddChild(channel_node);

                                            //分频
                                            var t_divfreinfo = (from p in rootcard.T_DivFreInfo where p.T_AbstractChannelInfo_Code == t_abstractchannel[ichannel].Code orderby p.Create_Time select p).ToList();
                                            if (t_divfreinfo.Count > 0)
                                            {
                                                channel.DivFreInfo = new ObservableCollection<DivFreInfo>();
                                                for (int idivfreinfo = 0; idivfreinfo < t_divfreinfo.Count; idivfreinfo++)
                                                {
                                                    var divfreinfo = _convertFromDataBaseFormat.DivFreInfoConvert(t_divfreinfo[idivfreinfo]);
                                                    //divfreinfo.T_DivFreInfo = t_divfreinfo[idivfreinfo];//添加引用   
                                                    channel.DivFreInfo.Add(divfreinfo);
                                                }
                                            }
                                        }
                                    }
                                    continue;
                                }
                            }
                            #endregion
                            #region EddyCurrentKeyPhase 
                            {
                                var t_slot = (from p in rootcard.T_EddyCurrentKeyPhaseSlot where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code select p).FirstOrDefault();
                                if (t_slot != null)
                                {
                                    var slot = _convertFromDataBaseFormat.BaseSlotConvert(t_slot) as EddyCurrentKeyPhaseSlot;
                                    _convertFromDataBaseFormat.AbstractSlotInfoConvert(slot, t_abstractslot[islot]);
                                    //slot.T_EddyCurrentKeyPhaseSlot = t_slot;//添加引用
                                    //slot.T_AbstractSlotInfo = t_abstractslot[islot];//添加引用
                                    card.EddyCurrentKeyPhaseSlot = slot;
                                    SlotTreeItemViewModel slot_node = new SlotTreeItemViewModel(slot);
                                    card_node.AddChild(slot_node);

                                    var t_abstractchannel = (from p in rootcard.T_AbstractChannelInfo where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code orderby p.CHNum select p).ToList();//按通道号顺序
                                    if (t_abstractchannel.Count > 0)
                                    {
                                        slot.EddyCurrentKeyPhaseChannelInfo = new List<EddyCurrentKeyPhaseChannelInfo>();
                                    }
                                    for (int ichannel = 0; ichannel < t_abstractchannel.Count; ichannel++)
                                    {
                                        var t_channel = (from p in rootcard.T_EddyCurrentKeyPhaseChannelInfo where p.T_AbstractChannelInfo_Code == t_abstractchannel[ichannel].Code select p).FirstOrDefault();
                                        if (t_channel != null)//t_channel一定不为空，除非数据不完整
                                        {
                                            var channel = _convertFromDataBaseFormat.BaseChannelConvert(t_channel) as EddyCurrentKeyPhaseChannelInfo;
                                            _convertFromDataBaseFormat.AbstractChannelInfoConvert(channel, t_abstractchannel[ichannel]);
                                            //channel.T_EddyCurrentKeyPhaseChannelInfo = t_channel;//添加引用
                                            //channel.T_AbstractChannelInfo = t_abstractchannel[ichannel];//添加引用
                                            slot.EddyCurrentKeyPhaseChannelInfo.Add(channel);
                                            ChannelTreeItemViewModel channel_node = new ChannelTreeItemViewModel(channel);
                                            slot_node.AddChild(channel_node);

                                            //分频
                                        }
                                    }
                                    continue;
                                }
                            }
                            #endregion
                            #region EddyCurrentTachometer 
                            {
                                var t_slot = (from p in rootcard.T_EddyCurrentTachometerSlot where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code select p).FirstOrDefault();
                                if (t_slot != null)
                                {
                                    var slot = _convertFromDataBaseFormat.BaseSlotConvert(t_slot) as EddyCurrentTachometerSlot;
                                    _convertFromDataBaseFormat.AbstractSlotInfoConvert(slot, t_abstractslot[islot]);
                                    //slot.T_EddyCurrentTachometerSlot = t_slot;//添加引用
                                    //slot.T_AbstractSlotInfo = t_abstractslot[islot];//添加引用
                                    card.EddyCurrentTachometerSlot = slot;
                                    SlotTreeItemViewModel slot_node = new SlotTreeItemViewModel(slot);
                                    card_node.AddChild(slot_node);

                                    var t_abstractchannel = (from p in rootcard.T_AbstractChannelInfo where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code orderby p.CHNum select p).ToList();//按通道号顺序
                                    if (t_abstractchannel.Count > 0)
                                    {
                                        slot.EddyCurrentTachometerChannelInfo = new List<EddyCurrentTachometerChannelInfo>();
                                    }
                                    for (int ichannel = 0; ichannel < t_abstractchannel.Count; ichannel++)
                                    {
                                        var t_channel = (from p in rootcard.T_EddyCurrentTachometerChannelInfo where p.T_AbstractChannelInfo_Code == t_abstractchannel[ichannel].Code select p).FirstOrDefault();
                                        if (t_channel != null)//t_channel一定不为空，除非数据不完整
                                        {
                                            var channel = _convertFromDataBaseFormat.BaseChannelConvert(t_channel) as EddyCurrentTachometerChannelInfo;
                                            _convertFromDataBaseFormat.AbstractChannelInfoConvert(channel, t_abstractchannel[ichannel]);
                                            //channel.T_EddyCurrentTachometerChannelInfo = t_channel;//添加引用
                                            //channel.T_AbstractChannelInfo = t_abstractchannel[ichannel];//添加引用
                                            slot.EddyCurrentTachometerChannelInfo.Add(channel);
                                            ChannelTreeItemViewModel channel_node = new ChannelTreeItemViewModel(channel);
                                            slot_node.AddChild(channel_node);

                                            //分频
                                        }
                                    }
                                    continue;
                                }
                            }
                            #endregion
                            #region DigitTachometer 
                            {
                                var t_slot = (from p in rootcard.T_DigitTachometerSlot where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code select p).FirstOrDefault();
                                if (t_slot != null)
                                {
                                    var slot = _convertFromDataBaseFormat.BaseSlotConvert(t_slot) as DigitTachometerSlot;
                                    _convertFromDataBaseFormat.AbstractSlotInfoConvert(slot, t_abstractslot[islot]);
                                    //slot.T_DigitTachometerSlot = t_slot;//添加引用
                                    //slot.T_AbstractSlotInfo = t_abstractslot[islot];//添加引用
                                    card.DigitTachometerSlot = slot;
                                    SlotTreeItemViewModel slot_node = new SlotTreeItemViewModel(slot);
                                    card_node.AddChild(slot_node);

                                    var t_abstractchannel = (from p in rootcard.T_AbstractChannelInfo where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code orderby p.CHNum select p).ToList();//按通道号顺序
                                    if (t_abstractchannel.Count > 0)
                                    {
                                        slot.DigitTachometerChannelInfo = new List<DigitTachometerChannelInfo>();
                                    }
                                    for (int ichannel = 0; ichannel < t_abstractchannel.Count; ichannel++)
                                    {
                                        var t_channel = (from p in rootcard.T_DigitTachometerChannelInfo where p.T_AbstractChannelInfo_Code == t_abstractchannel[ichannel].Code select p).FirstOrDefault();
                                        if (t_channel != null)//t_channel一定不为空，除非数据不完整
                                        {
                                            var channel = _convertFromDataBaseFormat.BaseChannelConvert(t_channel) as DigitTachometerChannelInfo;
                                            _convertFromDataBaseFormat.AbstractChannelInfoConvert(channel, t_abstractchannel[ichannel]);
                                            //channel.T_DigitTachometerChannelInfo = t_channel;//添加引用
                                            //channel.T_AbstractChannelInfo = t_abstractchannel[ichannel];//添加引用
                                            slot.DigitTachometerChannelInfo.Add(channel);
                                            ChannelTreeItemViewModel channel_node = new ChannelTreeItemViewModel(channel);
                                            slot_node.AddChild(channel_node);

                                            //分频
                                        }
                                    }
                                    continue;
                                }
                            }
                            #endregion
                            #region AnalogRransducerIn 
                            {
                                var t_slot = (from p in rootcard.T_AnalogRransducerInSlot where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code select p).FirstOrDefault();
                                if (t_slot != null)
                                {
                                    var slot = _convertFromDataBaseFormat.BaseSlotConvert(t_slot) as AnalogRransducerInSlot;
                                    _convertFromDataBaseFormat.AbstractSlotInfoConvert(slot, t_abstractslot[islot]);
                                    //slot.T_AnalogRransducerInSlot = t_slot;//添加引用
                                    //slot.T_AbstractSlotInfo = t_abstractslot[islot];//添加引用
                                    card.AnalogRransducerInSlot = slot;
                                    SlotTreeItemViewModel slot_node = new SlotTreeItemViewModel(slot);
                                    card_node.AddChild(slot_node);

                                    var t_abstractchannel = (from p in rootcard.T_AbstractChannelInfo where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code orderby p.CHNum select p).ToList();//按通道号顺序
                                    if (t_abstractchannel.Count > 0)
                                    {
                                        slot.AnalogRransducerInChannelInfo = new List<AnalogRransducerInChannelInfo>();
                                    }
                                    for (int ichannel = 0; ichannel < t_abstractchannel.Count; ichannel++)
                                    {
                                        var t_channel = (from p in rootcard.T_AnalogRransducerInChannelInfo where p.T_AbstractChannelInfo_Code == t_abstractchannel[ichannel].Code select p).FirstOrDefault();
                                        if (t_channel != null)//t_channel一定不为空，除非数据不完整
                                        {
                                            var channel = _convertFromDataBaseFormat.BaseChannelConvert(t_channel) as AnalogRransducerInChannelInfo;
                                            _convertFromDataBaseFormat.AbstractChannelInfoConvert(channel, t_abstractchannel[ichannel]);
                                            //channel.T_AnalogRransducerInChannelInfo = t_channel;//添加引用
                                            //channel.T_AbstractChannelInfo = t_abstractchannel[ichannel];//添加引用
                                            slot.AnalogRransducerInChannelInfo.Add(channel);
                                            ChannelTreeItemViewModel channel_node = new ChannelTreeItemViewModel(channel);
                                            slot_node.AddChild(channel_node);

                                            //分频
                                        }
                                    }
                                    continue;
                                }
                            }
                            #endregion
                            #region Relay 
                            {
                                var t_slot = (from p in rootcard.T_RelaySlot where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code select p).FirstOrDefault();
                                if (t_slot != null)
                                {
                                    var slot = _convertFromDataBaseFormat.BaseSlotConvert(t_slot) as RelaySlot;
                                    _convertFromDataBaseFormat.AbstractSlotInfoConvert(slot, t_abstractslot[islot]);
                                    //slot.T_RelaySlot = t_slot;//添加引用
                                    //slot.T_AbstractSlotInfo = t_abstractslot[islot];//添加引用
                                    card.RelaySlot = slot;
                                    SlotTreeItemViewModel slot_node = new SlotTreeItemViewModel(slot);
                                    card_node.AddChild(slot_node);

                                    var t_abstractchannel = (from p in rootcard.T_AbstractChannelInfo where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code orderby p.CHNum select p).ToList();//按通道号顺序
                                    if (t_abstractchannel.Count > 0)
                                    {
                                        slot.RelayChannelInfo = new List<RelayChannelInfo>();
                                    }
                                    for (int ichannel = 0; ichannel < t_abstractchannel.Count; ichannel++)
                                    {
                                        var t_channel = (from p in rootcard.T_RelayChannelInfo where p.T_AbstractChannelInfo_Code == t_abstractchannel[ichannel].Code select p).FirstOrDefault();
                                        if (t_channel != null)//t_channel一定不为空，除非数据不完整
                                        {
                                            var channel = _convertFromDataBaseFormat.BaseChannelConvert(t_channel) as RelayChannelInfo;
                                            _convertFromDataBaseFormat.AbstractChannelInfoConvert(channel, t_abstractchannel[ichannel]);
                                            //channel.T_RelayChannelInfo = t_channel;//添加引用
                                            //channel.T_AbstractChannelInfo = t_abstractchannel[ichannel];//添加引用
                                            slot.RelayChannelInfo.Add(channel);
                                            ChannelTreeItemViewModel channel_node = new ChannelTreeItemViewModel(channel);
                                            slot_node.AddChild(channel_node);

                                            //分频
                                        }
                                    }
                                    continue;
                                }
                            }
                            #endregion
                            #region DigitRransducerIn 
                            {
                                var t_slot = (from p in rootcard.T_DigitRransducerInSlot where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code select p).FirstOrDefault();
                                if (t_slot != null)
                                {
                                    var slot = _convertFromDataBaseFormat.BaseSlotConvert(t_slot) as DigitRransducerInSlot;
                                    _convertFromDataBaseFormat.AbstractSlotInfoConvert(slot, t_abstractslot[islot]);
                                    //slot.T_DigitRransducerInSlot = t_slot;//添加引用
                                    //slot.T_AbstractSlotInfo = t_abstractslot[islot];//添加引用
                                    card.DigitRransducerInSlot = slot;
                                    SlotTreeItemViewModel slot_node = new SlotTreeItemViewModel(slot);
                                    card_node.AddChild(slot_node);

                                    var t_abstractchannel = (from p in rootcard.T_AbstractChannelInfo where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code orderby p.CHNum select p).ToList();//按通道号顺序
                                    if (t_abstractchannel.Count > 0)
                                    {
                                        slot.DigitRransducerInChannelInfo = new List<DigitRransducerInChannelInfo>();
                                    }
                                    for (int ichannel = 0; ichannel < t_abstractchannel.Count; ichannel++)
                                    {
                                        var t_channel = (from p in rootcard.T_DigitRransducerInChannelInfo where p.T_AbstractChannelInfo_Code == t_abstractchannel[ichannel].Code select p).FirstOrDefault();
                                        if (t_channel != null)//t_channel一定不为空，除非数据不完整
                                        {
                                            var channel = _convertFromDataBaseFormat.BaseChannelConvert(t_channel) as DigitRransducerInChannelInfo;
                                            _convertFromDataBaseFormat.AbstractChannelInfoConvert(channel, t_abstractchannel[ichannel]);
                                            //channel.T_DigitRransducerInChannelInfo = t_channel;//添加引用
                                            //channel.T_AbstractChannelInfo = t_abstractchannel[ichannel];//添加引用
                                            slot.DigitRransducerInChannelInfo.Add(channel);
                                            ChannelTreeItemViewModel channel_node = new ChannelTreeItemViewModel(channel);
                                            slot_node.AddChild(channel_node);

                                            //分频
                                        }
                                    }
                                    continue;
                                }
                            }
                            #endregion
                            #region DigitRransducerOut 
                            {
                                var t_slot = (from p in rootcard.T_DigitRransducerOutSlot where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code select p).FirstOrDefault();
                                if (t_slot != null)
                                {
                                    var slot = _convertFromDataBaseFormat.BaseSlotConvert(t_slot) as DigitRransducerOutSlot;
                                    _convertFromDataBaseFormat.AbstractSlotInfoConvert(slot, t_abstractslot[islot]);
                                    //slot.T_DigitRransducerOutSlot = t_slot;//添加引用
                                    //slot.T_AbstractSlotInfo = t_abstractslot[islot];//添加引用
                                    card.DigitRransducerOutSlot = slot;
                                    SlotTreeItemViewModel slot_node = new SlotTreeItemViewModel(slot);
                                    card_node.AddChild(slot_node);

                                    var t_abstractchannel = (from p in rootcard.T_AbstractChannelInfo where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code orderby p.CHNum select p).ToList();//按通道号顺序
                                    if (t_abstractchannel.Count > 0)
                                    {
                                        slot.DigitRransducerOutChannelInfo = new List<DigitRransducerOutChannelInfo>();
                                    }
                                    for (int ichannel = 0; ichannel < t_abstractchannel.Count; ichannel++)
                                    {
                                        var t_channel = (from p in rootcard.T_DigitRransducerOutChannelInfo where p.T_AbstractChannelInfo_Code == t_abstractchannel[ichannel].Code select p).FirstOrDefault();
                                        if (t_channel != null)//t_channel一定不为空，除非数据不完整
                                        {
                                            var channel = _convertFromDataBaseFormat.BaseChannelConvert(t_channel) as DigitRransducerOutChannelInfo;
                                            _convertFromDataBaseFormat.AbstractChannelInfoConvert(channel, t_abstractchannel[ichannel]);
                                            //channel.T_DigitRransducerOutChannelInfo = t_channel;//添加引用
                                            //channel.T_AbstractChannelInfo = t_abstractchannel[ichannel];//添加引用
                                            slot.DigitRransducerOutChannelInfo.Add(channel);
                                            ChannelTreeItemViewModel channel_node = new ChannelTreeItemViewModel(channel);
                                            slot_node.AddChild(channel_node);

                                            //分频
                                        }
                                    }
                                    continue;
                                }
                            }
                            #endregion
                            #region AnalogRransducerOut 
                            {
                                var t_slot = (from p in rootcard.T_AnalogRransducerOutSlot where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code select p).FirstOrDefault();
                                if (t_slot != null)
                                {
                                    var slot = _convertFromDataBaseFormat.BaseSlotConvert(t_slot) as AnalogRransducerOutSlot;
                                    _convertFromDataBaseFormat.AbstractSlotInfoConvert(slot, t_abstractslot[islot]);
                                    //slot.T_AnalogRransducerOutSlot = t_slot;//添加引用
                                    //slot.T_AbstractSlotInfo = t_abstractslot[islot];//添加引用
                                    card.AnalogRransducerOutSlot = slot;
                                    SlotTreeItemViewModel slot_node = new SlotTreeItemViewModel(slot);
                                    card_node.AddChild(slot_node);

                                    var t_abstractchannel = (from p in rootcard.T_AbstractChannelInfo where p.T_AbstractSlotInfo_Code == t_abstractslot[islot].Code orderby p.CHNum select p).ToList();//按通道号顺序
                                    if (t_abstractchannel.Count > 0)
                                    {
                                        slot.AnalogRransducerOutChannelInfo = new List<AnalogRransducerOutChannelInfo>();
                                    }
                                    for (int ichannel = 0; ichannel < t_abstractchannel.Count; ichannel++)
                                    {
                                        var t_channel = (from p in rootcard.T_AnalogRransducerOutChannelInfo where p.T_AbstractChannelInfo_Code == t_abstractchannel[ichannel].Code select p).FirstOrDefault();
                                        if (t_channel != null)//t_channel一定不为空，除非数据不完整
                                        {
                                            var channel = _convertFromDataBaseFormat.BaseChannelConvert(t_channel) as AnalogRransducerOutChannelInfo;
                                            _convertFromDataBaseFormat.AbstractChannelInfoConvert(channel, t_abstractchannel[ichannel]);
                                            //channel.T_AnalogRransducerOutChannelInfo = t_channel;//添加引用
                                            //channel.T_AbstractChannelInfo = t_abstractchannel[ichannel];//添加引用
                                            slot.AnalogRransducerOutChannelInfo.Add(channel);
                                            ChannelTreeItemViewModel channel_node = new ChannelTreeItemViewModel(channel);
                                            slot_node.AddChild(channel_node);

                                            //分频
                                        }
                                    }
                                    continue;
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion

                    #region 无线接收卡

                    var wirelessReceiveCard = (from p in rootcard.T_WirelessReceiveCard where p.T_MainControlCard_IP == mainCard.IP select p).FirstOrDefault();//只有一块接收板
                    if (wirelessReceiveCard != null)
                    {
                        maincard_node.WirelessReceiveCard = _convertFromDataBaseFormat.WirelessReceiveCardConvert(wirelessReceiveCard);
                        //maincard_node.WirelessReceiveCard.T_WirelessReceiveCard = wirelessReceiveCard;//添加引用
                        WirelessReceiveCardTreeItemViewModel wirelesscard_node = new WirelessReceiveCardTreeItemViewModel(maincard_node.WirelessReceiveCard);
                        maincard_node.AddChild(wirelesscard_node);

                        var t_transmissionCard = (from p in rootcard.T_TransmissionCard where p.T_WirelessReceiveCard_Code == wirelessReceiveCard.Code orderby p.SlaveIdentifier select p).ToList();//按SlaveId顺序
                        if (t_transmissionCard.Count > 0)
                        {
                            maincard_node.WirelessReceiveCard.TransmissionCard = new List<TransmissionCard>();
                        }

                        for (int icard = 0; icard < t_transmissionCard.Count; icard++)
                        {
                            var card = _convertFromDataBaseFormat.TransmissionCardConvert(t_transmissionCard[icard]);
                            //card.T_TransmissionCard = t_transmissionCard[icard];//添加引用
                            maincard_node.WirelessReceiveCard.TransmissionCard.Add(card);
                            TransmissionCardTreeItemViewModel card_node = new TransmissionCardTreeItemViewModel(card);
                            wirelesscard_node.AddChild(card_node);

                            #region Scalar 
                            {
                                var t_slot = (from p in rootcard.T_WirelessScalarSlot where p.T_TransmissionCard_Code == t_transmissionCard[icard].Code select p).FirstOrDefault();
                                if (t_slot != null)
                                {
                                    var slot = _convertFromDataBaseFormat.BaseSlotConvert(t_slot) as WirelessScalarSlot;
                                    //slot.T_WirelessScalarSlot = t_slot;//添加引用                                   
                                    card.WirelessScalarSlot = slot;
                                    SlotTreeItemViewModel slot_node = new SlotTreeItemViewModel(slot);
                                    card_node.AddChild(slot_node);

                                    var t_abstractchannel = (from p in rootcard.T_AbstractChannelInfo where p.T_AbstractSlotInfo_Code == t_slot.Code orderby p.CHNum select p).ToList();//按通道号顺序
                                    if (t_abstractchannel.Count > 0)
                                    {
                                        slot.WirelessScalarChannelInfo = new List<WirelessScalarChannelInfo>();
                                    }
                                    for (int ichannel = 0; ichannel < t_abstractchannel.Count; ichannel++)
                                    {
                                        var t_channel = (from p in rootcard.T_WirelessScalarChannelInfo where p.T_AbstractChannelInfo_Code == t_abstractchannel[ichannel].Code select p).FirstOrDefault();
                                        if (t_channel != null)//t_channel一定不为空，除非数据不完整
                                        {
                                            var channel = _convertFromDataBaseFormat.BaseChannelConvert(t_channel) as WirelessScalarChannelInfo;
                                            _convertFromDataBaseFormat.AbstractChannelInfoConvert(channel, t_abstractchannel[ichannel]);
                                            //channel.T_WirelessScalarChannelInfo = t_channel;//添加引用
                                            //channel.T_AbstractChannelInfo = t_abstractchannel[ichannel];//添加引用
                                            slot.WirelessScalarChannelInfo.Add(channel);
                                            ChannelTreeItemViewModel channel_node = new ChannelTreeItemViewModel(channel);
                                            slot_node.AddChild(channel_node);

                                            //分频                                           
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region WirelessVibration 
                            {
                                var t_slot = (from p in rootcard.T_WirelessVibrationSlot where p.T_TransmissionCard_Code == t_transmissionCard[icard].Code select p).FirstOrDefault();
                                if (t_slot != null)
                                {
                                    var slot = _convertFromDataBaseFormat.BaseSlotConvert(t_slot) as WirelessVibrationSlot;
                                    //slot.T_WirelessVibrationSlot = t_slot;//添加引用
                                    card.WirelessVibrationSlot = slot;
                                    SlotTreeItemViewModel slot_node = new SlotTreeItemViewModel(slot);
                                    card_node.AddChild(slot_node);

                                    var t_abstractchannel = (from p in rootcard.T_AbstractChannelInfo where p.T_AbstractSlotInfo_Code == t_slot.Code orderby p.CHNum select p).ToList();//按通道号顺序
                                    if (t_abstractchannel.Count > 0)
                                    {
                                        slot.WirelessVibrationChannelInfo = new List<WirelessVibrationChannelInfo>();
                                    }
                                    for (int ichannel = 0; ichannel < t_abstractchannel.Count; ichannel++)
                                    {
                                        var t_channel = (from p in rootcard.T_WirelessVibrationChannelInfo where p.T_AbstractChannelInfo_Code == t_abstractchannel[ichannel].Code select p).FirstOrDefault();
                                        if (t_channel != null)//t_channel一定不为空，除非数据不完整
                                        {
                                            var channel = _convertFromDataBaseFormat.BaseChannelConvert(t_channel) as WirelessVibrationChannelInfo;
                                            _convertFromDataBaseFormat.AbstractChannelInfoConvert(channel, t_abstractchannel[ichannel]);
                                            //channel.T_WirelessVibrationChannelInfo = t_channel;//添加引用
                                            //channel.T_AbstractChannelInfo = t_abstractchannel[ichannel];//添加引用
                                            slot.WirelessVibrationChannelInfo.Add(channel);
                                            ChannelTreeItemViewModel channel_node = new ChannelTreeItemViewModel(channel);
                                            slot_node.AddChild(channel_node);

                                            //分频
                                            var t_divfreinfo = (from p in rootcard.T_DivFreInfo where p.T_AbstractChannelInfo_Code == t_abstractchannel[ichannel].Code orderby p.Create_Time select p).ToList();
                                            if (t_divfreinfo.Count > 0)
                                            {
                                                channel.DivFreInfo = new ObservableCollection<DivFreInfo>();
                                                for (int idivfreinfo = 0; idivfreinfo < t_divfreinfo.Count; idivfreinfo++)
                                                {
                                                    var divfreinfo = _convertFromDataBaseFormat.DivFreInfoConvert(t_divfreinfo[idivfreinfo]);
                                                    //divfreinfo.T_DivFreInfo = t_divfreinfo[idivfreinfo];//添加引用  
                                                    channel.DivFreInfo.Add(divfreinfo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                        }
                    }
                    #endregion

                }
            }
        }

        public async Task<List<ChannelTreeItemViewModel>> AddTransmissionCard(string serverIP, string maincardIP, string identifier, string json)
        {
            if (ServerTreeItems != null && ServerTreeItems.Count > 0)
            {
                var server = (from p in ServerTreeItems where p.ServerIP == serverIP select p).FirstOrDefault();
                if (server != null && server.Children.Count > 0)
                {
                    var root = await _databaseComponent.ComplexWithJson(serverIP, maincardIP, json, null, null, null);
                    if (root != null)
                    {
                        var maincard = server.Children.OfType<MainCardTreeItemViewModel>().Where(p => p.MainControlCardIP == maincardIP).FirstOrDefault();
                        var receivecard = maincard.Children.OfType<WirelessReceiveCardTreeItemViewModel>().FirstOrDefault();

                        var t_rootcard = CardTreeGenerate.GenerateTransmissionCard(root, maincardIP, identifier);

                        if (await _databaseComponent.UploadHardwave(serverIP, t_rootcard) == true)
                        {
                            var transmissionCard = root.WirelessReceiveCard.TransmissionCard.Where(p => p.SlaveIdentifier == identifier).FirstOrDefault();
                            maincard.WirelessReceiveCard.TransmissionCard.Add(transmissionCard);
                            var channels = CardTreeGenerate.GetWirelessReceiveCardTree(receivecard, transmissionCard);
                            return channels;
                        }
                    }
                }
            }
            return null;
        }

        public async Task<List<ChannelTreeItemViewModel>> DeleteTransmissionCard(string serverIP, string maincardIP, string identifier, string json)
        {
            if (ServerTreeItems != null && ServerTreeItems.Count > 0)
            {
                var server = (from p in ServerTreeItems where p.ServerIP == serverIP select p).FirstOrDefault();
                if (server != null && server.Children.Count > 0)
                {
                    var maincard = server.Children.OfType<MainCardTreeItemViewModel>().Where(p => p.MainControlCardIP == maincardIP).FirstOrDefault();
                    var receivecard = maincard.Children.OfType<WirelessReceiveCardTreeItemViewModel>().FirstOrDefault();
                    var transmissioncard = receivecard.Children.OfType<TransmissionCardTreeItemViewModel>().Where(p => p.SlaveIdentifier == identifier).FirstOrDefault();

                    var card = maincard.WirelessReceiveCard.TransmissionCard.Where(p => p.SlaveIdentifier == identifier).FirstOrDefault();

                    T1_RootCard t_rootcard = new T1_RootCard();
                    CardTreeGenerate.FindTransmissionCard(t_rootcard, card);
                    //找到所有的通道
                    var channels = _cardProcess.GetChannels(transmissioncard.Children);
                    var deleteDic = RootToDictionary(t_rootcard);
                    var root = await _databaseComponent.ComplexWithJson(serverIP, maincardIP, json, null, null, deleteDic);
                    if (root != null)
                    {
                        transmissioncard.Parent.RemoveChild(transmissioncard);
                        maincard.WirelessReceiveCard.TransmissionCard.Remove(card);
                        return channels;
                    }
                }
            }
            return null;
        }
    }
}

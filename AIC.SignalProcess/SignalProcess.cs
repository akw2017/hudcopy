using AIC.Core;
using AIC.Core.DataModels;
using AIC.Core.Events;
using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.Core.Servers;
using AIC.Core.SignalModels;
using AIC.CoreType;
using AIC.M9600.Client.DataProvider;
using AIC.M9600.Common.DTO.Device;
using AIC.M9600.Common.DTO.Web;
using AIC.ServiceInterface;
using AIC.SignalProcess.Tests;
using AIC.MatlabMath;
using Kent.Boogaart.Truss;
using Newtonsoft.Json;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIC.M9600.Common.SlaveDB.Generated;

namespace AIC.LocalConfiguration
{
    public class SignalProcess : ISignalProcess
    {
        private readonly IOrganizationService _organizationService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDatabaseComponent _databaseComponent;
        private readonly ICardProcess _cardProcess;
        private readonly IHardwareService _hardwareService;

        public event SignalChangedEvent SignalsAdded;
        public event SignalChangedEvent SignalsRemoved;

        public SignalProcess(IOrganizationService organizationService, IEventAggregator eventAggregator, IDatabaseComponent databaseComponent, ICardProcess cardProcess, IHardwareService hardwareService)
        {
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _databaseComponent = databaseComponent;
            _cardProcess = cardProcess;
            _hardwareService = hardwareService;

            bindingManager = new BindingManager();
            SgDict = new Dictionary<Guid, BaseAlarmSignal>();
        }

        #region 属性与字段
        private IDictionary<Guid, BaseAlarmSignal> SgDict;
        public IEnumerable<BaseAlarmSignal> Signals
        {
            get { return SgDict.Values; }
        }
        public BaseAlarmSignal GetSignal(Guid guid)
        {
            if (SgDict.ContainsKey(guid))
            {
                return SgDict[guid];
            }
            else
            {
                return null;
            }
        }
        #endregion

        private bool isSuspend;
        public void Suspend(bool suspend)
        {
            isSuspend = suspend;
        }

        private void NotifySignalsAdded(BaseAlarmSignal signals)
        {
            SignalChangedEvent handler = SignalsAdded;
            if (handler != null)
            {
                handler(signals);
            }
        }

        private void NotifySignalsRemoved(BaseAlarmSignal signal)
        {
            SignalChangedEvent handler = SignalsRemoved;
            if (handler != null)
            {
                handler(signal);
            }
        }

        public void Initialize()
        {

        }

        private BindingManager bindingManager;
        public void InitSignals()
        {
            SgDict.Clear();
            foreach (var item in _organizationService.ItemTreeItems.Where(p => p.IsPaired == true))
            {
                item.InitSignal();
                item.BaseAlarmSignal.HasBattery = false;
                if (item.BaseAlarmSignal is IEPEChannelSignal)
                {
                    var sg = item.BaseAlarmSignal as IEPEChannelSignal;
                }
                else if (item.BaseAlarmSignal is EddyCurrentDisplacementChannelSignal)
                {
                    var sg = item.BaseAlarmSignal as EddyCurrentDisplacementChannelSignal;
                }
                else if (item.BaseAlarmSignal is EddyCurrentKeyPhaseChannelSignal)
                {
                    var sg = item.BaseAlarmSignal as EddyCurrentKeyPhaseChannelSignal;
                }
                else if (item.BaseAlarmSignal is EddyCurrentTachometerChannelSignal)
                {
                    var sg = item.BaseAlarmSignal as EddyCurrentTachometerChannelSignal;
                }
                else if (item.BaseAlarmSignal is DigitTachometerChannelSignal)
                {
                    var sg = item.BaseAlarmSignal as DigitTachometerChannelSignal;
                }
                else if (item.BaseAlarmSignal is AnalogRransducerInChannelSignal)
                {
                    var sg = item.BaseAlarmSignal as AnalogRransducerInChannelSignal;
                }
                else if (item.BaseAlarmSignal is RelayChannelSignal)
                {
                    var sg = item.BaseAlarmSignal as RelayChannelSignal;
                }
                else if (item.BaseAlarmSignal is DigitRransducerInChannelSignal)
                {
                    var sg = item.BaseAlarmSignal as DigitRransducerInChannelSignal;
                }
                else if (item.BaseAlarmSignal is DigitRransducerOutChannelSignal)
                {
                    var sg = item.BaseAlarmSignal as DigitRransducerOutChannelSignal;
                }
                else if (item.BaseAlarmSignal is AnalogRransducerOutChannelSignal)
                {
                    var sg = item.BaseAlarmSignal as AnalogRransducerOutChannelSignal;
                }
                else if (item.BaseAlarmSignal is WirelessScalarChannelSignal)
                {
                    var sg = item.BaseAlarmSignal as WirelessScalarChannelSignal;
                    sg.HasBattery = true;
                }
                else if (item.BaseAlarmSignal is WirelessVibrationChannelSignal)
                {
                    var sg = item.BaseAlarmSignal as WirelessVibrationChannelSignal;
                    sg.HasBattery = true;
                }
                if (!SgDict.ContainsKey(item.T_Item.Guid))
                {
                    //获取显示名称
                    //item.BaseAlarmSignal.OrganizationName = _cardProcess.GetOrganizationName(item.Parent.Parent);
                    //item.BaseAlarmSignal.DeviceName = item.Parent.Name;
                    //item.BaseAlarmSignal.ItemName = item.Name;
                    item.BaseAlarmSignal.Names = item.Names;
                    item.BaseAlarmSignal.IP = item.T_Item.IP;
                    item.BaseAlarmSignal.ServerIP = item.ServerIP;

                    //延时报警,改为延时加载
                    //var channel = _cardProcess.GetChannel(_hardwareService.ServerTreeItems, item.T_Item);
                    //if (channel != null)
                    //{
                    //    item.BaseAlarmSignal.DelayAlarmTime = channel.IChannel.DelayAlarmTime;
                    //    item.BaseAlarmSignal.SubscribeAlarmGrade(item.BaseAlarmSignal.DelayAlarmTime);
                    //    item.BaseAlarmSignal.NotOKDelayAlarmTime = channel.IChannel.NotOKDelayAlarmTime;
                    //    item.BaseAlarmSignal.SubscribeIsNotOK(item.BaseAlarmSignal.NotOKDelayAlarmTime);
                    //}

                    SgDict.Add(item.T_Item.Guid, item.BaseAlarmSignal);
                    bindingManager.Bindings.Add(new TypedBinding<ItemTreeItemViewModel, BaseAlarmSignal>(item, tm => tm.Alarm, item.BaseAlarmSignal, s => s.DelayAlarmGrade) { Mode = BindingMode.OneWayToTarget });
                    bindingManager.Bindings.Add(new TypedBinding<ItemTreeItemViewModel, BaseAlarmSignal>(item, tm => tm.IsConnected, item.BaseAlarmSignal, s => s.IsConnected) { Mode = BindingMode.OneWayToTarget });
                    bindingManager.Bindings.Add(new TypedBinding<ItemTreeItemViewModel, BaseAlarmSignal>(item, tm => tm.IsRunning, item.BaseAlarmSignal, s => s.IsRunning) { Mode = BindingMode.OneWayToTarget });
                    bindingManager.Bindings.Add(new TypedBinding<ItemTreeItemViewModel, BaseAlarmSignal>(item, tm => tm.Names, item.BaseAlarmSignal, s => s.Names) { Mode = BindingMode.OneWayToSource });
                    if (item.Children != null && item.Children.Count > 0)
                    {
                        foreach (var div in item.Children)
                        {
                            DivFreTreeItemViewModel divfreTM = div as DivFreTreeItemViewModel;
                            //DivFreSignal divFre = new DivFreSignal() { DisPlayName = divfreTM.Name };
                            DivFreSignal divFre = divfreTM.DivFreSignal;
                            ((BaseDivfreSignal)item.BaseAlarmSignal).AddDivFre(divFre);
                            bindingManager.Bindings.Add(new TypedBinding<DivFreTreeItemViewModel, DivFreSignal>(divfreTM, tm => tm.Alarm, divFre, d => d.DelayAlarmGrade) { Mode = BindingMode.OneWayToTarget });
                            bindingManager.Bindings.Add(new TypedBinding<DivFreTreeItemViewModel, DivFreSignal>(divfreTM, tm => tm.IsConnected, divFre, d => d.IsConnected) { Mode = BindingMode.OneWayToTarget });
                            bindingManager.Bindings.Add(new TypedBinding<DivFreTreeItemViewModel, DivFreSignal>(divfreTM, tm => tm.Name, divFre, d => d.DisPlayName) { Mode = BindingMode.OneWayToSource });
                        }
                    }
                }
            }
        }
        public void LazyInitSignals()
        {
            foreach (var item in _organizationService.ItemTreeItems.Where(p => p.IsPaired == true))
            {
                //延时报警
                var channel = _cardProcess.GetChannel(_hardwareService.ServerTreeItems, item.T_Item);
                if (channel != null)
                {
                    item.BaseAlarmSignal.DelayAlarmTime = channel.IChannel.DelayAlarmTime;
                    item.BaseAlarmSignal.SubscribeAlarmGrade(item.BaseAlarmSignal.DelayAlarmTime);
                    item.BaseAlarmSignal.NotOKDelayAlarmTime = channel.IChannel.NotOKDelayAlarmTime;
                    item.BaseAlarmSignal.SubscribeIsNotOK(item.BaseAlarmSignal.NotOKDelayAlarmTime);
                }
            }
        }
        public void BindItem(ItemTreeItemViewModel item)
        {
            if (item == null) return;
            if (!SgDict.ContainsKey(item.T_Item.Guid))
            {
                lock (SgDict)
                {
                    item.InitSignal();
                    if (item.BaseAlarmSignal is WirelessScalarChannelSignal)
                    {
                        item.BaseAlarmSignal.HasBattery = true;
                    }
                    else if (item.BaseAlarmSignal is WirelessVibrationChannelSignal)
                    {
                        item.BaseAlarmSignal.HasBattery = true;
                    }
                    else
                    {
                        item.BaseAlarmSignal.HasBattery = false;
                    }

                    //获取显示名称
                    //item.BaseAlarmSignal.OrganizationName = _cardProcess.GetOrganizationName(item.Parent.Parent);
                    //item.BaseAlarmSignal.DeviceName = item.Parent.Name;
                    //item.BaseAlarmSignal.ItemName = item.Name;
                    item.BaseAlarmSignal.Names = item.Names;
                    item.BaseAlarmSignal.IP = item.T_Item.IP;
                    item.BaseAlarmSignal.ServerIP = item.ServerIP;

                    //延时报警
                    var channel = _cardProcess.GetChannel(_hardwareService.ServerTreeItems, item.T_Item);
                    if (channel != null)
                    {
                        item.BaseAlarmSignal.DelayAlarmTime = channel.IChannel.DelayAlarmTime;
                        item.BaseAlarmSignal.SubscribeAlarmGrade(item.BaseAlarmSignal.DelayAlarmTime);
                        item.BaseAlarmSignal.NotOKDelayAlarmTime = channel.IChannel.NotOKDelayAlarmTime;
                        item.BaseAlarmSignal.SubscribeIsNotOK(item.BaseAlarmSignal.NotOKDelayAlarmTime);
                    }

                    SgDict.Add(item.T_Item.Guid, item.BaseAlarmSignal);

                    bindingManager.Bindings.Add(new TypedBinding<ItemTreeItemViewModel, BaseAlarmSignal>(item, tm => tm.Alarm, item.BaseAlarmSignal, s => s.DelayAlarmGrade) { Mode = BindingMode.OneWayToTarget });
                    bindingManager.Bindings.Add(new TypedBinding<ItemTreeItemViewModel, BaseAlarmSignal>(item, tm => tm.IsConnected, item.BaseAlarmSignal, s => s.IsConnected) { Mode = BindingMode.OneWayToTarget });
                    bindingManager.Bindings.Add(new TypedBinding<ItemTreeItemViewModel, BaseAlarmSignal>(item, tm => tm.IsRunning, item.BaseAlarmSignal, s => s.IsRunning) { Mode = BindingMode.OneWayToTarget });
                    bindingManager.Bindings.Add(new TypedBinding<ItemTreeItemViewModel, BaseAlarmSignal>(item, tm => tm.Names, item.BaseAlarmSignal, s => s.Names) { Mode = BindingMode.OneWayToSource });
                    if (item.Children != null && item.Children.Count >0)
                    {
                        foreach (var child in item.Children)
                        {
                            var divfreTM = child as DivFreTreeItemViewModel;
                            //DivFreSignal divFre = new DivFreSignal();
                            //divFre.DisPlayName = divfreTM.Name;
                            DivFreSignal divFre = divfreTM.DivFreSignal;
                            ((BaseDivfreSignal)item.BaseAlarmSignal).AddDivFre(divFre);
                            bindingManager.Bindings.Add(new TypedBinding<DivFreTreeItemViewModel, DivFreSignal>(divfreTM, tm => tm.Alarm, divFre, d => d.DelayAlarmGrade) { Mode = BindingMode.OneWayToTarget });
                            bindingManager.Bindings.Add(new TypedBinding<DivFreTreeItemViewModel, DivFreSignal>(divfreTM, tm => tm.IsConnected, divFre, d => d.IsConnected) { Mode = BindingMode.OneWayToTarget });
                            bindingManager.Bindings.Add(new TypedBinding<DivFreTreeItemViewModel, DivFreSignal>(divfreTM, tm => tm.Name, divFre, d => d.DisPlayName) { Mode = BindingMode.OneWayToSource });
                        }
                    }

                    NotifySignalsAdded(item.BaseAlarmSignal);
                }
            }
        }
        public void UnBindItem(ItemTreeItemViewModel item)
        {
            if (item == null) return;
            if (SgDict.ContainsKey(item.T_Item.Guid))
            {
                lock (SgDict)
                {
                    SgDict.Remove(item.T_Item.Guid);

                    var bindings = bindingManager.Bindings.OfType<TypedBinding<ItemTreeItemViewModel, BaseAlarmSignal>>().Where(o => o.TargetObject == item).ToArray();
                    foreach (var binding in bindings)
                    {
                        bindingManager.Bindings.Remove(binding);
                    }
                    if (item.Children != null && item.Children.Count > 0)
                    {
                        foreach (var divTM in item.Children)
                        {
                            var divBindings = bindingManager.Bindings.OfType<TypedBinding<DivFreTreeItemViewModel, DivFreSignal>>().Where(o => o.TargetObject == divTM).ToArray();
                            foreach (var divBinding in divBindings)
                            {
                                bindingManager.Bindings.Remove(divBinding);
                            }
                        }
                    }

                    NotifySignalsRemoved(item.BaseAlarmSignal);
                }
            }
        }
        public void AddDivfre(DivFreTreeItemViewModel divfreTM)
        {
            if (divfreTM == null) return;
            var item = divfreTM.Parent as ItemTreeItemViewModel;
            if (item == null) return;
            if (item.IsPaired)
            {
                lock (SgDict)
                {
                    if (SgDict.ContainsKey(item.T_Item.Guid))
                    {
                        var sg = SgDict[item.T_Item.Guid];
                        if (sg is BaseDivfreSignal)
                        {
                            var vSg = sg as BaseDivfreSignal;
                            //DivFreSignal divFre = new DivFreSignal(divfreTM.Name);                           
                            vSg.AddDivFre(divfreTM.DivFreSignal);

                            bindingManager.Bindings.Add(new TypedBinding<DivFreTreeItemViewModel, DivFreSignal>(divfreTM, tm => tm.Alarm, divfreTM.DivFreSignal, d => d.DelayAlarmGrade) { Mode = BindingMode.OneWayToTarget });
                            bindingManager.Bindings.Add(new TypedBinding<DivFreTreeItemViewModel, DivFreSignal>(divfreTM, tm => tm.IsConnected, divfreTM.DivFreSignal, d => d.IsConnected) { Mode = BindingMode.OneWayToTarget });
                            bindingManager.Bindings.Add(new TypedBinding<DivFreTreeItemViewModel, DivFreSignal>(divfreTM, tm => tm.Name, divfreTM.DivFreSignal, d => d.DisPlayName) { Mode = BindingMode.OneWayToSource });
                        }
                    }
                }
            }
        }
        public void DeleteDivfre(DivFreTreeItemViewModel divfreTM)
        {
            if (divfreTM == null) return;
            var item = divfreTM.Parent as ItemTreeItemViewModel;
            if (item == null) return;
            if (item.IsPaired)
            {
                lock (SgDict)
                {
                    if (SgDict.ContainsKey(item.T_Item.Guid))
                    {
                        var sg = SgDict[item.T_Item.Guid];
                        if (sg is BaseDivfreSignal)
                        {
                            var vSg = sg as BaseDivfreSignal;
                            //DivFreSignal divFre = vSg.DivFres.Where(o => o.DisPlayName == divfreTM.Name).SingleOrDefault();
                            DivFreSignal divFre = divfreTM.DivFreSignal;
                            if (divFre != null)
                            {
                                vSg.RemoveDivFre(divFre);
                            }
                        }
                    }
                }

                lock (bindingManager.Bindings)
                {
                    var bindings = bindingManager.Bindings.OfType<TypedBinding<DivFreTreeItemViewModel, DivFreSignal>>().Where(o => o.TargetObject == divfreTM).ToArray();
                    foreach (var binding in bindings)
                    {
                        bindingManager.Bindings.Remove(binding);
                    }
                }
            }
        }
        public async Task<bool> GetSignalData(DateTime time, bool isHistoryMode)
        {
            try
            {
                Dictionary<string, LatestSampleData> latestdata = new Dictionary<string, LatestSampleData>();

                if (isHistoryMode == true)
                {
                    latestdata = await GetLatestHistoryData(time, LocalSetting.HistoryModeDataInterval);
                    return await CheckedSignal(latestdata);
                }
                else
                {
                    latestdata = await _databaseComponent.GetLatestData();
                    return await CheckedSignal(latestdata);
                }
               
            }
            catch(Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据获取", ex));
            }
            return true;
        }
        private async Task<bool> CheckedSignal(Dictionary<string, LatestSampleData> latestdata)
        {
            if (latestdata == null)
            {
                foreach (var sg in Signals)
                {
                    sg.IsConnected = false;
                    sg.IsRunning = false;
                    if (sg is BaseAlarmSignal)
                    {
                        ((BaseAlarmSignal)sg).AlarmGrade = AlarmGrade.DisConnect;
                        //((BaseAlarmSignal)sg).DelayAlarmGrade = AlarmGrade.DisConnect;//断线不延时                          
                    }
                }
                return false;
            }
            else if (latestdata.Count == 0)
            {
                foreach (var sg in Signals)
                {
                    sg.IsConnected = false;
                    sg.IsRunning = false;
                    if (sg is BaseAlarmSignal)
                    {
                        ((BaseAlarmSignal)sg).AlarmGrade = AlarmGrade.DisConnect;
                        //((BaseAlarmSignal)sg).DelayAlarmGrade = AlarmGrade.DisConnect;//断线不延时                                                 
                    }
                }
            }
            else
            {
                foreach (var data in latestdata.Values)
                {
                    await ProcessSignal(data);
                }
            }
            return true;
        }
        private async Task<Dictionary<string, LatestSampleData>> GetLatestHistoryData(DateTime historyplayTime, float dataInterval)
        {
            Dictionary<string, LatestSampleData> latestdata = new Dictionary<string, LatestSampleData>();
            var GroupSignals = Signals.GroupBy(p => p.ServerIP);
            foreach (var subSignals in GroupSignals)
            {
                Dictionary<Guid, string> itemGuids = new Dictionary<Guid, string>();

                foreach (var sg in subSignals)
                {
                    string sgtype = string.Empty;
                    if (sg is IEPEChannelSignal)
                    {
                        sgtype = "D_IEPESlot";
                    }
                    else if (sg is EddyCurrentDisplacementChannelSignal)
                    {
                        sgtype = "D_EddyCurrentDisplacementSlot";
                    }
                    else if (sg is EddyCurrentKeyPhaseChannelSignal)
                    {
                        sgtype = "D_EddyCurrentKeyPhaseSlot";
                    }
                    else if (sg is EddyCurrentTachometerChannelSignal)
                    {
                        sgtype = "D_EddyCurrentTachometerSlot";
                    }
                    else if (sg is DigitTachometerChannelSignal)
                    {
                        sgtype = "D_DigitTachometerSlot";
                    }
                    else if (sg is AnalogRransducerInChannelSignal)
                    {
                        sgtype = "D_AnalogRransducerInSlot";
                    }
                    else if (sg is RelayChannelSignal)
                    {
                        sgtype = "D_RelayChannelSlot";
                    }
                    else if (sg is DigitRransducerInChannelSignal)
                    {
                        sgtype = "D_DigitRransducerInSlot";
                    }
                    else if (sg is DigitRransducerOutChannelSignal)
                    {
                        sgtype = "D_DigitRransducerOutSlot";
                    }
                    else if (sg is AnalogRransducerOutChannelSignal)
                    {
                        sgtype = "D_AnalogRransducerOutSlot";
                    }
                    else if (sg is WirelessScalarChannelSignal)
                    {
                        sgtype = "D_WirelessScalarSlot";
                    }
                    else if (sg is WirelessVibrationChannelSignal)
                    {
                        sgtype = "D_WirelessVibrationSlot";
                    }
                    else
                    {
                        continue;
                    }
                    itemGuids.Add(sg.Guid, sgtype);
                }               

                var data = await _databaseComponent.GetHistoryData(subSignals.Key, itemGuids, historyplayTime.AddSeconds(0 - dataInterval), historyplayTime);
                if (data == null)
                {
                    continue;
                }

                LatestSampleData filterdata = new LatestSampleData();
                if (data.IEPESlot != null)
                {
                    List<IEPESlotData> sublist = new List<IEPESlotData>();
                    var subdata = data.IEPESlot.GroupBy(p => p.T_Item_Guid);
                    foreach(var sub in subdata)
                    {
                        var lastsub = sub.LastOrDefault();
                        sublist.Add(lastsub);
                    }
                    filterdata.IEPESlot = sublist.ToArray();
                }
                if (data.EddyCurrentDisplacementSlot != null)
                {
                    List<EddyCurrentDisplacementSlotData> sublist = new List<EddyCurrentDisplacementSlotData>();
                    var subdata = data.EddyCurrentDisplacementSlot.GroupBy(p => p.T_Item_Guid);
                    foreach (var sub in subdata)
                    {
                        var lastsub = sub.LastOrDefault();
                        sublist.Add(lastsub);
                    }
                    filterdata.EddyCurrentDisplacementSlot = sublist.ToArray();
                }
                if (data.EddyCurrentKeyPhaseSlot != null)
                {
                    List<EddyCurrentKeyPhaseSlotData> sublist = new List<EddyCurrentKeyPhaseSlotData>();
                    var subdata = data.EddyCurrentKeyPhaseSlot.GroupBy(p => p.T_Item_Guid);
                    foreach (var sub in subdata)
                    {
                        var lastsub = sub.LastOrDefault();
                        sublist.Add(lastsub);
                    }
                    filterdata.EddyCurrentKeyPhaseSlot = sublist.ToArray();
                }
                if (data.EddyCurrentTachometerSlot != null)
                {
                    List<EddyCurrentTachometerSlotData> sublist = new List<EddyCurrentTachometerSlotData>();
                    var subdata = data.EddyCurrentTachometerSlot.GroupBy(p => p.T_Item_Guid);
                    foreach (var sub in subdata)
                    {
                        var lastsub = sub.LastOrDefault();
                        sublist.Add(lastsub);
                    }
                    filterdata.EddyCurrentTachometerSlot = sublist.ToArray();
                }
                if (data.DigitTachometerSlot != null)
                {
                    List<DigitTachometerSlotData> sublist = new List<DigitTachometerSlotData>();
                    var subdata = data.DigitTachometerSlot.GroupBy(p => p.T_Item_Guid);
                    foreach (var sub in subdata)
                    {
                        var lastsub = sub.LastOrDefault();
                        sublist.Add(lastsub);
                    }
                    filterdata.DigitTachometerSlot = sublist.ToArray();
                }
                if (data.AnalogRransducerInSlot != null)
                {
                    List<AnalogRransducerInSlotData> sublist = new List<AnalogRransducerInSlotData>();
                    var subdata = data.AnalogRransducerInSlot.GroupBy(p => p.T_Item_Guid);
                    foreach (var sub in subdata)
                    {
                        var lastsub = sub.LastOrDefault();
                        sublist.Add(lastsub);
                    }
                    filterdata.AnalogRransducerInSlot = sublist.ToArray();
                }
                if (data.RelaySlot != null)
                {
                    List<RelaySlotData> sublist = new List<RelaySlotData>();
                    var subdata = data.RelaySlot.GroupBy(p => p.T_Item_Guid);
                    foreach (var sub in subdata)
                    {
                        var lastsub = sub.LastOrDefault();
                        sublist.Add(lastsub);
                    }
                    filterdata.RelaySlot = sublist.ToArray();
                }
                if (data.DigitRransducerInSlot != null)
                {
                    List<DigitRransducerInSlotData> sublist = new List<DigitRransducerInSlotData>();
                    var subdata = data.DigitRransducerInSlot.GroupBy(p => p.T_Item_Guid);
                    foreach (var sub in subdata)
                    {
                        var lastsub = sub.LastOrDefault();
                        sublist.Add(lastsub);
                    }
                    filterdata.DigitRransducerInSlot = sublist.ToArray();
                }
                if (data.DigitRransducerOutSlot != null)
                {
                    List<DigitRransducerOutSlotData> sublist = new List<DigitRransducerOutSlotData>();
                    var subdata = data.DigitRransducerOutSlot.GroupBy(p => p.T_Item_Guid);
                    foreach (var sub in subdata)
                    {
                        var lastsub = sub.LastOrDefault();
                        sublist.Add(lastsub);
                    }
                    filterdata.DigitRransducerOutSlot = sublist.ToArray();
                }
                if (data.AnalogRransducerOutSlot != null)
                {
                    List<AnalogRransducerOutSlotData> sublist = new List<AnalogRransducerOutSlotData>();
                    var subdata = data.AnalogRransducerOutSlot.GroupBy(p => p.T_Item_Guid);
                    foreach (var sub in subdata)
                    {
                        var lastsub = sub.LastOrDefault();
                        sublist.Add(lastsub);
                    }
                    filterdata.AnalogRransducerOutSlot = sublist.ToArray();
                }
                if (data.WirelessScalarSlot != null)
                {
                    List<WirelessScalarSlotData> sublist = new List<WirelessScalarSlotData>();
                    var subdata = data.WirelessScalarSlot.GroupBy(p => p.T_Item_Guid);
                    foreach (var sub in subdata)
                    {
                        var lastsub = sub.LastOrDefault();                      
                        sublist.Add(lastsub);
                    }
                    filterdata.WirelessScalarSlot = sublist.ToArray();
                }
                if (data.WirelessVibrationSlot != null)
                {
                    List<WirelessVibrationSlotData> sublist = new List<WirelessVibrationSlotData>();
                    var subdata = data.WirelessVibrationSlot.GroupBy(p => p.T_Item_Guid);
                    Dictionary<WirelessVibrationSlotData, Task<List<D_WirelessVibrationSlot_Waveform>>> lttask = new Dictionary<WirelessVibrationSlotData, Task<List<D_WirelessVibrationSlot_Waveform>>>();
                    foreach (var sub in subdata)
                    {
                        var lastsub = sub.LastOrDefault();
                        if (lastsub.IsValidWave == true)
                        {
                            //var result = (await _databaseComponent.GetHistoryData<D_WirelessVibrationSlot_Waveform>(subSignals.Key, lastsub.T_Item_Guid, new string[] { "WaveData", "SampleFre", "SamplePoint", "WaveUnit" }, lastsub.ACQDatetime.AddSeconds(-1), lastsub.ACQDatetime.AddSeconds(20), "(RecordLab = @0)", new object[] { lastsub.RecordLab })).FirstOrDefault();
                            //WirelessVibrationSlotData_Waveform waveform = new WirelessVibrationSlotData_Waveform();
                            //waveform.WaveData = result.WaveData;
                            //waveform.WaveUnit = result.WaveUnit;
                            //waveform.SampleFre = result.SampleFre;
                            //waveform.SamplePoint = result.SamplePoint;
                            //lastsub.Waveform = waveform;
                            lttask.Add(lastsub, _databaseComponent.GetHistoryData<D_WirelessVibrationSlot_Waveform>(subSignals.Key, lastsub.T_Item_Guid, new string[] { "WaveData", "SampleFre", "SamplePoint", "WaveUnit" }, lastsub.ACQDatetime.AddSeconds(-1), lastsub.ACQDatetime.AddSeconds(20), "(RecordLab = @0)", new object[] { lastsub.RecordLab }));
                        }
                        sublist.Add(lastsub);
                    }
                    if (lttask.Count > 0)
                    {
                        await Task.WhenAll(lttask.Values.ToArray());
                        foreach (var task in lttask)
                        {
                            if (task.Value != null)
                            {
                                var result = task.Value.Result.FirstOrDefault();
                                WirelessVibrationSlotData_Waveform waveform = new WirelessVibrationSlotData_Waveform();
                                waveform.WaveData = result.WaveData;
                                waveform.WaveUnit = result.WaveUnit;
                                waveform.SampleFre = result.SampleFre;
                                waveform.SamplePoint = result.SamplePoint;
                                task.Key.Waveform = waveform;
                            }
                        }
                    }
                    filterdata.WirelessVibrationSlot = sublist.ToArray();
                }

                latestdata.Add(subSignals.Key, filterdata);
            }
            return latestdata;
        }
        public async Task ProcessSignal(LatestSampleData data)
        {
            List<KeyValuePair<BaseAlarmSignal, IBaseAlarmSlot>> keyValuePairs = new List<KeyValuePair<BaseAlarmSignal, IBaseAlarmSlot>>();
            //获取数据
            if (data.IEPESlot != null)
            {
                foreach (var slotdata in data.IEPESlot)
                {
                    var signal = GetSignal(slotdata.T_Item_Guid);
                    if (signal != null)
                    {
                        var islotdata = ClassCopyHelper.AutoCopy<IEPESlotData, D1_IEPESlot>(slotdata);
                        keyValuePairs.Add(new KeyValuePair<BaseAlarmSignal, IBaseAlarmSlot>(signal, islotdata));
                    }
                }
            }
            if (data.EddyCurrentDisplacementSlot != null)
            {
                foreach (var slotdata in data.EddyCurrentDisplacementSlot)
                {
                    var signal = GetSignal(slotdata.T_Item_Guid);
                    if (signal != null)
                    {
                        var islotdata = ClassCopyHelper.AutoCopy<EddyCurrentDisplacementSlotData, D1_EddyCurrentDisplacementSlot>(slotdata);
                        keyValuePairs.Add(new KeyValuePair<BaseAlarmSignal, IBaseAlarmSlot>(signal, islotdata));
                    }
                }
            }
            if (data.EddyCurrentKeyPhaseSlot != null)
            {
                foreach (var slotdata in data.EddyCurrentKeyPhaseSlot)
                {
                    var signal = GetSignal(slotdata.T_Item_Guid);
                    if (signal != null)
                    {
                        var islotdata = ClassCopyHelper.AutoCopy<EddyCurrentKeyPhaseSlotData, D1_EddyCurrentKeyPhaseSlot>(slotdata);
                        keyValuePairs.Add(new KeyValuePair<BaseAlarmSignal, IBaseAlarmSlot>(signal, islotdata));
                    }
                }
            }
            if (data.EddyCurrentTachometerSlot != null)
            {
                foreach (var slotdata in data.EddyCurrentTachometerSlot)
                {
                    var signal = GetSignal(slotdata.T_Item_Guid);
                    if (signal != null)
                    {
                        var islotdata = ClassCopyHelper.AutoCopy<EddyCurrentTachometerSlotData, D1_EddyCurrentTachometerSlot>(slotdata);
                        keyValuePairs.Add(new KeyValuePair<BaseAlarmSignal, IBaseAlarmSlot>(signal, islotdata));
                    }
                }
            }
            if (data.DigitTachometerSlot != null)
            {
                foreach (var slotdata in data.DigitTachometerSlot)
                {
                    var signal = GetSignal(slotdata.T_Item_Guid);
                    if (signal != null)
                    {
                        var islotdata = ClassCopyHelper.AutoCopy<DigitTachometerSlotData, D1_DigitTachometerSlot>(slotdata);
                        keyValuePairs.Add(new KeyValuePair<BaseAlarmSignal, IBaseAlarmSlot>(signal, islotdata));
                    }
                }
            }
            if (data.AnalogRransducerInSlot != null)
            {
                foreach (var slotdata in data.AnalogRransducerInSlot)
                {
                    var signal = GetSignal(slotdata.T_Item_Guid);
                    if (signal != null)
                    {
                        var islotdata = ClassCopyHelper.AutoCopy<AnalogRransducerInSlotData, D1_AnalogRransducerInSlot>(slotdata);
                        keyValuePairs.Add(new KeyValuePair<BaseAlarmSignal, IBaseAlarmSlot>(signal, islotdata));
                    }
                }
            }
            if (data.RelaySlot != null)
            {
                foreach (var slotdata in data.RelaySlot)
                {
                    var signal = GetSignal(slotdata.T_Item_Guid);
                    if (signal != null)
                    {
                        var islotdata = ClassCopyHelper.AutoCopy<RelaySlotData, D1_RelaySlot>(slotdata);
                        keyValuePairs.Add(new KeyValuePair<BaseAlarmSignal, IBaseAlarmSlot>(signal, islotdata));
                    }
                }
            }
            if (data.DigitRransducerInSlot != null)
            {
                foreach (var slotdata in data.DigitRransducerInSlot)
                {
                    var signal = GetSignal(slotdata.T_Item_Guid);
                    if (signal != null)
                    {
                        var islotdata = ClassCopyHelper.AutoCopy<DigitRransducerInSlotData, D1_DigitRransducerInSlot>(slotdata);
                        keyValuePairs.Add(new KeyValuePair<BaseAlarmSignal, IBaseAlarmSlot>(signal, islotdata));
                    }
                }
            }
            if (data.DigitRransducerOutSlot != null)
            {
                foreach (var slotdata in data.DigitRransducerOutSlot)
                {
                    var signal = GetSignal(slotdata.T_Item_Guid);
                    if (signal != null)
                    {
                        var islotdata = ClassCopyHelper.AutoCopy<DigitRransducerOutSlotData, D1_DigitRransducerOutSlot>(slotdata);
                        keyValuePairs.Add(new KeyValuePair<BaseAlarmSignal, IBaseAlarmSlot>(signal, islotdata));
                    }
                }
            }
            if (data.AnalogRransducerOutSlot != null)
            {
                foreach (var slotdata in data.AnalogRransducerOutSlot)
                {
                    var signal = GetSignal(slotdata.T_Item_Guid);
                    if (signal != null)
                    {
                        var islotdata = ClassCopyHelper.AutoCopy<AnalogRransducerOutSlotData, D1_AnalogRransducerOutSlot>(slotdata);
                        keyValuePairs.Add(new KeyValuePair<BaseAlarmSignal, IBaseAlarmSlot>(signal, islotdata));
                    }
                }
            }
            if (data.WirelessScalarSlot != null)
            {
                foreach (var slotdata in data.WirelessScalarSlot)
                {
                    var signal = GetSignal(slotdata.T_Item_Guid);
                    if (signal != null)
                    {
                        var islotdata = ClassCopyHelper.AutoCopy<WirelessScalarSlotData, D1_WirelessScalarSlot>(slotdata);
                        keyValuePairs.Add(new KeyValuePair<BaseAlarmSignal, IBaseAlarmSlot>(signal, islotdata));
                    }
                }
            }
            if (data.WirelessVibrationSlot != null)
            {
                foreach (var slotdata in data.WirelessVibrationSlot)
                {
                    var signal = GetSignal(slotdata.T_Item_Guid);
                    if (signal != null)
                    {
                        var islotdata = ClassCopyHelper.AutoCopy<WirelessVibrationSlotData, D1_WirelessVibrationSlot>(slotdata);
                        keyValuePairs.Add(new KeyValuePair<BaseAlarmSignal, IBaseAlarmSlot>(signal, islotdata));
                    }
                }
            }

            List<BaseAlarmSignal> disConnectedSignals = Signals.Except(keyValuePairs.Select(o => o.Key)).ToList();
            if (disConnectedSignals.Count > 0)
            {
                foreach (var sg in disConnectedSignals)
                {
                    sg.IsConnected = false;
                    sg.IsRunning = false;
                    sg.AlarmGrade = AlarmGrade.DisConnect;
                    //sg.DelayAlarmGrade = AlarmGrade.DisConnect;//断线不延时
                    if (sg is BaseDivfreSignal && (sg as BaseDivfreSignal).DivFres.Count() > 0)
                    {
                        foreach(var divfre in (sg as BaseDivfreSignal).DivFres)
                        {
                            divfre.IsConnected = false;
                            divfre.AlarmGrade = AlarmGrade.DisConnect;
                            //divfre.DelayAlarmGrade = AlarmGrade.DisConnect;//断线不延时                           
                        }
                    }
                }
            }

            if (!isSuspend)
            {
                await Task.Run(() =>
                {
                    foreach (var keyValuePair in keyValuePairs)
                    {
                        if (keyValuePair.Key is WirelessVibrationChannelSignal)
                        {
                            SetDivfreSignal(keyValuePair.Key as WirelessVibrationChannelSignal, keyValuePair.Value);
                        }
                        else if (keyValuePair.Key is WirelessScalarChannelSignal)
                        {
                            SetAlarmSignal(keyValuePair.Key as WirelessScalarChannelSignal, keyValuePair.Value);
                        }
                    }
                    Parallel.ForEach(keyValuePairs, keyValuePair =>
                    {
                        if (keyValuePair.Key is BaseWaveSignal)
                        {
                            var vSg = keyValuePair.Key as BaseWaveSignal;
                            {
                                if (keyValuePair.Value is D1_WirelessVibrationSlot)
                                {
                                    var waveslot = keyValuePair.Value as D1_WirelessVibrationSlot;
                                    if (waveslot.Waveform == null || waveslot.Waveform.WaveData == null)
                                    {
                                        return;
                                    }

                                    //获取原始波形
                                    vSg.Waveform = Algorithm.ByteToSingle(waveslot.Waveform.WaveData);
                                    vSg.SampleFre = waveslot.SampleFre.Value;
                                    vSg.SamplePoint = waveslot.SamplePoint.Value;
                                    if (vSg.SampleFre == 0 || vSg.SamplePoint == 0)
                                    {
                                        EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("波形采样频率/点数为0", null));
                                        return;
                                    }
                                }                               

                                if (vSg.NeedProcess)
                                {
                                    try
                                    {
                                        if (vSg.SignalProcessTypes.Contains(SignalProcessorType.VData))
                                        {
                                            //if (keyValuePair.Value is D1_WirelessVibrationSlot)
                                            //{
                                            //    var waveslot = keyValuePair.Value as D1_WirelessVibrationSlot;
                                            //    if (waveslot.Waveform == null || waveslot.Waveform.WaveData == null)
                                            //    {
                                            //        return;
                                            //    }

                                            //    vSg.Waveform = Algorithm.ByteToSingle(waveslot.Waveform.WaveData);
                                            //    vSg.SampleFre = waveslot.SampleFre.Value;
                                            //    vSg.SamplePoint = waveslot.SamplePoint.Value;
                                            //    if (vSg.SampleFre == 0 || vSg.SamplePoint == 0)
                                            //    {
                                            //        EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("波形采样频率/点数", null));
                                            //        return;
                                            //    }
                                            //}
                                        }
                                        else
                                        {
                                            return;
                                        }
                                        if (vSg.SignalProcessTypes.Contains(SignalProcessorType.Filter))//滤波
                                        {
                                            vSg.Filter();
                                        }
                                        if (vSg.SignalProcessTypes.Contains(SignalProcessorType.Envelope))//包络
                                        {
                                            vSg.Waveform = Algorithm.Instance.Envelope(vSg.Waveform, vSg.SamplePoint);
                                        }
                                        else if (vSg.SignalProcessTypes.Contains(SignalProcessorType.TFF))//TFF
                                        {
                                            vSg.Waveform = Algorithm.Instance.TFF(vSg.Waveform, vSg.SamplePoint, vSg.SampleFre);
                                        }
                                        else if (vSg.SignalProcessTypes.Contains(SignalProcessorType.Cepstrum))//倒频谱
                                        {
                                            vSg.Waveform = Algorithm.Instance.Cepstrum(vSg.Waveform, vSg.SamplePoint);
                                        }

                                        if (vSg.SignalProcessTypes.Contains(SignalProcessorType.Time))
                                        {
                                            var paras = Algorithm.CalculatePara(vSg.Waveform);
                                            if (paras != null)
                                            {
                                                vSg.RmsValue = paras[0];
                                                vSg.PeakValue = paras[1];
                                                vSg.PeakPeakValue = paras[2];
                                                vSg.Slope = paras[3];
                                                vSg.Kurtosis = paras[4];
                                                vSg.KurtosisValue = paras[5];
                                                vSg.WaveIndex = paras[6];
                                                vSg.PeakIndex = paras[7];
                                                vSg.ImpulsionIndex = paras[8];
                                                vSg.RootAmplitude = paras[9];
                                                vSg.ToleranceIndex = paras[10];
                                            }
                                        }
                                        if (vSg.SignalProcessTypes.Contains(SignalProcessorType.Frequency) || vSg.SignalProcessTypes.Contains(SignalProcessorType.PowerSpectrum) || vSg.SignalProcessTypes.Contains(SignalProcessorType.PowerSpectrumDensity))
                                        {
                                            double frequencyInterval = vSg.SampleFre / vSg.SamplePoint;
                                            int length = (int)(vSg.SamplePoint / 2.56) + 1;
                                            if (vSg.Frequency == null || vSg.Frequency.Length != length)
                                            {
                                                vSg.Frequency = new double[length];
                                            }
                                            for (int i = 0; i < length; i++)
                                            {
                                                vSg.Frequency[i] = frequencyInterval * i;
                                            }
                                        }
                                        if (vSg.SignalProcessTypes.Contains(SignalProcessorType.Frequency))
                                        {
                                            int length = (int)(vSg.SamplePoint / 2.56) + 1;
                                            var output = Algorithm.Instance.FFT2AndPhaseAction(vSg.Waveform, vSg.SamplePoint);
                                            if (output != null)
                                            {
                                                vSg.Amplitude = output[0].Take(length).ToArray();
                                                vSg.Phase = output[1].Take(length).ToArray();
                                            }
                                        }
                                        if (vSg.SignalProcessTypes.Contains(SignalProcessorType.PowerSpectrum))
                                        {
                                            int length = (int)(vSg.SamplePoint / 2.56) + 1;
                                            var output = Algorithm.Instance.PowerSpectrumAction(vSg.Waveform, vSg.SampleFre, vSg.SamplePoint, vSg.IsPowerSpectrumDB);
                                            vSg.PowerSpectrum = output.Take(length).ToArray();
                                        }
                                        if (vSg.SignalProcessTypes.Contains(SignalProcessorType.PowerSpectrumDensity))
                                        {
                                            int length = (int)(vSg.SamplePoint / 2.56) + 1;
                                            var output = Algorithm.Instance.PowerSpectrumDensityAction(vSg.Waveform, vSg.SampleFre, vSg.SamplePoint, vSg.IsPowerSpectrumDensityDB);
                                            vSg.PowerSpectrumDensity = output.Take(length).ToArray();
                                        }
                                    }
                                    catch(Exception ex)
                                    {
                                        EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据处理", ex));
                                    }
                                }
                            }
                        }
                    });
                });
            }
        }
        private void ProcessWave(BaseWaveSignal vSg)
        {
            //包络
            List<string> EnvelopeList = new List<string>();
            if (vSg.SignalProcessTypes.Contains(SignalProcessorType.EnvelopeVData))
            {
                EnvelopeList.Add("EnvelopeVData");
            }
            if (vSg.SignalProcessTypes.Contains(SignalProcessorType.EnvelopeTime))
            {
                EnvelopeList.Add("Time");
            }
            if (vSg.SignalProcessTypes.Contains(SignalProcessorType.EnvelopeFrequency))
            {
                EnvelopeList.Add("Frequency");
            }
            if (vSg.SignalProcessTypes.Contains(SignalProcessorType.EnvelopePowerSpectrum))
            {
                EnvelopeList.Add("PowerSpectrum");
            }
            if (vSg.SignalProcessTypes.Contains(SignalProcessorType.EnvelopePowerSpectrumDensity))
            {
                EnvelopeList.Add("PowerSpectrumDensity");
            }
            SubProcessWave(vSg, vSg.Waveform, "Envelope", EnvelopeList);

            //TFF
            List<string> TFFList = new List<string>();
            if (vSg.SignalProcessTypes.Contains(SignalProcessorType.TFFVData))
            {
                TFFList.Add("TFFVData");
            }
            if (vSg.SignalProcessTypes.Contains(SignalProcessorType.TFFTime))
            {
                TFFList.Add("Time");
            }
            if (vSg.SignalProcessTypes.Contains(SignalProcessorType.TFFFrequency))
            {
                TFFList.Add("Frequency");
            }
            if (vSg.SignalProcessTypes.Contains(SignalProcessorType.TFFPowerSpectrum))
            {
                TFFList.Add("PowerSpectrum");
            }
            if (vSg.SignalProcessTypes.Contains(SignalProcessorType.TFFPowerSpectrumDensity))
            {
                TFFList.Add("PowerSpectrumDensity");
            }
            SubProcessWave(vSg, vSg.Waveform, "TFF", TFFList);

            //TFF
            List<string> CepstrumList = new List<string>();
            if (vSg.SignalProcessTypes.Contains(SignalProcessorType.CepstrumVData))
            {
                CepstrumList.Add("TFFVData");
            }
            if (vSg.SignalProcessTypes.Contains(SignalProcessorType.CepstrumTime))
            {
                CepstrumList.Add("Time");
            }
            if (vSg.SignalProcessTypes.Contains(SignalProcessorType.CepstrumFrequency))
            {
                CepstrumList.Add("Frequency");
            }
            if (vSg.SignalProcessTypes.Contains(SignalProcessorType.CepstrumPowerSpectrum))
            {
                CepstrumList.Add("PowerSpectrum");
            }
            if (vSg.SignalProcessTypes.Contains(SignalProcessorType.CepstrumPowerSpectrumDensity))
            {
                CepstrumList.Add("PowerSpectrumDensity");
            }
            SubProcessWave(vSg, vSg.Waveform, "Cepstrum", CepstrumList);
        }
        private void SubProcessWave(BaseWaveSignal vSg, double[] waveform, string processName, List<string> processList)
        {
            if (processList.Contains("EnvelopeVData"))//包络
            {
                if (vSg.WaveformList == null)
                {
                    vSg.WaveformList = new Dictionary<string, double[]>();
                }
                if (!vSg.WaveformList.Keys.Contains(processName))
                {
                    vSg.WaveformList.Add(processName, null);
                }

                vSg.WaveformList[processName] = Algorithm.Instance.Envelope(waveform, vSg.SamplePoint);
            }
            else if (processList.Contains("TFFVData"))//TFF
            {
                if (vSg.WaveformList == null)
                {
                    vSg.WaveformList = new Dictionary<string, double[]>();
                }
                if (!vSg.WaveformList.Keys.Contains(processName))
                {
                    vSg.WaveformList.Add(processName, null);
                }

                vSg.Waveform = Algorithm.Instance.TFF(waveform, vSg.SamplePoint, vSg.SampleFre);
            }
            else if (processList.Contains("CepstrumVData"))//倒频谱
            {
                if (vSg.WaveformList == null)
                {
                    vSg.WaveformList = new Dictionary<string, double[]>();
                }
                if (!vSg.WaveformList.Keys.Contains(processName))
                {
                    vSg.WaveformList.Add(processName, null);
                }

                vSg.Waveform = Algorithm.Instance.Cepstrum(waveform, vSg.SamplePoint);
            }
            else
            {
                return;
            }

            if (processList.Contains("Time"))
            {
                var paras = Algorithm.CalculatePara(vSg.WaveformList[processName]);
                if (paras != null)
                {
                    vSg.RmsValue = paras[0];
                    vSg.PeakValue = paras[1];
                    vSg.PeakPeakValue = paras[2];
                    vSg.Slope = paras[3];
                    vSg.Kurtosis = paras[4];
                    vSg.KurtosisValue = paras[5];
                    vSg.WaveIndex = paras[6];
                    vSg.PeakIndex = paras[7];
                    vSg.ImpulsionIndex = paras[8];
                    vSg.RootAmplitude = paras[9];
                    vSg.ToleranceIndex = paras[10];
                }
            }
            if (processList.Contains("Frequency") || processList.Contains("PowerSpectrum") || processList.Contains("PowerSpectrumDensity"))
            {
                double frequencyInterval = vSg.SampleFre / vSg.SamplePoint;
                int length = (int)(vSg.SamplePoint / 2.56) + 1;
                if (vSg.FrequencyList == null)
                {
                    vSg.FrequencyList = new Dictionary<string, double[]>();
                }
                if (!vSg.FrequencyList.Keys.Contains(processName))
                {
                    vSg.FrequencyList.Add(processName, null);
                }

                if (vSg.FrequencyList[processName] == null || vSg.FrequencyList[processName].Length != length)
                {
                    vSg.FrequencyList[processName] = new double[length];
                }
                for (int i = 0; i < length; i++)
                {
                    vSg.FrequencyList[processName][i] = frequencyInterval * i;
                }
            }
            if (processList.Contains("Frequency"))
            {
                int length = (int)(vSg.SamplePoint / 2.56) + 1;
                var output = Algorithm.Instance.FFT2AndPhaseAction(vSg.WaveformList[processName], vSg.SamplePoint);
                if (output != null)
                {
                    if (vSg.AmplitudeList == null)
                    {
                        vSg.AmplitudeList = new Dictionary<string, double[]>();
                    }
                    if (!vSg.AmplitudeList.Keys.Contains(processName))
                    {
                        vSg.AmplitudeList.Add(processName, null);
                    }
                    vSg.AmplitudeList[processName] = output[0].Take(length).ToArray();
                    if (vSg.PhaseList == null)
                    {
                        vSg.PhaseList = new Dictionary<string, double[]>();
                    }
                    if (!vSg.PhaseList.Keys.Contains(processName))
                    {
                        vSg.PhaseList.Add(processName, null);
                    }
                    vSg.PhaseList[processName] = output[1].Take(length).ToArray();
                }
            }
            if (processList.Contains("PowerSpectrum"))
            {
                int length = (int)(vSg.SamplePoint / 2.56) + 1;
                var output = Algorithm.Instance.PowerSpectrumAction(vSg.WaveformList[processName], vSg.SampleFre, vSg.SamplePoint, vSg.IsPowerSpectrumDB);
                if (vSg.PowerSpectrumList == null)
                {
                    vSg.PowerSpectrumList = new Dictionary<string, double[]>();
                }
                if (!vSg.PowerSpectrumList.Keys.Contains(processName))
                {
                    vSg.PowerSpectrumList.Add(processName, null);
                }
                vSg.PowerSpectrumList[processName] = output.Take(length).ToArray();
            }
            if (processList.Contains("PowerSpectrumDensity"))
            {
                int length = (int)(vSg.SamplePoint / 2.56) + 1;
                var output = Algorithm.Instance.PowerSpectrumDensityAction(vSg.WaveformList[processName], vSg.SampleFre, vSg.SamplePoint, vSg.IsPowerSpectrumDensityDB);
                if (vSg.PowerSpectrumDensityList  == null)
                {
                    vSg.PowerSpectrumDensityList = new Dictionary<string, double[]>();
                }
                if (!vSg.PowerSpectrumDensityList.Keys.Contains(processName))
                {
                    vSg.PowerSpectrumDensityList.Add(processName, null);
                }
                vSg.PowerSpectrumDensityList[processName] = output.Take(length).ToArray();
            }
        }
        private void SetDivfreSignal(BaseDivfreSignal sg, IBaseAlarmSlot idata)
        {
            sg.IBaseAlarmSlot = idata;

            sg.IsResetFlag = true;

            if (idata is D1_WirelessVibrationSlot)
            {
                var data = idata as D1_WirelessVibrationSlot;
                sg.BatteryEnergy = (float)data.BatteryEnergy.Value;

                setwave(sg, data);

                var divfres = data.DivFreInfo;

                //分频信号
                foreach (var item in sg.DivFres)
                {
                    var divFreContract = divfres.Where(p => p.Guid == item.Guid).SingleOrDefault();
                    if (divFreContract != null)
                    {
                        item.RecordLab = divFreContract.RecordLab.Value;
                        item.ACQDatetime = divFreContract.ACQDatetime;
                        item.DescriptionFre = divFreContract.DescriptionFre;
                        item.AlarmType = divFreContract.AlarmGrade;
                        item.DivFreType = (DivFreType)Enum.Parse(typeof(DivFreType), divFreContract.DivFreCode.ToString());
                        item.Name = divFreContract.Name;

                        if (divFreContract.AlarmLimit != null)
                        {
                            item.LowDanger = divFreContract.AlarmLimit.Where(p => p.Name == "低危").Select(p => p.Limit).FirstOrDefault();
                            item.LowAlert = divFreContract.AlarmLimit.Where(p => p.Name == "低警").Select(p => p.Limit).FirstOrDefault();
                            item.LowNormal = divFreContract.AlarmLimit.Where(p => p.Name == "低正常").Select(p => p.Limit).FirstOrDefault();
                            item.HighNormal = divFreContract.AlarmLimit.Where(p => p.Name == "高正常").Select(p => p.Limit).FirstOrDefault();
                            item.HighAlert = divFreContract.AlarmLimit.Where(p => p.Name == "高警").Select(p => p.Limit).FirstOrDefault();
                            item.HighDanger = divFreContract.AlarmLimit.Where(p => p.Name == "高危").Select(p => p.Limit).FirstOrDefault();
                            item.AlarmGrade = (AlarmGrade)Enum.Parse(typeof(AlarmGrade), divFreContract.AlarmGrade.ToString());
                            item.Phase = divFreContract.Phase.Value;
                            item.Result = divFreContract.Result.Value;
                        }
                        item.IsConnected = true;
                        item.IsRead = true;
                    }
                    else
                    {
                        item.IsConnected = false;
                        item.IsRead = false;
                    }
                }
            }

            //if (idata.AlarmLimit != null)
            //{
            //    sg.LowDanger = idata.AlarmLimit.Where(p => p.Name == "低危").Select(p => p.Limit).FirstOrDefault();
            //    sg.LowAlert = idata.AlarmLimit.Where(p => p.Name == "低警").Select(p => p.Limit).FirstOrDefault();
            //    sg.LowNormal = idata.AlarmLimit.Where(p => p.Name == "低正常").Select(p => p.Limit).FirstOrDefault();
            //    sg.HighNormal = idata.AlarmLimit.Where(p => p.Name == "高正常").Select(p => p.Limit).FirstOrDefault();
            //    sg.HighAlert = idata.AlarmLimit.Where(p => p.Name == "高警").Select(p => p.Limit).FirstOrDefault();
            //    sg.HighDanger = idata.AlarmLimit.Where(p => p.Name == "高危").Select(p => p.Limit).FirstOrDefault();
            //}           

            setalarm(sg, idata);            

            sg.IsConnected = true;
            sg.IsRunning = (sg.DelayAlarmGrade == AlarmGrade.Abnormal) ? false : true;
        }
        private void SetAlarmSignal(BaseAlarmSignal sg, IBaseAlarmSlot idata)
        {
            sg.IBaseAlarmSlot = idata;          
         
            sg.IsResetFlag = true;

            if (idata is D1_WirelessScalarSlot)
            {
                sg.BatteryEnergy = (float)(idata as D1_WirelessScalarSlot).BatteryEnergy.Value;                
            }

            //if (idata.AlarmLimit != null)
            //{
            //    sg.LowDanger = idata.AlarmLimit.Where(p => p.Name == "低危").Select(p => p.Limit).FirstOrDefault();
            //    sg.LowAlert = idata.AlarmLimit.Where(p => p.Name == "低警").Select(p => p.Limit).FirstOrDefault();
            //    sg.LowNormal = idata.AlarmLimit.Where(p => p.Name == "低正常").Select(p => p.Limit).FirstOrDefault();
            //    sg.HighNormal = idata.AlarmLimit.Where(p => p.Name == "高正常").Select(p => p.Limit).FirstOrDefault();
            //    sg.HighAlert = idata.AlarmLimit.Where(p => p.Name == "高警").Select(p => p.Limit).FirstOrDefault();
            //    sg.HighDanger = idata.AlarmLimit.Where(p => p.Name == "高危").Select(p => p.Limit).FirstOrDefault();
            //}    
            
            setalarm(sg, idata);
            sg.IsConnected = true;
            sg.IsRunning = false;           
        }
        private void setwave(BaseDivfreSignal sg, D1_WirelessVibrationSlot idata)
        {
            sg.MountDegree = idata.MountDegree.Value;
            sg.TPDirCode = (int)idata.TPDirCode.Value;
            sg.IsValidWave = idata.IsValidWave.Value;
            sg.WaveUnit = idata.WaveUnit;
            sg.RPM = (float)idata.RPM.Value;
            sg.IsMultiplication = idata.IsMultiplication.Value;
            sg.MultiplicationCor = (float)idata.MultiplicationCor.Value;
            sg.BiasVoltHigh = (float)idata.BiasVoltHigh.Value;
            sg.BiasVoltLow = (float)idata.BiasVoltLow.Value;          
        }
        private void setalarm(BaseAlarmSignal sg, IBaseAlarmSlot idata)
        {
            if (sg.ACQDatetime != null && sg.Result != null && sg.Result != idata.Result)//缓存数据
            {
                TrendPointData tp = new TrendPointData(sg.ACQDatetime.Value, sg.Result.Value, sg.Unit, (int)sg.AlarmGrade);
                if (sg is IBaseWaveSlot)
                {
                    tp.RecordLab = sg.RecordLab.Value;
                    tp.IsValidWave = (sg as IBaseWaveSlot).IsValidWave.Value;
                }
                sg.BufferData.Add(tp);
                if (sg.ACQDatetime >= sg.BufferData[0].ACQDateTime.AddSeconds(10))//弹出一个数据
                {
                    sg.BufferData.RemoveAt(0);
                }
            }

            sg.ACQDatetime = idata.ACQDatetime;
            sg.ACQ_Unit_Type = idata.ACQ_Unit_Type;
            sg.AsySyn = idata.AsySyn;
            sg.MainCardCode = idata.MainCardCode;
            sg.SynWaveCode = idata.SynWaveCode;
            sg.IsHdBypass = idata.IsHdBypass;
            sg.IsHdMultiplication = idata.IsHdMultiplication;
            sg.RecordLab = idata.RecordLab;
            sg.SaveLab = idata.SaveLab;
            sg.ContinueLab = idata.ContinueLab;
            sg.ExtraInfo = idata.ExtraInfo;
            sg.Result = idata.Result;
            sg.IsValidCH = idata.IsValidCH;
            sg.Unit = idata.Unit;
            //sg.ChannelHDID = idata.ChannelHDID;//数据不完整，抛弃

            sg.AlarmGrade = (AlarmGrade)(idata.AlarmGrade & 0x00ffff00);
            sg.Low8Alarm = idata.AlarmGrade & 0xff;
            sg.AlarmLimit = idata.AlarmLimit;            
        }
    }
}

﻿using AIC.Core.Models;
using AIC.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIC.Core.ControlModels;
using AIC.Core.OrganizationModels;
using AIC.ServiceInterface;
using AIC.Core;
using System.Windows.Input;
using AIC.Resources.Models;
using AIC.M9600.Common.SlaveDB.Generated;
using System.Windows;
using AIC.Core.DataModels;
using System.Reflection;
using AIC.CoreType;
using System.ComponentModel;
using System.Windows.Data;
using AIC.HistoryDataPage.Models;
using AIC.Core.HardwareModels;

namespace AIC.HistoryDataPage.ViewModels
{
    class HistoryDataListViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;       
        private readonly IOrganizationService _organizationService;
        private readonly IDatabaseComponent _databaseComponent;
        private readonly ICardProcess _cardProcess;
        private readonly IHardwareService _hardwareService;
        public HistoryDataListViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, IDatabaseComponent databaseComponent, ICardProcess cardProcess, IHardwareService hardwareService)
        {           
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _databaseComponent = databaseComponent;
            _cardProcess = cardProcess;
            _hardwareService = hardwareService;

            _vInfoObjectsView = new ListCollectionView(vInfoCollection);
            _vInfoObjectsView.Filter = (object item) =>
            {
                return true;
            };
            _vInfoObjectsView.GroupDescriptions.Add(new PropertyGroupDescription("OrganizationName"));//对视图进行分组
            //_vInfoObjectsView.GroupDescriptions.Add(new PropertyGroupDescription("DeviceName"));//对视图进行分组
            //_vInfoObjectsView.GroupDescriptions.Add(new PropertyGroupDescription("ItemName"));//对视图进行分组

            _anInfoObjectsView = new ListCollectionView(anInfoCollection);
            _anInfoObjectsView.Filter = (object item) =>
            {
                return true;
            };
            _anInfoObjectsView.GroupDescriptions.Add(new PropertyGroupDescription("OrganizationName"));//对视图进行分组
            //_anInfoObjectsView.GroupDescriptions.Add(new PropertyGroupDescription("DeviceName"));//对视图进行分组
            //_anInfoObjectsView.GroupDescriptions.Add(new PropertyGroupDescription("ItemName"));//对视图进行分组

            _divFreObjectsView = new ListCollectionView(divFreCollection);
            _divFreObjectsView.Filter = (object item) =>
            {
                return true;
            };
            _divFreObjectsView.GroupDescriptions.Add(new PropertyGroupDescription("OrganizationName"));//对视图进行分组

            InitTree();
        }

        #region 管理树
        private void InitTree()
        { 
            OrganizationTreeItems = _organizationService.OrganizationTreeItems;
            RecycledTreeItems = _organizationService.RecycledTreeItems;
            SelectedTreeItem = _cardProcess.GetSelectedTree(OrganizationTreeItems);//可能无效了
            TreeExpanded();
        }
        private void TreeExpanded()
        {
            foreach (var first in OrganizationTreeItems)
            {
                first.IsExpanded = true;
                foreach (var second in first.Children)
                {
                    second.IsExpanded = true;
                    foreach (var third in second.Children)
                    {
                        third.IsExpanded = true;
                    }
                }
            }
        }
        #endregion

        #region 属性与字段
        private ObservableCollection<OrganizationTreeItemViewModel> _organizationTreeItems;
        public ObservableCollection<OrganizationTreeItemViewModel> OrganizationTreeItems
        {
            get { return _organizationTreeItems; }
            set
            {
                _organizationTreeItems = value;
                OnPropertyChanged("OrganizationTreeItems");
            }
        }

        private ObservableCollection<OrganizationTreeItemViewModel> _recycledTreeItems;
        public ObservableCollection<OrganizationTreeItemViewModel> RecycledTreeItems
        {
            get { return _recycledTreeItems; }
            set
            {
                _recycledTreeItems = value;
                OnPropertyChanged("RecycledTreeItems");
            }
        }

        private readonly ICollectionView _vInfoObjectsView;
        public ICollectionView VInfoObjectsView { get { return _vInfoObjectsView; } }

        private readonly ICollectionView _anInfoObjectsView;
        public ICollectionView AnInfoObjectsView { get { return _anInfoObjectsView; } }

        private readonly ICollectionView _divFreObjectsView;
        public ICollectionView DivFreObjectsView { get { return _divFreObjectsView; } }

        private FastObservableCollection<RMSObject> vInfoCollection = new FastObservableCollection<RMSObject>();
        public IEnumerable<RMSObject> VInfoObjects { get { return vInfoCollection; } }

        private FastObservableCollection<RMSObject> anInfoCollection = new FastObservableCollection<RMSObject>();
        public IEnumerable<RMSObject> AnInfoObjects { get { return anInfoCollection; } }

        private FastObservableCollection<DivFreObject> divFreCollection = new FastObservableCollection<DivFreObject>();
        public IEnumerable<DivFreObject> DivFreObjects { get { return divFreCollection; } }     

        private int channelRecordLength = 50000;
        public int ChannelRecordLength
        {
            get { return channelRecordLength; }
            set
            {
                if (value != channelRecordLength)
                {
                    channelRecordLength = value;
                    OnPropertyChanged("ChannelRecordLength");
                }
            }
        }

        private bool allowNormal = true;
        public bool AllowNormal
        {
            get { return allowNormal; }
            set
            {
                if (allowNormal != value)
                {
                    allowNormal = value;
                    OnPropertyChanged(() => AllowNormal);
                }
            }
        }

        private bool allowPreWarning = true;
        public bool AllowPreWarning
        {
            get { return allowPreWarning; }
            set
            {
                if (allowPreWarning != value)
                {
                    allowPreWarning = value;
                    OnPropertyChanged(() => AllowPreWarning);
                }
            }
        }

        private bool allowWarning = true;
        public bool AllowWarning
        {
            get { return allowWarning; }
            set
            {
                if (allowWarning != value)
                {
                    allowWarning = value;
                    OnPropertyChanged(() => AllowWarning);
                }
            }
        }

        private bool allowDanger = true;
        public bool AllowDanger
        {
            get { return allowDanger; }
            set
            {
                if (allowDanger != value)
                {
                    allowDanger = value;
                    OnPropertyChanged(() => AllowDanger);
                }
            }
        }

        private bool allowInvalid = true;
        public bool AllowInvalid
        {
            get { return allowInvalid; }
            set
            {
                if (allowInvalid != value)
                {
                    allowInvalid = value;
                    OnPropertyChanged(() => AllowInvalid);
                }
            }
        }

        private double? peakValue;
        public double? PeakValue
        {
            get { return peakValue; }
            set
            {
                if (peakValue != value)
                {
                    peakValue = value;
                    this.OnPropertyChanged(() => PeakValue);
                }
            }
        }

        private double? peakPeakValue;
        public double? PeakPeakValue
        {
            get { return peakPeakValue; }
            set
            {
                if (peakPeakValue != value)
                {
                    peakPeakValue = value;
                    this.OnPropertyChanged(() => PeakPeakValue);
                }
            }
        }

        private DateTime? startTime = DateTime.Now.Subtract(TimeSpan.FromHours(1));
        public DateTime? StartTime
        {
            get { return startTime; }
            set
            {
                if (value != startTime)
                {
                    startTime = value;                   
                    this.OnPropertyChanged("StartTime");
                }
            }
        }

        private DateTime? endTime = DateTime.Now;
        public DateTime? EndTime
        {
            get { return endTime; }
            set
            {
                if (value != endTime)
                {
                    endTime = value;                  
                    this.OnPropertyChanged("EndTime");
                }
            }
        }     

        private AIC.CoreType.Unit unitFilter = AIC.CoreType.Unit.Velocity;
        public AIC.CoreType.Unit UnitFilter
        {
            get { return unitFilter; }
            set
            {
                if (unitFilter != value)
                {
                    unitFilter = value;
                    OnPropertyChanged(() => this.UnitFilter);
                }
            }
        }

        private AIC.CoreType.TriggerType triggerFilter = AIC.CoreType.TriggerType.Auto;
        public AIC.CoreType.TriggerType TriggerFilter
        {
            get { return triggerFilter; }
            set
            {
                if (triggerFilter != value)
                {
                    triggerFilter = value;
                    OnPropertyChanged(() => this.TriggerFilter);
                }
            }
        }

        private ViewModelStatus _status = ViewModelStatus.None;
        public ViewModelStatus Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public string waitinfo;
        public string WaitInfo
        {
            get
            {
                return waitinfo;
            }
            set
            {
                waitinfo = value;
                OnPropertyChanged("WaitInfo");
            }
        }

        private bool isComposing;
        public bool IsComposing
        {
            get { return isComposing; }
            set
            {
                if (isComposing != value)
                {
                    isComposing = value;
                    OnPropertyChanged("IsComposing");
                }
            }
        }       

        private bool allowRPMFilter;
        public bool AllowRPMFilter
        {
            get { return allowRPMFilter; }
            set
            {
                if (allowRPMFilter != value)
                {
                    allowRPMFilter = value;
                    OnPropertyChanged("AllowRPMFilter");
                }
            }
        }

        private double upRPMFilter;
        public double UpRPMFilter
        {
            get { return upRPMFilter; }
            set
            {
                if (upRPMFilter != value)
                {
                    upRPMFilter = value;
                    OnPropertyChanged("UpRPMFilter");
                }
            }
        }

        private double downRPMFilter;
        public double DownRPMFilter
        {
            get { return downRPMFilter; }
            set
            {
                if (downRPMFilter != value)
                {
                    downRPMFilter = value;
                    OnPropertyChanged("DownRPMFilter");
                }
            }
        }

        public List<string> UnitCategory
        {
            get
            {
                return _databaseComponent.UnitCategory;
            }
        }

        private string unit;
        public string Unit
        {
            get { return unit; }
            set
            {
                if (unit != value)
                {
                    unit = value;
                    OnPropertyChanged("Unit");
                }
            }
        }

        private bool vInfoSelected;
        public bool VInfoSelected
        {
            get { return vInfoSelected; }
            set
            {
                if (vInfoSelected != value)
                {
                    vInfoSelected = value;
                    OnPropertyChanged("VInfoSelected");
                }
            }
        }

        private bool anInfoSelected;
        public bool AnInfoSelected
        {
            get { return anInfoSelected; }
            set
            {
                if (anInfoSelected != value)
                {
                    anInfoSelected = value;
                    OnPropertyChanged("AnInfoSelected");
                }
            }
        }

        private OrganizationTreeItemViewModel selectedTreeItem;
        public OrganizationTreeItemViewModel SelectedTreeItem
        {
            get { return selectedTreeItem; }
            set
            {
                if (selectedTreeItem != value)
                {
                    selectedTreeItem = value;
                    OnPropertyChanged("SelectedTreeItem");
                }
            }
        }
        #endregion

        #region 命令
        private ICommand selectedTreeChangedComamnd;
        public ICommand SelectedTreeChangedComamnd
        {
            get
            {
                return this.selectedTreeChangedComamnd ?? (this.selectedTreeChangedComamnd = new DelegateCommand<object>(para => this.SelectedTreeChanged(para)));
            }
        }
        private ICommand addDataCommand;
        public ICommand AddDataCommand
        {
            get
            {
                return this.addDataCommand ?? (this.addDataCommand = new DelegateCommand<object>(para => this.AddData(para)));
            }
        }
        private ICommand doubleClickAddDataCommand;
        public ICommand DoubleClickAddDataCommand
        {
            get
            {
                return this.doubleClickAddDataCommand ?? (this.doubleClickAddDataCommand = new DelegateCommand<object>(para => this.DoubleClickAddData(para)));
            }
        }
        private ICommand refreshDataCommand;
        public ICommand RefreshDataCommand
        {
            get
            {
                return this.refreshDataCommand ?? (this.refreshDataCommand = new DelegateCommand<object>(para => this.RefreshData(para)));
            }
        }
        private ICommand clearDataCommand;
        public ICommand ClearDataCommand
        {
            get
            {
                return this.clearDataCommand ?? (this.clearDataCommand = new DelegateCommand<object>(para => this.ClearData(para)));
            }
        }
        #endregion

        private void SelectedTreeChanged(object para)
        {
            SelectedTreeItem = para as OrganizationTreeItemViewModel;
            if (SelectedTreeItem != null)
            {
                var itemTree = SelectedTreeItem as ItemTreeItemViewModel;
                if (itemTree != null && itemTree.BaseAlarmSignal != null)
                {
                    if (itemTree.BaseAlarmSignal.Unit != null)
                    {
                        Unit = itemTree.BaseAlarmSignal.Unit;//自动获取单位
                    }
                }
            }            
        }

        private void DoubleClickAddData(object para)
        {
            SelectedTreeItem = para as OrganizationTreeItemViewModel;
            if (SelectedTreeItem is ItemTreeItemViewModel)
            {
                AddData(para);
            }
        }

        private async void AddData(object para)
        {
            if (SelectedTreeItem == null)
            {
#if XBAP
                MessageBox.Show("请选中要查询的组织机构", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请选中要查询的组织机构", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }

            if (Unit == null || Unit == "")
            {
#if XBAP
                MessageBox.Show("请选择要查询的测点的数据类型", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请选择要查询的测点的数据类型", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }

            if (SelectedTreeItem is ItemTreeItemViewModel)
            {
                if ((SelectedTreeItem as ItemTreeItemViewModel).T_Item != null && (SelectedTreeItem as ItemTreeItemViewModel).T_Item.ItemType == (int)ChannelType.WirelessScalarChannelInfo)
                {                   
                    AnInfoSelected = true;
                }
                if ((SelectedTreeItem as ItemTreeItemViewModel).T_Item != null && (SelectedTreeItem as ItemTreeItemViewModel).T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
                {
                    VInfoSelected = true;
                }
            }

            string conditionWave;
            string conditionAlarm;
            object[] objectWave;
            object[] objectAlarm;
            ConditionClass.GetConditionStr(out conditionWave, out conditionAlarm, out objectWave, out objectAlarm, AllowNormal, AllowPreWarning, AllowWarning, AllowDanger, AllowInvalid, AllowRPMFilter, Unit, DownRPMFilter, UpRPMFilter);

            string selectedip = _cardProcess.GetOrganizationServer(SelectedTreeItem);

            #region 分频
            var divfre = SelectedTreeItem as DivFreTreeItemViewModel;
            if (divfre != null)
            {
                try
                {
                    WaitInfo = "获取数据中";
                    Status = ViewModelStatus.Querying;

                    var item_parent = divfre.Parent as ItemTreeItemViewModel;
                    var divfreinfo = divfre.T_DivFreInfo;
                    if (divfreinfo == null)
                    {
                        return;
                    }

                    //var channel = _cardProcess.GetChannel(_hardwareService.ServerTreeItems, item_parent.T_Item);
                    //if (channel == null || channel.IChannel == null)
                    //{
                    //    return;
                    //}

                    var result = await _databaseComponent.GetHistoryData<D1_DivFreInfo>(selectedip, divfreinfo.Guid, null, StartTime.Value, EndTime.Value, null, null);
                    if (result.Count == 0)
                    {
#if XBAP
                        MessageBox.Show("没有数据，请重新选择条件", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                        Xceed.Wpf.Toolkit.MessageBox.Show("没有数据，请重新选择条件", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                        return;
                    }

                    List<IBaseDivfreSlot> slotdata = null;

                    switch (item_parent.T_Item.ItemType)
                    {
                        case (int)ChannelType.IEPEChannelInfo:
                            {
                                var resultslot = await _databaseComponent.GetHistoryData<D_IEPESlot>(selectedip, item_parent.T_Item.Guid, null, StartTime.Value, EndTime.Value, null, null);
                                if (resultslot.Count == 0)
                                {
                                    return;
                                }
                                slotdata = resultslot.Select(p => p as IBaseDivfreSlot).ToList();
                                break;
                            }
                        case (int)ChannelType.EddyCurrentDisplacementChannelInfo:
                            {
                                var resultslot = await _databaseComponent.GetHistoryData<D_EddyCurrentDisplacementSlot>(selectedip, item_parent.T_Item.Guid, null, StartTime.Value, EndTime.Value, null, null);
                                if (resultslot.Count == 0)
                                {
                                    return;
                                }
                                slotdata = resultslot.Select(p => p as IBaseDivfreSlot).ToList();
                                break;
                            }
                        case (int)ChannelType.WirelessVibrationChannelInfo:
                            {
                                string unit = Unit;
                                var resultslot = await _databaseComponent.GetHistoryData<D_WirelessVibrationSlot>(selectedip, item_parent.T_Item.Guid, null, StartTime.Value, EndTime.Value, conditionWave, new object[] { unit, DownRPMFilter, UpRPMFilter });
                                if (resultslot.Count == 0)
                                {
                                    return;
                                }
                                slotdata = resultslot.Select(p => p as IBaseDivfreSlot).ToList();
                                break;
                            }
                        default: return;
                    }


                    if (slotdata == null)
                    {
                        return;
                    }

                    for (int i = 0; i < result.Count; i++)
                    {
                        DivFreObject divFreObj = new DivFreObject();
                        divFreObj.OrganizationName = item_parent.BaseAlarmSignal.OrganizationName;
                        divFreObj.DeviceName = item_parent.BaseAlarmSignal.DeviceName;
                        divFreObj.ItemName = item_parent.BaseAlarmSignal.ItemName;
                        divFreObj.DivFreName = divfre.Name;
                        var data = (from p in slotdata where p.RecordLab == result[i].RecordLab select new { p.ACQDatetime, p.Unit }).SingleOrDefault();
                        if (data == null)
                        {
                            return;
                        }

                        divFreObj.ACQDatetime = data.ACQDatetime;
                        divFreObj.DescriptionFre = result[i].DescriptionFre;
                        divFreObj.Result = result[i].Result.Value;
                        //divFreObj.Phase = result[i].Phase;
                        divFreObj.Unit = data.Unit;

                        if (divFreCollection.Where(p => p.OrganizationName == divFreObj.OrganizationName
                                && p.DeviceName == divFreObj.DeviceName
                                && p.ItemName == divFreObj.ItemName
                                && p.DivFreName == divFreObj.DivFreName
                                && p.ACQDatetime == divFreObj.ACQDatetime).Count() == 0) //去重     
                        {
                            divFreCollection.Add(divFreObj);
                        }
                    }

                }
                catch (Exception ex)
                {
                    _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-分频查询", ex));
                }
                finally
                {
                    Status = ViewModelStatus.None;
                }
                return;
            }
            #endregion

            #region 测点
            var item = SelectedTreeItem as ItemTreeItemViewModel;
            if (item != null)
            {
                if (item.T_Item != null && item.T_Item.ItemType != 0)
                {
                    try
                    {
                        WaitInfo = "获取数据中";
                        Status = ViewModelStatus.Querying;
                        await SubAddData(item, conditionWave, conditionAlarm, objectWave, objectAlarm);
                    }
                    catch (Exception ex)
                    {
                        _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-测点查询", ex));
                    }
                    finally
                    {
                        Status = ViewModelStatus.None;
                    }

                    return;
                }
                else
                {
#if XBAP
                    MessageBox.Show("该测点没绑定或无信息", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                    Xceed.Wpf.Toolkit.MessageBox.Show("该测点无信息", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                    return;
                }
            }

            #endregion

            #region 组织机构           
            if (SelectedTreeItem != null)
            {
                try
                {
                    WaitInfo = "获取数据中";
                    Status = ViewModelStatus.Querying;
                    var items = _cardProcess.GetItems(SelectedTreeItem).Where(p => p.IsPaired);

                    foreach (var subitem in items)
                    {
                        await SubAddData(subitem, conditionWave, conditionAlarm,  objectWave, objectAlarm, false);
                    }
                }
                catch (Exception ex)
                {
                    _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-测点批量查询", ex));
                }
                finally
                {
                    Status = ViewModelStatus.None;
                }
                return;
            }
            #endregion
        }

        private async Task SubAddData(ItemTreeItemViewModel item, string conditionWave, string conditionAlarm, object[] objectWave, object[] objectAlarm, bool showmessagbox = true)
        {
            List<IBaseAlarmSlot> result = new List<IBaseAlarmSlot>();

            if (item.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
            {
                result = await _databaseComponent.GetUniformHistoryData(item.T_Item.ItemType, item.ServerIP, item.T_Item.Guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade" }, StartTime.Value, EndTime.Value, conditionWave, objectWave);
            }
            else if (item.T_Item.ItemType == (int)ChannelType.WirelessScalarChannelInfo)
            {
                result = await _databaseComponent.GetUniformHistoryData(item.T_Item.ItemType, item.ServerIP, item.T_Item.Guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade" }, StartTime.Value, EndTime.Value, conditionAlarm, objectAlarm);
            }

            if (result == null || result.Count == 0)
            {
                if (showmessagbox == true)
                {
#if XBAP
                    MessageBox.Show("没有数据，请重新选择条件", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                    Xceed.Wpf.Toolkit.MessageBox.Show("没有数据，请重新选择条件", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                }
                return;
            }
            for (int i = 0; i < result.Count; i++)
            {
                RMSObject amsObj = new RMSObject();
                if (item.BaseAlarmSignal != null)
                {
                    amsObj.OrganizationName = item.BaseAlarmSignal.OrganizationName;
                    amsObj.DeviceName = item.BaseAlarmSignal.DeviceName;
                    amsObj.ItemName = item.BaseAlarmSignal.ItemName;
                }
                else if (item.Parent is OrganizationTreeItemViewModel)//回收站
                {
                    amsObj.OrganizationName = "回收站";
                    amsObj.DeviceName = item.ServerIP;
                    amsObj.ItemName = item.Name;
                }

                amsObj.ACQDatetime = result[i].ACQDatetime;
                amsObj.Result = result[i].Result.Value;
                amsObj.Unit = result[i].Unit;
                amsObj.AlarmGrade = (AlarmGrade)(result[i].AlarmGrade & 0x00ffff00);

                if (item.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
                {
                    if (vInfoCollection.Where(p => p.OrganizationName == amsObj.OrganizationName
                         && p.DeviceName == amsObj.DeviceName
                         && p.ItemName == amsObj.ItemName
                         && p.ACQDatetime == amsObj.ACQDatetime).Count() == 0) //去重     
                    {
                        vInfoCollection.Add(amsObj);
                    }
                }
                else if (item.T_Item.ItemType == (int)ChannelType.WirelessScalarChannelInfo)
                {
                    if (anInfoCollection.Where(p => p.OrganizationName == amsObj.OrganizationName
                           && p.DeviceName == amsObj.DeviceName
                           && p.ItemName == amsObj.ItemName
                           && p.ACQDatetime == amsObj.ACQDatetime).Count() == 0) //去重     
                    {
                        anInfoCollection.Add(amsObj);
                    }
                }
            }
        }
        private void RefreshData(object para)
        {
            vInfoCollection.Clear();
            anInfoCollection.Clear();
            divFreCollection.Clear();
            AddData(para);
        }
        private void ClearData(object para)
        {
            vInfoCollection.Clear();
            anInfoCollection.Clear();
            divFreCollection.Clear();
        }
    }

    public class RMSObject
    {
        public string OrganizationName { get; set; }
        public string DeviceName { get; set; }
        public string ItemName { get; set; }
        public DateTime ACQDatetime { get; set; }
        public double Result { get; set; }
        public string Unit { get; set; }
        public double RPM { get; set; }
        public AlarmGrade AlarmGrade { get; set;}
        
    }

    public class DivFreObject
    {
        public string OrganizationName { get; set; }
        public string DeviceName { get; set; }
        public string ItemName { get; set; }
        public string DivFreName { get; set; }
        public DateTime ACQDatetime { get; set; }
        public string DescriptionFre { get; set; }     
        public double Result { get; set; }
        public double Phase { get; set; }
        public string Unit { get; set; }
       
    }
}

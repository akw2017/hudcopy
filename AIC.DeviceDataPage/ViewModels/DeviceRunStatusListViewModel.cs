using AIC.Core.Events;
using AIC.Core.ExCommand;
using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.Core.Servers;
using AIC.Core.UserManageModels;
using AIC.Resources.Models;
using AIC.ServiceInterface;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using System.Xml.Linq;
using AIC.Core.LMModels;
using AIC.CoreType;
using AIC.Core.SignalModels;
using AIC.Core.OrganizationModels;
using System.Windows.Input;
using AIC.DeviceDataPage.Models;
using AIC.M9600.Common.SlaveDB.Generated;
using Newtonsoft.Json;
using AIC.M9600.Common.DTO.Device;
using System.Diagnostics;

namespace AIC.DeviceDataPage.ViewModels
{
    class DeviceRunStatusListViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        private readonly IDatabaseComponent _databaseComponent;
        public delegate void UpdateBarSeries(List<DeviceRunInfo> deviceList);
        public event UpdateBarSeries UpdateChart;

        public DeviceRunStatusListViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess, IDatabaseComponent databaseComponent)
        {
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _signalProcess = signalProcess;
            _cardProcess = cardProcess;
            _databaseComponent = databaseComponent;

            InitTree();
            var deviceTrees = _cardProcess.GetDevices(OrganizationTreeItems);

            foreach (var deviceTree in deviceTrees)
            {
                devices.Add(new DeviceRunInfo()
                {
                    DeviceTreeItemViewModel = deviceTree,
                });
            }

            _view = new ListCollectionView(devices);
            _view.Filter = (object item) =>
            {
                if (OrganizationNames == null || OrganizationNames.Count == 0) return false;
                var itemPl = (DeviceRunInfo)item;
                if (itemPl == null) return false;
                if (OrganizationNames.Contains(itemPl.DeviceTreeItemViewModel.FullName) && itemPl.DeviceTreeItemViewModel.ServerIP == SelectedOrganization.ServerIP)
                {
                    return true;
                }
                return false;
            };
        }

        #region 管理树
        private void InitTree()
        {
            OrganizationTreeItems = _organizationService.OrganizationTreeItems;
            //TreeExpanded();
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

        private readonly ICollectionView _view;
        public ICollectionView DevicesView { get { return _view; } }

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

        private ObservableCollection<DeviceRunInfo> devices = new ObservableCollection<DeviceRunInfo>();
        public IEnumerable<DeviceRunInfo> Devices { get { return devices; } }

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

        public string waitinfo = "数据统计中";
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


        private int selectedDay = 7;

        public int SelectedDay
        {
            get
            {
                return selectedDay;
            }
            set
            {
                selectedDay = value;
                OnPropertyChanged("SelectedDay");
            }
        }

        public List<int> DayItems
        {
            get
            {
                return new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
            }
        }

        private DateTime startTime = DateTime.Now.AddDays(-7);
        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
                OnPropertyChanged("StartTime");
            }
        }
      
        #endregion

        #region Command
        private ICommand selectedTreeChangedComamnd;
        public ICommand SelectedTreeChangedComamnd
        {
            get
            {
                return this.selectedTreeChangedComamnd ?? (this.selectedTreeChangedComamnd = new DelegateCommand<object>(para => this.SelectedTreeChanged(para)));
            }
        }

        private DelegateCommand searchCommand;
        public DelegateCommand SearchCommand
        {
            get
            {
                return this.searchCommand ?? (this.searchCommand = new DelegateCommand(() => this.Search()));
            }
        }

        private DelegateCommand searchAllCommand;
        public DelegateCommand SearchAllCommand
        {
            get
            {
                return this.searchAllCommand ?? (this.searchAllCommand = new DelegateCommand(() => this.SearchAll()));
            }
        }
        #endregion

        private List<string> OrganizationNames;
        private OrganizationTreeItemViewModel SelectedOrganization;
        public void SelectedTreeChanged(object para)
        {
            if (para is DeviceTreeItemViewModel)
            {
                OrganizationNames = _cardProcess.GetDevices(para as OrganizationTreeItemViewModel).Select(p => p.FullName).ToList();
                SelectedOrganization = para as OrganizationTreeItemViewModel;
                _view.Refresh();
            }
            else if (para is OrganizationTreeItemViewModel)
            {
                if ((para as OrganizationTreeItemViewModel).Children != null && (para as OrganizationTreeItemViewModel).Children.Count > 0 )
                {
                    OrganizationNames = _cardProcess.GetDevices(para as OrganizationTreeItemViewModel).Select(p => p.FullName).ToList();
                    SelectedOrganization = para as OrganizationTreeItemViewModel;
                    _view.Refresh();
                }
                else
                {
                    OrganizationNames = new List<string>();
                    _view.Refresh();
                }
            }

        }

        private async void Search()
        {
            _view.Refresh();
            var sw = Stopwatch.StartNew();
            try
            {
                int number = 0;
                Status = ViewModelStatus.Querying;

                List<DeviceRunInfo> deviceList = new List<DeviceRunInfo>();
                foreach (var sub in _view.AsParallel())
                {
                    var device = sub as DeviceRunInfo;
                    if (device != null)
                    {
                        //foreach (var child in device.DeviceTreeItemViewModel.Children)
                        //{
                        //    if (child is ItemTreeItemViewModel)
                        //    {
                        //        ItemTreeItemViewModel itemTree = child as ItemTreeItemViewModel;
                        //        if (itemTree.T_Item != null && itemTree.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
                        //        {
                        //             await _databaseComponent.GetHistoryData<D_WirelessVibrationSlot>(itemTree.ServerIP, itemTree.T_Item.Guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade", "ExtraInfoJSON" }, StartTime, StartTime.AddDays(SelectedDay), null, null);
                        //        }
                        //    }
                        //}
                        WaitInfo = "数据统计: " + number.ToString();
                        await GetDeviceRunInfo(device);
                        number++;

                        deviceList.Add(device);
                    }
                }
                if (UpdateChart != null)
                {
                    UpdateChart(deviceList);
                }
            }
            catch(Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("设备数据-运行状态查询", ex));
            }
            finally
            {
                Console.WriteLine("消耗时间" + sw.Elapsed.ToString());
                Status = ViewModelStatus.None;
            }
        }

        private async Task GetDeviceRunInfo(DeviceRunInfo device)
        {
            List<Task<List<D_WirelessVibrationSlot>>> lttask = new List<Task<List<D_WirelessVibrationSlot>>>();
            foreach (var child in device.DeviceTreeItemViewModel.Children)
            {
                if (child is ItemTreeItemViewModel)
                {
                    ItemTreeItemViewModel itemTree = child as ItemTreeItemViewModel;
                    if (itemTree.T_Item != null && itemTree.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
                    {
                        lttask.Add(_databaseComponent.GetHistoryData<D_WirelessVibrationSlot>(itemTree.ServerIP, itemTree.T_Item.Guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade", "ExtraInfoJSON" }, StartTime, StartTime.AddDays(SelectedDay), null, null));                        
                    }
                }
            }
            await Task.WhenAll(lttask.ToArray());
            var infoList = lttask.Select(p => GetSubDeviceRunInfo(p.Result)).OrderBy(p => p.RunHours).ToList();
            if (infoList.Count > 0)
            {
                int count = infoList.Count;
                var midinfo = infoList[count / 2];
                device.StartTime = StartTime;
                device.EndTime = StartTime.AddDays(SelectedDay);
                device.RunHours = midinfo.RunHours;
                device.TotalHours = SelectedDay * 24;
                device.StopHours = device.TotalHours - device.RunHours;
                device.PreAlarmCount = infoList.Select(p => p.PreAlarmCount).Sum();
                device.AlarmCount = infoList.Select(p => p.AlarmCount).Sum();
                device.DangerCount = infoList.Select(p => p.DangerCount).Sum();
            }
        }

        private DeviceRunInfo GetSubDeviceRunInfo(List<D_WirelessVibrationSlot> result)
        {
            DeviceRunInfo DeviceRunInfo = new DeviceRunInfo();
            DeviceRunInfo.RunHours = 0;
            DeviceRunInfo.PreAlarmCount = 0;
            DeviceRunInfo.AlarmCount = 0;
            DeviceRunInfo.DangerCount = 0;

            if (result != null && result.Count > 0)
            {
                foreach (var data in result)
                {
                    var extraInfoJson = data.ExtraInfoJSON;
                    if (!string.IsNullOrWhiteSpace(extraInfoJson))
                    {
                        M9600.Common.DTO.Device.ExtraInfo extraInfo = JsonConvert.DeserializeObject<M9600.Common.DTO.Device.ExtraInfo>(extraInfoJson.Substring(1, extraInfoJson.Length - 2));
                        if (extraInfo != null)
                        {
                            DeviceRunInfo.RunHours += (extraInfo.NormalTimeLength + extraInfo.PreAlarmTimeLength + extraInfo.AlarmTimeLength + extraInfo.DangerTimeLength) / 3600;
                            DeviceRunInfo.PreAlarmCount += extraInfo.PreAlarmCount;
                            DeviceRunInfo.AlarmCount += extraInfo.AlarmCount;
                            DeviceRunInfo.DangerCount += extraInfo.DangerCount;
                        }
                    }                    
                }
            }

            return DeviceRunInfo;
        }

        private async void SearchAll()
        {  
            HashSet<Guid> guidlist = new HashSet<Guid>();
            string ip = null;
         
            foreach (var sub in _view.AsParallel())
            {               
                var device = sub as DeviceRunInfo;
                if (device != null)
                {
                    foreach (var child in device.DeviceTreeItemViewModel.Children)
                    {
                        if (child is ItemTreeItemViewModel)
                        {
                            ItemTreeItemViewModel itemTree = child as ItemTreeItemViewModel;
                            if (itemTree.T_Item != null && itemTree.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
                            {
                                guidlist.Add(itemTree.T_Item.Guid);
                            }
                        }
                    }
                    if (ip == null)
                    {
                        ip = device.DeviceTreeItemViewModel.ServerIP;
                    }
                }
            }

            var runlist = await _databaseComponent.GetStatisticsData(ip, guidlist);
            if (runlist == null)
            {
                return;
            }
            var allcounts = runlist.Where(o => o.Value.ContainsKey("NormalTimeLength") && o.Value.ContainsKey("PreAlarmTimeLength") && o.Value.ContainsKey("AlarmTimeLength") && o.Value.ContainsKey("DangerTimeLength")).OrderBy(o => o.Value["NormalTimeLength"] + o.Value["PreAlarmTimeLength"] + o.Value["AlarmTimeLength"] + o.Value["DangerTimeLength"]).ToList();
            List<DeviceRunInfo> deviceList = new List<DeviceRunInfo>();
            foreach (var sub in _view.AsParallel())
            {
                var device = sub as DeviceRunInfo;
                if (device != null)
                {
                    var counts = allcounts.Where(o => device.DeviceTreeItemViewModel.Children.OfType<ItemTreeItemViewModel>().Select(p => p.T_Item.Guid).Contains(o.Key)).ToList();
                    if (counts.Count > 0)
                    {
                        int count = counts.Count;
                        var alarm = counts[count / 2];
                        device.StartTime = new DateTime((long)alarm.Value["FirstUploadTime"]);
                        device.EndTime = new DateTime((long)alarm.Value["LastUploadTime"]);
                        device.RunHours = (alarm.Value["NormalTimeLength"] + alarm.Value["PreAlarmTimeLength"] + alarm.Value["AlarmTimeLength"] + alarm.Value["DangerTimeLength"]) / 3600;
                        device.TotalHours = (device.EndTime - device.StartTime).TotalHours;
                        device.StopHours = device.TotalHours - device.RunHours;
                        device.PreAlarmCount = counts.Select(p => p.Value["PreAlarmCount"]).Sum();
                        device.AlarmCount = counts.Select(p => p.Value["AlarmCount"]).Sum();
                        device.DangerCount = counts.Select(p => p.Value["DangerCount"]).Sum();
                    }
                    deviceList.Add(device);
                }
            }
            if (UpdateChart != null)
            {
                UpdateChart(deviceList);
            }
            
        }
    }
}

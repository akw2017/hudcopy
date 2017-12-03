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

namespace AIC.DeviceDataPage.ViewModels
{
    class DeviceRunStatusListViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        private readonly IDatabaseComponent _databaseComponent;
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

        public string waitinfo = "数据查询中";
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


        private int selectedDay;

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
                if ((para as OrganizationTreeItemViewModel).Children != null && (para as OrganizationTreeItemViewModel).Children.Count > 0 && (para as OrganizationTreeItemViewModel).Children[0] is DeviceTreeItemViewModel)
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

        private void Search()
        {
            _view.Refresh();   
            foreach(var device in _view)
            {

            }
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
                            ItemTreeItemViewModel item = child as ItemTreeItemViewModel;
                            if (item.T_Item != null && item.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
                            {
                                guidlist.Add(item.T_Item.Guid);
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
                        device.PreAlarmCount = alarm.Value["PreAlarmCount"];
                        device.AlarmCount = alarm.Value["AlarmCount"];
                        device.DangerCount = alarm.Value["DangerCount"];
                    }
                }
            }                
        }
    }
}

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
using AIC.DeviceDataPage.Views;
using System.Data;

namespace AIC.DeviceDataPage.ViewModels
{
    class DeviceHourlyDataViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        private readonly IDatabaseComponent _databaseComponent;

        public DeviceHourlyDataViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess, IDatabaseComponent databaseComponent)
        {
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _signalProcess = signalProcess;
            _cardProcess = cardProcess;
            _databaseComponent = databaseComponent;

            InitTree();

            StartTime = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));
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

        private ObservableCollection<DeviceHourlyDataInfo> devicesView = new ObservableCollection<DeviceHourlyDataInfo>();
        public ObservableCollection<DeviceHourlyDataInfo> DevicesView
        {
            get { return devicesView; }
            set
            {
                devicesView = value;
                OnPropertyChanged("DevicesView");
            }
        }

        private ObservableCollection<DeviceHourlySelectedResult> selectedResult = new ObservableCollection<DeviceHourlySelectedResult>();
        public ObservableCollection<DeviceHourlySelectedResult> DeviceHourlySelectedResult
        {
            get { return selectedResult; }
            set
            {
                selectedResult = value;
                OnPropertyChanged("DeviceHourlySelectedResult");
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
                return new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 360 };
            }
        }

        private DateTime startTime;// = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));
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

        private ICommand selectedChangedComamnd;
        public ICommand SelectedChangedComamnd
        {
            get
            {
                return this.selectedChangedComamnd ?? (this.selectedChangedComamnd = new DelegateCommand<object>(para => this.SelectedChanged(para)));
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

        private DelegateCommand printCommand;
        public DelegateCommand PrintCommand
        {
            get
            {
                return this.printCommand ?? (this.printCommand = new DelegateCommand(() => this.Print()));
            }
        }

        private DelegateCommand exportCommand;
        public DelegateCommand ExportCommand
        {
            get
            {
                return this.exportCommand ?? (this.exportCommand = new DelegateCommand(() => this.Export()));
            }
        }

        private DelegateCommand checkedAllCommand;
        public DelegateCommand CheckedAllCommand
        {
            get
            {
                return this.checkedAllCommand ?? (this.checkedAllCommand = new DelegateCommand(() => this.CheckedAll()));
            }
        }

        private DelegateCommand unCheckedAllCommand;
        public DelegateCommand UnCheckedAllCommand
        {
            get
            {
                return this.unCheckedAllCommand ?? (this.unCheckedAllCommand = new DelegateCommand(() => this.UnCheckedAll()));
            }
        }
        #endregion

        private OrganizationTreeItemViewModel selectedOrganization;
        public void SelectedTreeChanged(object para)
        {
            selectedOrganization = para as OrganizationTreeItemViewModel;
        }

        public void SelectedChanged(object para)
        {
            var selected =  para as DeviceHourlySelectedResult;
            if (selected != null)
            {              
                DevicesView = new ObservableCollection<Models.DeviceHourlyDataInfo>();
                var devices = resultDevices.SelectMany(p => p.Where(d => d.ACQDate.Date == selected.DateTime.Date));
                DevicesView.AddRange(devices);               
            }
        }


        #region 
        private List<List<DeviceHourlyDataInfo>> resultDevices = new List<List<DeviceHourlyDataInfo>>();
        private async void Search()
        {
            var sw = Stopwatch.StartNew();
            try
            {
                int number = 0;
                Status = ViewModelStatus.Querying;

                DevicesView.Clear();
                resultDevices.Clear();
                DeviceHourlySelectedResult.Clear();
               
                for (int i = 0; i < SelectedDay; i++)
                {
                    DeviceHourlySelectedResult.Add(new DeviceHourlySelectedResult() { DateTime = StartTime.AddDays(i), IsChecked = true });                  
                }


                var deviceTrees = _cardProcess.GetDevices(selectedOrganization);
                if (deviceTrees == null)
                {
                    return;
                }

                foreach (var deviceTree in deviceTrees)
                {               
                    WaitInfo = "数据统计: " + number.ToString();
                    List<DeviceHourlyDataInfo> devices = new List<DeviceHourlyDataInfo>();

                    DateTime start = StartTime;                    
                    for (int day = SelectedDay; day > 0;)
                    {
                        int dayRange = day > 30 ? 30 : day;
                        devices.AddRange(await GetDeviceHourlyData(deviceTree, start, dayRange));
                        day = day - dayRange;
                        start = start.AddDays(dayRange);
                    }
                    if (devices == null)
                    {
                        continue;
                    }
                    devices.ForEach(p => p.DeviceTreeItemViewModel = deviceTree);
                    resultDevices.Add(devices);
                    DeviceHourlyDataInfo device = devices.FirstOrDefault();

                    number++;

                    DevicesView.Add(device);
                }   
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("设备数据-运行状态查询", ex));
            }
            finally
            {
                Console.WriteLine("消耗时间" + sw.Elapsed.ToString());
                Status = ViewModelStatus.None;
            }
        }

        private async Task<List<DeviceHourlyDataInfo>> GetDeviceHourlyData(DeviceTreeItemViewModel deviceTree, DateTime start, int day)
        {
            List<Guid> guids = new List<Guid>();
            foreach (var child in deviceTree.Children)
            {
                if (child is ItemTreeItemViewModel)
                {
                    ItemTreeItemViewModel itemTree = child as ItemTreeItemViewModel;
                    if (itemTree.T_Item != null && itemTree.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
                    {
                        guids.Add(itemTree.T_Item.Guid);                     
                    }
                }
            }
          
            var result = await _databaseComponent.GetHistoryData<D_WirelessVibrationSlot>(deviceTree.ServerIP, guids.ToArray(), new string[] { "T_Item_Guid", "ACQDatetime", "Result", "Unit", "AlarmGrade", "RPM", "RecordLab", "IsValidWave", "ExtraInfoJSON" }, start, start.AddDays(day), null, null);
            if (result == null || result.Count == 0)
            {
                return GetSubDeviceHourlyData(result, start, day);
            }
            var infoList = result.GroupBy(p => p.T_Item_Guid, (key, group) => new { Key = key, Value = group }).Select(p => GetSubDeviceHourlyData(p.Value.ToList(), start, day)).OrderBy(p => p.FirstOrDefault().HourlyData1).ToList();
            if (infoList.Count > 0)
            {
                int count = infoList.Count;
                var midinfo = infoList[count / 2];
                return midinfo;
            }
            else
            {
                return GetSubDeviceHourlyData(result, start, day);
            }
        }
      

        private List<DeviceHourlyDataInfo> GetSubDeviceHourlyData(List<D_WirelessVibrationSlot> result, DateTime start, int day)
        {
            List<DeviceHourlyDataInfo> devices = new List<DeviceHourlyDataInfo>();
            for (int i = 0; i < day; i++)
            {
                DeviceHourlyDataInfo device = new DeviceHourlyDataInfo();
                device.ACQDate = start.AddDays(i);
                device.HasData = false;
                devices.Add(device);
            }

            if (result != null && result.Count > 0)
            {
                //分组
                var groupresult = result.OrderBy(p => p.ACQDatetime).GroupBy(p => new { Year = p.ACQDatetime.Year, Month = p.ACQDatetime.Month, Day = p.ACQDatetime.Day });
                foreach (var groupdata in groupresult)//按天数据
                {
                    D_WirelessVibrationSlot firstdata = groupdata.FirstOrDefault();
                    DeviceHourlyDataInfo device = devices.Where(p => p.ACQDate.Date == firstdata.ACQDatetime.Date).FirstOrDefault();                   
                    device.Unit = firstdata.Unit;
                    device.HasData = true;

                    var subgroupresult = groupdata.GroupBy(p => p.ACQDatetime.Hour);
                    foreach (var subgroupdata in subgroupresult)//按小时数据
                    {
                        var data = subgroupdata.FirstOrDefault();
                        #region
                        if (data.ACQDatetime.Hour == 0)
                        {
                            device.HourlyData0 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 1)
                        {
                            device.HourlyData1 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 2)
                        {
                            device.HourlyData2 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 3)
                        {
                            device.HourlyData3 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 4)
                        {
                            device.HourlyData4 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 5)
                        {
                            device.HourlyData5 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 6)
                        {
                            device.HourlyData6 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 7)
                        {
                            device.HourlyData7 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 8)
                        {
                            device.HourlyData8 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 9)
                        {
                            device.HourlyData9 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 10)
                        {
                            device.HourlyData10 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 11)
                        {
                            device.HourlyData11 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 12)
                        {
                            device.HourlyData12 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 13)
                        {
                            device.HourlyData13 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 14)
                        {
                            device.HourlyData14 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 15)
                        {
                            device.HourlyData15 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 16)
                        {
                            device.HourlyData16 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 17)
                        {
                            device.HourlyData17 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 18)
                        {
                            device.HourlyData18 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 19)
                        {
                            device.HourlyData19 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 20)
                        {
                            device.HourlyData20 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 21)
                        {
                            device.HourlyData21 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 22)
                        {
                            device.HourlyData22 = data.Result.Value;
                        }
                        else if (data.ACQDatetime.Hour == 23)
                        {
                            device.HourlyData23 = data.Result.Value;
                        }
                     
                        #endregion
                    }                   
                }
            }

            return devices;
        }
        #endregion
        private void Print()
        {
            List<DeviceHourlyPrintResult> printList = new List<Models.DeviceHourlyPrintResult>();
            foreach(var result in DeviceHourlySelectedResult.Where(p => p.IsChecked == true))
            {
                DeviceHourlyPrintResult print = new Models.DeviceHourlyPrintResult();
                print.DateTime = result.DateTime;
                print.DeviceHourlyDataInfo = new List<DeviceHourlyDataInfo>(resultDevices.SelectMany(p => p.Where(d => d.ACQDate.Date == result.DateTime.Date)));
                printList.Add(print);
            }

            DeviceHourlyDataPrintPreviewWindow previewWnd = new DeviceHourlyDataPrintPreviewWindow("/AIC.DeviceDataPage;component/Views/DeviceHourlyDataFlowDocument.xaml", printList, new DeviceHourlyDataDocumentRenderer());
            previewWnd.ShowDialog();
        }

        private void Export()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("设备", typeof(string)));
            dt.Columns.Add(new DataColumn("时间", typeof(string)));
            dt.Columns.Add(new DataColumn("单位", typeof(string)));
            dt.Columns.Add(new DataColumn("0h", typeof(string)));
            dt.Columns.Add(new DataColumn("1h", typeof(string)));
            dt.Columns.Add(new DataColumn("2h", typeof(string)));
            dt.Columns.Add(new DataColumn("3h", typeof(string)));
            dt.Columns.Add(new DataColumn("4h", typeof(string)));
            dt.Columns.Add(new DataColumn("5h", typeof(string)));
            dt.Columns.Add(new DataColumn("6h", typeof(string)));
            dt.Columns.Add(new DataColumn("7h", typeof(string)));
            dt.Columns.Add(new DataColumn("8h", typeof(string)));
            dt.Columns.Add(new DataColumn("9h", typeof(string)));
            dt.Columns.Add(new DataColumn("10h", typeof(string)));
            dt.Columns.Add(new DataColumn("11h", typeof(string)));
            dt.Columns.Add(new DataColumn("12h", typeof(string)));
            dt.Columns.Add(new DataColumn("13h", typeof(string)));
            dt.Columns.Add(new DataColumn("14h", typeof(string)));
            dt.Columns.Add(new DataColumn("15h", typeof(string)));
            dt.Columns.Add(new DataColumn("16h", typeof(string)));
            dt.Columns.Add(new DataColumn("17h", typeof(string)));
            dt.Columns.Add(new DataColumn("18h", typeof(string)));
            dt.Columns.Add(new DataColumn("19h", typeof(string)));
            dt.Columns.Add(new DataColumn("20h", typeof(string)));
            dt.Columns.Add(new DataColumn("21h", typeof(string)));
            dt.Columns.Add(new DataColumn("22h", typeof(string)));
            dt.Columns.Add(new DataColumn("23h", typeof(string)));


            foreach (var result in DeviceHourlySelectedResult.Where(p => p.IsChecked == true))
            {               
                var dayList = new List<DeviceHourlyDataInfo>(resultDevices.SelectMany(p => p.Where(d => d.ACQDate.Date == result.DateTime.Date)));
                foreach (var day in dayList)
                {
                    dt.Rows.Add(day.DeviceTreeItemViewModel.Name, " " + day.ACQDate.ToString("yyyy-MM-dd"), day.Unit,
                        (day.HourlyData0 != null) ? day.HourlyData0.Value.ToString("f3") : null,
                        (day.HourlyData1 != null) ? day.HourlyData1.Value.ToString("f3") : null,
                        (day.HourlyData2 != null) ? day.HourlyData2.Value.ToString("f3") : null,
                        (day.HourlyData3 != null) ? day.HourlyData3.Value.ToString("f3") : null,
                        (day.HourlyData4 != null) ? day.HourlyData4.Value.ToString("f3") : null,
                        (day.HourlyData5 != null) ? day.HourlyData5.Value.ToString("f3") : null,
                        (day.HourlyData6 != null) ? day.HourlyData6.Value.ToString("f3") : null,
                        (day.HourlyData7 != null) ? day.HourlyData7.Value.ToString("f3") : null,
                        (day.HourlyData8 != null) ? day.HourlyData8.Value.ToString("f3") : null,
                        (day.HourlyData9 != null) ? day.HourlyData9.Value.ToString("f3") : null,
                        (day.HourlyData10 != null) ? day.HourlyData10.Value.ToString("f3") : null,
                        (day.HourlyData11 != null) ? day.HourlyData11.Value.ToString("f3") : null,
                        (day.HourlyData12 != null) ? day.HourlyData12.Value.ToString("f3") : null,
                        (day.HourlyData13 != null) ? day.HourlyData13.Value.ToString("f3") : null,
                        (day.HourlyData14 != null) ? day.HourlyData14.Value.ToString("f3") : null,
                        (day.HourlyData15 != null) ? day.HourlyData15.Value.ToString("f3") : null,
                        (day.HourlyData16 != null) ? day.HourlyData16.Value.ToString("f3") : null,
                        (day.HourlyData17 != null) ? day.HourlyData17.Value.ToString("f3") : null,
                        (day.HourlyData18 != null) ? day.HourlyData18.Value.ToString("f3") : null,
                        (day.HourlyData19 != null) ? day.HourlyData19.Value.ToString("f3") : null,
                        (day.HourlyData20 != null) ? day.HourlyData20.Value.ToString("f3") : null,
                        (day.HourlyData21 != null) ? day.HourlyData21.Value.ToString("f3") : null,
                        (day.HourlyData22 != null) ? day.HourlyData22.Value.ToString("f3") : null,
                        (day.HourlyData23 != null) ? day.HourlyData23.Value.ToString("f3") : null);
                }
                dt.Rows.Add("-");
            }

            ExportPreviewWindow previewWnd = new ExportPreviewWindow(dt);
            previewWnd.ShowDialog();
        }

        private void CheckedAll()
        {
            foreach (var result in DeviceHourlySelectedResult)
            {
                result.IsChecked = true;
            }
        }

        private void UnCheckedAll()
        {
            foreach(var result in DeviceHourlySelectedResult)
            {
                result.IsChecked = false;
            }
        }

    }
}

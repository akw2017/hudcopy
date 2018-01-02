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
    class DeviceRunAnalyzeListViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        private readonly IDatabaseComponent _databaseComponent;
        public delegate void UpdateBarSeries(IEnumerable<DeviceRunInfo> deviceList);
        public event UpdateBarSeries UpdateChart;
        public delegate void ShowBarSeries(DeviceRunInfo device);
        public event ShowBarSeries ShowBarSeriesChanged;

        public DeviceRunAnalyzeListViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess, IDatabaseComponent databaseComponent)
        {
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _signalProcess = signalProcess;
            _cardProcess = cardProcess;
            _databaseComponent = databaseComponent;

            InitTree();
            var deviceTrees = _cardProcess.GetDevices(OrganizationTreeItems);
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

        private ObservableCollection<DeviceRunInfo> devicesView;
        public ObservableCollection<DeviceRunInfo> DevicesView
        {
            get { return devicesView; }
            set
            {
                devicesView = value;
                OnPropertyChanged("DevicesView");
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
                return new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
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

        private ICommand selectedDataGridChangedComamnd;
        public ICommand SelectedDataGridChangedComamnd
        {
            get
            {
                return this.selectedDataGridChangedComamnd ?? (this.selectedDataGridChangedComamnd = new DelegateCommand<object>(para => this.SelectedDataGridChanged(para)));
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

        public ICommand mouseDoubleClickComamnd;
        public ICommand MouseDoubleClickComamnd
        {
            get
            {
                return this.mouseDoubleClickComamnd ?? (this.mouseDoubleClickComamnd = new DelegateCommand<object>(para => this.MouseDoubleClick(para)));
            }
        }
        #endregion

        private OrganizationTreeItemViewModel selectedOrganization;
        public void SelectedTreeChanged(object para)
        {
            selectedOrganization = para as OrganizationTreeItemViewModel;
        }

        public void SelectedDataGridChanged(object para)
        {
            var device = para as DeviceRunInfo;
            if (device != null && ShowBarSeriesChanged != null)
            {
                ShowBarSeriesChanged(device);
            }
        }

        private void MouseDoubleClick(object para)
        {
            var device = para as DeviceRunInfo;
            if (device != null && device.DiagnosticAdvice != null)
            {
#if XBAP
                MessageBox.Show(device.DiagnosticInfo + "\n\r" + device.DiagnosticAdvice, "诊断详情", MessageBoxButton.OK, MessageBoxImage.Information);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show(device.DiagnosticInfo + "\n\r" + device.DiagnosticAdvice, "诊断详情", MessageBoxButton.OK, MessageBoxImage.Information);
#endif
            }
        }

        #region 
        private async void Search()
        {
            var sw = Stopwatch.StartNew();
            try
            {
                int number = 0;
                Status = ViewModelStatus.Querying;

                DevicesView = new ObservableCollection<DeviceRunInfo>();
                var deviceTrees = _cardProcess.GetDevices(selectedOrganization);
                if (deviceTrees == null)
                {
                    return;
                }

                foreach (var deviceTree in deviceTrees)
                {
                    DeviceRunInfo device = new DeviceRunInfo();
                    device.DeviceTreeItemViewModel = deviceTree;
                    WaitInfo = "数据统计: " + number.ToString();
                    await GetDeviceRunInfo(device, StartTime, SelectedDay);
                    number++;

                    DevicesView.Add(device);
                }
                Dictionary<Guid, Tuple<Guid, DateTime>> recordLabs = new Dictionary<Guid, Tuple<Guid, DateTime>>();
                foreach (var device in DevicesView)
                {
                    if (device.RecordLab == new Guid())
                    {
                        continue;
                    }
                    recordLabs.Add(device.RecordLab, Tuple.Create<Guid, DateTime>(device.T_Item_Guid, device.ACQDatetime));   
                }

                if (DevicesView.Count > 0)
                {
                    var waves = await _databaseComponent.GetHistoryWaveformData<D_WirelessVibrationSlot_Waveform>(DevicesView[0].DeviceTreeItemViewModel.ServerIP, recordLabs);
                    foreach (var wave in waves.GroupBy(p => p.T_Item_Guid))
                    {
                        DeviceRunInfo device = DevicesView.Where(p => p.T_Item_Guid == wave.FirstOrDefault().T_Item_Guid).First();
                        string diagnosticInfo;
                        string diagnosticAdvice;
                        DiagnosticInfoClass.GetDiagnosticInfo(wave.FirstOrDefault().AlarmGrade, device.T_Item_Guid, wave.FirstOrDefault(), device.RPM, out diagnosticInfo, out diagnosticAdvice);
                        device.DiagnosticInfo = diagnosticInfo;
                        device.DiagnosticAdvice = diagnosticAdvice;
                    }
                }
                if (UpdateChart != null && number > 0)
                {
                    UpdateChart(DevicesView);
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

        private async Task GetDeviceRunInfo(DeviceRunInfo device, DateTime start, int day)
        {
            //List<Task<List<D_WirelessVibrationSlot>>> lttask = new List<Task<List<D_WirelessVibrationSlot>>>();
            List<Guid> guids = new List<Guid>();
            foreach (var child in device.DeviceTreeItemViewModel.Children)
            {
                if (child is ItemTreeItemViewModel)
                {
                    ItemTreeItemViewModel itemTree = child as ItemTreeItemViewModel;
                    if (itemTree.T_Item != null && itemTree.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
                    {
                        guids.Add(itemTree.T_Item.Guid);
                        //lttask.Add(_databaseComponent.GetHistoryData<D_WirelessVibrationSlot>(itemTree.ServerIP, itemTree.T_Item.Guid, new string[] { "ACQDatetime", "Result", "Unit", "RPM", "AlarmGrade", "ExtraInfoJSON" }, StartTime, StartTime.AddDays(SelectedDay), null, null));                        
                    }
                }
            }
            //await Task.WhenAll(lttask.ToArray());
            //var infoList = lttask.Select(p => GetSubDeviceRunInfo(p.Result, StartTime, SelectedDay, device)).OrderBy(p => p.Result.RunHours).Select(p => p.Result).ToList();
            var result = await _databaseComponent.GetHistoryData<D_WirelessVibrationSlot>(device.DeviceTreeItemViewModel.ServerIP, guids.ToArray(), new string[] { "T_Item_Guid", "ACQDatetime", "Result", "Unit", "AlarmGrade", "RPM", "RecordLab", "IsValidWave", "ExtraInfoJSON" }, start, start.AddDays(day), null, null);
            if (result == null || result.Count == 0)
            {
                return; 
            }
            var infoList = result.GroupBy(p => p.T_Item_Guid, (key, group) => new { Key = key, Value = group }).Select(p => GetSubDeviceRunInfo(p.Value.ToList(), start, day)).OrderBy(p => p.RunHours).ToList();
            if (infoList.Count > 0)
            {
                int count = infoList.Count;
                var midinfo = infoList[count / 2];
                device.StartTime = start;
                device.EndTime = start.AddDays(day);
                device.RunHours = midinfo.RunHours;
                device.TotalHours = day * 24;
                device.StopHours = device.TotalHours - device.RunHours;
                device.PreAlarmCount = infoList.Select(p => p.PreAlarmCount).Sum();
                device.AlarmCount = infoList.Select(p => p.AlarmCount).Sum();
                device.DangerCount = infoList.Select(p => p.DangerCount).Sum();
                device.RunInfo = midinfo.RunInfo;
  
                device.ACQDatetime = midinfo.ACQDatetime;
                device.RecordLab = midinfo.RecordLab;
                device.T_Item_Guid = midinfo.T_Item_Guid;
                device.RPM = (float)midinfo.RPM;
                device.MaxResult = midinfo.MaxResult;
                device.MinResult = midinfo.MinResult;
                device.Unit = midinfo.Unit;
                device.AlarmGrade = midinfo.AlarmGrade;
            }
        }

        private DeviceRunInfo GetSubDeviceRunInfo(List<D_WirelessVibrationSlot> result, DateTime start, int day)
        {
            DeviceRunInfo devicerunInfo = new DeviceRunInfo();
            devicerunInfo.RunInfo = new List<RunInfo>();
            for (int i = 0; i < day; i++)
            {
                devicerunInfo.RunInfo.Add(new RunInfo() { Time = start.AddDays(i), });
            }

            devicerunInfo.RunHours = 0;
            devicerunInfo.PreAlarmCount = 0;
            devicerunInfo.AlarmCount = 0;
            devicerunInfo.DangerCount = 0;

            if (result != null && result.Count > 0)
            {
                //分组
                var groupresult = result.GroupBy(p => new { Year = p.ACQDatetime.Year, Month = p.ACQDatetime.Month, Day = p.ACQDatetime.Day });
                foreach (var groupdata in groupresult)
                {
                    double hours = 0;
                    foreach (var data in groupdata)
                    {
                        var extraInfoJson = data.ExtraInfoJSON;
                        if (!string.IsNullOrWhiteSpace(extraInfoJson))
                        {
                            M9600.Common.DTO.Device.ExtraInfo extraInfo = JsonConvert.DeserializeObject<M9600.Common.DTO.Device.ExtraInfo>(extraInfoJson.Substring(1, extraInfoJson.Length - 2));
                            if (extraInfo != null)
                            {
                                hours += (extraInfo.NormalTimeLength + extraInfo.PreAlarmTimeLength + extraInfo.AlarmTimeLength + extraInfo.DangerTimeLength) / 3600;
                                //DeviceRunInfo.RunHours += (extraInfo.NormalTimeLength + extraInfo.PreAlarmTimeLength + extraInfo.AlarmTimeLength + extraInfo.DangerTimeLength) / 3600;
                                devicerunInfo.PreAlarmCount += extraInfo.PreAlarmCount;
                                devicerunInfo.AlarmCount += extraInfo.AlarmCount;
                                devicerunInfo.DangerCount += extraInfo.DangerCount;
                            }
                        }
                    }
                    DateTime time = groupdata.First().ACQDatetime;
                    devicerunInfo.RunHours += hours;
                    RunInfo runinfo = devicerunInfo.RunInfo.Where(p => p.Time.Year == time.Year && p.Time.Month == time.Month && p.Time.Day == time.Day).First();
                    runinfo.RunHours = hours;
                    var max = groupdata.Where(p => p.IsValidWave == true).OrderBy(p => p.AlarmGrade & 0xff).ThenBy(n => n.Result).LastOrDefault();
                    if (max != null)
                    {
                        runinfo.MaxResult = max.Result.Value;
                        runinfo.MinResult = groupdata.Where(p => p.AlarmGrade == max.AlarmGrade).OrderBy(n => n.Result).Select(p => p.Result).First() ?? runinfo.MaxResult;
                        runinfo.ACQDatetime = max.ACQDatetime;
                        runinfo.RecordLab = (max.IsValidWave == true) ? max.RecordLab.Value : new Guid();
                        runinfo.T_Item_Guid = max.T_Item_Guid;
                        runinfo.RPM = (float)max.RPM;
                        runinfo.Unit = max.Unit;
                        runinfo.AlarmGrade = (AlarmGrade)(max.AlarmGrade & 0x00ffff00);
                    }
                }

                RunInfo maxruninfo = devicerunInfo.RunInfo.OrderBy(p => p.MaxResult).LastOrDefault();
                devicerunInfo.ACQDatetime = maxruninfo.ACQDatetime;
                devicerunInfo.RecordLab = maxruninfo.RecordLab;
                devicerunInfo.T_Item_Guid = maxruninfo.T_Item_Guid;
                devicerunInfo.RPM = maxruninfo.RPM;
                devicerunInfo.MaxResult = maxruninfo.MaxResult;
                devicerunInfo.MinResult = maxruninfo.MinResult;
                devicerunInfo.Unit = maxruninfo.Unit;
                devicerunInfo.AlarmGrade = maxruninfo.AlarmGrade;
            }

            return devicerunInfo;
        }
        #endregion

        //        private async void Search()
        //        {
        //            _view.Refresh();
        //            var sw = Stopwatch.StartNew();
        //            try
        //            {
        //                int number = 0;
        //                Status = ViewModelStatus.Querying;
        //                var progress = new Progress<double>();
        //                progress.ProgressChanged += (sender, args) =>
        //                {
        //                    number++;
        //                    WaitInfo = "数据统计: " + number.ToString();
        //                };
        //                WaitInfo = "数据统计: " + number.ToString();

        //                Dictionary<DeviceRunInfo, Task<List<D_WirelessVibrationSlot>>> lttask = new Dictionary<DeviceRunInfo, Task<List<D_WirelessVibrationSlot>>>();
        //                List<DeviceRunInfo> deviceList = new List<DeviceRunInfo>();
        //                foreach (var sub in _view.AsParallel())
        //                {
        //                    var device = sub as DeviceRunInfo;
        //                    if (device != null)
        //                    {
        //                        List<Guid> guids = new List<Guid>();
        //                        foreach (var child in device.DeviceTreeItemViewModel.Children)
        //                        {
        //                            if (child is ItemTreeItemViewModel)
        //                            {
        //                                ItemTreeItemViewModel itemTree = child as ItemTreeItemViewModel;
        //                                if (itemTree.T_Item != null && itemTree.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
        //                                {
        //                                    guids.Add(itemTree.T_Item.Guid);
        //                                }
        //                            }
        //                        }

        //                        lttask.Add(device, _databaseComponent.GetHistoryData<D_WirelessVibrationSlot>(device.DeviceTreeItemViewModel.ServerIP, guids.ToArray(), new string[] { "T_Item_Guid", "ACQDatetime", "Result", "Unit", "AlarmGrade", "RPM", "RecordLab", "IsValidWave", "ExtraInfoJSON" }, StartTime, StartTime.AddDays(SelectedDay), null, null, progress));

        //                        deviceList.Add(device);
        //                    }
        //                }
        //                await Task.WhenAll(lttask.Values.ToArray());

        //                Dictionary<Guid, Tuple<Guid, DateTime>> recordLabs = new Dictionary<Guid, Tuple<Guid, DateTime>>();
        //                foreach (var task in lttask)
        //                {                    
        //                    var device = task.Key;
        //                    if (task.Value.Result == null || task.Value.Result.Count == 0)
        //                    {
        //                        continue;
        //                    }
        //                    var xxx = task.Value.Result.GroupBy(p => p.T_Item_Guid, (key, group) => new { Key = key, Value = group }).Select(p => p.Value);
        //                    foreach(var xx in xxx)
        //                    { 
        //}
        //                    var infoList = task.Value.Result.GroupBy(p => p.T_Item_Guid, (key, group) => new { Key = key, Value = group }).Select(p => GetSubDeviceRunInfo(p.Value.ToList(), StartTime, SelectedDay)).OrderBy(p => p.RunHours).ToList();
        //                    if (infoList.Count > 0)
        //                    {
        //                        int count = infoList.Count;
        //                        var midinfo = infoList[count / 2];
        //                        device.StartTime = StartTime;
        //                        device.EndTime = StartTime.AddDays(SelectedDay);
        //                        device.RunHours = midinfo.RunHours;
        //                        device.TotalHours = SelectedDay * 24;
        //                        device.StopHours = device.TotalHours - device.RunHours;
        //                        device.PreAlarmCount = infoList.Select(p => p.PreAlarmCount).Sum();
        //                        device.AlarmCount = infoList.Select(p => p.AlarmCount).Sum();
        //                        device.DangerCount = infoList.Select(p => p.DangerCount).Sum();
        //                        device.RunInfo = midinfo.RunInfo;
        //                        device.T_Item_Guid = midinfo.T_Item_Guid;
        //                        device.RecordLab = midinfo.RecordLab;
        //                        device.ACQDatetime = midinfo.ACQDatetime;
        //                        device.RPM = device.RPM;
        //                    }
        //                    recordLabs.Add(device.RecordLab, Tuple.Create<Guid, DateTime>(device.T_Item_Guid, device.ACQDatetime));     
        //                    //if (recordLabs.Count > 0)
        //                    //{
        //                    //    break;
        //                    //}         
        //                }

        //                if (deviceList.Count > 0)
        //                {
        //                    var waves = await _databaseComponent.GetHistoryWaveformData<D_WirelessVibrationSlot_Waveform>(deviceList[0].DeviceTreeItemViewModel.ServerIP, recordLabs);
        //                    foreach (var wave in waves.GroupBy(p => p.T_Item_Guid))
        //                    {
        //                        DeviceRunInfo device = deviceList.Where(p => p.T_Item_Guid == wave.FirstOrDefault().T_Item_Guid).First();
        //                        string diagnosticInfo;
        //                        string diagnosticAdvice;
        //                        DiagnosticInfoClass.GetDiagnosticInfo(wave.FirstOrDefault().AlarmGrade, device.DeviceTreeItemViewModel.Name, device.T_Item_Guid, wave.ToArray(), device.RPM, out diagnosticInfo, out diagnosticAdvice);
        //                        device.DiagnosticInfo = diagnosticInfo + "\r\n" + diagnosticAdvice;
        //                    }
        //                }

        //                if (UpdateChart != null && number > 0)
        //                {
        //                    UpdateChart(deviceList);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("设备数据-运行状态查询", ex));
        //            }
        //            finally
        //            {
        //                Console.WriteLine("消耗时间" + sw.Elapsed.ToString());
        //                Status = ViewModelStatus.None;
        //            }
        //        }



    }
}

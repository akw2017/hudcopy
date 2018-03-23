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
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System.Windows.Media;
using AIC.M9600.Common.DTO;
using System.Collections;
using AIC.DeviceDataPage.Views;

namespace AIC.DeviceDataPage.ViewModels
{
    class DeviceRunAnalyzeListViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        private readonly IDatabaseComponent _databaseComponent;
        public delegate void DeviceRunAnalyzePicGenerate(DeviceRunAnalyzeDataInfo deviceRunAnalyze);
        public event DeviceRunAnalyzePicGenerate DeviceRunAnalyzePicGenerated;

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

        private DeviceRunAnalyzeDataInfo deviceRunAnalyze = new DeviceRunAnalyzeDataInfo();
        public DeviceRunAnalyzeDataInfo DeviceRunAnalyze
        {
            get
            {
                return deviceRunAnalyze;
            }
            set
            {
                deviceRunAnalyze = value;
                OnPropertyChanged("deviceRunAnalyze");
            }
        }      

        private SeriesCollection maxColumnSeries;
        public SeriesCollection MaxColumnSeries
        {
            get { return maxColumnSeries; }
            set
            {
                if (maxColumnSeries != value)
                {
                    maxColumnSeries = value;
                    OnPropertyChanged("MaxColumnSeries");
                }
            }
        }

        private string[] maxColumnLabels;
        public string[] MaxColumnLabels
        {
            get { return maxColumnLabels; }
            set
            {
                if (maxColumnLabels != value)
                {
                    maxColumnLabels = value;
                    OnPropertyChanged("MaxColumnLabels");
                }
            }
        }

        public Func<double, string> MaxColumnFormatter
        {
            get { return value => value.ToString("f3"); }
        }

        private SeriesCollection maxMoreColumnSeries;
        public SeriesCollection MaxMoreColumnSeries
        {
            get { return maxMoreColumnSeries; }
            set
            {
                if (maxMoreColumnSeries != value)
                {
                    maxMoreColumnSeries = value;
                    OnPropertyChanged("MaxMoreColumnSeries");
                }
            }
        }

        private string[] maxMoreColumnLabels;
        public string[] MaxMoreColumnLabels
        {
            get { return maxMoreColumnLabels; }
            set
            {
                if (maxMoreColumnLabels != value)
                {
                    maxMoreColumnLabels = value;
                    OnPropertyChanged("MaxMoreColumnLabels");
                }
            }
        }

        public Func<double, string> MaxMoreColumnFormatter
        {
            get { return value => value.ToString("f3"); }
        }

        private SeriesCollection runColumnSeries;
        public SeriesCollection RunColumnSeries
        {
            get { return runColumnSeries; }
            set
            {
                if (runColumnSeries != value)
                {
                    runColumnSeries = value;
                    OnPropertyChanged("RunColumnSeries");
                }
            }
        }

        private string[] runColumnLabels;
        public string[] RunColumnLabels
        {
            get { return runColumnLabels; }
            set
            {
                if (runColumnLabels != value)
                {
                    runColumnLabels = value;
                    OnPropertyChanged("RunColumnLabels");
                }
            }
        }

        public Func<double, string> RunColumnFormatter
        {
            get { return value => value.ToString("f1"); }
        }

        private SeriesCollection runMoreColumnSeries;
        public SeriesCollection RunMoreColumnSeries
        {
            get { return runMoreColumnSeries; }
            set
            {
                if (runMoreColumnSeries != value)
                {
                    runMoreColumnSeries = value;
                    OnPropertyChanged("RunMoreColumnSeries");
                }
            }
        }

        private string[] runMoreColumnLabels;
        public string[] RunMoreColumnLabels
        {
            get { return runMoreColumnLabels; }
            set
            {
                if (runMoreColumnLabels != value)
                {
                    runMoreColumnLabels = value;
                    OnPropertyChanged("RunMoreColumnLabels");
                }
            }
        }

        public Func<double, string> RunMoreColumnFormatter
        {
            get { return value => value.ToString("f1"); }
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

        private DelegateCommand printCommand;
        public DelegateCommand PrintCommand
        {
            get
            {
                return this.printCommand ?? (this.printCommand = new DelegateCommand(() => this.Print()));
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
            var devices = (para as IList).OfType<DeviceRunInfo>();
            if (devices != null)
            {
                string[] names = devices.Select(p => p.DeviceTreeItemViewModel.Name).ToArray();
                SelectChart(names);
            }
        }

        private void MouseDoubleClick(object para)
        {
            var device = para as DeviceRunInfo;
            if (device != null && device.DiagnosticAdvice != null)
            {
#if XBAP
                MessageBox.Show(device.DiagnosticInfo + "\r\n" + device.DiagnosticAdvice, "诊断详情", MessageBoxButton.OK, MessageBoxImage.Information);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show(device.DiagnosticInfo + "\r\n" + device.DiagnosticAdvice, "诊断详情", MessageBoxButton.OK, MessageBoxImage.Information);
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

                bool newmethod = true;
                foreach (var deviceTree in deviceTrees)
                {
                    DeviceRunInfo device = new DeviceRunInfo();
                    device.DeviceTreeItemViewModel = deviceTree;
                    WaitInfo = "数据统计: " + number.ToString();
                    if (newmethod == true)
                    {
                        newmethod = await NewGetDeviceRunInfo(device, StartTime, SelectedDay);//优先使用新方法                       
                    }
                    if (newmethod == false)
                    {
                        await GetDeviceRunInfo(device, StartTime, SelectedDay);//兼容没有统计方法的服务器
                    }
                    number++;

                    DevicesView.Add(device);
                }
                Dictionary<Guid, Tuple<Guid, DateTime>> recordLabsWirelessVibration = new Dictionary<Guid, Tuple<Guid, DateTime>>();//需要修改，扩展除了无限振动卡
                foreach (var device in DevicesView)
                {
                    if (device.RecordLab == new Guid())
                    {
                        continue;
                    }
                    if (device.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
                    {
                        recordLabsWirelessVibration.Add(device.RecordLab, Tuple.Create<Guid, DateTime>(device.T_Item_Guid, device.ACQDatetime));
                    }
                }

                if (DevicesView.Count > 0)
                {
                    var waves = await _databaseComponent.GetHistoryWaveformData<D_WirelessVibrationSlot_Waveform>(DevicesView[0].DeviceTreeItemViewModel.ServerIP, recordLabsWirelessVibration);
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
                if (number > 0)
                {
                    UpdateChart();
                    GetConclusion();
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

        private void Print()
        {
            if (DeviceRunAnalyzePicGenerated != null)
            {
                DeviceRunAnalyzePicGenerated(DeviceRunAnalyze);
            }
            DeviceRunAnalyzePrintPreviewWindow previewWnd = new DeviceRunAnalyzePrintPreviewWindow("/AIC.DeviceDataPage;component/Views/DeviceRunAnalyzeFlowDocument.xaml", DeviceRunAnalyze);
            previewWnd.ShowDialog();
        }

        private void GetConclusion()
        {
            string subconclusion = string.Empty;
            bool hasconclusion = false;            

            //1
            foreach (var device in DevicesView)
            {                
                subconclusion += device.DeviceTreeItemViewModel.Name + "、";
            }
            if (subconclusion != string.Empty)
            {
                subconclusion = subconclusion.Substring(0, subconclusion.Length - 1);
            }
            this.DeviceRunAnalyze.ConclusionDevices = subconclusion;

            //2
            this.DeviceRunAnalyze.ConclusionHeader = "在" + StartTime.ToString("yyyy-MM-dd HH:mm:ss") + "至" + StartTime.AddDays(SelectedDay).ToString("yyyy-MM-dd HH:mm:ss") + "期间设备运行状态如下：";

            //3
            subconclusion = null;
            foreach (var device in DevicesView)
            {
                if (device.AlarmGrade == AlarmGrade.HighPreAlarm || device.AlarmGrade == AlarmGrade.LowPreAlarm 
                    || device.AlarmGrade == AlarmGrade.HighAlarm || device.AlarmGrade == AlarmGrade.LowAlarm
                    || device.AlarmGrade == AlarmGrade.HighDanger || device.AlarmGrade == AlarmGrade.LowDanger)
                {
                    if (subconclusion == null)
                    {
                        subconclusion = "需要关注的设备有：";
                    }
                    subconclusion += device.DeviceTreeItemViewModel.Name + "(" + device.MaxResult.ToString("f3") + device.Unit + ")" + "、";
                    hasconclusion = true; 
                }         
            }
            if (subconclusion != null)
            {
                subconclusion = subconclusion.Substring(0, subconclusion.Length - 1);
            }
            this.DeviceRunAnalyze.ConclusionAlarm = subconclusion;

            //4
            subconclusion = null;
            foreach (var device in DevicesView)
            {
                if (device.DiagnosticAdvice != null && device.DiagnosticInfo != null)
                {
                    if (subconclusion == null)
                    {
                        subconclusion = "需要重点关注的设备有：\r\n";
                    }
                    subconclusion += device.DeviceTreeItemViewModel.Name + "(" + device.DiagnosticInfo + ")\r\n" + device.DiagnosticAdvice + "\r\n";
                    hasconclusion = true;
                }
            }
            if (subconclusion != null)
            {
                subconclusion = subconclusion.Substring(0, subconclusion.Length - 2);
            }
            this.DeviceRunAnalyze.ConclusionDanger = subconclusion;

            //5
            if (hasconclusion == true)
            {
                this.DeviceRunAnalyze.ConclusionEnd = "其它设备正常运行";
            }
            else
            {
                this.DeviceRunAnalyze.ConclusionEnd += "所有设备正常运行";
            }
        }

        private void UpdateChart()
        {
            MaxColumnSeries = new SeriesCollection
                            {
                                new ColumnSeries
                                {
                                    Title = "最大值",
                                    Values = new ChartValues<ObservableValue>(DevicesView.Select(p => new ObservableValue(p.MaxResult))),
                                    Fill = new SolidColorBrush(Color.FromRgb(0x00, 0xbf, 0xff)), //蓝色     
                                    DataLabels = true
                                },
                            };
            MaxColumnLabels = DevicesView.Select(p => p.DeviceTreeItemViewModel.Name).ToArray();

            MaxMoreColumnSeries = new SeriesCollection();
            MaxMoreColumnLabels = null;
            foreach (var device in DevicesView)
            {
                if (device.RunInfo == null || device.RunInfo.Count == 0)
                {
                    continue;
                }
                MaxMoreColumnSeries.Add(new ColumnSeries
                {
                    Title = device.DeviceTreeItemViewModel.Name,
                    Values = new ChartValues<ObservableValue>(device.RunInfo.Select(p => new ObservableValue((p == null) ? 0 : p.Result))),
                    DataLabels = true
                });
                if (MaxMoreColumnLabels == null)
                {
                    MaxMoreColumnLabels = device.RunInfo.Select(p => p.Time.ToString("MM/dd")).ToArray();
                }
            }

            RunColumnSeries = new SeriesCollection
                            {
                                new ColumnSeries
                                {
                                    Title = "运行时间",
                                    Values = new ChartValues<ObservableValue>(DevicesView.Select(p => new ObservableValue(p.RunHours))),
                                    Fill = new SolidColorBrush(Color.FromRgb(0x00, 0x80, 0x00)), //绿色     
                                    DataLabels = true
                                },
                                 new ColumnSeries
                                {
                                    Title = "停止时间",
                                    Values = new ChartValues<ObservableValue>(DevicesView.Select(p => new ObservableValue(p.StopHours))),
                                    Fill = new SolidColorBrush(Color.FromRgb(0xff, 0x00, 0x00)), //红色     
                                    DataLabels = true
                                },
                            };
            RunColumnLabels = DevicesView.Select(p => p.DeviceTreeItemViewModel.Name).ToArray();

            RunMoreColumnSeries = new SeriesCollection();
            RunMoreColumnLabels = null;
            foreach (var device in DevicesView)
            {
                if (device.RunInfo == null || device.RunInfo.Count == 0)
                {
                    continue;
                }
                RunMoreColumnSeries.Add(new ColumnSeries
                {
                    Title = device.DeviceTreeItemViewModel.Name,
                    Values = new ChartValues<ObservableValue>(device.RunInfo.Select(p => new ObservableValue((p == null) ? 0 : p.RunHours))),
                    DataLabels = true
                });
                if (RunMoreColumnLabels == null)
                {
                    RunMoreColumnLabels = device.RunInfo.Select(p => p.Time.ToString("MM/dd")).ToArray();
                }
            }
        }

        private void SelectChart(string[] names)
        {
            foreach (var serie in MaxMoreColumnSeries.OfType<ColumnSeries>())
            {
                if (names.Contains(serie.Title))
                {
                    serie.Visibility = Visibility.Visible;
                }
                else
                {
                    serie.Visibility = Visibility.Collapsed;
                }
            }
            foreach (var serie in RunMoreColumnSeries.OfType<ColumnSeries>())
            {
                if (names.Contains(serie.Title))
                {
                    serie.Visibility = Visibility.Visible;
                }
                else
                {
                    serie.Visibility = Visibility.Collapsed;
                }
            }
        }

        private async Task GetDeviceRunInfo(DeviceRunInfo device, DateTime start, int day)
        {
            List<Guid> guids = new List<Guid>();
            foreach (var child in device.DeviceTreeItemViewModel.Children)
            {
                if (child is ItemTreeItemViewModel)
                {
                    ItemTreeItemViewModel itemTree = child as ItemTreeItemViewModel;
                    if (itemTree.T_Item != null && itemTree.BaseAlarmSignal is BaseWaveSignal)
                    {
                        guids.Add(itemTree.T_Item.Guid);                       
                    }
                }
            }
            
            //htzk123,这个地方需要修改，如果新接口没有问题，那么可以不修改
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
                device.Unit = midinfo.Unit;
                device.AlarmGrade = midinfo.AlarmGrade;

                device.ItemType = device.DeviceTreeItemViewModel.Children.OfType<ItemTreeItemViewModel>().Where(p => p.T_Item != null && p.T_Item.Guid == midinfo.T_Item_Guid).Select(p => p.T_Item.ItemType).FirstOrDefault();
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
                                switch (data.AlarmGrade & 0xff)
                                {
                                    case 0: break;//无效数据可能不存
                                    case 1: hours += extraInfo.NormalTimeLength / 3600; break;
                                    case 2: hours += extraInfo.PreAlarmTimeLength / 3600; break;
                                    case 3: hours += extraInfo.AlarmTimeLength / 3600; break;
                                    case 4: hours += extraInfo.DangerTimeLength / 3600; break;
                                }
                                devicerunInfo.PreAlarmCount += extraInfo.PreAlarmCount;
                                devicerunInfo.AlarmCount += extraInfo.AlarmCount;
                                devicerunInfo.DangerCount += extraInfo.DangerCount;
                            }
                        }
                    }

                    hours = (hours > 24) ? 24 : hours;
                    DateTime time = groupdata.First().ACQDatetime;
                    devicerunInfo.RunHours += hours;
                    RunInfo runinfo = devicerunInfo.RunInfo.Where(p => p.Time.Date == time.Date).First();
                    runinfo.RunHours = hours;
                    var secondmax = groupdata.Where(p => p.IsValidWave == true).OrderByDescending(p => p.AlarmGrade & 0xff).ThenByDescending(n => n.Result).Skip(1).Take(1).FirstOrDefault();
                    if (secondmax != null)//优先查有波形的
                    {
                        runinfo.Result = secondmax.Result.Value;
                        runinfo.ACQDatetime = secondmax.ACQDatetime;
                        runinfo.RecordLab = (secondmax.IsValidWave == true) ? secondmax.RecordLab.Value : new Guid();
                        runinfo.T_Item_Guid = secondmax.T_Item_Guid;
                        runinfo.RPM = (float)secondmax.RPM;
                        runinfo.Unit = secondmax.Unit;
                        runinfo.AlarmGrade = (AlarmGrade)(secondmax.AlarmGrade & 0x00ffff00);
                    }
                    else
                    {
                        secondmax = groupdata.OrderByDescending(p => p.AlarmGrade & 0xff).ThenByDescending(n => n.Result).Skip(1).Take(1).FirstOrDefault();
                        if (secondmax != null)
                        {
                            runinfo.Result = secondmax.Result.Value;
                            runinfo.ACQDatetime = secondmax.ACQDatetime;
                            runinfo.RecordLab = (secondmax.IsValidWave == true) ? secondmax.RecordLab.Value : new Guid();
                            runinfo.T_Item_Guid = secondmax.T_Item_Guid;
                            runinfo.RPM = (float)secondmax.RPM;
                            runinfo.Unit = secondmax.Unit;
                            runinfo.AlarmGrade = (AlarmGrade)(secondmax.AlarmGrade & 0x00ffff00);
                        }
                    }
                }

                RunInfo maxruninfo = devicerunInfo.RunInfo.OrderBy(p => p.Result).LastOrDefault();
                devicerunInfo.ACQDatetime = maxruninfo.ACQDatetime;
                devicerunInfo.RecordLab = maxruninfo.RecordLab;
                devicerunInfo.T_Item_Guid = maxruninfo.T_Item_Guid;
                devicerunInfo.RPM = maxruninfo.RPM;
                devicerunInfo.MaxResult = maxruninfo.Result;
                devicerunInfo.Unit = maxruninfo.Unit;
                devicerunInfo.AlarmGrade = maxruninfo.AlarmGrade;
            }

            return devicerunInfo;
        }

        private async Task<bool> NewGetDeviceRunInfo(DeviceRunInfo device, DateTime start, int day)
        {
            HashSet<Guid> guidlist = new HashSet<Guid>();
            foreach (var child in device.DeviceTreeItemViewModel.Children)
            {
                if (child is ItemTreeItemViewModel)
                {
                    ItemTreeItemViewModel itemTree = child as ItemTreeItemViewModel;
                    if (itemTree.T_Item != null && itemTree.BaseAlarmSignal is BaseWaveSignal)
                    {
                        guidlist.Add(itemTree.T_Item.Guid);
                    }
                }
            }

            var result = await _databaseComponent.GetDailyStatisticsData(device.DeviceTreeItemViewModel.ServerIP, guidlist, start.AddDays(1), start.AddDays(1 + day));
            if (result == null || result.Count == 0)
            {
                return false;
            }
            foreach (var subresult in result)
            {
                if (subresult.Value.Count < day)//数据不完整
                {
                    return false;
                }
            }
            var infoList = result.Select(p => NewGetSubDeviceRunInfo(p.Value, start, day)).OrderBy(p => p.RunHours).ToList();
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
                device.Unit = midinfo.Unit;
                device.AlarmGrade = midinfo.AlarmGrade;

                device.ItemType = device.DeviceTreeItemViewModel.Children.OfType<ItemTreeItemViewModel>().Where(p => p.T_Item != null && p.T_Item.Guid == midinfo.T_Item_Guid).Select(p => p.T_Item.ItemType).FirstOrDefault();
            }
            return true;
        }

        private DeviceRunInfo NewGetSubDeviceRunInfo(List<D_SlotStatistic> result, DateTime start, int day)
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
                foreach (var dayresult in result)
                {
                    DateTime time = dayresult.StatisticsTime.AddDays(-1);
                    double hours = 0;
                    var extraData = JsonConvert.DeserializeObject<SlotExtraData>(dayresult.ExtraData);
                    if (extraData != null)
                    {
                        M9600.Common.DTO.Device.ExtraInfo extraInfo = new M9600.Common.DTO.Device.ExtraInfo();
                        extraInfo = DictionaryToClassHelper.DicToObject<M9600.Common.DTO.Device.ExtraInfo>(extraData.StatisticsInfo.ToDictionary(p => p.Key, p => p.Value as object));

                        var runhours = (extraInfo.NormalTimeLength + extraInfo.PreAlarmTimeLength + extraInfo.AlarmTimeLength + extraInfo.DangerTimeLength) / 3600;
                        var allhours = (extraInfo.InvalidTimeLength + extraInfo.NotOKTimeLength + extraInfo.NormalTimeLength + extraInfo.PreAlarmTimeLength + extraInfo.AlarmTimeLength + extraInfo.DangerTimeLength) / 3600;

                        devicerunInfo.RunHours += runhours / allhours * 24;
                        hours = runhours / allhours * 24;
                        devicerunInfo.PreAlarmCount += extraInfo.PreAlarmCount;
                        devicerunInfo.AlarmCount += extraInfo.AlarmCount;
                        devicerunInfo.DangerCount += extraInfo.DangerCount;
                    }


                    RunInfo runinfo = devicerunInfo.RunInfo.Where(p => p.Time.Year == time.Year && p.Time.Month == time.Month && p.Time.Day == time.Day).First();
                    runinfo.RunHours = hours;
                    var secondmax = JsonConvert.DeserializeObject<SlotDiagnosticData>(dayresult.SecondaryMaxDiagnosticData);
                    if (secondmax != null)
                    {
                        runinfo.Result = secondmax.Result;
                        runinfo.ACQDatetime = secondmax.ACQDateTime;
                        runinfo.RecordLab = secondmax.RecordLab.Value;
                        runinfo.T_Item_Guid = dayresult.T_Item_Guid;
                        runinfo.RPM = (float)secondmax.RPM;
                        runinfo.Unit = secondmax.Unit;
                        runinfo.AlarmGrade = (AlarmGrade)(secondmax.AlarmGrade & 0x00ffff00);
                    }

                }

                RunInfo maxruninfo = devicerunInfo.RunInfo.OrderBy(p => p.Result).LastOrDefault();
                devicerunInfo.ACQDatetime = maxruninfo.ACQDatetime;
                devicerunInfo.RecordLab = maxruninfo.RecordLab;
                devicerunInfo.T_Item_Guid = maxruninfo.T_Item_Guid;
                devicerunInfo.RPM = maxruninfo.RPM;
                devicerunInfo.MaxResult = maxruninfo.Result;
                devicerunInfo.Unit = maxruninfo.Unit;
                devicerunInfo.AlarmGrade = maxruninfo.AlarmGrade;
            }

            return devicerunInfo;
        }
        #endregion

    }
}

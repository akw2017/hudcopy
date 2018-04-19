using AIC.Core.Models;
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
using AIC.Core.SignalModels;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Data;
using System.Diagnostics;
using System.Threading;
using AIC.ServiceInterface;
using System.Windows;
using AIC.Resources.Models;
using AIC.Core.DataModels;
using AIC.M9600.Common.DTO.Device;
using AIC.M9600.Common.SlaveDB.Generated;
using AIC.CoreType;
using System.Collections;
using AIC.Core.HardwareModels;
using AIC.Core.DiagnosticModels;
using AIC.Core.ExCommand;
using System.Windows.Controls;
using AIC.MatlabMath;
using Newtonsoft.Json;
using System.IO;
using OpenXmlPowerTools;
using System.Xml.Linq;
using AIC.DiagnosePage.Views;
using AIC.PDAPage.Models;
using AIC.Core;
using AIC.Core.DiagnosticBaseModels;
using AIC.OnLineDataPage.ViewModels.SubViewModels;
using AIC.M9600.Common.MasterDB.Generated;
using AIC.DiagnosePage.Models;

namespace AIC.DiagnosePage.ViewModels
{
    class DeviceFaultDiagnoseViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        private readonly IDatabaseComponent _databaseComponent;
        private readonly IRegionManager _regionManager;
        private readonly ILoginUserService _loginUserService;
        private readonly IDeviceDiagnoseTemplateService _deviceDiagnoseTemplateService;
        public DeviceFaultDiagnoseViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess, IDatabaseComponent databaseComponent, IRegionManager regionManager, ILoginUserService loginUserService, IDeviceDiagnoseTemplateService deviceDiagnoseTemplateService)
        {
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _signalProcess = signalProcess;
            _cardProcess = cardProcess;
            _databaseComponent = databaseComponent;
            _regionManager = regionManager;
            _loginUserService = loginUserService;
            _deviceDiagnoseTemplateService = deviceDiagnoseTemplateService;

            _view = new ListCollectionView(vibrationSignals);
            _view.GroupDescriptions.Add(new PropertyGroupDescription("OrganizationDeviceName"));//对视图进行分组
            InitTree();

            TimeDomainOnLineVM = new TimeDomainChartViewModel(null);
            FrequencyDomainOnLineVM = new FrequencyDomainChartViewModel(null);
        }
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

        private readonly ICollectionView _view;
        public ICollectionView VibrationSignalsView { get { return _view; } }

        private ObservableCollection<BaseDivfreSignal> vibrationSignals = new ObservableCollection<BaseDivfreSignal>();
        public IEnumerable<BaseAlarmSignal> VibrationSignals { get { return vibrationSignals; } }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged(() => FirstName);
                }
            }
        }      


        private ObservableCollection<DiagnoseResult> diagnoseResults = new ObservableCollection<DiagnoseResult>();
        public ObservableCollection<DiagnoseResult> DiagnoseResults
        {
            get { return diagnoseResults; }
            set
            {
                diagnoseResults = value;
                OnPropertyChanged("DiagnoseResults");
            }
        }

        private ObservableCollection<DeviceTreeItemViewModel> deviceTreeItems = new ObservableCollection<DeviceTreeItemViewModel>();
        public ObservableCollection<DeviceTreeItemViewModel> DeviceTreeItems
        {
            get { return deviceTreeItems; }
            set
            {
                deviceTreeItems = value;
                OnPropertyChanged("DeviceTreeItems");
            }
        }

        //private DeviceTreeItemViewModel selectedDeviceTreeItem;
        //public DeviceTreeItemViewModel SelectedDeviceTreeItem
        //{
        //    get { return selectedDeviceTreeItem; }
        //    set
        //    {
        //        if (selectedDeviceTreeItem != value)
        //        {
        //            selectedDeviceTreeItem = value;
        //            OnPropertyChanged("SelectedDeviceTreeItem");
        //        }
        //    }
        //}

        private TimeDomainChartViewModel timeDomainOnLineVM;
        public TimeDomainChartViewModel TimeDomainOnLineVM
        {
            get { return timeDomainOnLineVM; }
            set
            {
                if (value != timeDomainOnLineVM)
                {
                    timeDomainOnLineVM = value;
                    this.OnPropertyChanged("TimeDomainOnLineVM");
                }
            }
        }
        private FrequencyDomainChartViewModel frequencyDomainOnLineVM;
        public FrequencyDomainChartViewModel FrequencyDomainOnLineVM
        {
            get { return frequencyDomainOnLineVM; }
            set
            {
                if (value != frequencyDomainOnLineVM)
                {
                    frequencyDomainOnLineVM = value;
                    this.OnPropertyChanged("FrequencyDomainOnLineVM");
                }
            }
        }

        private ObservableCollection<ComponentNaturalFrequency> componentNaturalFrequency;
        public ObservableCollection<ComponentNaturalFrequency> ComponentNaturalFrequency
        {
            get { return componentNaturalFrequency; }
            set
            {
                if (value != componentNaturalFrequency)
                {
                    componentNaturalFrequency = value;
                    this.OnPropertyChanged("ComponentNaturalFrequency");
                }
            }
        }

        public bool isDeviceModelShow = true;
        public bool IsDeviceModelShow
        {
            get
            {
                return isDeviceModelShow;
            }
            set
            {
                isDeviceModelShow = value;
                OnPropertyChanged("IsDeviceModelShow");
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

        public ICommand mouseDoubleClickComamnd;
        public ICommand MouseDoubleClickComamnd
        {
            get
            {
                return this.mouseDoubleClickComamnd ?? (this.mouseDoubleClickComamnd = new DelegateCommand<object>(para => this.MouseDoubleClick(para)));
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

        private ICommand startDiagnosisCommand;
        public ICommand StartDiagnosisCommand
        {
            get
            {
                return this.startDiagnosisCommand ?? (this.startDiagnosisCommand = new DelegateCommand<object>(para => this.StartDiagnosis(para)));
            }
        }
        
        private ICommand saveDiagnosisResultCommand;
        public ICommand SaveDiagnosisResultCommand
        {
            get
            {
                return this.saveDiagnosisResultCommand ?? (this.saveDiagnosisResultCommand = new DelegateCommand<object>(para => this.SaveDiagnosisResult(para)));
            }
        }

        private ICommand printDiagnosisResultCommand;
        public ICommand PrintDiagnosisResultCommand
        {
            get
            {
                return this.printDiagnosisResultCommand ?? (this.printDiagnosisResultCommand = new DelegateCommand<object>(para => this.PrintDiagnosisResult(para)));
            }
        }

        private ICommand editDeviceCommand;
        public ICommand EditDeviceCommand
        {
            get
            {
                return this.editDeviceCommand ?? (this.editDeviceCommand = new DelegateCommand<object>(para => this.EditDevice(para)));
            }
        }

        private ICommand deviceChangedCommand;
        public ICommand DeviceChangedCommand
        {
            get
            {
                return this.deviceChangedCommand ?? (this.deviceChangedCommand = new DelegateCommand<object>(para => this.DeviceChanged(para)));
            }
        }
        
        #endregion

        #region 管理树
        private void InitTree()
        {
            OrganizationTreeItems = _organizationService.OrganizationTreeItems;
            //TreeExpanded();
            //DeviceDiagnoseModel.Add(new DeviceClassExamples().GetDeviceClass1(this));
            //DeviceDiagnoseModel.Add(new DeviceClassExamples().GetDeviceClass2(this));
        }

        public void Init(DeviceTreeItemViewModel device, DateTime dt)
        {
            SelectedTreeItem = device;
            SelectedTreeItem.IsSelected = true;
            StartTime = dt;
            SelectedTreeChanged(SelectedTreeItem);
            StartDiagnosis(null);
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

        #region 选择项
        private async void SelectedTreeChanged(object para)
        {
            DeviceTreeItems.Clear();
            SelectedTreeItem = para as OrganizationTreeItemViewModel;
            if (SelectedTreeItem != null)
            {
                List<DeviceTreeItemViewModel> devices = _cardProcess.GetDevices(SelectedTreeItem);
                foreach(var device in devices)
                {
                    if (device.DeviceDiagnoseComponent == null)
                    {
                        var component = await _deviceDiagnoseTemplateService.GetDeviceDiagnoseComponent(device.ServerIP, device.T_Organization.Guid);
                        device.IntiDeviceDiagnoseComponent(component);
                    }
                }
                DeviceTreeItems.AddRange(devices);
            }
        }

        private void SelectedDataGridChanged(object para)
        {
            BaseDivfreSignal sg = para as BaseDivfreSignal;
            if (sg != null)
            {
                //波形展示
                ClearFrequencyProcess(FrequencyDomainOnLineVM.Signal as BaseWaveSignal);
                FrequencyProcess(sg);
                TimeProcess(sg);
                TimeDomainOnLineVM.SetSignal(sg);
                FrequencyDomainOnLineVM.SetSignal(sg);
            }
        }

        private void MouseDoubleClick(object para)
        {
            var sg = para as BaseDivfreSignal;
            if (sg != null && sg.DiagnosticAdvice != null)
            {
#if XBAP
                MessageBox.Show(sg.DiagnosticInfo + "\r\n" + sg.DiagnosticAdvice, "诊断详情", MessageBoxButton.OK, MessageBoxImage.Information);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show(sg.DiagnosticInfo + "\r\n" + sg.DiagnosticAdvice, "诊断详情", MessageBoxButton.OK, MessageBoxImage.Information);
#endif
            }
        }

        private void ClearFrequencyProcess(BaseWaveSignal vSg)
        {
            if (vSg == null) return;
            vSg.Frequency = null;
            vSg.Amplitude = null;
            vSg.Phase = null;
        }

        private void TimeProcess(BaseWaveSignal vSg)
        {
            if (vSg == null) return;
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
        private void FrequencyProcess(BaseWaveSignal vSg)
        {
            if (vSg == null) return;
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

            var output = Algorithm.Instance.FFT2AndPhaseAction(vSg.Waveform, vSg.SamplePoint);
            if (output != null)
            {
                vSg.Amplitude = output[0].Take(length).ToArray();
                vSg.Phase = output[1].Take(length).ToArray();
            }
        }
        #endregion

        #region 数据加载
        private async Task AddData(object para)
        {
            #region 测点
            var item = SelectedTreeItem as ItemTreeItemViewModel;
            if (item != null)
            {
                if (item.T_Item != null && item.T_Item.ItemType != 0)
                {
                    var sg = await SubAddData(item);
                    if (sg != null)
                    {
                        vibrationSignals.Add(sg);
                    }
                    FirstName = (vibrationSignals.FirstOrDefault() != null) ? vibrationSignals.FirstOrDefault().OrganizationDeviceName : null;
                    return;
                }
            }

            #endregion

            #region 组织机构           
            if (SelectedTreeItem != null)
            {
                var items = _cardProcess.GetItems(SelectedTreeItem).Where(p => p.IsPaired);
                foreach (var subitem in items)
                {
                    if (subitem.T_Item != null && subitem.T_Item.ItemType != 0)
                    {
                        var sg = await SubAddData(subitem);
                        if (sg != null)
                        {
                            vibrationSignals.Add(sg);
                        }
                    }
                }
                FirstName = (vibrationSignals.FirstOrDefault() != null) ? vibrationSignals.FirstOrDefault().OrganizationDeviceName : null;
                return;
            }
            #endregion
            
        }

        private async Task<BaseDivfreSignal> SubAddData(ItemTreeItemViewModel item)
        {
            BaseDivfreSignal sg = new BaseDivfreSignal(item.T_Item.Guid);
            sg.Names = item.Names;
            List<IBaseAlarmSlot> result = new List<IBaseAlarmSlot>();

            if (item.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
            {
                result = await _databaseComponent.GetUniformHistoryData(item.T_Item.ItemType, item.ServerIP, item.T_Item.Guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade", "RPM", "RecordLab", "TPDirCode", "IsValidWave" }, StartTime.Value, StartTime.Value.AddDays(1), null, null);
            }
            else if (item.T_Item.ItemType == (int)ChannelType.WirelessScalarChannelInfo)
            {
                return null;
            }

            if (result == null || result.Count == 0)
            {
                return sg; 
            }

            if (item.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
            {
                IBaseDivfreSlot firstresult = result.OfType<IBaseDivfreSlot>().Where(p => p.IsValidWave == true && ((p.AlarmGrade & 0xff) >= 2)).FirstOrDefault();//报警有波形的点
                if (firstresult == null)
                {
                    sg.ACQDatetime = result[0].ACQDatetime;
                    sg.Result = result[0].Result;
                    sg.Unit = result[0].Unit;
                    sg.AlarmGrade = (AlarmGrade)(result[0].AlarmGrade & 0x00ffff00);
                    sg.RPM = (float)(result[0] as IBaseDivfreSlot).RPM;
                    sg.TPDirCode = (result[0] as IBaseDivfreSlot).TPDirCode;
                    return sg;
                }
                sg.ACQDatetime = firstresult.ACQDatetime;
                sg.Result = firstresult.Result;
                sg.Unit = firstresult.Unit;
                sg.AlarmGrade = (AlarmGrade)(firstresult.AlarmGrade & 0x00ffff00);
                sg.RPM = (float)firstresult.RPM;
                sg.TPDirCode = firstresult.TPDirCode;

                Dictionary<Guid, Tuple<Guid, DateTime>> recordLabs = new Dictionary<Guid, Tuple<Guid, DateTime>>();
                recordLabs.Add(firstresult.RecordLab.Value, Tuple.Create<Guid, DateTime>(item.T_Item.Guid, firstresult.ACQDatetime));
                var waves = await _databaseComponent.GetHistoryWaveformData<D_WirelessVibrationSlot_Waveform>(item.ServerIP, recordLabs);
                if (waves != null && waves.Count > 0)
                {
                    IWaveformData waveform = new WirelessVibrationSlotData_Waveform();
                    waveform.WaveData = waves[0].WaveData;
                    waveform.WaveUnit = waves[0].WaveUnit;
                    waveform.SampleFre = waves[0].SampleFre;
                    waveform.SamplePoint = waves[0].SamplePoint;
                    firstresult.Waveform = waveform;
                    sg.SampleFre = firstresult.Waveform.SampleFre.Value;
                    sg.SamplePoint = firstresult.Waveform.SamplePoint.Value;
                    sg.Waveform = Algorithm.ByteToSingle(firstresult.Waveform.WaveData);
                    sg.Bytes = firstresult.Waveform.WaveData;
                }
            }
            return sg;
        }

        #endregion

        #region 故障诊断
        private async void StartDiagnosis(object para)
        {
            DiagnoseResults.Clear();
            vibrationSignals.Clear();
            try
            {
                WaitInfo = "故障诊断中";
                Status = ViewModelStatus.Querying;
                if (SelectedTreeItem is ItemTreeItemViewModel)
                {
                    ItemTreeItemViewModel itemTree = SelectedTreeItem as ItemTreeItemViewModel;
                    await AddData(itemTree);
                    DiagnoseResult diagnoseResult = FilterData(itemTree, vibrationSignals);
                    DiagnoseResults.Add(diagnoseResult);                    
                }
                else if (SelectedTreeItem is DeviceTreeItemViewModel)
                {
                    DeviceTreeItemViewModel deviceTree = SelectedTreeItem as DeviceTreeItemViewModel;
                    if (deviceTree != null)
                    {
                        await AddData(deviceTree);
                        DiagnoseResult diagnoseResult = FilterData(deviceTree, vibrationSignals);
                        DiagnoseResults.Add(diagnoseResult);                       
                    }
                }
                else if (SelectedTreeItem is OrganizationTreeItemViewModel)
                {
                    OrganizationTreeItemViewModel oragnizationTree = SelectedTreeItem as OrganizationTreeItemViewModel;
                    if (oragnizationTree != null)
                    {
                        await AddData(oragnizationTree);

                        var deviceTrees = _cardProcess.GetDevices(oragnizationTree);
                        foreach (var deviceTree in deviceTrees)
                        { 
                            var devicevibrationSignals = vibrationSignals.Where(p => p.DeviceName == deviceTree.Name);                       
                            DiagnoseResult diagnoseResult = FilterData(deviceTree, devicevibrationSignals.ToArray());
                            DiagnoseResults.Add(diagnoseResult);
                        }                        
                    }
                }
                
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("设备诊断", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }

        }

        private DiagnoseResult FilterData(OrganizationTreeItemViewModel organizationTree, IList<BaseDivfreSignal> sgs)
        {
            DiagnoseResult diagnoseResult = new DiagnoseResult();
            foreach (var sg in sgs)
            {
                if (sg.Result == null)
                {
                    sg.DiagnosticInfo = "无数据";
                }
                else if (sg.Bytes == null)
                {
                    sg.DiagnosticInfo = "正常";
                }
                else
                {
                    sg.DiagnosticInfo = "异常";
                }
            }

            var waveSignals = sgs.Where(p => p.Bytes != null).ToArray();
            if (waveSignals.Length != 0)
            {
                var rpm = waveSignals[0].RPM;
                if (waveSignals.Any(p => p.RPM != rpm))
                {
                    diagnoseResult.Error = "设备的测点转速不一致";
                    diagnoseResult.SetDiagnosticResult(waveSignals[0].DeviceName);
                    return null;
                }
                else
                {
                    if (organizationTree is ItemTreeItemViewModel)
                    {
                        var devicediagnosis = GetItemDiagnosticInfo(sgs);
                        string json = JsonConvert.SerializeObject(devicediagnosis);
                        StringBuilder condition = new StringBuilder(json);
                        var conculsion = Algorithm.Instance.GetDiagnosisConclusionAction(condition);
                        diagnoseResult = JsonConvert.DeserializeObject<DiagnoseResult>(conculsion);
                        diagnoseResult.SetDiagnosticResult(organizationTree.Name);
                        return diagnoseResult;
                    }
                    else if (organizationTree is DeviceTreeItemViewModel)
                    {
                        var deviceTree = organizationTree as DeviceTreeItemViewModel;
                        DeviceDiagnoseInfo devicediagnosis = GetDeviceDiagnoseInfo(deviceTree, rpm, sgs);
                        string json = JsonConvert.SerializeObject(devicediagnosis);
                        StringBuilder condition = new StringBuilder(json);
                        var conculsion = Algorithm.Instance.GetDiagnosisConclusionAction(condition);
                        diagnoseResult = JsonConvert.DeserializeObject<DiagnoseResult>(conculsion);
                        diagnoseResult.SetDiagnosticResult(organizationTree.Name);

                        deviceTree.DeviceDiagnoseComponent.ComponentNaturalFrequency.Clear();
                        deviceTree.DeviceDiagnoseComponent.ComponentNaturalFrequency.AddRange(CalculateNatureFrequency(devicediagnosis));//获取特征频率
                        return diagnoseResult;
                    }
                }
            }
            if (sgs.Count >= 1)
            {
                diagnoseResult.SetDiagnosticResult(sgs[0].DeviceName);
            }            
            return diagnoseResult;
        }     

        private DeviceDiagnoseInfo GetDeviceDiagnoseInfo(DeviceTreeItemViewModel device, float rpm, IList<BaseDivfreSignal> sgs)
        {
            if (device != null && device.DeviceDiagnoseComponent != null && device.DeviceDiagnoseComponent.Component != null)
            {
                DeviceDiagnoseClass deviceDiagnosisClass = device.DeviceDiagnoseComponent.Component;

                DeviceDiagnoseInfo devicediagnosis = new DeviceDiagnoseInfo();
                devicediagnosis.HeadDivFreThreshold = deviceDiagnosisClass.HeadDivFreThreshold;//
                devicediagnosis.FreDiagnosisSetupInterval = deviceDiagnosisClass.FreDiagnosisSetupInterval;//
                devicediagnosis.FrePeakFilterInterval = deviceDiagnosisClass.FrePeakFilterInterval;//
                devicediagnosis.IsDeviceDiagnosis = deviceDiagnosisClass.IsDeviceDiagnosis;//
                devicediagnosis.KurtosisIndexThreshold = deviceDiagnosisClass.KurtosisIndexThreshold;//
                devicediagnosis.MeanThreshold = deviceDiagnosisClass.MeanThreshold;//htzk123 界面是否漏了这个字段
                devicediagnosis.PeakIndexThreshold = deviceDiagnosisClass.PeakIndexThreshold;//
                devicediagnosis.PeakThreshold = deviceDiagnosisClass.PeakThreshold;//htzk123 界面是否漏了这个字段
                devicediagnosis.PulseIndexThreshold = deviceDiagnosisClass.PulseIndexThreshold;//
                devicediagnosis.RMSThreshold = deviceDiagnosisClass.RMSThreshold;//htzk123 界面是否漏了这个字段
                devicediagnosis.DiagnosisMethod = (int)deviceDiagnosisClass.DiagnosisMethod;//
                devicediagnosis.IsFaultprobability = deviceDiagnosisClass.IsFaultprobability;//

                #region 设备诊断
                if (devicediagnosis.IsDeviceDiagnosis == true)
                {
                    List<ShaftInfo> shaftInfoList = new List<ShaftInfo>();
                    foreach (var shaftComponent in deviceDiagnosisClass.Shafts)
                    {
                        ShaftComponent shaftProxy = shaftComponent as ShaftComponent;
                        ShaftInfo shaftInfo = new ShaftInfo();
                        shaftInfo.Name = shaftProxy.Name;
                        if (shaftProxy.Component != null)
                        {
                            shaftInfo.DeltaRPM = shaftProxy.Component.DeltaRPM;
                            shaftInfo.RPM = shaftProxy.Component.DefaultRPM;
                            shaftInfo.RPMCoeff = shaftProxy.Component.RPMCoeff;
                            shaftInfo.IsSlidingBearing = shaftProxy.Component.IsSlidingBearing;

                            var bearingProxys = shaftProxy.Component.MachComponents.OfType<BearingComponent>().ToArray();
                            if (bearingProxys.Length > 0)
                            {
                                shaftInfo.BearingInfos = new BearingInfo[bearingProxys.Length];
                                for (int j = 0; j < bearingProxys.Length; j++)
                                {
                                    if (bearingProxys[j].Component != null)
                                    {
                                        shaftInfo.BearingInfos[j] = new BearingInfo
                                        {
                                            Name = bearingProxys[j].Name,
                                            ContactAngle = bearingProxys[j].Component.ContactAngle,
                                            InnerRingDiameter = bearingProxys[j].Component.InnerRingDiameter,
                                            NumberOfColumns = bearingProxys[j].Component.NumberOfColumns,
                                            NumberOfRoller = bearingProxys[j].Component.NumberOfRoller,
                                            OuterRingDiameter = bearingProxys[j].Component.OuterRingDiameter,
                                            PitchDiameter = bearingProxys[j].Component.PitchDiameter,
                                            RollerDiameter = bearingProxys[j].Component.RollerDiameter,
                                        };
                                    }
                                }
                            }


                            var gears = shaftProxy.Component.MachComponents.OfType<GearComponent>().ToArray();
                            if (gears.Length > 0)
                            {
                                shaftInfo.GearInfos = new GearInfo[gears.Length];
                                for (int j = 0; j < gears.Length; j++)
                                {
                                    shaftInfo.GearInfos[j] = new GearInfo
                                    {
                                        Name = gears[j].Name,
                                        TeethNumber = gears[j].Component.TeethNumber
                                    };
                                }
                            }

                            var belts = shaftProxy.Component.MachComponents.OfType<BeltComponent>().ToArray();
                            if (belts.Length > 0)
                            {
                                shaftInfo.BeltInfos = new BeltInfo[belts.Length];
                                for (int j = 0; j < belts.Length; j++)
                                {
                                    shaftInfo.BeltInfos[j] = new BeltInfo
                                    {
                                        Name = belts[j].Name,
                                        BeltLength = belts[j].Component.BeltLength,
                                        PulleyDiameter = belts[j].Component.PulleyDiameter,
                                    };
                                }
                            }

                            var impellers = shaftProxy.Component.MachComponents.OfType<ImpellerComponent>().ToArray();
                            if (impellers.Length > 0)
                            {
                                shaftInfo.ImpellerInfos = new ImpellerInfo[impellers.Length];
                                for (int j = 0; j < impellers.Length; j++)
                                {
                                    shaftInfo.ImpellerInfos[j] = new ImpellerInfo
                                    {
                                        Name = impellers[j].Name,
                                        VaneNumber = impellers[j].Component.NumberOfBlades,
                                    };
                                }
                            }

                            var motors = shaftProxy.Component.MachComponents.OfType<MotorComponent>().ToArray();
                            if (motors.Length > 0)
                            {
                                shaftInfo.MotorInfos = new MotorInfo[motors.Length];
                                for (int j = 0; j < motors.Length; j++)
                                {
                                    shaftInfo.MotorInfos[j] = new MotorInfo
                                    {
                                        Name = motors[j].Name,
                                        LineFrequency = motors[j].Component.LineFrequency,
                                        Poles = motors[j].Component.Poles,
                                        RotorBars = motors[j].Component.RotorBars,
                                        StatorCoils = motors[j].Component.StatorCoils,
                                        WindingSlots = motors[j].Component.WindingSlots,
                                        SCRs = motors[j].Component.SCRs,
                                        MotorType = (int)motors[j].Component.MotorType,
                                    };
                                }
                            }

                            var negationDivFreStrategies = shaftProxy.Component.NegationDivFreStrategies.ToArray();
                            if (negationDivFreStrategies.Length > 0)
                            {
                                shaftInfo.NegationDivFreStrategies = new NegationDivFreStrategyInfo[negationDivFreStrategies.Length];
                                for (int j = 0; j < negationDivFreStrategies.Length; j++)
                                {
                                    shaftInfo.NegationDivFreStrategies[j] = new NegationDivFreStrategyInfo
                                    {
                                        Code = negationDivFreStrategies[j].Code,
                                        Name = negationDivFreStrategies[j].Fault,
                                        RelativeX = negationDivFreStrategies[j].RelativeX,
                                        RelativeY = negationDivFreStrategies[j].RelativeY,
                                        RelativeZ = negationDivFreStrategies[j].RelativeZ
                                    };
                                }
                            }

                            var naturalFres = shaftProxy.Component.NaturalFres.ToArray();
                            if (naturalFres.Length > 0)
                            {
                                foreach (var naturalFreGroup in naturalFres.GroupBy(o => o.Mode))
                                {
                                    if (naturalFreGroup.Key == NaturalFreMode.Additive)
                                    {
                                        var addNaturalFres = naturalFreGroup.ToArray();
                                        shaftInfo.AddNaturalFres = new NaturalFreInfo[addNaturalFres.Length];
                                        for (int j = 0; j < addNaturalFres.Length; j++)
                                        {
                                            shaftInfo.AddNaturalFres[j] = new NaturalFreInfo
                                            {
                                                DivFreType = (int)addNaturalFres[j].DivFre,
                                                Name = addNaturalFres[j].Fault,
                                                Value1 = addNaturalFres[j].Value1,
                                                Value2 = addNaturalFres[j].Value2,
                                                Proposal = addNaturalFres[j].Proposal,
                                                Harm = addNaturalFres[j].Harm,
                                            };
                                        }
                                    }
                                    else if (naturalFreGroup.Key == NaturalFreMode.Subtractive)
                                    {
                                        var subNaturalFres = naturalFreGroup.ToArray();
                                        shaftInfo.DeleteNaturalFres = new NaturalFreInfo[subNaturalFres.Length];
                                        for (int j = 0; j < subNaturalFres.Length; j++)
                                        {
                                            shaftInfo.DeleteNaturalFres[j] = new NaturalFreInfo
                                            {
                                                DivFreType = (int)subNaturalFres[j].DivFre,
                                                Name = subNaturalFres[j].Fault,
                                                Value1 = subNaturalFres[j].Value1,
                                                Value2 = subNaturalFres[j].Value2
                                            };
                                        }
                                    }
                                }
                            }

                            var dvFreThresholdProportiones = shaftProxy.Component.DivFreThresholdProportiones.ToArray();
                            if (dvFreThresholdProportiones.Length > 0)
                            {
                                shaftInfo.DivFreThresholdProportions = new DivFreThresholdProportionInfo[dvFreThresholdProportiones.Length];
                                for (int j = 0; j < dvFreThresholdProportiones.Length; j++)
                                {
                                    shaftInfo.DivFreThresholdProportions[j] = new DivFreThresholdProportionInfo
                                    {
                                        DivFreType = (int)dvFreThresholdProportiones[j].DivFre,
                                        Name = dvFreThresholdProportiones[j].Fault,
                                        Proportion = dvFreThresholdProportiones[j].Proportion,
                                        Threshold = dvFreThresholdProportiones[j].Threshold,
                                        Value1 = dvFreThresholdProportiones[j].Value1,
                                        Value2 = dvFreThresholdProportiones[j].Value2
                                    };
                                }
                            }

                            shaftInfo.FilterType = (int)shaftProxy.Component.FilterType;
                            shaftInfo.BindRPMForFilter = shaftProxy.Component.BindRPMForFilter;
                            var bpFilter = shaftProxy.Component.DgBandPassFilter;
                            if (bpFilter != null)
                            {
                                shaftInfo.BandPassFilter = new BandPassFilterInfo()
                                {
                                    PassbandAttenuationDB = bpFilter.PassbandAttenuationDB,
                                    StopbandAttenuationDB = bpFilter.StopbandAttenuationDB,
                                    BPPassbandFreLow = bpFilter.BPPassbandFreLow,
                                    BPPassbandFreHigh = bpFilter.BPPassbandFreHigh,
                                    BPStopbandFreLow = bpFilter.BPStopbandFreLow,
                                    BPStopbandFreHigh = bpFilter.BPStopBandFreHigh,
                                };
                            }

                            var hpFilter = shaftProxy.Component.DgHighPassFilter;
                            if (hpFilter != null)
                            {
                                shaftInfo.HighPassFilter = new HighPassFilterInfo()
                                {
                                    PassbandAttenuationDB = hpFilter.PassbandAttenuationDB,
                                    StopbandAttenuationDB = hpFilter.StopbandAttenuationDB,
                                    PassbandFre = hpFilter.PassbandFre,
                                    StopbandFre = hpFilter.StopbandFre,
                                };
                            }

                            var lpFilter = shaftProxy.Component.DgLowPassFilter;
                            if (lpFilter != null)
                            {
                                shaftInfo.LowPassFilter = new LowPassFilterInfo()
                                {
                                    PassbandAttenuationDB = lpFilter.PassbandAttenuationDB,
                                    StopbandAttenuationDB = lpFilter.StopbandAttenuationDB,
                                    PassbandFre = lpFilter.PassbandFre,
                                    StopbandFre = lpFilter.StopbandFre,
                                };
                            }

                            var items = shaftProxy.Component.AllotItems;
                            if (items != null && sgs != null)
                            {
                                List<TestPointGroupInfo> tpGroupInfos = new List<TestPointGroupInfo>();
                                for (int i = 0; i < items.Count; i++)
                                {
                                    TestPointGroupInfo tpGroupInfo = new TestPointGroupInfo();
                                    for (int j = 0; j < 3; j++)
                                    {
                                        var sg = sgs.Where(p => p.Guid == items[i].Guid).FirstOrDefault();
                                        if (sg != null)
                                        {
                                            if (j == 0)
                                            {
                                                if (sg.Bytes != null)
                                                {
                                                    tpGroupInfo.X = new TestPointInfo() { Name = sg.ItemName, SampleFre = sg.SampleFre, SamplePoint = sg.SamplePoint, VData = Convert.ToBase64String(sg.Bytes) };
                                                }
                                            }
                                            else if (j == 1)
                                            {
                                                if (sg.Bytes != null)
                                                {
                                                    tpGroupInfo.Y = new TestPointInfo() { Name = sg.ItemName, SampleFre = sg.SampleFre, SamplePoint = sg.SamplePoint, VData = Convert.ToBase64String(sg.Bytes) };
                                                }
                                            }
                                            else if (j == 2)
                                            {
                                                if (sg.Bytes != null)
                                                {
                                                    tpGroupInfo.Z = new TestPointInfo() { Name = sg.ItemName, SampleFre = sg.SampleFre, SamplePoint = sg.SamplePoint, VData = Convert.ToBase64String(sg.Bytes) };
                                                }
                                            }
                                        }
                                        i++;
                                        if (i >= items.Count)
                                        {
                                            break;
                                        }
                                    }
                                    tpGroupInfos.Add(tpGroupInfo);
                                }
                                shaftInfo.TestPointGroupInfos = tpGroupInfos.ToArray();
                            }
                            shaftInfoList.Add(shaftInfo);
                        }
                    }
                    devicediagnosis.ShaftInfos = shaftInfoList.ToArray();
                }
                #endregion
                else
                {
                    ShaftInfo shaftInfo = new ShaftInfo();
                    shaftInfo.Name = device.Name;
                    shaftInfo.RPM = rpm;
                    shaftInfo.RPMCoeff = 1;
                    shaftInfo.IsSlidingBearing = false;
                    devicediagnosis.ShaftInfos = new ShaftInfo[] { shaftInfo };

                    SingleTestPointInfo singleTestPointInfo = new SingleTestPointInfo();
                    singleTestPointInfo.RPM = rpm;
                    singleTestPointInfo.ShaftName = device.Name;
                    
                    var items = deviceDiagnosisClass.UnAllotItems;
                    if (items != null && items.Count > 0 && sgs != null)
                    {
                        TestPointGroupInfo testPointGroupInfo = new TestPointGroupInfo();
                        int i = 0;
                        for (int j = 0; j < 3; j++)
                        {
                            var sg = sgs.Where(p => p.Guid == items[i].Guid).FirstOrDefault();
                            if (sg != null)
                            {
                                if (j == 0)
                                {
                                    if (sg.Bytes != null)
                                    {
                                        testPointGroupInfo.X = new TestPointInfo() { Name = sg.ItemName, SampleFre = sg.SampleFre, SamplePoint = sg.SamplePoint, VData = Convert.ToBase64String(sg.Bytes) };
                                    }
                                }
                                else if (j == 1)
                                {
                                    if (sg.Bytes != null)
                                    {
                                        testPointGroupInfo.Y = new TestPointInfo() { Name = sg.ItemName, SampleFre = sg.SampleFre, SamplePoint = sg.SamplePoint, VData = Convert.ToBase64String(sg.Bytes) };
                                    }
                                }
                                else if (j == 2)
                                {
                                    if (sg.Bytes != null)
                                    {
                                        testPointGroupInfo.Z = new TestPointInfo() { Name = sg.ItemName, SampleFre = sg.SampleFre, SamplePoint = sg.SamplePoint, VData = Convert.ToBase64String(sg.Bytes) };
                                    }
                                }
                            }
                            i++;
                            if (i >= items.Count)
                            {
                                break;
                            }

                        }
                        singleTestPointInfo.TestPointGroupInfo = testPointGroupInfo;

                    }
                    devicediagnosis.SingleTestPointInfo = singleTestPointInfo;
                }
                return devicediagnosis;
            }
            else
            {
                return null;
            }
        }

        private DeviceDiagnoseInfo GetItemDiagnosticInfo(IList<BaseDivfreSignal> sgs)
        {
            var sg = sgs.OrderByDescending(p => p.Result).FirstOrDefault();

            DeviceDiagnoseInfo devicediagnosis = new DeviceDiagnoseInfo();
            devicediagnosis.HeadDivFreThreshold = 0.1;
            devicediagnosis.FreDiagnosisSetupInterval = 5;
            devicediagnosis.FrePeakFilterInterval = 5;
            devicediagnosis.IsDeviceDiagnosis = false;
            devicediagnosis.KurtosisIndexThreshold = 50;
            devicediagnosis.MeanThreshold = 150;
            devicediagnosis.PeakIndexThreshold = 50;
            devicediagnosis.PeakThreshold = 200;
            devicediagnosis.PulseIndexThreshold = 50;
            devicediagnosis.RMSThreshold = 150;

            ShaftInfo shaftInfo = new ShaftInfo();
            shaftInfo.Name = sg.DeviceName;
            shaftInfo.RPM = sg.RPM;
            shaftInfo.RPMCoeff = 1;
            shaftInfo.IsSlidingBearing = false;        

            SingleTestPointInfo singleTestPointInfo = new SingleTestPointInfo();
            singleTestPointInfo.RPM = sg.RPM;
            singleTestPointInfo.ShaftName = sg.DeviceName;          
            singleTestPointInfo.TestPointGroupInfo = new TestPointGroupInfo()
            {
                X = new TestPointInfo() { Name = sg.ItemName, SampleFre = sg.SampleFre, SamplePoint = sg.SamplePoint, VData = Convert.ToBase64String(sg.Bytes) },
                Y = new TestPointInfo() { Name = "测点SY", SampleFre = 2560, SamplePoint = 1024 },
                Z = new TestPointInfo() { Name = "测点SZ", SampleFre = 2560, SamplePoint = 1024 },
            };

            devicediagnosis.ShaftInfos = new ShaftInfo[] { shaftInfo };
            devicediagnosis.SingleTestPointInfo = singleTestPointInfo;
                           
            return devicediagnosis;
        }

        private IEnumerable<ComponentNaturalFrequency> CalculateNatureFrequency(DeviceDiagnoseInfo deviceInfo)
        {
            List<ComponentNaturalFrequency> list = new List<ComponentNaturalFrequency>();
            if (deviceInfo.ShaftInfos != null)
            {
                foreach (var shaftInfo in deviceInfo.ShaftInfos)
                {
                    double rotaryFrequency = shaftInfo.RPM * shaftInfo.RPMCoeff / 60;
                    list.Add(new ComponentNaturalFrequency(shaftInfo.Name + "_转频", rotaryFrequency));
                    if (shaftInfo.BearingInfos != null)
                    {
                        foreach (var bearing in shaftInfo.BearingInfos)
                        {
                            list.Add(new ComponentNaturalFrequency(bearing.Name + "_内环特征频率", Algorithm.Instance.GetBearingInnerRingFrequencyAction(bearing.PitchDiameter, bearing.NumberOfRoller, bearing.RollerDiameter, bearing.ContactAngle)));
                            list.Add(new ComponentNaturalFrequency(bearing.Name + "_外环特征频率", Algorithm.Instance.GetBearingOuterRingFrequencyAction(bearing.PitchDiameter, bearing.NumberOfRoller, bearing.RollerDiameter, bearing.ContactAngle)));
                            list.Add(new ComponentNaturalFrequency(bearing.Name + "_滚动体特征频率", Algorithm.Instance.GetBearingRollerFrequencyAction(bearing.PitchDiameter, bearing.NumberOfRoller, bearing.RollerDiameter, bearing.ContactAngle)));
                            list.Add(new ComponentNaturalFrequency(bearing.Name + "_保持架特征频率", Algorithm.Instance.GetBearingMaintainsFrequencyAction(bearing.PitchDiameter, bearing.NumberOfRoller, bearing.RollerDiameter, bearing.ContactAngle)));
                        }
                    }

                    if (shaftInfo.GearInfos != null)
                    {
                        foreach (var gear in shaftInfo.GearInfos)
                        {
                            list.Add(new ComponentNaturalFrequency(gear.Name + "_啮合频率", gear.TeethNumber * rotaryFrequency));
                        }
                    }
                }
            }
            return list;
        }
        #endregion

        #region 报表
        private void SaveDiagnosisResult(object para)
        {
            string exception = string.Empty;
            try
            {
                string dir = System.AppDomain.CurrentDomain.BaseDirectory + "MyData\\Diagnosis";
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();
                dialog.Title = "请选择保存诊断报表文件夹";
                dialog.FileName = dir + "\\诊断报告.docx";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileInfo templateDoc = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\诊断报告模板.docx");
                    WmlDocument wmlDoc = new WmlDocument(templateDoc.FullName);

                    var data = new XElement("Devices");
                    var time = new XElement("DateTime", StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    data.Add(time);
                    foreach (var result in DiagnoseResults)
                    {
                        bool hasdescription = false;
                        if (result.description.Count() > 0)
                        {
                            hasdescription = true;
                        }
                        var report = new XElement("Device",
                            new XElement("Name", result.Name),
                            new XElement("State", result.Result),
                            new XElement("HasDescription", hasdescription));
                        var diagnoseResult = new XElement("DiagnoseResult");
                        foreach (var diagnoseFault in result.description)
                        {
                            diagnoseResult.Add(new XElement("DiagnoseFault",
                                new XElement("Code", diagnoseFault.Code),
                                new XElement("Fault", diagnoseFault.Fault),
                                new XElement("Harm", diagnoseFault.Harm),
                                new XElement("Proposal", diagnoseFault.Proposal)));
                        }
                        report.Add(diagnoseResult);

                        data.Add(report);
                    }
                    bool templateError;
                    WmlDocument wmlAssembledDoc = DocumentAssembler.AssembleDocument(wmlDoc, data, out templateError);
                    if (templateError)
                    {
#if XBAP
                        MessageBox.Show("请选中要查询的组织机构", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                        Xceed.Wpf.Toolkit.MessageBox.Show("报告模板存在错误，详情请查看诊断报告", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                    }
                   
                    FileInfo assembledDoc = new FileInfo(dialog.FileName);
                    wmlAssembledDoc.SaveAs(assembledDoc.FullName);
                    System.Diagnostics.Process.Start("explorer.exe", Path.GetFullPath(dialog.FileName));
                }
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
            }
            if (!string.IsNullOrEmpty(exception))
            {
#if XBAP
                MessageBox.Show("请选中要查询的组织机构", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("生成报告失败，详情请查看C:\\AIC\\SFD\\诊断报告.docx", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif

            }
        }

        private void PrintDiagnosisResult(object para)
        {
            DeviceFaultDiagnosePrintPreviewWindow previewWnd = new DeviceFaultDiagnosePrintPreviewWindow("/AIC.DiagnosePage;component/Views/DeviceFaultDiagnoseFlowDocument.xaml", DiagnoseResults.ToArray(), new DeviceFaultDiagnoseDocumentRenderer());
            previewWnd.ShowDialog();
        }
        #endregion

        #region 编辑模型

        private void EditDevice(object para)
        {
            DeviceTreeItemViewModel device = para as DeviceTreeItemViewModel;
            if (device != null)
            {
                EditDeviceComponentsView view = _loginUserService.GotoTab<EditDeviceComponentsView>("MenuEditDeviceComponents") as EditDeviceComponentsView;
                if (view != null)
                {
                    view.GotoDevice(device);
                }
            }
        }

        private void DeviceChanged(object para)
        {

        }
        #endregion
    }
}
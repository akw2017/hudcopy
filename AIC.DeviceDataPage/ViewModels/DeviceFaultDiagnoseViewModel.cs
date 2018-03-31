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
using AIC.OnLineDataPage.ViewModels.SubViewModels;
using AIC.MatlabMath;
using AIC.DeviceDataPage.Models;
using Newtonsoft.Json;

namespace AIC.DeviceDataPage.ViewModels
{
    class DeviceFaultDiagnoseViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        private readonly IDatabaseComponent _databaseComponent;

        public DeviceFaultDiagnoseViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess, IDatabaseComponent databaseComponent)
        {
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _signalProcess = signalProcess;
            _cardProcess = cardProcess;
            _databaseComponent = databaseComponent;

            _view = new ListCollectionView(vibrationSignals);
            _view.GroupDescriptions.Add(new PropertyGroupDescription("OrganizationDeviceName"));//对视图进行分组
            InitTree();
            //test();
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
        #endregion

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

        #region 选择项
        private void SelectedTreeChanged(object para)
        {
            SelectedTreeItem = para as OrganizationTreeItemViewModel;
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
                    DiagnoseResult diagnoseResult = FilterData(vibrationSignals);
                    DiagnoseResults.Add(diagnoseResult);                    
                }
                else if (SelectedTreeItem is DeviceTreeItemViewModel)
                {
                    DeviceTreeItemViewModel deviceTree = SelectedTreeItem as DeviceTreeItemViewModel;
                    if (deviceTree != null)
                    {
                        await AddData(deviceTree);
                        DiagnoseResult diagnoseResult = FilterData(vibrationSignals);
                        DiagnoseResults.Add(diagnoseResult);                       
                    }
                }
                else if (SelectedTreeItem is OrganizationTreeItemViewModel)
                {
                    OrganizationTreeItemViewModel oragnizationTree = SelectedTreeItem as OrganizationTreeItemViewModel;
                    if (oragnizationTree != null)
                    {
                        await AddData(oragnizationTree);
                        var devicevibrationSignals = vibrationSignals.GroupBy(p => p.DeviceName);
                        foreach (var sgs in devicevibrationSignals)
                        {
                            DiagnoseResult diagnoseResult = FilterData(sgs.ToArray());
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

        private DiagnoseResult FilterData(IList<BaseDivfreSignal> sgs)
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
                    diagnoseResult.Result = waveSignals[0].DeviceName + "数据有误";
                    diagnoseResult.Error = "设备的测点转速不一致";
                    return null;
                }
                var firstwavesignal = waveSignals.OrderByDescending(p => p.Result).FirstOrDefault();
                diagnoseResult =  GetDiagnosticInfo(firstwavesignal);
                diagnoseResult.Result = waveSignals[0].DeviceName + "诊断";
                return diagnoseResult;
            }
            if (sgs.Count == 1)
            {
                diagnoseResult.Result = sgs[0].ItemName + "正常";
            }
            else if (sgs.Count > 1)
            {
                diagnoseResult.Result = sgs[0].DeviceName + "正常";
            }
            return diagnoseResult;
        }

        private DiagnoseResult GetDiagnosticInfo(BaseDivfreSignal sg)
        {
            DeviceDiagnosisInfo devicediagnosis = new DeviceDiagnosisInfo();
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
            shaftInfo.BearingInfos = new BearingInfo[]
            {
                new BearingInfo() {Name="轴承1", ContactAngle = 0.0, InnerRingDiameter = 10.0, NumberOfColumns = 1, NumberOfRoller = 1, OuterRingDiameter = 100.0, PitchDiameter = 20.0, RollerDiameter = 15.0 },
                new BearingInfo() {Name="轴承2", ContactAngle = 0.0, InnerRingDiameter = 10.0, NumberOfColumns = 1, NumberOfRoller = 1, OuterRingDiameter = 100.0, PitchDiameter = 20.0, RollerDiameter = 15.0 },
            };
            shaftInfo.GearInfos = new GearInfo[]
            {
                new GearInfo { Name="齿轮1", TeethNumber = 10 },
                new GearInfo { Name="齿轮2", TeethNumber = 10 },
            };
            shaftInfo.BeltInfos = new BeltInfo[]
            {
                new BeltInfo {Name="皮带1", BeltLength = 100, PulleyDiameter = 10 },
                new BeltInfo {Name="皮带2", BeltLength = 100, PulleyDiameter = 10 },
            };
            shaftInfo.ImpellerInfos = new ImpellerInfo[]
            {
                new ImpellerInfo {Name="叶轮1", VaneNumber = 15 } ,
                new ImpellerInfo {Name="叶轮2", VaneNumber = 15 },
            };
            shaftInfo.AddNaturalFres = new NaturalFreInfo[]
            {
                new NaturalFreInfo() { DivFreType = 0, Name = "不平衡", Value1 = 1 },
                new NaturalFreInfo() { DivFreType = 0, Name = "不对中", Value1 = 1 },
            };
            shaftInfo.DeleteNaturalFres = new NaturalFreInfo[]
            {
                new NaturalFreInfo() { DivFreType = 0, Name = "不平衡", Value1 = 1 },
                new NaturalFreInfo() { DivFreType = 0, Name = "不对中", Value1 = 1 },
            };
            shaftInfo.DivFreThresholdProportions = new DivFreThresholdProportionInfo[]
            {
                new DivFreThresholdProportionInfo() { DivFreType = 0, Name = "不平衡", Proportion = 1.0, Threshold = 0.1, Value1 = 1.0, Value2 = 2.0 },
                new DivFreThresholdProportionInfo() { DivFreType = 0, Name = "不对中", Proportion = 1.0, Threshold = 0.1, Value1 = 1.0, Value2 = 2.0 },
            };
            shaftInfo.NegationDivFreStrategies = new NegationDivFreStrategyInfo[]
            {
                new NegationDivFreStrategyInfo { Code = 1, Name = "不平衡", RelativeX = 1, RelativeY = 1, RelativeZ = 2 },
                new NegationDivFreStrategyInfo { Code = 1, Name = "不对中", RelativeX = 1, RelativeY = 1, RelativeZ = 2 }
            };
            shaftInfo.TestPointGroupInfos = new TestPointGroupInfo[]
            {
                new TestPointGroupInfo()
                {
                    X = new TestPointInfo() { Name = "测点1X方向", SampleFre = 2560, SamplePoint = 1024 },
                    Y = new TestPointInfo() { Name = "测点1Y方向", SampleFre = 2560, SamplePoint = 1024 },
                    Z = new TestPointInfo() { Name = "测点1Z方向", SampleFre = 2560, SamplePoint = 1024 },
                },
                new TestPointGroupInfo()
                {
                    X = new TestPointInfo() { Name = "测点2X方向", SampleFre = 2560, SamplePoint = 1024 },
                    Y = new TestPointInfo() { Name = "测点2Y方向", SampleFre = 2560, SamplePoint = 1024 },
                    Z = new TestPointInfo() { Name = "测点2Z方向", SampleFre = 2560, SamplePoint = 1024 },
                }
            };

            SingleTestPointInfo singleTestPointInfo = new SingleTestPointInfo();
            singleTestPointInfo.RPM = sg.RPM;
            singleTestPointInfo.ShaftName = sg.DeviceName;
            singleTestPointInfo.TestPointGroupInfo = new TestPointGroupInfo();
          
            singleTestPointInfo.TestPointGroupInfo = new TestPointGroupInfo()
            {
                X = new TestPointInfo() { Name = sg.ItemName, SampleFre = sg.SampleFre, SamplePoint = sg.SamplePoint, VData = Convert.ToBase64String(sg.Bytes) },
                Y = new TestPointInfo() { Name = "测点SY", SampleFre = 2560, SamplePoint = 1024 },
                Z = new TestPointInfo() { Name = "测点SZ", SampleFre = 2560, SamplePoint = 1024 },
            };

            devicediagnosis.ShaftInfos = new ShaftInfo[] { shaftInfo };
            devicediagnosis.SingleTestPointInfo = singleTestPointInfo;

            string json = JsonConvert.SerializeObject(devicediagnosis);
            StringBuilder condition = new StringBuilder(json);
            var conculsion = Algorithm.Instance.GetDiagnosisConclusionAction(condition);
            DiagnoseResult diagnoseResult = JsonConvert.DeserializeObject<DiagnoseResult>(conculsion);         
            return diagnoseResult;
        }
        #endregion
    }
}
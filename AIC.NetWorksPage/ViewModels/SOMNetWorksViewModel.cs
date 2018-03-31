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
using AIC.NetWorksPage.Models;
using System.Windows;
using AIC.Resources.Models;
using AIC.Core.DataModels;
using AIC.M9600.Common.DTO.Device;
using AIC.M9600.Common.SlaveDB.Generated;
using AIC.CoreType;
using AIC.MatlabMath;
using System.Collections;
using AIC.Core.HardwareModels;
using AIC.Core.DiagnosticModels;
using AIC.Core.ExCommand;
using System.Windows.Controls;
using Wpf.ZoomableCanvas;
using AIC.OnLineDataPage.ViewModels.SubViewModels;

namespace AIC.NetWorksPage.ViewModels
{
    class SOMNetWorksViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        private readonly IDatabaseComponent _databaseComponent;

        public SOMNetWorksViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess, IDatabaseComponent databaseComponent)
        {
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _signalProcess = signalProcess;
            _cardProcess = cardProcess;
            _databaseComponent = databaseComponent;

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

        private ObservableCollection<NodeViewModel> bPNetNodes;
        public ObservableCollection<NodeViewModel> NetNodes
        {
            get { return bPNetNodes; }
            set
            {
                bPNetNodes = value;
                OnPropertyChanged("NetNodes");
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

        private ObservableCollection<BaseDivfreSignal> offlineSignal = new ObservableCollection<BaseDivfreSignal>();
        public ObservableCollection<BaseDivfreSignal> OfflineSignal
        {
            get { return offlineSignal; }
            set
            {
                if (value != offlineSignal)
                {
                    offlineSignal = value;
                    this.OnPropertyChanged("OfflineSignal");
                }
            }
        }

        private ObservableCollection<BaseDivfreSignal> onlineSignal = new ObservableCollection<BaseDivfreSignal>();
        public ObservableCollection<BaseDivfreSignal> OnlineSignal
        {
            get { return onlineSignal; }
            set
            {
                if (value != onlineSignal)
                {
                    onlineSignal = value;
                    this.OnPropertyChanged("OnlineSignal");
                }
            }
        }

        private string testTrainResult;
        public string TestTrainResult
        {
            get { return testTrainResult; }
            set
            {
                if (value != testTrainResult)
                {
                    testTrainResult = value;
                    this.OnPropertyChanged("TestTrainResult");
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

        #endregion

        #region 命令
        private ICommand addTrainDataCommand;
        public ICommand AddTrainDataCommand
        {
            get
            {
                return this.addTrainDataCommand ?? (this.addTrainDataCommand = new DelegateCommand<object>(para => this.AddTrainData(para)));
            }
        }

        private ICommand deleteTrainDataCommand;
        public ICommand DeleteTrainDataCommand
        {
            get
            {
                return this.deleteTrainDataCommand ?? (this.deleteTrainDataCommand = new DelegateCommand<object>(para => this.DeleteTrainData(para)));
            }
        }

        private ICommand addTestDataCommand;
        public ICommand AddTestDataCommand
        {
            get
            {
                return this.addTestDataCommand ?? (this.addTestDataCommand = new DelegateCommand<object>(para => this.AddTestData(para)));
            }
        }

        private ICommand deleteTestDataCommand;
        public ICommand DeleteTestDataCommand
        {
            get
            {
                return this.deleteTestDataCommand ?? (this.deleteTestDataCommand = new DelegateCommand<object>(para => this.DeleteTestData(para)));
            }
        }

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

        public ICommand startTrainCommand;
        public ICommand StartTrainCommand
        {
            get
            {
                return this.startTrainCommand ?? (this.startTrainCommand = new DelegateCommand(() => this.StartTrain()));
            }
        }

        public ICommand testTrainCommand;
        public ICommand TestTrainCommand
        {
            get
            {
                return this.testTrainCommand ?? (this.testTrainCommand = new DelegateCommand(() => this.TestTrain()));
            }
        }

        public ICommand loadNetWorksCommand;
        public ICommand LoadNetWorksCommand
        {
            get
            {
                return this.loadNetWorksCommand ?? (this.loadNetWorksCommand = new DelegateCommand(() => this.LoadNetWorks()));
            }
        }

        public ICommand unloadNetWorksCommand;
        public ICommand UnloadNetWorksCommand
        {
            get
            {
                return this.unloadNetWorksCommand ?? (this.unloadNetWorksCommand = new DelegateCommand(() => this.UnloadNetWorks()));
            }
        }


        public ICommand saveNetWorksCommand;
        public ICommand SaveNetWorksCommand
        {
            get
            {
                return this.saveNetWorksCommand ?? (this.saveNetWorksCommand = new DelegateCommand(() => this.SaveNetWorks()));
            }
        }

        public ICommand listBoxSizeChangedComamnd;
        public ICommand ListBoxSizeChangedComamnd
        {
            get
            {
                return this.listBoxSizeChangedComamnd ?? (this.listBoxSizeChangedComamnd = new DelegateCommand<object>(para => this.ListBoxSizeChanged(para)));
            }
        }

        public ICommand listBoxLoadedComamnd;
        public ICommand ListBoxLoadedComamnd
        {
            get
            {
                return this.listBoxLoadedComamnd ?? (this.listBoxLoadedComamnd = new DelegateCommand<object>(para => this.ListBoxLoaded(para)));
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
                PretectTrain(sg);

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
        private async void AddTrainData(object para)
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
                        await SubAddTrainData(item);
                    }
                    catch (Exception ex)
                    {
                        _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("神经网络-数据添加", ex));
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
                        await SubAddTrainData(subitem, false);
                    }
                }
                catch (Exception ex)
                {
                    _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("神经网络-数据批量添加", ex));
                }
                finally
                {
                    Status = ViewModelStatus.None;
                }
                return;
            }
            #endregion
        }
        private async Task SubAddTrainData(ItemTreeItemViewModel item, bool showmessagbox = true)
        {
            List<IBaseAlarmSlot> result = new List<IBaseAlarmSlot>();

            if (item.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
            {
                result = await _databaseComponent.GetUniformHistoryData(item.T_Item.ItemType, item.ServerIP, item.T_Item.Guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade", "RPM", "RecordLab" }, StartTime.Value, EndTime.Value, "(IsValidWave = @0)", new object[] { 1 });
            }
            else if (item.T_Item.ItemType == (int)ChannelType.WirelessScalarChannelInfo)
            {
                return;
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
            if (item.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
            {

                Dictionary<Guid, Tuple<Guid, DateTime>> recordLabs = new Dictionary<Guid, Tuple<Guid, DateTime>>();
                foreach (var subresult in result)
                {
                    recordLabs.Add(subresult.RecordLab.Value, Tuple.Create<Guid, DateTime>(item.T_Item.Guid, subresult.ACQDatetime));
                }
                var waves = await _databaseComponent.GetHistoryWaveformData<D_WirelessVibrationSlot_Waveform>(item.ServerIP, recordLabs);
                foreach (var wave in waves)
                {
                    var subresult = result.Where(p => p.ACQDatetime == wave.ACQDatetime).FirstOrDefault();
                    if (subresult != null)
                    {
                        IWaveformData waveform = new WirelessVibrationSlotData_Waveform();
                        waveform.WaveData = wave.WaveData;
                        waveform.WaveUnit = wave.WaveUnit;
                        waveform.SampleFre = wave.SampleFre;
                        waveform.SamplePoint = wave.SamplePoint;
                        (subresult as IBaseWaveSlot).Waveform = waveform;
                    }              
                }
                foreach (var subresult in result.OfType<IBaseWaveSlot>().Where(p => p.Waveform != null))
                {
                    BaseDivfreSignal sg = new BaseDivfreSignal(item.T_Item.Guid);
                    sg.Names = item.Names;
                    sg.ACQDatetime = subresult.ACQDatetime;
                    sg.Result = subresult.Result;
                    sg.Unit = subresult.Unit;
                    sg.AlarmGrade = (AlarmGrade)(subresult.AlarmGrade & 0x00ffff00);
                    sg.RPM = (float)(subresult as IBaseDivfreSlot).RPM;
                    sg.SampleFre = subresult.Waveform.SampleFre.Value;
                    sg.SamplePoint = subresult.Waveform.SamplePoint.Value;
                    sg.Waveform = Algorithm.ByteToSingle(subresult.Waveform.WaveData);
                    sg.NetWorkIO = DiagnosticInfoClass.GetDiagnosticInfo(sg);
                    if (sg.NetWorkIO.Input != null && sg.NetWorkIO.Output != null)
                    {
                        OfflineSignal.Add(sg);
                    }
                }
            }
        }
        private void DeleteTrainData(object para)
        {
            if (para is string)
            {
                OfflineSignal.Clear();
            }
            else
            {
                var sgs = (para as IList).OfType<BaseDivfreSignal>().ToArray();
                if (sgs != null)
                {
                    for (int i = sgs.Length - 1; i >= 0; i--)
                    {
                        OfflineSignal.Remove(sgs[i]);
                    }
                }
            }
        }
        private async void AddTestData(object para)
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
                        await SubAddTestData(item);
                    }
                    catch (Exception ex)
                    {
                        _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("神经网络-数据添加", ex));
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
                        await SubAddTestData(subitem, false);
                    }
                }
                catch (Exception ex)
                {
                    _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("神经网络-数据批量添加", ex));
                }
                finally
                {
                    Status = ViewModelStatus.None;
                }
                return;
            }
            #endregion
        }
        private async Task SubAddTestData(ItemTreeItemViewModel item, bool showmessagbox = true)
        {
            List<IBaseAlarmSlot> result = new List<IBaseAlarmSlot>();

            if (item.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
            {
                result = await _databaseComponent.GetUniformHistoryData(item.T_Item.ItemType, item.ServerIP, item.T_Item.Guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade", "RPM", "RecordLab" }, StartTime.Value, EndTime.Value, "(IsValidWave = @0)", new object[] { 1 });
            }
            else if (item.T_Item.ItemType == (int)ChannelType.WirelessScalarChannelInfo)
            {
                return;
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
            if (item.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
            {

                Dictionary<Guid, Tuple<Guid, DateTime>> recordLabs = new Dictionary<Guid, Tuple<Guid, DateTime>>();
                foreach (var subresult in result)
                {
                    recordLabs.Add(subresult.RecordLab.Value, Tuple.Create<Guid, DateTime>(item.T_Item.Guid, subresult.ACQDatetime));
                }
                var waves = await _databaseComponent.GetHistoryWaveformData<D_WirelessVibrationSlot_Waveform>(item.ServerIP, recordLabs);
                foreach (var wave in waves)
                {
                    var subresult = result.Where(p => p.ACQDatetime == wave.ACQDatetime).FirstOrDefault();
                    if (subresult != null)
                    {
                        IWaveformData waveform = new WirelessVibrationSlotData_Waveform();
                        waveform.WaveData = wave.WaveData;
                        waveform.WaveUnit = wave.WaveUnit;
                        waveform.SampleFre = wave.SampleFre;
                        waveform.SamplePoint = wave.SamplePoint;
                        (subresult as IBaseWaveSlot).Waveform = waveform;
                    }
                }
                foreach (var subresult in result.OfType<IBaseWaveSlot>().Where(p => p.Waveform != null))
                {
                    BaseDivfreSignal sg = new BaseDivfreSignal(item.T_Item.Guid);
                    sg.Names = item.Names;
                    sg.ACQDatetime = subresult.ACQDatetime;
                    sg.Result = subresult.Result;
                    sg.Unit = subresult.Unit;
                    sg.AlarmGrade = (AlarmGrade)(subresult.AlarmGrade & 0x00ffff00);
                    sg.RPM = (float)(subresult as IBaseDivfreSlot).RPM;
                    sg.SampleFre = subresult.Waveform.SampleFre.Value;
                    sg.SamplePoint = subresult.Waveform.SamplePoint.Value;
                    sg.Waveform = Algorithm.ByteToSingle(subresult.Waveform.WaveData);
                    sg.NetWorkIO = DiagnosticInfoClass.GetEnergyInfo(sg);
                    if (sg.NetWorkIO.Input != null)
                    {
                        OnlineSignal.Add(sg);
                    }
                }
            }
        }
        private void DeleteTestData(object para)
        {
            var sgs = (para as IList).OfType<BaseDivfreSignal>().ToArray();
            if (sgs != null)
            {
                for (int i = sgs.Length - 1; i >= 0; i--)
                {
                    OnlineSignal.Remove(sgs[i]);
                }
            }
        }
        #endregion

        #region 网络训练
        private SOMNet somnet;
        private void LoadNetWorks()
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择打开模型文件夹";
            dialog.SelectedPath = dir;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                somnet = new SOMNet();
                somnet.readParas(foldPath + "\\para.txt");
                somnet.initial();
                somnet.readMatrixW(somnet.IO_send, foldPath + "\\w.txt");
                InitNetDisplay(somnet);
            }
        }
        private void UnloadNetWorks()
        {
            somnet = null;
            ClearNetDisplay();
        }

        private string dir = System.AppDomain.CurrentDomain.BaseDirectory + "MyData\\SOMNetWork";
        private void SaveNetWorks()
        {
            if (somnet == null)
            {
                return;
            }
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择保存模型文件夹";
            dialog.SelectedPath = dir;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                somnet.saveMatrix(somnet.IO_send, foldPath + "\\w.txt");
                somnet.saveParas(foldPath + "\\para.txt");
            }

        }
        private async void StartTrain()
        {
            if (OfflineSignal.Count == 0)
            {
                return;
            }

            int inNum = OfflineSignal[0].NetWorkIO.Input.Length;//输入节点数  
            int outNum = OfflineSignal[0].NetWorkIO.Output.Length;//输出层节点数  

            var sw = Stopwatch.StartNew();
            try
            {
                WaitInfo = "网络训练中";
                Status = ViewModelStatus.Querying;               
                await Task.Run(() =>
                {
                 
                    double[,] p1 = new double[OfflineSignal.Count, inNum];
                    for (int i = 0; i < OfflineSignal.Count; i++)
                    {
                        for (int j = 0; j < inNum; j++)
                        {
                            p1[i, j] = OfflineSignal[i].NetWorkIO.Input[j];
                        }
                    }
                    if (somnet == null)//如果为空需要初始化
                    {
                        somnet = new SOMNet(inNum, 8);
                    }
                    int study = 0;                
                    do
                    {
                        study++;
                        somnet.train(p1);
                        TestTrainResult = sw.Elapsed.ToString();
                    } while (somnet.now_time <= somnet.times);
                    //Console.Write("第" + study + "次学习：");
                    //Console.WriteLine("均方差为 " + somnet.e);                   
                });
            }
            catch
            {

            }
            finally
            {
                InitNetDisplay(somnet);
                TestTrainResult = sw.Elapsed.ToString();
                Status = ViewModelStatus.None;
            }
        }
        private void TestTrain()
        {
            PretectTrain(OnlineSignal);       
        }
        public void PretectTrain(BaseDivfreSignal sg)
        {
            if (somnet == null)
            {
                return;
            }

            double[,] p2 = new double[1, somnet.I_num];
            for (int j = 0; j < somnet.I_num; j++)
            {
                p2[0, j] = sg.NetWorkIO.Input[j];
            }

            int number = p2.GetLength(0);
            double[] input = new double[somnet.I_num];
            SOMNet.index index = new Core.DiagnosticModels.SOMNet.index();
            for (int n = 0; n < number; n++)
            {
                for (int i = 0; i < somnet.I_num; i++)
                {
                    input[i] = p2[n, i];
                }
                index = somnet.sim(input);               
                //Console.WriteLine("预测数据" + n.ToString() + "：x" + output.x + " y" + output.y + "");                
            }          
            ResultNetDisplay(input, index, somnet.O_num);
        }
        public void PretectTrain(IList<BaseDivfreSignal> sgs)
        {
            if (somnet == null)
            {
                return;
            }

            double[,] p2 = new double[sgs.Count, somnet.I_num];
            for (int i = 0; i < sgs.Count; i++)
            {
                for (int j = 0; j < somnet.I_num; j++)
                {
                    p2[i, j] = sgs[i].NetWorkIO.Input[j];
                }
            }

            double[] input = new double[somnet.I_num];
            SOMNet.index index = new Core.DiagnosticModels.SOMNet.index();
            for (int n = 0; n < sgs.Count; n++)
            {
                for (int i = 0; i < somnet.I_num; i++)
                {
                    input[i] = p2[n, i];
                }
                index = somnet.sim(input);

                sgs[n].NetWorkIO.DiagnosticResult = (index.x * somnet.O_num + index.y + 1).ToString();
            }
        }
        private void InitNetDisplay(SOMNet som)
        {
            if (som == null)
            {
                return;
            }

            //范围控制在（0~520,0~300）
            NetNodes = new ObservableCollection<NodeViewModel>();


            int height = 260 / (som.I_num - 1);
            for (int i = 0; i < som.I_num; i++)
            {
                som.I_layer[i].Index = i + 1;
                som.I_layer[i].Left = 0;
                som.I_layer[i].Top = i * height;
                som.I_layer[i].Width = som.I_layer[i].Height = 39;
                EllipseNodeViewModel node = new EllipseNodeViewModel(som.I_layer[i], "Input");
                NetNodes.Add(node);
            }

            for (int i = 0; i < som.O_num; i++)
            {
                for (int j = 0; j < som.O_num; j++)
                {
                    som.O_layer[i, j].Index = i * som.O_num + j + 1;
                    som.O_layer[i, j].Width = som.O_layer[i, j].Height = 38;
                    som.O_layer[i, j].Left = 200 + j * som.O_layer[i, j].Width;
                    som.O_layer[i, j].Top = i * som.O_layer[i, j].Height;
                    RectangleNodeViewModel node = new RectangleNodeViewModel(som.O_layer[i, j], "Output");
                    NetNodes.Add(node);
                }
            }

            for (int i = 0; i < som.IO_send.GetLength(0); i++)
            {
                for (int j = 0; j < som.IO_send.GetLength(1); j++)
                {                   
                    for (int k = 0; k < som.IO_send.GetLength(2); k++)
                    {
                        som.IO_send[i, j, k].Index = new int[] { i, j, k, };
                        som.IO_send[i, j, k].Left = som.I_layer[k].Left + som.I_layer[k].Width;
                        som.IO_send[i, j, k].Top = som.I_layer[k].Top + som.I_layer[k].Height / 2;
                        som.IO_send[i, j, k].Width = som.O_layer[i, j].Left + (som.O_layer[i, j].Width / 2) - som.I_layer[k].Left - som.I_layer[k].Width;
                        som.IO_send[i, j, k].Height = som.O_layer[i, j].Top - som.I_layer[k].Top;
                        LineNodeViewModel node = new LineNodeViewModel(som.IO_send[i, j, k]);
                        NetNodes.Insert(0, node);
                    }
                }               
            }
           
        }
        private void ClearNetDisplay()
        {
            NetNodes.Clear();
        }
        private void ResultNetDisplay(double[] input, SOMNet.index index, int num)
        {
            for (int i = 1; i <= input.Length; i++)
            {
                var innode = NetNodes.OfType<EllipseNodeViewModel>().Where(p => (p.Name == "Input") && (p.Index == i)).FirstOrDefault();
                innode.ShowValue = input[i - 1];                
            }

            foreach (var node in NetNodes.OfType<RectangleNodeViewModel>().Where(p => (p.Name == "Output")))
            {
                if (node.Index == (index.x * num + index.y + 1))
                {
                    node.IsSelected = true;
                }
                else
                {
                    node.IsSelected = false;
                }
            }
            TestTrainResult = (index.x * num + index.y + 1).ToString();
        }
        #endregion

        #region 网论图布局控制
        private void ListBoxSizeChanged(object para)
        {
            var sender = ((ExCommandParameter)para).Sender as ListBox;
            if (sender != null)
            {
                this.ZoomableCanvasOffset(sender);
            }
        }
        private void ListBoxLoaded(object para)
        {
            var sender = ((ExCommandParameter)para).Sender as ListBox;
            if (sender != null)
            {
                this.ZoomableCanvasOffset(sender);
            }
        }
        private void ZoomableCanvasOffset(ListBox listbox)
        {
            ZoomableCanvas canvas = listbox.Template.FindName("zoomCanvas", listbox) as ZoomableCanvas;
            canvas.Scale = Math.Min(Math.Min(listbox.ActualWidth / 520, listbox.ActualHeight / 300), 1);
            canvas.Offset = new Point(0 - (listbox.ActualWidth - 520 * canvas.Scale) / 2, 0 - (listbox.ActualHeight - 300 * canvas.Scale) / 2);
        }
        #endregion
    }
}

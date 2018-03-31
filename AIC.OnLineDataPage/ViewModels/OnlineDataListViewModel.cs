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
using AIC.ServiceInterface;
using AIC.Core.SignalModels;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Data;
using AIC.CoreType;
using AIC.MatlabMath;
using AIC.OnLineDataPage.Models;
using System.Diagnostics;
using System.Threading;
using AIC.Core.HardwareModels;

namespace AIC.OnLineDataPage.ViewModels
{
    class OnlineDataListViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;

        public OnlineDataListViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess)
        {
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _signalProcess = signalProcess;
            _cardProcess = cardProcess;

            _signalProcess.SignalsAdded += _signalCache_SignalAdded;
            _signalProcess.SignalsRemoved += _signalCache_SignalRemoved;

            foreach (var sg in _signalProcess.Signals.OfType<BaseAlarmSignal>().ToArray())
            {
                signals.Add(sg);
            }

            selectedsignals = signals;
            FirstName = (selectedsignals.FirstOrDefault() != null) ? signals.FirstOrDefault().OrganizationDeviceName : null;

            _view = new ListCollectionView(signals);
            _view.Filter = (object item) =>
            {
                var itemPl = (BaseAlarmSignal)item;
                if (itemPl == null) return false;
                if (selectedsignals.Contains(itemPl))
                {
                    if (IsInvalidSignal == false && IsNormalSignal == false && IsPreAlarmSignal == false && IsAlarmSignal == false && IsDangerSignal == false && DisConnectSignal == false)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.Invalid && IsInvalidSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.HighNormal && IsNormalSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.HighPreAlarm && IsPreAlarmSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.HighAlarm && IsAlarmSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.HighDanger && IsDangerSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.LowDanger && IsDangerSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.LowAlarm && IsAlarmSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.LowPreAlarm && IsPreAlarmSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.LowNormal && IsNormalSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.DisConnect && DisConnectSignal == true)
                    {
                        return true;
                    }

                    return false;
                }
                return false;
            };
            _view.GroupDescriptions.Add(new PropertyGroupDescription("OrganizationDeviceName"));//对视图进行分组
            InitTree();

            readDataTimer.Tick += new EventHandler(timeCycle);
            readDataTimer.Interval = new TimeSpan(0, 0, 0, 1);
            readDataTimer.Start();
        }
        #region 属性与字段

        private readonly ICollectionView _view;
        public ICollectionView SignalsView { get { return _view; } }

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

        private ObservableCollection<BaseAlarmSignal> signals = new ObservableCollection<BaseAlarmSignal>();
        public IEnumerable<BaseAlarmSignal> Signals { get { return signals; } }

        private BaseAlarmSignal selectedSignal;
        public BaseAlarmSignal SelectedSignal
        {
            get { return selectedSignal; }
            set
            {
                if (selectedSignal != value)
                {
                    selectedSignal = value;
                    OnPropertyChanged(() => SelectedSignal);
                }
            }
        }

        private string diagnosticInfo;
        public string DiagnosticInfo
        {
            get { return diagnosticInfo; }
            set
            {
                if (diagnosticInfo != value)
                {
                    diagnosticInfo = value;
                    OnPropertyChanged(() => DiagnosticInfo);
                }
            }
        }

        private string diagnosticAdvice;
        public string DiagnosticAdvice
        {
            get { return diagnosticAdvice; }
            set
            {
                if (diagnosticAdvice != value)
                {
                    diagnosticAdvice = value;
                    OnPropertyChanged(() => DiagnosticAdvice);
                }
            }
        }

        private bool isInvalidSignal;
        public bool IsInvalidSignal
        {
            get { return isInvalidSignal; }
            set
            {
                if (isInvalidSignal != value)
                {
                    isInvalidSignal = value;
                    OnPropertyChanged(() => IsInvalidSignal);
                    Refresh();
                }
            }
        }

        private bool isNormalSignal;
        public bool IsNormalSignal
        {
            get { return isNormalSignal; }
            set
            {
                if (isNormalSignal != value)
                {
                    isNormalSignal = value;
                    OnPropertyChanged(() => IsNormalSignal);
                    Refresh();
                }
            }
        }

        private bool isPreAlertSignal;
        public bool IsPreAlarmSignal
        {
            get { return isPreAlertSignal; }
            set
            {
                if (isPreAlertSignal != value)
                {
                    isPreAlertSignal = value;
                    OnPropertyChanged(() => IsPreAlarmSignal);
                    Refresh();
                }
            }
        }

        private bool isAlertSignal;
        public bool IsAlarmSignal
        {
            get { return isAlertSignal; }
            set
            {
                if (isAlertSignal != value)
                {
                    isAlertSignal = value;
                    OnPropertyChanged(() => IsAlarmSignal);
                    Refresh();
                }
            }
        }

        private bool isDangerSignal;
        public bool IsDangerSignal
        {
            get { return isDangerSignal; }
            set
            {
                if (isDangerSignal != value)
                {
                    isDangerSignal = value;
                    OnPropertyChanged(() => IsDangerSignal);
                    Refresh();
                }
            }
        }     

        private bool isUnConnectSignal;
        public bool DisConnectSignal
        {
            get { return isUnConnectSignal; }
            set
            {
                if (isUnConnectSignal != value)
                {
                    isUnConnectSignal = value;
                    OnPropertyChanged(() => DisConnectSignal);
                    Refresh();
                }
            }
        }

        private int normalCount;
        public int NormalCount
        {
            get { return normalCount; }
            set
            {
                if (normalCount != value)
                {
                    normalCount = value;
                    this.OnPropertyChanged(() => this.NormalCount);
                }
            }
        }

        private int preAlertCount;
        public int PreAlertCount
        {
            get { return preAlertCount; }
            set
            {
                if (preAlertCount != value)
                {
                    preAlertCount = value;
                    this.OnPropertyChanged(() => this.PreAlertCount);
                }
            }
        }

        private int alertCount;
        public int AlertCount
        {
            get { return alertCount; }
            set
            {
                if (alertCount != value)
                {
                    alertCount = value;
                    this.OnPropertyChanged(() => this.AlertCount);
                }
            }
        }

        private int dangerCount;
        public int DangerCount
        {
            get { return dangerCount; }
            set
            {
                if (dangerCount != value)
                {
                    dangerCount = value;
                    this.OnPropertyChanged(() => this.DangerCount);
                }
            }
        }

        private int abnormalCount;
        public int AbnormalCount
        {
            get { return abnormalCount; }
            set
            {
                if (abnormalCount != value)
                {
                    abnormalCount = value;
                    this.OnPropertyChanged(() => this.AbnormalCount);
                }
            }
        }

        private int unConnectCount;
        public int UnConnectCount
        {
            get { return unConnectCount; }
            set
            {
                if (unConnectCount != value)
                {
                    unConnectCount = value;
                    this.OnPropertyChanged(() => this.UnConnectCount);
                }
            }
        }

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

        public ICommand selectedDataGridChangedComamnd;
        public ICommand SelectedDataGridChangedComamnd
        {
            get
            {
                return this.selectedDataGridChangedComamnd ?? (this.selectedDataGridChangedComamnd = new DelegateCommand<object>(para => this.SelectedDataGridChanged(para)));
            }
        }

        public ICommand groupViewCommand;
        public ICommand GroupViewCommand
        {
            get
            {
                return this.groupViewCommand ?? (this.groupViewCommand = new DelegateCommand(() => this.GroupView()));
            }
        }

        public ICommand unGroupViewCommand;
        public ICommand UnGroupViewCommand
        {
            get
            {
                return this.unGroupViewCommand ?? (this.unGroupViewCommand = new DelegateCommand(() => this.UnGroupView()));
            }
        }
        #endregion

        #region 信号增减
        private void _signalCache_SignalAdded(BaseAlarmSignal sg)
        {
            if (!(signals.Contains(sg)))
            {
                signals.Add(sg);
            }
        }

        private void _signalCache_SignalRemoved(BaseAlarmSignal sg)
        {
            if (signals.Contains(sg))
            {
                signals.Remove(sg);
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

        private CancellationTokenSource cts;
        private IEnumerable<BaseAlarmSignal> selectedsignals;
        public async void SelectedTreeChanged(object para)
        {
            var sw = Stopwatch.StartNew();
            if (para is OrganizationTreeItemViewModel)
            {
                selectedsignals = _cardProcess.GetItems(para as OrganizationTreeItemViewModel).Select(p => p.BaseAlarmSignal);
                FirstName = (selectedsignals.FirstOrDefault() != null) ? selectedsignals.FirstOrDefault().OrganizationDeviceName : null;
                _view.Refresh();
            }
            if (para is DeviceTreeItemViewModel)
            {
                var deviceTree = para as OrganizationTreeItemViewModel;
                if (deviceTree.Children != null)
                {
                    var itemTrees = deviceTree.Children.OfType<ItemTreeItemViewModel>().Where(p => p.T_Item != null && p.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo && p.BaseAlarmSignal != null);
                    var abnormal = itemTrees.Where(p => p.Alarm != AlarmGrade.Invalid && p.Alarm != AlarmGrade.HighNormal && p.Alarm != AlarmGrade.LowNormal && p.Alarm != AlarmGrade.DisConnect).OrderByDescending(p => (int)p.Alarm & 0xff).ThenByDescending(p => p.BaseAlarmSignal.Result).FirstOrDefault();
                    if (abnormal != null && abnormal.BaseAlarmSignal is BaseWaveSignal)
                    {
                        var sg = abnormal.BaseAlarmSignal as BaseWaveSignal;
                        sg.IsDiagnostic = true;
                        try
                        {
                            await WaitingDiagnostic(sg, 10);
                        }
                        catch (OperationCanceledException)
                        {
                            return;
                        }
                        DiagnosticInfo = sg.DeviceItemName + "-诊断信息:" + sg.DiagnosticInfo;
                        DiagnosticAdvice = sg.DiagnosticAdvice;
                    }
                    else
                    {
                        DiagnosticInfo = null;
                        DiagnosticAdvice = null;
                    }
                }
            }

            Console.WriteLine("消耗时间" + sw.Elapsed.ToString());
        }

        private async void SelectedDataGridChanged(object para)
        {
            var sg = para as BaseWaveSignal;
            if (sg != null)
            {
                (sg as BaseWaveSignal).IsDiagnostic = true;
                try
                {
                    await WaitingDiagnostic(sg, 10);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                DiagnosticInfo = sg.DeviceItemName + "-诊断信息:" + sg.DiagnosticInfo;
                DiagnosticAdvice = sg.DiagnosticAdvice;
            }
            else
            {
                DiagnosticInfo = "不是振动数据";
                DiagnosticAdvice = null;
            }
        }

        private async Task WaitingDiagnostic(BaseWaveSignal sg, int delaytime)
        {
            if (cts != null)
            {
                cts.Cancel();
            }

            DiagnosticInfo = null;
            DiagnosticAdvice = null;

            cts = new CancellationTokenSource();
            await Task.Run(() =>
            {
                for (int i = 0; i < delaytime; i++)
                {
                    Thread.Sleep(1000);

                    if (sg.IsDiagnostic == true)
                    {
                        cts.Token.ThrowIfCancellationRequested();
                        break;
                    }
                }
            },cts.Token);
        }
        private void GroupView()
        {
            _view.GroupDescriptions.Clear();
            _view.GroupDescriptions.Add(new PropertyGroupDescription("OrganizationDeviceName"));//对视图进行分组
        }

        private void UnGroupView()
        {
            _view.GroupDescriptions.Clear();
            _view.GroupDescriptions.Add(new PropertyGroupDescription("NullName"));//对视图进行分组
        }

        private void Refresh()
        {
            if (selectedsignals == null)
            {
                return;
            }

            BaseAlarmSignal firstsignal;
            if (IsInvalidSignal == false && IsNormalSignal == false && IsPreAlarmSignal == false && IsAlarmSignal == false && IsDangerSignal == false && DisConnectSignal == false)
            {
                firstsignal = selectedsignals.FirstOrDefault();                           
            }
            else
            {
                List<AlarmGrade> grades = new List<AlarmGrade>();
                if (IsInvalidSignal == true)
                {
                    grades.Add(AlarmGrade.Invalid);
                }
                if (IsNormalSignal == true)
                {
                    grades.Add(AlarmGrade.HighNormal);
                    grades.Add(AlarmGrade.LowNormal);
                }
                if (IsPreAlarmSignal == true)
                {
                    grades.Add(AlarmGrade.HighPreAlarm);
                    grades.Add(AlarmGrade.LowPreAlarm);
                }
                if (IsAlarmSignal == true)
                {
                    grades.Add(AlarmGrade.HighAlarm);
                    grades.Add(AlarmGrade.LowAlarm);
                }
                if (IsDangerSignal == true)
                {
                    grades.Add(AlarmGrade.HighDanger);
                    grades.Add(AlarmGrade.LowDanger);
                }
                if (DisConnectSignal == true)
                {
                    grades.Add(AlarmGrade.DisConnect);
                }

                firstsignal = selectedsignals.Where(p => grades.Contains(p.AlarmGrade)).FirstOrDefault();              
            }
            FirstName = (firstsignal != null) ? firstsignal.OrganizationDeviceName : null;
            _view.Refresh();
        }

        private System.Windows.Threading.DispatcherTimer readDataTimer = new System.Windows.Threading.DispatcherTimer();
        private void timeCycle(object sender, EventArgs e)
        {
            NormalCount = 0;
            PreAlertCount = 0;
            AlertCount = 0;
            DangerCount = 0;
            AbnormalCount = 0;
            UnConnectCount = 0;
            foreach (var sg in selectedsignals)
            {
                if (sg != null)
                {
                    switch (sg.DelayAlarmGrade)
                    {
                        case AlarmGrade.HighNormal:
                        case AlarmGrade.LowNormal:
                            NormalCount++; break;
                        case AlarmGrade.HighPreAlarm:
                        case AlarmGrade.LowPreAlarm:
                            PreAlertCount++; break;
                        case AlarmGrade.HighAlarm:
                        case AlarmGrade.LowAlarm:
                            AlertCount++; break;
                        case AlarmGrade.HighDanger:
                        case AlarmGrade.LowDanger:
                            DangerCount++; break;
                        case AlarmGrade.Abnormal:
                            AbnormalCount++; break;
                        case AlarmGrade.DisConnect:
                            UnConnectCount++; break;
                        default:
                            UnConnectCount++; break;
                    }
                }
            }
        }
    }
}

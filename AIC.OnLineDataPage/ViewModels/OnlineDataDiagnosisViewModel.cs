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
using System.Threading;
using AIC.Resources.Models;
using System.Windows;

namespace AIC.OnLineDataPage.ViewModels
{
    class OnlineDataDiagnosisViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        public delegate void UpdatePie3D(IList<int> countlist);
        public event UpdatePie3D UpdateChart;

        public OnlineDataDiagnosisViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess)
        {
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _signalProcess = signalProcess;
            _cardProcess = cardProcess;

            _signalProcess.SignalsAdded += _signalCache_SignalAdded;
            _signalProcess.SignalsRemoved += _signalCache_SignalRemoved;

            foreach (var sg in _signalProcess.Signals.OfType<BaseWaveSignal>().ToArray())
            {
                signals.Add(sg);
            }

            selectedsignals = signals;
            FirstName = (selectedsignals.FirstOrDefault() != null) ? signals.FirstOrDefault().OrganizationDeviceName : null;

            _view = new ListCollectionView(signals);
            _view.Filter = (object item) =>
            {
                var itemPl = (BaseWaveSignal)item;
                if (itemPl == null) return false;
                if (selectedsignals.Contains(itemPl))
                {
                    //if (itemPl.DiagnosticGrade == AlarmGrade.HighNormal || itemPl.DiagnosticGrade == AlarmGrade.LowNormal)
                    //{
                    //    return false;
                    //}
                    return true;
                }
                return false;
            };
            _view.SortDescriptions.Add(new SortDescription("DiagnosticGrade", ListSortDirection.Descending));
            _view.GroupDescriptions.Add(new PropertyGroupDescription("DiagnosticGrade"));//对视图进行分组
            InitTree();
        }

        #region 信号增减
        private void _signalCache_SignalAdded(BaseAlarmSignal sg)
        {
            BaseWaveSignal waveSg = sg as BaseWaveSignal;
            if (waveSg != null)
            {
                if (!(signals.Contains(waveSg)))
                {
                    signals.Add(waveSg);
                }
            }
        }

        private void _signalCache_SignalRemoved(BaseAlarmSignal sg)
        {
            BaseWaveSignal waveSg = sg as BaseWaveSignal;
            if (waveSg != null)
            {
                if (signals.Contains(waveSg))
                {
                    signals.Remove(waveSg);
                }
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

        private ObservableCollection<BaseWaveSignal> signals = new ObservableCollection<BaseWaveSignal>();
        public IEnumerable<BaseWaveSignal> Signals { get { return signals; } }

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

        public string waitinfo = "诊断中";
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

        private bool canCancel = true;
        public bool CanCancel
        {
            get
            {
                return canCancel;
            }
            set
            {
                canCancel = value;
                OnPropertyChanged("CanCancel");
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

        private AlarmGrade firstAlarmGrade;
        public AlarmGrade FirstAlarmGrade
        {
            get { return firstAlarmGrade; }
            set
            {
                if (firstAlarmGrade != value)
                {
                    firstAlarmGrade = value;
                    OnPropertyChanged(() => FirstAlarmGrade);
                }
            }
        }

        private DelegateCommand refreshCommand;
        public DelegateCommand RefreshCommand
        {
            get
            {
                return this.refreshCommand ?? (this.refreshCommand = new DelegateCommand(() => this.Refresh()));
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

        public ICommand mouseDoubleClickComamnd;
        public ICommand MouseDoubleClickComamnd
        {
            get
            {
                return this.mouseDoubleClickComamnd ?? (this.mouseDoubleClickComamnd = new DelegateCommand<object>(para => this.MouseDoubleClick(para)));
            }
        }

        private DelegateCommand startDiagnosisCommand;
        public DelegateCommand StartDiagnosisCommand
        {
            get
            {
                return this.startDiagnosisCommand ?? (this.startDiagnosisCommand = new DelegateCommand(() => this.StartDiagnosis(), () => CanStartDiagnosis()));
            }
        }

        private DelegateCommand cancelCommmad;
        public DelegateCommand CancelCommmad
        {
            get
            {
                return this.cancelCommmad ?? (this.cancelCommmad = new DelegateCommand(() => this.StopDiagnosis(), () => CanStopDiagnosis()));
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

        private IEnumerable<BaseWaveSignal> selectedsignals;
        public void SelectedTreeChanged(object para)
        {
            if (para is OrganizationTreeItemViewModel)
            {
                selectedsignals = _cardProcess.GetItems(para as OrganizationTreeItemViewModel).Select(p => p.BaseAlarmSignal).OfType<BaseWaveSignal>();
                Refresh();
            }
        }


        private void MouseDoubleClick(object para)
        {
            var sg = para as BaseWaveSignal;
            if (sg != null && sg.DiagnosticAdvice != null)
            {
#if XBAP
                MessageBox.Show(sg.DiagnosticInfo + "\n\r" + sg.DiagnosticAdvice, "诊断详情", MessageBoxButton.OK, MessageBoxImage.Information);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show(sg.DiagnosticInfo + "\n\r" + sg.DiagnosticAdvice, "诊断详情", MessageBoxButton.OK, MessageBoxImage.Information);
#endif
            }
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

        private CancellationTokenSource cts;

        private bool CanStartDiagnosis()
        {
            return true;
        }

        private bool CanStopDiagnosis()
        {
            return true;
        }
        private async void StartDiagnosis()
        {    
            try
            {
                Status = ViewModelStatus.Querying;
                cts = new CancellationTokenSource();
      
                foreach (var sg in selectedsignals.OfType<BaseWaveSignal>())
                {
                    sg.IsDiagnostic = true;
                }
                //await Task.Delay(TimeSpan.FromSeconds(20), cts.Token);
                await WaitDiagnosis(10);
            }
            catch (OperationCanceledException)
            {
#if XBAP
                MessageBox.Show("取消成功！！！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("取消成功！！！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
#endif
            }
            catch(Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("诊断异常", ex));
            }
            finally
            {
                Refresh();

                Status = ViewModelStatus.None;
            }
        }

        private void StopDiagnosis()
        {
            cts.Cancel();
        }

        private async Task WaitDiagnosis(int delaytime)
        {
            for (int i = 0; i < delaytime; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), cts.Token);

                if (cts.IsCancellationRequested) //点击取消按钮
                {
                    break;         
                }

                bool finish = true;
                foreach (var sg in selectedsignals.OfType<BaseWaveSignal>())
                {
                    if (sg.IsDiagnostic == true)
                    {
                        finish = false;
                        break;
                    }
                }
                if (finish == true)
                {
                    cts.Token.ThrowIfCancellationRequested();
                    break;
                }
            }
        }

        private void ShowAlarmCount()
        {
            NormalCount = 0;
            PreAlertCount = 0;
            AlertCount = 0;
            DangerCount = 0;
            AbnormalCount = 0;
            UnConnectCount = 0;
            foreach (var sg in selectedsignals.OfType<BaseWaveSignal>())
            {
                if (sg != null)
                {
                    switch (sg.DiagnosticGrade)
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

        private void UpdateFirstName()
        {
            if (IsInvalidSignal == true)
            {
                var first = selectedsignals.Where(p => p.DiagnosticGrade == AlarmGrade.Invalid).FirstOrDefault();
                FirstName = (first != null) ? first.OrganizationDeviceName : null;
            }
            else if (IsNormalSignal == true)
            {
                var first = selectedsignals.Where(p => p.DiagnosticGrade == AlarmGrade.HighNormal || p.AlarmGrade == AlarmGrade.LowNormal).FirstOrDefault();
                FirstName = (first != null) ? first.OrganizationDeviceName : null;
            }
            else if (IsPreAlarmSignal == true)
            {
                var first = selectedsignals.Where(p => p.DiagnosticGrade == AlarmGrade.HighPreAlarm || p.AlarmGrade == AlarmGrade.LowPreAlarm).FirstOrDefault();
                FirstName = (first != null) ? first.OrganizationDeviceName : null;
            }
            else if (IsAlarmSignal == true)
            {
                var first = selectedsignals.Where(p => p.DiagnosticGrade == AlarmGrade.HighAlarm || p.AlarmGrade == AlarmGrade.LowAlarm).FirstOrDefault();
                FirstName = (first != null) ? first.OrganizationDeviceName : null;
            }
            else if (IsDangerSignal == true)
            {
                var first = selectedsignals.Where(p => p.DiagnosticGrade == AlarmGrade.HighDanger || p.AlarmGrade == AlarmGrade.LowDanger).FirstOrDefault();
                FirstName = (first != null) ? first.OrganizationDeviceName : null;
            }
            else if (DisConnectSignal == true)
            {
                var first = selectedsignals.Where(p => p.DiagnosticGrade == AlarmGrade.DisConnect || p.DiagnosticGrade == 0x00).FirstOrDefault();
                FirstName = (first != null) ? first.OrganizationDeviceName : null;
            }
            else
            {
                var first = selectedsignals.FirstOrDefault();
                FirstName = (first != null) ? first.OrganizationDeviceName : null;
            }
        }

        private void Refresh()
        {
            //UpdateFirstName();
            //_view.Refresh();
            //ShowAlarmCount();
            if (selectedsignals == null)
            {
                return;
            }

            _view.Refresh();
            int NormalCount = selectedsignals.Where(o => (o.DiagnosticGrade == AlarmGrade.HighNormal || o.DiagnosticGrade == AlarmGrade.LowNormal)).Count();
            int PreAlertCount = selectedsignals.Where(o => (o.DiagnosticGrade == AlarmGrade.HighPreAlarm || o.DiagnosticGrade == AlarmGrade.LowPreAlarm)).Count();
            int AlertCount = selectedsignals.Where(o => (o.DiagnosticGrade == AlarmGrade.HighAlarm || o.DiagnosticGrade == AlarmGrade.LowAlarm)).Count();
            int DangerCount = selectedsignals.Where(o => (o.DiagnosticGrade == AlarmGrade.HighDanger || o.DiagnosticGrade == AlarmGrade.LowDanger)).Count();
            int AbnormalCount = selectedsignals.Where(o => (o.DiagnosticGrade == AlarmGrade.Abnormal)).Count();
            int UnConnectCount = selectedsignals.Where(o => (o.DiagnosticGrade == AlarmGrade.DisConnect || o.DiagnosticGrade == 0x00)).Count();
            if (UpdateChart != null)
            {
                UpdateChart(new List<int> { NormalCount, PreAlertCount, AlertCount, DangerCount, AbnormalCount, UnConnectCount });
            }
        }

        public void SliceClick(AlarmGrade alarmgrade)
        {
            List<AlarmGrade> grades = new List<AlarmGrade>();
            foreach (AlarmGrade grade in Enum.GetValues(typeof(AlarmGrade)))
            {
                if ((alarmgrade & grade) == grade)
                {
                    if (!grades.Contains(grade))
                    {
                        grades.Add(grade);
                    }
                }
            }

            var sg = selectedsignals.Where(p => grades.Contains(p.DiagnosticGrade)).FirstOrDefault();
            if (sg != null)
            {
                FirstAlarmGrade = sg.DiagnosticGrade;
            }
            //_view.Refresh();
        }
    }
}

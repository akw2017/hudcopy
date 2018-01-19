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
                var itemPl = (BaseAlarmSignal)item;
                if (itemPl == null) return false;
                if (selectedsignals.Contains(itemPl))
                {
                    return true;
                }
                return false;
            };
            _view.GroupDescriptions.Add(new PropertyGroupDescription("OrganizationDeviceName"));//对视图进行分组
            InitTree();
        }

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

        private IEnumerable<BaseAlarmSignal> selectedsignals;
        public void SelectedTreeChanged(object para)
        {
            if (para is OrganizationTreeItemViewModel)
            {
                selectedsignals = _cardProcess.GetItems(para as OrganizationTreeItemViewModel).Select(p => p.BaseAlarmSignal);
                FirstName = (selectedsignals.FirstOrDefault() != null) ? selectedsignals.FirstOrDefault().OrganizationDeviceName : null;
                _view.Refresh();
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
                var token = cts.Token;

                await Task.Run(() =>
                {                   
                    foreach (var item in _view.AsParallel())
                    {
                        var sg = item as BaseWaveSignal;
                        DiagnosticInfoClass.GetDiagnosticInfo(sg);     
                    }
                }, token);               
            }
            catch(OperationCanceledException)
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
                Status = ViewModelStatus.None;
            }
        }

        private void StopDiagnosis()
        {
            cts.Cancel();
        }       
   
    }
}

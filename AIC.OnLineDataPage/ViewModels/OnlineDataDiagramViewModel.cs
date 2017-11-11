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
using AIC.Core;
using AIC.Domain;
using System.Windows.Input;
using System.Windows.Controls;
using DiagramDesigner;
using System.IO;
using AIC.OnLineDataPage.Models;
using System.Xml.Serialization;
using System.Collections.Specialized;
using System.Reactive.Linq;
using AIC.Core.SignalModels;
using AIC.CoreType;
using AIC.OnLineDataPage.ViewModels.SubViewModels;
using AIC.OnLineDataPage.Controllers;
using System.Configuration;

namespace AIC.OnLineDataPage.ViewModels
{
    class OnlineDataDiagramViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        //private readonly IPropertyController propertyController;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        private readonly ILoginUserService _loginUserService;
        public OnlineDataDiagramViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess, ILoginUserService loginUserService)
        {
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _signalProcess = signalProcess;
            _cardProcess = cardProcess;
            _loginUserService = loginUserService;

            ConnectorViewModel.PathFinder = new OrthogonalPathFinder();

            DiagramViewModel = new DiagramViewModel();            
            graphFunctionCollection = new FastObservableCollection<ChartViewModelBase>();

            diagramItemsChangedSubscription = DiagramViewModel.WhenItemsChanged.Subscribe(OnDiagramItemsChanged);
            DiagramViewModel.WhenPropertyChanged.Where(p => p == "SelectedItem").Subscribe(OnSelectedItemChanged);

            Initialize();
            InitTree();

            if (_loginUserService.LoginInfo.ServerInfo.Permission.Contains("admin") || _loginUserService.LoginInfo.ServerInfo.Permission.Contains("管理员"))
            {
                CanOperated = true;
            }
            else
            {
                CanOperated = false;
            }
        }

        #region 管理树
        private void InitTree()
        {
            OrganizationTreeItems = _organizationService.OrganizationTreeItems;
            var selectedTreeItem = _cardProcess.GetSelectedTree(OrganizationTreeItems);
            SelectedTreeChanged(selectedTreeItem);
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

        private FastObservableCollection<ChartViewModelBase> graphFunctionCollection;
        public IEnumerable<ChartViewModelBase> GraphFunctions { get { return graphFunctionCollection; } }

        public DiagramViewModel DiagramViewModel { get; private set; }

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

        private BaseAlarmSignal selectedSignal;
        public BaseAlarmSignal SelectedSignal
        {
            get { return selectedSignal; }
            set
            {
                if (selectedSignal != value)
                {
                    selectedSignal = value;
                    OnPropertyChanged(() => this.SelectedSignal);
                }
            }
        }

        private string deviceName;
        public string DeviceName
        {
            get { return deviceName; }
            set
            {
                if (deviceName != value)
                {
                    deviceName = value;
                    OnPropertyChanged("DeviceName");
                }
            }
        }

        private bool isFilter;
        public bool IsFilter
        {
            get { return isFilter; }
            set
            {
                if (isFilter != value)
                {
                    isFilter = value;
                    OnPropertyChanged("IsFilter");
                }
            }
        }

        private bool isEnvelope;
        public bool IsEnvelope
        {
            get { return isEnvelope; }
            set
            {
                if (isEnvelope != value)
                {
                    isEnvelope = value;
                    OnPropertyChanged("IsEnvelope");
                }
            }
        }

        private bool isTFF;
        public bool IsTFF
        {
            get { return isTFF; }
            set
            {
                if (isTFF != value)
                {
                    isTFF = value;
                    OnPropertyChanged("IsTFF");
                }
            }
        }

        private bool isCepstrum;
        public bool IsCepstrum
        {
            get { return isCepstrum; }
            set
            {
                if (isCepstrum != value)
                {
                    isCepstrum = value;
                    OnPropertyChanged("IsCepstrum");
                }
            }
        }

        private bool itemIsExpanded;
        public bool ItemIsExpanded
        {
            get { return itemIsExpanded; }
            set
            {
                if (itemIsExpanded != value)
                {
                    itemIsExpanded = value;
                    OnPropertyChanged("ItemIsExpanded");
                }
            }
        }

        

        #region IsComposing
        private bool isComposing = false;
        public bool IsComposing
        {
            get { return isComposing; }
            set
            {
                if (isComposing != value)
                {
                    isComposing = value;
                    OnPropertyChanged("IsComposing");
                }
            }
        }
        #endregion

        #region IsValueChecked
        private bool isValueChecked;
        public bool IsValueChecked
        {
            get { return isValueChecked; }
            set
            {
                if (isValueChecked != value)
                {
                    isValueChecked = value;
                    OnPropertyChanged("IsValueChecked");
                }
            }
        }
        #endregion

        #region IsTimeDomainChecked
        private bool isTimeDomainChecked;
        public bool IsTimeDomainChecked
        {
            get { return isTimeDomainChecked; }
            set
            {
                if (isTimeDomainChecked != value)
                {
                    isTimeDomainChecked = value;
                    OnPropertyChanged("IsTimeDomainChecked");
                }
            }
        }
        #endregion

        #region IsFrequencyDomainChecked
        private bool isFrequencyDomainChecked;
        public bool IsFrequencyDomainChecked
        {
            get { return isFrequencyDomainChecked; }
            set
            {
                if (isFrequencyDomainChecked != value)
                {
                    isFrequencyDomainChecked = value;
                    OnPropertyChanged("IsFrequencyDomainChecked");
                }
            }
        }
        #endregion

        #region IsAMSTrendChecked
        private bool isAMSTrendChecked;
        public bool IsAMSTrendChecked
        {
            get { return isAMSTrendChecked; }
            set
            {
                if (isAMSTrendChecked != value)
                {
                    isAMSTrendChecked = value;
                    OnPropertyChanged("IsAMSTrendChecked");
                }
            }
        }
        #endregion

        #region IsMultiDivFreChecked
        private bool isMultiDivFreChecked;
        public bool IsMultiDivFreChecked
        {
            get { return isMultiDivFreChecked; }
            set
            {
                if (isMultiDivFreChecked != value)
                {
                    isMultiDivFreChecked = value;
                    OnPropertyChanged("IsMultiDivFreChecked");
                }
            }
        }
        #endregion

        #region IsOrthoChecked
        private bool isOrthoChecked;
        public bool IsOrthoChecked
        {
            get { return isOrthoChecked; }
            set
            {
                if (isOrthoChecked != value)
                {
                    isOrthoChecked = value;
                    OnPropertyChanged("IsOrthoChecked");
                }
            }
        }
        #endregion

        #region IsBodeChecked
        private bool isBodeChecked;
        public bool IsBodeChecked
        {
            get { return isBodeChecked; }
            set
            {
                if (isBodeChecked != value)
                {
                    isBodeChecked = value;
                    OnPropertyChanged("IsBodeChecked");
                }
            }
        }
        #endregion

        #region IsOrderChecked
        private bool isOrderChecked;
        public bool IsOrderChecked
        {
            get { return isOrderChecked; }
            set
            {
                if (isOrderChecked != value)
                {
                    isOrderChecked = value;
                    OnPropertyChanged("IsOrderChecked");
                }
            }
        }
        #endregion

        #region IsNyquistChecked
        private bool isNyquistChecked;
        public bool IsNyquistChecked
        {
            get { return isNyquistChecked; }
            set
            {
                if (isNyquistChecked != value)
                {
                    isNyquistChecked = value;
                    OnPropertyChanged("IsNyquistChecked");
                }
            }
        }
        #endregion

        #region IsTime3DChecked
        private bool isTime3DChecked;
        public bool IsTime3DChecked
        {
            get { return isTime3DChecked; }
            set
            {
                if (isTime3DChecked != value)
                {
                    isTime3DChecked = value;
                    OnPropertyChanged("IsTime3DChecked");
                }
            }
        }
        #endregion

        #region IsRPM3DChecked
        private bool isRPM3DChecked;
        public bool IsRPM3DChecked
        {
            get { return isRPM3DChecked; }
            set
            {
                if (isRPM3DChecked != value)
                {
                    isRPM3DChecked = value;
                    OnPropertyChanged("IsRPM3DChecked");
                }
            }
        }
        #endregion

        #region IsShowNumericChart
        private bool isShowNumericChart = true;
        public bool IsShowNumericChart
        {
            get { return isShowNumericChart; }
            set
            {
                if (isShowNumericChart != value)
                {
                    isShowNumericChart = value;
                    OnPropertyChanged("IsShowNumericChart");
                }
            }
        }
        #endregion IsShowNumericChart

        private bool canOperated;
        public bool CanOperated
        {
            get { return canOperated; }
            set
            {
                canOperated = value;
                OnPropertyChanged("CanOperated");
            }
        }
        #endregion

        #region 私有变量
        private ImageValueDesigner imageVM = null;

        private TimeDomainChartViewModel timeDomainOnLineVM;
        private FrequencyDomainChartViewModel frequencyDomainOnLineVM;
        private RMSTrendChartViewModel amsTrendOnLineVM;
        private BodeChartViewModel bodeOnLineVM;
        private MultiDivFreChartViewModel multiDivFreOnLineVM;
        private NyquistChartViewModel nyquistOnLineVM;
        private OrderAnalysisChartViewModel orderAnalysisOnLineVM;
        private OrthoChartViewModel orthoOnLineVM;
        //private PropertyViewModel propertyVM;
        private RPM3DChartViewModel rpm3DSpectrumOnLineVM;
        private Time3DChartViewModel time3DSpectrumOnLineVM;
        private PowerSpectrumChartViewModel powerSpectrumOnLineVM;
        private PowerSpectrumDensityChartViewModel powerSpectrumDensityOnLineVM;

        private DeviceTreeItemViewModel deviceTreeItem;
        private DeviceElement currentEquipElement;        
        private DeviceDocument equipmentDocument;

        private IDisposable diagramItemsChangedSubscription;
        #endregion

        #region Command
        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                return this.addCommand ?? (this.addCommand = new DelegateCommand<object>(para => this.Add(para)));
            }
        }

        private ICommand addFilterCommand;
        public ICommand AddFilterCommand
        {
            get
            {
                return this.addFilterCommand ?? (this.addFilterCommand = new DelegateCommand<object>(para => this.AddFilter(para)));
            }
        }

        private ICommand removeFilterCommand;
        public ICommand RemoveFilterCommand
        {
            get
            {
                return this.removeFilterCommand ?? (this.removeFilterCommand = new DelegateCommand<object>(para => this.RemoveFilter(para)));
            }
        }

        private ICommand showPropertyCommand;
        public ICommand ShowPropertyCommand
        {
            get
            {
                return this.showPropertyCommand ?? (this.showPropertyCommand = new DelegateCommand<object>(para => this.ShowProperty(para), CanShowProperty));
            }
        }

        private ICommand addGraphFuncCommand;
        public ICommand AddGraphFuncCommand
        {
            get
            {
                return this.addGraphFuncCommand ?? (this.addGraphFuncCommand = new DelegateCommand<string>(para => this.AddGraphFunc(para)));
            }
        }

        private ICommand removeGraphFuncCommand;
        public ICommand RemoveGraphFuncCommand
        {
            get
            {
                return this.removeGraphFuncCommand ?? (this.removeGraphFuncCommand = new DelegateCommand<string>(para => this.RemoveGraphFunc(para)));
            }
        }

        private ICommand saveLayoutCommand;
        public ICommand SaveLayoutCommand
        {
            get
            {
                return this.saveLayoutCommand ?? (this.saveLayoutCommand = new DelegateCommand<string>(para => this.SaveLayout(para), para => CanOperate(para)));
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
        #endregion

        #region 初始化
        public Task Initialization { get; private set; }

        public void Initialize()
        {
            try
            {
                _signalProcess.SignalsAdded += _signalProcess_SignalAdded;
                _signalProcess.SignalsRemoved += _signalProcess_SignalRemoved;

                List<ChartViewModelBase> list = new List<ChartViewModelBase>();
               // timeDomainOnLineVM = new TimeDomainChartViewModel(SelectedSignal, true);
                //list.Add(timeDomainOnLineVM);
                //IsTimeDomainChecked = false;

                //frequencyDomainOnLineVM = new FrequencyDomainChartViewModel(SelectedSignal, true);
                //list.Add(frequencyDomainOnLineVM);
                //IsFrequencyDomainChecked = false;

                amsTrendOnLineVM = new RMSTrendChartViewModel(SelectedSignal, true);
                list.Add(amsTrendOnLineVM);
                IsAMSTrendChecked = true;

                graphFunctionCollection.AddItems(list);

                timeDomainOnLineVM = new TimeDomainChartViewModel(null, true);
                frequencyDomainOnLineVM = new FrequencyDomainChartViewModel(null, true);
                bodeOnLineVM = new BodeChartViewModel(null, true);
                multiDivFreOnLineVM = new MultiDivFreChartViewModel(null, true);
                nyquistOnLineVM = new NyquistChartViewModel(null, true);
                orderAnalysisOnLineVM = new OrderAnalysisChartViewModel(null, true);
                orthoOnLineVM = new OrthoChartViewModel(null, true);
                rpm3DSpectrumOnLineVM = new RPM3DChartViewModel(null, true);
                time3DSpectrumOnLineVM = new Time3DChartViewModel(null, true);
                powerSpectrumOnLineVM = new PowerSpectrumChartViewModel(null, true);
                powerSpectrumDensityOnLineVM = new PowerSpectrumDensityChartViewModel(null, true);

                LoadGroupDocument();

                
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-设备初始化失败", ex));
            }
        }

        private bool CanOperate(object para)
        {
            return CanOperated;
            //if (_loginUserService.LoginInfo.ServerInfo.Permission.Contains("admin") || _loginUserService.LoginInfo.ServerInfo.Permission.Contains("管理员"))
            //{
            //    CanOperated = true;
            //    return true;
            //}
            //else
            //{
            //    CanOperated = false;
            //    return false;
            //}
        }       
        #endregion


        #region 添加显示
        public void Add(object para)
        {
            DeviceTreeItemViewModel device = para as DeviceTreeItemViewModel;
            try
            {
                if (deviceTreeItem != device)
                {
                    deviceTreeItem = device;
                    DeviceName = device.Name;
                    //currentEquipElement = equipmentDocument.Equipments.Where(o => o.Name == device.Name && o.OrganizationName == device.FullName).SingleOrDefault();
                    currentEquipElement = equipmentDocument.Equipments.Where(o => (o.Name == device.Name && o.OrganizationName == device.FullName) || o.Guid == device.T_Organization.Guid).SingleOrDefault();
                    if (currentEquipElement == null)
                    {
                        DeviceElement eqElement = new DeviceElement()
                        {
                            OrganizationName = device.FullName,
                            Name = device.Name,
                            Guid = device.T_Organization.Guid,                           
                        };
                        eqElement.ImageDesignElement.Left = 350;
                        equipmentDocument.Equipments.Add(eqElement);
                        currentEquipElement = eqElement;
                    }
                    else if (currentEquipElement.Guid == null || currentEquipElement.Guid == new Guid())//为了兼容之前的
                    {
                        currentEquipElement.Guid = device.T_Organization.Guid;
                    }
                    else
                    {
                        if (currentEquipElement.OrganizationName != device.FullName)
                        {
                            currentEquipElement.OrganizationName = device.FullName;
                        }
                        if (currentEquipElement.Name != device.Name)
                        {
                            currentEquipElement.Name = device.Name;
                        }
                    }

                    BuildDevice(device);                    
                }
                
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-设备导航", ex));
            }
            finally
            {
                ItemIsExpanded = false;
            }
        }
        private void BuildDevice(DeviceTreeItemViewModel device)
        {
            try
            {
                double offset = 0;             
                if (diagramItemsChangedSubscription != null)
                {
                    diagramItemsChangedSubscription.Dispose();
                }

                if (imageVM == null)
                {
                    imageVM = new ImageValueDesigner();
                    imageVM.Top = 50;
                    imageVM.Left = 50;
                    DiagramViewModel.AddItemCommand.Execute(imageVM);
                }
                
                imageVM.ImageDesignElement = currentEquipElement.ImageDesignElement;

                var items = DiagramViewModel.Items.Except(new ImageValueDesigner[] { imageVM }).ToArray();
                foreach (var item in items)
                {
                    DiagramViewModel.RemoveItemCommand.Execute(item);
                }

                var bindeditems = device.Children.Where(p => ((ItemTreeItemViewModel)p).IsPaired).ToArray();
                if (bindeditems.Length > 0)
                {
                    for (int i = 0; i < bindeditems.Count(); i++)
                    {
                        ItemTreeItemViewModel tp = bindeditems[i] as ItemTreeItemViewModel;                        
                        var sg = tp.BaseAlarmSignal;//获取信号
                        if (sg == null)
                        {
                            continue;
                        }
                        if (sg is BaseAlarmSignal)
                        {
                            var newControl = new SignalValueDesigner(sg);
                            var designElement = currentEquipElement.DesignElements.Where(o => o.ID == sg.Guid.GetHashCode().ToString()).SingleOrDefault();
                            if (designElement == null)
                            {
                                if (i % 2 == 0)
                                {
                                    designElement = new DesignElement() { ID = sg.Guid.GetHashCode().ToString(), Left = 200, Top = offset, Width = 150, Height = 70 };
                                    currentEquipElement.DesignElements.Add(designElement);                                   
                                }
                                else
                                {
                                    designElement = new DesignElement() { ID = sg.Guid.GetHashCode().ToString(), Left = 360, Top = offset, Width = 150, Height = 70 };
                                    currentEquipElement.DesignElements.Add(designElement);
                                    offset += 70;
                                }
                            }
                            newControl.DesignElement = designElement;
                            DiagramViewModel.AddItemCommand.Execute(newControl);
                        }                        
                    }
                    var connectionElements = currentEquipElement.ConnectionElements.ToArray();
                    foreach (var elemnet in connectionElements)
                    {
                        FullyCreatedConnectorInfo sourceConnector = null;
                        FullyCreatedConnectorInfo sinkConnector = null;
                        var sourceItem = DiagramViewModel.Items.OfType<DesignerItemViewModelBase>().Where(o => o.Id == elemnet.SourceID).SingleOrDefault();
                        if (sourceItem != null)
                        {
                            sourceConnector = sourceItem.Connectors.Where(o => o.XRatio == elemnet.SourceXRatio && o.YRatio == elemnet.SourceYRatio).FirstOrDefault();
                        }
                        var sinkItem = DiagramViewModel.Items.OfType<DesignerItemViewModelBase>().Where(o => o.Id == elemnet.SinkID).SingleOrDefault();
                        if (sinkItem != null)
                        {
                            sinkConnector = sinkItem.Connectors.Where(o => o.XRatio == elemnet.SinkXRatio && o.YRatio == elemnet.SinkYRatio).FirstOrDefault();
                        }

                        if (sourceConnector != null && sinkConnector != null)
                        {
                            DiagramViewModel.AddItemCommand.Execute(new ConnectorViewModel(sourceConnector, sinkConnector));
                        }
                        else
                        {
                            currentEquipElement.ConnectionElements.Remove(elemnet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-构建设备", ex));
            }
            finally
            {
                diagramItemsChangedSubscription = DiagramViewModel.WhenItemsChanged.Subscribe(OnDiagramItemsChanged);
            }
        }       

        private void _signalProcess_SignalAdded(BaseAlarmSignal sg)
        {
            try
            {
                if (sg == null) return;
                if (currentEquipElement != null)
                {
                    if (sg.DeviceName == currentEquipElement.Name)
                    {
                        var newControl = new SignalValueDesigner(sg);
                        var designElement = currentEquipElement.DesignElements.Where(o => o.ID == sg.Guid.GetHashCode().ToString()).SingleOrDefault();
                        if (designElement == null)
                        {
                            designElement = new DesignElement() { ID = sg.Guid.GetHashCode().ToString(), Left = 0, Top = 0, Width = 150, Height = 70 };
                            currentEquipElement.DesignElements.Add(designElement);

                        }
                        newControl.DesignElement = designElement;
                        DiagramViewModel.AddItemCommand.Execute(newControl);
                    }
                }
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-设备测点添加", ex));
            }
        }
        private void _signalProcess_SignalRemoved(BaseAlarmSignal sg)
        {
            try
            {
                if (sg == null) return;
                var item = DiagramViewModel.Items.OfType<SignalValueDesigner>().Where(o => o.Signal == sg).SingleOrDefault();
                if (item != null)
                {
                    if (currentEquipElement != null)
                    {
                        if (item.DesignElement is DesignElement)
                        {
                            if (currentEquipElement.DesignElements.Contains(item.DesignElement))
                            {
                                currentEquipElement.DesignElements.Remove(item.DesignElement);
                            }
                        }                       
                    }

                    RemoveConnection(item);
                    DiagramViewModel.RemoveItemCommand.Execute(item);
                }
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-设备测点移除", ex));
            }
        }

        private void OnDiagramItemsChanged(NotifyCollectionChangedEventArgs args)
        {        
            try
            {
                if (deviceTreeItem != null && currentEquipElement != null)
                {
                    if (args.Action == NotifyCollectionChangedAction.Add)
                    {
                        if (args.NewItems != null)
                        {
                            foreach (var item in args.NewItems)
                            {
                                if (item is ConnectorViewModel && ((ConnectorViewModel)item).SinkConnectorInfo is FullyCreatedConnectorInfo)
                                {
                                    //Source
                                    var connectorVM = item as ConnectorViewModel;
                                    ConnectionElement element = new ConnectionElement();
                                    element.SourceID = connectorVM.SourceConnectorInfo.DataItem.Id;
                                    element.SourceOrientation = connectorVM.SourceConnectorInfo.Orientation.ToString();
                                    element.SourceXRatio = connectorVM.SourceConnectorInfo.XRatio;
                                    element.SourceYRatio = connectorVM.SourceConnectorInfo.YRatio;
                                    element.SinkID = ((FullyCreatedConnectorInfo)connectorVM.SinkConnectorInfo).DataItem.Id;
                                    element.SinkOrientation = ((FullyCreatedConnectorInfo)connectorVM.SinkConnectorInfo).Orientation.ToString();
                                    element.SinkXRatio = ((FullyCreatedConnectorInfo)connectorVM.SinkConnectorInfo).XRatio;
                                    element.SinkYRatio = ((FullyCreatedConnectorInfo)connectorVM.SinkConnectorInfo).YRatio;
                                    connectorVM.DesignElement = element;
                                    currentEquipElement.ConnectionElements.Add(element);
                                }
                            }
                        }
                    }

                    if (args.Action == NotifyCollectionChangedAction.Remove)
                    {
                        foreach (var item in args.OldItems)
                        {
                            if (item is ConnectorViewModel && ((ConnectorViewModel)item).SinkConnectorInfo is FullyCreatedConnectorInfo)
                            {
                                ConnectorViewModel connectorVM = item as ConnectorViewModel;
                                if (currentEquipElement.ConnectionElements.Contains(connectorVM.DesignElement))
                                {
                                    currentEquipElement.ConnectionElements.Remove(connectorVM.DesignElement);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //InteractionRequestService.Instance.InteractionRequest.Raise(new Confirmation() { Content = ex.ToString(), Title = "DiagramViewModel_ItemsChanged" }, confirm => { });
            }
        }

        private void OnSelectedItemChanged(string propertyName)
        {
            try
            {
                if (DiagramViewModel.SelectedItem is SignalValueDesigner)
                {
                    SelectedSignal = (DiagramViewModel.SelectedItem as SignalValueDesigner).Signal;
                    SignalValueSelected(SelectedSignal);
                }
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-选取信号", ex));
            }
        }

        private void SelectedTreeChanged(object para)
        {           
            if (para is ItemTreeItemViewModel)
            {
                ItemTreeItemViewModel item = para as ItemTreeItemViewModel;
                if (item == null || item.T_Item == null)
                {
                    return;
                }
                Add(item.Parent);

                ItemIsExpanded = true;
                
                //if (DiagramViewModel.SelectedItem != null && DiagramViewModel .SelectedItem is SignalValueDesigner && item.T_Item.Guid.GetHashCode().ToString() == DiagramViewModel.SelectedItem.Id)
                //{
                //    return;
                //}                

                foreach (var signalValueDesigner in DiagramViewModel.Items.OfType<SignalValueDesigner>())
                {
                    if (signalValueDesigner.Id == item.T_Item.Guid.GetHashCode().ToString())
                    {
                        signalValueDesigner.IsSelected = true;                       
                    }
                    else
                    {
                        signalValueDesigner.IsSelected = false;
                    }
                }               

                SignalValueSelected(item.BaseAlarmSignal);
               
            }
            else if (para is DeviceTreeItemViewModel)
            {
                Add(para);               
            }

        }
        #endregion

        #region 图表增减
        private void AddFilter(object args)
        {
            if (SelectedSignal is BaseWaveSignal)
            {
                if (args.ToString() == "Filter")
                {
                    ((BaseWaveSignal)SelectedSignal).AddProcess(SignalProcessorType.Filter);
                }
                else if (args.ToString() == "Envelop")
                {
                    ((BaseWaveSignal)SelectedSignal).AddProcess(SignalProcessorType.Envelope);
                }
                else if (args.ToString() == "TFF")
                {
                    ((BaseWaveSignal)SelectedSignal).AddProcess(SignalProcessorType.TFF);
                }
                else if (args.ToString() == "Cepstrum")
                {
                    ((BaseWaveSignal)SelectedSignal).AddProcess(SignalProcessorType.Cepstrum);
                }
            }
        }
        private void RemoveFilter(object args)
        {
            if (SelectedSignal is BaseWaveSignal)
            {
                if (args.ToString() == "Filter")
                {
                    ((BaseWaveSignal)SelectedSignal).RemoveProcess(SignalProcessorType.Filter);
                }
                else if (args.ToString() == "Envelop")
                {
                    ((BaseWaveSignal)SelectedSignal).RemoveProcess(SignalProcessorType.Envelope);
                }
                else if (args.ToString() == "TFF")
                {
                    ((BaseWaveSignal)SelectedSignal).RemoveProcess(SignalProcessorType.TFF);
                }
                else if (args.ToString() == "Cepstrum")
                {
                    ((BaseWaveSignal)SelectedSignal).RemoveProcess(SignalProcessorType.Cepstrum);
                }
            }
        }     
        private void SignalValueSelected(BaseAlarmSignal sg)
        {
            try
            {
                if (SelectedSignal is BaseWaveSignal)
                {                  
                    if (IsFilter)
                    {
                        RemoveFilterCommand.Execute("Filter");
                    }
                    if (IsEnvelope)
                    {
                        RemoveFilterCommand.Execute("Envelop");
                    }
                    else if (IsTFF)
                    {
                        RemoveFilterCommand.Execute("TFF");
                    }
                    else if (IsCepstrum)
                    {
                        RemoveFilterCommand.Execute("Cepstrum");
                    }
                }

                SelectedSignal = sg;

                if (SelectedSignal is BaseWaveSignal)
                {                  
                  
                    if (IsFilter)
                    {
                        AddFilterCommand.Execute("Filter");
                    }
                    if (IsEnvelope)
                    {
                        AddFilterCommand.Execute("Envelop");
                    }
                    else if (IsTFF)
                    {
                        AddFilterCommand.Execute("TFF");
                    }
                    else if (IsCepstrum)
                    {
                        AddFilterCommand.Execute("Cepstrum");
                    }
                }

                amsTrendOnLineVM.SetSignal(sg);

                timeDomainOnLineVM.SetSignal(sg);
                frequencyDomainOnLineVM.SetSignal(sg);
                bodeOnLineVM.SetSignal(sg);
                multiDivFreOnLineVM.SetSignal(sg);
                nyquistOnLineVM.SetSignal(sg);
                orderAnalysisOnLineVM.SetSignal(sg);
                orthoOnLineVM.SetSignal(sg);
                rpm3DSpectrumOnLineVM.SetSignal(sg);
                time3DSpectrumOnLineVM.SetSignal(sg);
                powerSpectrumOnLineVM.SetSignal(sg);
                powerSpectrumDensityOnLineVM.SetSignal(sg);
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-选取信号", ex));
            }
        }
        private void RemoveConnection(DesignerItemViewModelBase item)
        {
            var connections = DiagramViewModel.Items.OfType<ConnectorViewModel>().Where(o => o.SourceConnectorInfo.DataItem == item ||
                ((o.SinkConnectorInfo is FullyCreatedConnectorInfo) && (((FullyCreatedConnectorInfo)o.SinkConnectorInfo).DataItem == item))).ToArray();
            foreach (var connect in connections)
            {
                if (currentEquipElement != null && currentEquipElement.ConnectionElements.Contains(connect.DesignElement))
                {
                    currentEquipElement.ConnectionElements.Remove(connect.DesignElement);
                }
                DiagramViewModel.RemoveItemCommand.Execute(connect);
            }
        }
        private void AddGraphFunc(string graphType)
        {
            try
            {
                switch (graphType)
                {
                    case "TimeDomain":
                        if (!graphFunctionCollection.Contains(timeDomainOnLineVM))
                        {
                            graphFunctionCollection.Add(timeDomainOnLineVM);
                            IsTimeDomainChecked = true;
                        }
                        break;
                    case "FrequencyDomain":
                        if (!graphFunctionCollection.Contains(frequencyDomainOnLineVM))
                        {
                            graphFunctionCollection.Add(frequencyDomainOnLineVM);
                            IsFrequencyDomainChecked = true;
                        }
                        break;
                    case "AMSTrend":
                        if (!graphFunctionCollection.Contains(amsTrendOnLineVM))
                        {
                            graphFunctionCollection.Add(amsTrendOnLineVM);
                            IsAMSTrendChecked = true;
                        }
                        break;
                    case "MultiDivFre":
                        if (!graphFunctionCollection.Contains(multiDivFreOnLineVM))
                        {
                            graphFunctionCollection.Add(multiDivFreOnLineVM);
                            IsMultiDivFreChecked = true;
                        }
                        break;
                    case "Ortho":
                        if (!graphFunctionCollection.Contains(orthoOnLineVM))
                        {
                            graphFunctionCollection.Add(orthoOnLineVM);
                            IsOrthoChecked = true;
                        }
                        break;
                    case "Bode":
                        if (!graphFunctionCollection.Contains(bodeOnLineVM))
                        {
                            graphFunctionCollection.Add(bodeOnLineVM);
                            IsBodeChecked = true;
                        }
                        break;
                    case "Nyquist":
                        if (!graphFunctionCollection.Contains(nyquistOnLineVM))
                        {
                            graphFunctionCollection.Add(nyquistOnLineVM);
                            IsNyquistChecked = true;
                        }
                        break;
                    case "Order":
                        if (!graphFunctionCollection.Contains(orderAnalysisOnLineVM))
                        {
                            graphFunctionCollection.Add(orderAnalysisOnLineVM);
                            IsOrderChecked = true;
                        }
                        break;
                    case "Time3D":
                        if (!graphFunctionCollection.Contains(time3DSpectrumOnLineVM))
                        {
                            graphFunctionCollection.Add(time3DSpectrumOnLineVM);
                            IsTime3DChecked = true;
                        }
                        break;
                    case "RPM3D":
                        if (!graphFunctionCollection.Contains(rpm3DSpectrumOnLineVM))
                        {
                            graphFunctionCollection.Add(rpm3DSpectrumOnLineVM);
                            IsRPM3DChecked = true;
                        }
                        break;
                    case "PowerSpectrum":
                        if (!graphFunctionCollection.Contains(powerSpectrumOnLineVM))
                        {
                            graphFunctionCollection.Add(powerSpectrumOnLineVM);
                            
                        }
                        break;
                    case "PowerSpectrumDensity":
                        if (!graphFunctionCollection.Contains(powerSpectrumDensityOnLineVM))
                        {
                            graphFunctionCollection.Add(powerSpectrumDensityOnLineVM);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-添加图谱", ex));
            }
        }
        public void RemoveGraphFunc(string graphType)
        {
            try
            {
                switch (graphType)
                {
                    case "TimeDomain":
                        if (graphFunctionCollection.Contains(timeDomainOnLineVM))
                        {
                            timeDomainOnLineVM.Close();
                            graphFunctionCollection.Remove(timeDomainOnLineVM);
                            IsTimeDomainChecked = false;
                        }
                        break;
                    case "FrequencyDomain":
                        if (graphFunctionCollection.Contains(frequencyDomainOnLineVM))
                        {
                            frequencyDomainOnLineVM.Close();
                            graphFunctionCollection.Remove(frequencyDomainOnLineVM);
                            IsFrequencyDomainChecked = false;
                        }
                        break;
                    case "AMSTrend":
                        if (graphFunctionCollection.Contains(amsTrendOnLineVM))
                        {
                            amsTrendOnLineVM.Close();
                            graphFunctionCollection.Remove(amsTrendOnLineVM);
                            IsAMSTrendChecked = false;
                        }
                        break;
                    case "MultiDivFre":
                        if (graphFunctionCollection.Contains(multiDivFreOnLineVM))
                        {
                            multiDivFreOnLineVM.Close();
                            graphFunctionCollection.Remove(multiDivFreOnLineVM);
                            IsMultiDivFreChecked = false;
                        }
                        break;
                    case "Ortho":
                        if (graphFunctionCollection.Contains(orthoOnLineVM))
                        {
                            orthoOnLineVM.Close();
                            graphFunctionCollection.Remove(orthoOnLineVM);
                            IsOrthoChecked = false;
                        }
                        break;
                    case "Bode":
                        if (graphFunctionCollection.Contains(bodeOnLineVM))
                        {
                            bodeOnLineVM.Close();
                            graphFunctionCollection.Remove(bodeOnLineVM);
                            IsBodeChecked = false;
                        }
                        break;
                    case "Nyquist":
                        if (graphFunctionCollection.Contains(nyquistOnLineVM))
                        {
                            nyquistOnLineVM.Close();
                            graphFunctionCollection.Remove(nyquistOnLineVM);
                            IsNyquistChecked = false;
                        }
                        break;
                    case "Order":
                        if (graphFunctionCollection.Contains(orderAnalysisOnLineVM))
                        {
                            orderAnalysisOnLineVM.Close();
                            graphFunctionCollection.Remove(orderAnalysisOnLineVM);
                            IsOrthoChecked = false; 
                        }
                        break;
                    case "Time3D":
                        if (graphFunctionCollection.Contains(time3DSpectrumOnLineVM))
                        {
                            time3DSpectrumOnLineVM.Close();
                            graphFunctionCollection.Remove(time3DSpectrumOnLineVM);
                            IsTime3DChecked = false;
                        }
                        break;
                    case "RPM3D":
                        if (graphFunctionCollection.Contains(rpm3DSpectrumOnLineVM))
                        {
                            rpm3DSpectrumOnLineVM.Close();
                            graphFunctionCollection.Remove(rpm3DSpectrumOnLineVM);
                            IsRPM3DChecked = false;
                        }
                        break;
                    case "PowerSpectrum":
                        if (graphFunctionCollection.Contains(powerSpectrumOnLineVM))
                        {
                            powerSpectrumOnLineVM.Close();
                            graphFunctionCollection.Remove(powerSpectrumOnLineVM);
                        }
                        break;
                    case "PowerSpectrumDensity":
                        if (graphFunctionCollection.Contains(powerSpectrumDensityOnLineVM))
                        {
                            powerSpectrumDensityOnLineVM.Close();
                            graphFunctionCollection.Remove(powerSpectrumDensityOnLineVM);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-移除图谱", ex));
            }
        }

        public void ShowProperty(object arg)
        {
            //if (propertyVM == null)
            //{
            //    propertyVM = new PropertyViewModel(SelectedSignal);
            //    propertyVM.ItemHeight = 200;
            //    propertyVM.ItemWidth = 600;
            //    DiagramViewModel.Items.Add(propertyVM);

            //}
        }
        public bool CanShowProperty(object arg)
        {
            return SelectedSignal != null;
        }

        #endregion

        #region 布局保存与读取
        private void LoadGroupDocument()
        {
            try
            {
                string layoutPath = LocalAddress.LayoutPath;
                //string layoutPath = @"C:\AIC\布局\DeviceDocument.xml";// System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\DeviceDocument.xml";
                if (File.Exists(@layoutPath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(DeviceDocument));
                    FileInfo fileInfo = new FileInfo(layoutPath);

                    using (TextReader reader = fileInfo.OpenText())
                    {
                        equipmentDocument = (DeviceDocument)serializer.Deserialize(reader);
                    }
                }
                else
                {
                    equipmentDocument = new DeviceDocument();
                }
            }
            catch (System.IO.FileNotFoundException fnfe)
            {
                throw new FileNotFoundException("The system document could not be found ", fnfe);
            }
            catch (System.IO.DirectoryNotFoundException dnfe)
            {
                throw new DirectoryNotFoundException("A required directory was nt found", dnfe);
            }
            catch (System.IO.IOException ioe)
            {
                throw new IOException("A file system error occurred", ioe);
            }
            catch (System.UnauthorizedAccessException uae)
            {
                throw new UnauthorizedAccessException("The requested file system access wasnot granted", uae);
            }
            catch (System.Security.SecurityException se)
            {
                throw new System.Security.SecurityException("The security policy prevents access to a file system resource", se);
            }
            catch (System.Exception e)
            {
                throw new System.Exception(
                    string.Format("The database format vc  invalid \r\n Exception:{0} \r\n InnerException:{1}", e.Message, e.InnerException.Message));
            }
        }
        public void SaveLayout(object arg)
        {
            try
            {
                string layoutPath = LocalAddress.LayoutPath;
                var filename = layoutPath.Substring(layoutPath.LastIndexOf("\\"));
                var direcory = layoutPath.Substring(0, layoutPath.Length - filename.Length);
                //string direcory = @"C:\AIC\布局";
                if (!Directory.Exists(@direcory))
                {
                    Directory.CreateDirectory(@direcory);
                }
                FileInfo file = new FileInfo(direcory + "\\" + "DeviceDocument.xml");
                equipmentDocument.Save(file);
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-保存布局", ex));
            }
        }
        #endregion












    }
}

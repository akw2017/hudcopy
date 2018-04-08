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
using AIC.DiagnosePage.Models;
using Newtonsoft.Json;
using System.IO;
using OpenXmlPowerTools;
using System.Xml.Linq;
using AIC.DiagnosePage.TestDatas;
using AIC.DiagnosePage.Views;
using AIC.PDAPage.Models;

namespace AIC.DiagnosePage.ViewModels
{
    class EditDeviceComponentsViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        private readonly IDatabaseComponent _databaseComponent;

        public EditDeviceComponentsViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess, IDatabaseComponent databaseComponent)
        {
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _signalProcess = signalProcess;
            _cardProcess = cardProcess;
            _databaseComponent = databaseComponent;

            InitTree();
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

        private DeviceDiagnosisModel selectedDeviceDiagnosisModel;
        public DeviceDiagnosisModel SelectedDeviceDiagnosisModel
        {
            get { return selectedDeviceDiagnosisModel; }
            set
            {
                if (selectedDeviceDiagnosisModel != value)
                {
                    selectedDeviceDiagnosisModel = value;
                    OnPropertyChanged("SelectedDeviceDiagnosisModel");
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

        private ICommand addComponentCommand;
        public ICommand AddComponentCommand
        {
            get
            {
                return this.addComponentCommand ?? (this.addComponentCommand = new DelegateCommand<object>(para => this.AddComponent(para)));
            }
        }


        private ICommand addShaftCommand;
        public ICommand AddShaftCommand
        {
            get
            {
                return this.addShaftCommand ?? (this.addShaftCommand = new DelegateCommand<object>(para => this.AddShaft(para)));
            }
        }

        private ICommand deleteShaftCommand;
        public ICommand DeleteShaftCommand
        {
            get
            {
                return this.deleteShaftCommand ?? (this.deleteShaftCommand = new DelegateCommand<object>(para => this.DeleteShaft(para)));
            }
        }

        private ICommand deleteComponentCommand;
        public ICommand DeleteComponentCommand
        {
            get
            {
                return this.deleteComponentCommand ?? (this.deleteComponentCommand = new DelegateCommand<object>(para => this.DeleteComponent(para)));
            }
        }

        private ICommand editShaftCommand;
        public ICommand EditShaftCommand
        {
            get
            {
                return this.editShaftCommand ?? (this.editShaftCommand = new DelegateCommand<object>(para => this.EditShaft(para)));
            }
        }

        private ICommand editComponentCommand;
        public ICommand EditComponentCommand
        {
            get
            {
                return this.editComponentCommand ?? (this.editComponentCommand = new DelegateCommand<object>(para => this.EditComponent(para)));
            }
        }

        private ICommand deviceEditCommand;
        public ICommand DeviceEditCommand
        {
            get
            {
                return this.deviceEditCommand ?? (this.deviceEditCommand = new DelegateCommand<object>(para => this.DeviceEdit(para)));
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
        }

        public void Init(DeviceDiagnosisModel device)
        {
            SelectedDeviceDiagnosisModel = device;
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
        #endregion

        #region 编辑模型
        private void AddShaft(object para)
        {
            DeviceDiagnosisModel deviceclass = para as DeviceDiagnosisModel;
            if (deviceclass != null && deviceclass.SelectedShaft != null)
            {
                int index = deviceclass.Shafts.IndexOf(deviceclass.SelectedShaft);
                deviceclass.Shafts.Insert(index, new Models.ShaftComponent()
                {
                    Component = new ShaftClassExamples().GetShaftClass2(deviceclass),
                    ID = Guid.NewGuid(),
                    Name = "后轴",
                });
            }
            else
            {
                deviceclass.Shafts.Add(new Models.ShaftComponent()
                {
                    Component = new ShaftClassExamples().GetShaftClass1(deviceclass),
                    ID = Guid.NewGuid(),
                    Name = "后轴",
                });
            }
        }

        private void DeleteShaft(object para)
        {
            DeviceDiagnosisModel deviceclass = para as DeviceDiagnosisModel;
            if (deviceclass != null && deviceclass.SelectedShaft != null)
            {
                deviceclass.Shafts.Remove(deviceclass.SelectedShaft);
            }
        }

        private void EditShaft(object para)
        {
            DeviceDiagnosisModel deviceclass = para as DeviceDiagnosisModel;
            if (deviceclass != null && deviceclass.SelectedShaft != null)
            {
                EditShaftComponentsWin win = new Views.EditShaftComponentsWin(deviceclass.SelectedShaft);
                win.ShowDialog();
            }
        }

        ShaftComponent selectedshaft;
        private void AddComponent(object para)
        {
            selectedshaft = para as ShaftComponent;
            if (selectedshaft != null)
            {
                EditMachComponentsWin win = new Views.EditMachComponentsWin(null);
                win.Parachanged += Component_Parachanged;
                win.ShowDialog();
            }
        }       

        private void DeleteComponent(object para)
        {
            selectedshaft = para as ShaftComponent;
            if (selectedshaft != null && selectedshaft.Component.SelectedComponent != null)
            {
                EditMachComponentsWin win = new Views.EditMachComponentsWin(selectedshaft.Component.SelectedComponent, EditOperateType.Delete);
                win.Parachanged += Component_Parachanged;
                win.ShowDialog();
            }
        }

        private void EditComponent(object para)
        {
            selectedshaft = para as ShaftComponent;
            if (selectedshaft != null && selectedshaft.Component.SelectedComponent != null)
            {
                EditMachComponentsWin win = new Views.EditMachComponentsWin(selectedshaft.Component.SelectedComponent, EditOperateType.Modify);
                win.Parachanged += Component_Parachanged;
                win.ShowDialog();
            }
        }

        private void Component_Parachanged(IMachComponent i_component, EditOperateType operateType)
        {
            switch (operateType)
            {
                case EditOperateType.Add:
                    {
                        if (selectedshaft != null && selectedshaft.Component.SelectedComponent != null)
                        {
                            int index = selectedshaft.Component.MachComponents.IndexOf(selectedshaft.Component.SelectedComponent);
                            selectedshaft.Component.MachComponents.Insert(index, i_component);
                        }
                        else
                        {
                            selectedshaft.Component.MachComponents.Add(i_component);
                        }
                        break;
                    }
                case EditOperateType.Delete:
                    {
                        if (selectedshaft != null && selectedshaft.Component.SelectedComponent != null)
                        {

                            selectedshaft.Component.MachComponents.Remove(selectedshaft.Component.SelectedComponent);
                        }
                        break;
                    }
            }
        }

        private void DeviceEdit(object para)
        {
            DeviceDiagnosisModel device = para as DeviceDiagnosisModel;
            if (device != null)
            {
                EditDeviceComponentsWin win = new Views.EditDeviceComponentsWin();
                win.Show();
            }
        }

        private void DeviceChanged(object para)
        {

        }
        #endregion
    }
}
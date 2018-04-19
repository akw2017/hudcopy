using AIC.Core.DiagnosticBaseModels;
using AIC.CoreType;
using AIC.PDAPage.Models;
using AIC.ServiceInterface;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AIC.DiagnosePage.ViewModels
{
    class EditMachComponentViewModel : BindableBase, INavigationAware
    {

        private readonly IDeviceDiagnoseTemplateService _deviceDiagnoseTemplateService;
        public EditMachComponentViewModel(IDeviceDiagnoseTemplateService deviceDiagnoseTemplateService)
        {
            _deviceDiagnoseTemplateService = deviceDiagnoseTemplateService;

            if (Bearings == null)
            {
                Bearings = _deviceDiagnoseTemplateService.BearingClassList;
            }
            if (Belts == null)
            {
                Belts = _deviceDiagnoseTemplateService.BeltClassList;
            }
            if (Gears == null)
            {
                Gears = _deviceDiagnoseTemplateService.GearClassList;
            }
            if (Impellers == null)
            {
                Impellers = _deviceDiagnoseTemplateService.ImpellerClassList;
            }
            if (Motors == null)
            {
                Motors = _deviceDiagnoseTemplateService.MotorClassList;
            }
        }

        #region 属性与字段
        private BearingComponent bearingComponent;
        public BearingComponent BearingComponent
        {
            get { return bearingComponent; }
            set
            {
                bearingComponent = value;
                OnPropertyChanged("BearingComponent");
            }
        }

        private BeltComponent beltComponent;
        public BeltComponent BeltComponent
        {
            get { return beltComponent; }
            set
            {
                beltComponent = value;
                OnPropertyChanged("BeltComponent");
            }
        }

        private GearComponent gearComponent;
        public GearComponent GearComponent
        {
            get { return gearComponent; }
            set
            {
                gearComponent = value;
                OnPropertyChanged("GearComponent");
            }
        }

        private ImpellerComponent impellerComponent;
        public ImpellerComponent ImpellerComponent
        {
            get { return impellerComponent; }
            set
            {
                impellerComponent = value;
                OnPropertyChanged("ImpellerComponent");
            }
        }

        private MotorComponent motorComponent;
        public MotorComponent MotorComponent
        {
            get { return motorComponent; }
            set
            {
                motorComponent = value;
                OnPropertyChanged("MotorComponent");
            }
        }

        private DeviceComponentType componentType;
        public DeviceComponentType ComponentType
        {
            get { return componentType; }
            set
            {
                if (componentType != value)
                {
                    if (isNavigated == true)
                    {
                        componentType = value;
                        OnPropertyChanged("ComponentType");
                    }
                    else
                    {
#if XBAP
                        MessageBoxResult result = MessageBox.Show("确定要重建" + selectedshaft.Component.SelectedComponent.Name + "的组件类型?", "重建组件", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                        MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show("确定要重建" + selectedshaft.Component.SelectedComponent.Name + "的组件类型? ", "重建组件", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
#endif
                        if (result == MessageBoxResult.OK)
                        {
                            componentType = value;
                            OnPropertyChanged("ComponentType");
                            ChangedComponentType();
                        }
                    }
                }
                isNavigated = false;
            }
        }

        private ObservableCollection<BearingClass> bearings;
        public ObservableCollection<BearingClass> Bearings
        {
            get { return bearings; }
            set
            {
                bearings = value;
                OnPropertyChanged("Bearings");
            }
        }

        private ObservableCollection<BeltClass> belts;
        public ObservableCollection<BeltClass> Belts
        {
            get { return belts; }
            set
            {
                belts = value;
                OnPropertyChanged("Belts");
            }
        }

        private ObservableCollection<GearClass> gears;
        public ObservableCollection<GearClass> Gears
        {
            get { return gears; }
            set
            {
                gears = value;
                OnPropertyChanged("Gears");
            }
        }

        private ObservableCollection<ImpellerClass> impellers;
        public ObservableCollection<ImpellerClass> Impellers
        {
            get { return impellers; }
            set
            {
                impellers = value;
                OnPropertyChanged("Impellers");
            }
        }

        private ObservableCollection<MotorClass> motors;
        public ObservableCollection<MotorClass> Motors
        {
            get { return motors; }
            set
            {
                motors = value;
                OnPropertyChanged("Motors");
            }
        }
        #endregion

        #region 命令
        private ICommand addComponentCommand;
        public ICommand AddComponentCommand
        {
            get
            {
                return this.addComponentCommand ?? (this.addComponentCommand = new DelegateCommand(() => this.AddComponent()));
            }
        }

     
        private ICommand deleteComponentCommand;
        public ICommand DeleteComponentCommand
        {
            get
            {
                return this.deleteComponentCommand ?? (this.deleteComponentCommand = new DelegateCommand(() => this.DeleteComponent()));
            }
        }

        private ICommand selectedBearingChangedComamnd;
        public ICommand SelectedBearingChangedComamnd
        {
            get
            {
                return this.selectedBearingChangedComamnd ?? (this.selectedBearingChangedComamnd = new DelegateCommand<object>(para => this.SelectedBearingChanged(para)));
            }
        }

        private ICommand selectedBeltChangedComamnd;
        public ICommand SelectedBeltChangedComamnd
        {
            get
            {
                return this.selectedBeltChangedComamnd ?? (this.selectedBeltChangedComamnd = new DelegateCommand<object>(para => this.SelectedBeltChanged(para)));
            }
        }

        private ICommand selectedGearChangedComamnd;
        public ICommand SelectedGearChangedComamnd
        {
            get
            {
                return this.selectedGearChangedComamnd ?? (this.selectedGearChangedComamnd = new DelegateCommand<object>(para => this.SelectedGearChanged(para)));
            }
        }

        private ICommand selectedMotorChangedComamnd;
        public ICommand SelectedMotorChangedComamnd
        {
            get
            {
                return this.selectedMotorChangedComamnd ?? (this.selectedMotorChangedComamnd = new DelegateCommand<object>(para => this.SelectedMotorChanged(para)));
            }
        }

        private ICommand selectedImpellerChangedComamnd;
        public ICommand SelectedImpellerChangedComamnd
        {
            get
            {
                return this.selectedImpellerChangedComamnd ?? (this.selectedImpellerChangedComamnd = new DelegateCommand<object>(para => this.SelectedImpellerChanged(para)));
            }
        }
        #endregion

        #region 编辑
        private void AddComponent()
        {
            IMachComponent component = null;
            switch (ComponentType)
            {
                case DeviceComponentType.Bearing:
                    {
                        component = new BearingComponent() { Component = new BearingClass() { Guid = Guid.NewGuid() }, Guid = Guid.NewGuid(), Name = "新建轴承" };
                        BearingComponent = component as BearingComponent;
                        break;
                    }
                case DeviceComponentType.Belt:
                    {
                        component = new BeltComponent() { Component = new BeltClass(), Guid = Guid.NewGuid(), Name = "新建皮带" };
                        BeltComponent = component as BeltComponent;
                        break;
                    }
                case DeviceComponentType.Gear:
                    {
                        component = new GearComponent() { Component = new GearClass(), Guid = Guid.NewGuid(), Name = "新建齿轮" };
                        GearComponent = component as GearComponent;
                        break;
                    }
                case DeviceComponentType.Impeller:
                    {
                        component = new ImpellerComponent() { Component = new ImpellerClass(), Guid = Guid.NewGuid(), Name = "新建叶轮" };
                        ImpellerComponent = component as ImpellerComponent;
                        break;
                    }
                case DeviceComponentType.Motor:
                    {
                        component = new MotorComponent() { Component = new MotorClass(), Guid = Guid.NewGuid(), Name = "新建电机" };
                        MotorComponent = component as MotorComponent;
                        break;
                    }
            }
            if (component != null)
            {
                selectedshaft.Component.MachComponents.Add(component);
                selectedshaft.Component.SelectedComponent = component;                
            }
        }

        private void DeleteComponent()
        {
            if (selectedshaft != null && selectedshaft.Component.SelectedComponent != null)
            {
#if XBAP
                MessageBoxResult result = MessageBox.Show("确定要删除" + selectedshaft.Component.SelectedComponent.Name + "?", "删除", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show("确定要删除" + selectedshaft.Component.SelectedComponent.Name + "?", "删除", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
#endif
                if (result == MessageBoxResult.OK)
                {
                    selectedshaft.Component.MachComponents.Remove(selectedshaft.Component.SelectedComponent);
                }
            }
        }

        private void ChangedComponentType()
        {            
            IMachComponent component = null;
            switch (ComponentType)
            {
                case DeviceComponentType.Bearing:
                    {
                        component = new BearingComponent() { Component = new BearingClass() { Guid = Guid.NewGuid() }, Guid = Guid.NewGuid(), Name = "新建轴承" };
                        break;
                    }
                case DeviceComponentType.Belt:
                    {
                        component = new BeltComponent() { Component = new BeltClass(), Guid = Guid.NewGuid(), Name = "新建皮带" };
                        break;
                    }
                case DeviceComponentType.Gear:
                    {
                        component = new GearComponent() { Component = new GearClass(), Guid = Guid.NewGuid(), Name = "新建齿轮" };
                        break;
                    }
                case DeviceComponentType.Impeller:
                    {
                        component = new ImpellerComponent() { Component = new ImpellerClass(), Guid = Guid.NewGuid(), Name = "新建叶轮" };
                        break;
                    }
                case DeviceComponentType.Motor:
                    {
                        component = new MotorComponent() { Component = new MotorClass(), Guid = Guid.NewGuid(), Name = "新建电机" };
                        break;
                    }
            }
            if (component != null)
            {
                //ComponentType = component.ComponentType;
                var oldcomponent = selectedshaft.Component.SelectedComponent;
                int index = selectedshaft.Component.MachComponents.IndexOf(oldcomponent);                
                selectedshaft.Component.MachComponents.Insert(index, component);
                selectedshaft.Component.MachComponents.Remove(oldcomponent);
                selectedshaft.Component.SelectedComponent = component;
            }
        }

        private void SelectedBearingChanged(object para)
        {
            BearingClass componentclass = para as BearingClass;
            if (componentclass != null)
            {
                selectedshaft.Component.SelectedComponent.Component = componentclass.DeepClone();
            }
        }

        private void SelectedBeltChanged(object para)
        {
            BeltClass componentclass = para as BeltClass;
            if (componentclass != null)
            {
                selectedshaft.Component.SelectedComponent.Component = componentclass.DeepClone();
            }
        }

        private void SelectedGearChanged(object para)
        {
            GearClass componentclass = para as GearClass;
            if (componentclass != null)
            {
                selectedshaft.Component.SelectedComponent.Component = componentclass.DeepClone();
            }
        }

        private void SelectedMotorChanged(object para)
        {
            MotorClass componentclass = para as MotorClass;
            if (componentclass != null)
            {
                selectedshaft.Component.SelectedComponent.Component = componentclass.DeepClone();
            }
        }

        private void SelectedImpellerChanged(object para)
        {
            ImpellerClass componentclass = para as ImpellerClass;
            if (componentclass != null)
            {
                selectedshaft.Component.SelectedComponent.Component = componentclass.DeepClone();
            }
        }
        #endregion

        #region 导航
        private ShaftComponent selectedshaft;
        private bool isNavigated = false;
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            isNavigated = true;
            var navigationService = navigationContext.NavigationService;
            selectedshaft = navigationContext.Parameters["ShaftComponent"] as ShaftComponent;
            if (selectedshaft != null && selectedshaft.Component != null && selectedshaft.Component.SelectedComponent != null)
            {                
                switch (selectedshaft.Component.SelectedComponent.ComponentType)
                {
                    case DeviceComponentType.Bearing: BearingComponent = selectedshaft.Component.SelectedComponent as BearingComponent; ComponentType = BearingComponent.ComponentType; break;
                    case DeviceComponentType.Belt: BeltComponent = selectedshaft.Component.SelectedComponent as BeltComponent; ComponentType = BeltComponent.ComponentType; break;
                    case DeviceComponentType.Gear: GearComponent = selectedshaft.Component.SelectedComponent as GearComponent; ComponentType = GearComponent.ComponentType; break;
                    case DeviceComponentType.Impeller: ImpellerComponent = selectedshaft.Component.SelectedComponent as ImpellerComponent; ComponentType = ImpellerComponent.ComponentType; break;
                    case DeviceComponentType.Motor: MotorComponent = selectedshaft.Component.SelectedComponent as MotorComponent; ComponentType = MotorComponent.ComponentType; break;
                }
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }
        #endregion
    }
}

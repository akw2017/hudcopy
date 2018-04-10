using AIC.Core.DiagnosticBaseModels;
using AIC.CoreType;
using AIC.PDAPage.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AIC.DiagnosePage.ViewModels
{
    class EditMachComponentViewModel : BindableBase, INavigationAware
    {


        public EditMachComponentViewModel()
        {
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
                    componentType = value;
                    OnPropertyChanged("ComponentType");
                    ChangedComponentType();
                }
                isNavigated = false;
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

        private ICommand changedComponentTypeCommand;
        public ICommand ChangedComponentTypeCommand
        {
            get
            {
                return this.changedComponentTypeCommand ?? (this.changedComponentTypeCommand = new DelegateCommand(() => this.ChangedComponentType()));
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
                        component = new BearingComponent() { Component = new BearingClass() { BearingID = Guid.NewGuid() }, ID = Guid.NewGuid(), Name = "新建轴承" };
                        BearingComponent = component as BearingComponent;
                        break;
                    }
                case DeviceComponentType.Belt:
                    {
                        component = new BeltComponent() { Component = new BeltClass(), ID = Guid.NewGuid(), Name = "新建皮带" };
                        BeltComponent = component as BeltComponent;
                        break;
                    }
                case DeviceComponentType.Gear:
                    {
                        component = new GearComponent() { Component = new GearClass(), ID = Guid.NewGuid(), Name = "新建齿轮" };
                        GearComponent = component as GearComponent;
                        break;
                    }
                case DeviceComponentType.Impeller:
                    {
                        component = new ImpellerComponent() { Component = new ImpellerClass(), ID = Guid.NewGuid(), Name = "新建叶轮" };
                        ImpellerComponent = component as ImpellerComponent;
                        break;
                    }
                case DeviceComponentType.Motor:
                    {
                        component = new MotorComponent() { Component = new MotorClass(), ID = Guid.NewGuid(), Name = "新建电机" };
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
            if (isNavigated == true)
            {               
                return;
            }
#if XBAP
            MessageBoxResult result = MessageBox.Show("确定要重建" + selectedshaft.Component.SelectedComponent.Name + "的组件类型?", "重建组件", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show("确定要重建" + selectedshaft.Component.SelectedComponent.Name + "的组件类型? ", "重建组件", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
#endif
            if (result == MessageBoxResult.OK)
            {
                IMachComponent component = null;
                switch (ComponentType)
                {
                    case DeviceComponentType.Bearing:
                        {
                            component = new BearingComponent() { Component = new BearingClass() { BearingID = Guid.NewGuid() }, ID = Guid.NewGuid(), Name = "新建轴承" };
                            break;
                        }
                    case DeviceComponentType.Belt:
                        {
                            component = new BeltComponent() { Component = new BeltClass(), ID = Guid.NewGuid(), Name = "新建皮带" };
                            break;
                        }
                    case DeviceComponentType.Gear:
                        {
                            component = new GearComponent() { Component = new GearClass(), ID = Guid.NewGuid(), Name = "新建齿轮" };
                            break;
                        }
                    case DeviceComponentType.Impeller:
                        {
                            component = new ImpellerComponent() { Component = new ImpellerClass(), ID = Guid.NewGuid(), Name = "新建叶轮" };
                            break;
                        }
                    case DeviceComponentType.Motor:
                        {
                            component = new MotorComponent() { Component = new MotorClass(), ID = Guid.NewGuid(), Name = "新建电机" };
                            break;
                        }
                }
                if (component != null)
                {
                    //ComponentType = component.ComponentType;
                    int index = selectedshaft.Component.MachComponents.IndexOf(selectedshaft.Component.SelectedComponent);
                    selectedshaft.Component.MachComponents.Remove(selectedshaft.Component.SelectedComponent);
                    selectedshaft.Component.MachComponents.Insert(index, component);
                    selectedshaft.Component.SelectedComponent = component;
                }
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

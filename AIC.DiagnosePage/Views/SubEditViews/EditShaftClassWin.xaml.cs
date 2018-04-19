using AIC.Core.DiagnosticBaseModels;
using MahApps.Metro.Controls;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AIC.CoreType;
using AIC.ServiceInterface;
using Microsoft.Practices.ServiceLocation;

namespace AIC.DiagnosePage.Views
{
    /// <summary>
    /// EditBearingClassWin.xaml 的交互逻辑
    /// </summary>
    public partial class EditShaftClassWin : MetroWindow
    {

        private static IDeviceDiagnoseTemplateService _deviceDiagnoseTemplateService;

        public EditShaftClassWin(ShaftClass component)
        {
            InitializeComponent();

            _deviceDiagnoseTemplateService = ServiceLocator.Current.GetInstance<IDeviceDiagnoseTemplateService>();
            Component = component;
            Templates = new ObservableCollection<IMach>(_deviceDiagnoseTemplateService.BearingClassList.Select(p => p as IMach));

            this.DataContext = this;
        }

        #region 属性与字段

        private DeviceComponentType componentType = DeviceComponentType.Bearing;
        public DeviceComponentType ComponentType
        {
            get { return componentType; }
            set
            {
                if (componentType != value)
                {
                    componentType = value;
                    OnPropertyChanged("ComponentType");
                    ShiftTemplate(componentType);
                }
            }
        }

        private ObservableCollection<IMach> templates = new ObservableCollection<IMach>();
        public ObservableCollection<IMach> Templates
        {
            get { return templates; }
            set
            {
                templates = value;
                OnPropertyChanged("Templates");
            }
        }

        private IMach selectedTemplate;
        public IMach SelectedTemplate
        {
            get { return selectedTemplate; }
            set
            {
                selectedTemplate = value;
                OnPropertyChanged("SelectedTemplate");
            }
        }

        private string newName;
        public string NewName
        {
            get { return newName; }
            set
            {
                newName = value;
                OnPropertyChanged("NewName");
            }
        }

        public ShaftClass Component { get; set; }
        #endregion

        #region 命令
        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                return this.addCommand ?? (this.addCommand = new DelegateCommand(() => this.Add()));
            }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return this.deleteCommand ?? (this.deleteCommand = new DelegateCommand(() => this.Delete()));
            }
        }
        #endregion

        #region 编辑
        private void Add()
        {
            if (NewName == null || NewName == "")
            {
                NewName = "新建组件";
            }
            if (SelectedTemplate != null)
            {
                switch (ComponentType)
                {
                    case DeviceComponentType.Bearing:
                        Component.AddBearingComponent(new BearingComponent() { Name = NewName, Component = SelectedTemplate as BearingClass });
                        break;
                    case DeviceComponentType.Belt:
                        Component.AddBeltComponent(new BeltComponent() { Name = NewName, Component = SelectedTemplate as BeltClass });
                        break;
                    case DeviceComponentType.Gear:
                        Component.AddGearComponent(new GearComponent() { Name = NewName, Component = SelectedTemplate as GearClass });
                        break;
                    case DeviceComponentType.Impeller:
                        Component.AddImpellerComponent(new ImpellerComponent() { Name = NewName, Component = SelectedTemplate as ImpellerClass });
                        break;
                    case DeviceComponentType.Motor:
                        Component.AddMotorComponent(new MotorComponent() { Name = NewName, Component = SelectedTemplate as MotorClass });
                        break;
                }
            }
        }

        private void Delete()
        {
            switch (Component.SelectedComponent.ComponentType)
            {
                case DeviceComponentType.Bearing:
                    Component.RemoveBearingComponent(Component.SelectedComponent as BearingComponent);
                    break;
                case DeviceComponentType.Belt:
                    Component.RemoveBeltComponent(Component.SelectedComponent as BeltComponent);
                    break;
                case DeviceComponentType.Gear:
                    Component.RemoveGearComponent(Component.SelectedComponent as GearComponent);
                    break;
                case DeviceComponentType.Impeller:
                    Component.RemoveImpellerComponent(Component.SelectedComponent as ImpellerComponent);
                    break;
                case DeviceComponentType.Motor:
                    Component.RemoveMotorComponent(Component.SelectedComponent as MotorComponent);
                    break;
            }
           
        }

        private void ShiftTemplate(DeviceComponentType type)
        {
            switch (type)
            {
                case DeviceComponentType.Bearing:
                    Templates.Clear();
                    Templates.AddRange(_deviceDiagnoseTemplateService.BearingClassList.Select(p => p as IMach));
                    SelectedTemplate = null;
                    break;
                case DeviceComponentType.Belt:
                    Templates.Clear();
                    Templates.AddRange(_deviceDiagnoseTemplateService.BeltClassList.Select(p => p as IMach));
                    SelectedTemplate = null;
                    break;
                case DeviceComponentType.Gear:
                    Templates.Clear();
                    Templates.AddRange(_deviceDiagnoseTemplateService.GearClassList.Select(p => p as IMach));
                    SelectedTemplate = null;
                    break;
                case DeviceComponentType.Impeller:
                    Templates.Clear();
                    Templates.AddRange(_deviceDiagnoseTemplateService.ImpellerClassList.Select(p => p as IMach));
                    SelectedTemplate = null;
                    break;
                case DeviceComponentType.Motor:
                    Templates.Clear();
                    Templates.AddRange(_deviceDiagnoseTemplateService.MotorClassList.Select(p => p as IMach));
                    SelectedTemplate = null;
                    break;
            }
        }
        #endregion

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

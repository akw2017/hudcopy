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
using AIC.DiagnosePage.TestDatas;

namespace AIC.DiagnosePage.Views
{
    /// <summary>
    /// EditBearingClassWin.xaml 的交互逻辑
    /// </summary>
    public partial class EditShaftClassWin : MetroWindow
    {
        public EditShaftClassWin(ShaftClass component)
        {
            InitializeComponent();

            Component = component;
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

        private ObservableCollection<IMachComponent> templates;
        public ObservableCollection<IMachComponent> Templates
        {
            get { return templates; }
            set
            {
                templates = value;
                OnPropertyChanged("Templates");
            }
        }

        private IMachComponent selectedTemplate;
        public IMachComponent SelectedTemplate
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
                NewName = "新建轴";
            }
            if (SelectedTemplate != null)
            {
                Component.MachComponents.Add(SelectedTemplate);
            }
        }

        private void Delete()
        {

        }

        private void ShiftTemplate(DeviceComponentType type)
        {
            switch (type)
            {
                case DeviceComponentType.Bearing:
                    Templates = new ObservableCollection<IMachComponent>(BearClassExamples.BearingClassLib.Select(p => p as IMachComponent));
                    SelectedTemplate = null;
                    break;
                case DeviceComponentType.Belt:
                    Templates = new ObservableCollection<IMachComponent>(BeltClassExamples.BeltClassLib.Select(p => p as IMachComponent));
                    SelectedTemplate = null;
                    break;
                case DeviceComponentType.Gear:
                    Templates = new ObservableCollection<IMachComponent>(GearClassExamples.GearClassLib.Select(p => p as IMachComponent));
                    SelectedTemplate = null;
                    break;
                case DeviceComponentType.Impeller:
                    Templates = new ObservableCollection<IMachComponent>(ImpellerClassExamples.ImpellerClassLib.Select(p => p as IMachComponent));
                    SelectedTemplate = null;
                    break;
                case DeviceComponentType.Motor:
                    Templates = new ObservableCollection<IMachComponent>(MotorClassExamples.MotorClassLib.Select(p => p as IMachComponent));
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

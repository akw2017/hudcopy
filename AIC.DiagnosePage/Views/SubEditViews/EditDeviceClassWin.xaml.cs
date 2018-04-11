using AIC.Core.DiagnosticBaseModels;
using AIC.DiagnosePage.TestDatas;
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

namespace AIC.DiagnosePage.Views
{
    /// <summary>
    /// EditBearingClassWin.xaml 的交互逻辑
    /// </summary>
    public partial class EditDeviceClassWin : MetroWindow
    {
        public EditDeviceClassWin(DeviceDiagnosisClass component)
        {
            InitializeComponent();

            Component = component;
            Templates = new ObservableCollection<ShaftClass>(ShaftClassExamples.ShaftClassLib);
            this.DataContext = this;
        }

        #region 属性与字段
        private ObservableCollection<ShaftClass> templates;
        public ObservableCollection<ShaftClass> Templates
        {
            get { return templates; }
            set
            {
                templates = value;
                OnPropertyChanged("Templates");
            }
        }

        private ShaftClass selectedTemplate;
        public ShaftClass SelectedTemplate
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

        public DeviceDiagnosisClass Component { get; set; }
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
                Component.AddChild(new ShaftComponent() { Name = NewName, Component = SelectedTemplate });
            }
        }

        private void Delete()
        {
            Component.RemoveChild(Component.SelectedShaft);
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

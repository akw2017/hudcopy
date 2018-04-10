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
        DeviceDiagnosisClass Component = new DeviceDiagnosisClass();
        public EditDeviceClassWin(DeviceDiagnosisClass component)
        {
            InitializeComponent();

            Component = component;
            this.DataContext = this;

            ShaftTemplates = new ObservableCollection<ShaftClass>(ShaftClassExamples.ShaftClassLib);
        }

        private ObservableCollection<ShaftClass> shaftTemplates;
        public ObservableCollection<ShaftClass> ShaftTemplates
        {
            get { return shaftTemplates; }
            set
            {
                shaftTemplates = value;
                OnPropertyChanged("ShaftTemplates");
            }
        }

        private ShaftClass selectedShaftTemplate;
        public ShaftClass SelectedShaftTemplate
        {
            get { return selectedShaftTemplate; }
            set
            {
                selectedShaftTemplate = value;
                OnPropertyChanged("SelectedShaftTemplate");
            }
        }

        #region 命令
        private ICommand addShaftCommand;
        public ICommand AddShaftCommand
        {
            get
            {
                return this.addShaftCommand ?? (this.addShaftCommand = new DelegateCommand(() => this.AddShaft()));
            }
        }

        private ICommand deleteShaftCommand;
        public ICommand DeleteShaftCommand
        {
            get
            {
                return this.deleteShaftCommand ?? (this.deleteShaftCommand = new DelegateCommand(() => this.DeleteShaft()));
            }
        }
        #endregion

        #region 编辑
        private void AddShaft()
        {
            Component.AddChild(new ShaftComponent() { Component = SelectedShaftTemplate });
        }

        private void DeleteShaft()
        {

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

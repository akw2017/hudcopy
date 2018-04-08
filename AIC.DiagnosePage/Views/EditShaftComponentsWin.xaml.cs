using AIC.Core.HardwareModels;
using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.DiagnosePage.Models;
using AIC.DiagnosePage.TestDatas;
using AIC.PDAPage.Models;
using MahApps.Metro.Controls;
using Prism.Commands;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainControlCardWin.xaml
    /// </summary>
    public partial class EditShaftComponentsWin : MetroWindow
    {
        public delegate void TransferParaData(IMachComponent i_component, EditOperateType type);
        public event TransferParaData Parachanged;

        private ShaftComponent shaftComponent = new ShaftComponent()
        {
            Component = new ShaftClassExamples().GetShaftClass1(null),
            ID = Guid.NewGuid(),
            Name = "后轴",
        };
        public ShaftComponent ShaftComponent
        {
            get { return shaftComponent; }
            set
            {
                shaftComponent = value;
                OnPropertyChanged("ShaftComponent");
            }
        }     

        private EditOperateType operateType;

        public EditShaftComponentsWin(IMachComponent i_component, EditOperateType type = EditOperateType.Add)
        {           
            InitializeComponent();

            operateType = type;
            switch (type)
            {
                case EditOperateType.Add: this.Title += "(新增)"; break;
                case EditOperateType.Modify: this.Title += "(修改)"; break;
                case EditOperateType.Delete:  myGrid.IsEnabled = false; this.Title += "(删除)"; break;        
            }         
            if (i_component != null)
            {
                ShaftComponent = i_component as ShaftComponent;
            }
           
            this.DataContext = this;
        }        

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (Parachanged != null)
            {
              
            }
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
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

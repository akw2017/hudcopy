using AIC.Core.HardwareModels;
using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.DiagnosePage.Models;
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
    public partial class EditMachComponentsWin : MetroWindow
    {
        public delegate void TransferParaData(IMachComponent i_component, EditOperateType type);
        public event TransferParaData Parachanged;

        private BearingComponent bearingComponent = new BearingComponent() { Component = new BearingClass() { BearingID = Guid.NewGuid() }, ID = Guid.NewGuid(), Name = "新建轴承" };
        public BearingComponent BearingComponent
        {
            get { return bearingComponent; }
            set
            {
                bearingComponent = value;
                OnPropertyChanged("BearingComponent");
            }
        }

        private BeltComponent beltComponent = new BeltComponent() { Component = new BeltClass(), ID = Guid.NewGuid(), Name = "新建皮带" };
        public BeltComponent BeltComponent
        {
            get { return beltComponent; }
            set
            {
                beltComponent = value;
                OnPropertyChanged("BeltComponent");
            }
        }

        private GearComponent gearComponent = new GearComponent() { Component = new GearClass(), ID = Guid.NewGuid(), Name = "新建齿轮" }; 
        public GearComponent GearComponent
        {
            get { return gearComponent; }
            set
            {
                gearComponent = value;
                OnPropertyChanged("GearComponent");
            }
        }

        private ImpellerComponent impellerComponent =  new ImpellerComponent() { Component = new ImpellerClass(), ID = Guid.NewGuid(), Name = "新建叶轮" }; 
        public ImpellerComponent ImpellerComponent
        {
            get { return impellerComponent; }
            set
            {
                impellerComponent = value;
                OnPropertyChanged("ImpellerComponent");
            }
        }

        private MotorComponent motorComponent = new MotorComponent() { Component = new MotorClass(), ID = Guid.NewGuid(), Name = "新建电机" };
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
                componentType = value;
                OnPropertyChanged("ComponentType");
            }
        }

        private EditOperateType operateType;

        public EditMachComponentsWin(IMachComponent i_component, EditOperateType type = EditOperateType.Add)
        {           
            InitializeComponent();

            operateType = type;
            switch (type)
            {
                case EditOperateType.Add: this.Title += "(新增)"; break;
                case EditOperateType.Modify: myComBox.IsEnabled = false; this.Title += "(修改)"; break;
                case EditOperateType.Delete: myComBox.IsEnabled = false; myGrid.IsEnabled = false; this.Title += "(删除)"; break;        
            }
           
            if (i_component != null)
            {
                switch (i_component.ComponentType)
                {
                    case DeviceComponentType.Bearing: BearingComponent = i_component as BearingComponent; ComponentType = BearingComponent.ComponentType; break;
                    case DeviceComponentType.Belt: BeltComponent = i_component as BeltComponent; ComponentType = BeltComponent.ComponentType; break;
                    case DeviceComponentType.Gear: GearComponent = i_component as GearComponent; ComponentType = GearComponent.ComponentType; break;
                    case DeviceComponentType.Impeller: ImpellerComponent = i_component as ImpellerComponent; ComponentType = ImpellerComponent.ComponentType; break;
                    case DeviceComponentType.Motor: MotorComponent = i_component as MotorComponent; ComponentType = MotorComponent.ComponentType; break;
                }
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
                switch (ComponentType)
                {
                    case DeviceComponentType.Bearing: Parachanged(BearingComponent, operateType); break;
                    case DeviceComponentType.Belt: Parachanged(BeltComponent, operateType); break;
                    case DeviceComponentType.Gear: Parachanged(GearComponent, operateType); break;
                    case DeviceComponentType.Impeller: Parachanged(ImpellerComponent, operateType); break;
                    case DeviceComponentType.Motor: Parachanged(MotorComponent, operateType); break;
                }
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

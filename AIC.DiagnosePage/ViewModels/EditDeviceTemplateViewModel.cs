using AIC.Core.DiagnosticBaseModels;
using AIC.CoreType;
using AIC.DiagnosePage.TestDatas;
using AIC.DiagnosePage.Views;
using AIC.PDAPage.Models;
using AIC.Resources.Models;
using AIC.ServiceInterface;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace AIC.DiagnosePage.ViewModels
{
    class EditDeviceTemplateViewModel : BindableBase
    {
        private readonly IDatabaseComponent _databaseComponent;
        private readonly ILocalConfiguration _localConfiguration;
        private readonly IDeviceDiagnoseTemplateService _deviceDiagnoseTemplateService;

        public EditDeviceTemplateViewModel(IDatabaseComponent databaseComponent, ILocalConfiguration localConfiguration, IDeviceDiagnoseTemplateService deviceDiagnoseTemplateService)
        {
            _databaseComponent = databaseComponent;
            _localConfiguration = localConfiguration;
            _deviceDiagnoseTemplateService = deviceDiagnoseTemplateService;

            ServerIPCategory = _databaseComponent.GetServerIPCategory();
            ServerIP = _databaseComponent.MainServerIp;

            Init(ServerIP);

            bearingsView = new ListCollectionView(Bearings);
            bearingsView.Filter = (object item) =>
            {
                if (SearchName == null || SearchName == "") return true;
                var itemPl = (BearingClass)item;
                if (itemPl.Name.Contains(SearchName))
                {
                    return true;
                }
                return false;
            };
            beltsView = new ListCollectionView(Belts);
            beltsView.Filter = (object item) =>
            {
                if (SearchName == null || SearchName == "") return true;
                var itemPl = (BeltClass)item;
                if (itemPl.Name.Contains(SearchName))
                {
                    return true;
                }
                return false;
            };
            gearsView = new ListCollectionView(Gears);
            gearsView.Filter = (object item) =>
            {
                if (SearchName == null || SearchName == "") return true;
                var itemPl = (GearClass)item;
                if (itemPl.Name.Contains(SearchName))
                {
                    return true;
                }
                return false;
            };
            impellersView = new ListCollectionView(Impellers);
            impellersView.Filter = (object item) =>
            {
                if (SearchName == null || SearchName == "") return true;
                var itemPl = (ImpellerClass)item;
                if (itemPl.Name.Contains(SearchName))
                {
                    return true;
                }
                return false;
            };
            motorsView = new ListCollectionView(Motors);
            motorsView.Filter = (object item) =>
            {
                if (SearchName == null || SearchName == "") return true;
                var itemPl = (MotorClass)item;
                if (itemPl.Name.Contains(SearchName))
                {
                    return true;
                }
                return false;
            };
            shaftsView = new ListCollectionView(Shafts);
            shaftsView.Filter = (object item) =>
            {
                if (SearchName == null || SearchName == "") return true;
                var itemPl = (ShaftClass)item;
                if (itemPl.Name.Contains(SearchName))
                {
                    return true;
                }
                return false;
            };
            devicesView = new ListCollectionView(Devices);
            devicesView.Filter = (object item) =>
            {
                if (SearchName == null || SearchName == "") return true;
                var itemPl = (DeviceDiagnosisClass)item;
                if (itemPl.Name.Contains(SearchName))
                {
                    return true;
                }
                return false;
            };
        }

        #region 属性与字段  
        private string _serverIP;
        public string ServerIP
        {
            get { return _serverIP; }
            set
            {
                if (_serverIP != value)
                {
                    _serverIP = value;
                    Init(_serverIP);
                    OnPropertyChanged("ServerIP");
                }
            }
        }

        private List<string> _serverIPCategory;
        public List<string> ServerIPCategory
        {
            get { return _serverIPCategory; }
            set
            {
                _serverIPCategory = value;
                OnPropertyChanged("ServerIPCategory");
            }
        }

        private string searchName = "";
        public string SearchName
        {
            get
            {
                return searchName;
            }
            set
            {
                searchName = value;
                OnPropertyChanged("SearchName");
            }
        }

        private string selectedName = "";
        public string SelectedName
        {
            get
            {
                return selectedName;
            }
            set
            {
                selectedName = value;
                OnPropertyChanged("SelectedName");
            }
        }

        private readonly ICollectionView bearingsView;
        public ICollectionView BearingsView { get { return bearingsView; } }

        private readonly ICollectionView beltsView;
        public ICollectionView BeltsView { get { return beltsView; } }

        private readonly ICollectionView gearsView;
        public ICollectionView GearsView { get { return gearsView; } }

        private readonly ICollectionView impellersView;
        public ICollectionView ImpellersView { get { return impellersView; } }

        private readonly ICollectionView motorsView;
        public ICollectionView MotorsView { get { return motorsView; } }

        private readonly ICollectionView shaftsView;
        public ICollectionView ShaftsView { get { return shaftsView; } }

        private readonly ICollectionView devicesView;
        public ICollectionView DevicesView { get { return devicesView; } }

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

        private ObservableCollection<ShaftClass> shafts;
        public ObservableCollection<ShaftClass> Shafts
        {
            get { return shafts; }
            set
            {
                shafts = value;
                OnPropertyChanged("Shafts");
            }
        }

        private ObservableCollection<DeviceDiagnosisClass> devices;
        public ObservableCollection<DeviceDiagnosisClass> Devices
        {
            get { return devices; }
            set
            {
                devices = value;
                OnPropertyChanged("Devices");
            }
        }


        private BearingClass selectedBearing;
        public BearingClass SelectedBearing
        {
            get { return selectedBearing; }
            set
            {
                if (selectedBearing != value)
                {
                    selectedBearing = value;
                    OnPropertyChanged("SelectedBearing");
                    if (BearingsIsSelected == true)
                    {
                        SelectedName = (selectedBearing == null) ? null : selectedBearing.Name;
                    }
                }
            }
        }

        private BeltClass selectedBelt;
        public BeltClass SelectedBelt
        {
            get { return selectedBelt; }
            set
            {
                if (selectedBelt != value)
                {
                    selectedBelt = value;
                    OnPropertyChanged("SelectedBelt");
                    if (BeltsIsSelected == true)
                    {
                        SelectedName = (selectedBelt == null) ? null : selectedBelt.Name;
                    }
                }
            }
        }

        private GearClass selectedGear;
        public GearClass SelectedGear
        {
            get { return selectedGear; }
            set
            {
                if (selectedGear != value)
                {
                    selectedGear = value;
                    OnPropertyChanged("SelectedGear");
                    if (GearsIsSelected == true)
                    {
                        SelectedName = (selectedGear == null) ? null : selectedGear.Name;
                    }
                }
            }
        }

        private ImpellerClass selectedImpeller;
        public ImpellerClass SelectedImpeller
        {
            get { return selectedImpeller; }
            set
            {
                if (selectedImpeller != value)
                {
                    selectedImpeller = value;
                    OnPropertyChanged("SelectedImpeller");
                    if (ImpellersIsSelected == true)
                    {
                        SelectedName = (selectedImpeller == null) ? null : selectedImpeller.Name;
                    }
                }
            }
        }

        private MotorClass selectedMotor;
        public MotorClass SelectedMotor
        {
            get { return selectedMotor; }
            set
            {
                if (selectedMotor != value)
                {
                    selectedMotor = value;
                    OnPropertyChanged("SelectedMotor");
                    if (MotorsIsSelected == true)
                    {
                        SelectedName = (selectedMotor == null) ? null : selectedMotor.Name;
                    }
                }
            }
        }

        private ShaftClass selectedShaft;
        public ShaftClass SelectedShaft
        {
            get { return selectedShaft; }
            set
            {
                if (selectedShaft != value)
                {
                    selectedShaft = value;
                    OnPropertyChanged("SelectedShaft");
                    if (ShaftsIsSelected == true)
                    {
                        SelectedName = (selectedShaft == null) ? null : selectedShaft.Name;
                    }
                }
            }
        }

        private DeviceDiagnosisClass selectedDevice;
        public DeviceDiagnosisClass SelectedDevice
        {
            get { return selectedDevice; }
            set
            {
                if (selectedDevice != value)
                {
                    selectedDevice = value;
                    OnPropertyChanged("SelectedDevice");
                    if (DevicesIsSelected == true)
                    {
                        SelectedName = (selectedDevice == null) ? null : selectedDevice.Name;
                    }
                }
            }
        }

        private bool devicesIsSelected;
        public bool DevicesIsSelected
        {
            get { return devicesIsSelected; }
            set
            {
                devicesIsSelected = value;
                OnPropertyChanged("DevicesIsSelected");
            }
        }

        private bool shaftsIsSelected;
        public bool ShaftsIsSelected
        {
            get { return shaftsIsSelected; }
            set
            {
                shaftsIsSelected = value;
                OnPropertyChanged("ShaftsIsSelected");
            }
        }

        private bool bearingsIsSelected;
        public bool BearingsIsSelected
        {
            get { return bearingsIsSelected; }
            set
            {
                bearingsIsSelected = value;
                OnPropertyChanged("BearingsIsSelected");
            }
        }

        private bool beltsIsSelected;
        public bool BeltsIsSelected
        {
            get { return beltsIsSelected; }
            set
            {
                beltsIsSelected = value;
                OnPropertyChanged("BeltsIsSelected");
            }
        }

        private bool gearsIsSelected;
        public bool GearsIsSelected
        {
            get { return gearsIsSelected; }
            set
            {
                gearsIsSelected = value;
                OnPropertyChanged("GearsIsSelected");
            }
        }

        private bool impellersIsSelected;
        public bool ImpellersIsSelected
        {
            get { return impellersIsSelected; }
            set
            {
                impellersIsSelected = value;
                OnPropertyChanged("ImpellersIsSelected");
            }
        }

        private bool motorsIsSelected;
        public bool MotorsIsSelected
        {
            get { return motorsIsSelected; }
            set
            {
                motorsIsSelected = value;
                OnPropertyChanged("MotorsIsSelected");
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
        #endregion

        private bool CanOperate(object para)
        {
            if (_localConfiguration.ServerInfoList.Where(p => p.IP == ServerIP).Where(p => p.Permission.Contains("admin") || p.Permission.Contains("管理员")).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region 命令
        private DelegateCommand<object> queryCommand;
        public DelegateCommand<object> QueryCommand
        {
            get
            {
                return this.queryCommand ?? (this.queryCommand = new DelegateCommand<object>(value => this.Query(value), value => CanOperate(value)));
            }
        }

        private DelegateCommand<object> addCommand;
        public DelegateCommand<object> AddCommand
        {
            get
            {
                return this.addCommand ?? (this.addCommand = new DelegateCommand<object>(value => this.Add(value), value => CanOperate(value)));
            }
        }

        private DelegateCommand<object> editCommand;
        public DelegateCommand<object> EditCommand
        {
            get
            {
                return this.editCommand ?? (this.editCommand = new DelegateCommand<object>(value => this.Edit(value), value => CanOperate(value)));
            }
        }

        private DelegateCommand<object> deleteCommand;
        public DelegateCommand<object> DeleteCommand
        {
            get
            {
                return this.deleteCommand ?? (this.deleteCommand = new DelegateCommand<object>(value => this.Delete(value), value => CanOperate(value)));
            }
        }

        private DelegateCommand<object> uploadCommand;
        public DelegateCommand<object> UploadCommand
        {
            get
            {
                return this.uploadCommand ?? (this.uploadCommand = new DelegateCommand<object>(value => this.Upload(value), value => CanOperate(value)));
            }
        }

        private DelegateCommand<object> loadCommand;
        public DelegateCommand<object> LoadCommand
        {
            get
            {
                return this.loadCommand ?? (this.loadCommand = new DelegateCommand<object>(value => this.Load(value), value => CanOperate(value)));
            }
        }
        #endregion
        private void Init(string ip)
        {
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
            if (Shafts == null)
            {
                Shafts = _deviceDiagnoseTemplateService.ShaftClassList;
            }
            if (Devices == null)
            {
                Devices = _deviceDiagnoseTemplateService.DeviceClassList;
            }            

            QueryCommand.RaiseCanExecuteChanged();
            AddCommand.RaiseCanExecuteChanged();
            EditCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();
            UploadCommand.RaiseCanExecuteChanged();
        }

        #region 编辑
        private void Query(object value)
        {
            if (DevicesIsSelected == true)
            {
                devicesView.Refresh();
                SelectedDevice = devicesView.CurrentItem as DeviceDiagnosisClass;
            }
            else if (ShaftsIsSelected == true)
            {
                shaftsView.Refresh();
                SelectedShaft = shaftsView.CurrentItem as ShaftClass;
            }
            else if (BearingsIsSelected == true)
            {
                bearingsView.Refresh();
                SelectedBearing = bearingsView.CurrentItem as BearingClass;
            }
            else if (BeltsIsSelected == true)
            {
                beltsView.Refresh();
                SelectedBelt = beltsView.CurrentItem as BeltClass;
            }
            else if (GearsIsSelected == true)
            {
                gearsView.Refresh();
                SelectedGear = gearsView.CurrentItem as GearClass;
            }
            else if (ImpellersIsSelected == true)
            {
                impellersView.Refresh();
                SelectedImpeller = impellersView.CurrentItem as ImpellerClass;
            }
            else if (MotorsIsSelected == true)
            {
                motorsView.Refresh();
                SelectedMotor = motorsView.CurrentItem as MotorClass;
            }
        }

        private void Add(object value)
        {
            if (DevicesIsSelected == true)
            {
                SelectedDevice = new DeviceDiagnosisClass() { Name = "新建设备" };
                Devices.Add(SelectedDevice);
            }
            else if (ShaftsIsSelected == true)
            {
                SelectedShaft = new ShaftClass() { Name = "新建轴" };
                Shafts.Add(SelectedShaft);
            }
            else if (BearingsIsSelected == true)
            {
                SelectedBearing = new BearingClass() { Name = "新建轴承" };
                Bearings.Add(SelectedBearing);
            }
            else if (BeltsIsSelected == true)
            {
                SelectedBelt = new BeltClass() { Name = "新建皮带" };
                Belts.Add(SelectedBelt);
            }
            else if (GearsIsSelected == true)
            {
                SelectedGear = new GearClass() { Name = "新建齿轮" };
                Gears.Add(SelectedGear);
            }
            else if (ImpellersIsSelected == true)
            {
                SelectedImpeller = new ImpellerClass() { Name = "新建叶轮" };
                Impellers.Add(SelectedImpeller);
            }
            else if (MotorsIsSelected == true)
            {
                SelectedMotor = new MotorClass() { Name = "新建电机" };
                Motors.Add(SelectedMotor);
            }
            ShowWin();
            //立即更新到服务器
        }

        private void Edit(object value)
        {
            ShowWin();
            //立即更新到服务器
        }

        private void Delete(object value)
        {
            if (DevicesIsSelected == true)
            {
                Devices.Remove(SelectedDevice);
            }
            else if (ShaftsIsSelected == true)
            { 
                Shafts.Remove(SelectedShaft);
            }
            else if (BearingsIsSelected == true)
            {
                Bearings.Remove(SelectedBearing);
            }
            else if (BeltsIsSelected == true)
            {
                Belts.Remove(SelectedBelt);
            }
            else if (GearsIsSelected == true)
            {
                Gears.Remove(SelectedGear);
            }
            else if (ImpellersIsSelected == true)
            {
                Impellers.Remove(SelectedImpeller);
            }
            else if (MotorsIsSelected == true)
            {
                Motors.Remove(SelectedMotor);
            }
            //立即更新到服务器
        }

        private void Upload(object value)
        {

        }

        private void Load(object value)
        {
            _deviceDiagnoseTemplateService.GetClasses(ServerIP);
        }
        private void ShowWin()
        {
            if (DevicesIsSelected == true)
            {
                EditDeviceClassWin win = new EditDeviceClassWin(SelectedDevice);
                win.ShowDialog();
            }
            else if (ShaftsIsSelected == true)
            {
                EditShaftClassWin win = new EditShaftClassWin(SelectedShaft);
                win.ShowDialog();
            }
            else if (BearingsIsSelected == true)
            {
                EditBearingClassWin win = new EditBearingClassWin(SelectedBearing);
                win.ShowDialog();
            }
            else if (BeltsIsSelected == true)
            {
                EditBeltClassWin win = new EditBeltClassWin(SelectedBelt);
                win.ShowDialog();
            }
            else if (GearsIsSelected == true)
            {;
                EditGearClassWin win = new EditGearClassWin(SelectedGear);
                win.ShowDialog();
            }
            else if (ImpellersIsSelected == true)
            {
                EditImpellerClassWin win = new EditImpellerClassWin(SelectedImpeller);
                win.ShowDialog();
            }
            else if (MotorsIsSelected == true)
            {
                EditMotorClassWin win = new EditMotorClassWin(SelectedMotor);
                win.ShowDialog();
            }
        }
        #endregion


    }
}

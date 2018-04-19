using AIC.Core.DiagnosticBaseModels;
using AIC.Core.Events;
using AIC.CoreType;
using AIC.DiagnosePage.Views;
using AIC.M9600.Common.MasterDB.Generated;
using AIC.PDAPage.Models;
using AIC.Resources.Models;
using AIC.ServiceInterface;
using Prism.Commands;
using Prism.Events;
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
        private readonly IEventAggregator _eventAggregator;

        public EditDeviceTemplateViewModel(IDatabaseComponent databaseComponent, ILocalConfiguration localConfiguration, IDeviceDiagnoseTemplateService deviceDiagnoseTemplateService, IEventAggregator eventAggregator)
        {
            _databaseComponent = databaseComponent;
            _localConfiguration = localConfiguration;
            _deviceDiagnoseTemplateService = deviceDiagnoseTemplateService;
            _eventAggregator = eventAggregator;

            ServerIPCategory = _databaseComponent.GetServerIPCategory();
            ServerIP = _databaseComponent.MainServerIp;

            Bearings = _deviceDiagnoseTemplateService.BearingClassList;
            Belts = _deviceDiagnoseTemplateService.BeltClassList;
            Gears = _deviceDiagnoseTemplateService.GearClassList;
            Impellers = _deviceDiagnoseTemplateService.ImpellerClassList;
            Motors = _deviceDiagnoseTemplateService.MotorClassList;
            Shafts = _deviceDiagnoseTemplateService.ShaftClassList;
            Devices = _deviceDiagnoseTemplateService.DeviceClassList;

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
                var itemPl = (DeviceDiagnoseClass)item;
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

        private ObservableCollection<DeviceDiagnoseClass> devices;
        public ObservableCollection<DeviceDiagnoseClass> Devices
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

        private DeviceDiagnoseClass selectedDevice;
        public DeviceDiagnoseClass SelectedDevice
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

        private bool uploadError = false;
        public bool UploadError
        {
            get { return uploadError; }
            set
            {
                uploadError = value;
                OnPropertyChanged("UploadError");
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

        private DelegateCommand<object> loadCommand;
        public DelegateCommand<object> LoadCommand
        {
            get
            {
                return this.loadCommand ?? (this.loadCommand = new DelegateCommand<object>(value => this.Load(value), value => CanOperate(value)));
            }
        }

        #endregion
        private async void Init(string ip)
        {
            await Load(null);

            QueryCommand.RaiseCanExecuteChanged();
            AddCommand.RaiseCanExecuteChanged();
            EditCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();
        }

        #region 编辑
        private void Query(object value)
        {
            if (DevicesIsSelected == true)
            {
                devicesView.Refresh();
                SelectedDevice = devicesView.CurrentItem as DeviceDiagnoseClass;
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
                SelectedDevice = new DeviceDiagnoseClass() { Name = "新建设备" };
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

        private async void Delete(object value)
        {
#if XBAP
            MessageBoxResult result = MessageBox.Show("确定要删除?", "删除", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show("确定要删除?", "删除", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
#endif
            if (result == MessageBoxResult.OK)
            {
                if (DevicesIsSelected == true)
                {                  
                    T_DeviceDiagnose t_model = DeviceDiagnoseClass.ConvertToDB(SelectedDevice);
                    if (t_model.id != -1)
                    {
                        await _databaseComponent.Delete<T_DeviceDiagnose>(ServerIP, t_model.id);
                    }
                    Devices.Remove(SelectedDevice);
                }
                else if (ShaftsIsSelected == true)
                {                  
                    T_Shaft t_model = ShaftClass.ConvertToDB(SelectedShaft);
                    if (t_model.id != -1)
                    {
                        await _databaseComponent.Delete<T_Shaft>(ServerIP, t_model.id);
                    }
                    Shafts.Remove(SelectedShaft);
                }
                else if (BearingsIsSelected == true)
                {                   
                    T_Bearing t_model = BearingClass.ConvertToDB(SelectedBearing);
                    if (t_model.id != -1)
                    {
                        await _databaseComponent.Delete<T_Bearing>(ServerIP, t_model.id);
                    }
                    Bearings.Remove(SelectedBearing);
                }
                else if (BeltsIsSelected == true)
                {
                    Belts.Remove(SelectedBelt);
                    T_Belt t_model = BeltClass.ConvertToDB(SelectedBelt);
                    if (t_model.id != -1)
                    {
                        await _databaseComponent.Delete<T_Belt>(ServerIP, t_model.id);
                    }
                }
                else if (GearsIsSelected == true)
                {                   
                    T_Gear t_model = GearClass.ConvertToDB(SelectedGear);
                    if (t_model.id != -1)
                    {
                        await _databaseComponent.Delete<T_Gear>(ServerIP, t_model.id);
                    }
                    Gears.Remove(SelectedGear);
                }
                else if (ImpellersIsSelected == true)
                {                   
                    T_Impeller t_model = ImpellerClass.ConvertToDB(SelectedImpeller);
                    if (t_model.id != -1)
                    {
                        await _databaseComponent.Delete<T_Impeller>(ServerIP, t_model.id);
                    }
                    Impellers.Remove(SelectedImpeller);
                }
                else if (MotorsIsSelected == true)
                {                  
                    T_Motor t_model = MotorClass.ConvertToDB(SelectedMotor);
                    if (t_model.id != -1)
                    {
                        await _databaseComponent.Delete<T_Motor>(ServerIP, t_model.id);
                    }
                    Motors.Remove(SelectedMotor);
                }
            }
        }

        private async Task Load(object value)
        {
            try
            {
                WaitInfo = "模板加载中";
                Status = ViewModelStatus.Querying;
                await _deviceDiagnoseTemplateService.GetClasses(ServerIP);
                UploadError = false;
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("设备诊断模板加载", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        private async void ShowWin()
        {
            if (DevicesIsSelected == true)
            {
                EditDeviceClassWin win = new EditDeviceClassWin(SelectedDevice);
                win.ShowDialog();

                T_DeviceDiagnose t_model = DeviceDiagnoseClass.ConvertToDB(SelectedDevice);
                if (SelectedDevice.id == -1)
                {
                    var id = await _databaseComponent.Add<T_DeviceDiagnose>(ServerIP, t_model);
                    if (id == -1)
                    {
                        UploadError = true;
                    }
                    else
                    {
                        SelectedDevice.id = id;
                    }
                }
                else
                {
                    UploadError = !await _databaseComponent.Modify<T_DeviceDiagnose>(ServerIP, null, t_model);
                }
            }
            else if (ShaftsIsSelected == true)
            {
                EditShaftClassWin win = new EditShaftClassWin(SelectedShaft);
                win.ShowDialog();

                T_Shaft t_model = ShaftClass.ConvertToDB(SelectedShaft);
                if (SelectedShaft.id == -1)
                {
                    var id = await _databaseComponent.Add<T_Shaft>(ServerIP, t_model);
                    if (id == -1)
                    {
                        UploadError = true;
                    }
                    else
                    {
                        SelectedShaft.id = id;
                    }
                }
                else
                {
                    UploadError = !await _databaseComponent.Modify<T_Shaft>(ServerIP, null, t_model);
                }
            }
            else if (BearingsIsSelected == true)
            {
                EditBearingClassWin win = new EditBearingClassWin(SelectedBearing);
                win.ShowDialog();

                T_Bearing t_model = BearingClass.ConvertToDB(SelectedBearing);
                if (SelectedBearing.id == -1)
                {
                    var id = await _databaseComponent.Add<T_Bearing>(ServerIP, t_model);
                    if (id == -1)
                    {
                        UploadError = true;
                    }
                    else
                    {
                        SelectedBearing.id = id;
                    }
                }
                else
                {
                    UploadError =  !await _databaseComponent.Modify<T_Bearing>(ServerIP, null, t_model);
                }
            }
            else if (BeltsIsSelected == true)
            {
                EditBeltClassWin win = new EditBeltClassWin(SelectedBelt);
                win.ShowDialog();

                T_Belt t_model = BeltClass.ConvertToDB(SelectedBelt);
                if (SelectedBelt.id == -1)
                {
                    var id = await _databaseComponent.Add<T_Belt>(ServerIP, t_model);
                    if (id == -1)
                    {
                        UploadError = true;
                    }
                    else
                    {
                        SelectedBelt.id = id;
                    }
                }
                else
                {
                    UploadError = !await _databaseComponent.Modify<T_Belt>(ServerIP, null, t_model);
                }
            }
            else if (GearsIsSelected == true)
            {;
                EditGearClassWin win = new EditGearClassWin(SelectedGear);
                win.ShowDialog();

                T_Gear t_model = GearClass.ConvertToDB(SelectedGear);
                if (SelectedGear.id == -1)
                {
                    var id = await _databaseComponent.Add<T_Gear>(ServerIP, t_model);
                    if (id == -1)
                    {
                        UploadError = true;
                    }
                    else
                    {
                        SelectedGear.id = id;
                    }
                }
                else
                {
                    UploadError = !await _databaseComponent.Modify<T_Gear>(ServerIP, null, t_model);
                }
            }
            else if (ImpellersIsSelected == true)
            {
                EditImpellerClassWin win = new EditImpellerClassWin(SelectedImpeller);
                win.ShowDialog();

                T_Impeller t_model = ImpellerClass.ConvertToDB(SelectedImpeller);
                if (SelectedImpeller.id == -1)
                {
                    var id = await _databaseComponent.Add<T_Impeller>(ServerIP, t_model);
                    if (id == -1)
                    {
                        UploadError = true;
                    }
                    else
                    {
                        SelectedImpeller.id = id;
                    }
                }
                else
                {
                    UploadError = !await _databaseComponent.Modify<T_Impeller>(ServerIP, null, t_model);
                }
            }
            else if (MotorsIsSelected == true)
            {
                EditMotorClassWin win = new EditMotorClassWin(SelectedMotor);
                win.ShowDialog();

                T_Motor t_model = MotorClass.ConvertToDB(SelectedMotor);
                if (SelectedMotor.id == -1)
                {
                    var id = await _databaseComponent.Add<T_Motor>(ServerIP, t_model);
                    if (id == -1)
                    {
                        UploadError = true;
                    }
                    else
                    {
                        SelectedMotor.id = id;
                    }
                }
                else
                {
                    UploadError = !await _databaseComponent.Modify<T_Motor>(ServerIP, null, t_model);
                }
            }
        }
        #endregion


    }
}

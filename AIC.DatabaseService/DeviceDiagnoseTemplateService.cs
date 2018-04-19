using AIC.Core.DiagnosticBaseModels;
using AIC.DatabaseService.TestDatas;
using AIC.M9600.Common.MasterDB.Generated;
using AIC.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DatabaseService
{
    public class DeviceDiagnoseTemplateService : IDeviceDiagnoseTemplateService
    {
        private readonly IDatabaseComponent _databaseComponent;

        public Dictionary<string, List<DeviceDiagnoseClass>> DeviceClasses { get; private set; } = new Dictionary<string, List<DeviceDiagnoseClass>>();
        public Dictionary<string, List<ShaftClass>> ShaftClasses { get; private set; } = new Dictionary<string, List<ShaftClass>>();
        public Dictionary<string, List<BearingClass>> BearingClasses { get; private set; } = new Dictionary<string, List<BearingClass>>();
        public Dictionary<string, List<BeltClass>> BeltClasses { get; private set; } = new Dictionary<string, List<BeltClass>>();
        public Dictionary<string, List<GearClass>> GearClasses { get; private set; } = new Dictionary<string, List<GearClass>>();
        public Dictionary<string, List<ImpellerClass>> ImpellerClasses { get; private set; } = new Dictionary<string, List<ImpellerClass>>();
        public Dictionary<string, List<MotorClass>> MotorClasses { get; private set; } = new Dictionary<string, List<MotorClass>>();


        public ObservableCollection<DeviceDiagnoseClass> DeviceClassList { get; private set; } = new ObservableCollection<DeviceDiagnoseClass>();
        public ObservableCollection<ShaftClass> ShaftClassList { get; private set; } = new ObservableCollection<ShaftClass>();
        public ObservableCollection<BearingClass> BearingClassList { get; private set; } = new ObservableCollection<BearingClass>();
        public ObservableCollection<BeltClass> BeltClassList { get; private set; } = new ObservableCollection<BeltClass>();
        public ObservableCollection<GearClass> GearClassList { get; private set; } = new ObservableCollection<GearClass>();
        public ObservableCollection<ImpellerClass> ImpellerClassList { get; private set; } = new ObservableCollection<ImpellerClass>();
        public ObservableCollection<MotorClass> MotorClassList { get; private set; } = new ObservableCollection<MotorClass>();

      
        public DeviceDiagnoseTemplateService(IDatabaseComponent databaseComponent)
        {
            _databaseComponent = databaseComponent;
            //GetTestClasses("内部");
        }

        //测试数据，废弃
        public void GetTestClasses(string serverip)
        {
            if (DeviceClasses.ContainsKey(serverip))
            {
                DeviceClasses.Remove(serverip);               
            }
            DeviceClasses.Add(serverip, DeviceClassExamples.DeviceDiagnoseClassLib);//实际从数据库获取
            if (ShaftClasses.ContainsKey(serverip))
            {
                ShaftClasses.Remove(serverip);             
            }
            ShaftClasses.Add(serverip, ShaftClassExamples.ShaftClassLib);//实际从数据库获取
            if (BearingClasses.ContainsKey(serverip))
            {
                BearingClasses.Remove(serverip);               
            }
            BearingClasses.Add(serverip, BearingClassExamples.BearingClassLib);//实际从数据库获取
            if (BeltClasses.ContainsKey(serverip))
            {
                BeltClasses.Remove(serverip);               
            }
            BeltClasses.Add(serverip, BeltClassExamples.BeltClassLib);//实际从数据库获取
            if (GearClasses.ContainsKey(serverip))
            {
                GearClasses.Remove(serverip);               
            }
            GearClasses.Add(serverip, GearClassExamples.GearClassLib);//实际从数据库获取
            if (ImpellerClasses.ContainsKey(serverip))
            {
                ImpellerClasses.Remove(serverip);               
            }
            ImpellerClasses.Add(serverip, ImpellerClassExamples.ImpellerClassLib);//实际从数据库获取
            if (MotorClasses.ContainsKey(serverip))
            {
                MotorClasses.Remove(serverip);                
            }
            MotorClasses.Add(serverip, MotorClassExamples.MotorClassLib);//实际从数据库获取

            DeviceClassList.Clear();
            DeviceClassList.AddRange(DeviceClasses.SelectMany(p => p.Value));
            ShaftClassList.Clear();
            ShaftClassList.AddRange(ShaftClasses.SelectMany(p => p.Value));
            BearingClassList.Clear();
            BearingClassList.AddRange(BearingClasses.SelectMany(p => p.Value));
            BeltClassList.Clear();
            BeltClassList.AddRange(BeltClasses.SelectMany(p => p.Value));
            GearClassList.Clear();
            GearClassList.AddRange(GearClasses.SelectMany(p => p.Value));
            ImpellerClassList.Clear();
            ImpellerClassList.AddRange(ImpellerClasses.SelectMany(p => p.Value));
            MotorClassList.Clear();
            MotorClassList.AddRange(MotorClasses.SelectMany(p => p.Value));
        }

        public async Task GetClasses(string serverip)
        {
            await GetDeviceClasses(serverip);
            await GetShaftClasses(serverip);
            await GetBearingClasses(serverip);
            await GetBeltClasses(serverip);
            await GetGearClasses(serverip);
            await GetImpellerClasses(serverip);
            await GetMotorClasses(serverip);
        }

        public async Task GetDeviceClasses(string serverip)
        {
            //设备
            if (DeviceClasses.ContainsKey(serverip))
            {
                DeviceClasses.Remove(serverip);
            }
            var t_models = await _databaseComponent.Query<T_DeviceDiagnose>(serverip, null, null, null);
            if (t_models != null)
            {
                List<DeviceDiagnoseClass> models = new List<DeviceDiagnoseClass>();
                foreach (var t_model in t_models)
                {
                    var model = DeviceDiagnoseClass.ConvertFromDB(t_model);
                    models.Add(model);
                }
                DeviceClasses.Add(serverip, models);
            }

            DeviceClassList.Clear();
            DeviceClassList.AddRange(DeviceClasses.SelectMany(p => p.Value));          
        }
        public async Task GetShaftClasses(string serverip)
        {
            //轴
            if (ShaftClasses.ContainsKey(serverip))
            {
                ShaftClasses.Remove(serverip);
            }
            var t_shafts = await _databaseComponent.Query<T_Shaft>(serverip, null, null, null);
            if (t_shafts != null)
            {
                List<ShaftClass> shafts = new List<ShaftClass>();
                foreach (var t_shaft in t_shafts)
                {
                    var shaft = ShaftClass.ConvertFromDB(t_shaft);
                    shafts.Add(shaft);
                }
                ShaftClasses.Add(serverip, shafts);
            }

            ShaftClassList.Clear();
            ShaftClassList.AddRange(ShaftClasses.SelectMany(p => p.Value));
        }
        public async Task GetBearingClasses(string serverip)
        {
            //轴承
            if (BearingClasses.ContainsKey(serverip))
            {
                BearingClasses.Remove(serverip);
            }
            var t_bearings = await _databaseComponent.Query<T_Bearing>(serverip, null, null, null);
            if (t_bearings != null)
            {
                List<BearingClass> bearings = new List<BearingClass>();
                foreach (var t_bearing in t_bearings)
                {
                    var bearing = BearingClass.ConvertFromDB(t_bearing);
                    bearings.Add(bearing);
                }
                BearingClasses.Add(serverip, bearings);
            }

            BearingClassList.Clear();
            BearingClassList.AddRange(BearingClasses.SelectMany(p => p.Value));
        }
        public async Task GetBeltClasses(string serverip)
        {
            //皮带
            if (BeltClasses.ContainsKey(serverip))
            {
                BeltClasses.Remove(serverip);
            }
            var t_belts = await _databaseComponent.Query<T_Belt>(serverip, null, null, null);
            if (t_belts != null)
            {
                List<BeltClass> belts = new List<BeltClass>();
                foreach (var t_belt in t_belts)
                {
                    var belt = BeltClass.ConvertFromDB(t_belt);
                    belts.Add(belt);
                }
                BeltClasses.Add(serverip, belts);
            }

            BeltClassList.Clear();
            BeltClassList.AddRange(BeltClasses.SelectMany(p => p.Value));
        }
        public async Task GetGearClasses(string serverip)
        {
            //齿轮
            if (GearClasses.ContainsKey(serverip))
            {
                GearClasses.Remove(serverip);
            }
            var t_gears = await _databaseComponent.Query<T_Gear>(serverip, null, null, null);
            if (t_gears != null)
            {
                List<GearClass> gears = new List<GearClass>();
                foreach (var t_gear in t_gears)
                {
                    var gear = GearClass.ConvertFromDB(t_gear);
                    gears.Add(gear);
                }
                GearClasses.Add(serverip, gears);
            }

            GearClassList.Clear();
            GearClassList.AddRange(GearClasses.SelectMany(p => p.Value));
        }
        public async Task GetImpellerClasses(string serverip)
        {
            //叶轮
            if (ImpellerClasses.ContainsKey(serverip))
            {
                ImpellerClasses.Remove(serverip);
            }
            var t_impellers = await _databaseComponent.Query<T_Impeller>(serverip, null, null, null);
            if (t_impellers != null)
            {
                List<ImpellerClass> impellers = new List<ImpellerClass>();
                foreach (var t_impeller in t_impellers)
                {
                    var impeller = ImpellerClass.ConvertFromDB(t_impeller);
                    impellers.Add(impeller);
                }
                ImpellerClasses.Add(serverip, impellers);
            }

            ImpellerClassList.Clear();
            ImpellerClassList.AddRange(ImpellerClasses.SelectMany(p => p.Value));
        }
        public async Task GetMotorClasses(string serverip)
        {
            //电机
            if (MotorClasses.ContainsKey(serverip))
            {
                MotorClasses.Remove(serverip);
            }
            var t_motors = await _databaseComponent.Query<T_Motor>(serverip, null, null, null);
            if (t_motors != null)
            {
                List<MotorClass> motors = new List<MotorClass>();
                foreach (var t_motor in t_motors)
                {
                    var motor = MotorClass.ConvertFromDB(t_motor);
                    motors.Add(motor);
                }
                MotorClasses.Add(serverip, motors);
            }

            MotorClassList.Clear();
            MotorClassList.AddRange(MotorClasses.SelectMany(p => p.Value));
        }

        public async Task<DeviceDiagnoseComponent> GetDeviceDiagnoseComponent(string serverip, Guid guid)
        {
            var t_models = await _databaseComponent.Query<T_Diagnosis_Model>(serverip, null, "(Guid = @0)", new object[] { guid });
            if (t_models != null && t_models.Count > 0)
            {
                var t_model = t_models[0];
                var model = DeviceDiagnoseComponent.ConvertFromDB(t_model);
                return model;
            }
            else
            {
                return null;
            }
        }

        public async Task<long> AddDeviceDiagnoseComponent(string serverip, object obj)
        {
            T_Diagnosis_Model t_model = obj as T_Diagnosis_Model;
            long id = await _databaseComponent.Add<T_Diagnosis_Model>(serverip, t_model);
            return id;
        }

        public async Task<bool> DeleteDeviceDiagnoseComponent(string serverip, long id)
        {
            bool success = await _databaseComponent.Delete<T_Diagnosis_Model>(serverip, id);
            return success;
        }

        public async Task<bool> ModifyDeviceDiagnoseComponent(string serverip, object obj)
        {
            T_Diagnosis_Model t_model = obj as T_Diagnosis_Model;
            bool success = await _databaseComponent.Modify<T_Diagnosis_Model>(serverip, null, t_model);
            return success;
        }
    }
}

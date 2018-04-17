using AIC.Core.DiagnosticBaseModels;
using AIC.DatabaseService.TestDatas;
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
        public Dictionary<string, List<DeviceDiagnosisClass>> DeviceClasses { get; private set; } = new Dictionary<string, List<DeviceDiagnosisClass>>();
        public Dictionary<string, List<ShaftClass>> ShaftClasses { get; private set; } = new Dictionary<string, List<ShaftClass>>();
        public Dictionary<string, List<BearingClass>> BearingClasses { get; private set; } = new Dictionary<string, List<BearingClass>>();
        public Dictionary<string, List<BeltClass>> BeltClasses { get; private set; } = new Dictionary<string, List<BeltClass>>();
        public Dictionary<string, List<GearClass>> GearClasses { get; private set; } = new Dictionary<string, List<GearClass>>();
        public Dictionary<string, List<ImpellerClass>> ImpellerClasses { get; private set; } = new Dictionary<string, List<ImpellerClass>>();
        public Dictionary<string, List<MotorClass>> MotorClasses { get; private set; } = new Dictionary<string, List<MotorClass>>();


        public ObservableCollection<DeviceDiagnosisClass> DeviceClassList { get; private set; } = new ObservableCollection<DeviceDiagnosisClass>();
        public ObservableCollection<ShaftClass> ShaftClassList { get; private set; } = new ObservableCollection<ShaftClass>();
        public ObservableCollection<BearingClass> BearingClassList { get; private set; } = new ObservableCollection<BearingClass>();
        public ObservableCollection<BeltClass> BeltClassList { get; private set; } = new ObservableCollection<BeltClass>();
        public ObservableCollection<GearClass> GearClassList { get; private set; } = new ObservableCollection<GearClass>();
        public ObservableCollection<ImpellerClass> ImpellerClassList { get; private set; } = new ObservableCollection<ImpellerClass>();
        public ObservableCollection<MotorClass> MotorClassList { get; private set; } = new ObservableCollection<MotorClass>();

        public DeviceDiagnoseTemplateService()
        {
            GetClasses("内置");//htzk123，测试用，别忘了删除
        }

        public void GetClasses(string serverip)
        {
            if (DeviceClasses.ContainsKey(serverip))
            {
                DeviceClasses.Remove(serverip);               
            }
            DeviceClasses.Add(serverip, DeviceClassExamples.DeviceDiagnosisClassLib);//实际从数据库获取
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
    }
}

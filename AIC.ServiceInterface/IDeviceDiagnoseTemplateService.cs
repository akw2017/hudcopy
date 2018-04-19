using AIC.Core.DiagnosticBaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.ServiceInterface
{
    public interface IDeviceDiagnoseTemplateService
    {
        ObservableCollection<DeviceDiagnoseClass> DeviceClassList { get; }
        ObservableCollection<ShaftClass> ShaftClassList { get; }
        ObservableCollection<BearingClass> BearingClassList { get; }
        ObservableCollection<BeltClass> BeltClassList { get; }
        ObservableCollection<GearClass> GearClassList { get; }
        ObservableCollection<ImpellerClass> ImpellerClassList { get; }
        ObservableCollection<MotorClass> MotorClassList { get; }
        Task GetClasses(string serverip);

        Task<DeviceDiagnoseComponent> GetDeviceDiagnoseComponent(string serverip, Guid guid);
        Task<long> AddDeviceDiagnoseComponent(string serverip, object obj);
        Task<bool> DeleteDeviceDiagnoseComponent(string serverip, long id);
        Task<bool> ModifyDeviceDiagnoseComponent(string serverip, object obj);
    }
}

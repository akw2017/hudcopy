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
        ObservableCollection<DeviceDiagnosisClass> DeviceClassList { get; }
        ObservableCollection<ShaftClass> ShaftClassList { get; }
        ObservableCollection<BearingClass> BearingClassList { get; }
        ObservableCollection<BeltClass> BeltClassList { get; }
        ObservableCollection<GearClass> GearClassList { get; }
        ObservableCollection<ImpellerClass> ImpellerClassList { get; }
        ObservableCollection<MotorClass> MotorClassList { get; }
        void GetClasses(string serverip);
    }
}

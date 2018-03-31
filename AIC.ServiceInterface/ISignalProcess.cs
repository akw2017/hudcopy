using AIC.Core.Events;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.Core.SignalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.ServiceInterface
{
    public interface ISignalProcess
    {
        IEnumerable<BaseAlarmSignal> Signals { get; }
        void Initialize();
        void InitSignals();
        void LazyInitSignals();
        void BindItem(ItemTreeItemViewModel item);
        void UnBindItem(ItemTreeItemViewModel item);

        void AddDivfre(DivFreTreeItemViewModel divfreTM);
        void DeleteDivfre(DivFreTreeItemViewModel divfreTMs);
        void Suspend(bool suspend);
        BaseAlarmSignal GetSignal(Guid guid, string serverip); //BaseAlarmSignal GetSignal(Guid guid);

        Task<bool> GetSignalData(DateTime time, bool isHistoryMode);

        event SignalChangedEvent SignalsAdded;
        event SignalChangedEvent SignalsRemoved;

        void GetDailyMedianData(DateTime startTime, DateTime endTime);
        event DailyChangedEvent DailyChanged;

        Dictionary<string, List<StatisticalInformationData>>  StatisticalInformation { get; }
        List<Tuple<DateTime, int, int, int, int>> GetStatisticalAlarmNumber(string serverip, Guid[] guids);
        AlarmObjectInfo GetStatisticalAlarmAlarmRate(string serverip);
        List<AlarmObjectInfo> GetStatisticalAlarmAlarmRate(string serverip, DeviceTreeItemViewModel[] devices);
        List<AlarmObjectInfo> GetStatisticalAlarmAlarmRate(string serverip, ItemTreeItemViewModel[] items);
        void GetRunningDays(DateTime now);
        Dictionary<string, double> RunningDays { get; }
    }
}

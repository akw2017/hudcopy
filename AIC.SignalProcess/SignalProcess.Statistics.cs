using AIC.Core.Events;
using AIC.Core.SignalModels;
using AIC.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.SignalProcess
{
    public partial class SignalProcess : ISignalProcess
    {
        public Dictionary<string, List<StatisticalInformationData>> StatisticalInformation { get; set; }
        public Dictionary<string, List<Tuple<DateTime, int, int, int, int>>> ServerLevelStatisticalResult { get; set; }

        public Dictionary<string, double> RunningDays { get; set; }

        public event DailyChangedEvent DailyChanged;

        public async void GetDailyMedianData(DateTime startTime, DateTime endTime)
        {
            StatisticalInformation = await _databaseComponent.GetDailyMedianData<StatisticalInformationData>(startTime, endTime, StatisticalInformationData.GetColumnsString());
            if (DailyChanged != null)
            {
                ServerLevelStatisticalResult = new Dictionary<string, List<Tuple<DateTime, int, int, int, int>>>();
                foreach(var serverip in _databaseComponent.GetServerIPCategory())
                {
                    var signals = Signals.Where(p => p.ServerIP == serverip).Select(p => p.Guid);
                    List<Tuple<DateTime, int, int, int, int>> tuple = new List<Tuple<DateTime, int, int, int, int>>();
                    for (DateTime dt = startTime; dt <= endTime; dt = dt.AddDays(1))//把每天危险测点加起来
                    {
                        var daysignals = StatisticalInformation[serverip].Where(p => p.ACQDatetime.Value.Date == dt.Date && signals.Contains(p.T_Item_Guid)).ToArray();
                        var allNumber = daysignals.Count();
                        var dangerNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 4).Count();
                        var alarmNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 3).Count();
                        var prealarmNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 2).Count();
                        tuple.Add(new Tuple<DateTime, int, int, int, int>(dt, allNumber, dangerNumber, alarmNumber, prealarmNumber));
                    }
                    ServerLevelStatisticalResult.Add(serverip, tuple);
                }
                DailyChanged();
            }
        }

        public async void GetRunningDays(DateTime now)
        {
            RunningDays = new Dictionary<string, double>();
            foreach (var sg in SgDict)
            {
                var runlist = await _databaseComponent.GetStatisticsData(sg.Key, new HashSet<Guid>(sg.Value.Values.Select(p => p.Guid)));
                var max = runlist.Select(p => p.Value["FirstUploadTime"]).Max();
                var date = new DateTime((long)max);
                var days = (now - date).TotalDays;
                RunningDays.Add(sg.Key, days);
            }
            
        }

    }
}

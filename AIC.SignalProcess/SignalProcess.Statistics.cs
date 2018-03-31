using AIC.Core.Events;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
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

        public Dictionary<string, double> RunningDays { get; set; }

        public event DailyChangedEvent DailyChanged;

        public async void GetDailyMedianData(DateTime startTime, DateTime endTime)
        {
            StatisticalInformation = await _databaseComponent.GetDailyMedianData<StatisticalInformationData>(startTime, endTime, StatisticalInformationData.GetColumnsString());
            if (DailyChanged != null)
            {               
                DailyChanged();
            }
        }

        //返回统计数据中每天的报警个数
        public List<Tuple<DateTime, int, int, int, int>> GetStatisticalAlarmNumber(string serverip, Guid[] guids)
        {
            if (StatisticalInformation == null)
            {
                return null;
            }        
            if (!StatisticalInformation.ContainsKey(serverip))
            {
                return null;
            }

            var selectStatisticalInfo = StatisticalInformation[serverip].Where(p => (guids == null || guids.Contains(p.T_Item_Guid))).ToArray();

            if (selectStatisticalInfo != null)
            {
                List<Tuple<DateTime, int, int, int, int>> tuple = new List<Tuple<DateTime, int, int, int, int>>();
                var daySelectStatisticalInfo = selectStatisticalInfo.OrderBy(p => p.ACQDatetime).GroupBy(p => p.ACQDatetime.Value.Date);
                foreach (var daysignals in daySelectStatisticalInfo)
                {
                    var allNumber = daysignals.Count();
                    var dangerNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 4).Count();
                    var alarmNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 3).Count();
                    var prealarmNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 2).Count();
                    tuple.Add(new Tuple<DateTime, int, int, int, int>(daysignals.FirstOrDefault().ACQDatetime.Value.Date, allNumber, dangerNumber, alarmNumber, prealarmNumber));
                }
                return tuple;
            }           

            return null;
        }

        public AlarmObjectInfo GetStatisticalAlarmAlarmRate(string serverip)
        {
            if (!StatisticalInformation.ContainsKey(serverip))
            {
                return null;
            }

            var selectStatisticalInfo = StatisticalInformation[serverip].ToArray();
            List<Tuple<DateTime, int, int, int, int, bool>> tuple = new List<Tuple<DateTime, int, int, int, int, bool>>();
            var daySelectStatisticalInfo = selectStatisticalInfo.OrderBy(p => p.ACQDatetime).GroupBy(p => p.ACQDatetime.Value.Date);
            foreach (var daysignals in daySelectStatisticalInfo)
            {
                var allNumber = daysignals.Count();
                var dangerNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 4).Count();
                var alarmNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 3).Count();
                var prealarmNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 2).Count();
                var running = false;//不进行运行判断
                tuple.Add(new Tuple<DateTime, int, int, int, int, bool>(daysignals.FirstOrDefault().ACQDatetime.Value.Date, allNumber, dangerNumber, alarmNumber, prealarmNumber, running));
            }
            AlarmObjectInfo serverinfo = new AlarmObjectInfo();
            serverinfo.Name = serverip;//需要在外部修正名字
            if (tuple.Count != 0)
            {
                serverinfo.AlarmRate = tuple.Select(p => (p.Item2 == 0) ? 0 : (double)(p.Item3 + p.Item4) / p.Item2).Average();
                if (tuple.Select(p => (p.Item2 == 0) ? 0 : (p.Item3)).Average() > 0)
                {
                    serverinfo.AlarmGrade = 4;
                }
                else if (tuple.Select(p => (p.Item2 == 0) ? 0 : (p.Item4)).Average() > 0)
                {
                    serverinfo.AlarmGrade = 3;
                }
                else if (tuple.Select(p => (p.Item2 == 0) ? 0 : (p.Item5)).Average() > 0)
                {
                    serverinfo.AlarmGrade = 2;
                }
                else
                {
                    serverinfo.AlarmGrade = 1;
                }
                serverinfo.DangerNumber = tuple.Select(p => p.Item3).Sum();
                serverinfo.AlarmNumber = tuple.Select(p => p.Item4).Sum();
                serverinfo.PreAlarmNumber = tuple.Select(p => p.Item5).Sum();
                serverinfo.DateRunning = tuple.Select(p => new Tuple<DateTime, bool>(p.Item1, p.Item6)).ToList();//取每一天的运行状态  
            }
            return serverinfo;
        }

        public List<AlarmObjectInfo> GetStatisticalAlarmAlarmRate(string serverip, DeviceTreeItemViewModel[] devices)
        {
            if (!StatisticalInformation.ContainsKey(serverip))
            {
                return null;
            }

            List<AlarmObjectInfo> serverInfoList = new List<AlarmObjectInfo>();
            if (devices == null)
            {              
            }
            else
            { 
                var guids = _cardProcess.GetItems(devices.SelectMany(p => p.Children).ToArray()).Where(p => p.BaseAlarmSignal != null).Select(p => p.BaseAlarmSignal.Guid).ToList();
                var selectStatisticalInfo = StatisticalInformation[serverip].Where(p => (guids == null || guids.Contains(p.T_Item_Guid))).ToArray();

                foreach (var device in devices)
                {
                    var itemguids = device.Children.OfType<ItemTreeItemViewModel>().Where(p => p.BaseAlarmSignal != null).Select(p => p.BaseAlarmSignal.Guid).ToArray();
                    var vibrationguids = device.Children.OfType<ItemTreeItemViewModel>().Where(p => p.BaseAlarmSignal != null && p.BaseAlarmSignal is BaseWaveSignal).Select(p => p.BaseAlarmSignal.Guid).ToArray();
                    List<Tuple<DateTime, int, int, int, int, bool>> tuple = new List<Tuple<DateTime, int, int, int, int, bool>>();
                    var daySelectStatisticalInfo = selectStatisticalInfo.Where(p => itemguids.Contains(p.T_Item_Guid)).OrderBy(p => p.ACQDatetime).GroupBy(p => p.ACQDatetime.Value.Date);
                    foreach (var daysignals in daySelectStatisticalInfo)
                    {
                        var allNumber = daysignals.Count();
                        var dangerNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 4).Count();
                        var alarmNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 3).Count();
                        var prealarmNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 2).Count();
                        int runitem = daysignals.Where(p => vibrationguids.Contains(p.T_Item_Guid) && (p.AlarmGrade & 0xff) >= 1).Count();
                        var running = (runitem >= vibrationguids.Count() - runitem);//运行判断
                        tuple.Add(new Tuple<DateTime, int, int, int, int, bool>(daysignals.FirstOrDefault().ACQDatetime.Value.Date, allNumber, dangerNumber, alarmNumber, prealarmNumber, running));
                    }
                    AlarmObjectInfo serverinfo = new AlarmObjectInfo();
                    serverinfo.Name = device.Name;
                    if (tuple.Count != 0)
                    {
                        serverinfo.AlarmRate = tuple.Select(p => (p.Item2 == 0) ? 0 : (double)(p.Item3 + p.Item4) / p.Item2).Average();
                        if (tuple.Select(p => (p.Item2 == 0) ? 0 : (p.Item3)).Average() > 0)
                        {
                            serverinfo.AlarmGrade = 4;
                        }
                        else if (tuple.Select(p => (p.Item2 == 0) ? 0 : (p.Item4)).Average() > 0)
                        {
                            serverinfo.AlarmGrade = 3;
                        }
                        else if (tuple.Select(p => (p.Item2 == 0) ? 0 : (p.Item5)).Average() > 0)
                        {
                            serverinfo.AlarmGrade = 2;
                        }
                        else
                        {
                            serverinfo.AlarmGrade = 1;
                        }
                        serverinfo.DangerNumber = tuple.Select(p => p.Item3).Sum();
                        serverinfo.AlarmNumber = tuple.Select(p => p.Item4).Sum();
                        serverinfo.PreAlarmNumber = tuple.Select(p => p.Item5).Sum();
                        serverinfo.DateRunning = tuple.Select(p => new Tuple<DateTime, bool>(p.Item1, p.Item6)).ToList();//取每一天的运行状态  
                    }
                    serverInfoList.Add(serverinfo);
                }
            }
            serverInfoList = serverInfoList.OrderByDescending(p => p.AlarmRate).ToList();
            serverInfoList.ForEach(p => p.Index = serverInfoList.IndexOf(p) + 1);
            return serverInfoList;
        }

        public List<AlarmObjectInfo> GetStatisticalAlarmAlarmRate(string serverip, ItemTreeItemViewModel[] items)
        {
            if (!StatisticalInformation.ContainsKey(serverip))
            {
                return null;
            }

            List<AlarmObjectInfo> serverInfoList = new List<AlarmObjectInfo>();
            if (items == null)
            {

            }
            else
            {
                foreach (var item in items)
                {
                    if (item.BaseAlarmSignal == null)
                    {
                        continue;
                    }
                    Guid itemguid = item.BaseAlarmSignal.Guid;
                    var selectStatisticalInfo = StatisticalInformation[serverip].ToArray();
                    List<Tuple<DateTime, int, int, int, int, bool>> tuple = new List<Tuple<DateTime, int, int, int, int, bool>>();
                    var daySelectStatisticalInfo = selectStatisticalInfo.Where(p => itemguid == p.T_Item_Guid).OrderBy(p => p.ACQDatetime).GroupBy(p => p.ACQDatetime.Value.Date);
                    foreach (var daysignals in daySelectStatisticalInfo)
                    {
                        var allNumber = daysignals.Count();
                        var dangerNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 4).Count();
                        var alarmNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 3).Count();
                        var prealarmNumber = daysignals.Where(p => (p.AlarmGrade & 0xff) == 2).Count();
                        var running = false;//
                        if (item.BaseAlarmSignal is BaseWaveSignal)
                        {
                            int runitem = daysignals.Where(p => (p.AlarmGrade & 0xff) >= 1).Count();
                            running = (runitem >= daysignals.Count() - runitem);//运行判断
                        }
                        tuple.Add(new Tuple<DateTime, int, int, int, int, bool>(daysignals.FirstOrDefault().ACQDatetime.Value.Date, allNumber, dangerNumber, alarmNumber, prealarmNumber, running));
                    }
                    AlarmObjectInfo serverinfo = new AlarmObjectInfo();
                    serverinfo.BaseAlarmSignal = item.BaseAlarmSignal;
                    serverinfo.Name = item.BaseAlarmSignal.DeviceItemName;
                    if (tuple.Count != 0)
                    {
                        serverinfo.AlarmRate = tuple.Select(p => (p.Item2 == 0) ? 0 : (double)(p.Item3 + p.Item4) / p.Item2).Average();
                        if (tuple.Select(p => (p.Item2 == 0) ? 0 : (p.Item3)).Average() > 0)
                        {
                            serverinfo.AlarmGrade = 4;
                        }
                        else if (tuple.Select(p => (p.Item2 == 0) ? 0 : (p.Item4)).Average() > 0)
                        {
                            serverinfo.AlarmGrade = 3;
                        }
                        else if (tuple.Select(p => (p.Item2 == 0) ? 0 : (p.Item5)).Average() > 0)
                        {
                            serverinfo.AlarmGrade = 2;
                        }
                        else
                        {
                            serverinfo.AlarmGrade = 1;
                        }
                    }
                    serverInfoList.Add(serverinfo);
                }
            }
            serverInfoList = serverInfoList.OrderByDescending(p => p.AlarmRate).ToList();
            serverInfoList.ForEach(p => p.Index = serverInfoList.IndexOf(p) + 1);
            return serverInfoList;
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

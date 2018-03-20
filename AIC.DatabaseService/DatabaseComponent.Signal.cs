using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nito.AsyncEx;
using AIC.Core.LMModels;
using AIC.M9600.Client.DataProvider;
using AIC.M9600.Common.DTO.Web;
using AIC.M9600.Common.MasterDB.Generated;
using AIC.ServiceInterface;
using AIC.Core.Helpers;
using AIC.Core.Events;
using AIC.Core;
using AIC.M9600.Common.DTO.Device;
using AIC.M9600.Common.SlaveDB.Generated;
using AIC.Core.DataModels;
using AIC.Core.Models;

namespace AIC.DatabaseService
{
    public partial class DatabaseComponent : IDatabaseComponent
    {
        //实时数据查询
        public async Task<Dictionary<string, LatestSampleData>> GetLatestData()
        {
            return await Task.Run(() =>
            {
                Dictionary<string, LatestSampleData> LatestSampleData = new Dictionary<string, LatestSampleData>();
                if (T_RootCard.Count > 0)
                {
                    foreach (var ip in T_RootCard.Keys)
                    {
                        var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                        WebResponse<LatestSampleData> latestResult = client.QueryLatestSampleData(LocalSetting.UpdateTime, null);

                        //先判断是不是OK
                        if (latestResult.IsOK)
                        {
                            LatestSampleData.Add(ip, latestResult.ResponseItem);
                        }
                        else
                        {
                            if (latestResult.ErrorType == "#ClientException")
                            {
                                return null;
                            }
                            else
                            {
                                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(latestResult.ErrorMessage)));
                            }
                        }
                    }
                }
                return LatestSampleData;
            });
        }

        //单测点查询
        public async Task<List<T>> GetHistoryData<T>(string ip, Guid guid, string[] columns, DateTime startTime, DateTime endTime, string condition, object[] args)
        {
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                var historyResult = client.QueryHistorySampleData<T>(new Guid[] { guid },
                columns, startTime, endTime, condition, args);

                //先判断是不是OK
                if (historyResult.IsOK)
                {
                    return historyResult.ResponseItem;
                }
                else
                {
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(historyResult.ErrorMessage)));
                    return null;
                }
            });
        }

        //统一返回基础类型
        public async Task<List<IBaseAlarmSlot>> GetUniformHistoryData(int itemType, string ip, Guid guid, string[] columns, DateTime startTime, DateTime endTime, string condition, object[] args)
        {
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                string errorMessage = null;
                switch (itemType)
                {
                    case (int)ChannelType.IEPEChannelInfo:
                        {
                            var historyResult = client.QueryHistorySampleData<D_IEPESlot>(new Guid[] { guid }, columns, startTime, endTime, condition, args);

                            //先判断是不是OK
                            if (historyResult.IsOK)
                            {
                                return historyResult.ResponseItem.OrderBy(p => p.ACQDatetime).Select(p => ClassCopyHelper.AutoCopy<D_IEPESlot, D1_IEPESlot>(p) as IBaseAlarmSlot).ToList();
                            }
                            else
                            {
                                errorMessage = historyResult.ErrorMessage;
                            }
                            break;
                        }
                    case (int)ChannelType.EddyCurrentDisplacementChannelInfo:
                        {
                            var historyResult = client.QueryHistorySampleData<D_EddyCurrentDisplacementSlot>(new Guid[] { guid }, columns, startTime, endTime, condition, args);

                            //先判断是不是OK
                            if (historyResult.IsOK)
                            {
                                return historyResult.ResponseItem.OrderBy(p => p.ACQDatetime).Select(p => ClassCopyHelper.AutoCopy<D_EddyCurrentDisplacementSlot, D1_EddyCurrentDisplacementSlot>(p) as IBaseAlarmSlot).ToList();
                            }
                            else
                            {
                                errorMessage = historyResult.ErrorMessage;
                            }
                            break;
                        }
                    case (int)ChannelType.EddyCurrentKeyPhaseChannelInfo:
                        {
                            var historyResult = client.QueryHistorySampleData<D_EddyCurrentKeyPhaseSlot>(new Guid[] { guid }, columns, startTime, endTime, condition, args);

                            //先判断是不是OK
                            if (historyResult.IsOK)
                            {
                                return historyResult.ResponseItem.OrderBy(p => p.ACQDatetime).Select(p => ClassCopyHelper.AutoCopy<D_EddyCurrentKeyPhaseSlot, D1_EddyCurrentKeyPhaseSlot>(p) as IBaseAlarmSlot).ToList();
                            }
                            break;
                        }
                    case (int)ChannelType.DigitTachometerChannelInfo:
                        {
                            var historyResult = client.QueryHistorySampleData<D_DigitTachometerSlot>(new Guid[] { guid }, columns, startTime, endTime, condition, args);

                            //先判断是不是OK
                            if (historyResult.IsOK)
                            {
                                return historyResult.ResponseItem.OrderBy(p => p.ACQDatetime).Select(p => ClassCopyHelper.AutoCopy<D_DigitTachometerSlot, D1_DigitTachometerSlot>(p) as IBaseAlarmSlot).ToList();
                            }
                            break;
                        }
                    case (int)ChannelType.AnalogRransducerInChannelInfo:
                        {
                            var historyResult = client.QueryHistorySampleData<D_AnalogRransducerInSlot>(new Guid[] { guid }, columns, startTime, endTime, condition, args);

                            //先判断是不是OK
                            if (historyResult.IsOK)
                            {
                                return historyResult.ResponseItem.OrderBy(p => p.ACQDatetime).Select(p => ClassCopyHelper.AutoCopy<D_AnalogRransducerInSlot, D1_AnalogRransducerInSlot>(p) as IBaseAlarmSlot).ToList();
                            }
                            break;
                        }
                    case (int)ChannelType.RelayChannelInfo:
                        {
                            var historyResult = client.QueryHistorySampleData<D_RelaySlot>(new Guid[] { guid }, columns, startTime, endTime, condition, args);

                            //先判断是不是OK
                            if (historyResult.IsOK)
                            {
                                return historyResult.ResponseItem.OrderBy(p => p.ACQDatetime).Select(p => ClassCopyHelper.AutoCopy<D_RelaySlot, D1_RelaySlot>(p) as IBaseAlarmSlot).ToList();
                            }
                            break;
                        }
                    case (int)ChannelType.DigitRransducerInChannelInfo:
                        {
                            var historyResult = client.QueryHistorySampleData<D_DigitRransducerInSlot>(new Guid[] { guid }, columns, startTime, endTime, condition, args);

                            //先判断是不是OK
                            if (historyResult.IsOK)
                            {
                                return historyResult.ResponseItem.OrderBy(p => p.ACQDatetime).Select(p => ClassCopyHelper.AutoCopy<D_DigitRransducerInSlot, D1_DigitRransducerInSlot>(p) as IBaseAlarmSlot).ToList();
                            }
                            break;
                        }
                    case (int)ChannelType.DigitRransducerOutChannelInfo:
                        {
                            var historyResult = client.QueryHistorySampleData<D_DigitRransducerOutSlot>(new Guid[] { guid }, columns, startTime, endTime, condition, args);

                            //先判断是不是OK
                            if (historyResult.IsOK)
                            {
                                return historyResult.ResponseItem.OrderBy(p => p.ACQDatetime).Select(p => ClassCopyHelper.AutoCopy<D_DigitRransducerOutSlot, D1_DigitRransducerOutSlot>(p) as IBaseAlarmSlot).ToList();
                            }
                            break;
                        }
                    case (int)ChannelType.AnalogRransducerOutChannelInfo:
                        {
                            var historyResult = client.QueryHistorySampleData<D_AnalogRransducerOutSlot>(new Guid[] { guid }, columns, startTime, endTime, condition, args);

                            //先判断是不是OK
                            if (historyResult.IsOK)
                            {
                                return historyResult.ResponseItem.OrderBy(p => p.ACQDatetime).Select(p => ClassCopyHelper.AutoCopy<D_AnalogRransducerOutSlot, D1_AnalogRransducerOutSlot>(p) as IBaseAlarmSlot).ToList();
                            }
                            break;
                        }
                    case (int)ChannelType.WirelessVibrationChannelInfo:
                        {
                            var historyResult = client.QueryHistorySampleData<D_WirelessVibrationSlot>(new Guid[] { guid }, columns, startTime, endTime, condition, args);

                            //先判断是不是OK
                            if (historyResult.IsOK)
                            {
                                return historyResult.ResponseItem.OrderBy(p => p.ACQDatetime).Select(p => ClassCopyHelper.AutoCopy<D_WirelessVibrationSlot, D1_WirelessVibrationSlot>(p) as IBaseAlarmSlot).ToList();
                            }
                            break;
                        }
                    case (int)ChannelType.WirelessScalarChannelInfo:
                        {
                            var historyResult = client.QueryHistorySampleData<D_WirelessScalarSlot>(new Guid[] { guid }, columns, startTime, endTime, condition, args);

                            //先判断是不是OK
                            if (historyResult.IsOK)
                            {
                                return historyResult.ResponseItem.OrderBy(p => p.ACQDatetime).Select(p => ClassCopyHelper.AutoCopy<D_WirelessScalarSlot, D1_WirelessScalarSlot>(p) as IBaseAlarmSlot).ToList();
                            }
                            break;
                        }
                    default: return null;
                }
                if (errorMessage != null)
                {
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(errorMessage)));
                }
                return null;                
               
            });
        }

        //多测点查询
        public async Task<List<T>> GetHistoryData<T>(string ip, Guid[] guids, string[] columns, DateTime startTime, DateTime endTime, string condition, object[] args, IProgress<double> process)
        {
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                var historyResult = client.QueryHistorySampleData<T>(guids,
                columns, startTime, endTime, condition, args);

                //先判断是不是OK
                if (historyResult.IsOK)
                {
                    if (process != null)
                    {
                        process.Report(1);
                    }
                    return historyResult.ResponseItem;
                }
                else
                {
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(historyResult.ErrorMessage)));
                    return null;
                }
            });
        }

        //历史实时数据查询
        public async Task<LatestSampleData> GetHistoryData(string ip, Dictionary<Guid, string> itemGuids, DateTime startTime, DateTime endTime)
        {
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                WebResponse<LatestSampleData> historyResult = client.QueryAllHistorySampleData(itemGuids, startTime, endTime);

                //先判断是不是OK
                if (historyResult.IsOK)
                {
                    return historyResult.ResponseItem;
                }
                else
                {
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(historyResult.ErrorMessage)));
                    return null;
                }

            });

        }

        //多个波形数据查询
        public async Task<List<T>> GetHistoryWaveformData<T>(string ip, Dictionary<Guid, Tuple<Guid, DateTime>> recordLabs, IProgress<double> process)
        {
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                var historyResult = client.QueryHistoryWaveformData<T>(recordLabs, new string[] { "WaveData", "SampleFre", "SamplePoint", "WaveUnit", "T_Item_Guid", "AlarmGrade" });

                //先判断是不是OK
                if (historyResult.IsOK)
                {
                    if (process != null)
                    {
                        process.Report(1);
                    }
                    return historyResult.ResponseItem;
                }
                else
                {
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(historyResult.ErrorMessage)));
                    return null;
                }
            });

        }

        //旧统计数据查询
        public async Task<Dictionary<Guid, Dictionary<string, double>>> GetStatisticsData(string ip, HashSet<Guid> guidlist)
        {
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                var statisticsData = client.QueryStatisticsData(guidlist);
                if (statisticsData.IsOK)
                {
                    //查询成功
                    return statisticsData.ResponseItem;
                }
                else
                {
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(statisticsData.ErrorMessage)));
                    return null;
                }
            });
        }

        //新统计数据查询
        public async Task<Dictionary<Guid, List<D_SlotStatistic>>> GetDailyStatisticsData(string ip, HashSet<Guid> guidlist, DateTime startTime, DateTime endTime)
        {
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                var statisticsData = client.QueryDailyStatisticsData(guidlist, startTime, endTime);
                if (statisticsData.IsOK)
                {
                    //查询成功
                    return statisticsData.ResponseItem;
                }
                else
                {
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(statisticsData.ErrorMessage)));
                    return null;
                }
            });
        }

        //自定义统计查询
        public async Task<List<T>> GetDailyMedianData<T>(string ip, string table, DateTime startTime, DateTime endTime, string columns ="*")//仅扩展了无线振动信号
        {
            return await Task.Run(() =>
            {
                if (startTime > endTime)
                {
                    return null;
                }

                string sqlfull = null;
                for (DateTime dt = startTime; dt <= endTime; dt = Convert.ToDateTime(dt.ToString("yyyy-MM-01 00:00:00")).AddMonths(1))
                {
                    //string tablename = "D_WirelessVibrationSlot_E" + dt.ToString("yyyyMM");
                    string tablename = table + "_E" + dt.ToString("yyyyMM");
                    string sql = @"
                        select " + columns + @"  from (  
                            select *,
	                        ROW_NUMBER() OVER(PARTITION BY T_Item_Guid, CONVERT(varchar(10), ACQDatetime, 120) order by Result) as RowNum2,/*取中值的第一个或第二个点*/
	                        COUNT(*) OVER(PARTITION BY T_Item_Guid, CONVERT(varchar(10), ACQDatetime, 120)) as Cnt2  /*中值有一个或两个点*/

                            from(
                                select *

                                , CONVERT(varchar(10), ACQDatetime, 120) as ACQDate
                                , ROW_NUMBER() OVER(PARTITION BY T_Item_Guid, CONVERT(varchar(10), ACQDatetime, 120) order by Result) as RowNum,
                                COUNT(*) OVER(PARTITION BY T_Item_Guid, CONVERT(varchar(10), ACQDatetime, 120)) as Cnt

                                from[AIC9600Slave].[dbo].[" + tablename + @"] where  ACQDatetime >= '" + dt.ToString("yyyy-MM-dd HH:mm:ss") + @"' and ACQDatetime <= '" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + @"') o
                          where RowNum IN((Cnt + 1) / 2, (Cnt + 2) / 2)   
                        )a
                        where RowNum2 = Cnt2 /*取中值的第两个点*/";
                    if (sqlfull == null)
                    {
                        sqlfull = sql;
                    }
                    else
                    {
                        sqlfull += " union " + sql;
                    }

                }

                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                var midResult = client.QueryWithCustomSQL<T>(sqlfull, null);

                if (midResult.IsOK)
                {
                    //查询成功
                    return midResult.ResponseItem;
                }
                else
                {
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(midResult.ErrorMessage)));
                    return null;
                }
            });
        }

        //实时自定义统计查询
        public async Task<Dictionary<string, List<T>>> GetDailyMedianData<T>(DateTime startTime, DateTime endTime, string columns = "*")
        {
            Dictionary<string, List<T>> mediandata = new Dictionary<string, List<T>>();
            if (T_RootCard.Count > 0)
            {                
                foreach (var ip in T_RootCard.Keys)
                {
                    mediandata.Add(ip, new List<T>());
                    var data = await GetDailyMedianData<T>(ip, "D_WirelessVibrationSlot", startTime, endTime, columns);
                    if (data != null)
                    {
                        mediandata[ip].AddRange(data);
                    }
                    data = await GetDailyMedianData<T>(ip, "D_WirelessScalarSlot", startTime, endTime, columns);
                    if (data != null)
                    {
                        mediandata[ip].AddRange(data);
                    }
                }
            }
            return mediandata;
        }
    }
}

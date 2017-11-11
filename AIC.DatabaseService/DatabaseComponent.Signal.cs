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

namespace AIC.DatabaseService
{
    public partial class DatabaseComponent : IDatabaseComponent
    {
        public async Task<Dictionary<string, LatestSampleData>> GetLatestData()
        {
            return await Task.Run(() =>
            {
                Dictionary<string, LatestSampleData> LatestSampleData = new Dictionary<string, LatestSampleData>();
                if (T_RootCard.Count > 0)
                {
                    foreach (var ip in T_RootCard.Keys)
                    {
                        var client = new DataProvider(ip, LocalAddress.ServerPort, LocalAddress.MajorVersion, LocalAddress.MinorVersion);
                        WebResponse<LatestSampleData> latestResult = client.QueryLatestSampleData(LocalAddress.UpdateTime, null);

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

        public async Task<List<T>> GetHistoryData<T>(string ip, Guid guid, string[] columns, DateTime startTime, DateTime endTime, string condition, object[] args)
        {
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalAddress.ServerPort, LocalAddress.MajorVersion, LocalAddress.MinorVersion);
                var historyResult = client.QueryHistorySampleData<T>(guid,
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

        public async Task<LatestSampleData> GetHistoryData(string ip, Dictionary<Guid, string> itemGuids, DateTime startTime, DateTime endTime)
        {
            return await Task.Run(() =>
            {               
                var client = new DataProvider(ip, LocalAddress.ServerPort, LocalAddress.MajorVersion, LocalAddress.MinorVersion);
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

        public async Task<Dictionary<Guid, Dictionary<string, double>>> GetStatisticsData(string ip, HashSet<Guid> guidlist)
        {
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalAddress.ServerPort, LocalAddress.MajorVersion, LocalAddress.MinorVersion);
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


    }
}

using AIC.M9600.Client.DataProvider;
using CitizenSoftwareLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIC9600QueryDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DataProvider client = new DataProvider(AppConfigOperator.GetAppConfigValue<string>("ServerIP"), AppConfigOperator.GetAppConfigValue<int>("ServerPort"), 1, 1, TimeSpan.FromSeconds(5));

                while (true)
                {
                    try
                    {
                        LogFactory.Get().Info("查询数据");
                        var data = client.QueryLatestSampleData(0, null);
                        if (data.IsOK)
                        {
                            LogFactory.Get().Info("WirelessScalarSlot Count = " + (data.ResponseItem.WirelessScalarSlot == null ? 0 : data.ResponseItem.WirelessScalarSlot.Length));
                            LogFactory.Get().Info("WirelessVibrationSlot Count = " + (data.ResponseItem.WirelessVibrationSlot == null ? 0 : data.ResponseItem.WirelessVibrationSlot.Length));
                        }
                        else
                        {
                            LogFactory.Get().Error("查询错误:" + data.ErrorMessage);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogFactory.Get().Error("异常:" + ex.Message);
                    }
                    finally
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
            catch(Exception ex)
            {
                LogFactory.Get().Error("异常:" + ex.Message);
            }
        }
    }
}

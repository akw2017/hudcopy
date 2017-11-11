using AIC.Core;
using AIC.Server.Common;
using AIC.Server.Storage.Contract;
using AIC.ServiceInterface;
using Newtonsoft.Json;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIC.RTDataService
{
    public class RTDataService : IRTDataService
    {
        private readonly IEventAggregator _eventAggregator;
        private string ctlAddress;
        public RTDataService(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            ctlAddress = ServerAddress.CTLAddress.Split('/')[2].Split(':')[0];
        }
        #region IService Members

        public async Task<ChannelDataContract[]> GetChannelContractsAsync()
        {
            try
            {
                string[] json = await Task.Run<string[]>(() => { return SocketCaller.ExecuteMethod(ctlAddress, 39997, "Query", 1, true, null); });
                if (!json[0].StartsWith("#"))
                {
                    ChannelDataContract[] conracts = new ChannelDataContract[json.Length];
                    for (int i = 0; i < json.Length; i++)
                    {
                        conracts[i] = JsonConvert.DeserializeObject<Dictionary<int, ChannelDataContract>>(json[i]).Single().Value;
                    }
                    return conracts;
                }
                return null;
            }
            catch (Exception ex)
            {
                //_eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("信号服务-读取数据失败", ex));
                return null;
            }
        }

        #endregion
    }
}

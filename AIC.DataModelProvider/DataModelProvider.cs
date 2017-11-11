using AIC.Cloud.Applications;
using AIC.Cloud.Applications.Events;
using AIC.Cloud.Database;
using AIC.CoreType;
using AIC.Server.ThirdParty.Contract;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DataModelProvider
{
    [Export(typeof(DataModelProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DataModelProvider : IAsyncInitialization
    {
        //private DatabaseComponent database;
        private List<LMCommandPara> lmCommandList;
        private List<LMHDPara> lmHDParaList;
        private List<LMVInfo> lmVInfoList;
        private List<LMAnInfo> lmAnInfoList;
        private List<LMVedio> lmVideos;
        private List<ThirdDataMapper> trdDataMapperList;
        private readonly IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public DataModelProvider(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            lmCommandList = new List<LMCommandPara>();
            lmHDParaList = new List<LMHDPara>();
            lmVInfoList = new List<LMVInfo>();
            lmAnInfoList = new List<LMAnInfo>();
            lmVideos = new List<LMVedio>();
            trdDataMapperList = new List<ThirdDataMapper>();
            Initialization = InitializeAsync();
        }
        public Task Initialization { get; private set; }

        public async Task InitializeAsync()
        {
            try
            {
                //database = await DatabaseComponent.Instance;
                CreateLMInfos(database);
            }
            catch (Exception ex)
            {
                //_eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据模型-初始化失败", ex));
            }
        }

        public void Resolve(string ip)
        {
            if (LMCommands.Select(o => o.IP).Contains(ip))
            {
                throw new Exception(string.Format("IP:{0}已存在", ip));
            }
            var lmCommadnContract = database.LMCommands.Where(o => o.IP == ip).SingleOrDefault();
            if (lmCommadnContract == null)
            {
                throw new Exception(string.Format("数据库不存在IP:{0}的数采器", ip));
            }
            LMCommandPara lmCommandPara = new LMCommandPara(lmCommadnContract);
            lmCommandList.Add(lmCommandPara);

            var availableRSlotNums = database.LMAnInfos.Where(o => o.IP == ip && o.SignalType == (int)SignalType.Digital).Select(o => o.SlotNum + 1).Distinct().ToArray();
            var availableRChaNs = database.LMAnInfos.Where(o => o.IP == ip && o.SignalType == (int)SignalType.Digital).Select(o => o.ChaN + 1).Distinct().ToArray();

            foreach (var item in database.LMHDParas.Where(o => o.IP == ip))
            {
                LMHDPara lmHDPara = new LMHDPara(item);
                lmHDPara.AvailableRSlotNums = availableRSlotNums;
                lmHDPara.AvailableRChaNs = availableRChaNs;
                lmHDParaList.Add(lmHDPara);
            }

            foreach (var item in database.LMVInfos.Where(o => o.IP == ip))
            {
                LMVInfo lmVInfo = new LMVInfo(item);
                lmVInfo.AvailableRSlotNums = availableRSlotNums;
                lmVInfo.AvailableRChaNs = availableRChaNs;
                var lmDivFres = database.LMDivFres.Where(o => o.ChannelID == item.ChannelID).ToArray();
                foreach (var div in lmDivFres)
                {
                    LMDivFre divFre = new LMDivFre(div);
                    lmVInfo.DivFres.Add(divFre);
                }
                lmVInfoList.Add(lmVInfo);
            }

            foreach (var item in database.LMAnInfos.Where(o => o.IP == ip))
            {
                LMAnInfo lmAnInfo = new LMAnInfo(item);
                lmAnInfoList.Add(lmAnInfo);
            }
        }

        private void CreateLMInfos(DatabaseComponent database)
        {
            foreach (var item in database.LMCommands)
            {
                lmCommandList.Add(new LMCommandPara(item));
            }
            foreach (var item in database.LMHDParas)
            {
                lmHDParaList.Add(new LMHDPara(item));
            }
            foreach (var item in database.LMVInfos)
            {
                LMVInfo lmVInfo = new LMVInfo(item);
                var lmDivFres = database.LMDivFres.Where(o => o.ChannelID == item.ChannelID).ToArray();
                foreach (var lmDivfre in lmDivFres)
                {
                    lmVInfo.DivFres.Add(new LMDivFre(lmDivfre));
                }
                lmVInfoList.Add(lmVInfo);
            }
            foreach (var item in database.LMAnInfos)
            {
                lmAnInfoList.Add(new LMAnInfo(item));
                if (item.SignalType == 2)
                {
                    int slotnum = item.SlotNum;
                }
            }
            foreach(var item in database.LMVideos)
            {
                lmVideos.Add(new LMVedio(item));
            }
            foreach (var item in database.TrdDataContracts)
            {
                trdDataMapperList.Add(new ThirdDataMapper(item));
            }

            var availableRS = LMAnInfos.Where(o => o.SignalType == SignalType.Digital).GroupBy(o => o.IP);
            foreach (var item in availableRS)
            {
                var availableRSlotNums = item.Select(o => o.SlotNum).Distinct().ToArray();
                var availableRChaNs = item.Select(o => o.ChaN).Distinct().ToArray();
                foreach (var lmHD in LMHDParas.Where(o => o.IP == item.Key))
                {
                    lmHD.AvailableRSlotNums = availableRSlotNums;
                    lmHD.AvailableRChaNs = availableRChaNs;
                }

                foreach (var lmVInfo in LMVInfos.Where(o => o.IP == item.Key))
                {
                    lmVInfo.AvailableRSlotNums = availableRSlotNums;
                    lmVInfo.AvailableRChaNs = availableRChaNs;
                }
            }
        }

        public void AddLMCommand(LMCommandPara lmCommandPara)
        {
            if (lmCommandPara == null) return;
            if (!lmCommandList.Contains(lmCommandPara))
            {
                lmCommandList.Add(lmCommandPara);
            }
        }
        public void RemoveLMCommand(LMCommandPara lmCommandPara)
        {
            if (lmCommandList.Contains(lmCommandPara))
            {
                lmCommandList.Remove(lmCommandPara);
            }
        }

        public void AddLMHDPara(LMHDPara lmHDPara)
        {
            if (lmHDPara == null) return;
            if (!lmHDParaList.Contains(lmHDPara))
            {
                lmHDParaList.Add(lmHDPara);
            }
        }
        public void RemoveLMHDPara(LMHDPara lmHDPara)
        {
            if (lmHDParaList.Contains(lmHDPara))
            {
                lmHDParaList.Remove(lmHDPara);
            }
        }

        public void AddLMVInfo(LMVInfo lmVInfo)
        {
            if (lmVInfo == null) return;
            if (!lmVInfoList.Contains(lmVInfo))
            {
                lmVInfoList.Add(lmVInfo);
            }
        }
        public void RemoveLMVInfo(LMVInfo lmVInfo)
        {
            if (lmVInfoList.Contains(lmVInfo))
            {
                lmVInfoList.Remove(lmVInfo);
            }
        }

        public void AddLMAnInfo(LMAnInfo lmAnInfo)
        {
            if (lmAnInfo == null) return;
            if (!lmAnInfoList.Contains(lmAnInfo))
            {
                lmAnInfoList.Add(lmAnInfo);
            }
        }
        public void RemoveLMAnInfo(LMAnInfo lmAnInfo)
        {
            if (lmAnInfoList.Contains(lmAnInfo))
            {
                lmAnInfoList.Remove(lmAnInfo);
            }
        }

        public void AddThirdDataMapper(ThirdDataMapper trdDataMapper)
        {
            if (trdDataMapper == null) return;
            if (!trdDataMapperList.Contains(trdDataMapper))
            {
                trdDataMapperList.Add(trdDataMapper);
            }
        }
        public void RemoveThirdDataMapper(ThirdDataMapper trdDataMapper)
        {
            if (trdDataMapperList.Contains(trdDataMapper))
            {
                trdDataMapperList.Remove(trdDataMapper);
            }
        }

        public IEnumerable<LMCommandPara> LMCommands { get { return lmCommandList; } }
        public IEnumerable<LMHDPara> LMHDParas { get { return lmHDParaList; } }
        public IEnumerable<LMVInfo> LMVInfos { get { return lmVInfoList; } }
        public IEnumerable<LMAnInfo> LMAnInfos { get { return lmAnInfoList; } }
        public IEnumerable<LMVedio> LMVedios { get { return lmVideos; } }     
        public IEnumerable<ThirdDataMapper> TrdDataMappers { get { return trdDataMapperList; } }
    }
}

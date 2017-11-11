using AIC.Core;
using AIC.CoreType;
using AIC.Database;
using AIC.Domain;
using AIC.OnlineSystem.Client;
using AIC.OnlineSystem.Server.DB.Models.Generated.Master;
using AIC.Server.Storage.Contract;
using AIC.ServiceInterface;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using MoreLinq;


namespace AIC.TreeService
{
    public class TreeService : ITreeService
    {
        private string NULLGLOBALID = BitConverter.ToString(SHA1Managed.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Empty))).Replace("-", string.Empty);
        private List<GroupTreeModel> groupTMList;
        private List<PDATreeModel> pdaTMList;
   
        private readonly IEventAggregator _eventAggregator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPDAService _pdaService;

        //private IDictionary<Guid, ChannelTreePair> channelDict;
        //private IDictionary<Guid, TestPointModel> testPointModels;

        private IDictionary<Guid, TestPoint> tpDict;
        private IDictionary<Guid, TestPointTreeModel> tpTMDict;
        private IDictionary<ChannelIdentity, ChannelTreeModel> channelTMDict;
   

        public TreeService(IEventAggregator eventAggregator,IUnitOfWork unitOfWork, IPDAService pdaService)
        {
            _eventAggregator = eventAggregator;
            _unitOfWork = unitOfWork;
            _pdaService = pdaService;

            groupTMList = new List<GroupTreeModel>();
            pdaTMList = new List<PDATreeModel>();

            tpDict = new Dictionary<Guid, TestPoint>();
            tpTMDict = new Dictionary<Guid, TestPointTreeModel>();
            channelTMDict = new Dictionary<ChannelIdentity, ChannelTreeModel>();
        }

        public Result Result { get; private set;}

        public void Initialize()
        {
            try
            {
                QueryTestPoints();
                BuildPDATrees();
                BuildGroupTrees();
            }
            catch (Exception ex)
            {
                Result = Result.Combine(Result, Result.Fail(ex.ToString()));
            }
        }

        private void QueryTestPoints()
        {
            _unitOfWork.TestPoints.Query()
                .Where(o => o.TestPointId != null)
                .ForEach(o =>
                {
                    if (!tpDict.ContainsKey(o.TestPointId.Value))
                    {
                        tpDict.Add(o.TestPointId.Value, o);
                    }
                });
            //UnitOfWorkDemo demo = new UnitOfWorkDemo();
            //demo.TestPoints.Query()
            //.Where(o => o.TestPointId != null)
            //.ForEach(o =>
            //{
            //    if (!tpDict.ContainsKey(o.TestPointId.Value))
            //    {
            //        tpDict.Add(o.TestPointId.Value, o);
            //    }
            //});
        }

        public void Save()
        {
            //var testPoints = groupTMList.SelectMany(o => o.Children).SelectMany(o => o.Children).SelectMany(o => o.Children).SelectMany(o => o.Children).OfType<TestPointTreeModel>();

            //testPoints.Where(o => !testPointModels.ContainsKey(o.TestPointId))
            //          .Select(o =>
            //          {
            //              var tm = CreateFromTreeModel(o);
            //              testPointModels.Add(o.TestPointId, tm);
            //              return tm;
            //          })
            //          .Select(o => ConverterToTestPoint(o))
            //          .ForEach(o => _unitOfWork.TestPoints.Add(o));

            //testPoints.Where(o => !CheckChange(o, testPointModels[o.TestPointId]))
            //          .Select(o =>
            //          {
            //              AssignChange(o, testPointModels[o.TestPointId]);
            //              return testPointModels[o.TestPointId];
            //          })
            //          .Select(o => ConverterToTestPoint(o))
            //          .ForEach(o => _unitOfWork.TestPoints.Update(o.id, o));
        }

        private void AssignChange(TestPointTreeModel tp, TestPointModel model)
        {
            model.GroupCOName = tp.Location[0].Value;
            model.CorporationName = tp.Location[1].Value;
            model.WorkShopName = tp.Location[2].Value;
            model.DevName = tp.Location[3].Value;
            model.DevSN = tp.Location[4].Value;
            model.Name = tp.Location[5].Value;
            model.MSSN = tp.Location[6].Value;
        }

        private bool CheckChange(TestPointTreeModel treeModel, TestPointModel model)
        {
            var location = treeModel.Location;
            bool result1 = location[0].Value == model.GroupCOName
                && location[1].Value == model.CorporationName
                && location[2].Value == model.WorkShopName
                && location[3].Value == model.DevName
                && location[4].Value == model.DevSN
                && location[5].Value == model.Name
                && location[6].Value == model.MSSN;

            if (treeModel.ChannelId.HasValue)
            {
                bool result2 = treeModel.ChannelId.Value.IP == model.IP
                    && treeModel.ChannelId.Value.CardNum == model.CardNum
                    && treeModel.ChannelId.Value.ChannelNum == model.ChannelNum;
                return result1 && result2;
            }
            return result1;
        }

        private TestPointModel CreateFromTreeModel(TestPointTreeModel treeModel)
        {
            var tm = new TestPointModel(new TestPoint())
            {
                TestPointId = treeModel.TestPointId,
                GroupCOName = treeModel.Location[0].Value,
                CorporationName = treeModel.Location[1].Value,
                WorkShopName = treeModel.Location[2].Value,
                DevName = treeModel.Location[3].Value,
                DevSN = treeModel.Location[4].Value,
                Name = treeModel.Location[5].Value,
                MSSN = treeModel.Location[6].Value,
            };

            if (treeModel.ChannelId.HasValue)
            {
                tm.IP = treeModel.ChannelId.Value.IP;
                tm.CardNum = treeModel.ChannelId.Value.CardNum;
                tm.ChannelNum = treeModel.ChannelId.Value.ChannelNum;
            }

            return tm;
        }

        private TestPointModel ConverterToTestPointModel(TestPoint tp)
        {
            return ObjectConvertor<TestPoint, TestPointModel>.Convert(tp);
        }

        private TestPoint ConverterToTestPoint(TestPointModel tpModel)
        {
            return ObjectConvertor<TestPointModel, TestPoint>.Convert(tpModel);
        }

        private void BuildPDATrees()
        {
            var pdas = _pdaService.GetPDAs();
            foreach (var pda in pdas)
            {
                var pdaTM = new PDATreeModel(pda.IP);
                foreach (var card in pda.Cards)
                {
                    var cardTM = new CardTreeModel(card.CardId.IP, card.CardId.CardNum);
                    pdaTM.AddChild(cardTM);
                    foreach (var channel in card.Channels)
                    {
                        var channelId = ChannelIdentity.Create(channel.ChannelId.IP, channel.ChannelId.CardNum, channel.ChannelId.ChannelNum).Value;
                        var channelTM = new ChannelTreeModel(channelId);
                        cardTM.AddChild(channelTM);
                        if (!channelTMDict.ContainsKey(channelId))
                        {
                            channelTMDict.Add(channelId, channelTM);
                        }
                    }
                }
                pdaTMList.Add(pdaTM);
            }
        }

        private void BuildGroupTrees()
        {
            var allChannels = pdaTMList.SelectMany(o => o.Children).SelectMany(o => o.Children).OfType<ChannelTreeModel>().ToArray();
            var groupTMs = tpDict.Values.GroupBy(o => o.GroupCOName, o => o);
            foreach (var g in groupTMs)
            {
                GroupTreeModel group = groupTMList.Where(o => o.Name.Value == g.Key).SingleOrDefault();
                if (group == null)
                {
                    group = new GroupTreeModel(g.Key);
                    groupTMList.Add(group);
                }

                var corporations = g.GroupBy(o => o.CorporationName, o => o);
                foreach (var corp in corporations)
                {
                    CorporationTreeModel corporation = group.Children.Where(o => o.Name.Value == corp.Key).SingleOrDefault() as CorporationTreeModel;
                    if (corporation == null)
                    {
                        corporation = new CorporationTreeModel(corp.Key);
                        group.AddChild(corporation);
                    }
                    var worpshops = corp.GroupBy(o => o.WorkShopName, o => o);
                    foreach (var ws in worpshops)
                    {
                        WorkShopTreeModel workshop = corporation.Children.Where(o => o.Name.Value == ws.Key).SingleOrDefault() as WorkShopTreeModel;
                        if (workshop == null)
                        {
                            workshop = new WorkShopTreeModel(ws.Key);
                            corporation.AddChild(workshop);
                        }
                        var equips = ws.OrderBy(o => OrderFunc(o)).OrderBy(o => o.DevName).GroupBy(o => o.DevName + "|" + o.DevSN, o => o);
                        foreach (var equip in equips)
                        {
                            EquipmentTreeModel equipment = workshop.Children.Where(o => o.Name.Value == equip.Key.Split('|')[0] && ((EquipmentTreeModel)o).MSSN.Value == equip.Key.Split('|')[1]).SingleOrDefault() as EquipmentTreeModel;
                            if (equipment == null)
                            {
                                equipment = new EquipmentTreeModel(equip.Key.Split('|')[0], equip.Key.Split('|')[1]);
                                workshop.AddChild(equipment);
                            }
                            foreach (var model in equip)
                            {
                                TestPointTreeModel testPoint = new TestPointTreeModel(model);
                                equipment.AddChild(testPoint);
                                CheckBind(testPoint);
                            }
                        }
                    }
                }
            }
        }

        private void CheckBind(TestPointTreeModel testPoint)
        {
            if (testPoint.IsPaired && !channelTMDict.ContainsKey(testPoint.ChannelId.Value))
            {
                testPoint.BindMiss();
            }
            else
            {
                channelTMDict[testPoint.ChannelId.Value].Bind();
            }
        }

        private int OrderFunc(TestPoint contract)
        {
            int x = 0;
            Int32.TryParse(Regex.Match(contract.Name, @"\d+").Value, out x);
            return x;
        }

        public IEnumerable<GroupTreeModel> GetAllGroups()
        {
            return groupTMList;
        }

        public IEnumerable<PDATreeModel> GetAllPDAs()
        {
            return pdaTMList;
        }

        public Result<TreeViewItemModel> AddTreeModel(TreeViewItemModel arg)
        {
            try
            {
                TreeViewItemModel treeModel = null;
                if (arg == null)
                {
                    int max = 0;
                    if (groupTMList.Count > 0)
                    {
                        string[] indexString = groupTMList.Select(o => o.Name.Value.Split('#').LastOrDefault()).ToArray();
                        int[] indexs = new int[indexString.Length];
                        for (int i = 0; i < indexs.Length; i++)
                        {
                            int.TryParse(indexString[i], out indexs[i]);
                        }
                        max = indexs.Max();
                    }

                    string groupName = string.Format("总厂#{0}", max + 1);
                    GroupTreeModel group = new GroupTreeModel(groupName);
                    CorporationTreeModel corp = new CorporationTreeModel("分厂#1");
                    group.AddChild(corp);
                    WorkShopTreeModel workshop = new WorkShopTreeModel("车间#1");
                    corp.AddChild(workshop);
                    EquipmentTreeModel equipemnt = new EquipmentTreeModel("设备", "#1");
                    workshop.AddChild(equipemnt);
                    var testPoint = CreateTestPointTreeModel("测点", "#1");
                    equipemnt.AddChild(testPoint);
                    groupTMList.Add(group);
                    treeModel = group;

                    if(!tpTMDict.ContainsKey(testPoint.TestPointId))
                    {
                        tpTMDict.Add(testPoint.TestPointId, testPoint);
                    }
                }
                else if (arg is GroupTreeModel)
                {
                    GroupTreeModel group = arg as GroupTreeModel;
                    int max = 0;
                    if (group.Count > 0)
                    {
                        string[] indexString = group.Children.Select(o => o.Name.Value.Split('#').LastOrDefault()).ToArray();
                        int[] indexs = new int[indexString.Length];
                        for (int i = 0; i < indexs.Length; i++)
                        {
                            int.TryParse(indexString[i], out indexs[i]);
                        }
                        max = indexs.Max();
                    }
                    string corporationName = string.Format("分厂#{0}", max + 1);
                    CorporationTreeModel corp = new CorporationTreeModel(corporationName);
                    group.AddChild(corp);
                    WorkShopTreeModel workshop = new WorkShopTreeModel("车间#1");
                    corp.AddChild(workshop);
                    EquipmentTreeModel equipemnt = new EquipmentTreeModel("设备", "#1");
                    workshop.AddChild(equipemnt);
                    var testPoint = CreateTestPointTreeModel("测点", "#1");
                    equipemnt.AddChild(testPoint);
                    treeModel = corp;

                    if (!tpTMDict.ContainsKey(testPoint.TestPointId))
                    {
                        tpTMDict.Add(testPoint.TestPointId, testPoint);
                    }
                }
                else if (arg is CorporationTreeModel)
                {
                    CorporationTreeModel corp = arg as CorporationTreeModel;
                    int max = 0;
                    if (corp.Count > 0)
                    {
                        string[] indexString = corp.Children.Select(o => o.Name.Value.Split('#').LastOrDefault()).ToArray();
                        int[] indexs = new int[indexString.Length];
                        for (int i = 0; i < indexs.Length; i++)
                        {
                            int.TryParse(indexString[i], out indexs[i]);
                        }
                        max = indexs.Max();
                    }
                    string workshopName = string.Format("车间#{0}", max + 1);
                    WorkShopTreeModel workshop = new WorkShopTreeModel(workshopName);
                    corp.AddChild(workshop);
                    EquipmentTreeModel equipemnt = new EquipmentTreeModel("设备", "#1");
                    workshop.AddChild(equipemnt);
                    var testPoint = CreateTestPointTreeModel("测点", "#1");
                    equipemnt.AddChild(testPoint);
                    treeModel = workshop;

                    if (!tpTMDict.ContainsKey(testPoint.TestPointId))
                    {
                        tpTMDict.Add(testPoint.TestPointId, testPoint);
                    }
                }
                else if (arg is WorkShopTreeModel)
                {
                    WorkShopTreeModel workshop = arg as WorkShopTreeModel;
                    int max = 0;
                    if (workshop.Count > 0)
                    {
                        string[] indexString = workshop.Children.Select(o => ((EquipmentTreeModel)o).MSSN.Value.Split('#').LastOrDefault()).ToArray();
                        int[] indexs = new int[indexString.Length];
                        for (int i = 0; i < indexs.Length; i++)
                        {
                            int.TryParse(indexString[i], out indexs[i]);
                        }
                        max = indexs.Max();
                    }
                    CorporationTreeModel corp = workshop.Parent as CorporationTreeModel;
                    GroupTreeModel group = corp.Parent as GroupTreeModel;
                    string equipmentMssn = string.Format("#{0}", max + 1);
                    EquipmentTreeModel equipemnt = new EquipmentTreeModel("设备", equipmentMssn);
                    workshop.AddChild(equipemnt);
                    var testPoint = CreateTestPointTreeModel("测点", "#1");
                    equipemnt.AddChild(testPoint);
                    treeModel = equipemnt;

                    if (!tpTMDict.ContainsKey(testPoint.TestPointId))
                    {
                        tpTMDict.Add(testPoint.TestPointId, testPoint);
                    }
                }
                else if (arg is EquipmentTreeModel)
                {
                    EquipmentTreeModel equipemnt = arg as EquipmentTreeModel;
                    int max = 0;
                    if (equipemnt.Count > 0)
                    {
                        string[] indexString = equipemnt.Children.Select(o => ((TestPointTreeModel)o).MSSN.Value.Split('#').LastOrDefault()).ToArray();
                        int[] indexs = new int[indexString.Length];
                        for (int i = 0; i < indexs.Length; i++)
                        {
                            int.TryParse(indexString[i], out indexs[i]);
                        }
                        max = indexs.Max();
                    }
                    string tpMssn = string.Format("#{0}", max + 1);

                    var testPoint =  CreateTestPointTreeModel("测点", tpMssn);
                    equipemnt.AddChild(testPoint);
                    treeModel = testPoint;

                    if (!tpTMDict.ContainsKey(testPoint.TestPointId))
                    {
                        tpTMDict.Add(testPoint.TestPointId, testPoint);
                    }
                }
                else if (arg is TestPointTreeModel)
                {
                    TestPointTreeModel testPoint = arg as TestPointTreeModel;
                    if (testPoint.SignalType == SignalType.Vibration && testPoint.IsPaired)
                    {
                        int max = 0;
                        if (testPoint.Count > 0)
                        {
                            string[] indexString = testPoint.Children.Select(o => o.Name.Value.Split('#').LastOrDefault()).ToArray();
                            int[] indexs = new int[indexString.Length];
                            for (int i = 0; i < indexs.Length; i++)
                            {
                                int.TryParse(indexString[i], out indexs[i]);
                            }
                            max = indexs.Max();
                        }
                        string divFreDescription = string.Format("分频#{0}", max + 1);
                        DivFreTreeModel divFreTM = new DivFreTreeModel(divFreDescription);
                        testPoint.AddChild(divFreTM);
                        treeModel = divFreTM;

                    }
                }
                return Result.Ok(treeModel);
            }
            catch (Exception ex)
            {
                return Result.Fail<TreeViewItemModel>(ex.ToString());
            }
        }

        private TestPointTreeModel CreateTestPointTreeModel(string name,string mssn)
        {
            var testPoint = new TestPoint()
            {
                TestPointId=Guid.NewGuid(),
                Name = name,
                MSSN = mssn,
            };
           return new TestPointTreeModel(testPoint);
        }

        public Result DeleteTreeModel(TreeViewItemModel model)
        {
            List<TestPointTreeModel> bindedTestPoints = new List<TestPointTreeModel>();
            if (model is GroupTreeModel)
            {
                bindedTestPoints = ((GroupTreeModel)model).Children.SelectMany(o => o.Children).SelectMany(o => o.Children).SelectMany(o => o.Children).OfType<TestPointTreeModel>().ToList();
            }
            else if (model is CorporationTreeModel)
            {
                bindedTestPoints = ((CorporationTreeModel)model).Children.SelectMany(o => o.Children).SelectMany(o => o.Children).OfType<TestPointTreeModel>().ToList();
            }
            else if (model is WorkShopTreeModel)
            {
                bindedTestPoints = ((WorkShopTreeModel)model).Children.SelectMany(o => o.Children).OfType<TestPointTreeModel>().ToList();
            }
            else if (model is EquipmentTreeModel)
            {
                bindedTestPoints = ((EquipmentTreeModel)model).Children.OfType<TestPointTreeModel>().ToList();
            }
            else if (model is TestPointTreeModel)
            {
                bindedTestPoints.Add(model as TestPointTreeModel);
            }
            else if (model is DivFreTreeModel)
            {
                var div = model as DivFreTreeModel;
                var tp = div.Parent as TestPointTreeModel;
                if (tp.IsPaired)
                {
                  
                }
            }

            bindedTestPoints.Where(o => o.IsPaired)
                .ForEach(o =>
                {
                    o.UnBind();
                    if (tpTMDict.ContainsKey(o.TestPointId))
                    {
                        tpTMDict.Remove(o.TestPointId);
                    }
                });

            //bindedTestPoints.Where(o=>testPointModels.ContainsKey(o.TestPointId))
            //    .Select(o=> testPointModels[o.TestPointId].id)
            //    .ForEach(o => _unitOfWork.TestPoints.Delete(o));


            if (model is GroupTreeModel)
            {
                groupTMList.Remove(model as GroupTreeModel);
            }
            else
            {
                var parent = model.Parent;
                if (parent != null)
                {
                    parent.RemoveChild(model);
                    if (parent.Count == 0)
                    {
                        parent.Alarm = AlarmGrade.HighNormal;
                    }
                }
            }
            return Result.Ok();
        }

        public Result<ChannelTreeModel> GetChannel(ChannelIdentity channelId)
        {
            if (channelTMDict.ContainsKey(channelId))
            {
                return Result.Ok(channelTMDict[channelId]);
            }
            return Result.Fail<ChannelTreeModel>("通道不存在");
        }

        public Result<TestPointTreeModel> GetTestPoint(ChannelIdentity channelId)
        {
            var testPoint = tpTMDict.Values.Where(o => o.IsPaired && o.ChannelId.Value == channelId).SingleOrDefault();
            if (testPoint != null)
            {
                return Result.Ok(testPoint);
            }
            return Result.Fail<TestPointTreeModel>("测点不存在");
        }

        public Result BindChannel(Guid id, ChannelIdentity channelId)
        {
            if (tpTMDict.ContainsKey(id))
            {
                tpTMDict[id].Bind(channelId);
            }
            if (channelTMDict.ContainsKey(channelId))
            {
                channelTMDict[channelId].Bind();
            }

            // _unitOfWork.TestPoints.Update(tpModel.id, ConverterToTestPoint(tpModel));
            return Result.Ok();
            // return Result.Fail<TestPointTreeModel>("通道Id不存在");
        }

        public Result UnBindChannel(ChannelIdentity channelId)
        {
            if(channelTMDict.ContainsKey(channelId))
            {
                channelTMDict[channelId].UnBind();
            }

            var testPoint = tpTMDict.Values.Where(o => o.IsPaired && o.ChannelId.Value == channelId).SingleOrDefault();
            if (testPoint != null)
            {
                testPoint.UnBind();
            }
           // _unitOfWork.TestPoints.Update(tpModel.id, ConverterToTestPoint(tpModel));

            return Result.Ok();
        }

        public Result DeleteDataCollector(PDATreeModel dataCollector)
        {
            throw new NotImplementedException();
        }

        public Result AttchTreeByIp(string ip)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupTreeModel> GroupTreeModels { get { return groupTMList; } }
        public IEnumerable<PDATreeModel> PDATreeModels { get { return pdaTMList; } }

      
    }
}

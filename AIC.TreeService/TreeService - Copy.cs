//using AIC.Core;
//using AIC.CoreType;
//using AIC.Database;
//using AIC.Domain;
//using AIC.OnlineSystem.Client;
//using AIC.OnlineSystem.Server.DB.Models.Generated.Master;
//using AIC.Server.Storage.Contract;
//using AIC.ServiceInterface;
//using Prism.Events;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Text.RegularExpressions;
//using MoreLinq;


//namespace AIC.TreeService
//{
//    public class TreeService : ITreeService
//    {
//        private string NULLGLOBALID = BitConverter.ToString(SHA1Managed.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Empty))).Replace("-", string.Empty);
//        private List<GroupTreeModel> groupTMList;
//        private List<PDATreeModel> pdaTMList;
   
//        private readonly IEventAggregator _eventAggregator;
//        private readonly IPDAService _pdaService;
//        private readonly IUnitOfWork _unitOfWork;

//        private IDictionary<Guid, ChannelTreePair> channelDict;
//        private IDictionary<ChannelIdentity, TestPointTreeModel> tpDict;
//        private IDictionary<Guid, TestPointModel> testPointModels;

//        public TreeService(IEventAggregator eventAggregator,IPDAService pdaService,IUnitOfWork unitOfWork)
//        {
//            _eventAggregator = eventAggregator;
//            _pdaService = pdaService;
//            _unitOfWork = unitOfWork;

//            groupTMList = new List<GroupTreeModel>();
//            tpDict = new Dictionary<ChannelIdentity, TestPointTreeModel>();
//            channelDict = new Dictionary<Guid, ChannelTreePair>();
//            pdaTMList = new List<PDATreeModel>();
//            testPointModels = new Dictionary<Guid,TestPointModel>();
//        }

//        public Result Result { get; private set;}

//        public void Initialize()
//        {
//            try
//            {
//                BuildTestPointModels();
//                BuildPDATrees();
//                BuildGroupTrees();
//            }
//            catch (Exception ex)
//            {
//                Result = Result.Combine(Result, Result.Fail(ex.ToString()));
//            }
//        }

//        private void BuildTestPointModels()
//        {
//            _unitOfWork.TestPoints.Query()
//                .Select(o => ConverterToTestPointModel(o))
//                .ForEach(o =>
//                {
//                    testPointModels.Add(o.TestPointId, o);
//                });
//        }

//        public void Save()
//        {
//            var testPoints = groupTMList.SelectMany(o => o.Children).SelectMany(o => o.Children).SelectMany(o => o.Children).SelectMany(o => o.Children).OfType<TestPointTreeModel>();

//            testPoints.Where(o => !testPointModels.ContainsKey(o.TestPointId))
//                      .Select(o =>
//                      {
//                          var tm = CreateFromTreeModel(o);
//                          testPointModels.Add(o.TestPointId, tm);
//                          return tm;
//                      })
//                      .Select(o => ConverterToTestPoint(o))
//                      .ForEach(o => _unitOfWork.TestPoints.Add(o));

//            testPoints.Where(o => !CheckChange(o, testPointModels[o.TestPointId]))
//                      .Select(o =>
//                      {
//                          AssignChange(o, testPointModels[o.TestPointId]);
//                          return testPointModels[o.TestPointId];
//                      })
//                      .Select(o => ConverterToTestPoint(o))
//                      .ForEach(o => _unitOfWork.TestPoints.Update(o.id, o));
//        }

//        private void AssignChange(TestPointTreeModel tp, TestPointModel model)
//        {
//            model.GroupCOName = tp.Location[0].Value;
//            model.CorporationName = tp.Location[1].Value;
//            model.WorkShopName = tp.Location[2].Value;
//            model.T_Device_Name = tp.Location[3].Value;
//            model.DevSN = tp.Location[4].Value;
//            model.Name = tp.Location[5].Value;
//            model.MSSN = tp.Location[6].Value;
//        }

//        private bool CheckChange(TestPointTreeModel treeModel, TestPointModel model)
//        {
//            var location = treeModel.Location;
//            bool result1 = location[0].Value == model.GroupCOName
//                && location[1].Value == model.CorporationName
//                && location[2].Value == model.WorkShopName
//                && location[3].Value == model.T_Device_Name
//                && location[4].Value == model.DevSN
//                && location[5].Value == model.Name
//                && location[6].Value == model.MSSN;

//            if (treeModel.ChannelId.HasValue)
//            {
//                bool result2 = treeModel.ChannelId.Value.IP == model.IP
//                    && treeModel.ChannelId.Value.CardNum == model.CardNum
//                    && treeModel.ChannelId.Value.ChannelNum == model.ChannelNum;
//                return result1 && result2;
//            }
//            return result1;
//        }

//        private TestPointModel CreateFromTreeModel(TestPointTreeModel treeModel)
//        {
//            var tm = new TestPointModel(new TestPoint())
//            {
//                TestPointId = treeModel.TestPointId,
//                GroupCOName = treeModel.Location[0].Value,
//                CorporationName = treeModel.Location[1].Value,
//                WorkShopName = treeModel.Location[2].Value,
//                T_Device_Name = treeModel.Location[3].Value,
//                DevSN = treeModel.Location[4].Value,
//                Name = treeModel.Location[5].Value,
//                MSSN = treeModel.Location[6].Value,
//            };

//            if (treeModel.ChannelId.HasValue)
//            {
//                tm.IP = treeModel.ChannelId.Value.IP;
//                tm.CardNum = treeModel.ChannelId.Value.CardNum;
//                tm.ChannelNum = treeModel.ChannelId.Value.ChannelNum;
//            }

//            return tm;
//        }

//        private TestPointModel ConverterToTestPointModel(TestPoint tp)
//        {
//            return ObjectConvertor<TestPoint, TestPointModel>.Convert(tp);
//        }

//        private TestPoint ConverterToTestPoint(TestPointModel tpModel)
//        {
//            return ObjectConvertor<TestPointModel, TestPoint>.Convert(tpModel);
//        }

//        private void BuildPDATrees()
//        {
//            var pdas = _pdaService.GetPDAs();
//            foreach(var pda in pdas)
//            {
//                var pdaTM = new PDATreeModel(pda.IP);
//                foreach (var card in pda.Cards)
//                {
//                    var cardTM = new CardTreeModel(card.CardId.IP, card.CardId.CardNum);
//                    pdaTM.AddChild(cardTM);
//                    foreach (var channel in card.Channels)
//                    {
//                        var channelTM = new ChannelTreeModel(ChannelIdentity.Create(channel.ChannelId.IP, channel.ChannelId.CardNum, channel.ChannelId.ChannelNum).Value);
//                        cardTM.AddChild(channelTM);
//                    }
//                }
//                pdaTMList.Add(pdaTM);
//            }
//        }

//        private void BuildGroupTrees()
//        {
//            var allChannels = pdaTMList.SelectMany(o => o.Children).SelectMany(o => o.Children).OfType<ChannelTreeModel>().ToArray();

//            var groupTMs = testPointModels.Values.GroupBy(o => o.GroupCOName, o => o);
//            foreach (var g in groupTMs)
//            {
//                GroupTreeModel group = groupTMList.Where(o => o.Name.Value == g.Key).SingleOrDefault();
//                if (group == null)
//                {
//                    group = new GroupTreeModel(g.Key);
//                    groupTMList.Add(group);
//                }

//                var corporations = g.GroupBy(o => o.CorporationName, o => o);
//                foreach (var corp in corporations)
//                {
//                    CorporationTreeModel corporation = group.Children.Where(o => o.Name.Value == corp.Key).SingleOrDefault() as CorporationTreeModel;
//                    if (corporation == null)
//                    {
//                        corporation = new CorporationTreeModel(corp.Key);
//                        group.AddChild(corporation);
//                    }
//                    var worpshops = corp.GroupBy(o => o.WorkShopName, o => o);
//                    foreach (var ws in worpshops)
//                    {
//                        WorkShopTreeModel workshop = corporation.Children.Where(o => o.Name.Value == ws.Key).SingleOrDefault() as WorkShopTreeModel;
//                        if (workshop == null)
//                        {
//                            workshop = new WorkShopTreeModel(ws.Key);
//                            corporation.AddChild(workshop);
//                        }
//                        var equips = ws.OrderBy(o => OrderFunc(o)).OrderBy(o => o.T_Device_Name).GroupBy(o => o.T_Device_Name + "|" + o.DevSN, o => o);
//                        foreach (var equip in equips)
//                        {
//                            EquipmentTreeModel equipment = workshop.Children.Where(o => o.Name.Value == equip.Key.Split('|')[0] && ((EquipmentTreeModel)o).MSSN.Value == equip.Key.Split('|')[1]).SingleOrDefault() as EquipmentTreeModel;
//                            if (equipment == null)
//                            {
//                                equipment = new EquipmentTreeModel(equip.Key.Split('|')[0], equip.Key.Split('|')[1]);
//                                workshop.AddChild(equipment);
//                            }
//                            foreach(var model in equip)
//                            {
//                                TestPointTreeModel testPoint = new TestPointTreeModel(model.Name, model.MSSN, model.TestPointId);
//                                equipment.AddChild(testPoint);

//                                if (!channelDict.ContainsKey(testPoint.TestPointId))
//                                {
//                                    var channelPair = new ChannelTreePair()
//                                    {
//                                        Id = testPoint.TestPointId,
//                                        TestPointTM = testPoint
//                                    };

//                                    if (!string.IsNullOrEmpty(model.IP) && !string.IsNullOrEmpty(model.CardNum) && !string.IsNullOrEmpty(model.ChannelNum))
//                                    {

//                                        var channelId = ChannelIdentity.Create(model.IP, model.CardNum, model.ChannelNum).Value;
//                                        var channel = allChannels.Where(o => o.ChannelId == channelId).SingleOrDefault();
//                                        if (channel != null)
//                                        {
//                                            channel.Bind(model.TestPointId);
//                                            channelPair.ChannelTM = channel;
//                                            testPoint.Bind(channelId);
//                                        }
//                                    }
//                                    channelDict.Add(testPoint.TestPointId, channelPair);
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//        }   

//        private int OrderFunc(TestPointModel contract)
//        {
//            int x = 0;
//            Int32.TryParse(Regex.Match(contract.Name, @"\d+").Value, out x);
//            return x;
//        }

//        public IEnumerable<GroupTreeModel> GetAllGroups()
//        {
//            return groupTMList;
//        }

//        public IEnumerable<PDATreeModel> GetAllPDAs()
//        {
//            return pdaTMList;
//        }

//        public Result<TreeViewItemModel> AddTreeModel(TreeViewItemModel arg)
//        {
//            try
//            {
//                TreeViewItemModel treeModel = null;
//                if (arg == null)
//                {
//                    int max = 0;
//                    if (groupTMList.Count > 0)
//                    {
//                        string[] indexString = groupTMList.Select(o => o.Name.Value.Split('#').LastOrDefault()).ToArray();
//                        int[] indexs = new int[indexString.Length];
//                        for (int i = 0; i < indexs.Length; i++)
//                        {
//                            int.TryParse(indexString[i], out indexs[i]);
//                        }
//                        max = indexs.Max();
//                    }

//                    string groupName = string.Format("总厂#{0}", max + 1);
//                    GroupTreeModel group = new GroupTreeModel(groupName);
//                    CorporationTreeModel corp = new CorporationTreeModel("分厂#1");
//                    group.AddChild(corp);
//                    WorkShopTreeModel workshop = new WorkShopTreeModel("车间#1");
//                    corp.AddChild(workshop);
//                    EquipmentTreeModel equipemnt = new EquipmentTreeModel("设备", "#1");
//                    workshop.AddChild(equipemnt);
//                    TestPointTreeModel testPoint = new TestPointTreeModel("测点", "#1");
//                    equipemnt.AddChild(testPoint);
//                    groupTMList.Add(group);
//                    treeModel = group;

//                    if(!channelDict.ContainsKey(testPoint.TestPointId))
//                    {
//                        var channelPair = new ChannelTreePair()
//                        {
//                            Id = testPoint.TestPointId,
//                            TestPointTM= testPoint
//                        };
//                        channelDict.Add(testPoint.TestPointId, channelPair);
//                    }
//                }
//                else if (arg is GroupTreeModel)
//                {
//                    GroupTreeModel group = arg as GroupTreeModel;
//                    int max = 0;
//                    if (group.Count > 0)
//                    {
//                        string[] indexString = group.Children.Select(o => o.Name.Value.Split('#').LastOrDefault()).ToArray();
//                        int[] indexs = new int[indexString.Length];
//                        for (int i = 0; i < indexs.Length; i++)
//                        {
//                            int.TryParse(indexString[i], out indexs[i]);
//                        }
//                        max = indexs.Max();
//                    }
//                    string corporationName = string.Format("分厂#{0}", max + 1);
//                    CorporationTreeModel corp = new CorporationTreeModel(corporationName);
//                    group.AddChild(corp);
//                    WorkShopTreeModel workshop = new WorkShopTreeModel("车间#1");
//                    corp.AddChild(workshop);
//                    EquipmentTreeModel equipemnt = new EquipmentTreeModel("设备", "#1");
//                    workshop.AddChild(equipemnt);
//                    TestPointTreeModel testPoint = new TestPointTreeModel("测点", "#1");
//                    equipemnt.AddChild(testPoint);
//                    treeModel = corp;

//                    if (!channelDict.ContainsKey(testPoint.TestPointId))
//                    {
//                        var channelPair = new ChannelTreePair()
//                        {
//                            Id = testPoint.TestPointId,
//                            TestPointTM = testPoint
//                        };
//                        channelDict.Add(testPoint.TestPointId, channelPair);
//                    }
//                }
//                else if (arg is CorporationTreeModel)
//                {
//                    CorporationTreeModel corp = arg as CorporationTreeModel;
//                    int max = 0;
//                    if (corp.Count > 0)
//                    {
//                        string[] indexString = corp.Children.Select(o => o.Name.Value.Split('#').LastOrDefault()).ToArray();
//                        int[] indexs = new int[indexString.Length];
//                        for (int i = 0; i < indexs.Length; i++)
//                        {
//                            int.TryParse(indexString[i], out indexs[i]);
//                        }
//                        max = indexs.Max();
//                    }
//                    string workshopName = string.Format("车间#{0}", max + 1);
//                    WorkShopTreeModel workshop = new WorkShopTreeModel(workshopName);
//                    corp.AddChild(workshop);
//                    EquipmentTreeModel equipemnt = new EquipmentTreeModel("设备", "#1");
//                    workshop.AddChild(equipemnt);
//                    TestPointTreeModel testPoint = new TestPointTreeModel("测点", "#1");
//                    equipemnt.AddChild(testPoint);
//                    treeModel = workshop;

//                    if (!channelDict.ContainsKey(testPoint.TestPointId))
//                    {
//                        var channelPair = new ChannelTreePair()
//                        {
//                            Id = testPoint.TestPointId,
//                            TestPointTM = testPoint
//                        };
//                        channelDict.Add(testPoint.TestPointId, channelPair);
//                    }
//                }
//                else if (arg is WorkShopTreeModel)
//                {
//                    WorkShopTreeModel workshop = arg as WorkShopTreeModel;
//                    int max = 0;
//                    if (workshop.Count > 0)
//                    {
//                        string[] indexString = workshop.Children.Select(o => ((EquipmentTreeModel)o).MSSN.Value.Split('#').LastOrDefault()).ToArray();
//                        int[] indexs = new int[indexString.Length];
//                        for (int i = 0; i < indexs.Length; i++)
//                        {
//                            int.TryParse(indexString[i], out indexs[i]);
//                        }
//                        max = indexs.Max();
//                    }
//                    CorporationTreeModel corp = workshop.Parent as CorporationTreeModel;
//                    GroupTreeModel group = corp.Parent as GroupTreeModel;
//                    string equipmentMssn = string.Format("#{0}", max + 1);
//                    EquipmentTreeModel equipemnt = new EquipmentTreeModel("设备", equipmentMssn);
//                    workshop.AddChild(equipemnt);
//                    TestPointTreeModel testPoint = new TestPointTreeModel("测点", "#1");
//                    equipemnt.AddChild(testPoint);
//                    treeModel = equipemnt;

//                    if (!channelDict.ContainsKey(testPoint.TestPointId))
//                    {
//                        var channelPair = new ChannelTreePair()
//                        {
//                            Id = testPoint.TestPointId,
//                            TestPointTM = testPoint
//                        };
//                        channelDict.Add(testPoint.TestPointId, channelPair);
//                    }
//                }
//                else if (arg is EquipmentTreeModel)
//                {
//                    EquipmentTreeModel equipemnt = arg as EquipmentTreeModel;
//                    int max = 0;
//                    if (equipemnt.Count > 0)
//                    {
//                        string[] indexString = equipemnt.Children.Select(o => ((TestPointTreeModel)o).MSSN.Value.Split('#').LastOrDefault()).ToArray();
//                        int[] indexs = new int[indexString.Length];
//                        for (int i = 0; i < indexs.Length; i++)
//                        {
//                            int.TryParse(indexString[i], out indexs[i]);
//                        }
//                        max = indexs.Max();
//                    }
//                    string tpMssn = string.Format("#{0}", max + 1);

//                    TestPointTreeModel testPoint = new TestPointTreeModel("测点", tpMssn);
//                    equipemnt.AddChild(testPoint);
//                    treeModel = testPoint;

//                    if (!channelDict.ContainsKey(testPoint.TestPointId))
//                    {
//                        var channelPair = new ChannelTreePair()
//                        {
//                            Id = testPoint.TestPointId,
//                            TestPointTM = testPoint
//                        };
//                        channelDict.Add(testPoint.TestPointId, channelPair);
//                    }
//                }
//                else if (arg is TestPointTreeModel)
//                {
//                    TestPointTreeModel testPoint = arg as TestPointTreeModel;
//                    if (testPoint.SignalType == SignalType.Vibration && testPoint.IsPaired)
//                    {
//                        int max = 0;
//                        if (testPoint.Count > 0)
//                        {
//                            string[] indexString = testPoint.Children.Select(o => o.Name.Value.Split('#').LastOrDefault()).ToArray();
//                            int[] indexs = new int[indexString.Length];
//                            for (int i = 0; i < indexs.Length; i++)
//                            {
//                                int.TryParse(indexString[i], out indexs[i]);
//                            }
//                            max = indexs.Max();
//                        }
//                        string divFreDescription = string.Format("分频#{0}", max + 1);
//                        DivFreTreeModel divFreTM = new DivFreTreeModel(divFreDescription);
//                        testPoint.AddChild(divFreTM);
//                        treeModel = divFreTM;

//                    }
//                }
//                return Result.Ok(treeModel);
//            }
//            catch (Exception ex)
//            {
//                return Result.Fail<TreeViewItemModel>(ex.ToString());
//            }
//        }

//        public Result DeleteTreeModel(TreeViewItemModel model)
//        {
//            List<TestPointTreeModel> bindedTestPoints = new List<TestPointTreeModel>();
//            if (model is GroupTreeModel)
//            {
//                bindedTestPoints = ((GroupTreeModel)model).Children.SelectMany(o => o.Children).SelectMany(o => o.Children).SelectMany(o => o.Children).OfType<TestPointTreeModel>().ToList();
//            }
//            else if (model is CorporationTreeModel)
//            {
//                bindedTestPoints = ((CorporationTreeModel)model).Children.SelectMany(o => o.Children).SelectMany(o => o.Children).OfType<TestPointTreeModel>().ToList();
//            }
//            else if (model is WorkShopTreeModel)
//            {
//                bindedTestPoints = ((WorkShopTreeModel)model).Children.SelectMany(o => o.Children).OfType<TestPointTreeModel>().ToList();
//            }
//            else if (model is EquipmentTreeModel)
//            {
//                bindedTestPoints = ((EquipmentTreeModel)model).Children.OfType<TestPointTreeModel>().ToList();
//            }
//            else if (model is TestPointTreeModel)
//            {
//                bindedTestPoints.Add(model as TestPointTreeModel);
//            }
//            else if (model is DivFreTreeModel)
//            {
//                var div = model as DivFreTreeModel;
//                var tp = div.Parent as TestPointTreeModel;
//                if (tp.IsPaired)
//                {
                  
//                }
//            }

//            //if (bindedTestPoints.Any(o => o.IsPaired))
//            //{
//            //    return Result.Fail("包含有已绑定的测点，请先解除绑定");
//            //}

//            bindedTestPoints.Where(o => channelDict.ContainsKey(o.TestPointId))
//                .ForEach(o =>
//                {
//                    o.UnBind();
//                    var channelTreePair = channelDict[o.TestPointId];
//                    if (channelTreePair.ChannelTM != null)
//                    {
//                        channelTreePair.ChannelTM.UnBind();
//                    }
//                    channelDict.Remove(o.TestPointId);
//                });

//            bindedTestPoints.Where(o=>testPointModels.ContainsKey(o.TestPointId))
//                .Select(o=> testPointModels[o.TestPointId].id)
//                .ForEach(o => _unitOfWork.TestPoints.Delete(o));


//            if (model is GroupTreeModel)
//            {
//                groupTMList.Remove(model as GroupTreeModel);
//            }
//            else
//            {
//                var parent = model.Parent;
//                if (parent != null)
//                {
//                    parent.RemoveChild(model);
//                    if (parent.Count == 0)
//                    {
//                        parent.Alarm = AlarmGrade.HighNormal;
//                    }
//                }
//            }
//            return Result.Ok();
//        }

//        public Result<ChannelTreeModel> GetChannel(Guid id)
//        {
//            if (channelDict.ContainsKey(id))
//            {
//                return Result.Ok(channelDict[id].ChannelTM);
//            }
//            return Result.Fail<ChannelTreeModel>("通道不存在");
//        }

//        public Result<TestPointTreeModel> GetTestPoint(Guid id)
//        { 
//            if (channelDict.ContainsKey(id))
//            {
//                return Result.Ok(channelDict[id].TestPointTM);
//            }
//            return Result.Fail<TestPointTreeModel>("测点不存在");
//        }

//        public Result BindChannel(Guid id, ChannelTreeModel channel)
//        {
//            if (channelDict.ContainsKey(id))
//            {
//                channelDict[id].ChannelTM = channel;
//                channel.Bind(id);
//                channelDict[id].TestPointTM.Bind(channel.ChannelId);
//            }

//            if (testPointModels.ContainsKey(id))
//            {
//                var tpModel = testPointModels[id];
//                tpModel.IP = channel.ChannelId.IP;
//                tpModel.CardNum = channel.ChannelId.CardNum;
//                tpModel.ChannelNum = channel.ChannelId.ChannelNum;
//                _unitOfWork.TestPoints.Update(tpModel.id, ConverterToTestPoint(tpModel));
//            }
//            return Result.Ok();
//           // return Result.Fail<TestPointTreeModel>("通道Id不存在");
//        }

//        public Result UnBindChannel(Guid id)
//        {
//            if (channelDict.ContainsKey(id))
//            {
//                channelDict[id].ChannelTM.UnBind();
//                channelDict[id].TestPointTM.UnBind();
//                channelDict[id].ChannelTM = null;
//            }

//            if (testPointModels.ContainsKey(id))
//            {
//                var tpModel = testPointModels[id];
//                tpModel.IP = string.Empty;
//                tpModel.CardNum = "-1";
//                tpModel.ChannelNum = "-1";
//                _unitOfWork.TestPoints.Update(tpModel.id, ConverterToTestPoint(tpModel));
//            }
//            return Result.Ok();
//            //return Result.Fail<TestPointTreeModel>("通道Id不存在");
//        }

//        public Result DeleteDataCollector(PDATreeModel dataCollector)
//        {
//            throw new NotImplementedException();
//        }

//        public Result AttchTreeByIp(string ip)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<GroupTreeModel> GroupTreeModels { get { return groupTMList; } }
//        public IEnumerable<PDATreeModel> PDATreeModels { get { return pdaTMList; } }

      
//    }
//}

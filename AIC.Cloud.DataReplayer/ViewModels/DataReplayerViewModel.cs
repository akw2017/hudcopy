using AIC.Cloud.Applications;
using AIC.Cloud.Applications.Events;
using AIC.Cloud.Applications.Services;
using AIC.Cloud.Database;
using AIC.Cloud.Domain;
using AIC.Cloud.Presentation;
using AIC.CoreType;
using AIC.Server.Common;
using AIC.Server.Storage.Contract;
using AIC.Server.Video.Contract;
using AIC.Server.Video.Interface;
using AICMath;
using Arction.WPF.LightningChartUltimate;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AIC.Cloud.DataReplayer.ViewModels
{
    [Export(typeof(DataReplayerViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataReplayerViewModel : BindableBase, IAsyncInitialization
    {
        #region Field
        private SynchronizationContext uiContext = SynchronizationContext.Current;

        private readonly string _listenAddress = Convert.ToString(ConfigurationManager.AppSettings["LISTENADDRESS"]);
        private readonly int _listenPort = Convert.ToInt32(ConfigurationManager.AppSettings["LISTENPORT"]);

        private ObservableCollection<LMVideoTableContract> videoChannelCollection = new ObservableCollection<LMVideoTableContract>();
        private ObservableCollection<ChannelDistributionContract> selectedChannels = new ObservableCollection<ChannelDistributionContract>();
        private ObservableCollection<VideoInfo> vedioInfoCollection = new ObservableCollection<VideoInfo>();
        private ObservableCollection<ChannelToken> addedChannels = new ObservableCollection<ChannelToken>();
        private ObservableCollection<HistoricalDataViewModel> historicalDataCollection = new ObservableCollection<HistoricalDataViewModel>();
        private ObservableCollection<GroupTreeModel> groupCollection = new ObservableCollection<GroupTreeModel>();

        private AMSReplayDataViewModel amsReplayVM;
        private TimeDomainDataViewModel timeDomainVM;
        private FrequencyDomainDataViewModel frequencyDomainVM;
        private PowerSpectrumDataViewModel powerSpectrumVM;
        private PowerSpectrumDensityDataViewModel powerSpectrumDensityVM;
        private OrthoDataViewModel orthoDataVM;
        private OffDesignConditionDataViewModel offDesignConditionVM;
        private OrderAnalysisDataViewModel orderAnalysisVM;
        private Time3DSpectrumDataViewModel time3DSpectrumVM;
        private RPM3DSpectrumDataViewModel rpm3DSpectrumVM;
        private AlarmPointTrendDataViewModel alarmPointTrendVM;

        private FastObservableCollection<AMSObject> vInfoCollection = new FastObservableCollection<AMSObject>();
        private FastObservableCollection<AMSObject> anInfoCollection = new FastObservableCollection<AMSObject>();
        private FastObservableCollection<DivFreObject> divFreCollection = new FastObservableCollection<DivFreObject>();

        private DatabaseComponent database;
        private Func<IEnumerable<VibrationChannelToken>, Task> trackTask { get; set; }
        private bool isTrackRunning;

        private DataModelProvider.DataModelProvider _dataModelProvider;
        private readonly IEventAggregator _eventAggregator;

        private int channelRecordLength = 50000;
        private bool allowNormal = true;
        private bool allowWarning = true;
        private bool allowDanger = true;
        private int fftLength = 6;
        private double? peakValue;
        private double? peakPeakValue;
        private DateTime? videoStartTime = DateTime.Now.Subtract(TimeSpan.FromHours(1));
        private DateTime? videoEndTime = DateTime.Now;
        private DateTime? startTime = DateTime.Now.Subtract(TimeSpan.FromHours(1));
        private DateTime? endTime = DateTime.Now;
        private DateTime? lowerTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(59));
        private DateTime? upperTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(1));
        private ChannelToken selectedChannel;
        private AIC.CoreType.Unit unitFilter = AIC.CoreType.Unit.Velocity;
        private AIC.CoreType.TriggerType triggerFilter = AIC.CoreType.TriggerType.Auto;
        private string listenAddress;
        private int listenPort;
        private LMVideoTableContract selectedVideoChannel;
        private VideoInfo selectedVideo;
        private double itemWidth = 500;
        private double itemHeight = 300;
        private double downRPMFilter;
        private double upRPMFilter;
        private bool allowRPMFilter;
        private string dataViewMode = "Chart";
        private string viewMode = "Data";
        private bool showAlarmPointTrend;
        private bool showRPM3DSpectrum;
        private bool showTime3DSpectrum;
        private bool showOrderAnalysis;
        private bool showOffCondition;
        private bool showOrtho;
        private bool showFrequencyDomain;
        private bool showPowerSpectrum;
        private bool showPowerSpectrumDensity;
        private bool showTimeDomain;
        private bool showAMS;
        private bool isComposing;

        #endregion Field

        [ImportingConstructor]
        public DataReplayerViewModel(IEventAggregator eventAggregator, DataModelProvider.DataModelProvider dataModelProvider)
        {
            this._eventAggregator = eventAggregator;
            this._dataModelProvider = dataModelProvider;
            listenAddress = _listenAddress;
            listenPort = _listenPort;   

            RemoveChannelsCommand = new DelegateCommand<object>(RemoveChannels, CanRemoveChannels);
            DiagnoseCommand = new DelegateCommand<object>(Diagnose, CanDiagnose);
            VideoQueryCommand = new DelegateCommand<object>(VideoQuery, CanVideoQuery);
            ViewModeSwitchCommand = new DelegateCommand<string>(ViewModeSwitch);
            DataViewModeSwitchCommand = new DelegateCommand<string>(DataViewModeSwitch);
            ClearListCommand = new DelegateCommand<string>(ClearList);
            RefreshCommand = AsyncCommand.Create(() => Refresh());
            LoadingDataCommand = AsyncCommand.Create((t, p) => LoadingData(p));
            LoadingEquipmentDataCommand = AsyncCommand.Create((t, p) => LoadingEquipmentData(p));
            LoadingWorkshoptDataCommand = AsyncCommand.Create((t, p) => LoadingWorkshoptData(p));
            LoadDivFreCommand = AsyncCommand.Create((t, p) => LoadDivFre(p));
            RefreshChannelDataCommand = AsyncCommand.Create(RefreshChannelData);

            Initialization = InitializeAsync();   
        }

        public Task Initialization { get; private set; }
        public async Task InitializeAsync()
        {
            try
            {
                amsReplayVM = new AMSReplayDataViewModel();
                amsReplayVM.Title = "通频";
                amsReplayVM.ItemWidth = ItemWidth;
                amsReplayVM.ItemHeight = ItemHeight;
               // amsReplayVM.SearchAdvancedCommand = AsyncCommand.Create(() => SearchAdvanced());

                timeDomainVM = new TimeDomainDataViewModel();
                timeDomainVM.Title = "时域";
                timeDomainVM.ItemWidth = ItemWidth;
                timeDomainVM.ItemHeight = ItemHeight;

                frequencyDomainVM = new FrequencyDomainDataViewModel();
                frequencyDomainVM.Title = "频域";
                frequencyDomainVM.ItemWidth = ItemWidth;
                frequencyDomainVM.ItemHeight = ItemHeight;

                powerSpectrumVM = new PowerSpectrumDataViewModel();
                powerSpectrumVM.Title = "功率谱";
                powerSpectrumVM.ItemWidth = ItemWidth;
                powerSpectrumVM.ItemHeight = ItemHeight;

                powerSpectrumDensityVM = new PowerSpectrumDensityDataViewModel();
                powerSpectrumDensityVM.Title = "功率谱密度";
                powerSpectrumDensityVM.ItemWidth = ItemWidth;
                powerSpectrumDensityVM.ItemHeight = ItemHeight;

                orthoDataVM = new OrthoDataViewModel();
                orthoDataVM.Title = "轴心轨迹";
                orthoDataVM.ItemWidth = ItemWidth;
                orthoDataVM.ItemHeight = ItemHeight;

                offDesignConditionVM = new OffDesignConditionDataViewModel(_dataModelProvider);
                offDesignConditionVM.Title = "变工况拟合";
                offDesignConditionVM.ItemWidth = ItemWidth;
                offDesignConditionVM.ItemHeight = ItemHeight;

                alarmPointTrendVM = new AlarmPointTrendDataViewModel();
                alarmPointTrendVM.Title = "报警点趋势";
                alarmPointTrendVM.ItemWidth = ItemWidth;
                alarmPointTrendVM.ItemHeight = ItemHeight;

                orderAnalysisVM = new OrderAnalysisDataViewModel();
                orderAnalysisVM.Title = "阶次分析";
                orderAnalysisVM.ItemWidth = ItemWidth;
                orderAnalysisVM.ItemHeight = ItemHeight;

                time3DSpectrumVM = new Time3DSpectrumDataViewModel();
                time3DSpectrumVM.Title = "时间三维谱";
                time3DSpectrumVM.ItemWidth = ItemWidth;
                time3DSpectrumVM.ItemHeight = ItemHeight;

                rpm3DSpectrumVM = new RPM3DSpectrumDataViewModel();
                rpm3DSpectrumVM.Title = "转速三维谱";
                rpm3DSpectrumVM.ItemWidth = ItemWidth;
                rpm3DSpectrumVM.ItemHeight = ItemHeight;

                ShowAMS = true;
                ShowAlarmPointTrend = true;
                ShowTimeDomain = true;
                ShowFrequencyDomain = true;
                ShowOrtho = false;
                ShowOffCondition = false;
                ShowOrderAnalysis = false;
                ShowTime3DSpectrum = false;
                ShowRPM3DSpectrum = false;

                trackTask = AMSTrackChanged;

                amsReplayVM.WhenTrackChanged.Sample(TimeSpan.FromMilliseconds(100)).ObserveOn(uiContext).Subscribe(RaiseTrackChanged);

                historicalDataCollection.Add(amsReplayVM);
                historicalDataCollection.Add(alarmPointTrendVM);
                historicalDataCollection.Add(timeDomainVM);
                historicalDataCollection.Add(frequencyDomainVM);
                historicalDataCollection.Add(powerSpectrumVM);
                historicalDataCollection.Add(powerSpectrumDensityVM);
                historicalDataCollection.Add(orthoDataVM);
                historicalDataCollection.Add(offDesignConditionVM);
                historicalDataCollection.Add(orderAnalysisVM);
                historicalDataCollection.Add(time3DSpectrumVM);
                historicalDataCollection.Add(rpm3DSpectrumVM);

                database = await DatabaseComponent.Instance;
                if (database != null)
                {
                    var groups = CreateTree();
                    foreach (var item in groups)
                    {
                        groupCollection.Add(item);
                    }
                 
                    foreach (var contract in database.LMVideos)
                    {
                        if (!string.IsNullOrEmpty(contract.GroupCOName)
                            && !string.IsNullOrEmpty(contract.CorporationName)
                            && !string.IsNullOrEmpty(contract.WorkShopName)
                            && !string.IsNullOrEmpty(contract.DevName)
                            && !string.IsNullOrEmpty(contract.DevSN)
                            && (!string.IsNullOrEmpty(contract.Name) || !string.IsNullOrEmpty(contract.MSSN)))
                        {
                            videoChannelCollection.Add(contract);
                        }
                    }
                    SelectedVideoChannel = videoChannelCollection.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-初始化异常", e));
            }
        }
        private IEnumerable<GroupTreeModel> CreateTree()
        {
            List<GroupTreeModel> groupList = new List<GroupTreeModel>();   
            var groups = database.ChannelContracts.GroupBy(o => o.GroupCOName, o => o);
            foreach (var g in groups)
            {
                GroupTreeModel group = new GroupTreeModel(g.Key);
                groupList.Add(group);
                var corporations = g.GroupBy(o => o.CorporationName, o => o);
                foreach (var corp in corporations)
                {
                    CorporationTreeModel corporation = new CorporationTreeModel(corp.Key);
                    group.AddChild(corporation);
                    var worpshops = corp.GroupBy(o => o.WorkShopName, o => o);
                    foreach (var ws in worpshops)
                    {
                        WorkShopTreeModel workshop = new WorkShopTreeModel(ws.Key);
                        corporation.AddChild(workshop);
                        var equips = ws.OrderBy(o => OrderFunc(o)).OrderBy(o => o.DevName).GroupBy(o => o.DevName + "|" + o.DevSN, o => o);
                        foreach (var equip in equips)
                        {
                            EquipmentTreeModel equipment = new EquipmentTreeModel(equip.Key.Split('|')[0], equip.Key.Split('|')[1]);
                            workshop.AddChild(equipment);
                            var testPoints = equip.GroupBy(o => o.Name + "|" + o.MSSN, o => o);
                            foreach (var tp in testPoints)
                            {
                                var channel = tp.Single();
                                TestPointTreeModel testPoint = new TestPointTreeModel(tp.Key.Split('|')[0], tp.Key.Split('|')[1],channel.ChannelID);
                                testPoint.SignalType = (SignalType)Enum.Parse(typeof(SignalType), channel.SignalType.ToString());
                                equipment.AddChild(testPoint);
                                var divFres = database.DivFreContracts.Where(o => o.ChannelID == testPoint.ChannelID);
                                if (divFres != null)
                                {
                                    foreach (var div in divFres)
                                    {
                                        DivFreTreeModel divTM = new DivFreTreeModel(div.FreDescription);
                                        testPoint.AddChild(divTM);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return groupList.ToArray();
        }
        private int OrderFunc(ChannelDistributionContract contract)
        {
            int x = 0;
            Int32.TryParse(Regex.Match(contract.DevSN, @"\d+").Value, out x);
            return x;
        }

        public bool MoveToNextVideo()
        {
            int index = vedioInfoCollection.IndexOf(SelectedVideo);
            if (index < vedioInfoCollection.Count - 1)
            {
                SelectedVideo = vedioInfoCollection[index + 1];
                if (SelectedVideo != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        #region AMSTrackChanged
        private async void RaiseTrackChanged(IEnumerable<VibrationChannelToken> tokens)
        {
            if (trackTask == null)
            {
                return;
            }

            if (isTrackRunning)
            {
                return;
            }

            try
            {
                // we're running it now
                isTrackRunning = true;
                await trackTask.Invoke(tokens);
            }
            catch (Exception)
            {
            }
            finally
            {
                // allow it to run again
                isTrackRunning = false;
            }
        }
        private async Task AMSTrackChanged(IEnumerable<VibrationChannelToken> tokens)
        {
            try
            {
                if (tokens == null) return;
                string ipAddress = ServerAddress.CTLAddress.Split('/')[2].Split(':')[0];
                var unValidTokens = tokens.Where(o => o.CurrentIndex == -1);
                foreach (var token in unValidTokens)
                {
                    token.VData = null;
                }

                var validTokens = tokens.Where(o => o.CurrentIndex != -1).ToArray();
                if (validTokens.Length == 0) return;

                var globalIndexes = validTokens.Select(o => o.DataContracts[o.CurrentIndex].ChannelGlobalIndex).ToArray();
                var ids = validTokens.Select(o => o.DataContracts[o.CurrentIndex].id).ToArray();
                var date = validTokens.Select(o => o.DataContracts[o.CurrentIndex].Date).First();

                var vInfoContracts = await Task.Run(() => database.QueryMutiVInfoByID(ipAddress, globalIndexes, ids, date, validTokens.Length));
                if (vInfoContracts.Length != validTokens.Length)
                {
                    throw new Exception(string.Format("给定通道数与返回通道数不一致，给定:{0},返回:{1}", validTokens.Length, vInfoContracts.Length));
                }
                for (int i = 0; i < validTokens.Length; i++)
                {
                    if (validTokens[i].Channel.ChannelGlobalIndex != vInfoContracts[i].ChannelGlobalIndex)
                        throw new Exception(string.Format("给定通道索引与返回通道索引一致，给定:{0},返回:{1}", validTokens[i].Channel.ChannelGlobalIndex, vInfoContracts[i].ChannelGlobalIndex));
                }

                await Task.Run(() => Parallel.For(0, vInfoContracts.Length, i =>
                  {
                      VibrationData vdata = new VibrationData();
                      vdata.AMS = vInfoContracts[i].VAMS;
                      vdata.SaveLab = vInfoContracts[i].SaveLab;
                      vdata.STIME = vInfoContracts[i].STIME;
                      vdata.RelatedChannelGlobalIndex = vInfoContracts[i].RelatedChannelGlobalIndex;
                      vdata.SampleFre = vInfoContracts[i].SampleFre;
                      vdata.SamplePoint = vInfoContracts[i].SamplePoint;
                      vdata.RPM = vInfoContracts[i].RPM ?? 0;
                      vdata.TeethNumber = vInfoContracts[i].TeethNumber ?? 0;
                      vdata.Trigger = (TriggerType)Enum.Parse(typeof(TriggerType), vInfoContracts[i].TriggerN.ToString());

                      switch (vInfoContracts[i].Unit)
                      {
                          case 0:
                              vdata.Unit = "m/s2";
                              break;
                          case 1:
                              vdata.Unit = "mm/s";
                              break;
                          case 2:
                              vdata.Unit = "um";
                              break;
                      }

                
                      vdata.Waveform = Algorithm.Instance.ByteToSingle(vInfoContracts[i].VData);
                      var paras = Algorithm.Instance.CalculatePara(vdata.Waveform);
                      if (paras != null)
                      {
                          vdata.PeakValue = paras[0];
                          vdata.PeakPeakValue = paras[1];
                          vdata.Slope = paras[2];
                          vdata.Kurtosis = paras[3];
                          vdata.KurtosisValue = paras[4];
                          vdata.WaveIndex = paras[5];
                          vdata.PeakIndex = paras[6];
                          vdata.ImpulsionIndex = paras[7];
                          vdata.RootAmplitude = paras[8];
                          vdata.ToleranceIndex = paras[9];
                      }
                      double sampleFre = vdata.SampleFre;
                      if (vdata.Trigger == TriggerType.Angle)
                      {
                          if (vdata.RPM > 0 && vdata.TeethNumber > 0)
                          {
                              sampleFre = vdata.RPM * vdata.TeethNumber / 60;
                          }
                      }

                      int length = (int)(vdata.SamplePoint / 2.56) + 1;
                      if (vdata.Frequency == null || vdata.Frequency.Length != length)
                      {
                          vdata.Frequency = new double[length];
                      }
                      double frequencyInterval = sampleFre / vdata.SamplePoint;
                      for (int j = 0; j < length; j++)
                      {
                          vdata.Frequency[j] = frequencyInterval * j;
                      }
                      var output = Algorithm.Instance.FFT2AndPhaseAction(vdata.Waveform, vdata.SamplePoint);
                      vdata.Amplitude = output[0].Take(length).ToArray();
                      vdata.Phase = output[1].Take(length).ToArray();
                      validTokens[i].VData = vdata;
                  }));

                if (ShowTimeDomain)
                {
                    timeDomainVM.ChangeChannelData(tokens);
                }
                if (ShowFrequencyDomain)
                {
                    frequencyDomainVM.ChangeChannelData(tokens);
                }
                if(ShowPowerSpectrum)
                {
                    powerSpectrumVM.ChangeChannelData(tokens);
                }
                if(ShowPowerSpectrumDensity)
                {
                    powerSpectrumDensityVM.ChangeChannelData(tokens);
                }
                if (ShowAlarmPointTrend)
                {
                    await alarmPointTrendVM.ChangeSnapshotData(tokens);
                }
                if (ShowOrtho)
                {
                    await orthoDataVM.ChangeOrthoData(tokens);
                }
                if (ShowTime3DSpectrum)
                {
                    //time3DSpectrumVM.Post(trackArgs.Tokens);
                }
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-TrackChanged", ex));
                //InteractionRequestService.Instance.InteractionRequest.Raise(new Confirmation() { Content = ex.ToString(), Title = "通道查询异常" }, confirm => { });
            }
        }

        private double[] VDataConvert(VInfoTableContract vInfo)
        {
            if (vInfo != null)
            {
                int length = vInfo.VData.Length / 4;
                var vdata = new double[length];
                for (int i = 0; i < length; i++)
                {
                    vdata[i] = BitConverter.ToSingle(vInfo.VData, i * 4);
                }
                return vdata;
            }
            return null;
        }

        #endregion AMSTrackChanged

        #region RefreshCommand
        public IAsyncCommand RefreshCommand { get; private set; }
        private async Task Refresh()
        {
            try
            {
                BusyIndicateService.Instance.Busy(true);
                groupCollection.Clear();
                await database.LoadChannelDistribution();

                var groups = CreateTree();
                foreach (var item in groups)
                {
                    groupCollection.Add(item);
                }
            }
            catch (Exception ex)
            {
                InteractionRequestService.Instance.InteractionRequest.Raise(new Confirmation() { Content = "刷新结构树失败", Title = "数据回放" }, confirm => { });
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-刷新结构树", ex));
            }
            finally
            {
                BusyIndicateService.Instance.Busy(false);
            }
        }

        #endregion RefreshCommand

        #region LoadDivFreCommand
        public IAsyncCommand LoadDivFreCommand { get; private set; }
        public async Task LoadDivFre(object args)
        {
            try
            {
                BusyIndicateService.Instance.Busy(true);
                if (args is DivFreTreeModel)
                {
                    var divFreTM = args as DivFreTreeModel;
                    var testPoint = divFreTM.Parent as TestPointTreeModel;
                    var dviFreContract = database.DivFreContracts.Where(o => o.ChannelID == testPoint.ChannelID && o.FreDescription == divFreTM.Name).SingleOrDefault();
                    if (dviFreContract == null) return;
                    var channelContract = database.ChannelContracts.Where(o => o.ChannelID == testPoint.ChannelID).SingleOrDefault();
                    if (channelContract == null) return;

                    if (DataViewMode == "Chart")
                    {
                        if (addedChannels.OfType<DivFreChannelToken>().Select(o => o.Channel).Contains(dviFreContract)) return;
                        var result = await LoadingDivFreDataAsync(dviFreContract.ChannelGlobalIndex, dviFreContract.DivFreGlobalIndex);
                        if (result.Length == 0) return;
                        DivFreChannelToken channel = new DivFreChannelToken()
                        {
                            GroupCOName = channelContract.GroupCOName,
                            CorporationName = channelContract.CorporationName,
                            WorkShopName = channelContract.WorkShopName,
                            DevName = channelContract.DevName,
                            DevSN = channelContract.DevSN,
                            Name = channelContract.Name,
                            MSSN = channelContract.MSSN,
                            Channel = dviFreContract,
                            DataContracts = result,
                        };

                        amsReplayVM.AddChannel(channel);
                        offDesignConditionVM.AddChannel(channel);
                        addedChannels.Add(channel);
                    }
                    else if (DataViewMode == "List")
                    {
                        var channel = database.ChannelContracts.Where(o => o.ChannelGlobalIndex == dviFreContract.ChannelGlobalIndex).SingleOrDefault();
                        if (channel == null)
                        {
                            Exception exception = new Exception("不存在与分频对应的振动通道");
                            exception.Data.Add("ChannelGlobalIndex", dviFreContract.ChannelGlobalIndex);
                            throw exception;
                        }
                        var result = await LoadingDivFreDataAsync(dviFreContract.ChannelGlobalIndex, dviFreContract.DivFreGlobalIndex);
                        if (result.Length == 0) return;
                        DivFreObject[] divFreObjects = new DivFreObject[result.Length];
                        for (int i = 0; i < result.Length; i++)
                        {
                            DivFreObject divFreObj = new DivFreObject();
                            divFreObj.GroupCOName = channel.GroupCOName;
                            divFreObj.CorporationName = channel.CorporationName;
                            divFreObj.WorkShopName = channel.WorkShopName;
                            divFreObj.DevName = channel.DevName;
                            divFreObj.DevSN = channel.DevSN;
                            divFreObj.CHName = channel.Name;
                            divFreObj.CHMSSN = channel.MSSN;
                            divFreObj.FreDescription = dviFreContract.FreDescription;
                            divFreObj.Date = result[i].Date;
                            divFreObj.FreV = result[i].FreV;
                            divFreObj.FreMV = result[i].FreMV;
                            divFreObj.Phase = result[i].Phase;
                            if (result[i].Unit == 0)
                            {
                                divFreObj.Unit = "m/s²";
                            }
                            else if (result[i].Unit == 1)
                            {
                                divFreObj.Unit = "mm/s";
                            }
                            else if (result[i].Unit == 2)
                            {
                                divFreObj.Unit = "um";
                            }
                            divFreObjects[i] = divFreObj;
                        }
                        divFreCollection.AddItems(divFreObjects);
                    }
                }
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-分频查询", ex));
            }
            finally
            {
                BusyIndicateService.Instance.Busy(false);
            }
        }
        private async Task<DivFreTableFreContract[]> LoadingDivFreDataAsync(int globalIndex,int divFreIndex)
        {
            string ipAddress = ServerAddress.CTLAddress.Split('/')[2].Split(':')[0];
            LinqWhereHelper helper = new LinqWhereHelper();
            string alarmConditionStr = string.Empty;
            if (AllowNormal)
            {
                alarmConditionStr = "1";
            }
            if (AllowWarning)
            {
                alarmConditionStr += ",2";
            }
            if (AllowDanger)
            {
                alarmConditionStr += ",3";
            }
            if (!string.IsNullOrEmpty(alarmConditionStr))
            {
                helper.AddCondition("AlarmGrade", "in", "(" + alarmConditionStr + ")");
            }
            if (AllowRPMFilter)
            {
                helper.AddCondition("RPM", ">", DownRPMFilter);
                helper.AddCondition("RPM", "<=", UpRPMFilter);
            }
            helper.AddCondition("Unit", "=", (int)UnitFilter);
            helper.AddCondition("STIME", ">", LowerTime);
            helper.AddCondition("STIME", "<=", UpperTime);
            helper.AddCondition("DivFreGlobalIndex", "=", divFreIndex);
            return await Task.Run(() => database.QueryDivFreTableFre(ipAddress, globalIndex, helper, LowerTime.Value, UpperTime.Value, ChannelRecordLength));
        }
        #endregion

        #region LoadingDataCommand
        public IAsyncCommand LoadingDataCommand { get; private set; }
        public IAsyncCommand LoadingEquipmentDataCommand { get; set; }
        public IAsyncCommand LoadingWorkshoptDataCommand { get; set; }

        private async Task LoadingWorkshoptData(object args)
        {
            var workshop = args as WorkShopTreeModel;
            if (workshop != null)
            {
                foreach(var eq in workshop.Children)
                {
                    await LoadingEquipmentData(eq);
                }
            }
        }

        private async Task LoadingEquipmentData(object args)
        {
            var equip = args as EquipmentTreeModel;
            if (equip != null)
            {
                foreach (var tp in equip.Children)
                {
                    await LoadingData(tp);
                }
            }
        }

        private async Task LoadingData(object args)
        {
            try
            {
                var testPoint = args as TestPointTreeModel;
                if (testPoint == null) return;
                BusyIndicateService.Instance.Busy(true);

                var channelContract = database.ChannelContracts.Where(o => o.ChannelID == testPoint.ChannelID).SingleOrDefault();
                if (channelContract == null) return;
                if (DataViewMode == "Chart")
                {
                    if (addedChannels.Select(o => o.Channel).Contains(channelContract)) return;
                    if (testPoint.SignalType == SignalType.Vibration)
                    {
                        var result = await LoadingVibrationDataAsync(channelContract.ChannelGlobalIndex);
                        if (result.Length == 0) return;
                        VibrationChannelToken channel = new VibrationChannelToken()
                        {
                            Channel = channelContract,
                            DataContracts = result,
                        };
                        amsReplayVM.AddChannel(channel);
                        timeDomainVM.AddChannel(channel);
                        frequencyDomainVM.AddChannel(channel);
                        powerSpectrumVM.AddChannel(channel);
                        powerSpectrumDensityVM.AddChannel(channel);
                        alarmPointTrendVM.AddChannel(channel);
                        orthoDataVM.AddChannel(channel);

                        offDesignConditionVM.AddChannel(channel);

                        addedChannels.Add(channel);
                    }
                    else if (testPoint.SignalType == SignalType.Analog || testPoint.SignalType == SignalType.Digital || testPoint.SignalType == SignalType.Composition)
                    {
                        var result = await LoadingAnalogDataAsync(channelContract.ChannelGlobalIndex);
                        if (result.Length == 0) return;
                        AnalogChannelToken channel = new AnalogChannelToken()
                        {
                            Channel = channelContract,
                            DataContracts = result,
                        };
                        if (ShowAMS)
                        {
                            amsReplayVM.AddChannel(channel);
                        }
                        addedChannels.Add(channel);
                    }
                }
                else if (DataViewMode == "List")
                {
                    if (testPoint.SignalType == SignalType.Vibration)
                    {
                        var result = await LoadingVibrationDataAsync(channelContract.ChannelGlobalIndex);
                        if (result.Length == 0) return;
                        AMSObject[] amsObjects = new AMSObject[result.Length];            
                        for (int i = 0; i < result.Length; i++)
                        {
                            AMSObject amsObj = new AMSObject();
                            amsObj.GroupCOName = channelContract.GroupCOName;
                            amsObj.CorporationName = channelContract.CorporationName;
                            amsObj.WorkShopName = channelContract.WorkShopName;
                            amsObj.DevName = channelContract.DevName;
                            amsObj.DevSN = channelContract.DevSN;
                            amsObj.CHName = channelContract.Name;
                            amsObj.CHMSSN = channelContract.MSSN;
                            amsObj.Date = result[i].Date;
                            amsObj.Value = result[i].Value;
                            if (result[i].Unit == 0)
                            {
                                amsObj.Unit = "m/s^2";
                            }
                            else if (result[i].Unit == 1)
                            {
                                amsObj.Unit = "mm/s";
                            }
                            else if (result[i].Unit == 2)
                            {
                                amsObj.Unit = "um";
                            }
                            amsObjects[i] = amsObj;
                        }
                        vInfoCollection.AddItems(amsObjects);
                    }
                    else if (testPoint.SignalType == SignalType.Analog || testPoint.SignalType == SignalType.Digital || testPoint.SignalType == SignalType.Composition)
                    {
                        var result = await LoadingAnalogDataAsync(channelContract.ChannelGlobalIndex);
                        if (result.Length == 0) return;
                        AMSObject[] amsObjects = new AMSObject[result.Length];
                        for (int i = 0; i < result.Length; i++)
                        {
                            AMSObject amsObj = new AMSObject();
                            amsObj.GroupCOName = channelContract.GroupCOName;
                            amsObj.CorporationName = channelContract.CorporationName;
                            amsObj.WorkShopName = channelContract.WorkShopName;
                            amsObj.DevName = channelContract.DevName;
                            amsObj.DevSN = channelContract.DevSN;
                            amsObj.CHName = channelContract.Name;
                            amsObj.CHMSSN = channelContract.MSSN;
                            amsObj.Date = result[i].Date;
                            amsObj.Value = result[i].Value;
                            if (result[i].Unit == 3)
                            {
                                amsObj.Unit = "°C";
                            }
                            else if (result[i].Unit == 4)
                            {
                                amsObj.Unit = "Pa";
                            }
                            else if (result[i].Unit == 5)
                            {
                                amsObj.Unit = "RPM";
                            }
                            else if (result[i].Unit == 6)
                            {
                                amsObj.Unit = "Unit";
                            }
                            amsObjects[i] = amsObj;
                        }
                        anInfoCollection.AddItems(amsObjects);
                    }
                }
            }
            catch (Exception ex)
            {
                //StringBuilder sb = new StringBuilder();
                //if (ex is AggregateException)
                //{
                //    var ae = ex as AggregateException;
                //    foreach (var e in ae.Flatten().InnerExceptions)
                //    {
                //        if (e is IOException)
                //        {
                //            sb.AppendLine("查询超时");
                //            sb.AppendLine(e.ToString());
                //        }
                //        else
                //        {
                //            sb.AppendLine(e.ToString());
                //        }
                //    }
                //}
                //else
                //{
                //    sb.AppendLine(ex.ToString());
                //}
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-通道数据跟新失败", ex));
               // InteractionRequestService.Instance.InteractionRequest.Raise(new Confirmation() { Content = sb.ToString().Trim(), Title = "数据查询异常" }, confirm => { });
            }
            finally
            {
                BusyIndicateService.Instance.Busy(false);
            }
        }
        private async Task<AnInfoTableAMSContract[]> LoadingAnalogDataAsync(int globalIndex)
        {
            string ipAddress = ServerAddress.CTLAddress.Split('/')[2].Split(':')[0];
            LinqWhereHelper helper1 = new LinqWhereHelper();
            helper1.AddCondition("STIME", ">", LowerTime);
            helper1.AddCondition("STIME", "<=", UpperTime);
            return await Task.Run(() => database.QueryAnInfoTableAMS(ipAddress, globalIndex, helper1, LowerTime.Value, UpperTime.Value, ChannelRecordLength));
        }
        private async Task<VInfoTableAMSContract[]> LoadingVibrationDataAsync(int globalIndex)
        {
            string ipAddress = ServerAddress.CTLAddress.Split('/')[2].Split(':')[0];
            LinqWhereHelper helper = new LinqWhereHelper();
            string alarmConditionStr = string.Empty;
            if (AllowNormal)
            {
                alarmConditionStr = "1";
            }
            if (AllowWarning)
            {
                alarmConditionStr += ",2";
            }
            if (AllowDanger)
            {
                alarmConditionStr += ",3";
            }
            if (!string.IsNullOrEmpty(alarmConditionStr))
            {
                helper.AddCondition("AlarmGrade", "in", "(" + alarmConditionStr + ")");
            }
            if (AllowRPMFilter)
            {
                helper.AddCondition("RPM", ">", DownRPMFilter);
                helper.AddCondition("RPM", "<=", UpRPMFilter);
            }
            helper.AddCondition("TriggerN", "=", (int)TriggerFilter);
            helper.AddCondition("Unit", "=", (int)UnitFilter);
            helper.AddCondition("STIME", ">", LowerTime);
            helper.AddCondition("STIME", "<=", UpperTime);
            return await Task.Run(() => database.QueryVInfoTableAMS(ipAddress, globalIndex, helper, LowerTime.Value, UpperTime.Value, ChannelRecordLength));
        }
        #endregion LoadingDataCommand

        #region ViewModeSwitchCommand
        public DelegateCommand<string> ViewModeSwitchCommand { get; private set; }
        private void ViewModeSwitch(string mode)
        {
            ViewMode = mode;
        }
        #endregion ViewModeSwitchCommand

        #region DataViewModeSwitchCommand
        public DelegateCommand<string> DataViewModeSwitchCommand { get; private set; }
        private void DataViewModeSwitch(string mode)
        {
            DataViewMode = mode;
        }
        #endregion

        #region ClearListCommand
        public DelegateCommand<string> ClearListCommand { get; private set; }
        private void ClearList(string signalType)
        {
            if (signalType == "Vibration")
            {
                vInfoCollection.Clear();
            }
            else if (signalType == "Analog")
            {
                anInfoCollection.Clear();
            }
            else if (signalType == "DivFre")
            {
                divFreCollection.Clear();
            }
        }
        #endregion

        #region RefreshChannelDataCommand
        public IAsyncCommand RefreshChannelDataCommand { get; private set; }
        private async Task RefreshChannelData()
        {
            try
            {
                BusyIndicateService.Instance.Busy(true);

                var vibratonChannels = addedChannels.OfType<VibrationChannelToken>().ToArray();
                if (vibratonChannels.Length > 0)
                {
                    foreach (var item in vibratonChannels)
                    {
                        var result = await LoadingVibrationDataAsync(item.Channel.ChannelGlobalIndex);
                        item.DataContracts = result;
                    }
                }
                var analogChannels = addedChannels.OfType<AnalogChannelToken>().ToArray();
                if (analogChannels.Length > 0)
                {
                    foreach (var item in analogChannels)
                    {
                        var result = await LoadingAnalogDataAsync(item.Channel.ChannelGlobalIndex);
                        item.DataContracts = result;
                    }
                }
                var divfreChannels = addedChannels.OfType<DivFreChannelToken>().ToArray();
                if (divfreChannels.Length > 0)
                {
                    foreach (var item in divfreChannels)
                    {
                        var result = await LoadingDivFreDataAsync(item.Channel.ChannelGlobalIndex, item.Channel.DivFreGlobalIndex);
                        item.DataContracts = result;
                    }
                }

                amsReplayVM.ChannelDataChanged(addedChannels);
                offDesignConditionVM.ChannelDataChanged(addedChannels);
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-通道数据跟新失败", ex));
                InteractionRequestService.Instance.InteractionRequest.Raise(new Confirmation() { Content = ex.Message, Title = "通道数据跟新失败" }, confirm => { });
            }
            finally
            {
                BusyIndicateService.Instance.Busy(false);
            }
        }
        private async Task<AnInfoTableAMSContract[]> LoadingAnalogDatasAsync(int[] globalIndexes)
        {
            string ipAddress = ServerAddress.CTLAddress.Split('/')[2].Split(':')[0];
            LinqWhereHelper helper1 = new LinqWhereHelper();
            helper1.AddCondition("STIME", ">", LowerTime);
            helper1.AddCondition("STIME", "<=", UpperTime);
            return await Task.Run(() => database.QueryMultiAnInfoTableAMS(ipAddress, globalIndexes, helper1, LowerTime.Value, UpperTime.Value, ChannelRecordLength));
        }
        private async Task<VInfoTableAMSContract[]> LoadingVibrationDatasAsync(int[] globalIndexes)
        {
            string ipAddress = ServerAddress.CTLAddress.Split('/')[2].Split(':')[0];
            LinqWhereHelper helper = new LinqWhereHelper();
            string alarmConditionStr = string.Empty;
            if (AllowNormal)
            {
                alarmConditionStr = "1";
            }
            if (AllowWarning)
            {
                alarmConditionStr += ",2";
            }
            if (AllowDanger)
            {
                alarmConditionStr += ",3";
            }
            if (!string.IsNullOrEmpty(alarmConditionStr))
            {
                helper.AddCondition("AlarmGrade", "in", "(" + alarmConditionStr + ")");
            }
            if (AllowRPMFilter)
            {
                helper.AddCondition("RPM", ">", DownRPMFilter);
                helper.AddCondition("RPM", "<=", UpRPMFilter);
            }
            helper.AddCondition("TriggerN", "=", (int)TriggerFilter);
            helper.AddCondition("Unit", "=", (int)UnitFilter);
            helper.AddCondition("STIME", ">", LowerTime);
            helper.AddCondition("STIME", "<=", UpperTime);
            return await Task.Run(() => database.QueryMutiVInfoTableAMS(ipAddress, globalIndexes, helper, LowerTime.Value, UpperTime.Value, ChannelRecordLength));
        }
        #endregion RefreshChannelDataCommand

        #region SearchAdvancedCommand
        public DelegateCommand<object> SearchAdvancedCommand { get; private set; }
        private async Task SearchAdvanced()
        {
            try
            {
                if (amsReplayVM.AdvancedCount > 400)
                {
                    InteractionRequestService.Instance.InteractionRequest.Raise(new Confirmation() { Content = "查询数据量不能超过400", Title = "上限溢出" }, confirm => { });
                    return;
                }
                BusyIndicateService.Instance.Busy(true);
                var graphTypes = amsReplayVM.SelectedGraphTypes.Select(o => (string)o.Value).ToArray();
                if (graphTypes.Contains("RPM3D"))
                {
                    ShowRPM3DSpectrum = true;
                }
                if (graphTypes.Contains("OrderAnalysis"))
                {
                    ShowOrderAnalysis = true;
                }
                if (amsReplayVM.SelecetedSeriesToken == null)
                {
                    InteractionRequestService.Instance.InteractionRequest.Raise(new Confirmation() { Content = "通道不能为空", Title = "参数为空" }, confirm => { });
                    return;
                }

                ChannelToken channel = amsReplayVM.SelecetedSeriesToken;
                int globalIndex = channel.Channel.ChannelGlobalIndex;

                VInfoTableContract[] vInfoDatas =await LoadVInfoData(globalIndex);
                List<KeyValuePair<VInfoTableContract, VibrationData>> fftConvert = await Task.Run(() => FFTConvert(vInfoDatas));

                //阶次分析
                if (ShowOrderAnalysis)
                {
                    OrderAnalysisObject createOrderAnalysis = CreateOrderAnalysis(fftConvert);
                    createOrderAnalysis.Token = channel;
                    orderAnalysisVM.OrderAnalysisData = createOrderAnalysis;
                }

                //转速三维谱
                if (ShowRPM3DSpectrum)
                {
                    RPM3DSpectrumObject createRPM3DSpectrum = CreateRPM3DSpectrum(fftConvert);
                    createRPM3DSpectrum.Token = channel;
                    rpm3DSpectrumVM.RPM3DSpectrumData = createRPM3DSpectrum;
                }
            }
            catch (Exception ex)
            {
                StringBuilder errors = new StringBuilder();
                if (ex is AggregateException)
                {
                    var ae = ex as AggregateException;
                    ae.Flatten().Handle(e =>
                    {
                        if (e is OperationCanceledException)
                        {
                            return true;
                        }
                        else
                        {
                            errors.AppendLine(e.ToString());
                            return true;
                        }
                    });
                }
                else
                {
                    errors.AppendLine(ex.ToString());
                }

                if (errors.Length != 0)
                {
                    InteractionRequestService.Instance.InteractionRequest.Raise(new Confirmation() { Content = errors.ToString(), Title = "振动通道查询异常" }, confirm => { });
                }
            }
            finally
            {
                BusyIndicateService.Instance.Busy(false);
            }
        }
        private async Task<VInfoTableContract[]> LoadVInfoData(int globalIndex)
        {
            LinqWhereHelper helper = new LinqWhereHelper();
            helper.AddCondition("STIME", ">", amsReplayVM.StartTime);
            helper.AddCondition("STIME", "<=", amsReplayVM.EndTime);
            string ipAddress = ServerAddress.CTLAddress.Split('/')[2].Split(':')[0];
            var list = await Task.Run(() => database.QueryVInfoTable(ipAddress, globalIndex, helper, amsReplayVM.StartTime.Value, amsReplayVM.EndTime.Value));
            if (list.Length > 0)
            {
                return list;
            }
            else
            {
                throw new Exception("搜索条件不包含任何数据");
            }
        }
        private List<KeyValuePair<VInfoTableContract, VibrationData>> FFTConvert(VInfoTableContract[] vInfos)
        {
            List<KeyValuePair<VInfoTableContract, VibrationData>> pairList = new List<KeyValuePair<VInfoTableContract, VibrationData>>();
            for (int i = 0; i < vInfos.Length; i++)
            {
                VibrationData vdata = new VibrationData();
                vdata.AMS = vInfos[i].VAMS;
                vdata.STIME = vInfos[i].STIME;
                vdata.RPM = vInfos[i].RPM ?? 0;
                vdata.TeethNumber = vInfos[i].TeethNumber ?? 0;
                vdata.SampleFre = vInfos[i].SampleFre;
                vdata.SamplePoint = vInfos[i].SamplePoint;
                vdata.Trigger = (TriggerType)Enum.Parse(typeof(TriggerType), vInfos[i].TriggerN.ToString());
                switch (vInfos[i].Unit)
                {
                    case 0:
                        vdata.Unit = "m/s2";
                        break;
                    case 1:
                        vdata.Unit = "mm/s";
                        break;
                    case 2:
                        vdata.Unit = "um";
                        break;
                }

                var paras = Algorithm.Instance.CalculatePara(vdata.Waveform);
                if (paras != null)
                {
                    vdata.PeakValue = paras[0];
                    vdata.PeakPeakValue = paras[1];
                    vdata.Slope = paras[2];
                    vdata.Kurtosis = paras[3];
                    vdata.KurtosisValue = paras[4];
                    vdata.WaveIndex = paras[5];
                    vdata.PeakIndex = paras[6];
                    vdata.ImpulsionIndex = paras[7];
                    vdata.RootAmplitude = paras[8];
                    vdata.ToleranceIndex = paras[9];
                }
                double sampleFre = vdata.SampleFre;
                if (vdata.Trigger == TriggerType.Angle)
                {
                    if (vdata.RPM > 0 && vdata.TeethNumber > 0)
                    {
                        sampleFre = vdata.RPM * vdata.TeethNumber / 60;
                    }
                }

                int length = (int)(vdata.SamplePoint / 2.56) + 1;
                if (vdata.Frequency == null || vdata.Frequency.Length != length)
                {
                    vdata.Frequency = new double[length];
                }
                double frequencyInterval = sampleFre / vdata.SamplePoint;
                for (int j = 0; j < length; j++)
                {
                    vdata.Frequency[i] = frequencyInterval * j;
                }
                var output = Algorithm.Instance.FFT2AndPhaseAction(vdata.Waveform, vdata.SamplePoint);
                vdata.Amplitude = output[0];
                vdata.Phase = output[1];  

                pairList.Add(new KeyValuePair<VInfoTableContract, VibrationData>(vInfos[i], vdata));
            }
            return pairList;
        }  
        private RPM3DSpectrumObject CreateRPM3DSpectrum(List<KeyValuePair<VInfoTableContract, VibrationData>> fftDict)
        {
            RPM3DSpectrumObject rpm3DSpectrum = new RPM3DSpectrumObject();
            VInfoTableContract vInfo = fftDict.First().Key;
            double maxResolution = 0;
            if (vInfo.TriggerN == 2)
            {
                if (vInfo.RPM == null || vInfo.TeethNumber == null || vInfo.RPM <= 0 || vInfo.TeethNumber <= 0) return null;
                maxResolution = ((double)vInfo.RPM * (int)vInfo.TeethNumber / 60) / 2.56;
            }
            else
            {
                maxResolution = vInfo.SampleFre / 2.56;
            }

            rpm3DSpectrum.RangeMinX = 0;
            rpm3DSpectrum.RangeMaxX = maxResolution;
            rpm3DSpectrum.RangeMinY = 0;
            double maxY = 0;
            foreach (var item in fftDict)
            {
                maxY = Math.Max(maxY, item.Value.Amplitude.Max());
            }
            rpm3DSpectrum.RangeMaxY = maxY;
            rpm3DSpectrum.RangeMinZ = 0;
            rpm3DSpectrum.RangeMaxZ = fftDict.Select(o => o.Key.RPM.Value).Max();

            Dictionary<Tuple<double, double>, double[]> dataSource = new Dictionary<Tuple<double, double>, double[]>();
            foreach (var item in fftDict)
            {
                double resolution = item.Key.SampleFre / item.Key.SamplePoint;
                if (item.Key.TriggerN == 2)
                {
                    resolution = ((double)item.Key.RPM * (int)item.Key.TeethNumber / 60) / item.Key.SamplePoint;
                }
                var key = Tuple.Create<double, double>((double)item.Key.RPM, resolution);
                if (!dataSource.ContainsKey(key))
                {
                    dataSource.Add(key, item.Value.Amplitude);
                }
            }
            rpm3DSpectrum.DataSource = dataSource;
            return rpm3DSpectrum;
        }
        private OrderAnalysisObject CreateOrderAnalysis(List<KeyValuePair<VInfoTableContract, VibrationData>> fftDict)
        {
            OrderAnalysisObject orderAnalysis = new OrderAnalysisObject();

            var indexList = new List<int[]>();
            int iRow = fftDict.Count;
            int iColumn = 100;
            double maxY = 0;
            for (int i = 0; i < fftDict.Count; i++)
            {
                var valueDict = fftDict[i].Value.Amplitude.Select((s, k) => new { Key = k, Value = s }).OrderByDescending(o => o.Value).Take(iColumn);
                maxY = Math.Max(maxY, valueDict.First().Value);
                indexList.Add(valueDict.Select(o => o.Key).ToArray());
            }

            orderAnalysis.MaxYValue = maxY * 1.2;
            orderAnalysis.MaxZValue = fftDict.Select(o => o.Key.RPM.Value).Max() * 1.2;

            double maxX = 0;
            SurfacePoint[,] pointsArray = new SurfacePoint[iRow, iColumn];

            if (TriggerFilter == AIC.CoreType.TriggerType.Angle)
            {
                for (int i = 0; i < iRow; i++)
                {
                    double[] frequency = indexList[i].Select(o => fftDict[i].Value.Frequency[o]).ToArray();
                    double[] amplitude = indexList[i].Select(o => fftDict[i].Value.Frequency[o]).ToArray();
                    for (int j = 0; j < iColumn; j++)
                    {
                        SurfacePoint point = new SurfacePoint();
                        point.X = frequency[j] / (fftDict[i].Key.SamplePoint / 64);
                        point.Y = amplitude[j];
                        point.Z = fftDict[i].Key.RPM.Value;
                        pointsArray[i, j] = point;
                        if (point.X > maxX)
                        {
                            maxX = point.X * 1.2;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < iRow; i++)
                {
                    for (int j = 0; j < iColumn; j++)
                    {
                        double[] frequency = indexList[i].Select(o => fftDict[i].Value.Frequency[o]).ToArray();
                        double[] amplitude = indexList[i].Select(o => fftDict[i].Value.Frequency[o]).ToArray();
                        SurfacePoint point = new SurfacePoint();
                        if (fftDict[i].Key.RPM > 0)
                        {
                            point.X = (frequency[j] * 60) / fftDict[i].Key.RPM.Value;
                        }
                        else
                        {
                            point.X = -1;
                        }
                        point.Y = amplitude[j];
                        point.Z = fftDict[i].Key.RPM.Value;
                        pointsArray[i, j] = point;
                        if (point.X > maxX)
                        {
                            maxX = point.X * 1.2;
                        }
                    }
                }
            }
            orderAnalysis.MaxXValue = maxX;
            orderAnalysis.SurfacePointArray = pointsArray;
            return orderAnalysis;
        }
        #endregion SearchAdvancedCommand

        #region RemoveChannels
        public DelegateCommand<object> RemoveChannelsCommand { get; private set; }
        public void RemoveChannels(object arg)
        {
            ChannelToken token = arg as ChannelToken;
            if (addedChannels.Contains(token))
            {
                addedChannels.Remove(token);
                amsReplayVM.RemoveChannel(token);
                alarmPointTrendVM.RemoveChannel(token as VibrationChannelToken);
                timeDomainVM.RemoveChannel(token as VibrationChannelToken);
                frequencyDomainVM.RemoveChannel(token as VibrationChannelToken);
                powerSpectrumVM.RemoveChannel(token as VibrationChannelToken);
                powerSpectrumDensityVM.RemoveChannel(token as VibrationChannelToken);
                orthoDataVM.RemoveChannel(token as VibrationChannelToken);
                time3DSpectrumVM.RemoveChannel(token as VibrationChannelToken);
                offDesignConditionVM.RemoveChannel(token);
            }
        }
        private bool CanRemoveChannels(object arg)
        {
            return true;
        }
        #endregion RemoveChannels

        #region DiagnoseCommand
        public DelegateCommand<object> DiagnoseCommand { get; private set; }
        private void Diagnose(object arg)
        {
            //CurrentState = "Busy";
            //Task.Factory.StartNew(() =>
            //{
            //    ChannelToken seriesToken = timeDomainVM.ContractsCollection.Where(o => o.Token == SelectedChannel).SingleOrDefault();
            //    VibrationData tdObject = seriesToken.SeriesDataSource as VibrationData;

            //    ChannelToken freSeriesToken = frequencyDomainVM.ContractsCollection.Where(o => o.Token == SelectedChannel).SingleOrDefault();
            //    VibrationData freObject = freSeriesToken.SeriesDataSource as VibrationData;

            //    Dictionary<string, object> dicdata = new Dictionary<string, object>();
            //    dicdata.Add("VData", tdObject.Waveform);
            //    dicdata.Add("SampleFre", freObject.SampleFre);
            //    dicdata.Add("SamplePoint", freObject.SamplePoint);
            //    dicdata.Add("RealRPM", 0.0);

            //    WebClient client = new WebClient();
            //    try
            //    {
            //        byte[] responses = client.UploadData(string.Concat("http://", ListenAddress, ":", ListenPort, "/"), "POST", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dicdata)));
            //        Encoding encoding = !string.IsNullOrWhiteSpace(client.ResponseHeaders.Get("ContentEncoding")) ? Encoding.GetEncoding(client.ResponseHeaders.Get("ContentEncoding")) : Encoding.UTF8;
            //        string response = Encoding.GetEncoding("gb2312").GetString(responses);
            //        // encoding.GetString(responses);

            //        //Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            //        //{
            //        //    this.cancelConfirmationInteractionRequest.Raise(
            //        //        new Confirmation { Content = response, Title = "诊断结果" },
            //        //        confirmation =>
            //        //        {
            //        //            if (confirmation.Confirmed)
            //        //            {
            //        //                //this.NavigateToQuestionnaireList();
            //        //            }
            //        //        });
            //        //}));
            //    }
            //    catch (Exception e)
            //    {
            //        InteractionRequestService.Instance.InteractionRequest.Raise(new Confirmation() { Content = "请检查断程序是否已启动，如过没有，请先启动诊断程序" + e.ToString(), Title = "诊断异常" }, confirm => { });
            //    }
            //    finally
            //    {
            //        client.Dispose();
            //        CurrentState = "Normal";
            //    }
            //});
        }
        private bool CanDiagnose(object arg)
        {
            return SelectedChannel != null;
        }
        #endregion DiagnoseCommand

        #region VideoQueryCommand
        public DelegateCommand<object> VideoQueryCommand { get; private set; }
        public void VideoQuery(object arg)
        {
            //List<LMVideoTableContract> LMVideo_Query(string condition, List<object> values);
            // List<string> VideoReplayQueryRange(string deviceSerialNumber, DateTime startTime, DateTime endTime);
            try
            {
                vedioInfoCollection.Clear();
                string globalID = SelectedVideoChannel.GetChannelGlobalID();
                var urls = WCFCaller<IVideoManagement>.ExecuteMethod(ServerAddress.VIDEOADDRESS, "VideoReplayQueryRange", globalID, VideoStartTime, VideoEndTime) as List<string>;
                if (urls != null)
                {
                    foreach (var url in urls)
                    {
                        var urlParts = url.Split('/');
                        if (urlParts.Length == 7)
                        {
                            VideoInfo info = new VideoInfo()
                            {
                                Address = new Uri(url),
                                Directory = string.Format("{0}\\{1}\\{2}\\", urlParts[3], urlParts[4], urlParts[5]),
                                FullPath = string.Format("{0}\\{1}\\{2}\\{3}", urlParts[3], urlParts[4], urlParts[5], urlParts[6]),
                                Name = string.Format("{0}/{1}/{2}", urlParts[4], urlParts[5], urlParts[6])
                            };
                            vedioInfoCollection.Add(info);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                InteractionRequestService.Instance.InteractionRequest.Raise(new Confirmation() { Content = e.ToString(), Title = "视频查询异常" }, confirm => { });
            }
        }
        private bool CanVideoQuery(object arg)
        {
            return SelectedVideoChannel != null;
        }
        #endregion VideoQueryCommand

        #region Public Property

        public int ChannelRecordLength
        {
            get { return channelRecordLength; }
            set
            {
                if (value != channelRecordLength)
                {
                    channelRecordLength = value;
                    this.OnPropertyChanged("ChannelRecordLength");
                }
            }
        }   
        public bool AllowNormal
        {
            get { return allowNormal; }
            set
            {
                if (allowNormal != value)
                {
                    allowNormal = value;
                    OnPropertyChanged(() => AllowNormal);
                }
            }
        }
        public bool AllowWarning
        {
            get { return allowWarning; }
            set
            {
                if (allowWarning != value)
                {
                    allowWarning = value;
                    OnPropertyChanged(() => AllowWarning);
                }
            }
        }      
        public bool AllowDanger
        {
            get { return allowDanger; }
            set
            {
                if (allowDanger != value)
                {
                    allowDanger = value;
                    OnPropertyChanged(() => AllowDanger);
                }
            }
        } 
        public int FFTLength
        {
            get { return fftLength; }
            set
            {
                if (fftLength != value)
                {
                    fftLength = value;
                    OnPropertyChanged("FFTLength");
                }
            }
        }
        public double? PeakValue
        {
            get { return peakValue; }
            set
            {
                if (peakValue != value)
                {
                    peakValue = value;
                    this.OnPropertyChanged(() => PeakValue);
                }
            }
        }  
        public double? PeakPeakValue
        {
            get { return peakPeakValue; }
            set
            {
                if (peakPeakValue != value)
                {
                    peakPeakValue = value;
                    this.OnPropertyChanged(() => PeakPeakValue);
                }
            }
        }    
        public DateTime? VideoStartTime
        {
            get { return videoStartTime; }
            set
            {
                if (value != videoStartTime)
                {
                    videoStartTime = value;
                    this.OnPropertyChanged("VideoStartTime");
                }
            }
        }   
        public DateTime? VideoEndTime
        {
            get { return videoEndTime; }
            set
            {
                if (value != videoEndTime)
                {
                    videoEndTime = value;
                    this.OnPropertyChanged("VideoEndTime");
                }
            }
        }
        public DateTime? StartTime
        {
            get { return startTime; }
            set
            {
                if (value != startTime)
                {
                    startTime = value;
                    LowerTime = value;
                    this.OnPropertyChanged("StartTime");
                }
            }
        }  
        public DateTime? EndTime
        {
            get { return endTime; }
            set
            {
                if (value != endTime)
                {
                    endTime = value;
                    UpperTime = value;
                    this.OnPropertyChanged("EndTime");
                }
            }
        }
        public DateTime? LowerTime
        {
            get { return lowerTime; }
            set
            {
                if (value != lowerTime)
                {
                    lowerTime = value;
                    this.OnPropertyChanged("LowerTime");
                }
            }
        }
        public DateTime? UpperTime
        {
            get { return upperTime; }
            set
            {
                if (value != upperTime)
                {
                    upperTime = value;
                    this.OnPropertyChanged("UpperTime");
                }
            }
        }
        public ChannelToken SelectedChannel
        {
            get { return selectedChannel; }
            set
            {
                if (selectedChannel != value)
                {
                    selectedChannel = value;
                    this.OnPropertyChanged(() => this.SelectedChannel);
                    DiagnoseCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public AIC.CoreType.Unit UnitFilter
        {
            get { return unitFilter; }
            set
            {
                if (unitFilter != value)
                {
                    unitFilter = value;
                    OnPropertyChanged(() => this.UnitFilter);
                }
            }
        }
        public AIC.CoreType.TriggerType TriggerFilter
        {
            get { return triggerFilter; }
            set
            {
                if (triggerFilter != value)
                {
                    triggerFilter = value;
                    OnPropertyChanged(() => this.TriggerFilter);
                }
            }
        }
        public string ListenAddress
        {
            get { return listenAddress; }
            set
            {
                if (listenAddress != value)
                {
                    listenAddress = value;
                    OnPropertyChanged(() => this.ListenAddress);

                    //save App Config
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    AppSettingsSection appSettings = (AppSettingsSection)config.GetSection("appSettings");
                    appSettings.Settings["LISTENADDRESS"].Value = ListenAddress;
                    config.Save();
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }
        public int ListenPort
        {
            get { return listenPort; }
            set
            {
                if (listenPort != value)
                {
                    listenPort = value;
                    OnPropertyChanged(() => this.ListenPort);
                    //save App Config
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    AppSettingsSection appSettings = (AppSettingsSection)config.GetSection("appSettings");
                    appSettings.Settings["LISTENPORT"].Value = ListenPort.ToString();
                    config.Save();
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }
        public LMVideoTableContract SelectedVideoChannel
        {
            get { return selectedVideoChannel; }
            set
            {
                if (selectedVideoChannel != value)
                {
                    selectedVideoChannel = value;
                    OnPropertyChanged(() => SelectedVideoChannel);
                }
            }
        }
        public VideoInfo SelectedVideo
        {
            get { return selectedVideo; }
            set
            {
                if (selectedVideo != value)
                {
                    selectedVideo = value;
                    OnPropertyChanged(() => SelectedVideo);
                }
            }
        }
        public double ItemWidth
        {
            get { return itemWidth; }
            set
            {
                if (itemWidth != value)
                {
                    itemWidth = value;
                    OnPropertyChanged("ItemWidth");
                    amsReplayVM.ItemWidth = value;
                    timeDomainVM.ItemWidth = value;
                    frequencyDomainVM.ItemWidth = value;
                    powerSpectrumVM.ItemWidth = value;
                    powerSpectrumDensityVM.ItemWidth = value;
                    orthoDataVM.ItemWidth = value;
                    offDesignConditionVM.ItemWidth = value;
                    orderAnalysisVM.ItemWidth = value;
                    time3DSpectrumVM.ItemWidth = value;
                    rpm3DSpectrumVM.ItemWidth = value;
                    alarmPointTrendVM.ItemWidth = value;
                }
            }
        }
        public double ItemHeight
        {
            get { return itemHeight; }
            set
            {
                if (itemHeight != value)
                {
                    itemHeight = value;
                    OnPropertyChanged("ItemHeight");
                    amsReplayVM.ItemHeight = value;
                    timeDomainVM.ItemHeight = value;
                    frequencyDomainVM.ItemHeight = value;
                    powerSpectrumVM.ItemHeight = value;
                    powerSpectrumDensityVM.ItemHeight = value;
                    orthoDataVM.ItemHeight = value;
                    offDesignConditionVM.ItemHeight = value;
                    orderAnalysisVM.ItemHeight = value;
                    time3DSpectrumVM.ItemHeight = value;
                    rpm3DSpectrumVM.ItemHeight = value;
                    alarmPointTrendVM.ItemHeight = value;
                }
            }
        }
        public bool IsComposing
        {
            get { return isComposing; }
            set
            {
                if (isComposing != value)
                {
                    isComposing = value;
                    OnPropertyChanged("IsComposing");
                }
            }
        }
        public bool ShowAMS
        {
            get { return showAMS; }
            set
            {
                if (showAMS != value)
                {
                    showAMS = value;
                    OnPropertyChanged("ShowAMS");
                    amsReplayVM.IsVisible = value;
                }
            }
        }
        public bool ShowTimeDomain
        {
            get { return showTimeDomain; }
            set
            {
                if (showTimeDomain != value)
                {
                    showTimeDomain = value;
                    OnPropertyChanged("ShowTimeDomain");
                    timeDomainVM.IsVisible = value;
                }
            }
        }
        public bool ShowFrequencyDomain
        {
            get { return showFrequencyDomain; }
            set
            {
                if (showFrequencyDomain != value)
                {
                    showFrequencyDomain = value;
                    OnPropertyChanged("ShowFrequencyDomain");
                    frequencyDomainVM.IsVisible = value;
                }
            }
        }
        public bool ShowPowerSpectrum
        {
            get { return showPowerSpectrum; }
            set
            {
                if (showPowerSpectrum != value)
                {
                    showPowerSpectrum = value;
                    OnPropertyChanged("ShowPowerSpectrum");
                    powerSpectrumVM.IsVisible = value;
                }
            }
        }
        public bool ShowPowerSpectrumDensity
        {
            get { return showPowerSpectrumDensity; }
            set
            {
                if (showPowerSpectrumDensity != value)
                {
                    showPowerSpectrumDensity = value;
                    OnPropertyChanged("ShowPowerSpectrumDensity");
                    powerSpectrumDensityVM.IsVisible = value;
                }
            }
        }
        public bool ShowOrtho
        {
            get { return showOrtho; }
            set
            {
                if (showOrtho != value)
                {
                    showOrtho = value;
                    OnPropertyChanged("ShowOrtho");
                    orthoDataVM.IsVisible = value;
                }
            }
        }
        public bool ShowOffCondition
        {
            get { return showOffCondition; }
            set
            {
                if (showOffCondition != value)
                {
                    showOffCondition = value;
                    OnPropertyChanged("ShowOffCondition");
                    offDesignConditionVM.IsVisible = value;
                }
            }
        }
        public bool ShowOrderAnalysis
        {
            get { return showOrderAnalysis; }
            set
            {
                if (showOrderAnalysis != value)
                {
                    showOrderAnalysis = value;
                    OnPropertyChanged("ShowOrderAnalysis");
                    orderAnalysisVM.IsVisible = value;
                }
            }
        }
        public bool ShowTime3DSpectrum
        {
            get { return showTime3DSpectrum; }
            set
            {
                if (showTime3DSpectrum != value)
                {
                    showTime3DSpectrum = value;
                    OnPropertyChanged("ShowTime3DSpectrum");
                    time3DSpectrumVM.IsVisible = value;
                }
            }
        }
        public bool ShowRPM3DSpectrum
        {
            get { return showRPM3DSpectrum; }
            set
            {
                if (showRPM3DSpectrum != value)
                {
                    showRPM3DSpectrum = value;
                    OnPropertyChanged("ShowRPM3DSpectrum");
                    rpm3DSpectrumVM.IsVisible = value;
                }
            }
        }
        public bool ShowAlarmPointTrend
        {
            get { return showAlarmPointTrend; }
            set
            {
                if (showAlarmPointTrend != value)
                {
                    showAlarmPointTrend = value;
                    OnPropertyChanged("ShowAlarmPointTrend");
                    alarmPointTrendVM.IsVisible = value;
                }
            }
        }
        public string ViewMode
        {
            get { return viewMode; }
            set
            {
                if (viewMode != value)
                {
                    viewMode = value;
                    OnPropertyChanged("ViewMode");
                }
            }
        }
        public string DataViewMode
        {
            get { return dataViewMode; }
            set
            {
                if (dataViewMode != value)
                {
                    dataViewMode = value;
                    OnPropertyChanged("DataViewMode");
                }
            }
        }
        public bool AllowRPMFilter
        {
            get { return allowRPMFilter; }
            set
            {
                if (allowRPMFilter != value)
                {
                    allowRPMFilter = value;
                    OnPropertyChanged("AllowRPMFilter");
                }
            }
        }
        public double UpRPMFilter
        {
            get { return upRPMFilter; }
            set
            {
                if (upRPMFilter != value)
                {
                    upRPMFilter = value;
                    OnPropertyChanged("UpRPMFilter");
                }
            }
        }
        public double DownRPMFilter
        {
            get { return downRPMFilter; }
            set
            {
                if (downRPMFilter != value)
                {
                    downRPMFilter = value;
                    OnPropertyChanged("DownRPMFilter");
                }
            }
        }
        public IEnumerable<ChannelToken> AddedChannels { get { return addedChannels; } }
        public IEnumerable<AMSObject> VInfoObjects { get { return vInfoCollection; } }
        public IEnumerable<AMSObject> AnInfoObjects { get { return anInfoCollection; } }
        public IEnumerable<DivFreObject> DivFreObjects { get { return divFreCollection; } }       
        public IEnumerable<LMVideoTableContract> VideoChannels { get { return videoChannelCollection; } }
        public IEnumerable<VideoInfo> VedioInfos { get { return vedioInfoCollection; } }
        public IEnumerable<HistoricalDataViewModel> HistoricalDatas { get { return historicalDataCollection; } }
        public IEnumerable<GroupTreeModel> Groups { get { return groupCollection; } }

        #endregion Public Property
    }

    public class VideoInfo : INotifyPropertyChanged
    {
        private string directory;
        public string Directory
        {
            get { return directory; }
            set
            {
                if (directory != value)
                {
                    directory = value;
                    NotifyPropertyChanged("Directory");
                }
            }
        }

        private string fullPath;
        public string FullPath
        {
            get { return fullPath; }
            set
            {
                if (fullPath != value)
                {
                    fullPath = value;
                    NotifyPropertyChanged("FullPath");
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private Uri address;
        public Uri Address
        {
            get { return address; }
            set
            {
                if (address != value)
                {
                    address = value;
                    NotifyPropertyChanged("Address");
                }
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify using pre-made PropertyChangedEventArgs
        /// </summary>
        /// <param name="args"></param>
        protected void NotifyPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, args);
            }
        }

        /// <summary>
        /// Notify using String property name
        /// </summary>
        protected void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}

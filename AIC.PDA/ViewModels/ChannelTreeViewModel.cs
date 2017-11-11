using AIC.Core;
using AIC.Domain;
using AIC.OnlineSystem.Common;
using AIC.PDA.Events;
using AIC.ServiceInterface;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AIC.PDA.ViewModels
{
    public class ChannelTreeViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<PDATreeModel> pdaCollection;
        private ObservableCollection<GroupTreeModel> groupCollection;

        private readonly ITreeService _treeService;

        public ChannelTreeViewModel(ITreeService treeService, IEventAggregator eventAggregator)
        {
            _treeService = treeService;
            _eventAggregator = eventAggregator;

            SelectTestPointCommand = new DelegateCommand<TreeViewItemModel>(SelectTestPoint);
            SelectChannelCommand = new DelegateCommand<TreeViewItemModel>(SelectChannel);
            AddTreeModelCommand = new DelegateCommand<TreeViewItemModel>(AddTreeModel);
            DeleteTreeModelCommand = new DelegateCommand<TreeViewItemModel>(DeleteTreeModel);
            BindChannelCommand = new DelegateCommand<object>(BindChannelAsync);
            //DeleteDataCollectorCommand = new DelegateCommand<object>(DeleteDataCollector);
            SaveTreeCommand= new DelegateCommand<object>(SaveTree);
            ConnectingPDACommand = new DelegateCommand<object>(ConnectingPDA);

            ConfirmationRequest = new InteractionRequest<IConfirmation>();

            groupCollection = new ObservableCollection<GroupTreeModel>(treeService.GetAllGroups());
            pdaCollection = new ObservableCollection<PDATreeModel>(treeService.GetAllPDAs());
        }

        private void ConnectingPDA(Object args)
        {
            var ips = pdaCollection.Select(o => o.IP);
            if (ips.Contains(TriggerIP))
            {
               // InteractionRequestService.Instance.InteractionRequest.Raise(new Confirmation() { Content = string.Format("IP:{0}已存在", TriggerIP), Title = "IP已存在" }, confirm => { });
                return;
            }

            string serverIP = ServerAddress.CTLAddress.Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(':')[0];
            int serverPort = int.Parse(ServerAddress.CTLAddress.Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(':')[1]);
            var command = new IncentiveCommand()
            {
                IP = TriggerIP,
                Port = TriggerPort,
                ServerIP = serverIP,
                ServerPort = serverPort
            };

            // var result = await Task.Run(() => (string)WCFCaller<IStorageManagement>.ExecuteMethod(ServerAddress.CTLAddress, "SendCommandFromWCFToPDA2", command));
            //WCFCaller<IStorageManagement>.ExecuteMethod(ServerAddress.CTLAddress, "SendCommandFromWCFToPDA2", command)
            //return Result.Ok(result);

            //BusyIndicateService.Instance.Busy(true);

            //await SendCommandPDA2Async()
            //     .OnSuccessAsync(JudgeCommandResult)
            //     .OnSuccessAsync(async () => await QueryIPAsync())
            //     .OnSuccessAsync(() => InteractionRequestService.Instance.InteractionRequest.Raise(new Confirmation() { Content = "链接成功", Title = "链接成功" }, confirm => { }))
            //     .OnFailureAsync(r => InteractionRequestService.Instance.InteractionRequest.Raise(new Confirmation() { Content = r.Error, Title = "链接失败" }, confirm => { }));

            //actorRef.Tell(new InformationManageActor.AttchTreeByIpMessage(TriggerIP));
            //actorRef.Tell(new InformationManageActor.RefreshTreeMessage());

            //BusyIndicateService.Instance.Busy(false);
        }

        private void SaveTree(object obj)
        {
            _treeService.Save();
        }

        private void BindChannelAsync(object arg)
        {
            ChannelTreeModel channel = arg as ChannelTreeModel;
            if (channel == null) return;

            if (channel.IsPaired)
            {
                ConfirmationRequest.Raise(new Confirmation() { Content = "确定要移除绑定么", Title = "移除绑定" }, confirm =>
                {
                    _treeService.UnBindChannel(channel.ChannelId);
                });
                return;
            }

            TestPointTreeModel testPoint = SelectedTestPoint as TestPointTreeModel;
            if (testPoint == null) return;
            if (testPoint.IsPaired)
            {
                ConfirmationRequest.Raise(new Confirmation() { Content = "测点已绑定", Title = "通道" }, confirm =>  {    });
                return;
            }
            else
            {
                _treeService.BindChannel(testPoint.TestPointId, channel.ChannelId);
                //actorRef.Tell(new InformationManageActor.BindChannelMessage(testPoint, channel));
            }
        }

        private void SelectChannel(TreeViewItemModel arg)
        {
            if (arg is ChannelTreeModel)
            {
                var channel = arg as ChannelTreeModel;
                var result = _treeService.GetTestPoint(channel.ChannelId);
                if (result.IsSuccess)
                {
                    SelectedTestPoint = result.Value;
                }
                _eventAggregator.GetEvent<SelectedChannelChangedEvent>().Publish(channel.ChannelId);
            }
            else if (arg is CardTreeModel)
            {
                var card = arg as CardTreeModel;
                _eventAggregator.GetEvent<SelectedCardChangedEvent>().Publish(Tuple.Create<string,string>(card.IP,card.SlotNum));
            }
        }
        private void SelectTestPoint(TreeViewItemModel arg)
        {
            if (arg is TestPointTreeModel)
            {
                var testPoint = arg as TestPointTreeModel;
                if(testPoint.IsPaired)
                {
                    var result = _treeService.GetChannel(testPoint.ChannelId.Value);
                    if (result.IsSuccess)
                    {
                        SelectedChannel = result.Value;
                    }
                }
            }
        }

        private void AddTreeModel(TreeViewItemModel treeItem)
        {
            var result = _treeService.AddTreeModel(treeItem).Value;
            if (result is GroupTreeModel)
            {
                groupCollection.Add(result as GroupTreeModel);
            }
        }

        private void DeleteTreeModel(TreeViewItemModel treeItem)
        {
            _treeService.DeleteTreeModel(treeItem);
        }

        private TreeViewItemModel selectedTestPoint;
        public TreeViewItemModel SelectedTestPoint
        {
            get { return selectedTestPoint; }
            set
            {
                if (selectedTestPoint != value)
                {
                    selectedTestPoint = value;
                    OnPropertyChanged("SelectedTestPoint");
                }
            }
        }

        private TreeViewItemModel selectedChannel;
        public TreeViewItemModel SelectedChannel
        {
            get { return selectedChannel; }
            set
            {
                if (selectedChannel != value)
                {
                    selectedChannel = value;
                    OnPropertyChanged("SelectedChannel");
                }
            }
        }

        private string triggerIP;
        public string TriggerIP
        {
            get { return triggerIP; }
            set { SetProperty(ref triggerIP, value); }
        }

        private int triggerPort;
        public int TriggerPort
        {
            get { return triggerPort; }
            set { SetProperty(ref triggerPort, value); }
        }

        public IEnumerable<GroupTreeModel> Groups { get { return groupCollection; } }
        public IEnumerable<PDATreeModel> PDAs { get { return pdaCollection; } }

        public ICommand BindChannelCommand { get; private set; }
        public ICommand AddTreeModelCommand { get; private set; }
        public ICommand DeleteTreeModelCommand { get; private set; }
        public ICommand ConnectingPDACommand { get; private set; }
        public ICommand SaveTreeCommand { get; private set; }
        public DelegateCommand<TreeViewItemModel> SelectTestPointCommand { get; private set; }
        public DelegateCommand<TreeViewItemModel> SelectChannelCommand { get; private set; }

        public InteractionRequest<INotification> NotificationRequest { get; private set; }
        public InteractionRequest<IConfirmation> ConfirmationRequest { get; private set; }
    }
}

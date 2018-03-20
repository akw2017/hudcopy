using AIC.Core.ControlModels;
using AIC.Core.Events;
using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.Core.ExCommand;
using AIC.PDAPage.Models;
using AIC.PDAPage.Views;
using AIC.Resources.Models;
using AIC.ServiceInterface;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using AIC.Core.LMModels;
using AIC.M9600.Common.MasterDB.Generated;
using System.IO;
using System.Windows.Threading;

namespace AIC.PDAPage.ViewModels
{
    partial class PDASystemManageViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IHardwareService _hardwareService;
        private readonly IOrganizationService _organizationService;
        private readonly IConvertToDataBaseFormat _convertToDataBaseFormat;
        private readonly ILoginUserService _loginUserService;
        private readonly ICardProcess _cardProcess;
        private readonly ISignalProcess _signalProcess;
        private readonly IDatabaseComponent _databaseComponent;
        private readonly ILocalConfiguration _localConfiguration;

        public PDASystemManageViewModel(IEventAggregator eventAggregator, IHardwareService hardwareService, IOrganizationService organizationService, IConvertToDataBaseFormat convertToDataBaseFormat, ILoginUserService loginUserService, ICardProcess cardProcess, ISignalProcess signalProcess, IDatabaseComponent databaseComponent, ILocalConfiguration localConfiguration)
        {
            _eventAggregator = eventAggregator;
            _hardwareService = hardwareService;
            _organizationService = organizationService;
            _convertToDataBaseFormat = convertToDataBaseFormat;
            _loginUserService = loginUserService;
            _cardProcess = cardProcess;
            _signalProcess = signalProcess;
            _databaseComponent = databaseComponent;
            _localConfiguration = localConfiguration;

            ServerIPCategory = _databaseComponent.GetServerIPCategory();
            ServerIP = _databaseComponent.MainServerIp;

            InitTree();
            InitHardware();
        }

        #region 私有变量
        private ServerTreeItemViewModel SelectedServerTree;
        private MainCardTreeItemViewModel SelectedMainCardTree;
        private WireMatchingCardTreeItemViewModel SelectedWireMatchingCardTree;
        private WirelessReceiveCardTreeItemViewModel SelectedWirelessReceiveCardTree;
        private TransmissionCardTreeItemViewModel SelectedTransmissionCardTree;
        private SlotTreeItemViewModel SelectedSlotTree;
        private ChannelTreeItemViewModel SelectedChannelTree;

        private MainControlCard CopyMainControlCard;
        private WireMatchingCard CopyWireMatchingCard;
        private IWireSlot CopySlot;
        private IChannel CopyChannel;
        private DivFreInfo CopyDivFre;
        private List<ChannelTreeItemViewModel> ChannelTreeItems;

        //private WireMatchingCard SelectedCard;
        //private IWireSlot SelectedSlot;
        //private IChannel SelectedChannel;
        private OrganizationTreeItemViewModel SelectedOrganizationTree;
        private int SelectedDivFreInfoIndex;
        private bool DivFreDoubleClick = false;

        private string Identifier;

        private string _mainControlCardIP;
        public string MainControlCardIP
        {
            get { return _mainControlCardIP; }
            set
            {
                _mainControlCardIP = value;
                MainControlCardIPEdit = value;
            }
        }
        #endregion

        #region 属性与字段
        private ObservableCollection<OrganizationTreeItemViewModel> _organizationTreeItems;
        public ObservableCollection<OrganizationTreeItemViewModel> OrganizationTreeItems
        {
            get { return _organizationTreeItems; }
            set
            {
                _organizationTreeItems = value;
                OnPropertyChanged("OrganizationTreeItems");
            }
        }
        private ObservableCollection<OrganizationTreeItemViewModel> _recycledTreeItems;
        public ObservableCollection<OrganizationTreeItemViewModel> RecycledTreeItems
        {
            get { return _recycledTreeItems; }
            set
            {
                _recycledTreeItems = value;
                OnPropertyChanged("RecycledTreeItems");
            }
        }
        private ObservableCollection<ServerTreeItemViewModel> _serverTreeItems;
        public ObservableCollection<ServerTreeItemViewModel> ServerTreeItems
        {
            get { return _serverTreeItems; }
            set
            {
                _serverTreeItems = value;
                OnPropertyChanged("ServerTreeItems");
            }
        }
        private ObservableCollection<ContextMenuViewModel> _menuItems;
        public ObservableCollection<ContextMenuViewModel> MenuItems
        {
            get { return _menuItems; }
            set { _menuItems = value; }
        }

        private ObservableCollection<MainControlCard> _maincard;//为了做出自动选中，放在ListView中
        public ObservableCollection<MainControlCard> MainCard
        {
            get { return _maincard; }
            set
            {
                _maincard = value;
                OnPropertyChanged("MainCard");
            }
        }

        private ObservableCollection<WirelessReceiveCard> _wirelessReceiveCard;
        public ObservableCollection<WirelessReceiveCard> WirelessReceiveCard
        {
            get { return _wirelessReceiveCard; }
            set
            {
                _wirelessReceiveCard = value;
                OnPropertyChanged("WirelessReceiveCard");
            }
        }

        private ObservableCollection<ICard> _cards;
        public ObservableCollection<ICard> Cards
        {
            get { return _cards; }
            set
            {
                _cards = value;
                OnPropertyChanged("Cards");
            }
        }

        private ObservableCollection<ISlot> _slots;
        public ObservableCollection<ISlot> Slots
        {
            get { return _slots; }
            set
            {
                _slots = value;
                OnPropertyChanged("Slots");
            }
        }

        private ObservableCollection<IChannel> _channels;
        public ObservableCollection<IChannel> Channels
        {
            get { return _channels; }
            set
            {
                _channels = value;
                OnPropertyChanged("Channels");
            }
        }

        private ICard _selectedCard;
        public ICard SelectedCard
        {
            get { return _selectedCard; }
            set
            {
                _selectedCard = value;
                OnPropertyChanged("SelectedCard");
            }
        }

        private ISlot _selectedSlot;
        public ISlot SelectedSlot
        {
            get { return _selectedSlot; }
            set
            {
                _selectedSlot = value;
                OnPropertyChanged("SelectedSlot");
            }
        }

        private IChannel _selectedChannel;
        public IChannel SelectedChannel
        {
            get { return _selectedChannel; }
            set
            {
                _selectedChannel = value;
                OnPropertyChanged("selectedChannel");
            }
        }

        public bool _isMultiSelected = false;
        public bool IsMultiSelected
        {
            get { return _isMultiSelected; }
            set
            {
                if (_isMultiSelected != value)
                {
                    _isMultiSelected = value;
                    MultiSelected(value);
                    OnPropertyChanged("IsMultiSelected");
                }
            }
        }

        private ViewModelStatus _status = ViewModelStatus.None;
        public ViewModelStatus Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public string waitInfo;
        public string WaitInfo
        {
            get { return waitInfo; }
            set
            {
                waitInfo = value;
                OnPropertyChanged("WaitInfo");
            }
        }

        private string _mainServerIP;
        public string MainServerIP
        {
            get { return _mainServerIP; }
            set
            {
                _mainServerIP = value;
                OnPropertyChanged("MainServerIP");
            }
        }

        private string _serverIP;
        public string ServerIP
        {
            get { return _serverIP; }
            set
            {
                if (_serverIP != value)
                {                  
                    _serverIP = value;
                    InitTree();
                    InitHardware();
                    InitPrivilege();
                    OnPropertyChanged("ServerIP");
                }
            }
        }

        private List<string> _serverIPCategory;
        public List<string> ServerIPCategory
        {
            get { return _serverIPCategory; }
            set
            {
                _serverIPCategory = value;
                OnPropertyChanged("ServerIPCategory");
            }
        }

        private string _mainControlCardIPEdit;
        public string MainControlCardIPEdit
        {
            get { return _mainControlCardIPEdit; }
            set
            {
                _mainControlCardIPEdit = value;
                OnPropertyChanged("MainControlCardIPEdit");
            }
        }

        private string _transmissionCardIdentifier;
        public string TransmissionCardIdentifier
        {
            get { return _transmissionCardIdentifier; }
            set
            {
                _transmissionCardIdentifier = value;
                OnPropertyChanged("TransmissionCardIdentifier");
            }
        }

        private bool _allowDropList = false;//先不让drop
        public bool AllowDropList
        {
            get { return _allowDropList; }
            set
            {
                if (_allowDropList != value)
                {
                    _allowDropList = value;
                    OnPropertyChanged("AllowDropList");
                }
            }
        }

        private bool _allowDropTree = false;//先不让drop
        public bool AllowDropTree
        {
            get { return _allowDropTree; }
            set
            {
                if (_allowDropTree != value)
                {
                    _allowDropTree = value;
                    OnPropertyChanged("AllowDropTree");
                }
            }
        }

        private bool _isShowDebug = false;
        public bool IsShowDebug
        {
            get { return _isShowDebug; }
            set
            {
                if (_isShowDebug != value)
                {
                    _isShowDebug = value;
                    OnPropertyChanged("IsShowDebug");
                }
            }
        }
        #endregion

        #region 命令
        public DelegateCommand<object> addCommand;
        public DelegateCommand<object> AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new DelegateCommand<object>(
                        para => Add(para), para => CanOperate(para)
                        );
                }
                return addCommand;
            }
        }

        public DelegateCommand<object> addRootCommand;
        public DelegateCommand<object> AddRootCommand
        {
            get
            {
                if (addRootCommand == null)
                {
                    addRootCommand = new DelegateCommand<object>(
                        para => AddRoot(para), para => CanOperate(para)
                        );
                }
                return addRootCommand;
            }
        }

        public DelegateCommand<object> sortCommand;
        public DelegateCommand<object> SortCommand
        {
            get
            {
                if (sortCommand == null)
                {
                    sortCommand = new DelegateCommand<object>(
                        para => Sort(para), para => CanOperate(para)
                        );
                }
                return sortCommand;
            }
        }

        public DelegateCommand<object> addOrganizationCommand;
        public DelegateCommand<object> AddOrganizationCommand
        {
            get
            {
                if (addOrganizationCommand == null)
                {
                    addOrganizationCommand = new DelegateCommand<object>(
                        para => AddOrganization(para), para => CanOperate(para)
                        );
                }
                return addOrganizationCommand;
            }
        }

        public DelegateCommand<object> deleteCommand;
        public DelegateCommand<object> DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new DelegateCommand<object>(
                        para => Delete(para), para => CanOperate(para)
                        );
                }
                return deleteCommand;
            }
        }

        public DelegateCommand<object> copyCommand;
        public DelegateCommand<object> CopyCommand
        {
            get
            {
                if (copyCommand == null)
                {
                    copyCommand = new DelegateCommand<object>(
                        para => Copy(para), para => CanOperate(para)
                        );
                }
                return copyCommand;
            }
        }

        public DelegateCommand<object> pasteCommand;
        public DelegateCommand<object> PasteCommand
        {
            get
            {
                if (pasteCommand == null)
                {
                    pasteCommand = new DelegateCommand<object>(
                        para => Paste(para), para => CanOperate(para)
                        );
                }
                return pasteCommand;
            }
        }

        public DelegateCommand<object> pasteRootCommand;
        public DelegateCommand<object> PasteRootCommand
        {
            get
            {
                if (pasteRootCommand == null)
                {
                    pasteRootCommand = new DelegateCommand<object>(
                        para => PasteRoot(para), para => CanOperate(para)
                        );
                }
                return pasteRootCommand;
            }
        }

        public DelegateCommand<object> renameCommand;
        public DelegateCommand<object> RenameCommand
        {
            get
            {
                if (renameCommand == null)
                {
                    renameCommand = new DelegateCommand<object>(
                        para => Rename(para), para => CanOperate(para)
                        );
                }
                return renameCommand;
            }
        }

        public DelegateCommand<object> endRenameCommand;
        public DelegateCommand<object> EndRenameCommand
        {
            get
            {
                if (endRenameCommand == null)
                {
                    endRenameCommand = new DelegateCommand<object>(
                        para => EndRename(para), para => CanOperate(para)
                        );
                }
                return endRenameCommand;
            }
        }

        public DelegateCommand<object> previewMouseRightComamnd;
        public DelegateCommand<object> PreviewMouseRightComamnd
        {
            get
            {
                if (previewMouseRightComamnd == null)
                {
                    previewMouseRightComamnd = new DelegateCommand<object>(
                        para => PreviewMouseRight(para), para => CanOperate(para)
                        );
                }
                return previewMouseRightComamnd;
            }
        }

        public DelegateCommand<object> bindChannelCommand;
        public DelegateCommand<object> BindChannelCommand
        {
            get
            {
                if (bindChannelCommand == null)
                {
                    bindChannelCommand = new DelegateCommand<object>(
                        para => BindChannel(para), para => CanOperate(para)
                        );
                }
                return bindChannelCommand;
            }
        }

        public DelegateCommand<object> selectedTreeChangedComamnd;
        public DelegateCommand<object> SelectedTreeChangedComamnd
        {
            get
            {
                if (selectedTreeChangedComamnd == null)
                {
                    selectedTreeChangedComamnd = new DelegateCommand<object>(
                        para => SelectedTreeChanged(para), para => CanOperate(para)
                        );
                }
                return selectedTreeChangedComamnd;
            }
        }

        public DelegateCommand<object> selectedServerChangedComamnd;
        public DelegateCommand<object> SelectedServerChangedComamnd
        {
            get
            {
                if (selectedServerChangedComamnd == null)
                {
                    selectedServerChangedComamnd = new DelegateCommand<object>(
                        para => SelectedServerChanged(para), para => CanOperate(para)
                        );
                }
                return selectedServerChangedComamnd;
            }
        }

        public DelegateCommand<object> mouseMoveComamnd;
        public DelegateCommand<object> MouseMoveComamnd
        {
            get
            {
                if (mouseMoveComamnd == null)
                {
                    mouseMoveComamnd = new DelegateCommand<object>(
                        para => this.MouseMove(para), para => CanOperate(para)
                        );
                }
                return mouseMoveComamnd;
            }
        }

        public DelegateCommand<object> mouseDownComamnd;
        public DelegateCommand<object> MouseDownComamnd
        {
            get
            {
                if (mouseDownComamnd == null)
                {
                    mouseDownComamnd = new DelegateCommand<object>(
                        para => this.MouseDown(para), para => CanOperate(para)
                        );
                }
                return mouseDownComamnd;
            }
        }

        public DelegateCommand<object> dragEnterCommand;
        public DelegateCommand<object> DragEnterCommand
        {
            get
            {
                if (dragEnterCommand == null)
                {
                    dragEnterCommand = new DelegateCommand<object>(
                        para => DragEnter(para), para => CanOperate(para)
                        );
                }
                return dragEnterCommand;
            }
        }

        public DelegateCommand<object> dragOverCommand;
        public DelegateCommand<object> DragOverCommand
        {
            get
            {
                if (dragOverCommand == null)
                {
                    dragOverCommand = new DelegateCommand<object>(
                        para => DragOver(para), para => CanOperate(para)
                        );
                }
                return dragOverCommand;
            }
        }

        public DelegateCommand<object> dragLeaveCommand;
        public DelegateCommand<object> DragLeaveCommand
        {
            get
            {
                if (dragLeaveCommand == null)
                {
                    dragLeaveCommand = new DelegateCommand<object>(
                        para => DragLeave(para), para => CanOperate(para)
                        );
                }
                return dragLeaveCommand;
            }
        }

        public DelegateCommand<object> dropCommand;
        public DelegateCommand<object> DropCommand
        {
            get
            {
                if (dropCommand == null)
                {
                    dropCommand = new DelegateCommand<object>(
                        para => Drop(para), para => CanOperate(para)
                        );
                }
                return dropCommand;
            }
        }

        public DelegateCommand<object> cardEditCommand;
        public DelegateCommand<object> CardEditCommand
        {
            get
            {
                if (cardEditCommand == null)
                {
                    cardEditCommand = new DelegateCommand<object>(
                        para => CardEdit(para), para => CanOperate(para)
                        );
                }
                return cardEditCommand;
            }
        }

        public DelegateCommand<object> slotEditCommand;
        public DelegateCommand<object> SlotEditCommand
        {
            get
            {
                if (slotEditCommand == null)
                {
                    slotEditCommand = new DelegateCommand<object>(
                        para => SlotEdit(para), para => CanOperate(para)
                        );
                }
                return slotEditCommand;
            }
        }

        public DelegateCommand<object> channelEditCommand;
        public DelegateCommand<object> ChannelEditCommand
        {
            get
            {
                if (channelEditCommand == null)
                {
                    channelEditCommand = new DelegateCommand<object>(
                        para => ChannelEdit(para), para => CanOperate(para)
                        );
                }
                return channelEditCommand;
            }
        }

        public DelegateCommand<object> divFreEditCommand;
        public DelegateCommand<object> DivFreEditCommand
        {
            get
            {
                if (divFreEditCommand == null)
                {
                    divFreEditCommand = new DelegateCommand<object>(
                        para => DivFreEdit(para), para => CanOperate(para)
                        );
                }
                return divFreEditCommand;
            }
        }

        public DelegateCommand<object> cardChangedCommand;
        public DelegateCommand<object> CardChangedCommand
        {
            get
            {
                if (cardChangedCommand == null)
                {
                    cardChangedCommand = new DelegateCommand<object>(
                        para => CardChanged(para), para => CanOperate(para)
                        );
                }
                return cardChangedCommand;
            }
        }

        public DelegateCommand<object> slotChangedComamnd;
        public DelegateCommand<object> SlotChangedComamnd
        {
            get
            {
                if (slotChangedComamnd == null)
                {
                    slotChangedComamnd = new DelegateCommand<object>(
                        para => SlotChanged(para), para => CanOperate(para)
                        );
                }
                return slotChangedComamnd;
            }
        }

        public DelegateCommand<object> channelChangedCommand;
        public DelegateCommand<object> ChannelChangedCommand
        {
            get
            {
                if (channelChangedCommand == null)
                {
                    channelChangedCommand = new DelegateCommand<object>(
                        para => ChannelChanged(para), para => CanOperate(para)
                        );
                }
                return channelChangedCommand;
            }
        }

        public DelegateCommand<object> addCardCommand;
        public DelegateCommand<object> AddCardCommand
        {
            get
            {
                if (addCardCommand == null)
                {
                    addCardCommand = new DelegateCommand<object>(
                        para => AddCard(para), para => CanOperate(para)
                        );
                }
                return addCardCommand;
            }
        }

        public DelegateCommand<object> deleteCardCommand;
        public DelegateCommand<object> DeleteCardCommand
        {
            get
            {
                if (deleteCardCommand == null)
                {
                    deleteCardCommand = new DelegateCommand<object>(
                        para => DeleteCard(para), para => CanOperate(para)
                        );
                }
                return deleteCardCommand;
            }
        }

        public DelegateCommand<object> forceDeleteCardCommand;
        public DelegateCommand<object> ForceDeleteCardCommand
        {
            get
            {
                if (forceDeleteCardCommand == null)
                {
                    forceDeleteCardCommand = new DelegateCommand<object>(
                        para => ForceDeleteCard(para), para => CanOperate(para)
                        );
                }
                return forceDeleteCardCommand;
            }
        }

        public DelegateCommand<object> downLoadCardCommand;
        public DelegateCommand<object> DownLoadCardCommand
        {
            get
            {
                if (downLoadCardCommand == null)
                {
                    downLoadCardCommand = new DelegateCommand<object>(
                        para => DownLoadCard(para), para => CanOperate(para)
                        );
                }
                return downLoadCardCommand;
            }
        }

        private DelegateCommand<object> saveCardCommand;
        public DelegateCommand<object> SaveCardCommand
        {
            get
            {
                if (saveCardCommand == null)
                {
                    saveCardCommand = new DelegateCommand<object>(
                        para => SaveCard(para), para => CanOperate(para)
                        );
                }
                return saveCardCommand;
            }
        }

        public DelegateCommand<object> addTransmissionCardCommand;
        public DelegateCommand<object> AddTransmissionCardCommand
        {
            get
            {
                if (addTransmissionCardCommand == null)
                {
                    addTransmissionCardCommand = new DelegateCommand<object>(
                        para => AddTransmissionCard(para), para => CanOperate(para)
                        );
                }
                return addTransmissionCardCommand;
            }
        }

        public DelegateCommand<object> deleteTransmissionCardCommand;
        public DelegateCommand<object> DeleteTransmissionCardCommand
        {
            get
            {
                if (deleteTransmissionCardCommand == null)
                {
                    deleteTransmissionCardCommand = new DelegateCommand<object>(
                        para => DeleteTransmissionCard(para), para => CanOperate(para)
                        );
                }
                return deleteTransmissionCardCommand;
            }
        }

        public DelegateCommand<object> copyParaCommand;
        public DelegateCommand<object> CopyParaCommand
        {
            get
            {
                if (copyParaCommand == null)
                {
                    copyParaCommand = new DelegateCommand<object>(
                        para => CopyPara(para), para => CanOperate(para)
                        );
                }
                return copyParaCommand;
            }
        }

        public DelegateCommand<object> pasteParaCommand;
        public DelegateCommand<object> PasteParaCommand
        {
            get
            {
                if (pasteParaCommand == null)
                {
                    pasteParaCommand = new DelegateCommand<object>(
                        para => PastePara(para), para => CanOperate(para)
                        );
                }
                return pasteParaCommand;
            }
        }

        public DelegateCommand<object> clearRecycledCommand;
        public DelegateCommand<object> ClearRecycledCommand
        {
            get
            {
                if (clearRecycledCommand == null)
                {
                    clearRecycledCommand = new DelegateCommand<object>(
                        para => ClearRecycled(para), para => CanOperate(para)
                        );
                }
                return clearRecycledCommand;
            }
        }

        public DelegateCommand<object> recoveredItemCommand;
        public DelegateCommand<object> RecoveredItemCommand
        {
            get
            {
                if (recoveredItemCommand == null)
                {
                    recoveredItemCommand = new DelegateCommand<object>(
                        para => RecoveredItem(para), para => CanOperate(para)
                        );
                }
                return recoveredItemCommand;
            }
        }

        public DelegateCommand<object> debugCommand;
        public DelegateCommand<object> DebugCommand
        {
            get
            {
                if (debugCommand == null)
                {
                    debugCommand = new DelegateCommand<object>(
                        para => Debug(para), para => CanOperate(para)
                        );
                }
                return debugCommand;
            }
        }

        public DelegateCommand<object> editDeviceCommand;
        public DelegateCommand<object> EditDeviceCommand
        {
            get
            {
                if (editDeviceCommand == null)
                {
                    editDeviceCommand = new DelegateCommand<object>(
                        para => EditDevice(para), para => CanOperate(para)
                        );
                }
                return editDeviceCommand;
            }
        }
        
        #endregion

        #region 管理树
        private void InitTree()
        {          
            OrganizationTreeItems = new ObservableCollection<OrganizationTreeItemViewModel>(_organizationService.OrganizationTreeItems.Where(p => p.ServerIP == ServerIP));
            RecycledTreeItems = new ObservableCollection<OrganizationTreeItemViewModel>(_organizationService.RecycledTreeItems.Where(p =>p.ServerIP == ServerIP));
            //TreeExpanded();
        }
        private void TreeExpanded()
        {
            foreach (var first in OrganizationTreeItems)
            {
                first.IsExpanded = true;
                foreach (var second in first.Children)
                {
                    second.IsExpanded = true;
                    foreach (var third in second.Children)
                    {
                        third.IsExpanded = true;
                    }
                }
            }
        }
        private void ServerExpanded()
        {
            foreach (var first in ServerTreeItems)
            {
                first.IsExpanded = true;
                foreach (var second in first.Children)
                {
                    second.IsExpanded = true;
                }
            }
        }

        private void Debug(object para)
        {
            OrganizationTreeItemViewModel organizationtree = para as OrganizationTreeItemViewModel;
            if (organizationtree != null)
            {
                TreeNodeDebugInfoWin win = new TreeNodeDebugInfoWin(organizationtree);
                win.ShowDialog();
            }
        }
        #endregion       

        #region 编辑树
        private bool CanOperate(object para)
        {
            if (_localConfiguration.ServerInfoList.Where(p => p.IP == ServerIP).Where(p => p.Permission.Contains("admin") || p.Permission.Contains("管理员")).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void InitPrivilege()
        {
            AddCommand.RaiseCanExecuteChanged();
            AddRootCommand.RaiseCanExecuteChanged();
            SortCommand.RaiseCanExecuteChanged();
            AddOrganizationCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();
            CopyCommand.RaiseCanExecuteChanged();
            PasteCommand.RaiseCanExecuteChanged();
            PasteRootCommand.RaiseCanExecuteChanged();
            RenameCommand.RaiseCanExecuteChanged();
            BindChannelCommand.RaiseCanExecuteChanged();
            CardEditCommand.RaiseCanExecuteChanged();
            SlotEditCommand.RaiseCanExecuteChanged();
            ChannelEditCommand.RaiseCanExecuteChanged();
            DivFreEditCommand.RaiseCanExecuteChanged();
            AddCardCommand.RaiseCanExecuteChanged();
            DeleteCardCommand.RaiseCanExecuteChanged();
            ForceDeleteCardCommand.RaiseCanExecuteChanged();
            DownLoadCardCommand.RaiseCanExecuteChanged();
            AddTransmissionCardCommand.RaiseCanExecuteChanged();
            DeleteTransmissionCardCommand.RaiseCanExecuteChanged();
            ClearRecycledCommand.RaiseCanExecuteChanged();
            RecoveredItemCommand.RaiseCanExecuteChanged();
            DebugCommand.RaiseCanExecuteChanged();
            EditDeviceCommand.RaiseCanExecuteChanged();
        }

        //添加目录
        private async void AddRoot(object para)
        {
            try
            {
                Status = ViewModelStatus.Querying;
                string str = para as string;
                if (str != null)
                {
                    if (str == "-1")//添加根节点
                    {
                        int sort_no = OrganizationTreeItems.Count;
                        if (OrganizationTreeItems.Count > 0)
                        {
                            sort_no = OrganizationTreeItems[OrganizationTreeItems.Count - 1].T_Organization.Sort_No + 1;
                        }
                        OrganizationTreeItemViewModel childtree = new OrganizationTreeItemViewModel(sort_no, ServerIP);

                        //重名检测
                        childtree.Name = _cardProcess.SameNameChecked(_databaseComponent.GetOrganizationData(ServerIP), childtree);

                        Dictionary<string, ICollection<object>> addDic = new Dictionary<string, ICollection<object>>();
                        addDic.Add("T_Organization", new List<object> { childtree.T_Organization });
                        if (await _databaseComponent.Complex(ServerIP, addDic, null, null) == true) //if (await _databaseComponent.Add<T_Organization>(ServerIP, childtree.T_Organization) == true)
                        {
                            OrganizationTreeItems.Add(childtree);
                        }
                        else
                        {
#if XBAP
                            MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                            Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                        }
                        Status = ViewModelStatus.None;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("添加节点", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }
        private async void Add(object para)
        {
            try
            {
                Status = ViewModelStatus.Querying;
                ItemTreeItemViewModel itemtree_parent = para as ItemTreeItemViewModel;
                if (itemtree_parent != null)//添加分频
                {
                    if (itemtree_parent.IsPaired == true)
                    {
                        IChannel i_channel = _cardProcess.GetHardwareChannel(ServerTreeItems, itemtree_parent.T_Item);

                        DivFreTreeItemViewModel childtree = new DivFreTreeItemViewModel(itemtree_parent);

                        //重名检测
                        childtree.Name = _cardProcess.SameNameChecked(_databaseComponent.GetOrganizationData(ServerIP), childtree);

                        if (i_channel is DivFreIChannel)
                        {
                            DivFreInfo divfreinfo = _cardProcess.GetDivFreInfo(i_channel, childtree, itemtree_parent);
                            T1_DivFreInfo t_divfreinfo = _convertToDataBaseFormat.DivFreInfoConvert(divfreinfo, itemtree_parent);

                            Dictionary<string, ICollection<object>> addDic = new Dictionary<string, ICollection<object>>();
                            addDic.Add("T_DivFreInfo", new List<object> { t_divfreinfo });
                            addDic.Add("T_Organization", new List<object> { childtree.T_Organization });

                            if (await _databaseComponent.Complex(ServerIP, addDic, null, null) == true)
                            {
                                //添加分频
                                divfreinfo.T_DivFreInfo = t_divfreinfo;
                                (i_channel as DivFreIChannel).DivFreInfo.Add(divfreinfo);//htzk123
                                childtree.T_DivFreInfo = t_divfreinfo;
                                itemtree_parent.AddChild(childtree);

                                AddDivfreNotify(childtree);
                                i_channel.IsEdited = true;
                            }
                            else
                            {
#if XBAP
                                MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                            }
                            Status = ViewModelStatus.None;
                        }
                        else
                        {
#if XBAP
                            MessageBox.Show((string)Application.Current.Resources["messagePDAManage1"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                            Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["messagePDAManage1"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                        }
                    }
                    else
                    {
#if XBAP
                        MessageBox.Show((string)Application.Current.Resources["messagePDAManage2"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                        Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["messagePDAManage2"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                    }
                    return;
                }

                DeviceTreeItemViewModel device_parent = para as DeviceTreeItemViewModel;
                if (device_parent != null)//添加测点
                {
                    ItemTreeItemViewModel childtree = new ItemTreeItemViewModel(device_parent);

                    //重名检测
                    childtree.Name = _cardProcess.SameNameChecked(_databaseComponent.GetOrganizationData(ServerIP), childtree);

                    Dictionary<string, ICollection<object>> addDic = new Dictionary<string, ICollection<object>>();
                    addDic.Add("T_Organization", new List<object> { childtree.T_Organization });
                    if (await _databaseComponent.Complex(ServerIP, addDic, null, null) == true) //if (await _databaseComponent.Add<T_Organization>(selectedip, childtree.T_Organization) == true)
                    {
                        device_parent.AddChild(childtree);
                        _organizationService.AddItem(childtree);
                    }
                    else
                    {
#if XBAP
                        MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                        Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                    }
                    return;
                }

                OrganizationTreeItemViewModel organization_parent = para as OrganizationTreeItemViewModel;
                if (organization_parent != null)//添加设备
                {
                    //string selectedip = _cardProcess.GetOrganizationServer(organization_parent);
                    DeviceTreeItemViewModel childtree = new DeviceTreeItemViewModel(organization_parent);

                    //重名检测
                    childtree.Name = _cardProcess.SameNameChecked(_databaseComponent.GetOrganizationData(ServerIP), childtree);

                    Dictionary<string, ICollection<object>> addDic = new Dictionary<string, ICollection<object>>();
                    addDic.Add("T_Organization", new List<object> { childtree.T_Organization });
                    if (await _databaseComponent.Complex(ServerIP, addDic, null, null) == true)//if (await _databaseComponent.Add<T_Organization>(selectedip, childtree.T_Organization) == true)
                    {
                        organization_parent.AddChild(childtree);
                    }
                    else
                    {
#if XBAP
                        MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                        Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("添加节点", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }
        //添加根目录
        private async void AddOrganization(object para)
        {
            try
            {
                Status = ViewModelStatus.Querying;
                OrganizationTreeItemViewModel organization_parent = para as OrganizationTreeItemViewModel;
                if (organization_parent != null)//添加组织机构
                {
                    //string selectedip = _cardProcess.GetOrganizationServer(organization_parent);
                    OrganizationTreeItemViewModel childtree = new OrganizationTreeItemViewModel(organization_parent);

                    //重名检测
                    childtree.Name = _cardProcess.SameNameChecked(_databaseComponent.GetOrganizationData(ServerIP), childtree);

                    Dictionary<string, ICollection<object>> addDic = new Dictionary<string, ICollection<object>>();
                    addDic.Add("T_Organization", new List<object> { childtree.T_Organization });
                    if (await _databaseComponent.Complex(ServerIP, addDic, null, null) == true) //if (await _databaseComponent.Add<T_Organization>(selectedip, childtree.T_Organization) == true)
                    {
                        organization_parent.AddChild(childtree);
                    }
                    else
                    {
#if XBAP
                        MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else 
                        Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("添加节点", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        //删除
        private async void Delete(object para)
        {
            try
            {
                Status = ViewModelStatus.Querying;
                //如果删除点不是测点，检测是否有测点被绑定
                if (!(para is ItemTreeItemViewModel))
                {
                    var itemtrees = _cardProcess.GetItems(para as OrganizationTreeItemViewModel).Where(p => p.IsPaired == true);
                    if (itemtrees.Count() > 0)
                    {
#if XBAP
                        MessageBox.Show("该组织机构下有绑定的测点,不可删除!", (string)Application.Current.Resources["delete"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#else
                        Xceed.Wpf.Toolkit.MessageBox.Show("该组织机构下有绑定的测点,不可删除!", (string)Application.Current.Resources["delete"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#endif
                        return;
                    }
                }
#if XBAP
                MessageBoxResult result = MessageBox.Show((string)Application.Current.Resources["messagePDAManage3"], (string)Application.Current.Resources["delete"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#else
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["messagePDAManage3"], (string)Application.Current.Resources["delete"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#endif
                if (result == MessageBoxResult.OK)
                {
                    OrganizationTreeItemViewModel organizationtree = para as OrganizationTreeItemViewModel;
                    if (organizationtree == null)
                    {
                        return;
                    }

                    //string selectedip = _cardProcess.GetOrganizationServer(organizationtree);

                    #region 删除分频
                    DivFreTreeItemViewModel divfretree = para as DivFreTreeItemViewModel;
                    if (divfretree != null)
                    {
                        var itemtree_parent = divfretree.Parent as ItemTreeItemViewModel;
                        IChannel i_channel = _cardProcess.GetHardwareChannel(ServerTreeItems, itemtree_parent.T_Item);
                        if (i_channel != null && i_channel is DivFreIChannel)
                        {
                            var divfreinfo = (i_channel as DivFreIChannel).DivFreInfo;
                            if (divfreinfo != null)
                            {
                                for (int i = 0; i < divfreinfo.Count; i++)
                                {
                                    if (divfreinfo[i].DivFreCode == -1)
                                    {
                                        continue;
                                    }

                                    if (divfreinfo[i].Guid == divfretree.T_Organization.Guid.ToString())
                                    {
                                        var t_divfre = _databaseComponent.GetRootCard(ServerIP).T_DivFreInfo.Where(p => p.Guid == divfretree.T_Organization.Guid).FirstOrDefault();

                                        Dictionary<string, Tuple<string, ICollection<object>>> deleteDic = new Dictionary<string, Tuple<string, ICollection<object>>>();
                                        deleteDic.Add("T_DivFreInfo", new Tuple<string, ICollection<object>>("id", new List<object> { t_divfre.id }));
                                        deleteDic.Add("T_Organization", new Tuple<string, ICollection<object>>("id", new List<object> { divfretree.T_Organization.id }));

                                        if (await _databaseComponent.Complex(ServerIP, null, null, deleteDic) == true)
                                        {
                                            divfreinfo.Remove(divfreinfo[i]);
                                            itemtree_parent.RemoveChild(divfretree);
                                            DeleteDivfreNotify(divfretree);
                                            i_channel.IsEdited = true;
                                        }
                                        else
                                        {
#if XBAP
                                            MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                            Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Dictionary<string, Tuple<string, ICollection<object>>> deleteDic = new Dictionary<string, Tuple<string, ICollection<object>>>();
                            deleteDic.Add("T_Organization", new Tuple<string, ICollection<object>>("id", new List<object> { divfretree.T_Organization.id }));
                            if (divfretree.T_DivFreInfo != null)
                            {
                                deleteDic.Add("T_DivFreInfo", new Tuple<string, ICollection<object>>("id", new List<object> { divfretree.T_DivFreInfo.id }));
                            }

                            if (await _databaseComponent.Complex(ServerIP, null, null, deleteDic) == true)
                            {
                                itemtree_parent.RemoveChild(divfretree);
                                _organizationService.DeleteDivFre(divfretree);
                                _signalProcess.DeleteDivfre(divfretree);
                            }
                            else
                            {
#if XBAP
                                MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                            }
                        }
                        return;
                    }
                    #endregion

                    #region 删除测点
                    ItemTreeItemViewModel itemtree = para as ItemTreeItemViewModel;
                    if (itemtree != null)
                    {
                        Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                        Dictionary<string, Tuple<string, ICollection<object>>> deleteDic = new Dictionary<string, Tuple<string, ICollection<object>>>();
                        List<DivFreInfo> divFreInfolist = new List<DivFreInfo>();
                        ObservableCollection<DivFreInfo> divfreinfo = null;

                        List<object> organizationIds = new List<object>();
                        organizationIds.Add(itemtree.T_Organization.id);
                        //deleteDic.Add("T_Organization", new Tuple<string, ICollection<object>>("id", new List<object> { itemtree.T_Organization.id }));
                        var channeltree = _cardProcess.GetChannel(ServerTreeItems, itemtree.T_Item);

                        if (itemtree.IsPaired == true && channeltree != null)
                        {
                            IChannel i_channel = channeltree.IChannel;
                            //解除绑定通道    
                            channeltree.UnBindTemp();
                            itemtree.UnBindTemp();

                            if (i_channel is DivFreIChannel)
                            {
                                divfreinfo = (i_channel as DivFreIChannel).DivFreInfo;
                                divFreInfolist = _cardProcess.GetDivFreInfoListWithoutTemp(i_channel as DivFreIChannel);
                            }

                            organizationIds.AddRange(itemtree.Children.Select(p => p.T_Organization.id as object).ToList());
                            editDic.Add("T_Item", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { itemtree.T_Item.TempData }));
                            editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { channeltree.IChannel.T_AbstractChannelInfo.TempData }));
                            deleteDic.Add("T_DivFreInfo", new Tuple<string, ICollection<object>>("id", divFreInfolist.Select(p => p.T_DivFreInfo.id as object).ToList()));
                        }
                        deleteDic.Add("T_Organization", new Tuple<string, ICollection<object>>("id", organizationIds));
                        if (await _databaseComponent.Complex(ServerIP, null, editDic, deleteDic) == true)
                        {
                            if (divfreinfo != null)
                            {
                                divFreInfolist.ForEach(p => divfreinfo.Remove(p));
                            }
                            if (itemtree.IsPaired == true && channeltree != null)
                            {
                                UnBindNotify(channeltree, itemtree);
                            }
                            foreach (var divfretree_child in itemtree.Children)
                            {
                                DeleteDivfreNotify(divfretree_child as DivFreTreeItemViewModel);
                            }
                            itemtree.Parent.RemoveChild(itemtree);
                            _organizationService.DeleteItem(itemtree);

                        }
                        else
                        {
#if XBAP
                            MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                            Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                        }
                        return;
                    }
                    #endregion

                    #region 删除组织机构                   
                    //await DeleteNode(OrganizationTreeItems, organizationtree);
                    var organizations = _cardProcess.GetChildOrganizations(organizationtree).Select(p => p as T_Organization).ToList();
                    List<object> ids = organizations.Select(p => p.id as object).ToList();
                    if (await _databaseComponent.Delete<T_Organization>(ServerIP, ids) == true)
                    {
                        if (organizationtree.Parent == null)
                        {
                            OrganizationTreeItems.Remove(organizationtree);
                        }
                        else
                        {
                            organizationtree.Parent.RemoveChild(organizationtree);
                        }
                    }
                    else
                    {
#if XBAP
                        MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                        Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("删除节点", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        //排序
        OrganizationTreeItemViewModel sortorganizationtree;
        private void Sort(object para)
        {
            if (winshow == true)
            {
                return;
            }
            try
            {
                winshow = true;

                OrganizationTreeItemViewModel organizationtree = para as OrganizationTreeItemViewModel;
                sortorganizationtree = organizationtree;
                if (organizationtree != null && organizationtree.Parent == null)
                {
                    ObservableCollection<OrganizationSort> organizationSorts = new ObservableCollection<OrganizationSort>();
                    foreach (var organization in OrganizationTreeItems.OrderBy(p => p.T_Organization.Sort_No))
                    {
                        organizationSorts.Add(new OrganizationSort() { Name = organization.Name, Sort_No = organization.T_Organization.Sort_No });
                    }
                    for (int i = 0; i < organizationSorts.Count; i++)//修正排序号，从0开始
                    {
                        organizationSorts[i].Sort_No = i;
                    }

                    TreeNodeSortWin win = new TreeNodeSortWin(organizationSorts);
                    win.Parachanged += Win_Parachanged;
                    win.ShowDialog();
                }
                else if (organizationtree != null && organizationtree.Parent != null)
                {
                    ObservableCollection<OrganizationSort> organizationSorts = new ObservableCollection<OrganizationSort>();
                    foreach (var organization in organizationtree.Parent.Children.OrderBy(p => p.T_Organization.Sort_No))
                    {
                        organizationSorts.Add(new OrganizationSort() { Name = organization.Name, Sort_No = organization.T_Organization.Sort_No });
                    }
                    for (int i = 0; i < organizationSorts.Count; i++)//修正排序号，从0开始
                    {
                        organizationSorts[i].Sort_No = i;
                    }

                    TreeNodeSortWin win = new TreeNodeSortWin(organizationSorts);
                    win.Parachanged += Win_Parachanged;
                    win.ShowDialog();
                }

            }
            finally
            {
                winshow = false;
            }
        }

        private async void Win_Parachanged(ObservableCollection<OrganizationSort> organizationSorts)
        {
            if (sortorganizationtree == null)
            {
                return;
            }

            var parenttree = sortorganizationtree.Parent;
            try
            {
                Status = ViewModelStatus.Querying;
                Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();

                if (parenttree == null)
                {
                    for (int i = 0; i < organizationSorts.Count; i++)
                    {
                        foreach (var organizationtree in OrganizationTreeItems)
                        {
                            if (organizationtree.Name == organizationSorts[i].Name)
                            {
                                organizationtree.T_Organization.SortTemp(organizationSorts[i].Sort_No);
                                break;
                            }
                        }
                    }
                    editDic.Add("T_Organization", new Tuple<ICollection<string>, ICollection<object>>(new string[] { "Sort_No" }, OrganizationTreeItems.Select(p => p.T_Organization.TempData as object).ToList()));
                    if (await _databaseComponent.Complex(OrganizationTreeItems[0].ServerIP, null, editDic, null) == true)
                    {
                        for (int i = 0; i < organizationSorts.Count; i++)
                        {
                            foreach (var organizationtree in OrganizationTreeItems)
                            {
                                if (organizationtree.Name == organizationSorts[i].Name)
                                {
                                    //organizationtree.T_Organization.Sort_No = organizationSorts[i].Sort_No;
                                    organizationtree.T_Organization.GetTempData();
                                    OrganizationTreeItems.Remove(organizationtree);
                                    OrganizationTreeItems.Insert(i, organizationtree);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
#if XBAP
                        MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                        Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                    }
                }
                else if (parenttree != null)
                {
                    for (int i = 0; i < organizationSorts.Count; i++)
                    {
                        foreach (var organizationtree in parenttree.Children)
                        {
                            if (organizationtree.Name == organizationSorts[i].Name)
                            {
                                organizationtree.T_Organization.SortTemp(organizationSorts[i].Sort_No);
                                break;
                            }
                        }
                    }
                    editDic.Add("T_Organization", new Tuple<ICollection<string>, ICollection<object>>(new string[] { "Sort_No" }, parenttree.Children.Select(p => p.T_Organization.TempData as object).ToList()));
                    if (await _databaseComponent.Complex(sortorganizationtree.ServerIP, null, editDic, null) == true)
                    {
                        for (int i = 0; i < organizationSorts.Count; i++)
                        {
                            foreach (var organizationtree in parenttree.Children)
                            {
                                if (organizationtree.Name == organizationSorts[i].Name)
                                {
                                    //organizationtree.T_Organization.Sort_No = organizationSorts[i].Sort_No;
                                    organizationtree.T_Organization.GetTempData();
                                    parenttree.RemoveChild(organizationtree);
                                    parenttree.InsertChild(i, organizationtree);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
#if XBAP
                        MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                        Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                    }
                }

            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("测点排序", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }


        OrganizationTreeItemViewModel copyorganizationtree;
        private void Copy(object para)
        {
            copyorganizationtree = para as OrganizationTreeItemViewModel;
        }

        private async void PasteRoot(object para)
        {
            if (copyorganizationtree == null)
            {
#if XBAP
                MessageBox.Show("请再粘贴之前进行复制", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请再粘贴之前进行复制", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }

            if (copyorganizationtree is DivFreTreeItemViewModel)
            {
#if XBAP
                MessageBox.Show("请选择测点进行粘贴", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请选择测点进行粘贴", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }

            if (copyorganizationtree is ItemTreeItemViewModel)
            {
#if XBAP
                MessageBox.Show("请选择设备进行粘贴", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请选择设备进行粘贴", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }

            if (copyorganizationtree is DeviceTreeItemViewModel)
            {
#if XBAP
                MessageBox.Show("请选择组织进行粘贴", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请选择组织进行粘贴", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }

            try
            {
                Status = ViewModelStatus.Querying;
                string str = para as string;
                if (str != null)
                {
                    if (str == "-1")//添加根节点
                    {
                        int sort_no = OrganizationTreeItems.Count;
                        if (OrganizationTreeItems.Count > 0)
                        {
                            sort_no = OrganizationTreeItems[OrganizationTreeItems.Count - 1].T_Organization.Sort_No + 1;
                        }

                        var organizationtrees = _cardProcess.GetCopyOrganizations(copyorganizationtree, null);
                        copyorganizationtree.T_Organization.TempData.Sort_No = sort_no;
                        bool isnew = true;
                        copyorganizationtree.T_Organization.TempData.Name = _cardProcess.SameNameChecked(_databaseComponent.GetOrganizationData(copyorganizationtree.ServerIP), copyorganizationtree, isnew);

                        Dictionary<string, ICollection<object>> addDic = new Dictionary<string, ICollection<object>>();
                        addDic.Add("T_Organization", organizationtrees.Select(p => p.T_Organization.TempData as object).ToList());

                        if (await _databaseComponent.Complex(copyorganizationtree.ServerIP, addDic, null, null) == true)
                        {
                            GetSubOrganization(OrganizationTreeItems, organizationtrees.Select(p => p.T_Organization.TempData), copyorganizationtree.ServerIP);
                            organizationtrees.ForEach(p => p.T_Organization.DeleteTemp());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("复制节点", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }
        private async void Paste(object para)
        {
            if (copyorganizationtree == null)
            {
#if XBAP
                MessageBox.Show("请再粘贴之前进行复制", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请再粘贴之前进行复制", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }

            if (copyorganizationtree is DivFreTreeItemViewModel && !(para is ItemTreeItemViewModel))
            {
#if XBAP
                MessageBox.Show("请选择测点进行粘贴", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请选择测点进行粘贴", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }

            if (copyorganizationtree is ItemTreeItemViewModel && !(para is DeviceTreeItemViewModel))
            {
#if XBAP
                MessageBox.Show("请选择设备进行粘贴", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请选择设备进行粘贴", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }

            if (copyorganizationtree is DeviceTreeItemViewModel && !(para is OrganizationTreeItemViewModel))
            {
#if XBAP
                MessageBox.Show("请选择组织进行粘贴", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请选择组织进行粘贴", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }

            if (copyorganizationtree is OrganizationTreeItemViewModel && !(para is OrganizationTreeItemViewModel))
            {
#if XBAP
                MessageBox.Show("请选择组织进行粘贴", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请选择组织进行粘贴", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }

            try
            {
                Status = ViewModelStatus.Querying;
                OrganizationTreeItemViewModel organizationtree = para as OrganizationTreeItemViewModel;
                if (organizationtree != null)
                {

                    int sort_no = organizationtree.Children.Count;
                    if (organizationtree.Children.Count > 0)
                    {
                        sort_no = organizationtree.Children[organizationtree.Children.Count - 1].T_Organization.Sort_No + 1;
                    }

                    var organizationtrees = _cardProcess.GetCopyOrganizations(copyorganizationtree, organizationtree);
                    copyorganizationtree.T_Organization.TempData.Sort_No = sort_no;
                    bool isnew = (organizationtree == copyorganizationtree.Parent);
                    copyorganizationtree.T_Organization.TempData.Name = _cardProcess.SameNameChecked(_databaseComponent.GetOrganizationData(organizationtree.ServerIP), copyorganizationtree, isnew);

                    Dictionary<string, ICollection<object>> addDic = new Dictionary<string, ICollection<object>>();
                    addDic.Add("T_Organization", organizationtrees.Select(p => p.T_Organization.TempData as object).ToList());

                    if (await _databaseComponent.Complex(organizationtree.ServerIP, addDic, null, null) == true)
                    {
                        GetSubOrganization(organizationtree, organizationtrees.Select(p => p.T_Organization.TempData));
                        organizationtrees.ForEach(p => p.T_Organization.DeleteTemp());
                    }
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("复制节点", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        private void GetSubOrganization(OrganizationTreeItemViewModel parent_organization, IEnumerable<T1_Organization> organizations)
        {
            if (parent_organization == null)
            {
                return;
            }
            var childs = from p in organizations where p.Parent_Guid == parent_organization.T_Organization.Guid && p.Is_Disabled == false orderby p.Sort_No select p;
            foreach (var child in childs)
            {
                if (child.NodeType == 0 && child.Is_Disabled == false)
                {
                    OrganizationTreeItemViewModel organization = new OrganizationTreeItemViewModel(child, parent_organization);
                    parent_organization.AddChild(organization);
                    GetSubOrganization(organization, organizations);
                }
                else if (child.NodeType == 1 && child.Is_Disabled == false)
                {
                    DeviceTreeItemViewModel organization = new DeviceTreeItemViewModel(child, parent_organization);
                    parent_organization.AddChild(organization);
                    GetSubOrganization(organization, organizations);
                }
                else if (child.NodeType == 2 && child.Is_Disabled == false)
                {
                    ItemTreeItemViewModel organization = new ItemTreeItemViewModel(child, parent_organization as DeviceTreeItemViewModel);
                    parent_organization.AddChild(organization);
                    GetSubOrganization(organization, organizations);
                }
            }
        }

        private void GetSubOrganization(IList<OrganizationTreeItemViewModel> root_organizations, IEnumerable<T1_Organization> organizations, string serverIP)
        {
            if (root_organizations == null)
            {
                return;
            }
            var childs = from p in organizations where p.Parent_Guid == null && p.Is_Disabled == false orderby p.Sort_No select p;
            foreach (var child in childs)
            {
                if (child.NodeType == 0 && child.Is_Disabled == false)
                {
                    OrganizationTreeItemViewModel organization = new OrganizationTreeItemViewModel(child, serverIP);
                    root_organizations.Add(organization);
                    GetSubOrganization(organization, organizations);
                }
            }
        }


        OrganizationTreeItemViewModel editorganization;
        string editorganization_oldname;

        //重命名
        private void Rename(object para)
        {
            OrganizationTreeItemViewModel organizationtree = para as OrganizationTreeItemViewModel;
            organizationtree.EditMode = true;
            editorganization = organizationtree;
            editorganization_oldname = organizationtree.Name;
        }

        private async void EndRename(object para)
        {
            if (editorganization.EditMode == false)
            {
                return;
            }

            string cmd = para as string;
            if (cmd != null)
            {
                if (cmd == "Esc")//取消重命名
                {
                    editorganization.Name = editorganization_oldname;
                    editorganization.EditMode = false;
                    return;
                }
                editorganization.EditMode = false;
            }

            try
            {
                Status = ViewModelStatus.Querying;
                //string selectedip = _cardProcess.GetOrganizationServer(editorganization);

                //重名检测
                if (_cardProcess.SameNameChecked(_databaseComponent.GetOrganizationData(ServerIP), editorganization) != editorganization.Name)
                {
#if XBAP
                    MessageBox.Show("名称重复,请重新重命名!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                    Xceed.Wpf.Toolkit.MessageBox.Show("名称重复,请重新重命名!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                    editorganization.Name = editorganization_oldname;
                    return;
                }

                DivFreTreeItemViewModel divfretree = editorganization as DivFreTreeItemViewModel;
                if (divfretree != null)
                {
                    var parent_item = divfretree.Parent as ItemTreeItemViewModel;
                    IChannel i_channel = _cardProcess.GetHardwareChannel(ServerTreeItems, parent_item.T_Item);
                    foreach (var divfreinfo in (i_channel as DivFreIChannel).DivFreInfo)
                    {
                        if (divfreinfo.DivFreCode == -1)
                        {
                            continue;
                        }
                        //修改为按创建时间来匹配                    
                        if (divfreinfo.Guid == divfretree.T_Organization.Guid.ToString())
                        {
                            //LM_DivFreInfo t_divfreinfo = _convertToDataBaseFormat.DivFreInfoConvert(divfreinfo, parent_item);
                            divfretree.T_DivFreInfo.Name = divfretree.Name;

                            Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                            editDic.Add("T_DivFreInfo", new Tuple<ICollection<string>, ICollection<object>>(new string[] { "Name" }, new List<object> { divfretree.T_DivFreInfo }));
                            editDic.Add("T_Organization", new Tuple<ICollection<string>, ICollection<object>>(new string[] { "Name" }, new List<object> { divfretree.T_Organization }));

                            if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                            {
                                divfreinfo.Name = divfretree.Name;
                            }
                            else
                            {
                                //数据回滚
                                divfretree.T_DivFreInfo.Name = editorganization_oldname;
                                divfretree.Name = editorganization_oldname;
#if XBAP
                                MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                            }
                            break;
                        }
                    }
                    return;
                }

                ItemTreeItemViewModel itemtree = editorganization as ItemTreeItemViewModel;
                if (itemtree != null)
                {
                    if (itemtree.IsPaired == true)
                    {
                        IChannel i_channel = _cardProcess.GetHardwareChannel(ServerTreeItems, itemtree.T_Item);
                        var abstractChannelInfo = i_channel.T_AbstractChannelInfo;//_databaseComponent.T_RootCard[selectedip].T_AbstractChannelInfo.Where(p => p.T_Item_Guid == itemtree.T_Item.Guid).FirstOrDefault();
                        itemtree.T_Item.Name = itemtree.Name;
                        abstractChannelInfo.T_Item_Name = itemtree.Name;

                        Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                        editDic.Add("T_Item", new Tuple<ICollection<string>, ICollection<object>>(new string[] { "Name" }, new List<object> { itemtree.T_Item }));
                        editDic.Add("T_Organization", new Tuple<ICollection<string>, ICollection<object>>(new string[] { "Name" }, new List<object> { itemtree.T_Organization }));
                        editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(new string[] { "Name" }, new List<object> { abstractChannelInfo }));

                        if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                        {
                            i_channel.T_Item_Name = itemtree.Name;
                        }
                        else
                        {
                            //数据回滚
                            itemtree.T_Item.Name = editorganization_oldname;
                            itemtree.Name = editorganization_oldname;
                            abstractChannelInfo.T_Item_Name = editorganization_oldname;

#if XBAP
                            MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                            Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                        }

                        return;
                    }
                }

                DeviceTreeItemViewModel device = editorganization as DeviceTreeItemViewModel;
                if (device != null)
                {
                    if (device.Children != null && device.Children.Count > 0)
                    {
                        Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();

                        editDic.Add("T_Organization", new Tuple<ICollection<string>, ICollection<object>>(new string[] { "Name" }, new List<object> { device.T_Organization }));

                        List<IChannel> i_channels = new List<IChannel>();
                        foreach (var item_child in device.Children.Where(p => (p as ItemTreeItemViewModel).IsPaired == true).Select(p => p as ItemTreeItemViewModel))
                        {
                            IChannel i_channel = _cardProcess.GetHardwareChannel(ServerTreeItems, item_child.T_Item);
                            var abstractChannelInfo = i_channel.T_AbstractChannelInfo;//_databaseComponent.T_RootCard[selectedip].T_AbstractChannelInfo.Where(p => p.T_Item_Guid == item_child.T_Item.Guid).FirstOrDefault();
                            abstractChannelInfo.T_Device_Name = device.Name;
                            i_channels.Add(i_channel);
                        }

                        if (i_channels.Count > 0)
                        {
                            editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(new string[] { "T_Device_Name" }, i_channels.Select(p => p.T_AbstractChannelInfo as object).ToList()));
                        }

                        if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                        {
                            if (i_channels.Count > 0)
                            {
                                i_channels.ForEach(p => p.T_Device_Name = device.Name);
                            }
                        }
                        else
                        {
                            //数据回滚                       
                            device.Name = editorganization_oldname;
                            if (i_channels.Count > 0)
                            {
                                i_channels.ForEach(p => p.T_AbstractChannelInfo.T_Device_Name = editorganization_oldname);
                            }
#if XBAP
                            MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                            Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                        }
                        return;
                    }
                }

                OrganizationTreeItemViewModel organizationtree = editorganization;
                if (organizationtree != null)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_Organization", new Tuple<ICollection<string>, ICollection<object>>(new string[] { "Name" }, new List<object> { organizationtree.T_Organization }));

                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {

                    }
                    else
                    {
                        //数据回滚                       
                        organizationtree.Name = editorganization_oldname;
#if XBAP
                        MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                        Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                    }
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("重命名节点", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        private void PreviewMouseRight(object para)
        {
            RoutedEventArgs e = ((ExCommandParameter)para).EventArgs as RoutedEventArgs;
            var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;

            if (treeViewItem == null) return;
            treeViewItem.Focus();
            e.Handled = true;
        }

        private static DependencyObject VisualUpwardSearch<M>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(M))
            {
                if (source is Visual || source is Visual3D)
                    source = VisualTreeHelper.GetParent(source);
                else
                    source = LogicalTreeHelper.GetParent(source);
            }
            return source;
        }

        //编辑设备
        private void EditDevice(object para)
        {
            var devicetree = para as DeviceTreeItemViewModel;
            if (devicetree != null && devicetree.T_Organization != null)
            {
                if (winshow == true)
                {
                    return;
                }
                try
                {
                    winshow = true;
                    DeviceEditWin win = new DeviceEditWin(devicetree.T_Organization);
                    win.Parachanged += Win_Parachanged;
                    win.ShowDialog();
                }
                finally
                {
                    winshow = false;
                }
            }
        }

        private async void Win_Parachanged(T_Organization organization, string remarks)
        {
            try
            {
                Status = ViewModelStatus.Querying;
                string oldremarks = organization.Remarks;
                organization.Remarks = remarks;
                Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                editDic.Add("T_Organization", new Tuple<ICollection<string>, ICollection<object>>(new string[] { "Remarks" }, new List<object> { organization }));
                if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                {

                }
                else
                {
                    organization.Remarks = oldremarks;
#if XBAP
                    MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                    Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("编辑设备", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }
        #endregion

        #region 拖拽,先禁用，启用需重新检查

        private int mousetick = 0;
        private object oleparameter;
        private void MouseDown(object para)
        {

            object sender = ((ExCommandParameter)para).Sender as object;
            MouseEventArgs e = ((ExCommandParameter)para).EventArgs as MouseEventArgs;
            object parameter = ((ExCommandParameter)para).Parameter as object;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (oleparameter != parameter)
                {
                    mousetick = 0;
                }
                else
                {
                    mousetick++;
                }

                oleparameter = parameter;
            }
            else
            {
                mousetick = 0;
            }
        }

        private void MouseMove(object para)
        {
            if (mousetick <= 5)
            {
                return;
            }
            object sender = ((ExCommandParameter)para).Sender as object;
            MouseEventArgs e = ((ExCommandParameter)para).EventArgs as MouseEventArgs;
            object parameter = ((ExCommandParameter)para).Parameter as object;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                TreeView tree = sender as TreeView;
                OrganizationTreeItemViewModel sourceStuff = parameter as OrganizationTreeItemViewModel;
                if ((sourceStuff != null) && (sourceStuff.Parent != null))//不允许根节点拖拽
                {
                    DragDropEffects finalDropEffect = DragDrop.DoDragDrop(tree, sourceStuff, DragDropEffects.Move);
                    if (finalDropEffect == DragDropEffects.Move)
                    {

                    }
                }
            }
        }

        private void DragEnter(object para)
        {
            ;
        }

        private void DragOver(object para)
        {
            if (mousetick <= 20)
            {
                return;
            }
            object sender = ((ExCommandParameter)para).Sender as object;
            DragEventArgs e = ((ExCommandParameter)para).EventArgs as DragEventArgs;
            object parameter = ((ExCommandParameter)para).Parameter as object;
            //允许拖拽的范围
            TreeViewItem container = GetNearestContainer(e.OriginalSource as UIElement);


            if (container != null)
            {
                OrganizationTreeItemViewModel sourceStuff = parameter as OrganizationTreeItemViewModel;
                OrganizationTreeItemViewModel targetStuff = (OrganizationTreeItemViewModel)container.Header;
                Type targetType = targetStuff.GetType();
                Type sourceType = sourceStuff.GetType();
                //目标是设备,选中是测点，可以移到
                if (targetType.Name == "DeviceTreeItemViewModel" && sourceType.Name == "ItemTreeItemViewModel")
                {
                    e.Effects = DragDropEffects.Copy;
                    return;
                }
                //目标是组织,选中是设备，可以移到
                else if (targetType.Name == "OrganizationTreeItemViewModel" && sourceType.Name == "DeviceTreeItemViewModel")
                {
                    e.Effects = DragDropEffects.Copy;
                    return;
                }
                //目标是组织,选中是组织，可以移到
                else if (targetType.Name == "OrganizationTreeItemViewModel" && sourceType.Name == "OrganizationTreeItemViewModel")
                {
                    e.Effects = DragDropEffects.Copy;
                    return;
                }
            }

            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        private void DragLeave(object para)
        {
            ;
        }

        private async void Drop(object para)
        {
            if (mousetick <= 20)
            {
                return;
            }
            object sender = ((ExCommandParameter)para).Sender as object;
            DragEventArgs e = ((ExCommandParameter)para).EventArgs as DragEventArgs;
            object parameter = ((ExCommandParameter)para).Parameter as object;

            TreeViewItem container = GetNearestContainer(e.OriginalSource as UIElement);
            if (container != null)
            {
                OrganizationTreeItemViewModel sourceStuff = parameter as OrganizationTreeItemViewModel;
                OrganizationTreeItemViewModel targetStuff = (OrganizationTreeItemViewModel)container.Header;
                if (sourceStuff == targetStuff)//避免自己删除自己
                {
                    return;
                }
                if ((sourceStuff != null) && (targetStuff != null))
                {
                    //更新父节点信息
                    Organization organization = new Organization();
                    organization.Name = sourceStuff.T_Organization.Name;
                    organization.Code = sourceStuff.T_Organization.Code;
                    organization.Guid = sourceStuff.T_Organization.Guid.ToString();
                    organization.Level = sourceStuff.T_Organization.Level;
                    organization.Create_Time = sourceStuff.T_Organization.Create_Time.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                    organization.Modify_Time = sourceStuff.T_Organization.Modify_Time.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                    organization.Parent_Code = targetStuff.T_Organization.Code;
                    organization.Parent_Guid = targetStuff.T_Organization.Guid.ToString();
                    organization.Parent_Level = targetStuff.T_Organization.Level;

                    Status = ViewModelStatus.Querying;
                    await Task.Run(() =>
                    {
                        //更新组织机构                       
                        var T_Organization = _convertToDataBaseFormat.OrganizationConvert(sourceStuff, targetStuff);

                        System.Threading.Thread.Sleep(1000);
                    });
                    Status = ViewModelStatus.None;

                    sourceStuff.T_Organization.Parent_Guid = targetStuff.T_Organization.Guid;
                    sourceStuff.T_Organization.Parent_Code = targetStuff.T_Organization.Code;
                    sourceStuff.T_Organization.Parent_Level = targetStuff.T_Organization.Level;

                    //更新节点的排序号                        
                    int sort_no = targetStuff.Children.Count;
                    if (targetStuff.Children.Count > 0)
                    {
                        sort_no = targetStuff.Children[targetStuff.Children.Count - 1].T_Organization.Sort_No + 1;
                    }
                    sourceStuff.T_Organization.Sort_No = sort_no;

                    //如果测点已经绑定
                    if (sourceStuff is ItemTreeItemViewModel)
                    {
                        ItemTreeItemViewModel itemtree = sourceStuff as ItemTreeItemViewModel;
                        if (itemtree.IsPaired == true)
                        {
                            IChannel i_channel = _cardProcess.GetHardwareChannel(ServerTreeItems, itemtree.T_Item);
                            var channeltree = _cardProcess.GetChannel(ServerTreeItems, itemtree.T_Item);

                            Status = ViewModelStatus.Querying;
                            await Task.Run(() =>
                            {
                                //找到綁定的通道，更新数据库硬件                        
                                T1_AbstractChannelInfo t_channel = _convertToDataBaseFormat.AbstractChannelInfoConvert(i_channel, itemtree.T_Item.IP, itemtree.T_Item.CardNum.Value, itemtree.T_Item.SlotNum.Value, organization);

                                //找到组织机构，更新数据库组织结构
                                T1_Item t_item = _convertToDataBaseFormat.ItemConvert(itemtree, organization);

                                System.Threading.Thread.Sleep(1000);
                            });
                            Status = ViewModelStatus.None;

                            //htzk123Organization,over
                            i_channel.Organization[1].Parent_Guid = targetStuff.T_Organization.Guid.ToString();
                            i_channel.Organization[1].Parent_Code = targetStuff.T_Organization.Code;
                            i_channel.Organization[1].Parent_Level = targetStuff.T_Organization.Level;
                            i_channel.T_Device_Guid = targetStuff.T_Organization.Guid.ToString();
                            i_channel.T_Device_Code = targetStuff.T_Organization.Code;
                            i_channel.T_Device_Name = targetStuff.T_Organization.Name;
                            channeltree.IChannel.T_Device_Guid = targetStuff.T_Organization.Guid.ToString();
                            channeltree.IChannel.T_Device_Code = targetStuff.T_Organization.Code;
                            channeltree.IChannel.T_Device_Name = targetStuff.T_Organization.Name;
                        }
                    }

                    sourceStuff.Parent.Children.Remove(sourceStuff);
                    targetStuff.Children.Add(sourceStuff);
                    e.Effects = DragDropEffects.Move;
                    e.Handled = true;
                }
            }
        }

        private TreeViewItem GetNearestContainer(UIElement element)
        {
            // Walk up the element tree to the nearest tree view itemtree.
            TreeViewItem container = element as TreeViewItem;
            while ((container == null) && (element != null))
            {
                element = VisualTreeHelper.GetParent(element) as UIElement;
                container = element as TreeViewItem;

            }

            return container;
        }
        #endregion

        #region 数据绑定

        private async void BindChannel(object para)
        {
            try
            {
                Status = ViewModelStatus.Querying;
                ChannelTreeItemViewModel channeltree = para as ChannelTreeItemViewModel;
                if (channeltree != null && channeltree.IsPaired == true)//如果已经绑定
                {
                    //TreeProcess.AutoServerTreeSelected(OrganizationTreeItems, channeltree); //自动选中节点 
                    TreeProcess.AutoServerTreeSelected(_organizationService.ItemTreeItems.Where(p => p.IsPaired == true).ToList(), channeltree);
                }
                ItemTreeItemViewModel itemtree = SelectedOrganizationTree as ItemTreeItemViewModel;
                if (channeltree != null && itemtree != null)
                {
                    if (itemtree.Error != null)
                    {
                        string str;
                        switch (itemtree.Error)
                        {
                            case "1": str = "测点有重名,不能进行绑定!!!"; break;
                            default: str = "测点有异常,不能进行绑定!!!"; break;
                        }
#if XBAP
                        MessageBox.Show(str, "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                        Xceed.Wpf.Toolkit.MessageBox.Show(str, "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                        return;
                    }

                    if (itemtree.IsPaired == false && (channeltree.IsPaired == false))//绑定
                    {
                        ItemTreeItemViewModel recycleditemtree = _cardProcess.GetRecycledItem(channeltree.MainControlCardIP, channeltree, RecycledTreeItems[0]); //从回收站找测点
                        if (recycleditemtree != null)
                        {
                            await BindFromRecycled(channeltree, itemtree, recycleditemtree);
                        }
                        else
                        {
                            await Bind(channeltree, itemtree);
                        }
                    }
                    else if (itemtree.IsPaired == true && channeltree.IsPaired == true)//解绑
                    {
                        if (itemtree.T_Item.Guid.ToString() == channeltree.IChannel.T_Item_Guid)
                        {
#if XBAP
                            MessageBoxResult result = MessageBox.Show((string)Application.Current.Resources["messagePDAManage4"], (string)Application.Current.Resources["unbind"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#else
                            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["messagePDAManage4"], (string)Application.Current.Resources["unbind"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#endif
                            if (result == MessageBoxResult.OK)
                            {
                                await UnBind(channeltree, itemtree);
                            }
                        }
                        else
                        {
#if XBAP
                            MessageBox.Show("信息不匹配,请重新选定通道！！！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                            Xceed.Wpf.Toolkit.MessageBox.Show("信息不匹配,请重新选定通道！！！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                        }
                    }
                    else if (itemtree.IsPaired == true && channeltree.IsPaired == false)//测点已经绑定
                    {
#if XBAP
                        MessageBox.Show((string)Application.Current.Resources["messagePDAManage5"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                        Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["messagePDAManage5"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                    }
                    else if (itemtree.IsPaired == false && channeltree.IsPaired == true)//通道已经绑定
                    {
#if XBAP
                        MessageBoxResult result = MessageBox.Show((string)Application.Current.Resources["messagePDAManage4"], "强制解绑", MessageBoxButton.OKCancel, MessageBoxImage.Question);
#else
                        MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["messagePDAManage4"], "强制解绑", MessageBoxButton.OKCancel, MessageBoxImage.Question);
#endif
                        if (result == MessageBoxResult.OK)
                        {
                            await ForceUnBind(channeltree);
                        }
                    }
                }
                else if (channeltree != null && channeltree.IsPaired == true)//强制解除绑定
                {
#if XBAP
                    MessageBoxResult result = MessageBox.Show((string)Application.Current.Resources["messagePDAManage4"], "强制解绑", MessageBoxButton.OKCancel, MessageBoxImage.Question);
#else
                    MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["messagePDAManage4"], "强制解绑", MessageBoxButton.OKCancel, MessageBoxImage.Question);
#endif
                    if (result == MessageBoxResult.OK)
                    {
                        await ForceUnBind(channeltree);
                    }
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("测点绑定", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        private async Task Bind(ChannelTreeItemViewModel channeltree, ItemTreeItemViewModel itemtree)
        {
            var organizations = _cardProcess.GetParentOrganizations(itemtree);//htzk123Organization,over 
            //绑定通道     
            channeltree.BindTemp(itemtree, organizations, itemtree.Parent as DeviceTreeItemViewModel);
            itemtree.BindTemp(channeltree, channeltree.MainControlCardIP, channeltree.SlaveIdentifier, channeltree.ServerIP);

            Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
            Dictionary<string, ICollection<object>> addDic = new Dictionary<string, ICollection<object>>();
            addDic.Add("T_Item", new List<object> { itemtree.T_Item.TempData });
            editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { channeltree.IChannel.T_AbstractChannelInfo.TempData }));
            if (await _databaseComponent.Complex(channeltree.ServerIP, addDic, editDic, null) == true)
            {
                BindNotify(channeltree, organizations, itemtree, null);
                channeltree.IChannel.IsEdited = true;
            }
            else
            {
#if XBAP
                MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
            }
        }

        private async Task BindFromRecycled(ChannelTreeItemViewModel channeltree, ItemTreeItemViewModel itemtree, ItemTreeItemViewModel recycleditemtree)
        {
            var organizations = _cardProcess.GetParentOrganizations(itemtree);//htzk123Organization,over 
            Organization organization = _cardProcess.GetNewOrganization(itemtree, recycleditemtree);

            if (organizations != null && organizations.Count > 0)
            {
                organizations[0] = organization;
            }

            channeltree.BindTemp(recycleditemtree, organizations, itemtree.Parent as DeviceTreeItemViewModel);
            recycleditemtree.BindTemp(channeltree, recycleditemtree.T_Item.IP, recycleditemtree.T_Item.Identifier, ServerIP, itemtree.T_Organization);
            itemtree.T_Organization.UpdateTemp(organization);

            Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
            editDic.Add("T_Item", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { recycleditemtree.T_Item.TempData }));
            editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { channeltree.IChannel.T_AbstractChannelInfo.TempData }));
            editDic.Add("T_Organization", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { itemtree.T_Organization.TempData }));

            if (await _databaseComponent.Complex(channeltree.ServerIP, null, editDic, null) == true)
            {
                BindNotify(channeltree, organizations, itemtree, recycleditemtree);
                channeltree.IChannel.IsEdited = true;
            }
            else
            {
#if XBAP
                MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
            }
        }

        private async Task UnBind(ChannelTreeItemViewModel channeltree, ItemTreeItemViewModel itemtree)
        {
            IChannel i_channel = channeltree.IChannel;

            Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
            Dictionary<string, Tuple<string, ICollection<object>>> deleteDic = new Dictionary<string, Tuple<string, ICollection<object>>>();
            List<DivFreInfo> divFreInfolist = new List<DivFreInfo>();

            //解除绑定通道    
            channeltree.UnBindTemp();
            itemtree.UnBindTemp();

            ObservableCollection<DivFreInfo> divfreinfo = null;
            if (i_channel is DivFreIChannel)
            {
                divfreinfo = (i_channel as DivFreIChannel).DivFreInfo;
                divFreInfolist = _cardProcess.GetDivFreInfoListWithoutTemp(i_channel as DivFreIChannel);
            }

            deleteDic.Add("T_DivFreInfo", new Tuple<string, ICollection<object>>("id", divFreInfolist.Select(p => p.T_DivFreInfo.id as object).ToList()));
            editDic.Add("T_Item", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { itemtree.T_Item.TempData }));
            editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { channeltree.IChannel.T_AbstractChannelInfo.TempData }));
            editDic.Add("T_Organization", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { itemtree.T_Organization.TempData }));

            if (await _databaseComponent.Complex(channeltree.ServerIP, null, editDic, deleteDic) == true)
            {
                foreach (var divfretree_child in itemtree.Children)
                {
                    DeleteDivfreNotify(divfretree_child as DivFreTreeItemViewModel);
                }
                if (divfreinfo != null)
                {
                    divFreInfolist.ForEach(p => divfreinfo.Remove(p));
                }

                UnBindNotify(channeltree, itemtree);
                i_channel.IsEdited = true;
            }
            else
            {
#if XBAP
                MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
            }
        }

        private async Task ForceUnBind(ChannelTreeItemViewModel channeltree)
        {
            IChannel i_channel = channeltree.IChannel;

            Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
            Dictionary<string, Tuple<string, ICollection<object>>> deleteDic = new Dictionary<string, Tuple<string, ICollection<object>>>();
            List<DivFreInfo> divFreInfolist = new List<DivFreInfo>();

            //解除绑定通道    
            channeltree.UnBindTemp();

            ObservableCollection<DivFreInfo> divfreinfo = null;
            if (i_channel is DivFreIChannel)
            {
                divfreinfo = (i_channel as DivFreIChannel).DivFreInfo;
                divFreInfolist = _cardProcess.GetDivFreInfoListWithoutTemp(i_channel as DivFreIChannel);
            }

            deleteDic.Add("T_DivFreInfo", new Tuple<string, ICollection<object>>("id", divFreInfolist.Select(p => p.T_DivFreInfo.id as object).ToList()));
            editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { channeltree.IChannel.T_AbstractChannelInfo.TempData }));

            if (await _databaseComponent.Complex(channeltree.ServerIP, null, editDic, deleteDic) == true)
            {
                if (divfreinfo != null)
                {
                    divFreInfolist.ForEach(p => divfreinfo.Remove(p));
                }

                UnBindNotify(channeltree, null);

                i_channel.IsEdited = true;
            }
            else
            {
#if XBAP
                MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
            }
        }

        private void SelectedTreeChanged(object para)//组织机构树选择改变
        {
            SelectedOrganizationTree = para as OrganizationTreeItemViewModel;
            if (SelectedOrganizationTree is ItemTreeItemViewModel)//只有测点允许拖拽,拖拽暂时没做
            {
                AllowDropList = true;
                if ((SelectedOrganizationTree as ItemTreeItemViewModel).IsPaired == true)//如果已经绑定
                {
                    var itemtree = TreeProcess.AutoOrganizationTreeSelected(ServerTreeItems, SelectedOrganizationTree as ItemTreeItemViewModel);
                    GetServerNode(itemtree);
                }
            }
            //else if (SelectedOrganizationTree is DivFreTreeItemViewModel)//选中分频,也代表选中测点
            //{
            //    if ((SelectedOrganizationTree.Parent as ItemTreeItemViewModel).IsPaired == true)//如果已经绑定
            //    {
            //        var itemtree = TreeProcess.AutoOrganizationTreeSelected(ServerTreeItems, SelectedOrganizationTree.Parent as ItemTreeItemViewModel);
            //        GetServerNode(itemtree);
            //    }
            //}
            else
            {
                AllowDropList = false;
            }
        }

        private void SelectedServerChanged(object para)//服务树选择改变
        {
            var selectedServer = para as ChannelTreeItemViewModel;
            if (selectedServer != null)
            {
                if (selectedServer.IsPaired == true)//如果已经绑定
                {
                    //TreeProcess.AutoServerTreeSelected(OrganizationTreeItems, selectedServer); //自动选中节点 
                    TreeProcess.AutoServerTreeSelected(_organizationService.ItemTreeItems.Where(p => p.IsPaired == true).ToList(), selectedServer);
                }
            }
            GetServerNode(para);//得到服务器根节点
        }

        private void GetServerNode(object para)//得到服务器根节点,并展开硬件卡
        {
            ServerTreeItemViewModel itemtree = para as ServerTreeItemViewModel;
            ServerTreeItemViewModel server = null;
            MainCardTreeItemViewModel maincard = null;
            WireMatchingCardTreeItemViewModel wirecard = null;
            WirelessReceiveCardTreeItemViewModel wirelesscard = null;
            TransmissionCardTreeItemViewModel transmissioncard = null;
            SlotTreeItemViewModel slot = null;
            ChannelTreeItemViewModel channeltree = null;

            if (itemtree is ChannelTreeItemViewModel)
            {
                channeltree = itemtree as ChannelTreeItemViewModel;
                slot = itemtree.Parent as SlotTreeItemViewModel;
                if (slot.ISlot is WirelessScalarSlot || slot.ISlot is WirelessVibrationSlot)
                {
                    transmissioncard = itemtree.Parent.Parent as TransmissionCardTreeItemViewModel;
                    wirelesscard = itemtree.Parent.Parent.Parent as WirelessReceiveCardTreeItemViewModel;
                    maincard = itemtree.Parent.Parent.Parent.Parent as MainCardTreeItemViewModel;
                    server = itemtree.Parent.Parent.Parent.Parent.Parent as ServerTreeItemViewModel;
                }
                else
                {
                    wirecard = itemtree.Parent.Parent as WireMatchingCardTreeItemViewModel;
                    maincard = itemtree.Parent.Parent.Parent as MainCardTreeItemViewModel;
                    server = itemtree.Parent.Parent.Parent.Parent as ServerTreeItemViewModel;
                }
            }
            else if (itemtree is SlotTreeItemViewModel)
            {
                slot = itemtree as SlotTreeItemViewModel;
                if (slot.ISlot is WirelessScalarSlot || slot.ISlot is WirelessVibrationSlot)
                {
                    transmissioncard = itemtree.Parent as TransmissionCardTreeItemViewModel;
                    wirelesscard = itemtree.Parent.Parent as WirelessReceiveCardTreeItemViewModel;
                    maincard = itemtree.Parent.Parent.Parent as MainCardTreeItemViewModel;
                    server = itemtree.Parent.Parent.Parent.Parent as ServerTreeItemViewModel;
                }
                else
                {
                    wirecard = itemtree.Parent as WireMatchingCardTreeItemViewModel;
                    maincard = itemtree.Parent.Parent as MainCardTreeItemViewModel;
                    server = itemtree.Parent.Parent.Parent as ServerTreeItemViewModel;
                }
            }
            else if (itemtree is WireMatchingCardTreeItemViewModel)
            {
                wirecard = itemtree as WireMatchingCardTreeItemViewModel;
                maincard = itemtree.Parent as MainCardTreeItemViewModel;
                server = itemtree.Parent.Parent as ServerTreeItemViewModel;
            }
            else if (itemtree is TransmissionCardTreeItemViewModel)
            {
                transmissioncard = itemtree as TransmissionCardTreeItemViewModel;
                wirelesscard = itemtree.Parent as WirelessReceiveCardTreeItemViewModel;
                maincard = itemtree.Parent.Parent as MainCardTreeItemViewModel;
                server = itemtree.Parent.Parent.Parent as ServerTreeItemViewModel;
            }
            else if (itemtree is WirelessReceiveCardTreeItemViewModel)
            {
                wirelesscard = itemtree as WirelessReceiveCardTreeItemViewModel;
                maincard = itemtree.Parent as MainCardTreeItemViewModel;
                server = itemtree.Parent.Parent as ServerTreeItemViewModel;
            }
            else if (itemtree is MainCardTreeItemViewModel)
            {
                maincard = itemtree as MainCardTreeItemViewModel;
                server = itemtree.Parent as ServerTreeItemViewModel;
            }
            else if (itemtree is ServerTreeItemViewModel)
            {
                server = itemtree as ServerTreeItemViewModel;
            }

            if (server != null && (SelectedServerTree != server || SelectedServerTree == null))//切换服务器
            {
                SelectedServerTree = server;
                UpdateSelectedServer(SelectedServerTree);
            }
            if (maincard != null && (SelectedMainCardTree != maincard || SelectedMainCardTree == null))//切换主板
            {
                SelectedMainCardTree = maincard;
                UpdateSelectedMainCard(SelectedMainCardTree);
            }
            if (wirecard != null && SelectedWireMatchingCardTree != wirecard)//切换配板
            {
                SelectedWireMatchingCardTree = wirecard;
                UpdateSelectedCard(SelectedWireMatchingCardTree);

                SelectedWirelessReceiveCardTree = null;//清无线接收卡
                SelectedTransmissionCardTree = null;//清无线传输卡
            }
            if (wirelesscard != null && SelectedWirelessReceiveCardTree != wirelesscard)//切换配板
            {
                SelectedWirelessReceiveCardTree = wirelesscard;
                UpdateSelectedWirelessReceiveCard(SelectedWirelessReceiveCardTree);

                SelectedWireMatchingCardTree = null;//清有限配板
            }
            if (transmissioncard != null && SelectedTransmissionCardTree != transmissioncard)//切换无线接收
            {
                SelectedTransmissionCardTree = transmissioncard;
                UpdateSelectedTransmissionCard(SelectedTransmissionCardTree);

                SelectedWireMatchingCardTree = null;//清有限配板
            }
            if (slot != null && SelectedSlotTree != slot)//切换槽
            {
                SelectedSlotTree = slot;
                UpdateSelectedSlot(SelectedSlotTree);
            }
            if (channeltree != null && SelectedChannelTree != channeltree)//切换通道
            {
                SelectedChannelTree = channeltree;
                UpdateSelectedChannel(SelectedChannelTree);
            }
        }
        #endregion

        #region 获取板卡
        private async void InitHardware()
        {
            Status = ViewModelStatus.Querying;
            WaitInfo = "数据获取中";
            await _loginUserService.AwaitLazyLoading();
            ServerTreeItems = new ObservableCollection<ServerTreeItemViewModel>(_hardwareService.ServerTreeItems.Where(p => p.ServerIP == ServerIP));//服务器
            if (ServerTreeItems.Count > 0)
            {
                SelectedServerTree = ServerTreeItems[0];
            }
            else
            {
                SelectedServerTree = null;
            }

            UpdateSelectedServer(SelectedServerTree);
            WaitInfo = "数据处理中";
            Status = ViewModelStatus.None;
        }

        private void UpdateSelectedServer(ServerTreeItemViewModel serverTree)
        {
            if (serverTree != null)
            {
                ServerIP = serverTree.ServerIP;
                if (serverTree.Children != null && serverTree.Children.Count > 0)
                {
                    SelectedMainCardTree = serverTree.Children[0] as MainCardTreeItemViewModel;
                }
                else
                {
                    SelectedMainCardTree = null;
                }
            }
            else
            {
                ServerIP = null;
                SelectedMainCardTree = null;
            }
            UpdateSelectedMainCard(SelectedMainCardTree);
        }

        private void UpdateSelectedMainCard(MainCardTreeItemViewModel mainCardTree)
        {
            if (mainCardTree != null)
            {
                if (MainCard == null)
                {
                    MainCard = new ObservableCollection<MainControlCard>();
                }
                else
                {
                    MainCard.Clear();//板卡
                }
                if (mainCardTree.MainControlCard != null)
                {
                    MainCard.Add(mainCardTree.MainControlCard);
                }

                //无线接收板卡
                if (WirelessReceiveCard == null)
                {
                    WirelessReceiveCard = new ObservableCollection<WirelessReceiveCard>();
                }
                else
                {
                    WirelessReceiveCard.Clear();
                }
                if (mainCardTree.WirelessReceiveCard != null)
                {
                    WirelessReceiveCard.Add(mainCardTree.WirelessReceiveCard);
                }

                //板卡
                if (Cards == null)
                {
                    Cards = new ObservableCollection<ICard>();
                }
                else
                {
                    Cards.Clear();//板卡
                }

                //有线板卡
                if (mainCardTree.WireMatchingCard != null)
                {
                    Cards.AddRange(mainCardTree.WireMatchingCard);
                }
                //无线传输卡
                if (mainCardTree.WirelessReceiveCard != null && mainCardTree.WirelessReceiveCard.TransmissionCard != null)
                {
                    Cards.AddRange(mainCardTree.WirelessReceiveCard.TransmissionCard);
                }
                if (Cards.Count > 0)
                {
                    SelectedCard = Cards[0];
                }

                CardChanged(SelectedCard);

                MainControlCardIP = mainCardTree.MainControlCardIP;
                Identifier = mainCardTree.MainControlCard.Identifier;
            }
            else
            {
                if (MainCard != null)
                {
                    MainCard.Clear();
                }

                if (Cards != null)
                {
                    Cards.Clear();
                }
                SelectedCard = null;

                if (Slots != null)
                {
                    Slots.Clear();
                }
                SelectedSlot = null;

                if (Channels != null)
                {
                    Channels.Clear();
                }
                SelectedChannel = null;

                MainControlCardIP = null;
                Identifier = null;
            }
        }

        private void UpdateSelectedCard(WireMatchingCardTreeItemViewModel cardTree)
        {
            var card = cardTree.WireMatchingCard;
            SelectedCard = card;
            CardChanged(SelectedCard);
        }

        private void UpdateSelectedWirelessReceiveCard(WirelessReceiveCardTreeItemViewModel cardTree)
        {
            var card = cardTree.WirelessReceiveCard;
            if (card != null && card.TransmissionCard != null && card.TransmissionCard.Count > 0)
            {
                SelectedCard = card.TransmissionCard[0];
            }
            else
            {
                SelectedCard = null;
            }
            CardChanged(SelectedCard);
        }

        private void UpdateSelectedTransmissionCard(TransmissionCardTreeItemViewModel cardTree)
        {
            var card = cardTree.TransmissionCard;
            SelectedCard = card;
            CardChanged(SelectedCard);
        }

        private void UpdateSelectedSlot(SlotTreeItemViewModel slotTree)
        {
            var slot = slotTree.ISlot;
            SelectedSlot = slot;
            SlotChanged(SelectedSlot);
        }

        private void UpdateSelectedChannel(ChannelTreeItemViewModel channelTree)
        {
            var channeltree = (from p in Channels where p.CHNum == channelTree.CHNum select p).FirstOrDefault();
            SelectedChannel = channeltree;
            ChannelChanged(SelectedChannel);
        }

        private void CardChanged(object para)
        {
            ICard i_card = para as ICard;

            if (i_card != null)
            {
                if (i_card is MainControlCard)
                {
                    return;
                }
                else if (i_card is WirelessReceiveCard)
                {
                    return;
                }
                else if (i_card is WireMatchingCard || i_card is TransmissionCard)
                {
                    if (i_card is TransmissionCard)
                    {
                        TransmissionCardIdentifier = (i_card as TransmissionCard).SlaveIdentifier;
                    }
                    else
                    {
                        TransmissionCardIdentifier = null;
                    }
                    Slots = new ObservableCollection<ISlot>(_cardProcess.GetCardSlot(SelectedCard));
                    SelectedSlot = (Slots.Count > 0) ? Slots[0] : null;
                    SlotChanged(SelectedSlot);
                }
            }
        }

        private void SlotChanged(object para)
        {
            ISlot i_slot = para as ISlot;

            if (i_slot != null)
            {
                if (IsMultiSelected == false)
                {
                    Channels = new ObservableCollection<IChannel>(_cardProcess.GetSlotChannel(i_slot));
                    if (Channels.Count > 0)
                    {

                        SelectedChannel = Channels[0];
                    }
                    else
                    {
                        SelectedChannel = null;
                    }
                }
                else
                {
                    Channels = new ObservableCollection<IChannel>();
                    foreach (var slot in Slots)
                    {
                        var subchannels = _cardProcess.GetSlotChannel(slot);
                        Channels.AddRange(subchannels);
                        if (slot == i_slot)
                        {
                            if (subchannels.Count > 0)
                            {
                                SelectedChannel = subchannels[0];
                            }
                            else
                            {
                                SelectedChannel = null;
                            }
                        }
                    }                   
                }
               
            }
        }

        private void ChannelChanged(object para)
        {
            IChannel i_channel = para as IChannel;

            if (i_channel != null)
            {
                //更新延时报警
                //if (i_channel.T_AbstractChannelInfo.T_Item_Guid != null)
                //{
                //    var sg = _signalProcess.GetSignal(i_channel.T_AbstractChannelInfo.T_Item_Guid.Value);
                //    if (sg != null)
                //    {
                //        sg.DelayAlarmTime = i_channel.DelayAlarmTime;
                //        sg.SubscribeAlarmGrade(sg.DelayAlarmTime);
                //        sg.NotOKDelayAlarmTime = i_channel.NotOKDelayAlarmTime;
                //        sg.SubscribeIsNotOK(sg.NotOKDelayAlarmTime);
                //    }
                //}
            }
        }
        #endregion

        #region 编辑板卡
        //仅仅是为了兼容网页模式ShowDialog不生效
        private bool winshow = false;

        MainControlCardWin mainControlCardWin;
        WireMatchingCardWin wireMatchingCardWin;
        WirelessReceiveCardWin wirelessReceiveCardWin;
        TransmissionCardWin transmissionCardWin;
        private void CardEdit(object para)
        {
            if (winshow == true)
            {
                return;
            }
            try
            {
                winshow = true;

                ICard i_card = para as ICard;
                if (i_card != null)
                {
                    if (i_card is MainControlCard)
                    {
                        mainControlCardWin = new MainControlCardWin(i_card);
                        mainControlCardWin.Parachanged += Win_Parachanged;
                        mainControlCardWin.ShowDialog();
                    }
                    else if (i_card is WireMatchingCard)
                    {
                        wireMatchingCardWin = new WireMatchingCardWin(i_card);
                        wireMatchingCardWin.Parachanged += Win_Parachanged;
                        wireMatchingCardWin.ShowDialog();
                    }
                    else if (i_card is WirelessReceiveCard)
                    {
                        wirelessReceiveCardWin = new WirelessReceiveCardWin(i_card);
                        wirelessReceiveCardWin.Parachanged += Win_Parachanged;
                        wirelessReceiveCardWin.ShowDialog();
                    }
                    else if (i_card is TransmissionCard)
                    {
                        transmissionCardWin = new TransmissionCardWin(i_card);
                        transmissionCardWin.Parachanged += Win_Parachanged;
                        transmissionCardWin.ShowDialog();
                    }
                }
            }
            finally
            {
                winshow = false;
            }
        }

        private async void Win_Parachanged(ICard i_card)
        {
            try
            {
                Status = ViewModelStatus.Querying;
                if (i_card is MainControlCard)
                {
                    _convertToDataBaseFormat.MainControlCardTempConvert(i_card as MainControlCard, MainCard[0]);
                }
                else if (i_card is WireMatchingCard)
                {
                    _convertToDataBaseFormat.WireMatchingCardTempConvert(i_card as WireMatchingCard, SelectedCard as WireMatchingCard);
                }
                else if (i_card is WirelessReceiveCard)
                {
                    _convertToDataBaseFormat.WirelessReceiveCardTempConvert(i_card as WirelessReceiveCard, WirelessReceiveCard[0]);
                }
                else if (i_card is TransmissionCard)
                {
                    _convertToDataBaseFormat.TransmissionCardTempConvert(i_card as TransmissionCard, SelectedCard as TransmissionCard);
                }

                if (i_card is MainControlCard)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_MainControlCard", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { MainCard[0].T_MainControlCard.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)                
                    {
                        MainCard[0].T_MainControlCard.GetTempData();
                        CardCopyHelper.MainControlCardLeftCopyToRight(i_card as MainControlCard, MainCard[0]);
                        mainControlCardWin.Close();
                        MainCard[0].IsEdited = true;
                    }
                }
                else if (i_card is WireMatchingCard)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_WireMatchingCard", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedCard as WireMatchingCard).T_WireMatchingCard.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)                   
                    {
                        (SelectedCard as WireMatchingCard).T_WireMatchingCard.GetTempData();
                        CardCopyHelper.WireMatchingCardLeftCopyToRight(i_card as WireMatchingCard, SelectedCard as WireMatchingCard);
                        wireMatchingCardWin.Close();
                        (SelectedCard as WireMatchingCard).IsEdited = true;
                    }
                }
                else if (i_card is WirelessReceiveCard)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_WirelessReceiveCard", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { WirelessReceiveCard[0].T_WirelessReceiveCard.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)                   
                    {
                        WirelessReceiveCard[0].T_WirelessReceiveCard.GetTempData();
                        CardCopyHelper.WirelessReceiveCardLeftCopyToRight(i_card as WirelessReceiveCard, WirelessReceiveCard[0]);
                        wirelessReceiveCardWin.Close();
                        WirelessReceiveCard[0].IsEdited = true;
                    }
                }
                else if (i_card is TransmissionCard)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_TransmissionCard", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedCard as TransmissionCard).T_TransmissionCard.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)                   
                    {
                        (SelectedCard as TransmissionCard).T_TransmissionCard.GetTempData();
                        CardCopyHelper.TransmissionCardLeftCopyToRight(i_card as TransmissionCard, SelectedCard as TransmissionCard);
                        transmissionCardWin.Close();
                        (SelectedCard as TransmissionCard).IsEdited = true;
                    }
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("编辑板卡", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        IEPESlotWin iEPESlotWin;
        EddyCurrentDisplacementSlotWin eddyCurrentDisplacementSlotWin;
        EddyCurrentKeyPhaseSlotWin eddyCurrentKeyPhaseSlotWin;
        EddyCurrentTachometerSlotWin eddyCurrentTachometerSlotWin;
        DigitTachometerSlotWin digitTachometerSlotWin;
        AnalogRransducerInSlotWin analogRransducerInSlotWin;
        RelaySlotWin relaySlotWin;
        DigitRransducerInSlotWin digitRransducerInSlotWin;
        DigitRransducerOutSlotWin digitRransducerOutSlotWin;
        AnalogRransducerOutSlotWin analogRransducerOutSlotWin;
        WirelessVibrationSlotWin wirelessVibrationSlotWin;
        private void SlotEdit(object para)
        {
            if (winshow == true)
            {
                return;
            }
            try
            {
                winshow = true;
                ISlot i_slot = para as ISlot;
                if (i_slot != null)
                {
                    if (i_slot is IEPESlot)
                    {
                        iEPESlotWin = new IEPESlotWin(i_slot);
                        iEPESlotWin.Parachanged += Win_Parachanged;
                        iEPESlotWin.ShowDialog();
                    }
                    else if (i_slot is EddyCurrentDisplacementSlot)
                    {
                        eddyCurrentDisplacementSlotWin = new EddyCurrentDisplacementSlotWin(i_slot);
                        eddyCurrentDisplacementSlotWin.Parachanged += Win_Parachanged;
                        eddyCurrentDisplacementSlotWin.ShowDialog();
                    }
                    else if (i_slot is EddyCurrentKeyPhaseSlot)
                    {
                        eddyCurrentKeyPhaseSlotWin = new EddyCurrentKeyPhaseSlotWin(i_slot);
                        eddyCurrentKeyPhaseSlotWin.Parachanged += Win_Parachanged;
                        eddyCurrentKeyPhaseSlotWin.ShowDialog();
                    }
                    else if (i_slot is EddyCurrentTachometerSlot)
                    {
                        eddyCurrentTachometerSlotWin = new EddyCurrentTachometerSlotWin(i_slot);
                        eddyCurrentTachometerSlotWin.Parachanged += Win_Parachanged;
                        eddyCurrentTachometerSlotWin.ShowDialog();
                    }
                    else if (i_slot is DigitTachometerSlot)
                    {
                        digitTachometerSlotWin = new DigitTachometerSlotWin(i_slot);
                        digitTachometerSlotWin.Parachanged += Win_Parachanged;
                        digitTachometerSlotWin.ShowDialog();
                    }
                    else if (i_slot is AnalogRransducerInSlot)
                    {
                        analogRransducerInSlotWin = new AnalogRransducerInSlotWin(i_slot);
                        analogRransducerInSlotWin.Parachanged += Win_Parachanged;
                        analogRransducerInSlotWin.ShowDialog();
                    }
                    else if (i_slot is RelaySlot)
                    {
                        relaySlotWin = new RelaySlotWin(i_slot);
                        relaySlotWin.Parachanged += Win_Parachanged;
                        relaySlotWin.ShowDialog();
                    }
                    else if (i_slot is DigitRransducerInSlot)
                    {
                        digitRransducerInSlotWin = new DigitRransducerInSlotWin(i_slot);
                        digitRransducerInSlotWin.Parachanged += Win_Parachanged;
                        digitRransducerInSlotWin.ShowDialog();
                    }
                    else if (i_slot is DigitRransducerOutSlot)
                    {
                        digitRransducerOutSlotWin = new DigitRransducerOutSlotWin(i_slot);
                        digitRransducerOutSlotWin.Parachanged += Win_Parachanged;
                        digitRransducerOutSlotWin.ShowDialog();
                    }
                    else if (i_slot is AnalogRransducerOutSlot)
                    {
                        analogRransducerOutSlotWin = new AnalogRransducerOutSlotWin(i_slot);
                        analogRransducerOutSlotWin.Parachanged += Win_Parachanged;
                        analogRransducerOutSlotWin.ShowDialog();
                    }
                    else if (i_slot is WirelessScalarSlot)
                    {
                    }
                    else if (i_slot is WirelessVibrationSlot)
                    {
                        wirelessVibrationSlotWin = new WirelessVibrationSlotWin(i_slot);
                        wirelessVibrationSlotWin.Parachanged += Win_Parachanged;
                        wirelessVibrationSlotWin.ShowDialog();
                    }
                }
            }
            finally
            {
                winshow = false;
            }
        }

        private async void Win_Parachanged(ISlot i_slot)
        {
            try
            {
                _convertToDataBaseFormat.SlotTempConvert(i_slot, SelectedSlot);
                Status = ViewModelStatus.Querying;
                if (i_slot is IEPESlot)
                {

                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_IEPESlot", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as IEPESlot).T_IEPESlot.TempData }));
                    editDic.Add("T_AbstractSlotInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as IEPESlot).T_AbstractSlotInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedSlot as IEPESlot).T_IEPESlot.GetTempData();
                        (SelectedSlot as IEPESlot).T_AbstractSlotInfo.GetTempData();
                        CardCopyHelper.SlotLeftCopyToRight(i_slot, SelectedSlot);
                        iEPESlotWin.Close();
                        (SelectedSlot as IEPESlot).IsEdited = true;
                    }
                }
                else if (i_slot is EddyCurrentDisplacementSlot)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_EddyCurrentDisplacementSlot", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as EddyCurrentDisplacementSlot).T_EddyCurrentDisplacementSlot.TempData }));
                    editDic.Add("T_AbstractSlotInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as EddyCurrentDisplacementSlot).T_AbstractSlotInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedSlot as EddyCurrentDisplacementSlot).T_EddyCurrentDisplacementSlot.GetTempData();
                        (SelectedSlot as EddyCurrentDisplacementSlot).T_AbstractSlotInfo.GetTempData();
                        CardCopyHelper.SlotLeftCopyToRight(i_slot, SelectedSlot);
                        eddyCurrentDisplacementSlotWin.Close();
                        (SelectedSlot as EddyCurrentDisplacementSlot).IsEdited = true;
                    }
                }
                else if (i_slot is EddyCurrentKeyPhaseSlot)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_EddyCurrentKeyPhaseSlot", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as EddyCurrentKeyPhaseSlot).T_EddyCurrentKeyPhaseSlot.TempData }));
                    editDic.Add("T_AbstractSlotInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as EddyCurrentKeyPhaseSlot).T_AbstractSlotInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedSlot as EddyCurrentKeyPhaseSlot).T_EddyCurrentKeyPhaseSlot.GetTempData();
                        (SelectedSlot as EddyCurrentKeyPhaseSlot).T_AbstractSlotInfo.GetTempData();
                        CardCopyHelper.SlotLeftCopyToRight(i_slot, SelectedSlot);
                        eddyCurrentKeyPhaseSlotWin.Close();
                        (SelectedSlot as EddyCurrentKeyPhaseSlot).IsEdited = true;
                    }
                }
                else if (i_slot is EddyCurrentTachometerSlot)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_EddyCurrentTachometerSlot", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as EddyCurrentTachometerSlot).T_EddyCurrentTachometerSlot.TempData }));
                    editDic.Add("T_AbstractSlotInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as EddyCurrentTachometerSlot).T_AbstractSlotInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedSlot as EddyCurrentTachometerSlot).T_EddyCurrentTachometerSlot.GetTempData();
                        (SelectedSlot as EddyCurrentTachometerSlot).T_AbstractSlotInfo.GetTempData();
                        CardCopyHelper.SlotLeftCopyToRight(i_slot, SelectedSlot);
                        eddyCurrentTachometerSlotWin.Close();
                        (SelectedSlot as EddyCurrentTachometerSlot).IsEdited = true;
                    }
                }
                else if (i_slot is DigitTachometerSlot)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_DigitTachometerSlot", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as DigitTachometerSlot).T_DigitTachometerSlot.TempData }));
                    editDic.Add("T_AbstractSlotInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as DigitTachometerSlot).T_AbstractSlotInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedSlot as DigitTachometerSlot).T_DigitTachometerSlot.GetTempData();
                        (SelectedSlot as DigitTachometerSlot).T_AbstractSlotInfo.GetTempData();
                        CardCopyHelper.SlotLeftCopyToRight(i_slot, SelectedSlot);
                        digitTachometerSlotWin.Close();
                        (SelectedSlot as DigitTachometerSlot).IsEdited = true;
                    }
                }
                else if (i_slot is AnalogRransducerInSlot)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_AnalogRransducerInSlot", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as AnalogRransducerInSlot).T_AnalogRransducerInSlot.TempData }));
                    editDic.Add("T_AbstractSlotInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as AnalogRransducerInSlot).T_AbstractSlotInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedSlot as AnalogRransducerInSlot).T_AnalogRransducerInSlot.GetTempData();
                        (SelectedSlot as AnalogRransducerInSlot).T_AbstractSlotInfo.GetTempData();
                        CardCopyHelper.SlotLeftCopyToRight(i_slot, SelectedSlot);
                        analogRransducerInSlotWin.Close();
                        (SelectedSlot as AnalogRransducerInSlot).IsEdited = true;
                    }
                }
                else if (i_slot is RelaySlot)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_RelaySlot", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as RelaySlot).T_RelaySlot.TempData }));
                    editDic.Add("T_AbstractSlotInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as RelaySlot).T_AbstractSlotInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedSlot as RelaySlot).T_RelaySlot.GetTempData();
                        (SelectedSlot as RelaySlot).T_AbstractSlotInfo.GetTempData();
                        CardCopyHelper.SlotLeftCopyToRight(i_slot, SelectedSlot);
                        relaySlotWin.Close();
                        (SelectedSlot as RelaySlot).IsEdited = true;
                    }
                }
                else if (i_slot is DigitRransducerInSlot)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_DigitRransducerInSlot", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as DigitRransducerInSlot).T_DigitRransducerInSlot.TempData }));
                    editDic.Add("T_AbstractSlotInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as DigitRransducerInSlot).T_AbstractSlotInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedSlot as DigitRransducerInSlot).T_DigitRransducerInSlot.GetTempData();
                        (SelectedSlot as DigitRransducerInSlot).T_AbstractSlotInfo.GetTempData();
                        CardCopyHelper.SlotLeftCopyToRight(i_slot, SelectedSlot);
                        digitRransducerInSlotWin.Close();
                        (SelectedSlot as DigitRransducerInSlot).IsEdited = true;
                    }
                }
                else if (i_slot is DigitRransducerOutSlot)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_DigitRransducerOutSlot", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as DigitRransducerOutSlot).T_DigitRransducerOutSlot.TempData }));
                    editDic.Add("T_AbstractSlotInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as DigitRransducerOutSlot).T_AbstractSlotInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedSlot as DigitRransducerOutSlot).T_DigitRransducerOutSlot.GetTempData();
                        (SelectedSlot as DigitRransducerOutSlot).T_AbstractSlotInfo.GetTempData();
                        CardCopyHelper.SlotLeftCopyToRight(i_slot, SelectedSlot);
                        digitRransducerOutSlotWin.Close();
                        (SelectedSlot as DigitRransducerOutSlot).IsEdited = true;
                    }
                }
                else if (i_slot is AnalogRransducerOutSlot)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_AnalogRransducerOutSlot", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as AnalogRransducerOutSlot).T_AnalogRransducerOutSlot.TempData }));
                    editDic.Add("T_AbstractSlotInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as AnalogRransducerOutSlot).T_AbstractSlotInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedSlot as AnalogRransducerOutSlot).T_AnalogRransducerOutSlot.GetTempData();
                        (SelectedSlot as AnalogRransducerOutSlot).T_AbstractSlotInfo.GetTempData();
                        CardCopyHelper.SlotLeftCopyToRight(i_slot, SelectedSlot);
                        analogRransducerOutSlotWin.Close();
                        (SelectedSlot as AnalogRransducerOutSlot).IsEdited = true;
                    }
                }
                else if (i_slot is WirelessScalarSlot)
                {
                }
                else if (i_slot is WirelessVibrationSlot)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_WirelessVibrationSlot", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedSlot as WirelessVibrationSlot).T_WirelessVibrationSlot.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedSlot as WirelessVibrationSlot).T_WirelessVibrationSlot.GetTempData();
                        CardCopyHelper.SlotLeftCopyToRight(i_slot, SelectedSlot);
                        wirelessVibrationSlotWin.Close();
                        (SelectedSlot as WirelessVibrationSlot).IsEdited = true;
                    }
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("编辑卡槽", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        WirelessScalarChannelInfoWin wirelessScalarChannelInfoWin;
        WirelessVibrationChannelInfoWin wirelessVibrationChannelInfoWin;
        IEPEChannelInfoWin iEPEChannelInfoWin;
        EddyCurrentDisplacementChannelInfoWin eddyCurrentDisplacementChannelInfoWin;
        EddyCurrentKeyPhaseChannelInfoWin eddyCurrentKeyPhaseChannelInfoWin;
        EddyCurrentTachometerChannelInfoWin eddyCurrentTachometerChannelInfoWin;
        DigitTachometerChannelInfoWin digitTachometerChannelInfoWin;
        AnalogRransducerInChannelInfoWin analogRransducerInChannelInfoWin;
        RelayChannelInfoWin relayChannelInfoWin;
        DigitRransducerInChannelInfoWin digitRransducerInChannelInfoWin;
        DigitRransducerOutChannelInfoWin digitRransducerOutChannelInfoWin;
        AnalogRransducerOutChannelInfoWin analogRransducerOutChannelInfoWin;
        private void ChannelEdit(object para)
        {
            if (DivFreDoubleClick)//避免channeltree编辑事件
            {
                DivFreDoubleClick = false;
                return;
            }

            if (winshow == true)
            {
                return;
            }
            try
            {
                winshow = true;
                IChannel i_channel = para as IChannel;
                if (i_channel != null)
                {
                    if (i_channel is WirelessScalarChannelInfo)
                    {
                        wirelessScalarChannelInfoWin = new WirelessScalarChannelInfoWin(i_channel);
                        wirelessScalarChannelInfoWin.Parachanged += Win_Parachanged;
                        wirelessScalarChannelInfoWin.ShowDialog();
                    }
                    else if (i_channel is WirelessVibrationChannelInfo)
                    {
                        wirelessVibrationChannelInfoWin = new WirelessVibrationChannelInfoWin(i_channel);
                        wirelessVibrationChannelInfoWin.Parachanged += Win_Parachanged;
                        wirelessVibrationChannelInfoWin.ShowDialog();
                    }
                    else if (i_channel is IEPEChannelInfo)
                    {
                        iEPEChannelInfoWin = new IEPEChannelInfoWin(i_channel);
                        iEPEChannelInfoWin.Parachanged += Win_Parachanged;
                        iEPEChannelInfoWin.ShowDialog();
                    }
                    else if (i_channel is EddyCurrentDisplacementChannelInfo)
                    {
                        eddyCurrentDisplacementChannelInfoWin = new EddyCurrentDisplacementChannelInfoWin(i_channel);
                        eddyCurrentDisplacementChannelInfoWin.Parachanged += Win_Parachanged;
                        eddyCurrentDisplacementChannelInfoWin.ShowDialog();
                    }
                    else if (i_channel is EddyCurrentKeyPhaseChannelInfo)
                    {
                        eddyCurrentKeyPhaseChannelInfoWin = new EddyCurrentKeyPhaseChannelInfoWin(i_channel);
                        eddyCurrentKeyPhaseChannelInfoWin.Parachanged += Win_Parachanged;
                        eddyCurrentKeyPhaseChannelInfoWin.ShowDialog();
                    }
                    else if (i_channel is EddyCurrentTachometerChannelInfo)
                    {
                        eddyCurrentTachometerChannelInfoWin = new EddyCurrentTachometerChannelInfoWin(i_channel);
                        eddyCurrentTachometerChannelInfoWin.Parachanged += Win_Parachanged;
                        eddyCurrentTachometerChannelInfoWin.ShowDialog();
                    }
                    else if (i_channel is DigitTachometerChannelInfo)
                    {
                        digitTachometerChannelInfoWin = new DigitTachometerChannelInfoWin(i_channel);
                        digitTachometerChannelInfoWin.Parachanged += Win_Parachanged;
                        digitTachometerChannelInfoWin.ShowDialog();
                    }
                    else if (i_channel is AnalogRransducerInChannelInfo)
                    {
                        analogRransducerInChannelInfoWin = new AnalogRransducerInChannelInfoWin(i_channel);
                        analogRransducerInChannelInfoWin.Parachanged += Win_Parachanged;
                        analogRransducerInChannelInfoWin.ShowDialog();
                    }
                    else if (i_channel is RelayChannelInfo)
                    {
                        relayChannelInfoWin = new RelayChannelInfoWin(i_channel);
                        relayChannelInfoWin.Parachanged += Win_Parachanged;
                        relayChannelInfoWin.ShowDialog();
                    }
                    else if (i_channel is DigitRransducerInChannelInfo)
                    {
                        digitRransducerInChannelInfoWin = new DigitRransducerInChannelInfoWin(i_channel);
                        digitRransducerInChannelInfoWin.Parachanged += Win_Parachanged;
                        digitRransducerInChannelInfoWin.ShowDialog();
                    }
                    else if (i_channel is DigitRransducerOutChannelInfo)
                    {
                        digitRransducerOutChannelInfoWin = new DigitRransducerOutChannelInfoWin(i_channel);
                        digitRransducerOutChannelInfoWin.Parachanged += Win_Parachanged;
                        digitRransducerOutChannelInfoWin.ShowDialog();
                    }
                    else if (i_channel is AnalogRransducerOutChannelInfo)
                    {
                        analogRransducerOutChannelInfoWin = new AnalogRransducerOutChannelInfoWin(i_channel);
                        analogRransducerOutChannelInfoWin.Parachanged += Win_Parachanged;
                        analogRransducerOutChannelInfoWin.ShowDialog();
                    }
                }

            }

            finally
            {
                winshow = false;
            }
        }

        private async void Win_Parachanged(IChannel i_channel)
        {
            try
            {
                Status = ViewModelStatus.Querying;
                _convertToDataBaseFormat.ChannelTempConvert(i_channel, SelectedChannel);

                if (i_channel is WirelessScalarChannelInfo)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_WirelessScalarChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as WirelessScalarChannelInfo).T_WirelessScalarChannelInfo.TempData }));
                    editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as WirelessScalarChannelInfo).T_AbstractChannelInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedChannel as WirelessScalarChannelInfo).T_WirelessScalarChannelInfo.GetTempData();
                        (SelectedChannel as WirelessScalarChannelInfo).T_AbstractChannelInfo.GetTempData();
                        CardCopyHelper.ChannelLeftCopyToRight(i_channel, SelectedChannel);
                        wirelessScalarChannelInfoWin.Close();
                        (SelectedChannel as WirelessScalarChannelInfo).IsEdited = true;
                    }
                }
                else if (i_channel is WirelessVibrationChannelInfo)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_WirelessVibrationChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as WirelessVibrationChannelInfo).T_WirelessVibrationChannelInfo.TempData }));
                    editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as WirelessVibrationChannelInfo).T_AbstractChannelInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedChannel as WirelessVibrationChannelInfo).T_WirelessVibrationChannelInfo.GetTempData();
                        (SelectedChannel as WirelessVibrationChannelInfo).T_AbstractChannelInfo.GetTempData();
                        CardCopyHelper.ChannelLeftCopyToRight(i_channel, SelectedChannel);
                        wirelessVibrationChannelInfoWin.Close();
                        (SelectedChannel as WirelessVibrationChannelInfo).IsEdited = true;
                    }
                }
                else if (i_channel is IEPEChannelInfo)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_IEPEChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as IEPEChannelInfo).T_IEPEChannelInfo.TempData }));
                    editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as IEPEChannelInfo).T_AbstractChannelInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedChannel as IEPEChannelInfo).T_IEPEChannelInfo.GetTempData();
                        (SelectedChannel as IEPEChannelInfo).T_AbstractChannelInfo.GetTempData();
                        CardCopyHelper.ChannelLeftCopyToRight(i_channel, SelectedChannel);
                        iEPEChannelInfoWin.Close();
                        (SelectedChannel as IEPEChannelInfo).IsEdited = true;
                    }
                }
                else if (i_channel is EddyCurrentDisplacementChannelInfo)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_EddyCurrentDisplacementChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as EddyCurrentDisplacementChannelInfo).T_EddyCurrentDisplacementChannelInfo.TempData }));
                    editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as EddyCurrentDisplacementChannelInfo).T_AbstractChannelInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedChannel as EddyCurrentDisplacementChannelInfo).T_EddyCurrentDisplacementChannelInfo.GetTempData();
                        (SelectedChannel as EddyCurrentDisplacementChannelInfo).T_AbstractChannelInfo.GetTempData();
                        CardCopyHelper.ChannelLeftCopyToRight(i_channel, SelectedChannel);
                        eddyCurrentDisplacementChannelInfoWin.Close();
                        (SelectedChannel as EddyCurrentDisplacementChannelInfo).IsEdited = true;
                    }
                }
                else if (i_channel is EddyCurrentKeyPhaseChannelInfo)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_EddyCurrentKeyPhaseChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as EddyCurrentKeyPhaseChannelInfo).T_EddyCurrentKeyPhaseChannelInfo.TempData }));
                    editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as EddyCurrentKeyPhaseChannelInfo).T_AbstractChannelInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedChannel as EddyCurrentKeyPhaseChannelInfo).T_EddyCurrentKeyPhaseChannelInfo.GetTempData();
                        (SelectedChannel as EddyCurrentKeyPhaseChannelInfo).T_AbstractChannelInfo.GetTempData();
                        CardCopyHelper.ChannelLeftCopyToRight(i_channel, SelectedChannel);
                        eddyCurrentKeyPhaseChannelInfoWin.Close();
                        (SelectedChannel as EddyCurrentKeyPhaseChannelInfo).IsEdited = true;
                    }
                }
                else if (i_channel is EddyCurrentTachometerChannelInfo)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_EddyCurrentTachometerChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as EddyCurrentTachometerChannelInfo).T_EddyCurrentTachometerChannelInfo.TempData }));
                    editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as EddyCurrentTachometerChannelInfo).T_AbstractChannelInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedChannel as EddyCurrentTachometerChannelInfo).T_EddyCurrentTachometerChannelInfo.GetTempData();
                        (SelectedChannel as EddyCurrentTachometerChannelInfo).T_AbstractChannelInfo.GetTempData();
                        CardCopyHelper.ChannelLeftCopyToRight(i_channel, SelectedChannel);
                        eddyCurrentTachometerChannelInfoWin.Close();
                        (SelectedChannel as EddyCurrentTachometerChannelInfo).IsEdited = true;
                    }
                }
                else if (i_channel is DigitTachometerChannelInfo)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_DigitTachometerChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as DigitTachometerChannelInfo).T_DigitTachometerChannelInfo.TempData }));
                    editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as DigitTachometerChannelInfo).T_AbstractChannelInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedChannel as DigitTachometerChannelInfo).T_DigitTachometerChannelInfo.GetTempData();
                        (SelectedChannel as DigitTachometerChannelInfo).T_AbstractChannelInfo.GetTempData();
                        CardCopyHelper.ChannelLeftCopyToRight(i_channel, SelectedChannel);
                        digitTachometerChannelInfoWin.Close();
                        (SelectedChannel as DigitTachometerChannelInfo).IsEdited = true;
                    }
                }
                else if (i_channel is AnalogRransducerInChannelInfo)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_AnalogRransducerInChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as AnalogRransducerInChannelInfo).T_AnalogRransducerInChannelInfo.TempData }));
                    editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as AnalogRransducerInChannelInfo).T_AbstractChannelInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedChannel as AnalogRransducerInChannelInfo).T_AnalogRransducerInChannelInfo.GetTempData();
                        (SelectedChannel as AnalogRransducerInChannelInfo).T_AbstractChannelInfo.GetTempData();
                        CardCopyHelper.ChannelLeftCopyToRight(i_channel, SelectedChannel);
                        analogRransducerInChannelInfoWin.Close();
                        (SelectedChannel as AnalogRransducerInChannelInfo).IsEdited = true;
                    }
                }
                else if (i_channel is RelayChannelInfo)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_RelayChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as RelayChannelInfo).T_RelayChannelInfo.TempData }));
                    editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as RelayChannelInfo).T_AbstractChannelInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedChannel as RelayChannelInfo).T_RelayChannelInfo.GetTempData();
                        (SelectedChannel as RelayChannelInfo).T_AbstractChannelInfo.GetTempData();
                        CardCopyHelper.ChannelLeftCopyToRight(i_channel, SelectedChannel);
                        relayChannelInfoWin.Close();
                        (SelectedChannel as RelayChannelInfo).IsEdited = true;
                    }
                }
                else if (i_channel is DigitRransducerInChannelInfo)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_DigitRransducerInChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as DigitRransducerInChannelInfo).T_DigitRransducerInChannelInfo.TempData }));
                    editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as DigitRransducerInChannelInfo).T_AbstractChannelInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedChannel as DigitRransducerInChannelInfo).T_DigitRransducerInChannelInfo.GetTempData();
                        (SelectedChannel as DigitRransducerInChannelInfo).T_AbstractChannelInfo.GetTempData();
                        CardCopyHelper.ChannelLeftCopyToRight(i_channel, SelectedChannel);
                        digitRransducerInChannelInfoWin.Close();
                        (SelectedChannel as DigitRransducerInChannelInfo).IsEdited = true;
                    }
                }
                else if (i_channel is DigitRransducerOutChannelInfo)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_DigitRransducerOutChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as DigitRransducerOutChannelInfo).T_DigitRransducerOutChannelInfo.TempData }));
                    editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as DigitRransducerOutChannelInfo).T_AbstractChannelInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedChannel as DigitRransducerOutChannelInfo).T_DigitRransducerOutChannelInfo.GetTempData();
                        (SelectedChannel as DigitRransducerOutChannelInfo).T_AbstractChannelInfo.GetTempData();
                        CardCopyHelper.ChannelLeftCopyToRight(i_channel, SelectedChannel);
                        digitRransducerOutChannelInfoWin.Close();
                        (SelectedChannel as DigitRransducerOutChannelInfo).IsEdited = true;
                    }
                }
                else if (i_channel is AnalogRransducerOutChannelInfo)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_AnalogRransducerOutChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as AnalogRransducerOutChannelInfo).T_AnalogRransducerOutChannelInfo.TempData }));
                    editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as AnalogRransducerOutChannelInfo).T_AbstractChannelInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedChannel as AnalogRransducerOutChannelInfo).T_AnalogRransducerOutChannelInfo.GetTempData();
                        (SelectedChannel as AnalogRransducerOutChannelInfo).T_AbstractChannelInfo.GetTempData();
                        CardCopyHelper.ChannelLeftCopyToRight(i_channel, SelectedChannel);
                        analogRransducerOutChannelInfoWin.Close();
                        (SelectedChannel as AnalogRransducerOutChannelInfo).IsEdited = true;
                    }
                }

                //更新延时报警
                if (SelectedChannel.T_AbstractChannelInfo.T_Item_Guid != null)
                {
                    var sg = _signalProcess.GetSignal(SelectedChannel.T_AbstractChannelInfo.T_Item_Guid.Value, SelectedServerTree.ServerIP); //var sg = _signalProcess.GetSignal(SelectedChannel.T_AbstractChannelInfo.T_Item_Guid.Value);
                    if (sg != null)
                    {
                        sg.DelayAlarmTime = SelectedChannel.DelayAlarmTime;
                        sg.SubscribeAlarmGrade(sg.DelayAlarmTime);
                        sg.NotOKDelayAlarmTime = SelectedChannel.NotOKDelayAlarmTime;
                        sg.SubscribeIsNotOK(sg.NotOKDelayAlarmTime);
                    }
                }

            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("编辑板卡", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        DivFreInfoWin divFreInfoWin;

        private void DivFreEdit(object para)
        {
            if (winshow == true)
            {
                return;
            }
            try
            {
                winshow = true;
                if (para is int)
                {                  
                    if (SelectedChannel is DivFreIChannel)
                    {
                        SelectedDivFreInfoIndex = (int)para;
                        DivFreInfo divfreinfo = (SelectedChannel as DivFreIChannel).DivFreInfo[SelectedDivFreInfoIndex];
                        divFreInfoWin = new DivFreInfoWin(divfreinfo);
                        divFreInfoWin.Parachanged += Win_Parachanged; ;
                        divFreInfoWin.ShowDialog();
                    }
                }
            }

            finally
            {
                DivFreDoubleClick = true;//避免channeltree编辑事件
                winshow = false;
            }
        }

        private async void Win_Parachanged(DivFreInfo divfreinfo)
        {
            try
            {
                Status = ViewModelStatus.Querying;              
                _convertToDataBaseFormat.DivFreInfoTempConvert(divfreinfo, (SelectedChannel as DivFreIChannel).DivFreInfo[SelectedDivFreInfoIndex]);
                if (SelectedChannel is DivFreIChannel)
                {
                    Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                    editDic.Add("T_DivFreInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { (SelectedChannel as DivFreIChannel).DivFreInfo[SelectedDivFreInfoIndex].T_DivFreInfo.TempData }));
                    if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                    {
                        (SelectedChannel as DivFreIChannel).DivFreInfo[SelectedDivFreInfoIndex].T_DivFreInfo.GetTempData();
                        CardCopyHelper.DivFreInfoLeftCopyToRight(divfreinfo, (SelectedChannel as DivFreIChannel).DivFreInfo[SelectedDivFreInfoIndex]);
                        divFreInfoWin.Close();
                        (SelectedChannel as DivFreIChannel).DivFreInfo[SelectedDivFreInfoIndex].IsEdited = true;
                    }
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("编辑分频", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        #endregion

        #region 添加删除下发板卡
        private async void AddCard(object para)
        {
            //做重名检查和无效检查,允许不同服务器下相同IP
            var server = ServerTreeItems.Where(p => p.ServerIP == ServerIP).FirstOrDefault();
            if (server == null)
            {
                return;
            }
            var maincard = server.Children.OfType<MainCardTreeItemViewModel>().Where(p => p.MainControlCardIP == MainControlCardIPEdit && p.MainControlCard.ServerIP == ServerIP);
            if (maincard.Count() > 0)
            {
#if XBAP
                MessageBox.Show((string)Application.Current.Resources["messagePDAManage7"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["messagePDAManage7"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }
            if (MainControlCardIPEdit == null || (Error != null && Error != ""))
            {
                return;
            }
            try
            {
                Status = ViewModelStatus.Querying;
                string json = GetRootjson(RequestType.GetHardWareInfo, null, ServerIP);
                List<ChannelTreeItemViewModel> channels = await _hardwareService.AddCard(ServerIP, MainControlCardIPEdit, json);
                await AddChannel(ServerIP, channels);
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("添加数采器", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        private async void DeleteCard(object para)
        {
            try
            {
                Status = ViewModelStatus.Querying;
                var server = ServerTreeItems.Where(p => p.ServerIP == ServerIP).FirstOrDefault();
                if (server == null)
                {
                    return;
                }
                var maincard = server.Children.OfType<MainCardTreeItemViewModel>().Where(p => p.MainControlCardIP == MainControlCardIPEdit).FirstOrDefault();
                if (maincard == null)
                {
#if XBAP
                    MessageBox.Show("请重新选中主板", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                    Xceed.Wpf.Toolkit.MessageBox.Show("请重新选中主板", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                    return;
                }

                string json = GetRootjson(RequestType.DeleteConfig, maincard, maincard.ServerIP);
                List<ChannelTreeItemViewModel> channels = await _hardwareService.DeleteCard(ServerIP, maincard.MainControlCardIP, json);

                await DeleteChannel(ServerIP, channels);

                if (MainCard != null)
                {
                    MainCard.Clear();
                }
                if (WirelessReceiveCard != null)
                {
                    WirelessReceiveCard.Clear();
                }
                if (Cards != null)
                {
                    Cards.Clear();
                }
                if (Slots != null)
                {
                    Slots.Clear();
                }
                if (Channels != null)
                {
                    Channels.Clear();
                }
                SelectedMainCardTree = null;
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("删除数采器", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        private async void ForceDeleteCard(object para)
        {
            try
            {
                Status = ViewModelStatus.Querying;
                var server = ServerTreeItems.Where(p => p.ServerIP == ServerIP).FirstOrDefault();
                if (server == null)
                {
                    return;
                }
                var maincard = server.Children.OfType<MainCardTreeItemViewModel>().Where(p => p.MainControlCardIP == MainControlCardIPEdit).FirstOrDefault();
                if (maincard == null)
                {
#if XBAP
                    MessageBox.Show("请重新选中主板", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                    Xceed.Wpf.Toolkit.MessageBox.Show("请重新选中主板", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                    return;
                }

                List<ChannelTreeItemViewModel> channels = await _hardwareService.ForceDeleteCard(ServerIP, maincard.MainControlCardIP);

                await DeleteChannel(ServerIP, channels);

                if (MainCard != null)
                {
                    MainCard.Clear();
                }
                if (WirelessReceiveCard != null)
                {
                    WirelessReceiveCard.Clear();
                }
                if (Cards != null)
                {
                    Cards.Clear();
                }
                if (Slots != null)
                {
                    Slots.Clear();
                }
                if (Channels != null)
                {
                    Channels.Clear();
                }
                SelectedMainCardTree = null;
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("删除数采器", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        private async void DownLoadCard(object para)
        {
            var server = ServerTreeItems.Where(p => p.ServerIP == ServerIP).FirstOrDefault();
            if (server == null)
            {
                return;
            }
            var maincard = server.Children.OfType<MainCardTreeItemViewModel>().Where(p => p.MainControlCardIP == MainControlCardIPEdit).FirstOrDefault();
            if (maincard == null)
            {
#if XBAP
                MessageBox.Show("请重新选中主板", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请重新选中主板", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }
            string json = GetRootjson(RequestType.ModifiedConfig, maincard, maincard.ServerIP);
            try
            {
                Status = ViewModelStatus.Querying;
                var root = await _databaseComponent.ComplexWithJson(maincard.ServerIP, maincard.MainControlCardIP, json, null, null, null);
                if (root != null)
                {
                    ClearEdited(maincard);
                }
                else
                {
#if XBAP
                    MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                    Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("下发数采器", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        private void SaveCard(object para)
        {
            var server = ServerTreeItems.Where(p => p.ServerIP == ServerIP).FirstOrDefault();
            if (server == null)
            {
                return;
            }
            var maincard = server.Children.OfType<MainCardTreeItemViewModel>().Where(p => p.MainControlCardIP == MainControlCardIPEdit).FirstOrDefault();
            if (maincard == null)
            {
#if XBAP
                MessageBox.Show("请重新选中主板", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请重新选中主板", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }
            string json = GetRootjson(RequestType.ModifiedConfig, maincard, maincard.ServerIP);
            using (StreamWriter sw = new StreamWriter("主板" + maincard.MainControlCardIP + "配置文件" + DateTime.Now.ToString("yyyy-MM-dd")+ ".json" , false))
            {               
                sw.Write(json);
                sw.Close();
                string dir = System.AppDomain.CurrentDomain.BaseDirectory;
                System.Diagnostics.Process.Start("explorer.exe", Path.GetFullPath(dir));
            }
        }

        private async void AddTransmissionCard(object para)
        {
            if (TransmissionCardIdentifier == null || TransmissionCardIdentifier == "")
            {
                return;
            }
            var server = ServerTreeItems.Where(p => p.ServerIP == ServerIP).FirstOrDefault();
            if (server == null)
            {
                return;
            }
            var maincard = server.Children.OfType<MainCardTreeItemViewModel>().Where(p => p.MainControlCardIP == MainControlCardIPEdit).FirstOrDefault();
            if (maincard == null)
            {
#if XBAP
                MessageBox.Show("请重新选中主板", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请重新选中主板", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }

            try
            {
                Status = ViewModelStatus.Querying;
                var rootcard = GetRootCard(RequestType.ModifiedConfig, maincard, maincard.ServerIP);
                if (rootcard.WirelessReceiveCard == null)
                {
                    rootcard.WirelessReceiveCard = new WirelessReceiveCard();
                }
                if (rootcard.WirelessReceiveCard.TransmissionCard == null)
                {
                    rootcard.WirelessReceiveCard.TransmissionCard = new List<TransmissionCard>();
                }

                if (rootcard.WirelessReceiveCard.TransmissionCard.Where(p => p.SlaveIdentifier == TransmissionCardIdentifier).Count() > 0)
                {
#if XBAP
                    MessageBox.Show("传输卡重名,请重新添加！！！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                    Xceed.Wpf.Toolkit.MessageBox.Show("传输卡重名,请重新添加！！！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                    return;
                }

                TransmissionCard transmissioncard = new TransmissionCard()
                {
                    SlaveIdentifier = TransmissionCardIdentifier,
                    TransmissionType = 0,
                    TransmissionName = "新建传输卡",
                    Version = "",
                    WorkTime = 1,
                    SleepTime = 1200,
                    BatteryEnergy = 1,
                    Remarks = "",
                };
                rootcard.WirelessReceiveCard.TransmissionCard.Add(transmissioncard);
                string json = JsonConvert.SerializeObject(rootcard);
                rootcard.WirelessReceiveCard.TransmissionCard.Remove(transmissioncard);
                List<ChannelTreeItemViewModel> channels = await _hardwareService.AddTransmissionCard(server.ServerIP, maincard.MainControlCardIP, TransmissionCardIdentifier, json);
                await AddChannel(server.ServerIP, channels);

                UpdateSelectedMainCard(SelectedMainCardTree);
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("添加传输卡", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        private async void DeleteTransmissionCard(object para)
        {
            if (TransmissionCardIdentifier == null || TransmissionCardIdentifier == "")
            {
                return;
            }
            var server = ServerTreeItems.Where(p => p.ServerIP == ServerIP).FirstOrDefault();
            if (server == null)
            {
                return;
            }
            var maincard = server.Children.OfType<MainCardTreeItemViewModel>().Where(p => p.MainControlCardIP == MainControlCardIPEdit).FirstOrDefault();
            if (maincard == null)
            {
#if XBAP
                MessageBox.Show("请重新选中主板", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请重新选中主板", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }
            var trancard = maincard.Children.OfType<WirelessReceiveCardTreeItemViewModel>().FirstOrDefault().Children.OfType<TransmissionCardTreeItemViewModel>().Where(p => p.SlaveIdentifier == TransmissionCardIdentifier).FirstOrDefault();
            if (trancard == null)
            {
#if XBAP
                MessageBox.Show("请重新选中接受卡！！！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("请重新选中接受卡！！！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                return;
            }

            try
            {
                Status = ViewModelStatus.Querying;

                var root = GetRootCard(RequestType.ModifiedConfig, maincard, maincard.ServerIP);
                var transmission = root.WirelessReceiveCard.TransmissionCard.Where(p => p.SlaveIdentifier == trancard.SlaveIdentifier).FirstOrDefault();
                root.WirelessReceiveCard.TransmissionCard.Remove(transmission);
                string json = JsonConvert.SerializeObject(root);
                root.WirelessReceiveCard.TransmissionCard.Add(transmission);

                List<ChannelTreeItemViewModel> channels = await _hardwareService.DeleteTransmissionCard(server.ServerIP, maincard.MainControlCardIP, trancard.SlaveIdentifier, json);
                await DeleteChannel(server.ServerIP, channels);

                Cards.Remove(transmission);

                //if (Slots != null)
                //{
                //    Slots.Clear();
                //}
                //if (Channels != null)
                //{
                //    Channels.Clear();
                //}
                //SelectedCard = null;               
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("删除传输卡", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        #endregion

        #region 复制板卡参数，有问题
        private void CopyPara(object para)
        {
            if (para is MainControlCard)
            {
                CopyMainControlCard = para as MainControlCard;
            }
        }

        private void PastePara(object para)
        {
            string str = para as string;
            switch (str)
            {
                case "MainControlCard":
                    {
                        if (CopyMainControlCard == null)
                        {
#if XBAP
                            MessageBox.Show((string)Application.Current.Resources["messagePDAManage8"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                            Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["messagePDAManage8"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                        }
                        var paras = CardCopyHelper.MainControlCardCopy(MainCard[0], CopyMainControlCard);
                        CardCopyView win = new CardCopyView(paras);
                        win.Parachanged += Win_Parachanged;
                        win.ShowDialog();
                        break;
                    }
            }
        }

        private async void Win_Parachanged(List<CardParaCopyModel> paras)
        {
            Status = ViewModelStatus.Querying;
            MainControlCard card = new MainControlCard();
            await Task.Run(() =>
            {
                CardCopyHelper.MainControlCardLeftCopyToRight(MainCard[0], card);//拷贝一份用于同步服务器
                CardCopyHelper.CopyToMainControlCard(card, paras);
                T1_MainControlCard t_card = _convertToDataBaseFormat.MainControlCardConvert(card, MainControlCardIP);

                System.Threading.Thread.Sleep(1000);
            });
            Status = ViewModelStatus.None;
            //发送给服务器,收到回应表示成功,更新界面。
            CardCopyHelper.MainControlCardLeftCopyToRight(card, MainCard[0]);
        }
        #endregion

        #region 回收站操作
        private void ClearRecycled(object para)
        {
#if XBAP
            MessageBox.Show((string)Application.Current.Resources["messagePDAManage9"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
            Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["messagePDAManage9"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
        }

        private async void RecoveredItem(object para)
        {
            try
            {
                var recycleditemtree = para as ItemTreeItemViewModel;//只有测点可以恢复
                if (recycleditemtree != null)
                {
                    var itemtree = SelectedOrganizationTree as ItemTreeItemViewModel;
                    if (itemtree != null)
                    {
                        if (itemtree.IsPaired == true)
                        {
#if XBAP
                            MessageBox.Show((string)Application.Current.Resources["messagePDAManage10"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                            Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["messagePDAManage10"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                            return;
                        }

                        var channeltree = _cardProcess.GetChannel(ServerTreeItems, recycleditemtree.T_Item);
                        //IChannel i_channel = _cardProcess.GetHardwareChannel(ServerTreeItems, itemtree.T_Item);
                        IChannel i_channel = channeltree.IChannel;
                        if (i_channel == null)
                        {
#if XBAP
                            MessageBox.Show((string)Application.Current.Resources["messagePDAManage11"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                            Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["messagePDAManage11"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                            return;
                        }

                        if (i_channel.T_Device_Guid != null && i_channel.T_Item_Guid != null)//htzk123Organization, over
                        {
#if XBAP
                            MessageBox.Show((string)Application.Current.Resources["messagePDAManage12"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                            Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["messagePDAManage12"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                            return;
                        }


                        Status = ViewModelStatus.Querying;

                        Organization organization = _cardProcess.GetNewOrganization(itemtree, recycleditemtree);
                        var organizations = _cardProcess.GetParentOrganizations(itemtree);//htzk123Organization,over
                        if (organizations != null && organizations.Count > 0)
                        {
                            organizations[0] = organization;
                        }

                        channeltree.BindTemp(recycleditemtree, organizations, itemtree.Parent as DeviceTreeItemViewModel);
                        recycleditemtree.BindTemp(channeltree, recycleditemtree.T_Item.IP, recycleditemtree.T_Item.Identifier, ServerIP, itemtree.T_Organization);
                        itemtree.T_Organization.UpdateTemp(organization);

                        Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                        editDic.Add("T_Item", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { recycleditemtree.T_Item.TempData }));
                        editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { i_channel.T_AbstractChannelInfo.TempData }));
                        editDic.Add("T_Organization", new Tuple<ICollection<string>, ICollection<object>>(null, new List<object> { itemtree.T_Organization.TempData }));

                        if (await _databaseComponent.Complex(ServerIP, null, editDic, null) == true)
                        {
                            BindNotify(channeltree, organizations, itemtree, recycleditemtree);
                        }
                        else
                        {
#if XBAP
                            MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                            Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                        }
                    }
                    else
                    {
#if XBAP
                        MessageBox.Show((string)Application.Current.Resources["messagePDAManage13"],"提示",MessageBoxButton.OK,MessageBoxImage.Warning);
#else
                        Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["messagePDAManage13"], "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                    }
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("恢复测点", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }
        #endregion

        #region 自动删除添加通道
        private async Task DeleteChannel(string serverip, List<ChannelTreeItemViewModel> channels)
        {
            if (channels != null && channels.Count > 0)
            {
                List<object> ids = new List<object>();
                List<ItemTreeItemViewModel> itemtrees_delete = new List<ItemTreeItemViewModel>();
                List<object> organizationIds = new List<object>();
                List<object> divfreIds = new List<object>();

                foreach (var channeltree in channels)
                {
                    //删除测点信息
                    if (channeltree.IsPaired == true)
                    {
                        var itemtree = _organizationService.ItemTreeItems.Where(p => p.IsPaired == true && p.T_Item.Guid.ToString() == channeltree.IChannel.T_Item_Guid).FirstOrDefault();
                        if (itemtree != null)
                        {
                            ids.Add(itemtree.T_Item.id);
                            itemtree.UnBindTemp();
                            itemtrees_delete.Add(itemtree);

                            organizationIds.AddRange(itemtree.Children.Select(p => p.T_Organization.id as object).ToList());
                            divfreIds.AddRange(itemtree.Children.OfType<DivFreTreeItemViewModel>().Select(p => p.T_DivFreInfo.id as object).ToList());
                        }
                    }
                }

                Dictionary<string, Tuple<string, ICollection<object>>> deleteDic = new Dictionary<string, Tuple<string, ICollection<object>>>();
                deleteDic.Add("T_Organization", new Tuple<string, ICollection<object>>("id", organizationIds));
                deleteDic.Add("T_DivFreInfo", new Tuple<string, ICollection<object>>("id", divfreIds));
                Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                editDic.Add("T_Item", new Tuple<ICollection<string>, ICollection<object>>(null, itemtrees_delete.Select(p => p.T_Item.TempData as object).ToList()));

                if (await _databaseComponent.Complex(serverip, null, editDic, deleteDic) == true)
                {
                    itemtrees_delete.ForEach(p =>
                    {
                        UnBindNotify(null, p);
                        foreach (var divfretree_child in p.Children)
                        {
                            DeleteDivfreNotify(divfretree_child as DivFreTreeItemViewModel);
                            p.RemoveChild(divfretree_child);
                        }
                    });
                }
                else
                {
#if XBAP
                    MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                    Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                }
            }
        }

        private async Task AddChannel(string serverip, List<ChannelTreeItemViewModel> channels)
        {
            //return;
            if (channels != null && channels.Count > 0)
            {
                List<AddChannelClass> addChannels = new List<AddChannelClass>();
                List<ForceDeleteChannelClass> deleteChannels = new List<ForceDeleteChannelClass>();

                foreach (var channeltree in channels)
                {
                    ForceDeleteChannelClass deletechannel = new ForceDeleteChannelClass();

                    if (channeltree.IChannel.T_Item_Guid != null && channeltree.IChannel.T_Item_Guid != "")
                    {
                        AddChannelClass addchannel = new AddChannelClass();

                        ItemTreeItemViewModel recycleditemtree = RecycledTreeItems[0].Children.OfType<ItemTreeItemViewModel>().Where(p => p.T_Item.Guid.ToString() == channeltree.IChannel.T_Item_Guid && p.T_Item.T_Device_Guid.ToString() == channeltree.IChannel.T_Device_Guid && p.T_Item.IP == channeltree.MainControlCardIP).FirstOrDefault(); //从回收站找测点
                                                                                                                                                                                                                                                                                                                                                     //恢复测点绑定
                        #region
                        if (recycleditemtree != null)
                        {
                            if (recycleditemtree.T_Item.Is_Disabled == false)//应该么有
                            {
                                deletechannel = SubForceDelete(channeltree);
                                deleteChannels.Add(deletechannel);
                                continue;
                            }
                            else//回收站回收
                            {
                                var device = _cardProcess.GetDevices(OrganizationTreeItems).Where(p => p.T_Organization.Guid.ToString() == channeltree.IChannel.T_Device_Guid).FirstOrDefault();
                                if (device == null)
                                {
                                    deletechannel = SubForceDelete(channeltree);
                                    deleteChannels.Add(deletechannel);
                                    continue;
                                }
                                ItemTreeItemViewModel old_item = null;
                                bool newnode = false;

                                if (device.Children != null && device.Children.Count > 0)
                                {
                                    old_item = device.Children.Where(p => (p as ItemTreeItemViewModel).T_Organization.Name == channeltree.IChannel.T_Item_Name).Select(p => p as ItemTreeItemViewModel).FirstOrDefault();
                                    if (old_item.IsPaired == true)
                                    {
                                        deletechannel = SubForceDelete(channeltree);
                                        deleteChannels.Add(deletechannel);
                                        continue;
                                    }
                                }
                                if (old_item == null)
                                {
                                    old_item = new ItemTreeItemViewModel(channeltree.IChannel.T_Item_Name, device);
                                    newnode = true;
                                }

                                Organization organization = new Organization();
                                organization.Name = channeltree.IChannel.T_Item_Name;
                                organization.Code = channeltree.IChannel.T_Item_Code;
                                organization.Guid = channeltree.IChannel.T_Item_Guid;
                                organization.Level = old_item.T_Organization.Level;
                                organization.Create_Time = recycleditemtree.T_Item.Create_Time.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                                organization.Modify_Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                organization.Parent_Code = channeltree.IChannel.T_Device_Code;
                                organization.Parent_Guid = channeltree.IChannel.T_Device_Guid;
                                organization.Parent_Level = device.T_Organization.Level;

                                recycleditemtree.BindTemp(channeltree, recycleditemtree.T_Item.IP, recycleditemtree.T_Item.Identifier, ServerIP, old_item.T_Organization);

                                old_item.T_Organization.UpdateTemp(organization);

                                List<object> t_organizations = new List<object>();

                                if (newnode == true)
                                {
                                    t_organizations.Add(old_item.T_Organization.TempData);
                                }

                                List<DivFreTreeItemViewModel> divFreTreelist = new List<DivFreTreeItemViewModel>();

                                if (channeltree.IChannel is DivFreIChannel)
                                {
                                    int sort_no = old_item.Children.Count;
                                    if (old_item.Children.Count > 0)
                                    {
                                        sort_no = old_item.Children[old_item.Children.Count - 1].T_Organization.Sort_No + 1;
                                    }

                                    foreach (var divfreinfo in (channeltree.IChannel as DivFreIChannel).DivFreInfo)
                                    {
                                        if (divfreinfo.DivFreCode == -1)
                                        {
                                            continue;
                                        }

                                        T1_DivFreInfo t_divfreinfo = divfreinfo.T_DivFreInfo;//_convertToDataBaseFormat.DivFreInfoConvert(divfreinfo, old_item);                                       
                                        DivFreTreeItemViewModel childtree = new DivFreTreeItemViewModel(old_item, t_divfreinfo, true, old_item.T_Organization.TempData, sort_no++);
                                        divFreTreelist.Add(childtree);
                                        t_organizations.Add(childtree.T_Organization);
                                    }
                                }

                                addchannel.channeltree = channeltree;
                                addchannel.devicetree = device;
                                addchannel.itemtree = old_item;
                                addchannel.recycleditemtree = recycleditemtree;
                                addchannel.divtreelist = divFreTreelist;
                                addchannel.newnode = newnode;
                                addchannel.t_organizations = t_organizations;
                                addChannels.Add(addchannel);
                            }
                        }
                        #endregion
                        else
                        {
                            deletechannel = SubForceDelete(channeltree);
                            deleteChannels.Add(deletechannel);
                        }

                    }
                    else
                    {
                        deletechannel = SubForceDelete(channeltree);
                        deleteChannels.Add(deletechannel);
                    }
                }

                Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                Dictionary<string, ICollection<object>> addDic = new Dictionary<string, ICollection<object>>();
                Dictionary<string, Tuple<string, ICollection<object>>> deleteDic = new Dictionary<string, Tuple<string, ICollection<object>>>();

                List<object> all_addorganizations = new List<object>();
                List<object> all_editorganizations = new List<object>();
                List<object> all_edititems = new List<object>();
                foreach (var addchannnel in addChannels)
                {
                    if (addchannnel.newnode == false)
                    {
                        all_editorganizations.Add(addchannnel.itemtree.T_Organization.TempData);
                    }

                    all_addorganizations.AddRange(addchannnel.t_organizations);
                    all_edititems.Add(addchannnel.recycleditemtree.T_Item.TempData);
                }

                List<object> all_deletedivfre = new List<object>();
                List<object> all_editchannels = new List<object>();
                foreach (var deletechannel in deleteChannels)
                {
                    all_deletedivfre.AddRange(deletechannel.divFreInfolist.Select(p => p.T_DivFreInfo.id as object));
                    all_editchannels.Add(deletechannel.channeltree.IChannel.T_AbstractChannelInfo.TempData);
                }

                editDic.Add("T_Item", new Tuple<ICollection<string>, ICollection<object>>(null, all_edititems));
                editDic.Add("T_Organization", new Tuple<ICollection<string>, ICollection<object>>(null, all_editorganizations));
                addDic.Add("T_Organization", all_addorganizations);
                deleteDic.Add("T_DivFreInfo", new Tuple<string, ICollection<object>>("id", all_deletedivfre));
                editDic.Add("T_AbstractChannelInfo", new Tuple<ICollection<string>, ICollection<object>>(null, all_editchannels));

                if (await _databaseComponent.Complex(serverip, addDic, editDic, null) == true)
                {
                    foreach (var addchannnel in addChannels)
                    {
                        addchannnel.devicetree.AddChild(addchannnel.itemtree);
                        BindNotify(null, null, addchannnel.itemtree, addchannnel.recycleditemtree);
                        _organizationService.AddItem(addchannnel.itemtree);

                        if (addchannnel.channeltree.IChannel is DivFreIChannel)
                        {
                            foreach (var divFreTree in addchannnel.divtreelist)
                            {
                                addchannnel.itemtree.Children.Add(divFreTree);
                                AddDivfreNotify(divFreTree);
                            }
                        }
                    }

                    foreach (var deletechannel in deleteChannels)
                    {
                        if (deletechannel.divfreinfo != null)
                        {
                            deletechannel.divFreInfolist.ForEach(p => deletechannel.divfreinfo.Remove(p));
                        }

                        UnBindNotify(deletechannel.channeltree, null);
                    }
                }
            }
        }

        private ForceDeleteChannelClass SubForceDelete(ChannelTreeItemViewModel channeltree)
        {
            ForceDeleteChannelClass deletechannel = new ForceDeleteChannelClass();
            IChannel i_channel = channeltree.IChannel;
            List<DivFreInfo> divFreInfolist = new List<DivFreInfo>();

            //解除绑定通道    
            channeltree.UnBindTemp();

            ObservableCollection<DivFreInfo> divfreinfo = null;
            if (i_channel is DivFreIChannel)
            {
                divfreinfo = (i_channel as DivFreIChannel).DivFreInfo;
                divFreInfolist = _cardProcess.GetDivFreInfoListWithoutTemp(i_channel as DivFreIChannel);
            }

            deletechannel.channeltree = channeltree;
            deletechannel.divFreInfolist = divFreInfolist;
            deletechannel.divfreinfo = divfreinfo;
            return deletechannel;
        }
        #endregion

        private void MultiSelected(bool isMulti)
        {
            SlotChanged(SelectedSlot);
        }

    }

}

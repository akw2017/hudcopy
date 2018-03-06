using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AIC.ServiceInterface;
using AIC.Core.LMModels;
using AIC.Resources.Models;
using AIC.Core.Helpers;
using AIC.Core.OrganizationModels;
using Prism.Commands;
using Microsoft.Win32;
using System.Windows;
using System.Data;
using System.IO;
using AIC.Core.Models;
using AIC.PDAPage.Models;

namespace AIC.PDAPage.ViewModels
{
    class ExportDBDataViewModel : BindableBase
    {           
        private readonly IOrganizationService _organizationService;
        private readonly ICardProcess _cardProcess;
        private readonly IDatabaseComponent _databaseComponent;
        private readonly ILocalConfiguration _localConfiguration;
        private readonly IHardwareService _hardwareService;

        public ExportDBDataViewModel(IOrganizationService organizationService, ICardProcess cardProcess, IDatabaseComponent databaseComponent, ILocalConfiguration localConfiguration, IHardwareService hardwareService)
        {      
            _organizationService = organizationService;
            _cardProcess = cardProcess;
            _databaseComponent = databaseComponent;
            _localConfiguration = localConfiguration;
            _hardwareService = hardwareService;

            ServerIPCategory = _databaseComponent.GetServerIPCategory();
            ServerIP = _databaseComponent.MainServerIp;

            InitTree();

        }
        #region 属性与字段
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

        public List<OrganizationTreeItemViewModel> OrganizationTreeItems { get; set; }
        public List<ItemTreeItemViewModel> ItemTreeItems { get; set; }

        public List<ServerTreeItemViewModel> ServerTreeItems { get; set; }

        private List<T2_Organization> organizationView;
        public List<T2_Organization> OrganizationView
        {
            get { return organizationView; }
            set
            {
                organizationView = value;
                OnPropertyChanged("OrganizationView");
            }
        }

        private List<T2_Item> itemView;
        public List<T2_Item> ItemView
        {
            get { return itemView; }
            set
            {
                itemView = value;
                OnPropertyChanged("ItemView");
            }
        }

        private List<I_WirelessChannelExport> wirelessScalarChannelInfoView;
        public List<I_WirelessChannelExport> WirelessScalarChannelInfoView
        {
            get { return wirelessScalarChannelInfoView; }
            set
            {
                wirelessScalarChannelInfoView = value;
                OnPropertyChanged("WirelessScalarChannelInfoView");
            }
        }

        private List<I_WirelessChannelExport> wirelessVibrationChannelInfoView;
        public List<I_WirelessChannelExport> WirelessVibrationChannelInfoView
        {
            get { return wirelessVibrationChannelInfoView; }
            set
            {
                wirelessVibrationChannelInfoView = value;
                OnPropertyChanged("WirelessVibrationChannelInfoView");
            }
        }

        private bool organizationChecked = true;
        public bool OrganizationChecked
        {
            get { return organizationChecked; }
            set
            {
                organizationChecked = value;
                OnPropertyChanged("OrganizationChecked");
            }
        }

        private bool itemChecked = true;
        public bool ItemChecked
        {
            get { return itemChecked; }
            set
            {
                itemChecked = value;
                OnPropertyChanged("ItemChecked");
            }
        }

        private bool channelChecked = true;
        public bool ChannelChecked
        {
            get { return channelChecked; }
            set
            {
                channelChecked = value;
                OnPropertyChanged("ChannelChecked");
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
        #endregion

        #region 命令
        public DelegateCommand exportCommand;
        public DelegateCommand ExportCommand
        {
            get
            {
                if (exportCommand == null)
                {
                    exportCommand = new DelegateCommand(
                        () => Export(), () => CanOperate()
                        );
                }
                return exportCommand;
            }
        }

        public DelegateCommand refreshCommand;
        public DelegateCommand RefreshCommand
        {
            get
            {
                if (refreshCommand == null)
                {
                    refreshCommand = new DelegateCommand(
                        () => Refresh(), () => CanOperate()
                        );
                }
                return exportCommand;
            }
        }
        #endregion

        private bool CanOperate()
        {
            return CanOperate(null);
        }
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
            ExportCommand.RaiseCanExecuteChanged();
            RefreshCommand.RaiseCanExecuteChanged();
        }

        #region 初始化

        private void InitTree()
        {
            if (CanOperate() == true)
            {
                OrganizationTreeItems = new List<OrganizationTreeItemViewModel>(_organizationService.OrganizationTreeItems.Where(p => p.ServerIP == ServerIP));
                ItemTreeItems = new List<ItemTreeItemViewModel>(_organizationService.ItemTreeItems.Where(p => p.ServerIP == ServerIP));
                ServerTreeItems = new List<ServerTreeItemViewModel>(_hardwareService.ServerTreeItems.Where(p => p.ServerIP == ServerIP));

                //组织机构
                OrganizationView = _databaseComponent.GetOrganizationData(ServerIP).Select(p => ClassCopyHelper.AutoCopy<T1_Organization, T2_Organization>(p)).ToList();
                ExportDBHelper.GetOrganizationStructure(OrganizationView, OrganizationTreeItems);
                //测点
                ItemView = _databaseComponent.GetItemData(ServerIP).Select(p => ClassCopyHelper.AutoCopy<T1_Item, T2_Item>(p)).ToList();
                ExportDBHelper.GetItemStructure(ItemView, ItemTreeItems);

                WirelessScalarChannelInfoView = _databaseComponent.GetRootCard(ServerIP).T_WirelessScalarChannelInfo.Select(p => ClassCopyHelper.AutoCopy<T1_WirelessScalarChannelInfo, T2_WirelessScalarChannelInfo>(p) as I_WirelessChannelExport).ToList();
                ExportDBHelper.GetAbstractChannelInfo(WirelessScalarChannelInfoView, ServerTreeItems, _cardProcess);

                WirelessVibrationChannelInfoView = _databaseComponent.GetRootCard(ServerIP).T_WirelessVibrationChannelInfo.Select(p => ClassCopyHelper.AutoCopy<T1_WirelessVibrationChannelInfo, T2_WirelessVibrationChannelInfo>(p) as I_WirelessChannelExport).ToList();
                ExportDBHelper.GetAbstractChannelInfo(WirelessVibrationChannelInfoView, ServerTreeItems, _cardProcess);
            }
            else
            {
                OrganizationView = new List<T2_Organization>();
                ItemView = new List<T2_Item>();
                WirelessScalarChannelInfoView = new List<I_WirelessChannelExport>();
                WirelessVibrationChannelInfoView = new List<I_WirelessChannelExport>();
            }
        }
        #endregion

        #region 导出
        private void Export()
        {
            List<DataTable> tables = new List<DataTable>();
            if (ChannelChecked == true)
            {
                tables.Add(WirelessVibrationChannelInfoToDataTable(WirelessVibrationChannelInfoView));
                tables.Add(WirelessScalarChannelInfoToDataTable(WirelessScalarChannelInfoView));
            }
            if (ItemChecked == true)
            {
                tables.Add(ItemToDataTable(ItemView));
            }
            if (OrganizationChecked == true)
            {
                tables.Add(OrganizationToDataTable(OrganizationView));
            }  
            if (tables.Count == 0)
            {
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel (*.XLS)|*.xls"; ;
            if ((bool)(saveFileDialog.ShowDialog()))
            {
                Status = ViewModelStatus.Querying;
                ExcelHelper _excelHelper = new ExcelHelper();
                string message = _excelHelper.SaveToExcel(saveFileDialog.FileName, tables);
                if (message != null)
                {
#if XBAP
                    MessageBox.Show(message,"保存失败", MessageBoxButton.OK, MessageBoxImage.Information);
#else
                    Xceed.Wpf.Toolkit.MessageBox.Show(message, "保存失败", MessageBoxButton.OK, MessageBoxImage.Information);
#endif
                }
                string dir = saveFileDialog.FileName;
                System.Diagnostics.Process.Start("explorer.exe", Path.GetFullPath(dir));
                Status = ViewModelStatus.None;
            }
        }

        private DataTable OrganizationToDataTable(List<T2_Organization> organizations)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Organization";
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Level", typeof(string)));
            dt.Columns.Add(new DataColumn("Sort_No", typeof(string)));
            dt.Columns.Add(new DataColumn("Structure", typeof(string)));
            dt.Columns.Add(new DataColumn("NodeType", typeof(string)));
            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
            dt.Rows.Add("名字", "深度", "排序号", "结构", "节点类型", "备注");//第二行为描述
            foreach (var organzation in organizations)
            {
                dt.Rows.Add(organzation.Name, organzation.Level, organzation.Sort_No, organzation.Structure, organzation.NodeType, organzation.Remarks);
            }
            return dt;
        }
        private DataTable ItemToDataTable(List<T2_Item> items)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Item";
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Structure", typeof(string)));
            dt.Columns.Add(new DataColumn("IP", typeof(string)));
            dt.Columns.Add(new DataColumn("SlaveIdentifier", typeof(string)));
            dt.Columns.Add(new DataColumn("CardNum", typeof(string)));
            dt.Columns.Add(new DataColumn("SlotNum", typeof(string)));
            dt.Columns.Add(new DataColumn("CHNum", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemType", typeof(string)));
            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
            dt.Rows.Add("名字", "结构", "发送卡ID", "卡号", "槽号", "通道号", "测点类型", "备注");//第二行为描述
            foreach (var item in items)
            {
                dt.Rows.Add(item.Name, item.Structure, item.IP, item.SlaveIdentifier, item.CardNum, item.SlotNum, item.CHNum, item.ItemType, item.Remarks);
            }
            return dt;
        }
        private DataTable WirelessScalarChannelInfoToDataTable(List<I_WirelessChannelExport> channels)
        {
            DataTable dt = new DataTable();
            dt.TableName = "WirelessScalarChannelInfo";
            dt.Columns.Add(new DataColumn("T_Item_Name", typeof(string)));
            dt.Columns.Add(new DataColumn("MainControlCardIP", typeof(string)));
            dt.Columns.Add(new DataColumn("SlaveIdentifier", typeof(string)));
            dt.Columns.Add(new DataColumn("SlotNum", typeof(string)));
            dt.Columns.Add(new DataColumn("CHNum", typeof(string)));
            dt.Columns.Add(new DataColumn("SubCHNum", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt.Columns.Add(new DataColumn("IsBypass", typeof(string)));
            dt.Columns.Add(new DataColumn("DelayAlarmTime", typeof(string)));
            dt.Columns.Add(new DataColumn("NotOKDelayAlarmTime", typeof(string)));
            dt.Columns.Add(new DataColumn("DangerValue", typeof(string)));
            dt.Columns.Add(new DataColumn("DangerIsAllow", typeof(string)));
            dt.Columns.Add(new DataColumn("AlarmValue", typeof(string)));
            dt.Columns.Add(new DataColumn("AlarmIsAllow", typeof(string)));
            dt.Columns.Add(new DataColumn("PreAlarmValue", typeof(string)));
            dt.Columns.Add(new DataColumn("PreAlarmIsAllow", typeof(string)));
            dt.Columns.Add(new DataColumn("NormalValue", typeof(string)));
            dt.Columns.Add(new DataColumn("NormalIsAllow", typeof(string)));
            dt.Columns.Add(new DataColumn("LowNormalValue", typeof(string)));
            dt.Columns.Add(new DataColumn("LowNormalIsAllow", typeof(string)));
            dt.Columns.Add(new DataColumn("LowPreAlarmValue", typeof(string)));
            dt.Columns.Add(new DataColumn("LowPreAlarmIsAllow", typeof(string)));
            dt.Columns.Add(new DataColumn("LowAlarmValue", typeof(string)));
            dt.Columns.Add(new DataColumn("LowAlarmIsAllow", typeof(string)));
            dt.Columns.Add(new DataColumn("LowDangerValue", typeof(string)));
            dt.Columns.Add(new DataColumn("LowDangerIsAllow", typeof(string)));
            dt.Rows.Add("测点名", "数采器IP", "接收卡ID", "槽号", "通道号", "子通道号", "单位", "旁路", "报警延时", "故障延时", "危险值", "危险启用", "警告值", "警告启用", "预警值", "预警启用"
                , "正常值", "正常启用", "低正常值", "低正常启用", "低预警值", "低预警启用", "低警告值", "低警告启用", "低危险值", "低危险启用");//第二行为描述
            foreach (var channel in channels.OfType<T2_WirelessScalarChannelInfo>())
            {
                dt.Rows.Add(channel.T_Item_Name, channel.MainControlCardIP, channel.SlaveIdentifier, channel.SlotNum, channel.CHNum, channel.SubCHNum,
                    channel.Unit, channel.IsBypass, channel.DelayAlarmTime, channel.NotOKDelayAlarmTime,
                    channel.DangerValue, channel.DangerIsAllow, channel.AlarmValue, channel.AlarmIsAllow, channel.PreAlarmValue, channel.PreAlarmIsAllow, channel.NormalValue, channel.NormalIsAllow,
                    channel.LowNormalValue, channel.LowNormalIsAllow, channel.LowPreAlarmValue, channel.LowPreAlarmIsAllow, channel.LowAlarmValue, channel.LowAlarmIsAllow, channel.LowDangerValue, channel.LowDangerIsAllow);
            }
            return dt;
        }
        private DataTable WirelessVibrationChannelInfoToDataTable(List<I_WirelessChannelExport> channels)
        {
            DataTable dt = new DataTable();
            dt.TableName = "WirelessVibrationChannelInfo";
            dt.Columns.Add(new DataColumn("T_Item_Name", typeof(string)));
            dt.Columns.Add(new DataColumn("MainControlCardIP", typeof(string)));
            dt.Columns.Add(new DataColumn("SlaveIdentifier", typeof(string)));
            dt.Columns.Add(new DataColumn("SlotNum", typeof(string)));
            dt.Columns.Add(new DataColumn("CHNum", typeof(string)));
            dt.Columns.Add(new DataColumn("SubCHNum", typeof(string)));         
            dt.Columns.Add(new DataColumn("IsBypass", typeof(string)));
            dt.Columns.Add(new DataColumn("DelayAlarmTime", typeof(string)));
            dt.Columns.Add(new DataColumn("NotOKDelayAlarmTime", typeof(string)));
            dt.Columns.Add(new DataColumn("DefaultRPM", typeof(string)));
            dt.Columns.Add(new DataColumn("VelocityCalibration", typeof(string)));
            dt.Columns.Add(new DataColumn("DisplacementCalibration", typeof(string)));
            dt.Columns.Add(new DataColumn("DangerValue", typeof(string)));
            dt.Columns.Add(new DataColumn("DangerIsAllow", typeof(string)));
            dt.Columns.Add(new DataColumn("DangerIsACQWave", typeof(string)));
            dt.Columns.Add(new DataColumn("AlarmValue", typeof(string)));
            dt.Columns.Add(new DataColumn("AlarmIsAllow", typeof(string)));
            dt.Columns.Add(new DataColumn("AlarmIsACQWave", typeof(string)));
            dt.Columns.Add(new DataColumn("PreAlarmValue", typeof(string)));
            dt.Columns.Add(new DataColumn("PreAlarmIsAllow", typeof(string)));
            dt.Columns.Add(new DataColumn("PreAlarmIsACQWave", typeof(string)));
            dt.Columns.Add(new DataColumn("NormalValue", typeof(string)));
            dt.Columns.Add(new DataColumn("NormalIsAllow", typeof(string)));
            dt.Columns.Add(new DataColumn("NormalIsACQWave", typeof(string)));
            dt.Columns.Add(new DataColumn("LowNormalValue", typeof(string)));
            dt.Columns.Add(new DataColumn("LowNormalIsAllow", typeof(string)));
            dt.Columns.Add(new DataColumn("LowNormalIsACQWave", typeof(string)));
            dt.Columns.Add(new DataColumn("LowPreAlarmValue", typeof(string)));
            dt.Columns.Add(new DataColumn("LowPreAlarmIsAllow", typeof(string)));
            dt.Columns.Add(new DataColumn("LowPreAlarmIsACQWave", typeof(string)));
            dt.Columns.Add(new DataColumn("LowAlarmValue", typeof(string)));
            dt.Columns.Add(new DataColumn("LowAlarmIsAllow", typeof(string)));
            dt.Columns.Add(new DataColumn("LowAlarmIsACQWave", typeof(string)));
            dt.Columns.Add(new DataColumn("LowDangerValue", typeof(string)));
            dt.Columns.Add(new DataColumn("LowDangerIsAllow", typeof(string)));
            dt.Columns.Add(new DataColumn("LowDangerIsACQWave", typeof(string)));
            dt.Rows.Add("测点名", "数采器IP", "接收卡ID", "槽号", "通道号", "子通道号", "旁路", "报警延时", "故障延时", "默认转速", "速度校准", "位移校准", "危险值", "危险启用", "危险波形", "警告值", "警告启用", "警告波形", "预警值", "预警启用", "预警波形"
                , "正常值", "正常启用", "正常波形", "低正常值", "低正常启用", "低正常波形", "低预警值", "低预警启用", "低预警波形", "低警告值", "低警告启用", "低警告波形", "低危险值", "低危险启用", "低危险波形");//第二行为描述
            foreach (var channel in channels.OfType<T2_WirelessVibrationChannelInfo>())
            {
                dt.Rows.Add(channel.T_Item_Name, channel.MainControlCardIP, channel.SlaveIdentifier, channel.SlotNum, channel.CHNum, channel.SubCHNum,
                    channel.IsBypass, channel.DelayAlarmTime, channel.NotOKDelayAlarmTime, channel.DefaultRPM, channel.VelocityCalibration, channel.DisplacementCalibration,
                    channel.DangerValue, channel.DangerIsAllow, channel.DangerIsACQWave, channel.AlarmValue, channel.AlarmIsAllow, channel.AlarmIsACQWave,
                    channel.PreAlarmValue, channel.PreAlarmIsAllow, channel.PreAlarmIsACQWave, channel.NormalValue, channel.NormalIsAllow, channel.NormalIsACQWave,
                    channel.LowNormalValue, channel.LowNormalIsAllow, channel.LowNormalIsACQWave, channel.LowPreAlarmValue, channel.LowPreAlarmIsAllow, channel.LowPreAlarmIsACQWave,
                    channel.LowAlarmValue, channel.LowAlarmIsAllow, channel.LowAlarmIsACQWave, channel.LowDangerValue, channel.LowDangerIsAllow, channel.LowDangerIsACQWave);
            }
            return dt;
        }
        #endregion

        private void Refresh()
        {

        }
    }
}

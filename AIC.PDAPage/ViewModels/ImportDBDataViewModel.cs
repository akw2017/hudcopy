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
using System;
using AIC.Core;
using AIC.PDAPage.Models;
using System.Threading.Tasks;

namespace AIC.PDAPage.ViewModels
{
    class ImportDBDataViewModel : BindableBase
    {           
        private readonly IOrganizationService _organizationService;
        private readonly ICardProcess _cardProcess;
        private readonly IDatabaseComponent _databaseComponent;
        private readonly ILoginUserService _loginUserService;
        private readonly IHardwareService _hardwareService;

        public ImportDBDataViewModel(IOrganizationService organizationService, ICardProcess cardProcess, IDatabaseComponent databaseComponent, ILoginUserService loginUserService, IHardwareService hardwareService)
        {      
            _organizationService = organizationService;
            _cardProcess = cardProcess;
            _databaseComponent = databaseComponent;
            _loginUserService = loginUserService;
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

        public event EventHandler NewMessageArrived;

        private string information;
        public string Information
        {
            get { return information; }
            set
            {
                if (information != value)
                {                   
                    information = value;
                    OnPropertyChanged("Information");
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

        public string waitInfo = "数据导入中...";
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
        public DelegateCommand importCommand;
        public DelegateCommand ImportCommand
        {
            get
            {
                if (importCommand == null)
                {
                    importCommand = new DelegateCommand(
                        () => Import(), () => CanOperate()
                        );
                }
                return importCommand;
            }
        }

        public DelegateCommand builtCommand;
        public DelegateCommand BuiltCommand
        {
            get
            {
                if (builtCommand == null)
                {
                    builtCommand = new DelegateCommand(
                        () => Built(), () => CanOperate()
                        );
                }
                return builtCommand;
            }
        }
        #endregion

        private bool CanOperate()
        {
            return CanOperate(null);
        }
        private bool CanOperate(object para)
        {
            if (_loginUserService.LoginInfo.ServerInfoList.Where(p => p.IP == ServerIP).Where(p => p.Permission.Contains("admin") || p.Permission.Contains("管理员")).Count() > 0)
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
            ImportCommand.RaiseCanExecuteChanged();
            BuiltCommand.RaiseCanExecuteChanged();
        }

        #region 初始化
        private void InitTree()
        {
            if (CanOperate() == true)
            {
                OrganizationTreeItems = new List<OrganizationTreeItemViewModel>(_organizationService.OrganizationTreeItems.Where(p => p.ServerIP == ServerIP));
                ItemTreeItems = new List<ItemTreeItemViewModel>(_organizationService.ItemTreeItems.Where(p => p.ServerIP == ServerIP));
                ServerTreeItems = new List<ServerTreeItemViewModel>(_hardwareService.ServerTreeItems.Where(p => p.ServerIP == ServerIP));
            }
        }
        #endregion

        #region 导入       
        private async void Import()
        {
           
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel (*.XLS)|*.xls"; ;
            if ((bool)(openFileDialog.ShowDialog()))
            {
                ExcelHelper _excelHelper = new ExcelHelper();
                try
                {
                    Status = ViewModelStatus.Querying;
                    SetInformation("导入","开始...", true);
                    await Task.Run(() =>
                    {
                        //获取Execl数据
                        SetInformation("Execl", "导入数据");
                        var tables = _excelHelper.OleDbLoadExcelSheet(openFileDialog.FileName);                      
                       
                        foreach (var table in tables)
                        {
                            table.Rows.RemoveAt(0);//删除注释行
                            if (table.TableName == "Organization")
                            {
                                #region
                                SetInformation("组织机构","开始导入...");
                                var olddatas = _databaseComponent.GetOrganizationData(ServerIP).Select(p => ClassCopyHelper.AutoCopy<T1_Organization, T2_Organization>(p)).ToList();
                                ExportDBHelper.GetOrganizationStructure(olddatas, OrganizationTreeItems);     
                                var newdatas = TableHelper<T2_Organization>.ConvertToModel(table).Select((value, index) => new { Index = index + 3, Value = value }).ToList();
                                //解析名字
                                newdatas.ForEach(p => p.Value.Names = ExportDBHelper.StructureConvertToNames(p.Value.Structure));
                                //检测数据有效性                                
                                var verifyStructure = newdatas.GroupBy(p => p.Value.Structure).Select(p => new {Count = p.Count(), Index = p.Select(i => i.Index).ToList() }).OrderByDescending(p => p.Count).FirstOrDefault();
                                if (verifyStructure != null && verifyStructure.Count > 1)
                                {
                                    string indexs = string.Empty;
                                    foreach(var index in verifyStructure.Index)
                                    {
                                        indexs += " 第" + index.ToString() + "行 ";
                                    }
                                    SetInformation("组织机构", "导入失败, exexl" + indexs + "数据Structure重复, 中断导入");
                                    return;
                                }
                                var verifySortNo = newdatas.GroupBy(p => new { p.Value.Level, p.Value.Sort_No, p.Value.ParentNamesString }).Select(p => new { Count = p.Count(), Index = p.Select(i => i.Index).ToList() }).OrderByDescending(p => p.Count).FirstOrDefault();
                                if (verifySortNo != null && verifySortNo.Count > 1)
                                {
                                    string indexs = string.Empty;
                                    foreach (var index in verifySortNo.Index)
                                    {
                                        indexs += " 第" + index.ToString() + "行 ";
                                    }
                                    SetInformation("组织机构", "导入失败, exexl" + indexs + "数据Level,Sort_No,Structure重复, 中断导入");
                                    return;
                                }
                                var verifyLevel = newdatas.GroupBy(p => new {Right = (p.Value.Level != p.Value.Names.Length - 1) }).Where(p => p.Key.Right == true).Select(p => new { Count = p.Count(), Index = p.Select(i => i.Index).ToList() }).FirstOrDefault();
                                if (verifyLevel != null)
                                {
                                    string indexs = string.Empty;
                                    foreach (var index in verifyLevel.Index)
                                    {
                                        indexs += " 第" + index.ToString() + "行 ";
                                    }
                                    SetInformation("组织机构", "导入失败, exexl" + indexs + "数据Level与Structure不匹配, 中断导入");
                                    return;
                                }
                                //开始匹配
                                List<T2_Organization> resolvedatas = new List<T2_Organization>();
                                foreach (var indexdata in newdatas)
                                {
                                    var newdata = indexdata.Value;
                                    var olddata = olddatas.Where(p => p.Structure == newdata.Structure).FirstOrDefault();
                                    if (olddata == null)
                                    {
                                        SetInformation("组织机构", "添加一条数据, execl第 " + indexdata.Index.ToString() + " 行");
                                        newdata.Operate = ImportOperate.Add;
                                        olddatas.Add(newdata);
                                        resolvedatas.Add(newdata);
                                    }
                                    else
                                    {
                                        if (ExportDBHelper.OrganizationEqual(newdata, olddata))
                                        {
                                            newdata.Operate = ImportOperate.None;
                                            resolvedatas.Add(olddata);
                                        }
                                        else
                                        {
                                            SetInformation("组织机构", "修改一条数据, execl第 " + indexdata.Index.ToString() + " 行");
                                            ExportDBHelper.OrganizationLeftCopyToRight(newdata, olddata);
                                            olddata.Operate = ImportOperate.Modify;
                                            resolvedatas.Add(olddata);
                                        }
                                    }
                                }
                                List<T2_Organization> deletes = olddatas.Except(resolvedatas).ToList();
                                foreach (var delete in deletes)
                                {
                                    delete.Operate = ImportOperate.Delete;
                                }
                                OrganizationView = new List<T2_Organization>(olddatas);
                                SetInformation("组织机构", "导入完成");
                                #endregion
                            }
                            else if (table.TableName == "Item")
                            {
                                #region
                                SetInformation("测点", "开始导入...");
                                var olddatas = _databaseComponent.GetItemData(ServerIP).Select(p => ClassCopyHelper.AutoCopy<T1_Item, T2_Item>(p)).ToList();
                                ExportDBHelper.GetItemStructure(olddatas, ItemTreeItems);
                                var newdatas = TableHelper<T2_Item>.ConvertToModel(table).Select((value, index) => new { Index = index + 3, Value = value }).ToList();
                                //解析名字
                                newdatas.ForEach(p => p.Value.Names = ExportDBHelper.StructureConvertToNames(p.Value.Structure));
                                //对齐发送卡Id
                                newdatas.ForEach(p => { if (p.Value.SlaveIdentifier != null) { p.Value.SlaveIdentifier = p.Value.SlaveIdentifier.PadLeft(4, '0'); } });
                                //检测数据有效性                                
                                var verifyStructure = newdatas.GroupBy(p => p.Value.Structure).Select(p => new { Count = p.Count(), Index = p.Select(i => i.Index).ToList() }).OrderByDescending(p => p.Count).FirstOrDefault();
                                if (verifyStructure != null && verifyStructure.Count > 1)
                                {
                                    string indexs = string.Empty;
                                    foreach (var index in verifyStructure.Index)
                                    {
                                        indexs += " 第" + index.ToString() + "行 ";
                                    }
                                    SetInformation("测点", "导入失败, exexl" + indexs + "数据Structure重复, 中断导入");
                                    return;
                                }
                                var verifySlaveIdentifier = newdatas.GroupBy(p => new { p.Value.IP, p.Value.SlaveIdentifier, p.Value.CardNum, p.Value.SlotNum, p.Value.CHNum }).Select(p => new { Count = p.Count(), Index = p.Select(i => i.Index).ToList() }).OrderByDescending(p => p.Count).FirstOrDefault();
                                if (verifySlaveIdentifier != null && verifySlaveIdentifier.Count > 1)
                                {
                                    string indexs = string.Empty;
                                    foreach (var index in verifySlaveIdentifier.Index)
                                    {
                                        indexs += " 第" + index.ToString() + "行 ";
                                    }
                                    SetInformation("测点", "导入失败, exexl" + indexs + "数据IP,SlaveIdentifier,CardNum,SlotNum,CHNum重复, 中断导入");
                                    return;
                                }
                                var verifyCardNum = newdatas.GroupBy(p => new { Right = ((p.Value.SlaveIdentifier == null && p.Value.CardNum <= 0) || (p.Value.SlaveIdentifier != null && p.Value.CardNum > 0)) }).Where(p => p.Key.Right == true).Select(p => new { Count = p.Count(), Index = p.Select(i => i.Index).ToList() }).FirstOrDefault();
                                if (verifyCardNum != null)
                                {
                                    string indexs = string.Empty;
                                    foreach (var index in verifyCardNum.Index)
                                    {
                                        indexs += " 第" + index.ToString() + "行 ";
                                    }
                                    SetInformation("组织机构", "导入失败, exexl" + indexs + "数据SlaveIdentifier与CardNum只能有一项有效, 中断导入");
                                    return;
                                }
                                //开始匹配
                                List<T2_Item> resolvedatas = new List<T2_Item>();
                                foreach (var indexdata in newdatas)
                                {
                                    var newdata = indexdata.Value;
                                    var olddata = olddatas.Where(p => p.Structure == newdata.Structure).FirstOrDefault();
                                    if (olddata == null)
                                    {
                                        SetInformation("测点", "添加一条数据, execl第 " + indexdata.Index.ToString() + " 行");
                                        newdata.Operate = ImportOperate.Add;
                                        olddatas.Add(newdata);
                                        resolvedatas.Add(newdata);
                                    }
                                    else
                                    {
                                        if (ExportDBHelper.ItemEqual(newdata, olddata))
                                        {
                                            newdata.Operate = ImportOperate.None;
                                            resolvedatas.Add(olddata);
                                        }
                                        else
                                        {
                                            SetInformation("测点", "修改一条数据, execl第 " + indexdata.Index.ToString() + " 行");
                                            ExportDBHelper.ItemLeftCopyToRight(newdata, olddata);
                                            olddata.Operate = ImportOperate.Modify;
                                            resolvedatas.Add(olddata);
                                        }
                                    }
                                }
                                List<T2_Item> deletes = olddatas.Except(resolvedatas).ToList();
                                foreach (var delete in deletes)
                                {
                                    delete.Operate = ImportOperate.Delete;
                                }
                                ItemView = new List<T2_Item>(olddatas);
                                SetInformation("测点", "导入完成");
                                #endregion
                            }
                            else if (table.TableName == "WirelessScalarChannelInfo")
                            {
                                #region
                                SetInformation("无线标量", "开始导入...");
                                var olddatas = _databaseComponent.GetRootCard(ServerIP).T_WirelessScalarChannelInfo.Select(p => ClassCopyHelper.AutoCopy<T1_WirelessScalarChannelInfo, T2_WirelessScalarChannelInfo>(p) as I_WirelessChannelExport).ToList();
                                ExportDBHelper.GetAbstractChannelInfo(olddatas, ServerTreeItems, _cardProcess);
                                var newdatas = TableHelper<T2_WirelessScalarChannelInfo>.ConvertToModel(table).Select((value, index) => new { Index = index + 3, Value = value as I_WirelessChannelExport }).ToList();
                                //对齐发送卡Id
                                newdatas.ForEach(p => { if (p.Value.SlaveIdentifier != null) { p.Value.SlaveIdentifier = p.Value.SlaveIdentifier.PadLeft(4, '0'); } });
                                //解析Alarm
                                newdatas.ForEach(p => p.Value.AlarmCategory = ExportDBHelper.GetAlarmCategory(
                                    p.Value.DangerValue, p.Value.AlarmValue, p.Value.PreAlarmValue, p.Value.NormalValue, p.Value.LowDangerValue, p.Value.LowAlarmValue, p.Value.LowPreAlarmValue, p.Value.LowNormalValue,
                                    p.Value.DangerIsAllow, p.Value.AlarmIsAllow, p.Value.PreAlarmIsAllow, p.Value.NormalIsAllow, p.Value.LowDangerIsAllow, p.Value.LowAlarmIsAllow, p.Value.LowPreAlarmIsAllow, p.Value.LowNormalIsAllow,
                                    false, false, false, false, false, false, false, false
                                    ));
                                //检测数据有效性       
                                var verifySlaveIdentifier = newdatas.GroupBy(p => new { p.Value.MainControlCardIP, p.Value.SlaveIdentifier, p.Value.SlotNum, p.Value.CHNum, p.Value.SubCHNum }).Select(p => new { Count = p.Count(), Index = p.Select(i => i.Index).ToList() }).OrderByDescending(p => p.Count).FirstOrDefault();
                                if (verifySlaveIdentifier != null && verifySlaveIdentifier.Count > 1)
                                {
                                    string indexs = string.Empty;
                                    foreach (var index in verifySlaveIdentifier.Index)
                                    {
                                        indexs += " 第" + index.ToString() + "行 ";
                                    }
                                    SetInformation("无线标量", "导入失败, exexl" + indexs + "数据MainControlCardIP,SlaveIdentifier,SlotNum,CHNum,SubCHNum重复, 中断导入");
                                    return;
                                }
                                var verifyDelayAlarmTime = newdatas.GroupBy(p => new { Right = (p.Value.DelayAlarmTime < 0 || p.Value.NotOKDelayAlarmTime < 0) }).Where(p => p.Key.Right == true).Select(p => new { Count = p.Count(), Index = p.Select(i => i.Index).ToList() }).FirstOrDefault();
                                if (verifyDelayAlarmTime != null)
                                {
                                    string indexs = string.Empty;
                                    foreach (var index in verifyDelayAlarmTime.Index)
                                    {
                                        indexs += " 第" + index.ToString() + "行 ";
                                    }
                                    SetInformation("无线标量", "导入失败, exexl" + indexs + "数据DelayAlarmTime与NotOKDelayAlarmTime不大于等于0, 中断导入");
                                    return;
                                }
                                var verifyAlarmCategory = newdatas.GroupBy(p => new { Right = !ExportDBHelper.VerifyAlarmCategory(p.Value.AlarmCategory) }).Where(p => p.Key.Right == true).Select(p => new { Count = p.Count(), Index = p.Select(i => i.Index).ToList() }).FirstOrDefault();
                                if (verifyAlarmCategory != null)
                                {
                                    string indexs = string.Empty;
                                    foreach (var index in verifyAlarmCategory.Index)
                                    {
                                        indexs += " 第" + index.ToString() + "行 ";
                                    }
                                    SetInformation("无线标量", "导入失败, exexl" + indexs + "数据Alarm设置不正常, 中断导入");
                                    return;
                                }
                                //开始匹配
                                List<I_WirelessChannelExport> resolvedatas = new List<I_WirelessChannelExport>();
                                foreach (var indexdata in newdatas)
                                {
                                    var newdata = indexdata.Value;
                                    var olddata = olddatas.Where(p => p.MainControlCardIP == newdata.MainControlCardIP && p.SlaveIdentifier == newdata.SlaveIdentifier && p.SlotNum == newdata.SlotNum && p.CHNum == newdata.CHNum && p.SubCHNum == newdata.SubCHNum).FirstOrDefault();
                                    if (olddata == null)
                                    {
                                        SetInformation("无线标量", "添加一条数据, execl第 " + indexdata.Index.ToString() + " 行");
                                        newdata.Operate = ImportOperate.Add;
                                        olddatas.Add(newdata);
                                        resolvedatas.Add(newdata);
                                    }
                                    else
                                    {
                                        if (ExportDBHelper.WirelessScalarChannelInfoEqual(newdata as T2_WirelessScalarChannelInfo, olddata as T2_WirelessScalarChannelInfo))
                                        {
                                            newdata.Operate = ImportOperate.None;
                                            resolvedatas.Add(olddata);
                                        }
                                        else
                                        {
                                            SetInformation("无线标量", "修改一条数据, execl第 " + indexdata.Index.ToString() + " 行");
                                            ExportDBHelper.WirelessScalarChannelInfoLeftCopyToRight(newdata as T2_WirelessScalarChannelInfo, olddata as T2_WirelessScalarChannelInfo);
                                            olddata.Operate = ImportOperate.Modify;
                                            resolvedatas.Add(olddata);
                                        }
                                    }
                                }
                                List<I_WirelessChannelExport> deletes = olddatas.Except(resolvedatas).ToList();
                                foreach (var delete in deletes)
                                {
                                    delete.Operate = ImportOperate.Delete;
                                }
                                WirelessScalarChannelInfoView = new List<I_WirelessChannelExport>(olddatas);
                                SetInformation("无线标量", "导入完成");
                                #endregion
                            }
                            else if (table.TableName == "WirelessVibrationChannelInfo")
                            {
                                #region
                                SetInformation("无线振动", "开始导入...");
                                var olddatas = _databaseComponent.GetRootCard(ServerIP).T_WirelessVibrationChannelInfo.Select(p => ClassCopyHelper.AutoCopy<T1_WirelessVibrationChannelInfo, T2_WirelessVibrationChannelInfo>(p) as I_WirelessChannelExport).ToList();
                                ExportDBHelper.GetAbstractChannelInfo(olddatas, ServerTreeItems, _cardProcess);
                                var newdatas = TableHelper<T2_WirelessVibrationChannelInfo>.ConvertToModel(table).Select((value, index) => new { Index = index + 3, Value = value as I_WirelessChannelExport }).ToList();
                                //对齐发送卡Id
                                newdatas.ForEach(p => { if (p.Value.SlaveIdentifier != null) { p.Value.SlaveIdentifier = p.Value.SlaveIdentifier.PadLeft(4, '0'); } });
                                //解析Alarm
                                newdatas.ForEach(p => p.Value.AlarmCategory = ExportDBHelper.GetAlarmCategory(
                                    p.Value.DangerValue, p.Value.AlarmValue, p.Value.PreAlarmValue, p.Value.NormalValue, p.Value.LowDangerValue, p.Value.LowAlarmValue, p.Value.LowPreAlarmValue, p.Value.LowNormalValue,
                                    p.Value.DangerIsAllow, p.Value.AlarmIsAllow, p.Value.PreAlarmIsAllow, p.Value.NormalIsAllow, p.Value.LowDangerIsAllow, p.Value.LowAlarmIsAllow, p.Value.LowPreAlarmIsAllow, p.Value.LowNormalIsAllow,
                                    p.Value.DangerIsACQWave, p.Value.AlarmIsACQWave, p.Value.PreAlarmIsACQWave, p.Value.NormalIsAllow, p.Value.LowDangerIsACQWave, p.Value.LowAlarmIsACQWave, p.Value.LowPreAlarmIsACQWave, p.Value.LowNormalIsAllow
                                    ));
                                //检测数据有效性       
                                var verifySlaveIdentifier = newdatas.GroupBy(p => new { p.Value.MainControlCardIP, p.Value.SlaveIdentifier, p.Value.SlotNum, p.Value.CHNum, p.Value.SubCHNum }).Select(p => new { Count = p.Count(), Index = p.Select(i => i.Index).ToList() }).OrderByDescending(p => p.Count).FirstOrDefault();
                                if (verifySlaveIdentifier != null && verifySlaveIdentifier.Count > 1)
                                {
                                    string indexs = string.Empty;
                                    foreach (var index in verifySlaveIdentifier.Index)
                                    {
                                        indexs += " 第" + index.ToString() + "行 ";
                                    }
                                    SetInformation("无线振动", "导入失败, exexl" + indexs + "数据MainControlCardIP,SlaveIdentifier,SlotNum,CHNum,SubCHNum重复, 中断导入");
                                    return;
                                }
                                var verifyDelayAlarmTime = newdatas.GroupBy(p => new { Right = (p.Value.DelayAlarmTime < 0 || p.Value.NotOKDelayAlarmTime < 0) }).Where(p => p.Key.Right == true).Select(p => new { Count = p.Count(), Index = p.Select(i => i.Index).ToList() }).FirstOrDefault();
                                if (verifyDelayAlarmTime != null)
                                {
                                    string indexs = string.Empty;
                                    foreach (var index in verifyDelayAlarmTime.Index)
                                    {
                                        indexs += " 第" + index.ToString() + "行 ";
                                    }
                                    SetInformation("无线振动", "导入失败, exexl" + indexs + "数据DelayAlarmTime与NotOKDelayAlarmTime不大于等于0, 中断导入");
                                    return;
                                }
                                var verifyAlarmCategory = newdatas.GroupBy(p => new { Right = !ExportDBHelper.VerifyAlarmCategory(p.Value.AlarmCategory) }).Where(p => p.Key.Right == true).Select(p => new { Count = p.Count(), Index = p.Select(i => i.Index).ToList() }).FirstOrDefault();
                                if (verifyAlarmCategory != null)
                                {
                                    string indexs = string.Empty;
                                    foreach (var index in verifyAlarmCategory.Index)
                                    {
                                        indexs += " 第" + index.ToString() + "行 ";
                                    }
                                    SetInformation("无线振动", "导入失败, exexl" + indexs + "数据Alarm设置不正常, 中断导入");
                                    return;
                                }
                                //开始匹配
                                List<I_WirelessChannelExport> resolvedatas = new List<I_WirelessChannelExport>();
                                foreach (var indexdata in newdatas)
                                {
                                    var newdata = indexdata.Value;
                                    var olddata = olddatas.Where(p => p.MainControlCardIP == newdata.MainControlCardIP && p.SlaveIdentifier == newdata.SlaveIdentifier && p.SlotNum == newdata.SlotNum && p.CHNum == newdata.CHNum && p.SubCHNum == newdata.SubCHNum).FirstOrDefault();
                                    if (olddata == null)
                                    {
                                        SetInformation("无线振动", "添加一条数据, execl第 " + indexdata.Index.ToString() + " 行");
                                        newdata.Operate = ImportOperate.Add;
                                        olddatas.Add(newdata);
                                        resolvedatas.Add(newdata);
                                    }
                                    else
                                    {
                                        if (ExportDBHelper.WirelessVibrationChannelInfoEqual(newdata as T2_WirelessVibrationChannelInfo, olddata as T2_WirelessVibrationChannelInfo))
                                        {
                                            newdata.Operate = ImportOperate.None;
                                            resolvedatas.Add(olddata);
                                        }
                                        else
                                        {
                                            SetInformation("无线振动", "修改一条数据, execl第 " + indexdata.Index.ToString() + " 行");
                                            ExportDBHelper.WirelessVibrationChannelInfoLeftCopyToRight(newdata as T2_WirelessVibrationChannelInfo, olddata as T2_WirelessVibrationChannelInfo);
                                            olddata.Operate = ImportOperate.Modify;
                                            resolvedatas.Add(olddata);
                                        }
                                    }
                                }
                                List<I_WirelessChannelExport> deletes = olddatas.Except(resolvedatas).ToList();
                                foreach (var delete in deletes)
                                {
                                    delete.Operate = ImportOperate.Delete;
                                }
                                WirelessVibrationChannelInfoView = new List<I_WirelessChannelExport>(olddatas);
                                SetInformation("无线振动", "导入完成");
                                #endregion
                            }
                        }
                        if (NewMessageArrived != null)
                        {
                            NewMessageArrived(this, EventArgs.Empty);
                        }
                    });
                }
                catch (Exception ex)
                {
                    SetInformation("导入", "失败" + ex.ToString());
                }
                finally
                {
                    Status = ViewModelStatus.None;
                }
            }
        }

        private void SetInformation(string name, string text, bool init = false)
        {
            if (init == true)
            {
                Information = name + text;
            }
            else
            {
                Information += "\n" + name + text;
            }
            if (NewMessageArrived != null)
            {
                NewMessageArrived(this, EventArgs.Empty);
            }
        }

        private void ClearView()
        {
            OrganizationView = new List<T2_Organization>();
            ItemView = new List<T2_Item>();
            WirelessScalarChannelInfoView = new List<I_WirelessChannelExport>();
            WirelessVibrationChannelInfoView = new List<I_WirelessChannelExport>();
        }
        #endregion

        private void Built()
        {

        }
    }
}

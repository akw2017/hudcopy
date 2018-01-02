using AIC.Core.ControlModels;
using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.SignalModels;
using AIC.CoreType;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AIC.Core.OrganizationModels
{
    public class ItemTreeItemViewModel : OrganizationTreeItemViewModel//测点组织机构树节点
    {
        private bool isRunning;
        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    if (Parent != null)
                    {
                        var alarms = Parent.Children.Where(o => (o as ItemTreeItemViewModel).T_Item != null && (o as ItemTreeItemViewModel).T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo).OrderBy(o => (o as ItemTreeItemViewModel).IsRunning).Select(o => (o as ItemTreeItemViewModel).IsRunning).ToList();
                        if (alarms.Count > 0)
                        {
                            int count = alarms.Count;
                            var alarm = alarms[count / 2];
                            (Parent as DeviceTreeItemViewModel).IsRunning = alarm;
                        }
                        //var isRunnings = Parent.Children.Where(o => (o as ItemTreeItemViewModel).IsRunning == true).Select(o => (o as ItemTreeItemViewModel).IsRunning);

                        //if (isRunnings == null || isRunnings.Count() == 0)
                        //{
                        //    (Parent as DeviceTreeItemViewModel).IsRunning = false;
                        //}
                        //else
                        //{
                        //    (Parent as DeviceTreeItemViewModel).IsRunning = true;
                        //}
                    }
                    OnPropertyChanged("IsRunning");
                }
            }
        }

        private bool isPaired;//绑定
        public bool IsPaired
        {
            get { return isPaired; }
            set
            {
                isPaired = value;
                OnPropertyChanged("IsPaired");
            }
        }

        private string error;//绑定
        public string Error
        {
            get { return error; }
            set
            {
                error = value;
                OnPropertyChanged("Error");
            }
        }

        private AlarmGrade alarm = AlarmGrade.HighNormal;
        public override AlarmGrade Alarm
        {
            get { return alarm; }
            set
            {
                if (alarm != value)
                {
                    alarm = value;
                    if (Parent != null)
                    {
                        Parent.Alarm = alarm;
                    }
                    OnPropertyChanged("Alarm");
                }
            }
        }

        private bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                if (isConnected != value)
                {
                    isConnected = value;
                    OnPropertyChanged("IsConnected");
                }
            }
        }

        public T1_Item T_Item { get; set; }
        public BaseAlarmSignal BaseAlarmSignal { get; set; }          
        public ItemTreeItemViewModel() { }

        public ItemTreeItemViewModel(T1_Organization organization)
        {
            T_Organization = organization;
        }
        public ItemTreeItemViewModel(T1_Item item)
        {
            T_Item = item;
            T_Organization = new T1_Organization();
            if (item != null)
            {
                T_Organization.Name = item.Name;
                T_Organization.Guid = item.Guid;
                T_Organization.Code = item.Code;
                T_Organization.Parent_Guid = item.T_Device_Guid;
                T_Organization.Parent_Code = item.T_Device_Code;               
            }            
        }
        public ItemTreeItemViewModel(DeviceTreeItemViewModel parent) : this("", parent) { }     
        public ItemTreeItemViewModel(string name, DeviceTreeItemViewModel parent) : base(name, parent)
        {
            if (name == "")
            {
                T_Organization.Name = "新建测点" + (T_Organization.Sort_No + 1).ToString();
            }
            else
            {
                T_Organization.Name = name;
            }
            item_init(parent);
        }
        public ItemTreeItemViewModel(string name, int sort_no, DeviceTreeItemViewModel parent) : base(name, sort_no, parent)
        {
            item_init(parent);          
        }
        public ItemTreeItemViewModel(T1_Organization organization, DeviceTreeItemViewModel parent) : base(organization.Name, organization.Sort_No, parent)
        {
            T_Organization = organization;
            item_init(parent);
        }      
        private void item_init(DeviceTreeItemViewModel parent)
        {
            T_Item = new T1_Item();
            T_Item.CardNum = -1;
            T_Item.SlotNum = -1;
            T_Item.CHNum = -1;
            T_Item.Guid = T_Organization.Guid;
            T_Item.Name = T_Organization.Name;
            T_Item.Code = T_Organization.Code;
            //T_Item.CardNum 
            //T_Item.SlotNum
            //T_Item.CHNum
            T_Item.T_Device_Guid = parent.T_Organization.Guid;
            T_Item.T_Device_Code = parent.T_Organization.Code;
            //T_Item.Remarks 
            T_Item.Create_Time = T_Organization.Create_Time;
            T_Item.Modify_Time = T_Organization.Modify_Time;
            T_Item.Sort_No = T_Organization.Sort_No;
            T_Item.Is_Disabled = T_Organization.Is_Disabled;
            //T_Item.IP      
            //T_Item.Identifier;
            //T_Item.ServerIP;
            //T_Item.ItemType;
            //T_Item.SlaveIdentifier;
            if (T_Item.CHNum == -1)
            {
                IsPaired = false;
            }
            else
            {
                IsPaired = true;
            }

            T_Organization.NodeType = 2;
        }
        public void RecoverBind(T1_Item t_item)
        {
            T_Item = t_item;
            IsPaired = true;
            //InitSignal();
        }
        public void BindItem(ChannelTreeItemViewModel channel, string ip, string identifier, string serverip)
        {
            IsPaired = true;

            if (channel.IChannel is WirelessVibrationChannelInfo || channel.IChannel is WirelessScalarChannelInfo)
            {
                T_Item.SlaveIdentifier = (channel.Parent.Parent as TransmissionCardTreeItemViewModel).TransmissionCard.SlaveIdentifier;
                T_Item.SlotNum = (channel.Parent as SlotTreeItemViewModel).SlotNum;
                T_Item.CHNum = channel.CHNum;
                //T_Item.ChannelHDID = ip + "_" + T_Item.SlaveIdentifier + "_" + T_Item.SlotNum + "_" + T_Item.CHNum;
                T_Item.ChannelHDID = T_Item.SlaveIdentifier.PadLeft(4, '0') + "_" + T_Item.SlotNum + "_" + T_Item.CHNum + "_0" + "@" + ip;
                if (channel.IChannel is WirelessVibrationChannelInfo)
                {
                    T_Item.ItemType = (int)ChannelType.WirelessVibrationChannelInfo;
                }
                else if (channel.IChannel is WirelessScalarChannelInfo)
                {
                    T_Item.ItemType = (int)ChannelType.WirelessScalarChannelInfo;
                }
            }
            else
            {
                T_Item.CardNum = (channel.Parent.Parent as WireMatchingCardTreeItemViewModel).CardNum;
                T_Item.SlotNum = (channel.Parent as SlotTreeItemViewModel).SlotNum;
                T_Item.CHNum = channel.CHNum;
                //T_Item.ChannelHDID = ip + "_" + T_Item.CardNum + "_" + T_Item.SlotNum + "_" + T_Item.CHNum;
                T_Item.ChannelHDID = T_Item.CardNum.Value.ToString("0000") + "_" + T_Item.SlotNum + "_" + T_Item.CHNum + "_0" + "@" + ip;
                if (channel.IChannel is IEPEChannelInfo)
                {
                    T_Item.ItemType = (int)ChannelType.IEPEChannelInfo;
                }
                else if (channel.IChannel is EddyCurrentDisplacementChannelInfo)
                {
                    T_Item.ItemType = (int)ChannelType.EddyCurrentDisplacementChannelInfo;
                }
                else if (channel.IChannel is EddyCurrentKeyPhaseChannelInfo)
                {
                    T_Item.ItemType = (int)ChannelType.EddyCurrentKeyPhaseChannelInfo;
                }
                else if (channel.IChannel is DigitTachometerChannelInfo)
                {
                    T_Item.ItemType = (int)ChannelType.DigitTachometerChannelInfo;
                }
                else if (channel.IChannel is AnalogRransducerInChannelInfo)
                {
                    T_Item.ItemType = (int)ChannelType.AnalogRransducerInChannelInfo;
                }
                else if (channel.IChannel is RelayChannelInfo)
                {
                    T_Item.ItemType = (int)ChannelType.RelayChannelInfo;
                }
                else if (channel.IChannel is DigitRransducerInChannelInfo)
                {
                    T_Item.ItemType = (int)ChannelType.DigitRransducerInChannelInfo;
                }
                else if (channel.IChannel is DigitRransducerOutChannelInfo)
                {
                    T_Item.ItemType = (int)ChannelType.DigitRransducerOutChannelInfo;
                }
                else if (channel.IChannel is AnalogRransducerOutChannelInfo)
                {
                    T_Item.ItemType = (int)ChannelType.AnalogRransducerOutChannelInfo;
                }
            }
            T_Item.IP = ip;
            T_Item.Identifier = identifier;
            T_Item.ServerIP = serverip;//废弃ServerIP，但数据库不允许为空，依旧填充
            T_Item.T_Device_Guid = T_Organization.Parent_Guid.Value;

            //InitSignal();
        }        
        private void ConnectDivFre(IChannel i_channel)
        {
            //连接分频信息            
        }
        private void BreakDivFre()
        {
            //断开分频信息
            Children.Clear();
        }
        public void InitSignal()
        {
            switch (T_Item.ItemType)
            {
                case (int)ChannelType.IEPEChannelInfo: BaseAlarmSignal = new IEPEChannelSignal(T_Item.Guid); break;
                case (int)ChannelType.EddyCurrentDisplacementChannelInfo: BaseAlarmSignal = new EddyCurrentDisplacementChannelSignal(T_Item.Guid); break;
                case (int)ChannelType.EddyCurrentKeyPhaseChannelInfo: BaseAlarmSignal = new EddyCurrentKeyPhaseChannelSignal(T_Item.Guid); break;
                case (int)ChannelType.DigitTachometerChannelInfo: BaseAlarmSignal = new DigitTachometerChannelSignal(T_Item.Guid); break;
                case (int)ChannelType.AnalogRransducerInChannelInfo: BaseAlarmSignal = new AnalogRransducerInChannelSignal(T_Item.Guid); break;
                case (int)ChannelType.RelayChannelInfo: BaseAlarmSignal = new RelayChannelSignal(T_Item.Guid); break;
                case (int)ChannelType.DigitRransducerInChannelInfo: BaseAlarmSignal = new DigitRransducerInChannelSignal(T_Item.Guid); break;
                case (int)ChannelType.DigitRransducerOutChannelInfo: BaseAlarmSignal = new DigitRransducerOutChannelSignal(T_Item.Guid); break;
                case (int)ChannelType.AnalogRransducerOutChannelInfo: BaseAlarmSignal = new AnalogRransducerOutChannelSignal(T_Item.Guid); break;
                case (int)ChannelType.WirelessVibrationChannelInfo: BaseAlarmSignal = new WirelessVibrationChannelSignal(T_Item.Guid); break;
                case (int)ChannelType.WirelessScalarChannelInfo: BaseAlarmSignal = new WirelessScalarChannelSignal(T_Item.Guid); break;
                default: return;
            }
            BaseAlarmSignal.ChannelHDID = T_Item.ChannelHDID;
        }
        //复制到回收站
        public ItemTreeItemViewModel RecycledItemTreeItem(ItemTreeItemViewModel old)
        {
            T_Organization = new T1_Organization();
            T_Organization.id = old.T_Organization.id;
            T_Organization.Name = old.T_Item.TempData.Name;
            T_Organization.Code = old.T_Organization.Code;
            T_Organization.Guid = old.T_Organization.Guid;
            T_Organization.Level = old.T_Organization.Level;
            T_Organization.Sort_No = old.T_Organization.Sort_No;
            T_Organization.Create_Time = old.T_Organization.Create_Time;
            T_Organization.Modify_Time = old.T_Item.TempData.Modify_Time;
            T_Organization.Is_Disabled = true;
            T_Organization.Parent_Code = old.T_Organization.Parent_Code;
            T_Organization.Parent_Guid = old.T_Organization.Parent_Guid;
            T_Organization.Parent_Level = old.T_Organization.Parent_Level;
            T_Organization.Remarks = old.T_Organization.Remarks;
            T_Organization.NodeType = old.T_Organization.NodeType;

            T_Item = new T1_Item();
            T_Item.id = old.T_Item.id;
            T_Item.CardNum = old.T_Item.CardNum;
            T_Item.SlotNum = old.T_Item.SlotNum;
            T_Item.CHNum = old.T_Item.CHNum;
            T_Item.Guid = old.T_Item.Guid;
            T_Item.Name = old.T_Item.TempData.Name;
            T_Item.Code = old.T_Item.Code;
            T_Item.T_Device_Guid = old.T_Item.T_Device_Guid;
            T_Item.T_Device_Code = old.T_Item.T_Device_Code;
            T_Item.Remarks = old.T_Item.Remarks;
            T_Item.Create_Time = old.T_Item.Create_Time;
            T_Item.Modify_Time = old.T_Item.TempData.Modify_Time;
            T_Item.Sort_No = old.T_Item.Sort_No;
            T_Item.Is_Disabled = true;
            T_Item.IP = old.T_Item.IP;
            T_Item.Identifier = old.T_Item.Identifier;
            T_Item.ServerIP = old.T_Item.ServerIP;//废弃ServerIP，但数据库不允许为空，依旧填充
            T_Item.ItemType = old.T_Item.ItemType;
            T_Item.SlaveIdentifier = old.T_Item.SlaveIdentifier;
            T_Item.ChannelHDID = old.T_Item.ChannelHDID;

            IsPaired = false;
            IsExpanded = true;
            return this;
        }
        //从回收站取出
        public ItemTreeItemViewModel RestoredItemTreeItem(ItemTreeItemViewModel recycle)
        {
            if (T_Organization == null || T_Item == null)
            {
                return this;
            }
            T_Organization.GetTempData();

            /*
            T_Organization.Name = recycle.T_Organization.Name;
            T_Organization.Code = recycle.T_Organization.Code;
            T_Organization.Guid = recycle.T_Organization.Guid;
            //T_Organization.Level = recycle.T_Organization.Level;
            //T_Organization.Sort_No = recycle.T_Organization.Sort_No;
            T_Organization.Create_Time = recycle.T_Organization.Create_Time;
            T_Organization.Modify_Time = DateTime.Now;
            T_Organization.Is_Disabled = false;
            //T_Organization.Parent_Code = recycle.T_Organization.Parent_Code;
            //T_Organization.Parent_Guid = recycle.T_Organization.Parent_Guid;
            //T_Organization.Parent_Level = recycle.T_Organization.Parent_Level;
            T_Organization.Remarks = recycle.T_Organization.Remarks;
            //T_Organization.NodeType = recycle.T_Organization.NodeType;*/

            recycle.T_Item.GetTempData();
            T_Item.id = recycle.T_Item.id;
            T_Item.CardNum = recycle.T_Item.CardNum;
            T_Item.SlotNum = recycle.T_Item.SlotNum;
            T_Item.CHNum = recycle.T_Item.CHNum;
            T_Item.Guid = recycle.T_Item.Guid;
            T_Item.Name = recycle.T_Item.Name;
            T_Item.Code = recycle.T_Item.Code;
            T_Item.T_Device_Guid = this.T_Organization.Parent_Guid.Value;// recycle.T_Item.T_Device_Guid;
            T_Item.T_Device_Code = this.T_Organization.Parent_Code; //recycle.T_Item.T_Device_Code;
            T_Item.Remarks = recycle.T_Item.Remarks;
            T_Item.Create_Time = recycle.T_Item.Create_Time;
            T_Item.Modify_Time = recycle.T_Item.Modify_Time;
            //T_Item.Sort_No = recycle.T_Item.Sort_No;
            T_Item.Is_Disabled = false;
            T_Item.IP = recycle.T_Item.IP;
            T_Item.Identifier = recycle.T_Item.Identifier;
            T_Item.ServerIP = recycle.T_Item.ServerIP;//废弃ServerIP，但数据库不允许为空，依旧填充
            T_Item.ItemType = recycle.T_Item.ItemType;
            T_Item.SlaveIdentifier = recycle.T_Item.SlaveIdentifier;
            T_Item.ChannelHDID = recycle.T_Item.ChannelHDID;

            IsPaired = true;
            IsExpanded = true;

            return this;
        }

        public void BindTemp(ChannelTreeItemViewModel channel, string ip, string identifier, string serverip)
        {
            T_Item.BindTemp(channel, ip, identifier, serverip);
        }

        public void BindTemp(ChannelTreeItemViewModel channel, string ip, string identifier, string serverip, T1_Organization organization)
        {
            T_Item.BindTemp(channel, ip, identifier, serverip, organization);
        }

        public void BindItem()
        {
            IsPaired = true;
            if (T_Item.TempData != null)
            {
                T_Item.GetTempData();
            }
            if (T_Organization.TempData != null)
            {
                T_Organization.GetTempData();
            }
            //InitSignal();
        }

        public void UnBindTemp()
        {
            T_Organization.UnBindTemp();
            T_Item.UnBindTemp();           
        }

        public void UnBindItem()
        {
            IsPaired = false;
            if (T_Organization.TempData != null)
            {
                T_Organization.GetTempData();
            }
            if (T_Item.TempData != null)
            {
                T_Item.TempData = null;
                //T_Item.GetTempData();
            }
            item_init(Parent as DeviceTreeItemViewModel);

            //T_Item.SlaveIdentifier = null;
            //T_Item.CardNum = -1;
            //T_Item.SlotNum = -1;
            //T_Item.CHNum = -1;
            //T_Item.ItemType = -1;
            //T_Item.IP = null;
            //T_Item.Identifier = null;
            //T_Item.ServerIP = null;
            //T_Organization.Guid = Guid.NewGuid();
            //T_Item.ChannelHDID = null;
            BreakDivFre();
            BaseAlarmSignal = null;            
        }        
    }
}

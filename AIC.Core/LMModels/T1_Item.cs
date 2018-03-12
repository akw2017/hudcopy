using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public class T1_Item : T_Item, Iid
    {
        //public int id { get; set; }//自增ID
        //public Guid Guid { get; set; }//Guid
        //public string ChannelHDID{get;set;}
        //public string Name { get; set; }//名称
        //public string Code { get; set; }//代号
        //public int CardNum { get; set; }//卡号
        //public int SlotNum { get; set; }//槽号
        //public int CHNum { get; set; }//通道号
        //public Guid T_Device_Guid { get; set; }//设备Guid
        //public string T_Device_Code { get; set; }//设备代号
        //public string Remarks { get; set; }//描述
        //public DateTime? Create_Time { get; set; }//创建时间
        //public DateTime? Modify_Time { get; set; }//修改时间
        //public int Sort_No { get; set; }//排序号
        //public bool Is_Disabled { get; set; }//禁止显示
        //public string IP { get; set; }//数据器IP          
        //public string Identifier { get; set; }//数采器标识符 
        //public string ServerIP { get; set; }//服务器IP
        //public int ItemType { get; set; }//测点类型
        //public string SlaveIdentifier { get; set; }//发送卡ID

        public void BindTemp(ChannelTreeItemViewModel channel, string ip, string identifier, string serverIP)
        {
            SaveTempData();
            if (channel.IChannel is WirelessVibrationChannelInfo || channel.IChannel is WirelessScalarChannelInfo)
            {
                TempData.SlaveIdentifier = (channel.Parent.Parent as TransmissionCardTreeItemViewModel).TransmissionCard.SlaveIdentifier;
                TempData.SlotNum = (channel.Parent as SlotTreeItemViewModel).SlotNum;
                TempData.CHNum = channel.CHNum;               
                TempData.ChannelHDID = TempData.SlaveIdentifier.PadLeft(4, '0') + "_" + TempData.SlotNum + "_" + TempData.CHNum + "_0" + "@" + ip;
                if (channel.IChannel is WirelessVibrationChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.WirelessVibrationChannelInfo;
                }
                else if (channel.IChannel is WirelessScalarChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.WirelessScalarChannelInfo;
                }
            }
            else
            {
                TempData.CardNum = (channel.Parent.Parent as WireMatchingCardTreeItemViewModel).CardNum;
                TempData.SlotNum = (channel.Parent as SlotTreeItemViewModel).SlotNum;
                TempData.CHNum = channel.CHNum;            
                TempData.ChannelHDID = TempData.CardNum.Value.ToString("0000") + "_" + TempData.SlotNum + "_" + TempData.CHNum + "_0" + "@" + ip;
                if (channel.IChannel is IEPEChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.IEPEChannelInfo;
                }
                else if (channel.IChannel is EddyCurrentDisplacementChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.EddyCurrentDisplacementChannelInfo;
                }
                else if (channel.IChannel is EddyCurrentKeyPhaseChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.EddyCurrentKeyPhaseChannelInfo;
                }
                else if (channel.IChannel is DigitTachometerChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.DigitTachometerChannelInfo;
                }
                else if (channel.IChannel is AnalogRransducerInChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.AnalogRransducerInChannelInfo;
                }
                else if (channel.IChannel is RelayChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.RelayChannelInfo;
                }
                else if (channel.IChannel is DigitRransducerInChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.DigitRransducerInChannelInfo;
                }
                else if (channel.IChannel is DigitRransducerOutChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.DigitRransducerOutChannelInfo;
                }
                else if (channel.IChannel is AnalogRransducerOutChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.AnalogRransducerOutChannelInfo;
                }
            }
            TempData.IP = ip;
            TempData.Identifier = identifier;
            TempData.ServerIP = serverIP;//废弃ServerIP，但数据库不允许为空，依旧填充//改为初始化时候填充，覆盖数据库的ServerIP。
            TempData.Is_Disabled = false;
            TempData.Modify_Time = DateTime.Now;
        }

        public void BindTemp(ChannelTreeItemViewModel channel, string ip, string identifier, string serverIP, T1_Organization organization)
        {
            SaveTempData();
            if (channel.IChannel is WirelessVibrationChannelInfo || channel.IChannel is WirelessScalarChannelInfo)
            {
                TempData.SlaveIdentifier = (channel.Parent.Parent as TransmissionCardTreeItemViewModel).TransmissionCard.SlaveIdentifier;
                TempData.SlotNum = (channel.Parent as SlotTreeItemViewModel).SlotNum;
                TempData.CHNum = channel.CHNum;              
                TempData.ChannelHDID = TempData.SlaveIdentifier.PadLeft(4, '0') + "_" + TempData.SlotNum + "_" + TempData.CHNum + "_0" + "@" + ip;
                if (channel.IChannel is WirelessVibrationChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.WirelessVibrationChannelInfo;
                }
                else if (channel.IChannel is WirelessScalarChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.WirelessScalarChannelInfo;
                }
            }
            else
            {
                TempData.CardNum = (channel.Parent.Parent as WireMatchingCardTreeItemViewModel).CardNum;
                TempData.SlotNum = (channel.Parent as SlotTreeItemViewModel).SlotNum;
                TempData.CHNum = channel.CHNum;
                TempData.ChannelHDID = TempData.CardNum.Value.ToString("0000") + "_" + TempData.SlotNum + "_" + TempData.CHNum + "_0" + "@" + ip;
                if (channel.IChannel is IEPEChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.IEPEChannelInfo;
                }
                else if (channel.IChannel is EddyCurrentDisplacementChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.EddyCurrentDisplacementChannelInfo;
                }
                else if (channel.IChannel is EddyCurrentKeyPhaseChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.EddyCurrentKeyPhaseChannelInfo;
                }
                else if (channel.IChannel is DigitTachometerChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.DigitTachometerChannelInfo;
                }
                else if (channel.IChannel is AnalogRransducerInChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.AnalogRransducerInChannelInfo;
                }
                else if (channel.IChannel is RelayChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.RelayChannelInfo;
                }
                else if (channel.IChannel is DigitRransducerInChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.DigitRransducerInChannelInfo;
                }
                else if (channel.IChannel is DigitRransducerOutChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.DigitRransducerOutChannelInfo;
                }
                else if (channel.IChannel is AnalogRransducerOutChannelInfo)
                {
                    TempData.ItemType = (int)ChannelType.AnalogRransducerOutChannelInfo;
                }
            }
            TempData.IP = ip;
            TempData.Identifier = identifier;
            TempData.ServerIP = serverIP;//废弃ServerIP，但数据库不允许为空，依旧填充//改为初始化时候填充，覆盖数据库的ServerIP。
            TempData.Is_Disabled = false;
            TempData.Modify_Time = DateTime.Now;
            TempData.Name = organization.Name;
            TempData.T_Device_Guid = organization.Parent_Guid.Value;
            TempData.T_Device_Code = organization.Parent_Code;
        }

        public void UnBindTemp()
        {
            SaveTempData();
            TempData.Name = TempData.Name + "-" + GetEnumDescription((ChannelType)ItemType);
            TempData.Is_Disabled = true;          
            TempData.Modify_Time = DateTime.Now;
        }     
      
        public T1_Item TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_Item();
            TempData.id = this.id;
            TempData.Guid = this.Guid;
            TempData.ChannelHDID = this.ChannelHDID;
            TempData.Name = this.Name;
            TempData.Code = this.Code;
            TempData.CardNum = this.CardNum;
            TempData.SlotNum = this.SlotNum;
            TempData.CHNum = this.CHNum;
            TempData.T_Device_Guid = this.T_Device_Guid;
            TempData.T_Device_Code = this.T_Device_Code;
            TempData.Remarks = this.Remarks;
            TempData.Create_Time = this.Create_Time;
            TempData.Modify_Time = this.Modify_Time;
            TempData.Sort_No = this.Sort_No;
            TempData.Is_Disabled = this.Is_Disabled;
            TempData.IP = this.IP;
            TempData.Identifier = this.Identifier;
            TempData.ServerIP = this.ServerIP;//废弃ServerIP，但数据库不允许为空，依旧填充//改为初始化时候填充，覆盖数据库的ServerIP。
            TempData.ItemType = this.ItemType;
            TempData.SlaveIdentifier = this.SlaveIdentifier;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.Guid = TempData.Guid;
            this.ChannelHDID = TempData.ChannelHDID;
            this.Name = TempData.Name;
            this.Code = TempData.Code;
            this.CardNum = TempData.CardNum;
            this.SlotNum = TempData.SlotNum;
            this.CHNum = TempData.CHNum;
            this.T_Device_Guid = TempData.T_Device_Guid;
            this.T_Device_Code = TempData.T_Device_Code;
            this.Remarks = TempData.Remarks;
            this.Create_Time = TempData.Create_Time;
            this.Modify_Time = TempData.Modify_Time;
            this.Sort_No = TempData.Sort_No;
            this.Is_Disabled = TempData.Is_Disabled;
            this.IP = TempData.IP;
            this.Identifier = TempData.Identifier;
            this.ServerIP = TempData.ServerIP;//废弃ServerIP，但数据库不允许为空，依旧填充//改为初始化时候填充，覆盖数据库的ServerIP。
            this.ItemType = TempData.ItemType;
            this.SlaveIdentifier = TempData.SlaveIdentifier;
            TempData = null;
        }

        private string GetEnumDescription(Enum enumObj)
        {
            FieldInfo fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

            object[] attribArray = fieldInfo.GetCustomAttributes(false);

            if (attribArray.Length == 0)
            {
                return enumObj.ToString();
            }
            else
            {
                DescriptionAttribute attrib = attribArray[0] as DescriptionAttribute;
                return attrib.Description;
            }
        }

        public string GetHardWaveInfo()
        {
            switch(this.ItemType)
            {
                case (int)ChannelType.IEPEChannelInfo: 
                case (int)ChannelType.EddyCurrentDisplacementChannelInfo:
                case (int)ChannelType.EddyCurrentKeyPhaseChannelInfo:
                case (int)ChannelType.DigitTachometerChannelInfo:
                case (int)ChannelType.AnalogRransducerInChannelInfo:
                case (int)ChannelType.RelayChannelInfo: 
                case (int)ChannelType.DigitRransducerInChannelInfo: 
                case (int)ChannelType.DigitRransducerOutChannelInfo: 
                case (int)ChannelType.AnalogRransducerOutChannelInfo: 
                    {
                        return this.CardNum.Value.ToString("0000") + "_" + this.SlotNum + "_" + this.CHNum + "_0" + "@" + this.ServerIP;
                    }
                case (int)ChannelType.WirelessVibrationChannelInfo: 
                case (int)ChannelType.WirelessScalarChannelInfo:
                    {
                        return this.SlaveIdentifier.PadLeft(4, '0') + "_" + this.SlotNum + "_" + this.CHNum + "_0" + "@" + this.ServerIP; 
                    }
            }
            return null;
        }

        public string GetHardWaveType()
        {
            switch (this.ItemType)
            {
                case (int)ChannelType.IEPEChannelInfo:
                    {
                        return "IEPE";
                    }
                case (int)ChannelType.EddyCurrentDisplacementChannelInfo:
                    {
                        return "EddyCurrentDisplacement";
                    }
                case (int)ChannelType.EddyCurrentKeyPhaseChannelInfo:
                    {
                        return "EddyCurrentKeyPhase";
                    }
                case (int)ChannelType.DigitTachometerChannelInfo:
                    {
                        return "DigitTachometer";
                    }
                case (int)ChannelType.AnalogRransducerInChannelInfo:
                    {
                        return "AnalogRransducerIn";
                    }
                case (int)ChannelType.RelayChannelInfo:
                    {
                        return "Relay";
                    }
                case (int)ChannelType.DigitRransducerInChannelInfo:
                    {
                        return "DigitRransducerIn";
                    }
                case (int)ChannelType.DigitRransducerOutChannelInfo:
                    {
                        return "DigitRransducerOut";
                    }
                case (int)ChannelType.AnalogRransducerOutChannelInfo:
                    {
                        return "AnalogRransducerOut";
                    }
                case (int)ChannelType.WirelessVibrationChannelInfo:
                    {
                        return "WirelessVibration";
                    }
                case (int)ChannelType.WirelessScalarChannelInfo:
                    {
                        return "WirelessScalar";
                    }
            }
            return null;
        }
    }
}

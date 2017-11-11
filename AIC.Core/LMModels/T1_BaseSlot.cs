using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public interface I_BaseSlot : Iid
    {
        //public int id { get; set; }//自增ID        
        //public int Integration { get; set; }//积分
        //public string WaveInfo { get; set; }//波形信息
        //public string SampleMode { get; set; }//采样方式
        //public bool Is24V { get; set; }//24V供电
        //public string EddyCurrentRPMSampleInfo { get; set; }//电涡流采样信息
        //public bool IsEnableMainCH { get; set; }//主通道使能
        //public string Code { get; set; }//槽代号
        //public string T_AbstractSlotInfo_Code { get; set; }//抽象信息槽
        //public int SlotCode { get; set; }//槽类型
    }

    public class T1_IEPESlot : T_IEPESlot, I_BaseSlot//IEPE槽
    {
        //public int id { get; set; }//自增ID        
        //public int Integration { get; set; }//积分
        //public string WaveInfo { get; set; }//波形信息
        //public string SampleMode { get; set; }//采样方式    
        //public string Code { get; set; }//槽代号
        //public string T_AbstractSlotInfo_Code { get; set; }//抽象信息槽  
        public T1_IEPESlot TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_IEPESlot();
            TempData.id = this.id;
            TempData.Integration = this.Integration;
            TempData.WaveInfo = this.WaveInfo;
            TempData.SampleMode = this.SampleMode;
            TempData.Code = this.Code;
            TempData.T_AbstractSlotInfo_Code = this.T_AbstractSlotInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.Integration = TempData.Integration;
            this.WaveInfo = TempData.WaveInfo;
            this.SampleMode = TempData.SampleMode;
            this.Code = TempData.Code;
            this.T_AbstractSlotInfo_Code = TempData.T_AbstractSlotInfo_Code;
            TempData = null;
        }
    }

    public class T1_EddyCurrentDisplacementSlot : T_EddyCurrentDisplacementSlot, I_BaseSlot//电涡流位移槽
    {
        //public int id { get; set; }//自增ID   
        //public string WaveInfo { get; set; }//波形信息
        //public string SampleMode { get; set; }//采样方式
        //public bool Is24V { get; set; }//24V供电      
        //public string Code { get; set; }//槽代号
        //public string T_AbstractSlotInfo_Code { get; set; }//抽象信息槽
        public T1_EddyCurrentDisplacementSlot TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_EddyCurrentDisplacementSlot();
            TempData.id = this.id;         
            TempData.WaveInfo = this.WaveInfo;
            TempData.SampleMode = this.SampleMode;
            TempData.Is24V = this.Is24V;
            TempData.Code = this.Code;
            TempData.T_AbstractSlotInfo_Code = this.T_AbstractSlotInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;           
            this.WaveInfo = TempData.WaveInfo;
            this.SampleMode = TempData.SampleMode;
            this.Is24V = TempData.Is24V;
            this.Code = TempData.Code;
            this.T_AbstractSlotInfo_Code = TempData.T_AbstractSlotInfo_Code;
            TempData = null;
        }
    }

    public class T1_EddyCurrentKeyPhaseSlot : T_EddyCurrentKeyPhaseSlot, I_BaseSlot //电涡流键相槽
    {
        //public int id { get; set; }//自增ID        
        //public bool Is24V { get; set; }//24V供电
        //public string EddyCurrentRPMSampleInfo { get; set; }//电涡流采样信息      
        //public string Code { get; set; }//槽代号
        //public string T_AbstractSlotInfo_Code { get; set; }//抽象信息槽  
        public T1_EddyCurrentKeyPhaseSlot TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_EddyCurrentKeyPhaseSlot();
            TempData.id = this.id; 
            TempData.Is24V = this.Is24V;
            TempData.EddyCurrentRPMSampleInfo = this.EddyCurrentRPMSampleInfo;
            TempData.Code = this.Code;
            TempData.T_AbstractSlotInfo_Code = this.T_AbstractSlotInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;  
            this.Is24V = TempData.Is24V;
            this.EddyCurrentRPMSampleInfo = TempData.EddyCurrentRPMSampleInfo;
            this.Code = TempData.Code;
            this.T_AbstractSlotInfo_Code = TempData.T_AbstractSlotInfo_Code;
            TempData = null;
        }
    }

    public class T1_EddyCurrentTachometerSlot : T_EddyCurrentTachometerSlot, I_BaseSlot//电涡流转速表槽
    {
        //public int id { get; set; }//自增ID    
        //public bool Is24V { get; set; }//24V供电
        //public string EddyCurrentRPMSampleInfo { get; set; }//电涡流采样信息
        //public bool IsEnableMainCH { get; set; }//主通道使能
        //public string Code { get; set; }//槽代号
        //public string T_AbstractSlotInfo_Code { get; set; }//抽象信息槽   
        public T1_EddyCurrentTachometerSlot TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_EddyCurrentTachometerSlot();
            TempData.id = this.id;
            TempData.Is24V = this.Is24V;
            TempData.EddyCurrentRPMSampleInfo = this.EddyCurrentRPMSampleInfo;
            TempData.IsEnableMainCH = this.IsEnableMainCH;
            TempData.Code = this.Code;
            TempData.T_AbstractSlotInfo_Code = this.T_AbstractSlotInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.Is24V = TempData.Is24V;
            this.EddyCurrentRPMSampleInfo = TempData.EddyCurrentRPMSampleInfo;
            this.IsEnableMainCH = TempData.IsEnableMainCH;
            this.Code = TempData.Code;
            this.T_AbstractSlotInfo_Code = TempData.T_AbstractSlotInfo_Code;
            TempData = null;
        }
    }

    public class T1_DigitTachometerSlot : T_DigitTachometerSlot, I_BaseSlot//数字转速表槽
    {
        //public int id { get; set; }//自增ID     
        //public string Code { get; set; }//槽代号
        //public string T_AbstractSlotInfo_Code { get; set; }//抽象信息槽    
        public T1_DigitTachometerSlot TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_DigitTachometerSlot();
            TempData.id = this.id;           
            TempData.Code = this.Code;
            TempData.T_AbstractSlotInfo_Code = this.T_AbstractSlotInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;           
            this.Code = TempData.Code;
            this.T_AbstractSlotInfo_Code = TempData.T_AbstractSlotInfo_Code;
            TempData = null;
        }
    }

    public class T1_AnalogRransducerInSlot : T_AnalogRransducerInSlot, I_BaseSlot//模拟变送器输入槽
    {
        //public int id { get; set; }//自增ID     
        //public string Code { get; set; }//槽代号
        //public string T_AbstractSlotInfo_Code { get; set; }//抽象信息槽
        public T1_AnalogRransducerInSlot TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_AnalogRransducerInSlot();
            TempData.id = this.id;
            TempData.Code = this.Code;
            TempData.T_AbstractSlotInfo_Code = this.T_AbstractSlotInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.Code = TempData.Code;
            this.T_AbstractSlotInfo_Code = TempData.T_AbstractSlotInfo_Code;
            TempData = null;
        }
    }

    public class T1_RelaySlot : T_RelaySlot, I_BaseSlot//继电器槽
    {
        //public int id { get; set; }//自增ID
        //public string Code { get; set; }//槽代号
        //public string T_AbstractSlotInfo_Code { get; set; }//抽象信息槽
        public T1_RelaySlot TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_RelaySlot();
            TempData.id = this.id;
            TempData.Code = this.Code;
            TempData.T_AbstractSlotInfo_Code = this.T_AbstractSlotInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.Code = TempData.Code;
            this.T_AbstractSlotInfo_Code = TempData.T_AbstractSlotInfo_Code;
            TempData = null;
        }
    }

    public class T1_DigitRransducerInSlot : T_DigitRransducerInSlot, I_BaseSlot//数字变送器输入槽
    {
        //public int id { get; set; }//自增ID     
        //public string ModBusTCPIP { get; set; }//基于ModBus的TCPIP
        //public bool EnableModBusTCPIP { get; set; }//是否启用TCPIP
        //public string ModBus485 { get; set; }//基于ModBus的485
        //public bool EnableModBus485 { get; set; }//是否启用485
        //public string Code { get; set; }//槽代号
        //public string T_AbstractSlotInfo_Code { get; set; }//抽象信息槽    
        public T1_DigitRransducerInSlot TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_DigitRransducerInSlot();
            TempData.id = this.id;
            TempData.ModBusTCPIP = this.ModBusTCPIP;
            TempData.EnableModBusTCPIP = this.EnableModBusTCPIP;
            TempData.ModBus485 = this.ModBus485;
            TempData.EnableModBus485 = this.EnableModBus485;
            TempData.Code = this.Code;
            TempData.T_AbstractSlotInfo_Code = this.T_AbstractSlotInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.ModBusTCPIP = TempData.ModBusTCPIP;
            this.EnableModBusTCPIP = TempData.EnableModBusTCPIP;
            this.ModBus485 = TempData.ModBus485;
            this.EnableModBus485 = TempData.EnableModBus485;
            this.Code = TempData.Code;
            this.T_AbstractSlotInfo_Code = TempData.T_AbstractSlotInfo_Code;
            TempData = null;
        }
    }

    public class T1_DigitRransducerOutSlot : T_DigitRransducerOutSlot, I_BaseSlot//数字变送器输出槽
    {
        //public int id { get; set; }//自增ID    
        //public string ModBusTCPIP { get; set; }//基于ModBus的TCPIP
        //public bool EnableModBusTCPIP { get; set; }//是否启用TCPIP
        //public string ModBus485 { get; set; }//基于ModBus的485
        //public bool EnableModBus485 { get; set; }//是否启用485 
        //public string Code { get; set; }//槽代号
        //public string T_AbstractSlotInfo_Code { get; set; }//抽象信息槽   
        public T1_DigitRransducerOutSlot TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_DigitRransducerOutSlot();
            TempData.id = this.id;
            TempData.ModBusTCPIP = this.ModBusTCPIP;
            TempData.EnableModBusTCPIP = this.EnableModBusTCPIP;
            TempData.ModBus485 = this.ModBus485;
            TempData.EnableModBus485 = this.EnableModBus485;
            TempData.Code = this.Code;
            TempData.T_AbstractSlotInfo_Code = this.T_AbstractSlotInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.ModBusTCPIP = TempData.ModBusTCPIP;
            this.EnableModBusTCPIP = TempData.EnableModBusTCPIP;
            this.ModBus485 = TempData.ModBus485;
            this.EnableModBus485 = TempData.EnableModBus485;
            this.Code = TempData.Code;
            this.T_AbstractSlotInfo_Code = TempData.T_AbstractSlotInfo_Code;
            TempData = null;
        }
    }

    public class T1_AnalogRransducerOutSlot : T_AnalogRransducerOutSlot, I_BaseSlot//模拟变送器输出槽
    {
        //public int id { get; set; }//自增ID     
        //public string Code { get; set; }//槽代号
        //public string T_AbstractSlotInfo_Code { get; set; }//抽象信息槽  
        public T1_AnalogRransducerOutSlot TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_AnalogRransducerOutSlot();
            TempData.id = this.id;
            TempData.Code = this.Code;
            TempData.T_AbstractSlotInfo_Code = this.T_AbstractSlotInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.Code = TempData.Code;
            this.T_AbstractSlotInfo_Code = TempData.T_AbstractSlotInfo_Code;
            TempData = null;
        }
    }

    public class T1_WirelessScalarSlot : T_WirelessScalarSlot, I_BaseSlot//标量槽
    {
        //public int id { get; set; }//自增ID  
        //public int SlotNum { get; set; }//槽号   
        //public string Code { get; set; }//槽代号
        //public string T_TransmissionCard_Code { get; set; }//  
        public T1_WirelessScalarSlot TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_WirelessScalarSlot();
            TempData.id = this.id;
            TempData.SlotNum = this.SlotNum;
            TempData.Code = this.Code;
            TempData.T_TransmissionCard_Code = this.T_TransmissionCard_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.SlotNum = TempData.SlotNum;
            this.Code = TempData.Code;
            this.T_TransmissionCard_Code = TempData.T_TransmissionCard_Code;
            TempData = null;
        }
    }

    public class T1_WirelessVibrationSlot : T_WirelessVibrationSlot, I_BaseSlot//振动槽
    {
        //public int id { get; set; }//自增ID  
        //public int Integration { get; set; }//积分设置
        //public string Unit { get; set; }//单位
        //public string SampleFreCategory { get; set; }//采样频率种类
        //public int SampleFreCode { get; set; }//采样频率代码
        //public string SamplePointCategory { get; set; }//采样点数种类
        //public int SamplePointCode { get; set; }//采样点数代码
        //public int SlotNum { get; set; }//槽号   
        //public string Code { get; set; }//槽代号
        //public string T_TransmissionCard_Code { get; set; }//抽象信息槽 
        public T1_WirelessVibrationSlot TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_WirelessVibrationSlot();
            TempData.id = this.id;
            TempData.Integration = this.Integration;
            TempData.Unit = this.Unit;
            TempData.SampleFreCategory = this.SampleFreCategory;
            TempData.SampleFreCode = this.SampleFreCode;
            TempData.SamplePointCategory = this.SamplePointCategory;
            TempData.SamplePointCode = this.SamplePointCode;
            TempData.SlotNum = this.SlotNum;
            TempData.Code = this.Code;
            TempData.T_TransmissionCard_Code = this.T_TransmissionCard_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.Integration = TempData.Integration;
            this.Unit = TempData.Unit;
            this.SampleFreCategory = TempData.SampleFreCategory;
            this.SampleFreCode = TempData.SampleFreCode;
            this.SamplePointCategory = TempData.SamplePointCategory;
            this.SamplePointCode = TempData.SamplePointCode;
            this.SlotNum = TempData.SlotNum;
            this.Code = TempData.Code;
            this.T_TransmissionCard_Code = TempData.T_TransmissionCard_Code;
            TempData = null;
        }
    }
}

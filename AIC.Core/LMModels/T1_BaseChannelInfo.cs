using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public interface I_BaseChannelInfo: Iid
    {
        //public int id { get; set; }//自增ID
        //public string CalibrationlInfo { get; set; }//校准信息
        //public string OtherInfo { get; set; }//其它信息        
        //public string ThresholdInfo { get; set; }//门槛值信息
        //public string VibrationAddition { get; set; }//振动附加
        //public string RPMChannelInfo { get; set; }//转速通道信息
        //public string RPMCouplingInfo { get; set; }//转速耦合信息
        //public string TransformMethod { get; set; }//变换方法
        //public string DigitRransducerInfo { get; set; }//数字变送器信息
        //public string SourceChannelInfo { get; set; }//数据来源通道信息
        //public string Code { get; set; }//通道代号
        //public string T_AbstractChannelInfo_Code { get; set; }//抽象信息通道
        //public int ChannelCode { get; set; }//通道类型
    }

    public class T1_IEPEChannelInfo : T_IEPEChannelInfo, I_BaseChannelInfo//IEPE通道信息
    {
        //public int id { get; set; }//自增ID
        //public string CalibrationlInfo { get; set; }//校准信息
        //public string OtherInfo { get; set; }//其它信息    
        //public string VibrationAddition { get; set; }//振动附加    
        //public string Code { get; set; }//通道代号
        //public string T_AbstractChannelInfo_Code { get; set; }//抽象信息通道  

        public T1_IEPEChannelInfo TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_IEPEChannelInfo();
            TempData.id = this.id;
            TempData.CalibrationlInfo = this.CalibrationlInfo;
            TempData.OtherInfo = this.OtherInfo;
            TempData.VibrationAddition = this.VibrationAddition;
            TempData.Code = this.Code;
            TempData.T_AbstractChannelInfo_Code = this.T_AbstractChannelInfo_Code;           
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.CalibrationlInfo = TempData.CalibrationlInfo;
            this.OtherInfo = TempData.OtherInfo;
            this.VibrationAddition = TempData.VibrationAddition;
            this.Code = TempData.Code;
            this.T_AbstractChannelInfo_Code = TempData.T_AbstractChannelInfo_Code;
            TempData = null;
        }
    }
    public class T1_EddyCurrentDisplacementChannelInfo : T_EddyCurrentDisplacementChannelInfo, I_BaseChannelInfo//电涡流位移通道信息
    {
        //public int id { get; set; }//自增ID     
        //public string OtherInfo { get; set; }//其它信息 
        //public string VibrationAddition { get; set; }//振动附加       
        //public string Code { get; set; }//通道代号
        //public string T_AbstractChannelInfo_Code { get; set; }//抽象信息通道  
        public T1_EddyCurrentDisplacementChannelInfo TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_EddyCurrentDisplacementChannelInfo();
            TempData.id = this.id;           
            TempData.OtherInfo = this.OtherInfo;
            TempData.VibrationAddition = this.VibrationAddition;
            TempData.Code = this.Code;
            TempData.T_AbstractChannelInfo_Code = this.T_AbstractChannelInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;            
            this.OtherInfo = TempData.OtherInfo;
            this.VibrationAddition = TempData.VibrationAddition;
            this.Code = TempData.Code;
            this.T_AbstractChannelInfo_Code = TempData.T_AbstractChannelInfo_Code;
            TempData = null;
        }
    }
    public class T1_EddyCurrentKeyPhaseChannelInfo : T_EddyCurrentKeyPhaseChannelInfo, I_BaseChannelInfo//电涡流键相通道信息
    {
        //public int id { get; set; }//自增ID      
        //public string ThresholdInfo { get; set; }//门槛值信息
        //public string VibrationAddition { get; set; }//振动附加
        //public string RPMChannelInfo { get; set; }//转速通道信息     
        //public string Code { get; set; }//通道代号
        //public string T_AbstractChannelInfo_Code { get; set; }//抽象信息通道 
        public T1_EddyCurrentKeyPhaseChannelInfo TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_EddyCurrentKeyPhaseChannelInfo();
            TempData.id = this.id;
            TempData.ThresholdInfo = this.ThresholdInfo;
            TempData.VibrationAddition = this.VibrationAddition;
            TempData.RPMChannelInfo = this.RPMChannelInfo;
            TempData.Code = this.Code;
            TempData.T_AbstractChannelInfo_Code = this.T_AbstractChannelInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.ThresholdInfo = TempData.ThresholdInfo;
            this.VibrationAddition = TempData.VibrationAddition;
            this.RPMChannelInfo = TempData.RPMChannelInfo;
            this.Code = TempData.Code;
            this.T_AbstractChannelInfo_Code = TempData.T_AbstractChannelInfo_Code;
            TempData = null;
        }
    }
    public class T1_EddyCurrentTachometerChannelInfo : T_EddyCurrentTachometerChannelInfo, I_BaseChannelInfo//电涡流转速表通道信息
    {
        //public int id { get; set; }//自增ID             
        //public string ThresholdInfo { get; set; }//门槛值信息
        //public string VibrationAddition { get; set; }//振动附加
        //public string RPMChannelInfo { get; set; }//转速通道信息
        //public string RPMCouplingInfo { get; set; }//转速耦合信息      
        //public string Code { get; set; }//通道代号
        //public string T_AbstractChannelInfo_Code { get; set; }//抽象信息通道 
        public T1_EddyCurrentTachometerChannelInfo TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_EddyCurrentTachometerChannelInfo();
            TempData.id = this.id;
            TempData.ThresholdInfo = this.ThresholdInfo;
            TempData.VibrationAddition = this.VibrationAddition;
            TempData.RPMChannelInfo = this.RPMChannelInfo;
            TempData.RPMCouplingInfo = this.RPMCouplingInfo;
            TempData.Code = this.Code;
            TempData.T_AbstractChannelInfo_Code = this.T_AbstractChannelInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.ThresholdInfo = TempData.ThresholdInfo;
            this.VibrationAddition = TempData.VibrationAddition;
            this.RPMChannelInfo = TempData.RPMChannelInfo;
            this.RPMCouplingInfo = TempData.RPMCouplingInfo;
            this.Code = TempData.Code;
            this.T_AbstractChannelInfo_Code = TempData.T_AbstractChannelInfo_Code;
            TempData = null;
        }
    }
    public class T1_DigitTachometerChannelInfo : T_DigitTachometerChannelInfo, I_BaseChannelInfo//数字转速表通道信息
    {
        //public int id { get; set; }//自增ID     
        //public string RPMChannelInfo { get; set; }//转速通道信息      
        //public string Code { get; set; }//通道代号
        //public string T_AbstractChannelInfo_Code { get; set; }//抽象信息通道   
        public T1_DigitTachometerChannelInfo TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_DigitTachometerChannelInfo();
            TempData.id = this.id;
            TempData.RPMChannelInfo = this.RPMChannelInfo;          
            TempData.Code = this.Code;
            TempData.T_AbstractChannelInfo_Code = this.T_AbstractChannelInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.RPMChannelInfo = TempData.RPMChannelInfo;        
            this.Code = TempData.Code;
            this.T_AbstractChannelInfo_Code = TempData.T_AbstractChannelInfo_Code;
            TempData = null;
        }
    }
    public class T1_AnalogRransducerInChannelInfo : T_AnalogRransducerInChannelInfo, I_BaseChannelInfo//模拟变送器输入通道信息
    {
        //public int id { get; set; }//自增ID     
        //public string TransformMethod { get; set; }//变换方法    
        //public string Code { get; set; }//通道代号
        //public string T_AbstractChannelInfo_Code { get; set; }//抽象信息通道        
        public T1_AnalogRransducerInChannelInfo TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_AnalogRransducerInChannelInfo();
            TempData.id = this.id;
            TempData.TransformMethod = this.TransformMethod;
            TempData.Code = this.Code;
            TempData.T_AbstractChannelInfo_Code = this.T_AbstractChannelInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.TransformMethod = TempData.TransformMethod;
            this.Code = TempData.Code;
            this.T_AbstractChannelInfo_Code = TempData.T_AbstractChannelInfo_Code;
            TempData = null;
        }
    }
    public class T1_RelayChannelInfo : T_RelayChannelInfo, I_BaseChannelInfo//继电器通道信息
    {
        //public int id { get; set; }//自增ID       
        //public string Code { get; set; }//通道代号
        //public string T_AbstractChannelInfo_Code { get; set; }//抽象信息通道    
        public T1_RelayChannelInfo TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_RelayChannelInfo();
            TempData.id = this.id;          
            TempData.Code = this.Code;
            TempData.T_AbstractChannelInfo_Code = this.T_AbstractChannelInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;           
            this.Code = TempData.Code;
            this.T_AbstractChannelInfo_Code = TempData.T_AbstractChannelInfo_Code;
            TempData = null;
        }
    }
    public class T1_DigitRransducerInChannelInfo : T_DigitRransducerInChannelInfo, I_BaseChannelInfo//数字变送器输入通道信息
    {
        //public int id { get; set; }//自增ID     
        //public string TransformMethod { get; set; }//变换方法
        //public string DigitRransducerInfo { get; set; }//数字变送器信息      
        //public string Code { get; set; }//通道代号
        //public string T_AbstractChannelInfo_Code { get; set; }//抽象信息通道  
        public T1_DigitRransducerInChannelInfo TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_DigitRransducerInChannelInfo();
            TempData.id = this.id;
            TempData.TransformMethod = this.TransformMethod;
            TempData.DigitRransducerInfo = this.DigitRransducerInfo;
            TempData.Code = this.Code;
            TempData.T_AbstractChannelInfo_Code = this.T_AbstractChannelInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.TransformMethod = TempData.TransformMethod;
            this.DigitRransducerInfo = TempData.DigitRransducerInfo;
            this.Code = TempData.Code;
            this.T_AbstractChannelInfo_Code = TempData.T_AbstractChannelInfo_Code;
            TempData = null;
        }
    }
    public class T1_DigitRransducerOutChannelInfo : T_DigitRransducerOutChannelInfo, I_BaseChannelInfo//数字变送器输出通道信息
    {
        //public int id { get; set; }//自增ID    
        //public string TransformMethod { get; set; }//变换方法
        //public string DigitRransducerInfo { get; set; }//数字变送器信息
        //public string SourceChannelInfo { get; set; }//数据来源通道信息
        //public string Code { get; set; }//通道代号
        //public string T_AbstractChannelInfo_Code { get; set; }//抽象信息通道   
        public T1_DigitRransducerOutChannelInfo TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_DigitRransducerOutChannelInfo();
            TempData.id = this.id;
            TempData.TransformMethod = this.TransformMethod;
            TempData.DigitRransducerInfo = this.DigitRransducerInfo;
            TempData.SourceChannelInfo = this.SourceChannelInfo;
            TempData.Code = this.Code;
            TempData.T_AbstractChannelInfo_Code = this.T_AbstractChannelInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.TransformMethod = TempData.TransformMethod;
            this.SourceChannelInfo = TempData.SourceChannelInfo;
            this.DigitRransducerInfo = TempData.DigitRransducerInfo;
            this.Code = TempData.Code;
            this.T_AbstractChannelInfo_Code = TempData.T_AbstractChannelInfo_Code;          
            TempData = null;
        }
    }
    public class T1_AnalogRransducerOutChannelInfo : T_AnalogRransducerOutChannelInfo, I_BaseChannelInfo//模拟变送器输出通道信息
    {
        //public int id { get; set; }//自增ID     
        //public string TransformMethod { get; set; }//变换方法      
        //public string SourceChannelInfo { get; set; }//数据来源通道信息
        //public string Code { get; set; }//通道代号
        //public string T_AbstractChannelInfo_Code { get; set; }//抽象信息通道
        public T1_AnalogRransducerOutChannelInfo TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_AnalogRransducerOutChannelInfo();
            TempData.id = this.id;
            TempData.TransformMethod = this.TransformMethod;           
            TempData.SourceChannelInfo = this.SourceChannelInfo;
            TempData.Code = this.Code;
            TempData.T_AbstractChannelInfo_Code = this.T_AbstractChannelInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.TransformMethod = TempData.TransformMethod;
            this.SourceChannelInfo = TempData.SourceChannelInfo;
            this.Code = TempData.Code;
            this.T_AbstractChannelInfo_Code = TempData.T_AbstractChannelInfo_Code;           
            TempData = null;
        }
    }

    public class T1_WirelessScalarChannelInfo : T_WirelessScalarChannelInfo, I_BaseChannelInfo//标量通道信息
    {
        //public int id { get; set; }//自增ID
        //public string TransformMethod { get; set; }//变换方法
        //public string Code { get; set; }//通道代号
        //public string T_AbstractChannelInfo_Code { get; set; }//抽象信息通道
        public T1_WirelessScalarChannelInfo TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_WirelessScalarChannelInfo();
            TempData.id = this.id;
            TempData.TransformMethod = this.TransformMethod;          
            TempData.Code = this.Code;
            TempData.T_AbstractChannelInfo_Code = this.T_AbstractChannelInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.TransformMethod = TempData.TransformMethod;
            this.Code = TempData.Code;
            this.T_AbstractChannelInfo_Code = TempData.T_AbstractChannelInfo_Code;            
            TempData = null;
        }
    }

    public class T1_WirelessVibrationChannelInfo :  T_WirelessVibrationChannelInfo, I_BaseChannelInfo//振动通道信息
    {
        //public int id { get; set; }//自增ID
        //public string CalibrationlInfo { get; set; }//校准信息
        //public string OtherInfo { get; set; }//其它信息    
        //public string VibrationAddition { get; set; }//振动附加    
        //public string Code { get; set; }//通道代号
        //public string T_AbstractChannelInfo_Code { get; set; }//抽象信息通道  
        public T1_WirelessVibrationChannelInfo TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_WirelessVibrationChannelInfo();
            TempData.id = this.id;
            TempData.CalibrationlInfo = this.CalibrationlInfo;
            TempData.OtherInfo = this.OtherInfo;
            TempData.VibrationAddition = this.VibrationAddition;
            TempData.Code = this.Code;
            TempData.T_AbstractChannelInfo_Code = this.T_AbstractChannelInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.CalibrationlInfo = TempData.CalibrationlInfo;
            this.OtherInfo = TempData.OtherInfo;
            this.VibrationAddition = TempData.VibrationAddition;
            this.Code = TempData.Code;
            this.T_AbstractChannelInfo_Code = TempData.T_AbstractChannelInfo_Code;
            TempData = null;
        }
    }
}

using AIC.Core.LMModels;
using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.HardwareModels
{
    public interface ICard
    {        
    }

    public interface IWireSlot : ISlot
    {
        T1_AbstractSlotInfo T_AbstractSlotInfo { get; set; }   
        List<InSignalCategory> InSignalCategory { get; set; }//输入信号种类
        int InSignalCode { get; set; }//输入信号代码
        int SlotNum { get; set; }//槽号
        string SlotName { get; set; }//槽名
        int UploadIntevalTime { get; set; }//上传时间间隔
        bool IsInput { get; set; }//输入或输出槽
        string Unit { get; set; }//单位
        string Version { get; set; }//版本号
    }


    public interface IChannel
    {
        bool IsEdited { get; set; }
        T1_AbstractChannelInfo T_AbstractChannelInfo { get; set; }    
        List<Organization> Organization { get; set; }//组织机构       
        string T_Device_Name { get; set; }//设备编号
        string T_Device_Code { get; set; }//设备编号
        string T_Device_Guid { get; set; }//设备Guid
        string T_Item_Name { get; set; }//测点名称
        string T_Item_Code { get; set; }//测点编号
        string T_Item_Guid { get; set; }//测点Guid
        int CHNum { get; set; }//通道号
        int SubCHNum { get; set; }//子通道号
        bool IsUploadData { get; set; }//是否上传
        string Unit { get; set; }//单位
        List<SVTypeCategory> SVTypeCategory { get; set; }//单值类型种类
        int SVTypeCode { get; set; }//单值类型代码
        List<LocalSaveCategory> LocalSaveCategory { get; set; }//本地存储策略
        int LocalSaveCode { get; set; }//本地存储代码
        bool IsBypass { get; set; }//是否旁路
        int DelayAlarmTime { get; set; }//延时报警
        int NotOKDelayAlarmTime { get; set; }//NotOK延时报警
        bool IsLogic { get; set; }//启用逻辑表达式
        string LogicExpression { get; set; }//逻辑表达式
        string Remarks { get; set; }//备注
        string Extra_Information { get; set; }//附加信息
        AlarmStrategy AlarmStrategy { get; set; }//报警策略
    }

    public interface DivFreIChannel : IChannel
    {
        ObservableCollection<DivFreInfo> DivFreInfo { get; set; }
    }

    public interface ICategory
    {
        int Code { get; set; }//代号
        string Name { get; set; }//名称
    }

   
    public class WireMatchingCard : INotifyPropertyChanged, ICard//配板
    {
        [JsonIgnore]
        public T1_WireMatchingCard T_WireMatchingCard { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }

        private string cardName;
        public string CardName//别名
        {
            get { return cardName; }
            set
            {
                cardName = value;
                OnPropertyChanged("CardName");
            }
        }
        public int CardNum { get; set; }//卡号
        public IEPESlot IEPESlot { get; set; }//IEPE槽
        public EddyCurrentDisplacementSlot EddyCurrentDisplacementSlot { get; set; }//电涡流位移槽
        public EddyCurrentKeyPhaseSlot EddyCurrentKeyPhaseSlot { get; set; }//电涡流键相槽(废弃)
        public EddyCurrentTachometerSlot EddyCurrentTachometerSlot { get; set; }//电涡流转速表槽
        public DigitTachometerSlot DigitTachometerSlot { get; set; }//数字转速表槽
        public AnalogRransducerInSlot AnalogRransducerInSlot { get; set; }//模拟变送器输入槽
        public RelaySlot RelaySlot { get; set; }//继电器槽
        public DigitRransducerInSlot DigitRransducerInSlot { get; set; }//数字变送器输入槽
        public DigitRransducerOutSlot DigitRransducerOutSlot { get; set; }//数字变送器输出槽
        public AnalogRransducerOutSlot AnalogRransducerOutSlot { get; set; }//模拟变送器输出槽
        //public WaveGeneralSlot WaveGeneralSlot { get; set; }//波形通用槽

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

   
    public class IEPESlot : VibrationSlotInfo//IEPE槽:振动槽信息
    {
        [JsonIgnore]
        public T1_IEPESlot T_IEPESlot { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }

        private int inSignalCod;
        public override int InSignalCode//输入信号代码
        {
            get { return inSignalCod; }
            set
            {
                inSignalCod = value;
                OnPropertyChanged("InSignalCode");
                SetIntegrationCategory(value);
            }
        }

        private int integration;
        public int Integration //积分
        {
            get { return integration; }
            set
            {
                integration = value;
                OnPropertyChanged("Integration");
            }
        }

        private List<BaseCategory> integrationCategory = new List<BaseCategory>();
        [JsonIgnore]
        public List<BaseCategory> IntegrationCategory //积分设置
        {
            get { return integrationCategory; }
            set
            {
                integrationCategory = value;
                OnPropertyChanged("IntegrationCategory");
            }
        }

        public void SetIntegrationCategory(int code)
        {
            IntegrationCategory.Clear();
            switch (code)
            {
                case 0:
                case 1:
                    IntegrationCategory.Add(new BaseCategory() { Code = 0, Name = "加速度" });
                    IntegrationCategory.Add(new BaseCategory() { Code = 1, Name = "速度" });
                    IntegrationCategory.Add(new BaseCategory() { Code = 2, Name = "位移" });
                    break;
                case 2:
                    IntegrationCategory.Add(new BaseCategory() { Code = 0, Name = "加速度" });
                    IntegrationCategory.Add(new BaseCategory() { Code = 1, Name = "速度" });
                    break;
                case 3:
                    IntegrationCategory.Add(new BaseCategory() { Code = 0, Name = "加速度" });
                    break;
            }
        }

        public List<IEPEChannelInfo> IEPEChannelInfo { get; set; }//IEPE通道       
    }

   
    public class EddyCurrentDisplacementSlot : VibrationSlotInfo//电涡流位移槽：振动槽信息
    {
        [JsonIgnore]
        public T1_EddyCurrentDisplacementSlot T_EddyCurrentDisplacementSlot { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }

        public bool Is24V { get; set; }//24V供电
        public List<EddyCurrentDisplacementChannelInfo> EddyCurrentDisplacementChannelInfo { get; set; }//电涡流位移通道

    }

   
    public class EddyCurrentKeyPhaseSlot : EddyCurrentRPMSlotInfo//电涡流键相槽:电涡流转速槽信息
    {
        [JsonIgnore]
        public T1_EddyCurrentKeyPhaseSlot T_EddyCurrentKeyPhaseSlot { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }

        public bool Is24V { get; set; }//24V供电
        public List<EddyCurrentKeyPhaseChannelInfo> EddyCurrentKeyPhaseChannelInfo { get; set; }//电涡流键相通道
    }

   
    public class EddyCurrentTachometerSlot : EddyCurrentRPMSlotInfo//电涡流转速表槽:电涡流转速槽信息
    {
        [JsonIgnore]
        public T1_EddyCurrentTachometerSlot T_EddyCurrentTachometerSlot { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }

        public bool Is24V { get; set; }//24V供电

        private bool isEnableMainCH;
        public bool IsEnableMainCH //主通道使能
        {
            get { return isEnableMainCH; }
            set
            {
                isEnableMainCH = value;
                OnPropertyChanged("IsEnableMainCH");
            }
        }
        public List<EddyCurrentTachometerChannelInfo> EddyCurrentTachometerChannelInfo { get; set; }//电涡流转速表通道信息

    }

   
    public class DigitTachometerSlot : AbstractSlotInfo//数字转速表槽:抽象槽信息
    {
        [JsonIgnore]
        public T1_DigitTachometerSlot T_DigitTachometerSlot { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }

        public List<DigitTachometerChannelInfo> DigitTachometerChannelInfo { get; set; }// 数字转速表通道信息
    }

   
    public class AnalogRransducerInSlot : AbstractSlotInfo//模拟变送器输入槽:抽象槽信息
    {
        [JsonIgnore]
        public T1_AnalogRransducerInSlot T_AnalogRransducerInSlot { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }

        public List<AnalogRransducerInChannelInfo> AnalogRransducerInChannelInfo { get; set; }//模拟变送器通道输入信息
    }

   
    public class RelaySlot : AbstractSlotInfo//继电器槽:抽象槽信息
    {
        [JsonIgnore]
        public T1_RelaySlot T_RelaySlot { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }

        public List<RelayChannelInfo> RelayChannelInfo { get; set; }//继电器通道信息
    }

   
    public class DigitRransducerInSlot : AbstractSlotInfo//数字变送器输入槽:抽象槽信息
    {
        [JsonIgnore]
        public T1_DigitRransducerInSlot T_DigitRransducerInSlot { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }

        public List<DigitRransducerInChannelInfo> DigitRransducerInChannelInfo { get; set; }//数字变送器输入通道信息
    }

   
    public class DigitRransducerOutSlot : AbstractSlotInfo//数字变送器输出槽	:抽象槽信息
    {
        [JsonIgnore]
        public T1_DigitRransducerOutSlot T_DigitRransducerOutSlot { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }

        public List<DigitRransducerOutChannelInfo> DigitRransducerOutChannelInfo { get; set; }//数字变送器输出通道信息
    }

   
    public class AnalogRransducerOutSlot : AbstractSlotInfo//模拟变送器输出槽:抽象槽信息
    {
        [JsonIgnore]
        public T1_AnalogRransducerOutSlot T_AnalogRransducerOutSlot { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }

        public List<AnalogRransducerOutChannelInfo> AnalogRransducerOutChannelInfo { get; set; }//模拟变送器输出通道信息
    }

   
    public class VibrationSlotInfo : AbstractSlotInfo//振动信息槽:抽象槽
    {
        public List<HighPassCategory> HighPassCategory { get; set; }//高通滤波

        private int highPassCode;
        public int HighPassCode//高通代码
        {
            get { return highPassCode; }
            set
            {
                highPassCode = value;
                OnPropertyChanged("HighPassCode");
            }
        }

        public List<WaveCategory> WaveCategory { get; set; }//波形种类

        private int waveCode;
        public int WaveCode//波形代码
        {
            get { return waveCode; }
            set
            {
                waveCode = value;
                OnPropertyChanged("WaveCode");
            }
        }

        private SampleMode sampleMode;
        public SampleMode SampleMode//采样方式
        {
            get { return sampleMode; }
            set
            {
                sampleMode = value;
                OnPropertyChanged("SampleMode");
            }
        }
    }

   
    public class EddyCurrentRPMSlotInfo : AbstractSlotInfo//电涡流转速信息槽:抽象槽
    {
        public List<EddyCurrentRPMSample> EddyCurrentRPMSample { get; set; }//采样方式

        private int eddyCurrentRPMCode;
        public int EddyCurrentRPMCode //代码
        {
            get { return eddyCurrentRPMCode; }
            set
            {
                eddyCurrentRPMCode = value;
                OnPropertyChanged("EddyCurrentRPMCode");
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //private void OnPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
    }

   
    public partial class AbstractSlotInfo : INotifyPropertyChanged, IWireSlot
    {
        [JsonIgnore]
        public T1_AbstractSlotInfo T_AbstractSlotInfo { get; set; }     

        public List<InSignalCategory> InSignalCategory { get; set; }//输入信号种类

        private int inSignalCod;
        public virtual int InSignalCode//输入信号代码
        {
            get { return inSignalCod; }
            set
            {
                inSignalCod = value;
                OnPropertyChanged("InSignalCode");
            }
        }
        public int SlotNum { get; set; }//槽号

        private string slotName;
        public string SlotName //槽名
        {
            get { return slotName; }
            set
            {
                slotName = value;
                OnPropertyChanged("SlotName");
            }
        }


        private int uploadIntevalTime;
        public int UploadIntevalTime //上传时间间隔
        {
            get { return uploadIntevalTime; }
            set
            {
                uploadIntevalTime = value;
                OnPropertyChanged("UploadIntevalTime");
            }
        }

        public bool IsInput { get; set; }//输入或输出槽

        private string unit;
        public string Unit//单位
        {
            get { return unit; }
            set
            {
                unit = value;
                OnPropertyChanged("Unit");
            }
        }
        public string Version { get; set; }//版本号

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

   
    public partial class IEPEChannelInfo : VibrationChannelInfo, DivFreIChannel//IEPE通道:振动通道
    {
        [JsonIgnore]
        public T1_IEPEChannelInfo T_IEPEChannelInfo { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }

        private float velocityCalibration;
        public float VelocityCalibration//速度校准
        {
            get { return velocityCalibration; }
            set
            {
                velocityCalibration = value;
                OnPropertyChanged("VelocityCalibration");
            }
        }

        private float displacementCalibration;
        public float DisplacementCalibration //位移校准
        {
            get { return displacementCalibration; }
            set
            {
                displacementCalibration = value;
                OnPropertyChanged("DisplacementCalibration");
            }
        }
    }
   
    public class EddyCurrentDisplacementChannelInfo : VibrationChannelInfo, DivFreIChannel//电涡流位移通道信息:振动通道
    {
        [JsonIgnore]
        public T1_EddyCurrentDisplacementChannelInfo T_EddyCurrentDisplacementChannelInfo { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }
    }

   
    public class EddyCurrentKeyPhaseChannelInfo : EddyCurrentRPMChannelInfo, IChannel//电涡流键相通道信息:电涡流转速通道信息
    {
        [JsonIgnore]
        public T1_EddyCurrentKeyPhaseChannelInfo T_EddyCurrentKeyPhaseChannelInfo { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }
    }

   
    public class EddyCurrentTachometerChannelInfo : EddyCurrentRPMChannelInfo, IChannel//电涡流转速表通道信息:电涡流转速通道信息\
    {
        [JsonIgnore]
        public T1_EddyCurrentTachometerChannelInfo T_EddyCurrentTachometerChannelInfo { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }
        public List<RPMCouplingCategory> RPMCouplingCategory { get; set; }//转速耦合种类

        private int rPMCouplingCode;
        public int RPMCouplingCode //耦合代码
        {
            get { return rPMCouplingCode; }
            set
            {
                rPMCouplingCode = value;
                OnPropertyChanged("RPMCouplingCode");
            }
        }
    }

   
    public class DigitTachometerChannelInfo : RPMChannelInfo, IChannel//数字转速表通道信息:抽象通道信息,AbstractChannelInfo避免多个基类移到RPMChannelInfo,转速通道信息
    {
        [JsonIgnore]
        public T1_DigitTachometerChannelInfo T_DigitTachometerChannelInfo { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }
    }

   
    public class AnalogRransducerInChannelInfo : TransformMethod, IChannel//模拟变送器输入通道信息:抽象通道信息,AbstractChannelInfo避免多个基类移到TransformMethod,变换方法
    {
        [JsonIgnore]
        public T1_AnalogRransducerInChannelInfo T_AnalogRransducerInChannelInfo { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }
    }

   
    public class RelayChannelInfo : AbstractChannelInfo, IChannel//继电器通道信息:抽象通道信息
    {
        [JsonIgnore]
        public T1_RelayChannelInfo T_RelayChannelInfo { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }
    }

   
    public class DigitRransducerInChannelInfo : DigitRransducerAddition, IChannel//数字变送器输入通道信息:抽象通道信息,AbstractChannelInfo避免多个基类移到DigitRransducerAddition,变送器附加
    {
        [JsonIgnore]
        public T1_DigitRransducerInChannelInfo T_DigitRransducerInChannelInfo { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }
    }

   
    public class DigitRransducerOutChannelInfo : SourceChannelInfo, IChannel//数字变送器输出通道信息:抽象通道信息,AbstractChannelInfo,DigitRransducerAddition避免多个基类移到SourceChannelInfo,变送器附加,数据来源通道信息
    {
        [JsonIgnore]
        public T1_DigitRransducerOutChannelInfo T_DigitRransducerOutChannelInfo { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }
    }

   
    public class AnalogRransducerOutChannelInfo : SourceChannelInfo, IChannel//模拟变送器输出通道信息:抽象通道信息,AbstractChannelInfo,TransformMethod避免多个基类移到TransformMethod,变换方法,数据来源通道信息
    {
        [JsonIgnore]
        public T1_AnalogRransducerOutChannelInfo T_AnalogRransducerOutChannelInfo { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }
    }

   
    public partial class VibrationChannelInfo : VibrationAddition//振动通道:抽象通道,AbstractChannelInfo避免多个基类移到RPMChannelInfo
    {
        private int rPMCardNum;
        public int RPMCardNum//转速卡号
        {
            get { return rPMCardNum; }
            set
            {
                rPMCardNum = value;
                OnPropertyChanged("RPMCardNum");
            }
        }

        private int rPMSlotNum;
        public int RPMSlotNum //转速槽号
        {
            get { return rPMSlotNum; }
            set
            {
                rPMSlotNum = value;
                OnPropertyChanged("RPMSlotNum");
            }
        }

        private int rPMCHNum;
        public int RPMCHNum//转速通道号
        {
            get { return rPMCHNum; }
            set
            {
                rPMCHNum = value;
                OnPropertyChanged("RPMCHNum");
            }
        }

        private bool isMultiplication;
        public bool IsMultiplication //是否倍增
        {
            get { return isMultiplication; }
            set
            {
                isMultiplication = value;
                OnPropertyChanged("IsMultiplication");
            }
        }

      
        private float multiplicationCor;
        public float MultiplicationCor //倍增系数
        {
            get { return multiplicationCor; }
            set
            {
                multiplicationCor = value;
                OnPropertyChanged("MultiplicationCor");
            }
        }

        private bool isSaveWaveToSD;
        public bool IsSaveWaveToSD //是否在SD存储波形
        {
            get { return isSaveWaveToSD; }
            set
            {
                isSaveWaveToSD = value;
                OnPropertyChanged("IsSaveWaveToSD");
            }
        }


        private bool isUploadWave;
        public bool IsUploadWave//是否上传波形
        {
            get { return isUploadWave; }
            set
            {
                isUploadWave = value;
                OnPropertyChanged("IsUploadWave");
            }
        }

        private float defaultRPM;
        public float DefaultRPM //默认转速
        {
            get { return defaultRPM; }
            set
            {
                defaultRPM = value;
                OnPropertyChanged("DefaultRPM");
            }
        }

        private ObservableCollection<DivFreInfo> divFreInfo;
        public ObservableCollection<DivFreInfo> DivFreInfo //分频信息
        {
            get { return divFreInfo; }
            set
            {
                divFreInfo = value;
                OnPropertyChanged("DivFreInfo");
            }
        }

        public VibrationChannelInfo()
        {
            IsShowSVType = false;
            IsShowUnit = false;
        }
    }

   
    public partial class EddyCurrentRPMChannelInfo : VibrationAddition//电涡流转速通道信息:抽象通道信息，AbstractChannelInfo,RPMChannelInfo避免多个基类移到VibrationAddition,转速通道信息,振动附加
    {
        private float thresholdVolt;
        public float ThresholdVolt//门槛值电压
        {
            get { return thresholdVolt; }
            set
            {
                thresholdVolt = value;
                OnPropertyChanged("ThresholdVolt");
            }
        }

        private float hysteresisVolt;
        public float HysteresisVolt //迟滞电压
        {
            get { return hysteresisVolt; }
            set
            {
                hysteresisVolt = value;
                OnPropertyChanged("HysteresisVolt");
            }
        }

        public List<ThresholdModeCategory> ThresholdModeCategory { get; set; }//门槛值种类

        private int thresholdModeCode;
        public int ThresholdModeCode//门槛值代码    
        {
            get { return thresholdModeCode; }
            set
            {
                thresholdModeCode = value;
                OnPropertyChanged("ThresholdModeCode");
            }
        }
    }

   
    public partial class RPMChannelInfo : AbstractChannelInfo//转速通道信息, 避免多个基类,移入AbstractChannelInfo
    {
        private float calibrationCor;
        public float CalibrationCor //转速校准系数
        {
            get { return calibrationCor; }
            set
            {
                calibrationCor = value;
                OnPropertyChanged("CalibrationCor");
            }
        }

        private bool isNotch;
        public bool IsNotch //凹槽还是凸槽
        {
            get { return isNotch; }
            set
            {
                isNotch = value;
                OnPropertyChanged("IsNotch");
            }
        }

        private int averageNumber;
        public int AverageNumber//平均次数
        {
            get { return averageNumber; }
            set
            {
                averageNumber = value;
                OnPropertyChanged("AverageNumber");
            }
        }

        private float teethNumber;
        public float TeethNumber //齿数
        {
            get { return teethNumber; }
            set
            {
                teethNumber = value;
                OnPropertyChanged("TeethNumber");
            }
        }
    }

   
    public partial class VibrationAddition : RPMChannelInfo//振动附加, 避免多个基类,移入AbstractChannelInfo,移入RPMChannelInfo
    {
        public List<TPDirCategory> TPDirCategory { get; set; }//测点方向

        private int tPDirCode;
        public int TPDirCode //测点方向
        {
            get { return tPDirCode; }
            set
            {
                tPDirCode = value;
                OnPropertyChanged("TPDirCode");
            }
        }

        private float biasVoltHigh;
        public float BiasVoltHigh //偏置高电压
        {
            get { return biasVoltHigh; }
            set
            {
                biasVoltHigh = value;
                OnPropertyChanged("BiasVoltHigh");
            }
        }

        private float biasVoltLow;
        public float BiasVoltLow //偏置低电压
        {
            get { return biasVoltLow; }
            set
            {
                biasVoltLow = value;
                OnPropertyChanged("BiasVoltLow");
            }
        }

        private float sensitivity;
        public float Sensitivity //灵敏度
        {
            get { return sensitivity; }
            set
            {
                sensitivity = value;
                OnPropertyChanged("Sensitivity");
            }
        }

        [JsonIgnore]
        public bool IsReadOnlySensitivity { get; set; }       
    }
   
    public partial class AbstractChannelInfo : INotifyPropertyChanged //抽象通道
    {
        [JsonIgnore]
        public T1_AbstractChannelInfo T_AbstractChannelInfo { get; set; }

        private List<Organization> organization;
        public List<Organization> Organization//组织机构
        {
            get { return organization; }
            set
            {
                organization = value;
                OnPropertyChanged("Organization");
            }
        }
        public string T_Device_Name { get; set; }//设备编号
        public string T_Device_Code { get; set; }//设备编号
        public string T_Device_Guid { get; set; }//设备Guid

        private string tPName;
        public string T_Item_Name//测点名称
        {
            get { return tPName; }
            set
            {
                tPName = value;
                OnPropertyChanged("T_Item_Name");
            }
        }

        private string tPCode;
        public string T_Item_Code //测点编号
        {
            get { return tPCode; }
            set
            {
                tPCode = value;
                OnPropertyChanged("T_Item_Code");
            }
        }

        private string tPGuid;
        public string T_Item_Guid//测点Guid
        {
            get { return tPGuid; }
            set
            {
                tPGuid = value;
                OnPropertyChanged("T_Item_Guid");
            }
        }

        public int CHNum { get; set; }//通道号
        public int SubCHNum { get; set; }//子通道号

        private bool isUploadData;
        public bool IsUploadData //是否上传
        {
            get { return isUploadData; }
            set
            {
                isUploadData = value;
                OnPropertyChanged("IsUploadData");
            }
        }

        private string unit;
        public string Unit//单位
        {
            get { return unit; }
            set
            {
                unit = value;
                OnPropertyChanged("Unit");
            }
        }

        [JsonIgnore]
        public List<string> UnitCategory
        {
            //get
            //{
            //    return new List<string>()
            //    {
            //        "m/s^2", "mm/s", "um", "Pa", "RPM", "°C", "Unit"
            //    };
            //}//从数据库获取
            get;  set;
        }

        private bool isShowUnit = true;
        [JsonIgnore]
        public bool IsShowUnit
        {
            get { return isShowUnit; }
            set
            {
                isShowUnit = value;
                OnPropertyChanged("IsShowUnit");
            }
        }

        private bool isShowSVType = true;
        [JsonIgnore]
        public bool IsShowSVType
        {
            get { return isShowSVType; }
            set
            {
                isShowSVType = value;
                OnPropertyChanged("IsShowSVType");
            }
        }

        public List<SVTypeCategory> SVTypeCategory { get; set; }//单值类型种类

        private int sVTypeCode;
        public int SVTypeCode //单值类型代码
        {
            get { return sVTypeCode; }
            set
            {
                sVTypeCode = value;
                OnPropertyChanged("SVTypeCode");
            }
        }

        public List<LocalSaveCategory> LocalSaveCategory { get; set; }//本地存储策略

        private int localSaveCode;
        public int LocalSaveCode//本地存储代码
        {
            get { return localSaveCode; }
            set
            {
                localSaveCode = value;
                OnPropertyChanged("LocalSaveCode");
            }
        }

        private bool isBypass;
        public bool IsBypass//是否旁路
        {
            get { return isBypass; }
            set
            {
                isBypass = value;
                OnPropertyChanged("IsBypass");
            }
        }

        private int delayAlarmTime;
        public int DelayAlarmTime //延时报警
        {
            get { return delayAlarmTime; }
            set
            {
                delayAlarmTime = value;
                OnPropertyChanged("DelayAlarmTime");
            }
        }

        private int notOKDelayAlarmTime;
        public int NotOKDelayAlarmTime //NotOK延时报警
        {
            get { return notOKDelayAlarmTime; }
            set
            {
                notOKDelayAlarmTime = value;
                OnPropertyChanged("NotOKDelayAlarmTime");
            }
        }

        public bool IsLogic { get; set; }//启用逻辑表达式
        public string LogicExpression { get; set; }//逻辑表达式
        private string remarks;
        public string Remarks//描述
        {
            get { return remarks; }
            set
            {
                remarks = value;
                OnPropertyChanged("Remarks");
            }
        }
        public string Extra_Information { get; set; }//附加信息
        public AlarmStrategy AlarmStrategy { get; set; }//报警策略

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

   
    public class HighPassCategory : ICategory//高通滤波种类
    {
        public int Code { get; set; }//代号
        public string Name { get; set; }//名称        
    }

   
    public class Organization : INotifyPropertyChanged//组织机构
    {
        private string name;
        public string Name //名称
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Code { get; set; }//代号

        private string guid;
        public string Guid //Guid
        {
            get { return guid; }
            set
            {
                guid = value;
                OnPropertyChanged("Guid");
            }
        }
        public int Level { get; set; }//级别
        public string Create_Time { get; set; }//创建时间
        public string Modify_Time { get; set; }//修改时间
        public string Parent_Code { get; set; }//父代号
        public string Parent_Guid { get; set; }//父Guid
        public int Parent_Level { get; set; }//父级别

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

   
    public partial class SVTypeCategory : ICategory//单值类型种类
    {
        public int Code { get; set; }//代号
        public string Name { get; set; }//名称
        public float ZeroValue { get; set; }//零点
    }

   
    public partial class TPDirCategory : ICategory//测点方向
    {
        public int Code { get; set; }//代号
        public string Name { get; set; }//名称
        public float Degree { get; set; }//安装角度
    }

   
    public class InSignalCategory : ICategory//信号输入种类
    {
        public int Code { get; set; }//编号
        public string Name { get; set; }//输入信号类型
    }

   
    public class LocalSaveCategory : ICategory//本地存储策略
    {
        public int Code { get; set; }//编号
        public string Name { get; set; }//输入信号类型
    }


   
    public class AlarmStrategy//报警策略
    {
        public AbsoluteAlarm Absolute { get; set; }//绝对报警
        public ComparativeAlarm Comparative { get; set; }//相对报警
    }

   
    public class AbsoluteAlarm: INotifyPropertyChanged
    {
        [JsonIgnore]
        public bool IsUploadWaveShow { get; set; }//只有有波形的才显示
        public List<AlarmCategory> Category { get; set; }//种类
        public List<AlarmPara> Para { get; set; }//参数
        public List<AlarmMode> Mode { get; set; }//绝对值报警模式
        public int ModeCode { get; set; }//模式代码  

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public partial class AlarmCategory : INotifyPropertyChanged, ICategory
    {
        public int Code { get; set; }//代号
        public string Name { get; set; }//名称

        private bool _isAllow;
        public bool IsAllow //是否启用
        {
            get { return _isAllow; }
            set
            {
                _isAllow = value;
                OnPropertyChanged("IsAllow");
            }
        }

        private bool _isACQWave;
        public bool IsACQWave //是否上传波形
        {
            get { return _isACQWave; }
            set
            {
                _isACQWave = value;
                OnPropertyChanged("IsACQWave");
            }
        }

        private float _value;
        public float Value //单点绝对值报警槛值
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }
        public string Formula { get; set; }//绝对值报警方程

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class AlarmPara : INotifyPropertyChanged
    {
        private int cardNum;
        public int CardNum //卡号
        {
            get { return cardNum; }
            set
            {
                cardNum = value;
                OnPropertyChanged("CardNum");
            }
        }

        private int slotNum;
        public int SlotNum //槽号
        {
            get { return slotNum; }
            set
            {
                slotNum = value;
                OnPropertyChanged("SlotNum");
            }
        }

        private int cHNum;
        public int CHNum//通道号
        {
            get { return cHNum; }
            set
            {
                cHNum = value;
                OnPropertyChanged("CHNum");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class AlarmMode : ICategory
    {
        private int code;
        public int Code//代号
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }

        private string name;
        public string Name //名称
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public partial class ComparativeAlarm
    {
        public string Range { get; set; }//工况范围
        public int IntevalTime { get; set; }//相对时间间隔(单位ms)
        public float Percent { get; set; }//相对百分比
        public bool IsAllow { get; set; }//是否允许报警
        public List<AlarmPara> Para { get; set; }//参数 
    }

   
    public class EddyCurrentRPMSample : ICategory//电涡流转速采样
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
        public float SampleFre { get; set; }//采样频率
    }

   
    public class ThresholdModeCategory : ICategory//门槛值种类
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
    }

   
    public class RPMCouplingCategory : ICategory//转速耦合种类
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
    }

   
    public class TransformMethod : AbstractChannelInfo//变换方法,避免多个基类,移入AbstractChannelInfo
    {
        public List<EquationCategory> EquationCategory { get; set; }//方程式种类

        private int equationCode;
        public int EquationCode //方程代码
        {
            get { return equationCode; }
            set
            {
                equationCode = value;
                OnPropertyChanged("EquationCode");
            }
        }

    }
   
    public partial class EquationCategory : ICategory//方程种类
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
        public string Formula { get; set; }//方程式
        public float CalibrationCor { get; set; }//校准系数
    }

   
    public class DigitRransducerAddition : TransformMethod//数字变换器附加:变换方法,避免多个基类,移入DigitRransducerAddition
    {
        public List<SwitchCategory> SwitchCategory { get; set; }//开关量总类

        private int switchCode;
        public int SwitchCode //开关代码
        {
            get { return switchCode; }
            set
            {
                switchCode = value;
                OnPropertyChanged("SwitchCode");
            }
        }

        public List<ModBusFunCategory> ModBusFunCategory { get; set; }//ModBus功能码种类

        private int modBusFunCode;
        public int ModBusFunCode //功能代码
        {
            get { return modBusFunCode; }
            set
            {
                modBusFunCode = value;
                OnPropertyChanged("ModBusFunCode");
            }
        }
    }

   
    public class ModBusFunCategory : ICategory//功能码种类
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
    }

   
    public class SwitchCategory : ICategory//开关量总类
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
        public bool IsFisrtItemofTRUE { get; set; }//真值为TRUE选择第一项
    }

   
    public partial class SourceChannelInfo : DigitRransducerAddition//数据来源通道信息避免多个基类,移入SourceChannelInfo
    {
        public int SourceCardNum { get; set; }//卡号
        public int SourceSlotNum { get; set; }//槽号
        public int SourceCHNum { get; set; }//通道号
        public int SourceSubCHNum { get; set; }//子通道号
    }

   
    public class DivFreInfo : INotifyPropertyChanged//分频信息
    {
        [JsonIgnore]
        public T1_DivFreInfo T_DivFreInfo { get; set; }
        private bool isEdited;
        [JsonIgnore]
        public bool IsEdited
        {
            get { return isEdited; }
            set
            {
                isEdited = value;
                OnPropertyChanged("IsEdited");
            }
        }

        public string Guid { get; set; }//Guid
        public string Code { get; set; }//代码

        private string name;
        public string Name //名称
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Create_Time { get; set; }//创建时间
        public string Modify_Time { get; set; }//修改时间

        private string remarks;
        public string Remarks//描述
        {
            get { return remarks; }
            set
            {
                remarks = value;
                OnPropertyChanged("Remarks");
            }
        }

        public string T_Item_Guid { get; set; }//Guid
        public string T_Item_Name { get; set; }//名称
        public string T_Item_Code { get; set; }//代号

        private int divFreCode;
        public int DivFreCode//分频代码
        {
            get { return divFreCode; }
            set
            {
                divFreCode = value;
                OnPropertyChanged("DivFreCode");
            }
        }
        public BasedOnRPM BasedOnRPM { get; set; }//基于转速
        public FixedFre FixedFre { get; set; }//固定频率
        public BasedOnRange BasedOnRange { get; set; }//基于范围
        public AlarmStrategy AlarmStrategy { get; set; }//报警策略

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

   
    public partial class BasedOnRPM : ICategory //基于转速
    {
        public int Code { get; set; }//代号
        public string Name { get; set; }//名称
        public float MultiFre { get; set; }//倍频
    }

   
    public partial class FixedFre : ICategory//固定频率
    {
        public int Code { get; set; }//代号
        public string Name { get; set; }//名称
        public float CharacteristicFre { get; set; }//特征频率
        public float Percent { get; set; }//百分比
    }

   
    public partial class BasedOnRange : ICategory//基于范围
    {
        public int Code { get; set; }//代号
        public string Name { get; set; }//名称
        public float FreLow { get; set; }//频率下限
        public float FreHigh { get; set; }//频率上限
        public float MaxFreNum { get; set; }//最大频率数
    }
}

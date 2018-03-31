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

    [Serializable]
    public partial class MainControlCard : INotifyPropertyChanged, ICard, ICloneable//主控板
    {
        [JsonIgnore]
        public T1_MainControlCard T_MainControlCard { get; set; }       
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

        public List<CommunicationCategory> CommunicationCategory { get; set; }//传输种类

        private int communicationCode;
        public int CommunicationCode//传输种类代码
        {
            get { return communicationCode; }
            set
            {
                communicationCode = value;
                OnPropertyChanged("CommunicationCode");
            }
        }

        public string Identifier { get; set; }//标识符

        private string aliasName;
        public string AliasName//别名
        {
            get { return aliasName; }
            set
            {
                aliasName = value;
                OnPropertyChanged("AliasName");
            }
        }

        private int aCQ_Unit_Type;
        public int ACQ_Unit_Type//数采器类型
        {
            get { return aCQ_Unit_Type; }
            set
            {
                aCQ_Unit_Type = value;
                OnPropertyChanged("ACQ_Unit_Type");
            }
        }
        public List<DataSourceCategory> DataSourceCategory { get; set; }//数据来源

        private int dataSourceCode;
        public int DataSourceCode//数据来源代码
        {
            get { return dataSourceCode; }
            set
            {
                dataSourceCode = value;
                OnPropertyChanged("DataSourceCode");
            }
        }

        private bool isAlarmLatch;
        public bool IsAlarmLatch //报警自锁
        {
            get { return isAlarmLatch; }
            set
            {
                isAlarmLatch = value;
                OnPropertyChanged("IsAlarmLatch");
            }
        }

        private bool isConfiguration;
        public bool IsConfiguration //软件允许配置
        {
            get { return isConfiguration; }
            set
            {
                isConfiguration = value;
                OnPropertyChanged("IsConfiguration");
            }
        }
        public bool IsHdBypass { get; set; }//硬件旁路
        public bool IsHdConfiguration { get; set; }//硬件允许配置
        public bool IsHdMultiplication { get; set; }//硬件倍增

        private bool isListen;
        public bool IsListen//监听
        {
            get { return isListen; }
            set
            {
                isListen = value;
                OnPropertyChanged("IsListen");
            }
        }

        private int asySyn;
        public int AsySyn //异步同步
        {
            get { return asySyn; }
            set
            {
                asySyn = value;
                OnPropertyChanged("AsySyn");
            }
        }

        public List<LanguageCategory> LanguageCategory { get; set; }//语言种类

        private int languageCode;
        public int LanguageCode //语言种类代码
        {
            get { return languageCode; }
            set
            {
                languageCode = value;
                OnPropertyChanged("LanguageCode");
            }
        }
        public List<MainCardCategory> MainCardCategory { get; set; }//主板种类

        private int mainCardCode;
        public int MainCardCode //主板代码
        {
            get { return mainCardCode; }
            set
            {
                mainCardCode = value;
                OnPropertyChanged("MainCardCode");
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

        private string serverIP;
        public string ServerIP //服务器IP
        {
            get { return serverIP; }
            set
            {
                serverIP = value;
                OnPropertyChanged("ServerIP");
            }
        }
        public List<WaveCategory> WaveCategory { get; set; }//波形种类

        private int synWaveCode;
        public int SynWaveCode//波形代码
        {
            get { return synWaveCode; }
            set
            {
                synWaveCode = value;
                OnPropertyChanged("SynWaveCode");
            }
        }
        public string Version { get; set; }//版本

        private float scaleDataRange;
        public float ScaleDataRange //比例数据量程
        {
            get { return scaleDataRange; }
            set
            {
                scaleDataRange = value;
                OnPropertyChanged("ScaleDataRange");
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public MainControlCard DeepClone()
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as MainControlCard;
            }
        }

        public MainControlCard ShallowClone()
        {
            return Clone() as MainControlCard;
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

    public class CommunicationCategory : ICategory//传输种类
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
    }


    public class DataSourceCategory : ICategory//数据来源
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
    }


    public class LanguageCategory : ICategory//语言种类
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
    }


    public class MainCardCategory : ICategory//主板种类
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
    }


    public class SampleMode : INotifyPropertyChanged //采样方式
    {
        private int code;
        public int Code //代码 
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }
        public FreeSample FreeSample { get; set; }//自由采样
        public RPMTriggerSample RPMTriggerSample { get; set; }//转速触发采样
        public EqualCycleSample EqualCycleSample { get; set; }//等周期采样
        public EqualAngleSample EqualAngleSample { get; set; }//等角度采样

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


    public class FreeSample : ICategory//自由采样
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
        public float SampleFre { get; set; }//采样频率
        public int SamplePoint { get; set; }//采样点数

    }


    public class RPMTriggerSample : ICategory//转速触发采样
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
        public int CardNum { get; set; }//卡号
        public int SlotNum { get; set; }//槽号
        public int CHNum { get; set; }//通道号
        public float SampleFre { get; set; }//采样频率
        public int SamplePoint { get; set; }//采样点数
    }


    public class EqualCycleSample : ICategory//等周期采样
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
        public int CardNum { get; set; }//卡号
        public int SlotNum { get; set; }//槽号
        public int CHNum { get; set; }//通道号
        public int ReferenceCycleCount { get; set; }//参考周期数
    }


    public class EqualAngleSample : ICategory//等角度采样
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
        public int CardNum { get; set; }//卡号
        public int SlotNum { get; set; }//槽号
        public int CHNum { get; set; }//通道号

    }


    public class WaveCategory : ICategory//波形种类
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
    }

    public class BaseCategory : ICategory//基本模式种类，htzk123,添加，只为界面展示使用    
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
    }

    public enum CardType
    {
        MainControlCard = 0,
        WireMatchingCard,
        WirelessReceiveCard,
        TransmissionCard
    }

    public enum SlotType
    {
        None = -1,
        IEPESlot = 0,
        EddyCurrentDisplacementSlot = 1,
        EddyCurrentKeyPhaseSlot = 2,
        EddyCurrentTachometerSlot = 3,
        DigitTachometerSlot = 4,
        AnalogRransducerInSlot = 5,
        AnalogRransducerOutSlot = 6,       
        DigitRransducerInSlot = 7,
        DigitRransducerOutSlot = 8,
        RelaySlot = 9,
        WaveGeneralSlot = 10,
        WirelessScalarSlot = 11,
        WirelessVibrationSlot = 12,
    }

    public enum ChannelType
    {
        [Description("None")]
        None = -1,
        [Description("IEPE")]
        IEPEChannelInfo = 0,
        [Description("Displacement")]
        EddyCurrentDisplacementChannelInfo = 1,
        [Description("KeyPhase")]
        EddyCurrentKeyPhaseChannelInfo = 2,
        [Description("Tachometer")]
        EddyCurrentTachometerChannelInfo = 3,
        [Description("D-Tachometer")]
        DigitTachometerChannelInfo = 4,
        [Description("AI")]
        AnalogRransducerInChannelInfo = 5,
        [Description("AO")]
        AnalogRransducerOutChannelInfo = 6,
        [Description("DI")]
        DigitRransducerInChannelInfo = 7,
        [Description("DO")]
        DigitRransducerOutChannelInfo = 8,
        [Description("Relay")]
        RelayChannelInfo = 9,
        [Description("Wave")]
        WaveGeneralChannelInfo = 10,
        [Description("Scalar")]
        WirelessScalarChannelInfo = 11,
        [Description("Vibration")]
        WirelessVibrationChannelInfo = 12,
    }   

    public enum WireType
    {
        Wire = 0,
        Wireless = 1,
    }
}

using AIC.Core.LMModels;
using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Models
{
    public interface ISlot
    {
        
    }
    public interface IWirelessSlot : ISlot
    {
        int SlotNum { get; set; }
    }

    public class WirelessReceiveCard : INotifyPropertyChanged, ICard
    {
        [JsonIgnore]
        public T1_WirelessReceiveCard T_WirelessReceiveCard { get; set; }
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
        public string MasterIdentifier {get; set;} //标识符

        private string receiveCardName;
        public string ReceiveCardName //名称
        {
            get { return receiveCardName; }
            set
            {
                receiveCardName = value;
                OnPropertyChanged("ReceiveCardName");
            }
        }
        public List<TransmissionCard> TransmissionCard { get; set; } //传输卡

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public partial class TransmissionCard : INotifyPropertyChanged, ICard
    {
        [JsonIgnore]
        public T1_TransmissionCard T_TransmissionCard { get; set; }
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
        public string SlaveIdentifier { get; set; } //标识符
        public int TransmissionType { get; set; } //传输类型
        public string Version { get; set; } //版本号

        private string transmissionName;
        public string TransmissionName//名称
        {
            get { return transmissionName; }
            set
            {
                transmissionName = value;
                OnPropertyChanged("TransmissionName");
            }
        }

        private int workTime;
        public int WorkTime  //工作时间
        {
            get { return workTime; }
            set
            {
                workTime = value;
                OnPropertyChanged("WorkTime");
            }
        }

        private int sleepTime;
        public int SleepTime//休眠时间
        {
            get { return sleepTime; }
            set
            {
                sleepTime = value;
                OnPropertyChanged("SleepTime");
            }
        }

        private float batteryEnergy;
        public float BatteryEnergy//电量
        {
            get { return batteryEnergy; }
            set
            {
                batteryEnergy = value;
                OnPropertyChanged("BatteryEnergy");
            }
        }

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
        public ExtraInfo ExtraInfo { get; set; } //附加信息
        public WirelessScalarSlot WirelessScalarSlot { get; set; } //标量槽
        public WirelessVibrationSlot WirelessVibrationSlot { get; set; } //振动槽

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class ExtraInfo
    {

    }

    public class WirelessScalarSlot : INotifyPropertyChanged, IWirelessSlot
    {
        [JsonIgnore]
        public T1_WirelessScalarSlot T_WirelessScalarSlot { get; set; }
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
        public List<WirelessScalarChannelInfo> WirelessScalarChannelInfo { get; set; }//标量通道信息   
        public int SlotNum { get; set; }//槽号	始终为0

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class WirelessScalarChannelInfo : TransformMethod, IChannel
    {
        [JsonIgnore]
        public T1_WirelessScalarChannelInfo T_WirelessScalarChannelInfo { get; set; }
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

    public class WirelessVibrationSlot : INotifyPropertyChanged, IWirelessSlot
    {
        public WirelessVibrationSlot()
        {
            SetIntegrationCategory(0);
        }

        [JsonIgnore]
        public T1_WirelessVibrationSlot T_WirelessVibrationSlot { get; set; }
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

        public List<WirelessVibrationChannelInfo> WirelessVibrationChannelInfo { get; set; }//IEPE通道信息

        private int integration;
        public int Integration //积分设置
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
            switch(code)
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
        public List<SampleFreCategory> SampleFreCategory  { get; set; }//采样频率种类

        private int sampleFreCode;
        public int SampleFreCode//采样频率代码
        {
            get { return sampleFreCode; }
            set
            {
                sampleFreCode = value;
                OnPropertyChanged("SampleFreCode");
            }
        }

        public List<SamplePointCategory> SamplePointCategory { get; set; }//采样点数种类

        private int samplePointCode;
        public int SamplePointCode//采样点数代码
        {
            get { return samplePointCode; }
            set
            {
                samplePointCode = value;
                OnPropertyChanged("SamplePointCode");
            }
        }

        public int SlotNum { get; set; }//槽号	始终为1

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public partial class WirelessVibrationChannelInfo : VibrationChannelInfo, DivFreIChannel
    {
        [JsonIgnore]
        public T1_WirelessVibrationChannelInfo T_WirelessVibrationChannelInfo { get; set; }
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

        [JsonIgnore]
        public bool IsReadOnlyVelocityCalibration { get; set; }

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

        [JsonIgnore]
        public bool IsReadOnlyDisplacementCalibration { get; set; }

        public WirelessVibrationChannelInfo()
        {
            IsReadOnlyVelocityCalibration = true;
            IsReadOnlyDisplacementCalibration = true;
            IsReadOnlySensitivity = true;
        }
    }

    public class SampleFreCategory : ICategory//采样频率种类
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
    }

    public class SamplePointCategory : ICategory//采样点数种类   
    {
        public int Code { get; set; }//代码
        public string Name { get; set; }//名称
    }
}

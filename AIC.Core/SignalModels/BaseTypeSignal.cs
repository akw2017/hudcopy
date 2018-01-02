using AIC.Core.DataModels;
using AIC.Core.LMModels;
using AIC.Core.OrganizationModels;
using AIC.CoreType;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.SignalModels
{
    public abstract class BaseTypeSignal : BindableBase
    {
        private IEnumerable<SignalDisplayType> supportedFunViews;
        public BaseTypeSignal()
        {
            supportedFunViews = CreateSupportFunView();
        }

        protected virtual IEnumerable<SignalDisplayType> CreateSupportFunView()
        {
            return new List<SignalDisplayType>();
        }

        public bool SupportFunView(SignalDisplayType displayType)
        {
            return supportedFunViews.Contains(displayType);
        }

        //总厂名称
        private string organizationName;
        public string OrganizationName
        {
            get { return organizationName; }
            set
            {
                if (organizationName != value)
                {
                    organizationName = value;
                    OnPropertyChanged("OrganizationName");
                }
            }
        }

        //设备名称
        private string deviceName;
        public string DeviceName
        {
            get { return deviceName; }
            set
            {
                if (deviceName != value)
                {
                    deviceName = value;
                    OnPropertyChanged("DeviceName");
                }
            }
        }

        //测点名称
        private string itemName;
        public string ItemName
        {
            get { return itemName; }
            set
            {
                if (itemName != value)
                {
                    itemName = value;
                    OnPropertyChanged("ItemName");
                }
            }
        }

        private string organizationDeviceName;
        public string OrganizationDeviceName
        {
            get { return organizationDeviceName; }
            set
            {
                if (organizationDeviceName != value)
                {
                    organizationDeviceName = value;
                    OnPropertyChanged("OrganizationDeviceName");
                }
            }
        }

        public string deviceItemName;
        public string DeviceItemName
        {
            get { return deviceItemName; }
            set
            {
                if (deviceItemName != value)
                {
                    deviceItemName = value;
                    OnPropertyChanged("DeviceItemName");
                }
            }
        }

        private string fullName;//完整名字
        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;                
            }
        }

        private string[] names;
        public string[] Names
        {
            get { return names; }
            set
            {
                names = value;
                if (names.Length >= 3)
                {
                    ItemName = names[0];
                    DeviceName = names[1];
                    
                    OrganizationName = string.Join("-", names.Where((p, i)=> i != 0 && i != 1).Select(p => p).Reverse());
                    OrganizationDeviceName = OrganizationName + "-" + DeviceName;
                    DeviceItemName = DeviceName + "-" + ItemName;
                    FullName = OrganizationName + "-" + DeviceName + "-" + ItemName;
                }
                OnPropertyChanged("Names");
            }
        }

        //取消分组使用
        public string NullName
        {
            get { return ""; }
        }

        private string ip;
        public string IP
        {
            get { return ip; }
            set
            {
                if (ip != value)
                {
                    ip = value;
                    OnPropertyChanged("IP");
                }
            }
        }
        public string ServerIP { get; set; }
        public Guid Guid { get; protected set; }       

        private bool? isNotOK;
        public bool? IsNotOK//通道是否正常
        {
            get { return isNotOK; }
            set
            {
                isNotOK = value;
                OnPropertyChanged("IsNotOK");
            }
        }

        public bool? delayIsNotOK;
        public bool? DelayIsNotOK//通道是否正常
        {
            get { return delayIsNotOK; }
            set
            {
                delayIsNotOK = value;
                OnPropertyChanged("DelayIsNotOK");
            }
        }

        private bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
            set { SetProperty(ref isConnected, value); }
        }

        private bool isRunning;
        public bool IsRunning
        {
            get { return isRunning; }
            set { SetProperty(ref isRunning, value); }
        }

        private IBaseAlarmSlot iBaseTypeSlot;
        public IBaseAlarmSlot IBaseAlarmSlot
        {
            get { return iBaseTypeSlot; }
            set { SetProperty(ref iBaseTypeSlot, value); }
        }

        private float batteryEnergy;
        public float BatteryEnergy
        {
            get { return batteryEnergy; }
            set { SetProperty(ref batteryEnergy, value); }
        }

        public bool HasBattery { get; set; }

        //private bool isBypass;
        //public bool IsBypass
        //{
        //    get { return isBypass; }
        //    set { SetProperty(ref isBypass, value); }
        //}

        //private bool isUpload;
        //public bool IsUpload
        //{
        //    get { return isUpload; }
        //    set { SetProperty(ref isUpload, value); }
        //}

        //private ChannelStatus channelStatus;
        //public ChannelStatus ChannelStatus
        //{
        //    get { return channelStatus; }
        //    set
        //    {
        //        if (value != channelStatus)
        //        {
        //            channelStatus = value;
        //            OnPropertyChanged("ChannelStatus");
        //        }
        //    }
        //}
        //测点名称
        //public string DeviceItemName { get { return string.Format("{0}.{1}", DeviceName, ItemName); } }       

        public bool IsResetFlag { get; set; }

        //public override string ToString()
        //{
        //    return DeviceItemName;
        //}

        public IObservable<string> WhenPropertyChanged
        {
            get
            {
                return Observable
                    .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                        h => this.PropertyChanged += h,
                        h => this.PropertyChanged -= h)
                    .Select(x => x.EventArgs.PropertyName);
            }
        }
    }
}
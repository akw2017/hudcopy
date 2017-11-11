using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public class T1_TransmissionCard : T_TransmissionCard, Iid//传输卡
    {
        //public int id { get; set; }//自增ID
        //public string SlaveIdentifier { get; set; }//标识符
        //public int TransmissionType { get; set; }//传输类型
        //public string Version { get; set; }//版本号
        //public string TransmissionName { get; set; }//名称
        //public int WorkTime { get; set; }//工作时间
        //public int SleepTime { get; set; }//休眠时间
        //public float BatteryEnergy { get; set; }//电量
        //public string Remarks { get; set; }//备注
        //public string ExtraInfo { get; set; }//附加信息
        //public string Code { get; set; }//代号
        //public string T_WirelessReceiveCard_Code { get; set; }//无线接受卡代号

        public T1_TransmissionCard TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_TransmissionCard();
            TempData.id = this.id;
            TempData.SlaveIdentifier = this.SlaveIdentifier;
            TempData.TransmissionType = this.TransmissionType;
            TempData.Version = this.Version;
            TempData.TransmissionName = this.TransmissionName;
            TempData.WorkTime = this.WorkTime;
            TempData.SleepTime = this.SleepTime;
            TempData.BatteryEnergy = this.BatteryEnergy;
            TempData.Remarks = this.Remarks;
            TempData.ExtraInfo = this.ExtraInfo;
            TempData.Code = this.Code;
            TempData.T_WirelessReceiveCard_Code = this.T_WirelessReceiveCard_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.SlaveIdentifier = TempData.SlaveIdentifier;
            this.TransmissionType = TempData.TransmissionType;
            this.Version = TempData.Version;
            this.TransmissionName = TempData.TransmissionName;
            this.WorkTime = TempData.WorkTime;
            this.SleepTime = TempData.SleepTime;
            this.BatteryEnergy = TempData.BatteryEnergy;
            this.Remarks = TempData.Remarks;
            this.ExtraInfo = TempData.ExtraInfo;
            this.Code = TempData.Code;
            this.T_WirelessReceiveCard_Code = TempData.T_WirelessReceiveCard_Code;

            TempData = null;
        }
    }
}

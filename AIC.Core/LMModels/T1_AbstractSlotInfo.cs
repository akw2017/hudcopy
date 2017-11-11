using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public class T1_AbstractSlotInfo : T_AbstractSlotInfo, Iid//抽象信息槽
    {
        //public int id { get; set; }//自增ID
        //public string InSignalCategory { get; set; }//输入信号种类
        //public int InSignalCode { get; set; }//输入信号代码
        //public int SlotNum { get; set; }//槽号
        //public string SlotName { get; set; }//槽名
        //public int UploadIntevalTime { get; set; }//上传时间间隔
        //public bool IsInput { get; set; }//输入或输出槽
        //public string Unit { get; set; }//单位
        //public string Version { get; set; }//版本
        //public string Code { get; set; }//抽象信息槽代号
        //public string T_WireMatchingCard_Code { get; set; }//配板代号

        public T1_AbstractSlotInfo TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_AbstractSlotInfo();
            TempData.id = this.id;
            TempData.InSignalCategory = this.InSignalCategory;
            TempData.InSignalCode = this.InSignalCode;
            TempData.SlotNum = this.SlotNum;
            TempData.SlotName = this.SlotName;
            TempData.UploadIntevalTime = this.UploadIntevalTime;
            TempData.IsInput = this.IsInput;
            TempData.Unit = this.Unit;
            TempData.Version = this.Version;
            TempData.Code = this.Code;
            TempData.T_WireMatchingCard_Code = this.T_WireMatchingCard_Code;           
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.InSignalCategory = TempData.InSignalCategory;
            this.InSignalCode = TempData.InSignalCode;
            this.SlotNum = TempData.SlotNum;
            this.SlotName = TempData.SlotName;
            this.UploadIntevalTime = TempData.UploadIntevalTime;
            this.IsInput = TempData.IsInput;
            this.Unit = TempData.Unit;
            this.Version = TempData.Version;
            this.Code = TempData.Code;
            this.T_WireMatchingCard_Code = TempData.T_WireMatchingCard_Code;          
            TempData = null;
        }
    }
}

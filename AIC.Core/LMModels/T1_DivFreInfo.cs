using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public class T1_DivFreInfo : T_DivFreInfo, Iid//分频信息
    {
        //public int id { get; set; }//自增ID
        //public Guid Guid { get; set; }//Guid
        //public string Code { get; set; }//Code
        //public string Name { get; set; }//名称
        //public DateTime? Create_Time { get; set; }//创建时间
        //public DateTime? Modify_Time { get; set; }//修改时间
        //public string Remarks { get; set; }//描述
        //public Guid T_Item_Guid { get; set; }//Guid
        //public string T_Item_Name { get; set; }//名称
        //public string T_Item_Code { get; set; }//代号
        //public int DivFreCode { get; set; }//分频代码
        //public string BasedOnRPM { get; set; }//基于转速
        //public string FixedFre { get; set; }//固定频率
        //public string BasedOnRange { get; set; }//基于范围
        //public string AlarmStrategy { get; set; }//报警策略
        //public string T_AbstractChannelInfo_Code { get; set; }//抽象通道代号
        public T1_DivFreInfo TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_DivFreInfo();
            TempData.id = this.id;
            TempData.Guid = this.Guid;
            TempData.Code = this.Code;
            TempData.Name = this.Name;          
            TempData.Create_Time = this.Create_Time;
            TempData.Modify_Time = this.Modify_Time;
            TempData.Remarks = this.Remarks;
            TempData.T_Item_Guid = this.T_Item_Guid;
            TempData.T_Item_Name = this.T_Item_Name;
            TempData.T_Item_Code = this.T_Item_Code;
            TempData.DivFreCode = this.DivFreCode;
            TempData.BasedOnRPM = this.BasedOnRPM;
            TempData.FixedFre = this.FixedFre;
            TempData.BasedOnRange = this.BasedOnRange;
            TempData.AlarmStrategy = this.AlarmStrategy;           
            TempData.T_AbstractChannelInfo_Code = this.T_AbstractChannelInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.Guid = TempData.Guid;
            this.Code = TempData.Code;
            this.Name = TempData.Name;
            this.Create_Time = TempData.Create_Time;
            this.Modify_Time = TempData.Modify_Time;
            this.Remarks = TempData.Remarks;
            this.T_Item_Guid = TempData.T_Item_Guid;
            this.T_Item_Name = TempData.T_Item_Name;
            this.T_Item_Code = TempData.T_Item_Code;
            this.DivFreCode = TempData.DivFreCode;
            this.BasedOnRPM = TempData.BasedOnRPM;
            this.FixedFre = TempData.FixedFre;
            this.BasedOnRange = TempData.BasedOnRange;
            this.AlarmStrategy = TempData.AlarmStrategy;
            this.T_AbstractChannelInfo_Code = TempData.T_AbstractChannelInfo_Code;
            TempData = null;
        }
    }
}

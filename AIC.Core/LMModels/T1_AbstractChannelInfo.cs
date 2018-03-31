using AIC.Core.HardwareModels;
using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.M9600.Common.MasterDB.Generated;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public class T1_AbstractChannelInfo : T_AbstractChannelInfo, Iid//抽象信息通道
    {
        //public int id { get; set; }//自增ID
        //public string Organization { get; set; }//组织机构
        //public string T_Device_Name { get; set; }//设备名称
        //public string T_Device_Code { get; set; }//设备编号
        //public Guid T_Device_Guid { get; set; }//设备Guid
        //public string T_Item_Name { get; set; }//测点名称
        //public string T_Item_Code { get; set; }//测点编号
        //public Guid T_Item_Guid { get; set; }//测点Guid
        //public int CHNum { get; set; }//通道号
        //public int SubCHNum { get; set; }//子通道号
        //public bool IsUploadData { get; set; }//是否上传
        //public string Unit { get; set; }//
        //public string SVTypeCategory { get; set; }//单值类型种类
        //public int SVTypeCode { get; set; }//单值类型代码
        //public string LocalSaveCategory { get; set; }//本地存储策略
        //public int LocalSaveCode { get; set; }//本地存储代码
        //public bool IsBypass { get; set; }//是否旁路
        //public int DelayAlarmTime { get; set; }//延时报警
        //public int NotOKDelayAlarmTime { get; set; }//NotOK延时报警
        //public bool IsLogic { get; set; }//启用逻辑表达式
        //public string LogicExpression { get; set; }//逻辑表达式
        //public string Remarks { get; set; }//备注
        //public string Extra_Information { get; set; }//附加信息
        //public string AlarmStrategy { get; set; }//报警策略
        //public string Code { get; set; }//抽象信息通道代号
        //public string T_AbstractSlotInfo_Code { get; set; }//抽象槽代号
        public void BindTemp(ItemTreeItemViewModel item, List<Organization> organizations, DeviceTreeItemViewModel device)
        {
            SaveTempData();

            TempData.Organization = JsonConvert.SerializeObject(organizations);
            TempData.T_Item_Name = organizations[0].Name;//item.T_Item.Name;
            TempData.T_Item_Code = item.T_Item.Code;
            TempData.T_Item_Guid = item.T_Item.Guid;
            TempData.T_Device_Name = device.T_Organization.Name;//htzk123,可能有问题
            TempData.T_Device_Code = device.T_Organization.Code;
            TempData.T_Device_Guid = device.T_Organization.Guid;
        }

        public void UnBindTemp()
        {
            SaveTempData();
          
            TempData.Organization = "";          
            TempData.T_Item_Name = "";
            TempData.T_Item_Code = "";
            TempData.T_Item_Guid = null;
            TempData.T_Device_Name = "";
            TempData.T_Device_Code = "";
            TempData.T_Device_Guid = null;
        }

        public T1_AbstractChannelInfo TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_AbstractChannelInfo();
            TempData.id = this.id;
            TempData.Organization = this.Organization;
            TempData.T_Device_Name = this.T_Device_Name;
            TempData.T_Device_Code = this.T_Device_Code;
            TempData.T_Device_Guid = this.T_Device_Guid;
            TempData.T_Item_Name = this.T_Item_Name;
            TempData.T_Item_Code = this.T_Item_Code;
            TempData.T_Item_Guid = this.T_Item_Guid;
            TempData.CHNum = this.CHNum;
            TempData.SubCHNum = this.SubCHNum;
            TempData.IsUploadData = this.IsUploadData;
            TempData.Unit = this.Unit;
            TempData.SVTypeCategory = this.SVTypeCategory;
            TempData.SVTypeCode = this.SVTypeCode;
            TempData.LocalSaveCategory = this.LocalSaveCategory;
            TempData.LocalSaveCode = this.LocalSaveCode;
            TempData.IsBypass = this.IsBypass;
            TempData.DelayAlarmTime = this.DelayAlarmTime;
            TempData.NotOKDelayAlarmTime = this.NotOKDelayAlarmTime;
            TempData.IsLogic = this.IsLogic;
            TempData.LogicExpression = this.LogicExpression;
            TempData.Remarks = this.Remarks;
            TempData.Extra_Information = this.Extra_Information;
            TempData.AlarmStrategy = this.AlarmStrategy;
            TempData.Code = this.Code;
            TempData.T_AbstractSlotInfo_Code = this.T_AbstractSlotInfo_Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.Organization = TempData.Organization;
            this.T_Device_Name = TempData.T_Device_Name;
            this.T_Device_Code = TempData.T_Device_Code;
            this.T_Device_Guid = TempData.T_Device_Guid;
            this.T_Item_Name = TempData.T_Item_Name;
            this.T_Item_Code = TempData.T_Item_Code;
            this.T_Item_Guid = TempData.T_Item_Guid;
            this.CHNum = TempData.CHNum;
            this.SubCHNum = TempData.SubCHNum;
            this.IsUploadData = TempData.IsUploadData;
            this.Unit = TempData.Unit;
            this.SVTypeCategory = TempData.SVTypeCategory;
            this.SVTypeCode = TempData.SVTypeCode;
            this.LocalSaveCategory = TempData.LocalSaveCategory;
            this.LocalSaveCode = TempData.LocalSaveCode;
            this.IsBypass = TempData.IsBypass;
            this.DelayAlarmTime = TempData.DelayAlarmTime;
            this.NotOKDelayAlarmTime = TempData.NotOKDelayAlarmTime;
            this.IsLogic = TempData.IsLogic;
            this.LogicExpression = TempData.LogicExpression;
            this.Remarks = TempData.Remarks;
            this.Extra_Information = TempData.Extra_Information;
            this.AlarmStrategy = TempData.AlarmStrategy;
            this.Code = TempData.Code;
            this.T_AbstractSlotInfo_Code = TempData.T_AbstractSlotInfo_Code;
            TempData = null;
        }

      
    }
}

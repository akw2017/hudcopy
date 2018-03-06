using AIC.Core.Models;
using AIC.M9600.Common.DTO.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.SignalModels
{
    public class StatisticalInformationData
    {
        public Guid T_Item_Guid { get; set; }
        public DateTime ACQDatetime { get; set; } //采集时间
        public int AlarmGrade { get; set; } //报警级别
        //Guid? RecordLab { get; set; } //guid
        //Guid? SaveLab { get; set; } //guid
        //Guid? ContinueLab { get; set; } //guid      
        //ExtraInfo ExtraInfo { get; set; } //扩张信息
        public double? Result { get; set; } //结果数据
        public string Unit { get; set; } //单位
        //AlarmLimit[] AlarmLimit { get; set; }
        public static string GetColumnsString()
        {
            string columns = string.Empty;
            PropertyInfo[] ps = new StatisticalInformationData().GetType().GetProperties();

            foreach (PropertyInfo info in ps)
            {
                columns += info.Name + ",";
            }
            if (columns == string.Empty)
            {
                return "*";
            }
            else
            {
                return columns.Substring(0, columns.Length - 1);
            }
        }
    }
}

using AIC.Core.Models;
using AIC.M9600.Common.DTO.Device;
using Newtonsoft.Json;
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
        public DateTime? ACQDatetime { get; set; } //采集时间
        public int AlarmGrade { get; set; } //报警级别
        public int Low8Alarm { get { return this.AlarmGrade & 0xff; } }
        public double PercentResult
        {
            get
            {
                if (this.AlarmLimit != null && this.AlarmLimit.Length > 0)
                {
                    var alarmMax = this.AlarmLimit.Select(p => Math.Abs(p.Limit)).Max();
                    var percentResult = (Math.Abs(Result ?? 0)) / alarmMax * 100;
                    return (percentResult > 100) ? 100 : percentResult;
                }
                else
                {
                    var percentResult = Math.Abs(Result ?? 0);
                    return (percentResult > 100) ? 100 : percentResult;
                }
            }

        }
        //Guid? RecordLab { get; set; } //guid
        //Guid? SaveLab { get; set; } //guid
        //Guid? ContinueLab { get; set; } //guid      
        //ExtraInfo ExtraInfo { get; set; } //扩张信息
        public double? Result { get; set; } //结果数据
        public string Unit { get; set; } //单位
        public AlarmLimit[] AlarmLimit
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.AlarmLimitJSON))
                {
                    return JsonConvert.DeserializeObject<AlarmLimit[]>(this.AlarmLimitJSON);
                }
                else return null;
            }
        }

        public string AlarmLimitJSON { get; set; }
        public static string GetColumnsString()
        {
            string columns = string.Empty;
            PropertyInfo[] ps = new StatisticalInformationData().GetType().GetProperties();

            foreach (PropertyInfo info in ps)
            {
                if (info.CanWrite)
                {
                    columns += info.Name + ",";
                }
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

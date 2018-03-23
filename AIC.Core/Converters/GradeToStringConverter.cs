using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AIC.Core
{
    public class GradeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is AlarmGrade || value is int)
            {
                AlarmGrade grade = (AlarmGrade)((int)value & 0x00ffff00);//兼容历史数据没有去除后8位
                if ((AlarmGrade)value == AlarmGrade.DisConnect)//DisConnect没有在中间8位
                {
                    grade = AlarmGrade.DisConnect;
                }
                switch (grade)
                {
                    case AlarmGrade.Invalid: return "无效";
                    case AlarmGrade.HighNormal: 
                    case AlarmGrade.LowNormal: return "正常";
                    case AlarmGrade.HighPreAlarm: 
                    case AlarmGrade.LowPreAlarm: return "预警";
                    case AlarmGrade.HighAlarm: 
                    case AlarmGrade.LowAlarm: return "警告";
                    case AlarmGrade.HighDanger: 
                    case AlarmGrade.LowDanger: return "危险";
                    case AlarmGrade.DisConnect:return "断线";
                    default: return "";
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static string Convert(AlarmGrade grade)//后台代码使用
        {
            switch (grade)
            {
                case AlarmGrade.Invalid: return "无效";
                case AlarmGrade.HighNormal:
                case AlarmGrade.LowNormal: return "正常";
                case AlarmGrade.HighPreAlarm:
                case AlarmGrade.LowPreAlarm: return "预警";
                case AlarmGrade.HighAlarm:
                case AlarmGrade.LowAlarm: return "警告";
                case AlarmGrade.HighDanger:
                case AlarmGrade.LowDanger: return "危险";
                case AlarmGrade.DisConnect: return "断线";
                    //default:return grade.ToString();
            }
            return null;
        }
    }


}

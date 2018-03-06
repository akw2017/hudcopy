using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace AIC.Core
{
    public class GradeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is AlarmGrade || value is int)
            {
                AlarmGrade grade = (AlarmGrade)value;
                switch (grade)
                {
                    case AlarmGrade.Invalid: return new SolidColorBrush(Color.FromRgb(0x00, 0x80, 0x00));//绿色
                    case AlarmGrade.HighNormal: 
                    case AlarmGrade.LowNormal: return new SolidColorBrush(Color.FromRgb(0x00, 0x80, 0x00));//绿色
                    case AlarmGrade.HighPreAlarm: 
                    case AlarmGrade.LowPreAlarm: return new SolidColorBrush(Color.FromRgb(0xff, 0xff, 0x00));//黄色
                    case AlarmGrade.HighAlarm: 
                    case AlarmGrade.LowAlarm: return new SolidColorBrush(Color.FromRgb(0xff, 0xa5, 0x00));//橙色
                    case AlarmGrade.HighDanger: 
                    case AlarmGrade.LowDanger: return new SolidColorBrush(Color.FromRgb(0xff, 0x00, 0x00));//红色
                    case AlarmGrade.DisConnect: return new SolidColorBrush(Color.FromRgb(0x00, 0x80, 0x00));//绿色
                    default: return new SolidColorBrush(Color.FromRgb(0x00, 0x80, 0x00));//绿色
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

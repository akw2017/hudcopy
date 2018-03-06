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
    public class GradeToMyValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string)
            {
                string para = parameter as string;
                if (value is AlarmGrade || value is int)
                {
                    AlarmGrade grade = (AlarmGrade)value;
                    switch (grade)
                    {
                        case AlarmGrade.Invalid:
                            {
                                if (para == "0")
                                {
                                    return 100;
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                        case AlarmGrade.HighNormal:
                        case AlarmGrade.LowNormal:
                            {
                                {
                                    if (para == "0")
                                    {
                                        return 100;
                                    }
                                    else
                                    {
                                        return 0;
                                    }
                                }
                            }
                        case AlarmGrade.HighPreAlarm:
                        case AlarmGrade.LowPreAlarm:
                            {
                                {
                                    if (para == "1")
                                    {
                                        return 100;
                                    }
                                    else
                                    {
                                        return 0;
                                    }
                                }
                            }
                        case AlarmGrade.HighAlarm:
                        case AlarmGrade.LowAlarm:
                            {
                                {
                                    if (para == "2")
                                    {
                                        return 100;
                                    }
                                    else
                                    {
                                        return 0;
                                    }
                                }
                            }
                        case AlarmGrade.HighDanger:
                        case AlarmGrade.LowDanger:
                            {
                                {
                                    if (para == "3")
                                    {
                                        return 100;
                                    }
                                    else
                                    {
                                        return 0;
                                    }
                                }
                            }
                        case AlarmGrade.DisConnect:
                            {
                                {
                                    if (para == "0")
                                    {
                                        return 100;
                                    }
                                    else
                                    {
                                        return 0;
                                    }
                                }
                            }
                        default: return 0;//绿色
                    }
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

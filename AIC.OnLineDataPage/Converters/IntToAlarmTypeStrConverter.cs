using System;
using System.Globalization;
using System.Windows.Data;

namespace AIC.OnLineDataPage.Converters
{
    public class IntToAlarmTypeStrConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string alarmTypeStr = string.Empty;
            int consts = (int)value & 0X03;
            int dynamic = (int)value & 0X0C;
            int comparative = (int)value & 0X30;
            switch (consts)
            {
                case 2:
                    alarmTypeStr += "常数报警：警告";
                    break;
                case 3:
                    alarmTypeStr += "常数报警：危险";
                    break;
            }
            switch (dynamic)
            {
                case 8:
                    alarmTypeStr += "\r\n" + "曲线报警：警告";
                    break;
                case 12:
                    alarmTypeStr += "\r\n" + "曲线报警：危险";
                    break;
            }
            switch (comparative)
            {
                case 32:
                    alarmTypeStr += "\r\n" + "相对报警：警告";
                    break;
                case 48:
                    alarmTypeStr += "\r\n" + "相对报警：危险";
                    break;
            }
            return alarmTypeStr;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}

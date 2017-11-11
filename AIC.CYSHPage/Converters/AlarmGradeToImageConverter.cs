using AIC.CoreType;
using System;
using System.Globalization;
using System.Windows.Data;

namespace AIC.CYSHPage.Converters
{
    public class AlarmGradeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is AlarmGrade)
            {
               // string assemblyRelativePath = string.Format("/{0};component/Images/", Assembly.GetExecutingAssembly().GetName().Name);
                string assemblyRelativePath = "../Images/";
                string imageName = string.Empty;
                var alarmGrade = (AlarmGrade)value;
                switch(alarmGrade)
                {
                    case AlarmGrade.Invalid:
                        imageName = "AbNormal.png";
                        break;
                    case AlarmGrade.HighNormal:
                        imageName = "Normal.png";
                        break;
                    case AlarmGrade.LowNormal:
                        imageName = "Normal.png";
                        break;
                    case AlarmGrade.HighAlert:
                        imageName ="Warning.png";
                        break;
                    case AlarmGrade.LowAlert:
                        imageName = "Warning.png";
                        break;
                    case AlarmGrade.HighDanger:
                        imageName ="Danger.png";
                        break;
                    case AlarmGrade.LowDanger:
                        imageName = "Danger.png";
                        break;
                }
                if (string.IsNullOrEmpty(imageName))
                {
                    imageName = "Null.png";
                }
                return assemblyRelativePath + imageName;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}

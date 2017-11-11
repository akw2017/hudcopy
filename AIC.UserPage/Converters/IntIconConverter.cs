using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AIC.UserPage.Converters
{
   
    public class IntIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                int number = (int)value;
                switch(number)
                {
                    case 0: return "/AIC.Resources;component/Images/users.png";
                    case 1: return "/AIC.Resources;component/Images/user.png";
                    case 2: return "/AIC.Resources;component/Images/application_edit.png";
                    case 3: return "/AIC.Resources;component/Images/chart_organisation.png";
                    case 4: return "/AIC.Resources;component/Images/page_error.png";
                    case 5: return "/AIC.Resources;component/Images/database_gear.png";
                    case 6: return "/AIC.Resources;component/Images/brick_edit.png";
                    case 7: return "/AIC.Resources;component/Images/chart_organisation.png";
                    case 8: return "/AIC.Resources;component/Images/database_refresh.png";
                    case 9: return "/AIC.Resources;component/Images/database_save.png";
                    case 10: return "/AIC.Resources;component/Images/database_error.png";
                    case 11: return "/AIC.Resources;component/Images/database_go.png";
                    case 12: return "/AIC.Resources;component/Images/database_lightning.png";
                    case 13: return "/AIC.Resources;component/Images/application_view_tile.png";
                    case 14: return "/AIC.Resources;component/Images/application_view_detail.png";
                    case 15: return "/AIC.Resources;component/Images/application_view_gallery.png";
                    case 16: return "/AIC.Resources;component/Images/application_home.png";
                    case 17: return "/AIC.Resources;component/Images/table_save.png";
                    case 18: return "/AIC.Resources;component/Images/picture_save.png";
                    case 19: return "/AIC.Resources;component/Images/pill_go.png";
                    case 20: return "/AIC.Resources;component/Images/chart_bar.png";
                }

            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

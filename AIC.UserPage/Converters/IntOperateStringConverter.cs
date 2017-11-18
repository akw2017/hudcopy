using AIC.Core.LMModels;
using AIC.Core.UserManageModels;
using AIC.CoreType;
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
   
    public class IntOperateStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is short)
            {
                if ((short)value == 0)
                {
                    return "登陆";
                }
                if ((short)value == (short)OperateType.MenuManage)
                {
                    return "菜单管理";
                }
                if ((short)value == (short)OperateType.OrganizationManage)
                {
                    return "组织管理";
                }
                if ((short)value == (short)OperateType.RoleManage)
                {
                    return "角色管理";
                }
                if ((short)value == (short)OperateType.UserManage)
                {
                    return "用户管理";
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

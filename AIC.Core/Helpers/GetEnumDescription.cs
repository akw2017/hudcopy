using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Helpers
{
    public static class GetEnumDescription
    {
        public static string GetDescription(Enum iValue)
        {
            FieldInfo fi = iValue.GetType().GetField(iValue.ToString());

            if (fi != null)
            {
                var attributes = (EnumDescription[])fi.GetCustomAttributes(typeof(EnumDescription), false);

                if (attributes.Length == 0)
                {
                    var descriptions = (DescriptionAttribute[])fi.
                        GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (descriptions.Length > 0)
                    {
                        return descriptions[0].Description;
                    }
                }

                return ((attributes.Length > 0) &&
                            (!String.IsNullOrEmpty(attributes[0].GetDescription())))
                        ? attributes[0].GetDescription()
                        : iValue.ToString();
            }

            return null;
        }
    }
}

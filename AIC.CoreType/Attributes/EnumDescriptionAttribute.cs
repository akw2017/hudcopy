using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Resources;

namespace AIC.CoreType
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class EnumDescription : DescriptionAttribute
    {
        private object _enumValue;

        public EnumDescription()
            : base()
        {
        }


        /// <summary>
        /// Gets or sets the enum value.
        /// </summary>
        /// <value>
        /// The enum value.
        /// </value>
        public object EnumValue
        {
            get
            {
                return _enumValue;
            }
            set
            {
                _enumValue = value;
            }
        }


        /// <summary>
        /// Gets the description stored in this attribute.
        /// </summary>
        /// <returns>The description stored in this attribute.</returns>
        public override string Description
        {
            get
            {
                return GetDescription();
            }
        }


        /// <summary>
        /// Gets the description.
        /// </summary>
        public string GetDescription()
        {
            var sb = new StringBuilder();
            Type enumType = EnumValue.GetType();

            sb.Append(string.Format("{0}_{1}", enumType.Name, EnumValue));

            return GetResourceString(enumType.Assembly, sb.ToString());
        }


        /// <summary>
        /// Gets the resource string.
        /// </summary>
        /// <param name="iAssembly">The assembly.</param>
        /// <param name="iResKey">The res key.</param>
        /// <returns></returns>
        public static string GetResourceString(Assembly iAssembly, string iResKey)
        {
            string ret;
            ResourceManager rm;
            string resManagerName;
            string assemblyName;

            assemblyName = iAssembly.GetName().Name;
            ret = string.Empty;

            resManagerName = string.Format("{0}.Properties.Resources", assemblyName);

            rm = new ResourceManager(resManagerName, iAssembly);

            ret = rm.GetString(iResKey);

            return ret;
        }

    }
}

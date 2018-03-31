using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.Core.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.HardwareModels
{
    public partial class TransmissionCard : IDataErrorInfo
    {
        class TransmissionCardMetadata
        {
            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的整数。")]
            public int WorkTime { get; set; }

            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的整数。")]
            public int SleepTime { get; set; }
        }

        [JsonIgnore]
        public string Error
        {
            get
            {
                string error = null;
                PropertyInfo[] propertys = this.GetType().GetProperties();
                foreach (PropertyInfo pinfo in propertys)
                {
                    //循环遍历属性
                    if (pinfo.CanRead && pinfo.CanWrite)
                    {
                        error = this.ValidateProperty<TransmissionCardMetadata>(pinfo.Name);
                        if (error != null && error.Length > 0)
                        {
                            break;
                        }
                    }                                  
                }
                return error;
            }
        }

        public string this[string columnName]
        {
            get
            {
                return this.ValidateProperty<TransmissionCardMetadata>(columnName);
            }
        }
    }

    public partial class WirelessVibrationChannelInfo : IDataErrorInfo
    {
        class WirelessVibrationChannelInfoMetadata
        {
            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的数。")]
            public float VelocityCalibration { get; set; }

            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的数。")]
            public float DisplacementCalibration { get; set; }
        }

        [JsonIgnore]
        public new string Error
        {
            get
            {
                if (base.Error != null)
                {
                    return base.Error;
                }
                string error = null;
                PropertyInfo[] propertys = this.GetType().GetProperties();
                foreach (PropertyInfo pinfo in propertys)
                {
                    //循环遍历属性
                    if (pinfo.CanRead && pinfo.CanWrite)
                    {
                        error = this.ValidateProperty<WirelessVibrationChannelInfoMetadata>(pinfo.Name);
                        if (error != null && error.Length > 0)
                        {
                            break;
                        }
                    }
                }
                return error;
            }
        }

        public new string this[string columnName]
        {
            get
            {
                return this.ValidateProperty<WirelessVibrationChannelInfoMetadata>(columnName);
            }
        }
    }
}

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

namespace AIC.Core.Models
{
    
    public partial class AbstractSlotInfo : IDataErrorInfo
    {
        class AbstractSlotInfoMetadata
        {
            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的整数。")]
            public int UploadIntevalTime { get; set; }          
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
                        error = this.ValidateProperty<AbstractSlotInfoMetadata>(pinfo.Name);
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
                return this.ValidateProperty<AbstractSlotInfoMetadata>(columnName);
            }
        }
    }

    public partial class IEPEChannelInfo : IDataErrorInfo
    {
        class IEPEChannelInfoMetadata
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
                        error = this.ValidateProperty<IEPEChannelInfoMetadata>(pinfo.Name);
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
                return this.ValidateProperty<IEPEChannelInfoMetadata>(columnName);
            }
        }
    }

    public partial class VibrationChannelInfo : IDataErrorInfo
    {
        class VibrationChannelInfoMetadata
        {
            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的整数。")]
            public int RPMCardNum { get; set; }

            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的整数。")]
            public int RPMSlotNum { get; set; }

            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的整数。")]
            public int RPMCHNum { get; set; }

            [Range(1, 65535, ErrorMessage = "请输入1-65535之间的数。")]
            public float MultiplicationCor { get; set; }

            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的数。")]
            public float DefaultRPM { get; set; }
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
                        error = this.ValidateProperty<VibrationChannelInfoMetadata>(pinfo.Name);
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
                return this.ValidateProperty<VibrationChannelInfoMetadata>(columnName);
            }
        }
    }

    public partial class EddyCurrentRPMChannelInfo : IDataErrorInfo
    {
        class EddyCurrentRPMChannelInfoMetadata
        {
            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的数。")]
            public float ThresholdVolt { get; set; }

            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的数。")]
            public float HysteresisVolt { get; set; }
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
                        error = this.ValidateProperty<EddyCurrentRPMChannelInfoMetadata>(pinfo.Name);
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
                return this.ValidateProperty<EddyCurrentRPMChannelInfoMetadata>(columnName);
            }
        }
    }

    public partial class RPMChannelInfo : IDataErrorInfo
    {
        class RPMChannelInfoMetadata
        {
            [Range(1, 65535, ErrorMessage = "请输入1-65535之间的数。")]
            public float CalibrationCor { get; set; }

            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的整数。")]
            public int AverageNumber { get; set; }

            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的数。")]
            public float TeethNumber { get; set; }           
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
                        error = this.ValidateProperty<RPMChannelInfoMetadata>(pinfo.Name);
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
                return this.ValidateProperty<RPMChannelInfoMetadata>(columnName);
            }
        }
    }

    public partial class VibrationAddition : IDataErrorInfo
    {
        class VibrationAdditionMetadata
        {
            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的数。")]
            public float BiasVoltHigh { get; set; }

            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的数。")]
            public float BiasVoltLow { get; set; }

            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的数。")]
            public float Sensitivity { get; set; }
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
                        error = this.ValidateProperty<VibrationAdditionMetadata>(pinfo.Name);
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
                return this.ValidateProperty<VibrationAdditionMetadata>(columnName);
            }
        }
    }

    public partial class AbstractChannelInfo : IDataErrorInfo
    {
        class AbstractChannelInfoMetadata
        {
            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的整数。")]
            public int DelayAlarmTime { get; set; }

            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的整数。")]
            public int NotOKDelayAlarmTime { get; set; }            
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
                        error = this.ValidateProperty<AbstractChannelInfoMetadata>(pinfo.Name);
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
                return this.ValidateProperty<AbstractChannelInfoMetadata>(columnName);
            }
        }
    }

    public partial class SVTypeCategory : IDataErrorInfo
    {
        class SVTypeCategoryMetadata
        {
            [Range(-24, 24, ErrorMessage = "请输入-24-24之间的整数。")]
            public float ZeroValue { get; set; }          
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
                        error = this.ValidateProperty<SVTypeCategoryMetadata>(pinfo.Name);
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
                return this.ValidateProperty<SVTypeCategoryMetadata>(columnName);
            }
        }
    }

    public partial class TPDirCategory : IDataErrorInfo
    {
        class TPDirCategoryMetadata
        {
            [Range(0, 359, ErrorMessage = "请输入0-359之间的数。")]
            public float Degree { get; set; }
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
                        error = this.ValidateProperty<TPDirCategoryMetadata>(pinfo.Name);
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
                return this.ValidateProperty<TPDirCategoryMetadata>(columnName);
            }
        }
    }

    public partial class AlarmCategory : IDataErrorInfo
    {
        class AlarmCategoryMetadata
        {
            [Range(-65536, 65535, ErrorMessage = "请输入0-65535之间的数。")]
            public float Value { get; set; }
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
                        error = this.ValidateProperty<AlarmCategoryMetadata>(pinfo.Name);
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
                return this.ValidateProperty<AlarmCategoryMetadata>(columnName);
            }
        }
    }

    public partial class ComparativeAlarm : IDataErrorInfo
    {
        class ComparativeAlarmMetadata
        {
            [RangeStringValidation]
            public string Range { get; set; }

            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的整数。")]
            public float IntevalTime { get; set; }

            [Range(0, 100, ErrorMessage = "请输入0-100之间的整数。")]
            public float Percent { get; set; }
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
                        error = this.ValidateProperty<ComparativeAlarmMetadata>(pinfo.Name);
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
                return this.ValidateProperty<ComparativeAlarmMetadata>(columnName);
            }
        }
    }

    public partial class EquationCategory : IDataErrorInfo
    {
        class EquationCategoryAlarmMetadata
        {          
            [Range(1, 65535, ErrorMessage = "请输入1-65535之间的数。")]
            public float CalibrationCor { get; set; }          
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
                        error = this.ValidateProperty<EquationCategoryAlarmMetadata>(pinfo.Name);
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
                return this.ValidateProperty<EquationCategoryAlarmMetadata>(columnName);
            }
        }
    }

    public partial class SourceChannelInfo : IDataErrorInfo
    {
        class SourceChannelInfoMetadata
        {
            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的整数。")]
            public int SourceCardNum { get; set; }

            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的整数。")]
            public int SourceSlotNum { get; set; }

            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的整数。")]
            public int SourceCHNum { get; set; }

            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的整数。")]
            public int SourceSubCHNum { get; set; }
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
                        error = this.ValidateProperty<SourceChannelInfoMetadata>(pinfo.Name);
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
                return this.ValidateProperty<SourceChannelInfoMetadata>(columnName);
            }
        }
    }

    public partial class BasedOnRPM : IDataErrorInfo
    {
        class BasedOnRPMMetadata
        {
            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的数。")]
            public float MultiFre { get; set; }          
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
                        error = this.ValidateProperty<BasedOnRPMMetadata>(pinfo.Name);
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
                return this.ValidateProperty<BasedOnRPMMetadata>(columnName);
            }
        }
    }

    public partial class FixedFre : IDataErrorInfo
    {
        class FixedFreMetadata
        {
            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的数。")]
            public float CharacteristicFre { get; set; }

            [Range(0, 100, ErrorMessage = "请输入0-100之间的数。")]
            public float Percent { get; set; }
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
                        error = this.ValidateProperty<FixedFreMetadata>(pinfo.Name);
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
                return this.ValidateProperty<FixedFreMetadata>(columnName);
            }
        }
    }

    public partial class BasedOnRange : IDataErrorInfo
    {
        class BasedOnRangeMetadata
        {
            //[Range(0, 65535, ErrorMessage = "请输入0-65535之间的数。")]
            //public float FreLow { get; set; }

            //[Range(0, 65535, ErrorMessage = "请输入0-65535之间的数。")]
            //public float FreHigh { get; set; }

            //[Range(0, 65535, ErrorMessage = "请输入0-65535之间的数。")]
            //public float MaxFreNum { get; set; }
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
                        error = this.ValidateProperty<BasedOnRangeMetadata>(pinfo.Name);
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
                return this.ValidateProperty<BasedOnRangeMetadata>(columnName);
            }
        }
    }
    
}

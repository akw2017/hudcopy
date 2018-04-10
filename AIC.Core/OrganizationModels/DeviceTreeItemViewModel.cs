using AIC.Core.ControlModels;
using AIC.Core.DiagnosticBaseModels;
using AIC.Core.LMModels;
using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AIC.Core.OrganizationModels
{
    public class DeviceTreeItemViewModel : OrganizationTreeItemViewModel//设备组织机构树节点
    {
        private bool isRunning;
        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                OnPropertyChanged("IsRunning");
            }
        }

        private DeviceDiagnosisComponent deviceDiagnosisComponent;
        public DeviceDiagnosisComponent DeviceDiagnosisComponent
        {
            get { return deviceDiagnosisComponent; }
            set
            {
                deviceDiagnosisComponent = value;
                OnPropertyChanged("DeviceDiagnosisComponent");
            }
        }

        public T1_Device T_Device { get; set; }
        public DeviceTreeItemViewModel(OrganizationTreeItemViewModel parent) : this("", parent) { }

        public DeviceTreeItemViewModel(string name, OrganizationTreeItemViewModel parent) : base(name, parent)
        {
            if (name == "")
            {
                T_Organization.Name = "新建设备" + (T_Organization.Sort_No + 1).ToString();
            }
            else
            {
                T_Organization.Name = name;
            }
            device_init();
        }

        public DeviceTreeItemViewModel(string name, int sort_no, OrganizationTreeItemViewModel parent) : base(name, sort_no, parent)
        {
            device_init();
        }

        public DeviceTreeItemViewModel(T1_Organization t_organzation, OrganizationTreeItemViewModel parent) : base(t_organzation.Name, t_organzation.Sort_No, parent)
        {
            T_Organization = t_organzation;
            device_init();
        }

        private void device_init()
        {
            T_Device = new T1_Device();
            T_Device.Name = T_Organization.Name;
            T_Device.Code = T_Organization.Code;
            T_Device.Guid = T_Organization.Guid;
            //Model { get; set; }//设备型号
            //Chart_Number { get; set; }//设备图号
            //Manufacturer { get; set; }//生产厂家
            //Serial_Number { get; set; }//出厂序列号
            //Material { get; set; }//设备材质
            //Vendor { get; set; }//供应商
            //Grade { get; set; }//控制等级
            //Status { get; set; }//设备状态
            //Class { get; set; }//设备分类
            //Installation_Site { get; set; }//安装位置
            //Person_In_Charge { get; set; }//设备负责人
            //Date_Of_Production { get; set; }//设备生产日期
            //Date_Of_Entering { get; set; }//设备入厂日期
            //Date_Of_Start { get; set; }//设备启用日期
            T_Device.Create_Time = T_Organization.Create_Time;
            T_Device.Modify_Time = T_Organization.Modify_Time;
            T_Device.Sort_No = T_Organization.Sort_No;
            T_Device.Is_Disabled = T_Organization.Is_Disabled;
            //Extend_Info { get; set; }//扩展信息
            //T_Diagnosis_Model_Guid { get; set; }//诊断模型GUID
            T_Device.T_Organization_Guid = T_Organization.Guid;//组织机构Guid
            T_Device.T_Organization_Code = T_Organization.Code;//组织机构Code
            T_Device.T_Organization_Level = T_Organization.Level;//组织机构Level
            //Remarks { get; set; }//

            T_Organization.NodeType = 1;

            IsExpanded = false;
        }

    }
}

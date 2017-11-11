using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.SignalModels;
using AIC.CoreType;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.OrganizationModels
{
    public class DivFreTreeItemViewModel : OrganizationTreeItemViewModel//分频组织机构树节点
    {
        private AlarmGrade alarm = AlarmGrade.HighNormal;
        public override AlarmGrade Alarm
        {
            get { return alarm; }
            set
            {
                if (alarm != value)
                {
                    alarm = value;
                    OnPropertyChanged("Alarm");
                }
            }
        }

        private bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                if (isConnected != value)
                {
                    isConnected = value;
                    OnPropertyChanged("IsConnected");
                }
            }
        }

        public T1_DivFreInfo T_DivFreInfo { get; set; }

        public DivFreTreeItemViewModel(ItemTreeItemViewModel parent) : this("", parent) { }
                
        public DivFreTreeItemViewModel(ItemTreeItemViewModel parent, T1_DivFreInfo t_divfreinfo, bool auto) : base(parent)
        {
            if (auto == true)//自动生成
            {
                T_Organization.Name = t_divfreinfo.Name;
                T_Organization.Code = t_divfreinfo.Code;
                T_Organization.Guid = t_divfreinfo.Guid;
                T_Organization.Create_Time = t_divfreinfo.Create_Time;
                T_Organization.Modify_Time = t_divfreinfo.Modify_Time;
                T_Organization.Remarks = t_divfreinfo.Remarks;                
            }

            T_Organization.NodeType = 3;
            T_DivFreInfo = t_divfreinfo;
            InitSignal();
        }

        public DivFreTreeItemViewModel(ItemTreeItemViewModel parent, T1_DivFreInfo t_divfreinfo, bool auto, T1_Organization organization, int sort_no) : this(parent, t_divfreinfo, auto)
        {
            T_Organization.Parent_Code = organization.Code;
            T_Organization.Parent_Guid = organization.Guid;
            T_Organization.Parent_Level = organization.Level;
            T_Organization.Sort_No = sort_no;
        }

        public DivFreTreeItemViewModel(string name, ItemTreeItemViewModel parent) : base(name, parent)
        {
            if (name == "")
            {
                T_Organization.Name = "新建分频" + (T_Organization.Sort_No + 1).ToString();
            }
            else
            {
                T_Organization.Name = name;
            }
            T_Organization.NodeType = 3;
            InitSignal();
        }

        public DivFreTreeItemViewModel(string name, int sort_no, ItemTreeItemViewModel parent) : base(name, sort_no, parent)
        {
            T_Organization.NodeType = 3;
            InitSignal();
        }

        public DivFreTreeItemViewModel(T1_Organization t_organization, ItemTreeItemViewModel parent) : base(t_organization.Name, t_organization.Sort_No, parent)
        {
            T_Organization = t_organization;
            T_Organization.NodeType = 3;
          
            InitSignal();
        }

        private void InitSignal()
        {
            DivFreSignal = new DivFreSignal(T_Organization.Guid, T_Organization.Name);           
        }

        public DivFreSignal DivFreSignal { get; set; }
    }
}

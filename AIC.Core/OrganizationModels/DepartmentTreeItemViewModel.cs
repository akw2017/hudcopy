using AIC.Core.ControlModels;
using AIC.Core.LMModels;
using AIC.CoreType;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AIC.Core.OrganizationModels
{
    public class DepartmentTreeItemViewModel : OrganizationTreeItemViewModel//组织机构树节点
    {
        public DepartmentTreeItemViewModel() : this("新建机构", 0, "") { }

        public DepartmentTreeItemViewModel(string name) : this(name, 0, "") { }

        public DepartmentTreeItemViewModel(int sort_no, string serverip) : this("", sort_no, serverip) { }      

        public DepartmentTreeItemViewModel(string name, int sort_no, string serverip)//添加根节点 
        {
            ServerIP = serverip;

            T_Organization = new T1_Organization();            
            if (name == "")
            {
                T_Organization.Name = "新建机构" + (sort_no + 1).ToString();
            }
            else
            {
                T_Organization.Name = name;
            }
            //T_Organization.Code;
            T_Organization.Guid = Guid.NewGuid();
            T_Organization.Level = 0;
            T_Organization.Sort_No = sort_no;
            T_Organization.Create_Time = DateTime.Now;
            T_Organization.Modify_Time = DateTime.Now;
            T_Organization.Is_Disabled = false;
            //T_Organization.Parent_Code
            //T_Organization.Parent_Guid
            T_Organization.Parent_Level = -1;
            //T_Organization.Remarks
            T_Organization.NodeType = 0;

            IsExpanded = true;
        }
        public DepartmentTreeItemViewModel(T1_Organization t_organziation, string serverip)//添加根节点 
        {
            ServerIP = serverip;

            T_Organization = t_organziation;           
            T_Organization.NodeType = 0;

            IsExpanded = true;
        }
        public DepartmentTreeItemViewModel(OrganizationTreeItemViewModel parent) : this("", parent) { }

        public DepartmentTreeItemViewModel(string name, OrganizationTreeItemViewModel parent) : this(name, -1, parent)//添加子节点
        {          
            int sort_no = parent.Children.Count;
            if (parent.Children.Count > 0)
            {
                sort_no = parent.Children[parent.Children.Count - 1].T_Organization.Sort_No + 1;
            }
            T_Organization.Sort_No = sort_no;
            if (name == "")
            {
                T_Organization.Name = "新建子机构" + (sort_no + 1).ToString();
            }
            else
            {
                T_Organization.Name = name;
            }         
        }

        public DepartmentTreeItemViewModel(string name, int sort_no, OrganizationTreeItemViewModel parent)//插入子节点
        {
            T_Organization = new T1_Organization();
            T_Organization.Name = name;
            //T_Organization.Code;
            T_Organization.Guid = Guid.NewGuid(); 
            T_Organization.Level = parent.T_Organization.Level + 1;
            //if (sort_no > 0)
            //{
            //    for (int i = 0; i < parent.Children.Count; i++)
            //    {
            //        if ((parent.Children[i] as OrganizationTreeItemViewModel).T_Organization.Sort_No >= sort_no)
            //        {
            //            (parent.Children[i] as OrganizationTreeItemViewModel).T_Organization.Sort_No++;
            //        }
            //    }
            //}
            T_Organization.Sort_No = sort_no;
            T_Organization.Create_Time = DateTime.Now;
            T_Organization.Modify_Time = DateTime.Now;
            T_Organization.Is_Disabled = false;
            T_Organization.Parent_Code = parent.T_Organization.Code;
            T_Organization.Parent_Guid = parent.T_Organization.Guid;
            T_Organization.Parent_Level = parent.T_Organization.Level;
            //T_Organization.Remarks
            T_Organization.NodeType = 0;

            IsExpanded = true;
        }

        public DepartmentTreeItemViewModel(T1_Organization t_organization, OrganizationTreeItemViewModel parent)//插入子节点
        {
            T_Organization = t_organization;
            T_Organization.NodeType = 0;

            IsExpanded = true;
        }
    }
}

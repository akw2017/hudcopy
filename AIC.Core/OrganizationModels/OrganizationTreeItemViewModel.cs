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
    public class OrganizationTreeItemViewModel : BindableBase//组织机构树节点
    {
        #region 基本属性 
        //private string name;
        public string Name
        {
            get { return T_Organization.Name; }
            set
            {                
                //name = value;
                T_Organization.Name = value;
                Names = GetFullNames(this);
                foreach(var child in Children)
                {
                    child.Name = child.Name;
                }
                OnPropertyChanged("Name");
            }
        }

        private string fullName;//完整名字
        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;                
            }
        }

        private string[] names;
        public string[] Names
        {
            get
            {
                if (names == null)
                {
                    return new string[] { Name };
                }
                else
                {
                    return names;
                }
            }
            set
            {
                names = value;
                FullName = string.Join("/", names.Reverse());
                OnPropertyChanged("Names");
            }
        }

        public string serverIP;
        public string ServerIP
        {
            get
            {
                if (Parent != null)
                {
                    return Parent.ServerIP;
                }
                else
                {
                    return serverIP;
                }
            }
            set
            {
                serverIP = value;
            }
        }
    

        public T1_Organization T_Organization { get; set; }

        private AlarmGrade alarm = AlarmGrade.HighNormal;
        public virtual AlarmGrade Alarm
        {
            get { return alarm; }
            set
            {
                if (alarm != value)
                {
                    alarm = value;
                    if (children.Count != 0)
                    {
                        var alarms = children.Select(o => o.Alarm);

                        if (alarms.Contains(AlarmGrade.HighDanger) || alarms.Contains(AlarmGrade.LowDanger))
                        {
                            alarm = AlarmGrade.HighDanger;
                        }
                        else if (alarms.Contains(AlarmGrade.HighAlarm) || alarms.Contains(AlarmGrade.LowAlarm))
                        {
                            alarm = AlarmGrade.HighAlarm;
                        }
                        else
                        {
                            alarm = AlarmGrade.HighNormal;
                        }
                    }
                    else
                    {
                        alarm = value;
                    }

                    if (Parent != null)
                    {
                        Parent.Alarm = alarm;
                    }

                    OnPropertyChanged("Alarm");
                }
            }
        }

        #endregion

        #region 附加属性
        private ObservableCollection<OrganizationTreeItemViewModel> children;
        public ObservableCollection<OrganizationTreeItemViewModel> Children
        {
            get
            {
                if (children == null)
                {
                    children = new ObservableCollection<OrganizationTreeItemViewModel>();
                    children.CollectionChanged += new NotifyCollectionChangedEventHandler(OnChildrenChanged);
                }

                return children;
            }
            //set
            //{
            //    children = value;
            //    OnPropertyChanged("Children");
            //}
        }

        private void OnChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Note: This section does not account for multiple items being involved in change operations.
            // Note: This section does not account for the replace operation.
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                OrganizationTreeItemViewModel child = (OrganizationTreeItemViewModel)e.NewItems[0];
                child.Parent = this;
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                OrganizationTreeItemViewModel child = (OrganizationTreeItemViewModel)e.OldItems[0];
                if (child.Parent == this)
                {
                    child.Parent = null;
                }
            }
        }

        public OrganizationTreeItemViewModel Parent { get; set; }        

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                if (isExpanded != value)
                {
                    isExpanded = value;
                    OnPropertyChanged("IsExpanded");
                }
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {               
                if (value != isChecked)
                {
                    isChecked = value;
                    OnPropertyChanged("IsChecked");

                    if (isChecked)
                    {
                        //如果选中则父项也应该选中
                        if (Parent != null)
                        {
                            Parent.IsChecked = true;
                        }
                    }
                    else
                    {
                        //如果取消选中子项也应该取消选中
                        foreach (OrganizationTreeItemViewModel child in Children)
                        {
                            child.IsChecked = false;
                        }
                    }
                }
            }
        }      

        /// <summary>
        /// 设置所有子项的选中状态
        /// </summary>
        /// <param name="isChecked"></param>
        public void SetChildrenChecked(bool isChecked)
        {
            foreach (OrganizationTreeItemViewModel child in Children)
            {
                child.IsChecked = IsChecked;
                child.SetChildrenChecked(IsChecked);
            }
        }

        /// <summary>
        /// 设置所有子项展开状态
        /// </summary>
        /// <param name="isExpanded"></param>
        public void SetChildrenExpanded(bool isExpanded)
        {
            foreach (OrganizationTreeItemViewModel child in Children)
            {
                child.IsExpanded = isExpanded;
                child.SetChildrenExpanded(isExpanded);
            }
        }


        public bool editMode;
        public bool EditMode
        {
            get { return editMode; }
            set
            {
                editMode = value;
                OnPropertyChanged("EditMode");
            }
        }
        public void InsertChild(int index, OrganizationTreeItemViewModel child)
        {
            if (!Children.Contains(child))
            {
                child.Parent = this;
                child.Names = GetFullNames(child);
                Children.Insert(index, child);
            }
        }

        public void AddChild(OrganizationTreeItemViewModel child)
        {
            if (!Children.Contains(child))
            {
                child.Parent = this;
                child.Names = GetFullNames(child);
                Children.Add(child);
            }
        }

        public void AddChildRange(IEnumerable<OrganizationTreeItemViewModel> childs)
        {
            foreach (var child in childs)
            {
                if (!Children.Contains(child))
                {
                    child.Parent = this;
                    child.Names = GetFullNames(child);
                    Children.Add(child);
                }
            }
        }

        public void RemoveChild(OrganizationTreeItemViewModel child)
        {
            if (Children.Contains(child))
            {                
                child.Parent = null;
                Children.Remove(child);
            }
        }

        public void ClearChild()
        {
            if (children != null)
            {
                children.Clear();
            }
        }
        #endregion       

        public OrganizationTreeItemViewModel() : this("新建机构", 0, "") { }

        public OrganizationTreeItemViewModel(string name) : this(name, 0, "") { }

        public OrganizationTreeItemViewModel(int sort_no, string serverip) : this("", sort_no, serverip) { }      

        public OrganizationTreeItemViewModel(string name, int sort_no, string serverip)//添加根节点 
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
        public OrganizationTreeItemViewModel(T1_Organization t_organziation, string serverip)//添加根节点 
        {
            ServerIP = serverip;

            T_Organization = t_organziation;           
            T_Organization.NodeType = 0;

            IsExpanded = true;
        }
        public OrganizationTreeItemViewModel(OrganizationTreeItemViewModel parent) : this("", parent) { }

        public OrganizationTreeItemViewModel(string name, OrganizationTreeItemViewModel parent) : this(name, -1, parent)//添加子节点
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

        public OrganizationTreeItemViewModel(string name, int sort_no, OrganizationTreeItemViewModel parent)//插入子节点
        {
            T_Organization = new T1_Organization();
            T_Organization.Name = name;
            //T_Organization.Code;
            T_Organization.Guid = Guid.NewGuid(); 
            T_Organization.Level = parent.T_Organization.Level + 1;
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

        public OrganizationTreeItemViewModel(T1_Organization t_organization, OrganizationTreeItemViewModel parent)//插入子节点
        {
            T_Organization = t_organization;
            T_Organization.NodeType = 0;

            IsExpanded = true;
        }

        private string[] GetFullNames(OrganizationTreeItemViewModel organization)
        {
            if (organization == null)
            {
                return null;
            }

            if (organization.Parent == null)
            {
                return new string[] { organization.Name };
            }

            List<string> allnames = new List<string>();

            allnames.Add(organization.Name);           
            var parent = organization.Parent;
            while (parent != null)
            {               
                allnames.Add(parent.Name);
                parent = parent.Parent;
            }
           
            return allnames.ToArray();
        }

    }
}

using AIC.Core.UserManageModels;
using AIC.M9600.Common.MasterDB.Generated;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{ 
    public class T1_User : T_User, IAnobject, INotifyPropertyChanged, Iid
    {
        //public int id { get; set; }//自增ID

        //private string name;
        public new string Name //用户登录名
        {
            get
            {
                return base.Name;
            }
            set
            {
                //name = value;
                base.Name = value;
                NotifyPropertyChange("Name");
            }
        }

        //private string code;
        public new string Code//用户代号
        {
            get
            {
                return base.Code;
            }
            set
            {
                //code = value;
                base.Code = value;
                NotifyPropertyChange("Code");
            }
        }

        //private string alias_Name;
        public new string Alias_Name//用户别名
        {
            get
            {
                return base.Alias_Name;
            }
            set
            {
                //alias_Name = value;
                base.Alias_Name = value;
                NotifyPropertyChange("Alias_Name");
            }
        }

        //private string password;
        public new string Password//用户密码
        {
            get
            {
                return base.Password;
            }
            set
            {
                //password = value;
                base.Password = value;
                NotifyPropertyChange("Password");
            }
        }

        //private string t_Role_Name;
        public new string T_Role_Name//所属角色职务名称
        {
            get
            {
                return base.T_Role_Name;
            }
            set
            {
                //t_Role_Name = value;
                base.T_Role_Name = value;
                NotifyPropertyChange("T_Role_Name");
            }
        }

        //public string T_Role_Code { get; set; }//所属角色职务Code

        //private Guid t_Role_Guid;
        public new Guid? T_Role_Guid//所属角色职务Guid
        {
            get
            {
                return base.T_Role_Guid;
            }
            set
            {
                //t_Role_Guid = value;
                base.T_Role_Guid = value;
                NotifyPropertyChange("T_Role_Guid");
            }
        }

        //private string t_Menu_Name;
        public new string T_Menu_Name//所属角色职务名称
        {
            get
            {
                return base.T_Menu_Name;
            }
            set
            {
                //t_Menu_Name = value;
                base.T_Menu_Name = value;
                NotifyPropertyChange("T_Menu_Name");
            }
        }

        //public string T_Menu_Code { get; set; }//所属角色职务Code

        //private Guid t_Menu_Guid;
        public new Guid? T_Menu_Guid//所属角色职务Guid
        {
            get
            {
                return base.T_Menu_Guid;
            }
            set
            {
                //t_Menu_Guid = value;
                base.T_Menu_Guid = value;
                NotifyPropertyChange("T_Menu_Guid");
            }
        }

        //private string t_OrganizationPrivilege_Name;
        public new string T_OrganizationPrivilege_Name//所属角色职务名称
        {
            get
            {
                return base.T_OrganizationPrivilege_Name;
            }
            set
            {
                //t_OrganizationPrivilege_Name = value;
                base.T_OrganizationPrivilege_Name = value;
                NotifyPropertyChange("T_OrganizationPrivilege_Name");
            }
        }

        //public string T_OrganizationPrivilege_Code { get; set; }//所属角色职务Code

        //private Guid t_OrganizationPrivilege_Guid;
        public new Guid? T_OrganizationPrivilege_Guid//所属角色职务Guid
        {
            get
            {
                return base.T_OrganizationPrivilege_Guid;
            }
            set
            {
                //t_OrganizationPrivilege_Guid = value;
                base.T_OrganizationPrivilege_Guid = value;
                NotifyPropertyChange("T_OrganizationPrivilege_Guid");
            }
        }

        //private string person_Telephone;
        public new string Person_Telephone//个人电话号码
        {
            get
            {
                return base.Person_Telephone;
            }
            set
            {
                //person_Telephone = value;
                base.Person_Telephone = value;
                NotifyPropertyChange("Person_Telephone");
            }
        }

        //private string office_Telephone;
        public new string Office_Telephone//办公室电话号码
        {
            get
            {
                return base.Office_Telephone;
            }
            set
            {
                //office_Telephone = value;
                base.Office_Telephone = value;
                NotifyPropertyChange("Office_Telephone");
            }
        }

        //private string email;
        public new string Email //电邮
        {
            get
            {
                return base.Email;
            }
            set
            {
                //email = value;
                base.Email = value;
                NotifyPropertyChange("Email");
            }
        }

        //public Guid Guid { get; set; }//GUID

        //private string contact;
        public new string Contact //地址等联系方式
        {
            get
            {
                return base.Contact;
            }
            set
            {
                //contact = value;
                base.Contact = value;
                NotifyPropertyChange("Contact");
            }
        }       

        //public bool Is_Disabled { get; set; }//禁止显示
        //public bool Is_Inside { get; set; }//是否可见  

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //浅拷贝
        public T1_User ShallowClone()
        {
            return this.Clone() as T1_User;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

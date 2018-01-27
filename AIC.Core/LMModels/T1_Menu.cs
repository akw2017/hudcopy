using AIC.Core.UserManageModels;
using AIC.M9600.Common.MasterDB.Generated;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{    
    public class T1_Menu : T_Menu, IAnobject, INotifyPropertyChanged, Iid
    {
        //public int id { get; set; }//自增ID

        //private string name;
        public new string Name//名称 
        {
            get
            {
                return base.Name;
            }
            set
            {                
                base.Name = value;
                NotifyPropertyChange("Name");//htzk123
            }
        }

        public string IconPath
        {
            get
            {
                return MenuManageList.GetIconPath(InternalNumber);
            }
        }

        //public Guid Guid { get; set; }//名称 
        //public string Code { get; set; }//代号 
        //public int Type { get; set; }//类型 
        //public int InternalNumber { get; set; }//编号
        //public string ShowText { get; set; }//显示文本  

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //浅拷贝
        public T1_Menu ShallowClone()
        {
            return this.Clone() as T1_Menu;
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

using AIC.Core.HardwareModels;
using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public class T1_Organization : T_Organization, Iid
    {
        //public int id { get; set; }//自增ID
        //public string Name { get; set; }//名称
        //public string Code { get; set; }//代号
        //public Guid Guid { get; set; }//Guid
        //public int Level { get; set; }//级别
        //public int Sort_No { get; set; }//排序号
        //public DateTime? Create_Time { get; set; }//创建时间
        //public DateTime? Modify_Time { get; set; }//修改时间
        //public bool Is_Disabled { get; set; }//禁止显示
        //public string Parent_Code { get; set; }//父代号
        //public Guid Parent_Guid { get; set; }//父Guid
        //public int Parent_Level { get; set; }//父级别
        //public string Remarks { get; set; }//备注   
        //public int NodeType { get; set; }//节点类型

        public void UpdateTemp(Organization organization)
        {
            SaveTempData();
            TempData.Name = organization.Name;
            TempData.Code = organization.Code;
            if (organization.Guid != null && organization.Guid != "")
            {
                TempData.Guid = new Guid(organization.Guid);
            }           
            TempData.Level = organization.Level;
            if (organization.Create_Time == "" || organization.Create_Time == DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss"))
            {
                TempData.Create_Time = null;
            }
            else
            {
                TempData.Create_Time = DateTime.ParseExact(organization.Create_Time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            }
            if (organization.Modify_Time == "" || organization.Modify_Time == DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss"))
            {
                TempData.Modify_Time = null;
            }
            else
            {
                TempData.Modify_Time = DateTime.ParseExact(organization.Modify_Time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            }
                     
            TempData.Parent_Code = organization.Parent_Code;
            if (organization.Parent_Guid != null && organization.Parent_Guid != "")
            {
                TempData.Parent_Guid = new Guid(organization.Parent_Guid);
            }            
            TempData.Parent_Level = organization.Parent_Level;                     
        }

        public T1_Organization TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_Organization();
            TempData.id = this.id;
            TempData.Name = this.Name;
            TempData.Code = this.Code;
            TempData.Guid = this.Guid;
            TempData.Level = this.Level;
            TempData.Sort_No = this.Sort_No;
            TempData.Create_Time = this.Create_Time;
            TempData.Modify_Time = this.Modify_Time;
            TempData.Is_Disabled = this.Is_Disabled;
            TempData.Parent_Code = this.Parent_Code;
            TempData.Parent_Guid = this.Parent_Guid;
            TempData.Parent_Level = this.Parent_Level;
            TempData.Remarks = this.Remarks;
            TempData.NodeType = this.NodeType;           
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.Name = TempData.Name;
            this.Code = TempData.Code;
            this.Guid = TempData.Guid;
            this.Level = TempData.Level;
            this.Sort_No = TempData.Sort_No;
            this.Create_Time = TempData.Create_Time;
            this.Modify_Time = TempData.Modify_Time;
            this.Is_Disabled = TempData.Is_Disabled;
            this.Parent_Code = TempData.Parent_Code;
            this.Parent_Guid = TempData.Parent_Guid;
            this.Parent_Level = TempData.Parent_Level;
            this.Remarks = TempData.Remarks;
            this.NodeType = TempData.NodeType;
            TempData = null;
        }

        public T1_Organization()
        {

        }

        public void UnBindTemp()
        {
            SaveTempData();
            TempData.Guid = Guid.NewGuid();
            TempData.Create_Time = DateTime.Now;
            TempData.Modify_Time = TempData.Create_Time;
        }

        public void SortTemp(int sort_no)
        {
            SaveTempData();
            TempData.Sort_No = sort_no;           
        }

        public void CopyTemp(T1_Organization parent)
        {
            SaveTempData();
            TempData.Guid = Guid.NewGuid();
            
            TempData.Create_Time = DateTime.Now;
            TempData.Modify_Time = TempData.Create_Time;
            //TempData.Sort_No = sort_no;
            if (parent != null)
            {
                TempData.Level = parent.Level + 1;
                TempData.Parent_Code = parent.Code;
                TempData.Parent_Guid = parent.Guid;
                TempData.Parent_Level = parent.Level;
            }
            else
            {
                TempData.Level = 0;
                TempData.Parent_Code = "";
                TempData.Parent_Guid = null;
                TempData.Parent_Level = -1;
            }
        }

        public void DeleteTemp()
        {
            TempData = null;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //浅拷贝
        public T1_Organization ShallowClone()
        {
            return this.Clone() as T1_Organization;
        }
    }
}

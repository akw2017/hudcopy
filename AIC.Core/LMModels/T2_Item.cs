using AIC.PDAPage.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public class T2_Item : T1_Item
    {
        public string[] Names { get; set; }
        public string ParentNamesString
        {
            get
            {
                if (Names != null && Names.Length > 1)
                {
                    string structure = string.Empty;
                    foreach (var name in Names.Take(Names.Length - 1))
                    {
                        structure = " -> " + name + structure;
                    }
                    return structure.Substring(4);
                }
                else
                {
                    return null;
                }
            }
        }
        public string Structure { get; set; }//结构
        public EditOperateType Operate { get; set; }//操作，增，删，改
        public string Hint { get; set; }//提示

    }
}

using AIC.CoreType;
using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public class T1_SystemEvent : T_SystemEvent
    {
        public CustomSystemDegree CustomSystemDegree
        {
            get
            {
                return (CustomSystemDegree)Degree;
            }
        }

        public CustomSystemType CustomSystemType
        {
            get
            {
                return (CustomSystemType)Type;
            }
        }
    }
}

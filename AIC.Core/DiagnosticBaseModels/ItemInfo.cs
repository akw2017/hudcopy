using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DiagnosticBaseModels
{
    [Serializable]
    public class ItemInfo
    {
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public ItemInfo(string name, Guid guid)
        {
            Name = name;
            Guid = guid;
        }
    }
}

using AIC.Core.OrganizationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.PDAPage.Models
{
    public class AddChannelClass
    {
        public ChannelTreeItemViewModel channeltree { get; set; }
        public DeviceTreeItemViewModel devicetree { get; set; }
        public ItemTreeItemViewModel itemtree { get; set; }
        public ItemTreeItemViewModel recycleditemtree { get; set; }
        public List<DivFreTreeItemViewModel> divtreelist { get; set; }
        public bool newnode { get; set; }
        public List<object> t_organizations { get; set; }
    }
}

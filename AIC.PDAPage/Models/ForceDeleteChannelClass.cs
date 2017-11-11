using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.PDAPage.Models
{
    public class ForceDeleteChannelClass
    {
        public ChannelTreeItemViewModel channeltree { get; set; }
        public List<DivFreInfo> divFreInfolist { get; set; }
        public ObservableCollection<DivFreInfo> divfreinfo { get; set; }
    }
}

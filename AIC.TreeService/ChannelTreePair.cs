using AIC.Domain;
using AIC.Server.Storage.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.TreeService
{
    public class ChannelTreePair
    {
        public Guid Id { get; set; }
        public TestPointTreeModel TestPointTM { get; set; }
        public ChannelTreeModel ChannelTM { get; set; }
    }
}

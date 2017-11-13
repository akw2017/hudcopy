using AIC.Core.Models;
using AIC.Core.SignalModels;
using Prism.Events;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Events
{
    public class CustomSystemEvent : PubSubEvent<CustomSystemException>
    {
    }
}

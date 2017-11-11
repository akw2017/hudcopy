using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CitizenSoftwareLib.Network.Socket
{
    internal class SyncController
    {
        public AutoResetEvent Event { get; private set; }
        public bool RaiseReceivedEvent { get; private set; }
        public SyncController(bool raiseReceivedEvent)
        {
            Event = new AutoResetEvent(false);
            this.RaiseReceivedEvent = raiseReceivedEvent;
        }

        public TCPMessage Message { get; set; }
    }
}

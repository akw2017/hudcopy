using AIC.AkkaSystem.Messages;
using AIC.Mathmatics;
using Akka.Actor;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace AIC.AkkaSystem.Actors
{
    public class ByteToSingleActor : ReceiveActor
    {

        private readonly HashSet<IActorRef> _linkList;

        public ByteToSingleActor()
        {
            _linkList = new HashSet<IActorRef>();
            Receive<VibrationMessage>(message => HandleBytes(message));
            Receive<Link>(message => HandleLink(message));

        }

        private void HandleLink(Link message)
        {
            if(!_linkList.Contains(message.Linker))
            {
                _linkList.Add(message.Linker);
            }
        }

        private void HandleBytes(VibrationMessage message)
        {
            //throw new ArgumentException("message is Null");
            var result = Algorithm.Instance.ByteToSingle(message.VDataBytes);
            var upodateMsg = message.UpdateVData(result);
            foreach(var linker in _linkList)
            {
                linker.Tell(upodateMsg);
            }
     
        }
    }
}

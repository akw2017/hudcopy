using AIC.AkkaSystem.Messages;

using AIC.Mathmatics;
using Akka.Actor;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using AIC.Domain;

namespace AIC.AkkaSystem.Actors
{
    public class VParmCalculateActor : ReceiveActor
    {
        private readonly HashSet<IActorRef> _subscriptions;
        private readonly HashSet<IActorRef> _linkList;
        public VParmCalculateActor()
        {
            _linkList = new HashSet<IActorRef>();
            _subscriptions = new HashSet<IActorRef>();
            NotWork();
        }

        private void Working()
        {
            Receive<VibrationMessage>(message => HandleVParmRequest(message));
            Receive<SubscribleProcessor>(message =>
            {
                if (!_subscriptions.Contains(message.Subscriber))
                {
                    _subscriptions.Add(message.Subscriber);
                }
            });
            Receive<UnSubscribleProcessor>(message =>
            {
                if (_subscriptions.Contains(message.Subscriber))
                {
                    _subscriptions.Remove(message.Subscriber);
                }
                if (_subscriptions.Count == 0)
                {
                    Become(NotWork);
                }
            });
            Receive<Link>(message => HandleLink(message));
            Receive<UnLink>(message => HandleUnLink(message));
        }
        private void NotWork()
        {
            Receive<VibrationMessage>(message => HandleNotWorkingVParmRequest(message));
            Receive<SubscribleProcessor>(message =>
            {
                if (!_subscriptions.Contains(message.Subscriber))
                {
                    _subscriptions.Add(message.Subscriber);
                }
                Become(Working);
            });
            Receive<Link>(message => HandleLink(message));
            Receive<UnLink>(message => HandleUnLink(message));
        }

        private void HandleLink(Link message)
        {
            if (!_linkList.Contains(message.Linker))
            {
                _linkList.Add(message.Linker);
            }
        }
        private void HandleUnLink(UnLink message)
        {
            if (_linkList.Contains(message.Linker))
            {
                _linkList.Remove(message.Linker);
            }
        }
        private void HandleVParmRequest(VibrationMessage message)
        {
            var para = Algorithm.Instance.CalculatePara(message.VData);
            var upodateMsg = message.UpdateVParm(new VibrationParm(para[0], para[1], para[2], para[3], para[4], para[5], para[6], para[7], para[8], para[9], para[10]));
            foreach (var sub in _subscriptions)
            {
                sub.Tell(upodateMsg);
            }
            foreach(var linker in _linkList)
            {
                linker.Tell(upodateMsg);
            }
        }
        private void HandleNotWorkingVParmRequest(VibrationMessage message)
        {
            foreach (var linker in _linkList)
            {
                linker.Tell(message);
            }
        }
    }
}

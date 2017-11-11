using AIC.AkkaSystem.Messages;
using AIC.Domain;
using AIC.ServiceInterface;
using Akka.Actor;
using System;
using System.Collections.Generic;

namespace AIC.AkkaSystem.Actors
{
    public class SignalCacheActor: ReceiveActor
    {
        #region Message
        public class AddSignal
        {
            public AddSignal(SignalBase signal)
            {
                Signal = signal;
            }

            public SignalBase Signal { get; }
        };

        public class RemoveSignal
        {
            public RemoveSignal(ChannelIdentity id)
            {
                Id = id;
            }
            public ChannelIdentity Id { get; }
        }
        #endregion

        private readonly ISignalCache _signalCache;
        private Dictionary<ChannelIdentity, IActorRef> signalActors;
        public SignalCacheActor(ISignalCache signalCache)
        {
            _signalCache = signalCache;

            signalActors = new Dictionary<ChannelIdentity, IActorRef>();

            foreach (var sg in  _signalCache.Signals)
            {
                IActorRef actor = CreateSignalActor(sg);

                if (actor != ActorRefs.Nobody)
                {
                    signalActors.Add(sg.ChannelId, actor);
                }
            }

            Receive<AddSignal>(message => HandleAddSignal(message));
            Receive<RemoveSignal>(message => HandleRemoveSignal(message));
            Receive<UpdatedRTData>(message => HandleUpdatedRTData(message));
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
           //maxNrOfRetries: 1,
           //withinTimeRange: TimeSpan.FromSeconds(10),
           localOnlyDecider: x =>
           {
               return Directive.Stop;
           });
        }

        private void HandleUpdatedRTData(UpdatedRTData message)
        {
            for (int i = 0; i < message.Data.Length; i++)
            {
                //var channelId = ChannelIdentity.Create(message.Data[i].IP, message.Data[i].SlotNum.Value, message.Data[i].ChaN.Value).Value;
                //if (signalActors.ContainsKey(channelId))
                //{
                //    signalActors[channelId].Tell(message.Data[i]);
                //}
            }
        }

        private void HandleAddSignal(AddSignal message)
        {
            if (signalActors.ContainsKey(message.Signal.ChannelId))
            {
                throw new ArgumentException($"信号已存在{message.Signal.ChannelId}");
            }

            IActorRef actor = CreateSignalActor(message.Signal);

            if (actor != ActorRefs.Nobody)
            {
                signalActors.Add(message.Signal.ChannelId, actor);
            }
        }

        private void HandleRemoveSignal(RemoveSignal message)
        {
            if (signalActors.ContainsKey(message.Id))
            {
                signalActors.Remove(message.Id);
            }
        }

        private IActorRef CreateSignalActor(SignalBase signal)
        {
            IActorRef actor = ActorRefs.Nobody;
            if(signal is AnalogInSignal)
            {
                var sg = signal as AnalogInSignal;
                actor = Context.ActorOf(Props.Create(() => new AnalogInSignalActor(sg)));
                sg.ActorRef = actor;
            }
            else if(signal is DigitTachometerSignal)
            {
                var sg = signal as DigitTachometerSignal;
                actor = Context.ActorOf(Props.Create(() => new DigitTachometerSignalActor(sg)));
                sg.ActorRef = actor;
            }
            else if (signal is EddyCurrentDisplacementSignal)
            {
                var sg = signal as EddyCurrentDisplacementSignal;
                actor = Context.ActorOf(Props.Create(() => new EddyCurrentDisplacementSignalActor(sg)));
                sg.ActorRef = actor;
            }
            else if (signal is EddyCurrentKeyPhaseSignal)
            {
                var sg = signal as EddyCurrentKeyPhaseSignal;
                actor = Context.ActorOf(Props.Create(() => new EddyCurrentKeyPhaseSignalActor(sg)));
                sg.ActorRef = actor;
            }
            else if (signal is EddyCurrentTachometerSignal)
            {
                var sg = signal as EddyCurrentTachometerSignal;
                actor = Context.ActorOf(Props.Create(() => new EddyCurrentTachometerSignalActor(sg)));
                sg.ActorRef = actor;
            }
            else if (signal is IEPESignal)
            {
                var sg = signal as IEPESignal;
                actor = Context.ActorOf(Props.Create(() => new IEPESignalActor(sg)));
                sg.ActorRef = actor;
            }
            else if (signal is RelaySignal)
            {
                var sg = signal as RelaySignal;
                actor = Context.ActorOf(Props.Create(() => new RelaySignalActor(sg)));
                sg.ActorRef = actor;
            }
            return actor;
        }
    }
}

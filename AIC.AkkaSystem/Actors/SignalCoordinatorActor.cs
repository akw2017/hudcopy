using AIC.AkkaSystem.Messages;

using Akka.Actor;
using Akka.DI.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIC.AkkaSystem.Actors
{
    public class SignalCoordinatorActor : ReceiveActor
    {

        private readonly IActorRef _signalCacheActor;
        private readonly IActorRef _rtDataAccessActor;
        private readonly IActorRef _treesActor;
        public SignalCoordinatorActor()
        {
           // _signalCacheActor = Context.ActorOf(Context.DI().Props<SignalCacheActor>(), ActorPaths.SignalCacheActor.Name);
          
            _signalCacheActor = Context.ActorOf(Context.DI().Props<SignalCacheActor>(), ActorPaths.SignalCacheActor.Name);
            _rtDataAccessActor = Context.ActorOf(Context.DI().Props<RTDataAccessActor>());
            _treesActor = Context.ActorOf(Context.DI().Props<TreesActor>(), ActorPaths.TreesActor.Name);

            Receive<RefreshRTData>(message => _rtDataAccessActor.Tell(message));
            Receive<UpdatedRTData>(message => _signalCacheActor.Tell(message));
            //Receive<SubscribleSignal>(message => signalCacheActor.Tell(message));
            //Receive<UnSubscribleSignal>(message => signalCacheActor.Tell(message));
        }

        protected override void PreStart()
        {
            //Context.System.Scheduler.
            //    ScheduleTellRepeatedlyCancelable(
            //    TimeSpan.FromMilliseconds(100),
            //    TimeSpan.FromMilliseconds(1800),
            //    Self,
            //    RefreshRTData.Instance, Self);
        }
    }
}

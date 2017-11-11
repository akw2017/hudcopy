using Akka.Actor;

namespace AIC.AkkaSystem.Messages
{
    public class SubscribleProcessor
    {
        public SubscribleProcessor(IActorRef subscriber)
        {
            Subscriber = subscriber;
        }

        public IActorRef Subscriber { get; }
    }
}

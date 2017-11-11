using Akka.Actor;

namespace AIC.AkkaSystem.Messages
{
    public class UnSubscribleProcessor
    {
        public UnSubscribleProcessor(IActorRef subscriber)
        {
            Subscriber = subscriber;
        }

        public IActorRef Subscriber { get; }
    }
}

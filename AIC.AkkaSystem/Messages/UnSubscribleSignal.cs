using Akka.Actor;

namespace AIC.AkkaSystem.Messages
{
    public class UnSubscribleSignal
    {
        public UnSubscribleSignal(ProcessType processType, IActorRef subscriber)
        {
            ProcessType = processType;
            Subscriber = subscriber;
        }

        public ProcessType ProcessType { get; }
        public IActorRef Subscriber { get; }
    }
}

using Akka.Actor;

namespace AIC.AkkaSystem.Messages
{
    public class UnLink
    {
        public UnLink(IActorRef linker)
        {
            Linker = linker;
        }

        public IActorRef Linker { get; }
    }
}

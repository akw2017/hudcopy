using Akka.Actor;

namespace AIC.AkkaSystem.Messages
{
    public class Link
    {
        public Link(IActorRef linker)
        {
            Linker = linker;
        }

        public IActorRef Linker { get; }
    }
}

using Akka.Actor;
using Akka.DI.Unity;
using Microsoft.Practices.Unity;

namespace AIC.AkkaSystem
{
    public static class ActorSystemReference
    {
        public static ActorSystem ActorSystem { get; private set; }

        static ActorSystemReference()
        {
           
        }


        internal static void CreateActorSystem(IUnityContainer unitContainer)
        {
            if (unitContainer == null) return;
            if (ActorSystem != null) return;

            ActorSystem = ActorSystem.Create("AICActorSystem");
            var resolver = new UnityDependencyResolver(unitContainer, ActorSystem);
        }
    }
}

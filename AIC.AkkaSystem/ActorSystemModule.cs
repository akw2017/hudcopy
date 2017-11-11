using AIC.AkkaSystem.Actors;
using AIC.Core;
using Akka.Actor;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using System;

namespace AIC.AkkaSystem
{
    [Roles("Admin")]
    public class ActorSystemModule : IModule
    {
        private readonly IUnityContainer _container;
        public ActorSystemModule(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
        }

        public void Initialize()
        {
            ActorSystemReference.CreateActorSystem(_container);

            ActorSystemReference.ActorSystem.ActorOf(Props.Create(() => new SignalCoordinatorActor()), ActorPaths.SignalCoordinatorActor.Name);
        }
    }
}

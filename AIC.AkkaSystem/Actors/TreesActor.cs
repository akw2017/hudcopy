using AIC.AkkaSystem.Messages;
using AIC.Core;
using AIC.ServiceInterface;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.AkkaSystem.Actors
{
    public class TreesActor : ReceiveActor
    {
        #region Message
        public class NameConflict
        {
            public NameConflict(AdvancedPropertyChangedEventArgs arguments)
            {
                Arguments = arguments;
            }
            public string NewValue { get; }
            public AdvancedPropertyChangedEventArgs Arguments { get; }
        }
        public class SubscribeNameConflict
        {  
            public SubscribeNameConflict(IActorRef subscriber)
            {
                Subscriber = subscriber;
       
            }
            public IActorRef Subscriber { get; }
        }
        #endregion
        private readonly ITreeService _treeService;
        private HashSet<IActorRef> nameConflictSubscribes;
        public TreesActor(ITreeService treeService)
        {
            _treeService = treeService;
           // _treeService.NamePropertyConflicted += NamePropertyConflicted;
            nameConflictSubscribes = new HashSet<IActorRef>();

            Receive<SubscribeNameConflict>(message => HandleSubscribeNameConflict(message));
        }

        private void NamePropertyConflicted(AdvancedPropertyChangedEventArgs e)
        {
            var message = new NameConflict(e);
            foreach (var subscriber in nameConflictSubscribes)
            {
                subscriber.Tell(message);
            }
        }
        private void HandleSubscribeNameConflict(SubscribeNameConflict message)
        {
            if(!nameConflictSubscribes.Contains(message.Subscriber))
            {
                nameConflictSubscribes.Add(message.Subscriber);
            }
        }
    }
}

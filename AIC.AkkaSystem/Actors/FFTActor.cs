using AIC.AkkaSystem.Messages;
using AIC.Mathmatics;
using Akka.Actor;
using System.Collections.Generic;
using System.Linq;

namespace AIC.AkkaSystem.Actors
{
    public class FFTActor : ReceiveActor
    {
        private readonly HashSet<IActorRef> _subscriptions;
        private readonly HashSet<IActorRef> _linkList;
        public FFTActor()
        {
            _subscriptions = new HashSet<IActorRef>();
            _linkList = new HashSet<IActorRef>();
            NotWork();
        }

        private void Working()
        {
            Receive<VibrationMessage>(message => HandleVibrationMessage(message));
            Receive<SubscribleProcessor>(message =>
            {
                if (!_subscriptions.Contains(message.Subscriber))
                {
                    _subscriptions.Add(message.Subscriber);
                }
            });
            Receive<UnSubscribleProcessor>(message =>
            {
                if (_subscriptions.Contains(message.Subscriber))
                {
                    _subscriptions.Remove(message.Subscriber);
                }
                if (_subscriptions.Count == 0)
                {
                    Become(NotWork);
                }
            });
            Receive<Link>(message => HandleLink(message));
            Receive<UnLink>(message => HandleUnLink(message));
        }

        private void NotWork()
        {
            Receive<VibrationMessage>(message => HandleNotWorkingVibrationMessage(message));
            Receive<SubscribleProcessor>(message =>
            {
                if (!_subscriptions.Contains(message.Subscriber))
                {
                    _subscriptions.Add(message.Subscriber);
                }
                Become(Working);
            });
            Receive<Link>(message => HandleLink(message));
            Receive<UnLink>(message => HandleUnLink(message));
        }

        private void HandleLink(Link message)
        {
            if (!_linkList.Contains(message.Linker))
            {
                _linkList.Add(message.Linker);
            }
        }
        private void HandleUnLink(UnLink message)
        {
            if (_linkList.Contains(message.Linker))
            {
                _linkList.Remove(message.Linker);
            }
        }
        private void HandleVibrationMessage(VibrationMessage message)
        {
            double frequencyInterval = message.SampleFre / message.SamplePoint;
            int length = (int)(message.SamplePoint / 2.56) + 1;

            double[] frequency = new double[length];
            //if (message.Frequency==null || message.Frequency.Length!=length)
            //{
            //    message.Frequency = new double[length];
            //    message.Amplitude = new double[length];
            //    message.Phase = new double[length];
            //}
            for (int i = 0; i < length; i++)
            {
                frequency[i] = frequencyInterval * i;
            }

            var output = Algorithm.Instance.FFT2AndPhaseAction(message.VData, message.SamplePoint);
            var updateMsg = message.UpdateFFT(frequency, output[0].Take(length).ToArray(), output[1].Take(length).ToArray());

            foreach (var sub in _subscriptions)
            {
                sub.Tell(updateMsg);
            }
            foreach (var linker in _linkList)
            {
                linker.Tell(updateMsg);
            }
        }
        private void HandleNotWorkingVibrationMessage(VibrationMessage message)
        {
            foreach (var sub in _subscriptions)
            {
                sub.Tell(message);
            }
            foreach (var linker in _linkList)
            {
                linker.Tell(message);
            }
        }
    }
}

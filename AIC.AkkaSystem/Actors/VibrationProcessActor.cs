using AIC.AkkaSystem.Messages;

using AIC.CoreType;
using AIC.Domain;
using AIC.Mathmatics;
using Akka.Actor;
using System.Collections.Generic;

namespace AIC.AkkaSystem.Actors
{
    public class VibrationProcessActor: ReceiveActor
    {
        #region MessageType

        #endregion

        private readonly HashSet<IActorRef> _linkList;
        public VibrationProcessActor()
        {
            _linkList = new HashSet<IActorRef>();
            ProcessNone();
        }
        private void ProcessNone()
        {
            Receive<VibrationMessage>(message => HandleNone(message));
            Receive<ProcessTypeChange>(message => HandleProcessTypeChange(message));
            Receive<Link>(message => HandleLink(message));
            Receive<UnLink>(message => HandleUnLink(message));
        }
        private void ProcessEnvelope()
        {
            Receive<VibrationMessage>(message => HandleEnvelope(message));
            Receive<ProcessTypeChange>(message => HandleProcessTypeChange(message));
            Receive<Link>(message => HandleLink(message));
            Receive<UnLink>(message => HandleUnLink(message));
        }
        private void ProcessTFF()
        {
            Receive<VibrationMessage>(message => HandleTFF(message));
            Receive<ProcessTypeChange>(message => HandleProcessTypeChange(message));
            Receive<Link>(message => HandleLink(message));
            Receive<UnLink>(message => HandleUnLink(message));
        }
        private void ProcessCepstrum()
        {
            Receive<VibrationMessage>(message => HandleCepstrum(message));
            Receive<ProcessTypeChange>(message => HandleProcessTypeChange(message));
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
        private void HandleProcessTypeChange(ProcessTypeChange message)
        {
            switch (message.ProcessType)
            {
                case VibrationProcessType.None:
                    Become(ProcessNone);
                    break;
                case VibrationProcessType.Envelope:
                    Become(ProcessEnvelope);
                    break;
                case VibrationProcessType.TFF:
                    Become(ProcessTFF);
                    break;
                case VibrationProcessType.Cepstrum:
                    Become(ProcessCepstrum);
                    break;
            }
        }
        private void HandleNone(VibrationMessage message)
        {
            foreach(var sub in _linkList)
            {
                sub.Tell(message);
            }
        }
        private void HandleTFF(VibrationMessage message)
        {
            var result = Algorithm.Instance.TFF(message.VData, message.SamplePoint, message.SampleFre);
            var upodateMsg = message.UpdateVData(result);
            foreach (var sub in _linkList)
            {
                sub.Tell(upodateMsg);
            }
        }
        private void HandleEnvelope(VibrationMessage message)
        {
            var result = Algorithm.Instance.Envelope(message.VData, message.SamplePoint);
            var upodateMsg = message.UpdateVData(result);
            foreach (var sub in _linkList)
            {
                sub.Tell(upodateMsg);
            }
        }
        private void HandleCepstrum(VibrationMessage message)
        {
            var result = Algorithm.Instance.Cepstrum(message.VData, message.SamplePoint);
            var upodateMsg = message.UpdateVData(result);
            foreach (var sub in _linkList)
            {
                sub.Tell(upodateMsg);
            }
        }

    }
}

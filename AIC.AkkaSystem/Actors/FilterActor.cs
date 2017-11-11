using AIC.AkkaSystem.Messages;

using AIC.CoreType;
using AIC.Domain;
using Akka.Actor;
using System.Collections.Generic;

namespace AIC.AkkaSystem.Actors
{
    public class FilterActor : ReceiveActor
    {
        #region Message

        #endregion
        private BandPassFilter bpFilter;
        private HighPassFilter hpFilter;
        private LowPassFilter lpFilter;
        private readonly HashSet<IActorRef> _linkList;
        public FilterActor()
        {
            _linkList = new HashSet<IActorRef>();
            ProcessNone();
        }

        private void ProcessNone()
        {
            Receive<VibrationMessage>(message => HandleNone(message));
            Receive<FilterChange>(message => HandleFilterTypeChange(message));
            Receive<Link>(message => HandleLink(message));
            Receive<UnLink>(message => HandleUnLink(message));
        }
        private void ProcessHighPass()
        {
            Receive<VibrationMessage>(message => HandleHighPass(message));
            Receive<FilterChange>(message => HandleFilterTypeChange(message));
            Receive<Link>(message => HandleLink(message));
            Receive<UnLink>(message => HandleUnLink(message));
        }
        private void ProcessLowPass()
        {
            Receive<VibrationMessage>(message => HandleLowPass(message));
            Receive<FilterChange>(message => HandleFilterTypeChange(message));
            Receive<Link>(message => HandleLink(message));
            Receive<UnLink>(message => HandleUnLink(message));
        }
        private void ProcessBandPass()
        {
            Receive<VibrationMessage>(message => HandleBandPass(message));
            Receive<FilterChange>(message => HandleFilterTypeChange(message));
            Receive<Link>(message => HandleLink(message));
            Receive<UnLink>(message => HandleUnLink(message));
        }
        private void HandleFilterTypeChange(FilterChange message)
        {
            bpFilter = message.BPFilter;
            hpFilter = message.HPFilter;
            lpFilter = message.LPFilter;
            switch (message.FilterType)
            {
                case FilterType.None:
                    Become(ProcessNone);
                    break;
                case FilterType.HighPass:
                    Become(ProcessHighPass);
                    break;
                case FilterType.LowPass:
                    Become(ProcessLowPass);
                    break;
                case FilterType.BandPass:
                    Become(ProcessBandPass);
                    break;
            }
        }
        private void HandleNone(VibrationMessage message)
        {
            foreach (var sub in _linkList)
            {
                sub.Tell(message);
            }
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
        private void HandleHighPass(VibrationMessage message)
        {
            var result = hpFilter.Filter(message.VData, message.SamplePoint, message.SampleFre);
            var upodateMsg = message.UpdateVData(result);
            foreach (var sub in _linkList)
            {
                sub.Tell(upodateMsg);
            }
        }
        private void HandleLowPass(VibrationMessage message)
        {
            var result = lpFilter.Filter(message.VData, message.SamplePoint, message.SampleFre);
            var upodateMsg = message.UpdateVData(result);
            foreach (var sub in _linkList)
            {
                sub.Tell(upodateMsg);
            }
        }
        private void HandleBandPass(VibrationMessage message)
        {
            var result = bpFilter.Filter(message.VData, message.SamplePoint, message.SampleFre);
            var upodateMsg = message.UpdateVData(result);
            foreach (var sub in _linkList)
            {
                sub.Tell(upodateMsg);
            }
        }
    }
}

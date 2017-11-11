using AIC.Domain;

using Akka.Actor;
using System;

namespace AIC.AkkaSystem.Actors
{
    public class RelaySignalActor : ReceiveActor
    {
        private readonly RelaySignal _signal;
        public RelaySignalActor(RelaySignal signal)
        {
            _signal = signal;
            Context.SetReceiveTimeout(TimeSpan.FromSeconds(30));
            Receive<RelayChannelModel>(message => HandleDataContract(message));
            Receive<ReceiveTimeout>(message => HandleTimeOut(message));
        }

        private void HandleTimeOut(ReceiveTimeout message)
        {
            _signal.IsConnected = false;
        }

        private void HandleDataContract(RelayChannelModel channel)
        {
            _signal.Expression = channel.Expression;
            _signal.IsBypass = channel.IsBypass;
            _signal.IsUpload = channel.IsUpload;
            _signal.IsConnected = true;
        }
    }
}

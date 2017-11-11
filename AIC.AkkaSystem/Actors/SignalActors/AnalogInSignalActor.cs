using AIC.Domain;

using Akka.Actor;
using System;

namespace AIC.AkkaSystem.Actors
{
    public class AnalogInSignalActor : ReceiveActor
    {
        private readonly AnalogInSignal _signal;
        public AnalogInSignalActor(AnalogInSignal signal)
        {
            _signal = signal;
            Context.SetReceiveTimeout(TimeSpan.FromSeconds(30));
            Receive<AnalogInChannelModel>(message => HandleDataContract(message));
            Receive<ReceiveTimeout>(message => HandleTimeOut(message));
        }

        private void HandleTimeOut(ReceiveTimeout message)
        {
            _signal.IsConnected = false;
        }

        private void HandleDataContract(AnalogInChannelModel channel)
        {
            _signal.Calibration = channel.Calibration;
            _signal.X0 = channel.X0;
            _signal.Y0 = channel.Y0;
            _signal.X1 = channel.X1;
            _signal.Y1 = channel.Y1;
            _signal.IsEnableFormula = channel.IsEnableFormula;
            _signal.Unit = channel.Unit;
            _signal.IsBypass = channel.IsBypass;
            _signal.IsUpload = channel.IsUpload;
            _signal.IsConnected = true;
        }
    }
}

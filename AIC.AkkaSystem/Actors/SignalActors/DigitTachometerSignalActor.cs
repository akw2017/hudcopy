using AIC.Domain;

using Akka.Actor;
using System;

namespace AIC.AkkaSystem.Actors
{
    public class DigitTachometerSignalActor: ReceiveActor
    {
        private readonly DigitTachometerSignal _signal;
        public DigitTachometerSignalActor(DigitTachometerSignal signal)
        {
            _signal = signal;
            Context.SetReceiveTimeout(TimeSpan.FromSeconds(30));
            Receive<DigitTachometerChannelModel>(message => HandleDataContract(message));
            Receive<ReceiveTimeout>(message => HandleTimeOut(message));
        }

        private void HandleTimeOut(ReceiveTimeout message)
        {
            _signal.IsConnected = false;
        }

        private void HandleDataContract(DigitTachometerChannelModel channel)
        {
            _signal.Calibration = channel.Calibration;
            _signal.IsNotch = channel.IsNotch;
            _signal.DefaultRPM = channel.DefaultRPM;
            _signal.TeethNumber = channel.TeethNumber;
            _signal.IsTwoMAClamp = channel.IsTwoMAClamp;
            _signal.Unit = channel.Unit;
            _signal.IsBypass = channel.IsBypass;
            _signal.IsUpload = channel.IsUpload;
            _signal.IsConnected = true;
        }
    }
}

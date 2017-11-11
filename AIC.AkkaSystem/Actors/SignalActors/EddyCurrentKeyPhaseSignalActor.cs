using AIC.Domain;

using Akka.Actor;
using System;

namespace AIC.AkkaSystem.Actors
{
    public class EddyCurrentKeyPhaseSignalActor : ReceiveActor
    {
        private readonly EddyCurrentKeyPhaseSignal _signal;
        public EddyCurrentKeyPhaseSignalActor(EddyCurrentKeyPhaseSignal signal)
        {
            _signal = signal;
            Context.SetReceiveTimeout(TimeSpan.FromSeconds(30));
            Receive<EddyCurrentKeyPhaseChannelModel>(message => HandleDataContract(message));
            Receive<ReceiveTimeout>(message => HandleTimeOut(message));
        }

        private void HandleTimeOut(ReceiveTimeout message)
        {
            _signal.IsConnected = false;
        }

        private void HandleDataContract(EddyCurrentKeyPhaseChannelModel channel)
        {
            _signal.BiasVoltHigh = channel.BiasVoltHigh;
            _signal.BiasVoltLow = channel.BiasVoltLow;
            _signal.ThresholdVolt = channel.ThresholdVolt;
            _signal.HysteresisVolt = channel.HysteresisVolt;
           // _signal.ThresholdMode = channel.ThresholdMode;
            _signal.Calibration = channel.Calibration;
            _signal.IsNotch = channel.IsNotch;
            _signal.DefaultRPM = channel.DefaultRPM;
            _signal.TeethNumber = channel.TeethNumber;
            _signal.IsTwoMAClamp = channel.IsTwoMAClamp;
            _signal.DelayAlarmTime = channel.DelayAlarmTime;
            _signal.Unit = channel.Unit;
            _signal.IsBypass = channel.IsBypass;
            _signal.IsUpload = channel.IsUpload;
            _signal.IsConnected = true;
        }
    }
}

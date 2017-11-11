using AIC.Domain;

using Akka.Actor;
using System;

namespace AIC.AkkaSystem.Actors
{
    public class EddyCurrentDisplacementSignalActor : ReceiveActor
    {
        private readonly EddyCurrentDisplacementSignal _signal;
        public EddyCurrentDisplacementSignalActor(EddyCurrentDisplacementSignal signal)
        {
            _signal = signal;
            Context.SetReceiveTimeout(TimeSpan.FromSeconds(30));
            Receive<EddyCurrentDisplacementChannelModel>(message => HandleDataContract(message));
            Receive<ReceiveTimeout>(message => HandleTimeOut(message));
        }

        private void HandleTimeOut(ReceiveTimeout message)
        {
            _signal.IsConnected = false;
        }

        private void HandleDataContract(EddyCurrentDisplacementChannelModel channel)
        {
            //_signal.TPDir = channel.TPDir;
            //_signal.ResultType = channel.ResultType;
            //_signal.ResultCode = channel.ResultCode;
            _signal.BiasVoltHigh = channel.BiasVoltHigh;
            _signal.BiasVoltLow = channel.BiasVoltLow;
            _signal.Sensitivity = channel.Sensitivity;
            _signal.IsTwoMAClamp = channel.IsTwoMAClamp;
            _signal.RPMCardNum = channel.RPMCardNum;
            _signal.RPMChannelNum = channel.RPMChannelNum;
            _signal.DelayAlarmTime = channel.DelayAlarmTime;
            _signal.Unit = channel.Unit;
            _signal.IsBypass = channel.IsBypass;
            _signal.IsUpload = channel.IsUpload;
            _signal.IsConnected = true;
        }
    }
}

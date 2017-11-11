using AIC.Domain;

using Akka.Actor;
using System;

namespace AIC.AkkaSystem.Actors
{
    public class IEPESignalActor : ReceiveActor
    {
        private readonly IEPESignal _signal;
        public IEPESignalActor(IEPESignal signal)
        {
            _signal = signal;
            Context.SetReceiveTimeout(TimeSpan.FromSeconds(30));
            Receive<IEPEChannelModel>(message => HandleDataContract(message));
            Receive<ReceiveTimeout>(message => HandleTimeOut(message));
        }

        private void HandleTimeOut(ReceiveTimeout message)
        {
            _signal.IsConnected = false;
        }

        private void HandleDataContract(IEPEChannelModel channel)
        {
           // _signal.TPDir = channel.TPDir;
           // _signal.SVType = channel.SVType;
            _signal.BiasVoltHigh = channel.BiasVoltHigh;
            _signal.BiasVoltLow = channel.BiasVoltLow;
            _signal.Sensitivity = channel.Sensitivity;
            _signal.VelocityCalibration = channel.VelocityCalibration;
            _signal.DisplacementCalibration = channel.DisplacementCalibration;
            _signal.IsTwoMAClamp = channel.IsTwoMAClamp;
            _signal.RPMCardNum = channel.RPMCardNum;
            _signal.RPMChannelNum = channel.RPMChannelNum;
            _signal.Unit = channel.Unit;
            _signal.DelayAlarmTime = channel.DelayAlarmTime;
            _signal.Unit = channel.Unit;
            _signal.IsBypass = channel.IsBypass;
            _signal.IsUpload = channel.IsUpload;
            _signal.IsConnected = true;
        }
    }
}

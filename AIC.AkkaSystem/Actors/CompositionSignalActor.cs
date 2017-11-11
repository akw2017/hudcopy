using AIC.CoreType;
using AIC.Domain;

using Akka.Actor;
using System;

namespace AIC.AkkaSystem.Actors
{
    public class CompositionSignalActor : ReceiveActor
    {
        private readonly CompositionSignal sg;
        public CompositionSignalActor(CompositionSignal signal)
        {
            sg = signal;
            Context.SetReceiveTimeout(TimeSpan.FromSeconds(30)); 
            Receive<ChannelDataContract>(message => HandleDataContract(message));
            Receive<ReceiveTimeout>(message => HandleTimeOut(message));
        }

        private void HandleTimeOut(ReceiveTimeout message)
        {
            sg.IsConnected = false;
        }

        private void HandleDataContract(ChannelDataContract dataContract)
        {
            sg.IsResetFlag = true;
            sg.RecordLab = dataContract.RecordLab;
            sg.SaveLab = dataContract.SaveLab;
            sg.GroupCOName = dataContract.GroupCOName;
            sg.CorporationName = dataContract.CorporationName;
            sg.WorkShopName = dataContract.WorkShopName;
            sg.T_Device_Name = dataContract.DevName;
            sg.DevSN = dataContract.DevSN;
            sg.Name = dataContract.CHName;
            sg.MSSN = dataContract.CHMSSN;
            sg.LowDanger = dataContract.LowDanger;
            sg.LowAlert = dataContract.LowAlert;
            sg.LowNormal = dataContract.LowNormal;
            sg.HighNormal = dataContract.HighNormal;
            sg.HighAlert = dataContract.HighAlert;
            sg.HighDanger = dataContract.HighDanger;
            sg.STIME = dataContract.STIME;
            sg.AMS = dataContract.AMS;
            sg.AlarmType = (int)dataContract.AlarmType;
            sg.AlarmGrade = (AlarmGrade)Enum.Parse(typeof(AlarmGrade), dataContract.AlarmGrade.ToString());
            sg.IP = dataContract.IP;
            sg.Unit = (Unit)Enum.Parse(typeof(Unit), dataContract.Unit.ToString());
            if (dataContract.SlotNum != null) 
            { 
                sg.SlotNum = dataContract.SlotNum.Value;
            }
            if (dataContract.ChaN != null)
            {
                sg.ChaN = dataContract.ChaN.Value;
            }
            sg.IsConnected = true;
        }


    }
}

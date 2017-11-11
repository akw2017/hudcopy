using AIC.AkkaSystem.Messages;

using AIC.CoreType;
using AIC.Domain;

using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.AkkaSystem.Actors
{
    public class VibrationSignalActor: ReceiveActor
    {
        #region Messages
        public class ProcessState { }
        public class NotProcessState { }
        #endregion

        private IActorRef _fftActor;
        private IActorRef _vParmCalculateActor;
        private IActorRef _vibrationProcessActor;
        private IActorRef _filterActor;
        private IActorRef _byteToSingleActor;

        private bool hasProcessed;

        private readonly HashSet<IActorRef> _linkList;
        private readonly HashSet<IActorRef> _subscriptions;
        private readonly VibrationSignal sg;

        public VibrationSignalActor(VibrationSignal signal)
        {
            sg = signal;
            _subscriptions = new HashSet<IActorRef>();
            _linkList = new HashSet<IActorRef>();  
            Context.SetReceiveTimeout(TimeSpan.FromSeconds(3));

            InitializeActor();

            NotProcess();
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {      
           return new OneForOneStrategy(
           //maxNrOfRetries: 1,
           //withinTimeRange: TimeSpan.FromSeconds(10),
           localOnlyDecider: x =>
           {
               hasProcessed = true;
               return Directive.Resume;
           });
        }

        private void InitializeActor()
        {
            _fftActor = Context.ActorOf(Props.Create(() => new FFTActor()));
            _fftActor.Tell(new Link(Self));

            _vParmCalculateActor = Context.ActorOf(Props.Create(() => new VParmCalculateActor()));
            _vParmCalculateActor.Tell(new Link(_fftActor));

            _vibrationProcessActor = Context.ActorOf(Props.Create(() => new VibrationProcessActor()));
            _vibrationProcessActor.Tell(new Link(_vParmCalculateActor));

            _filterActor = Context.ActorOf(Props.Create(() => new FilterActor()));
            _filterActor.Tell(new Link(_vibrationProcessActor));

            _byteToSingleActor = Context.ActorOf(Props.Create(() => new ByteToSingleActor()));
            _byteToSingleActor.Tell(new Link(_filterActor));
        }

        private void Process()
        {
            Receive<ChannelDataContract>(message =>
            {
                if (hasProcessed)
                {
                    hasProcessed = false;
                    _byteToSingleActor.Tell(new VibrationMessage(message));
                }
            });
            Receive<ReceiveTimeout>(message => HandleTimeOut(message));
            Receive<SubscribleSignal>(message =>
            {
                if (!_subscriptions.Contains(message.Subscriber))
                {
                    _subscriptions.Add(message.Subscriber);
                }

                switch (message.ProcessType)
                {
                    case ProcessType.Time:
                        _vParmCalculateActor.Tell(new SubscribleProcessor(message.Subscriber));
                        break;
                    case ProcessType.Frequency:
                        _fftActor.Tell(new SubscribleProcessor(message.Subscriber));
                        break;
                    default:
                        break;
                }
            });
            Receive<UnSubscribleSignal>(message =>
            {
                if (_subscriptions.Contains(message.Subscriber))
                {
                    _subscriptions.Remove(message.Subscriber);
                }

                switch (message.ProcessType)
                {
                    case ProcessType.Time:
                        _vParmCalculateActor.Tell(new UnSubscribleProcessor(message.Subscriber));
                        break;
                    case ProcessType.Frequency:
                        _fftActor.Tell(new UnSubscribleProcessor(message.Subscriber));
                        break;
                    default:
                        break;
                }

                if (_subscriptions.Count == 0)
                {
                    Become(NotProcess);
                }
            });
            Receive<VibrationMessage>(message => HandleVibrationMessage(message));
            Receive<ProcessTypeChange>(message => _vibrationProcessActor.Forward(message));
            Receive<FilterChange>(message =>  _filterActor.Forward(message));
            Receive<Link>(message => HandleLink(message));
            Receive<UnLink>(message => HandleUnLink(message));
        }

        private void NotProcess()
        {
            Receive<ChannelDataContract>(message =>
            {
                Self.Tell(new VibrationMessage(message));
            });
            Receive<ReceiveTimeout>(message => HandleTimeOut(message));
            Receive<SubscribleSignal>(message =>
            {
                if (!_subscriptions.Contains(message.Subscriber))
                {
                    _subscriptions.Add(message.Subscriber);
                }

                switch (message.ProcessType)
                {
                    case ProcessType.Time:
                        _vParmCalculateActor.Tell(new SubscribleProcessor(message.Subscriber));
                        break;
                    case ProcessType.Frequency:
                        _fftActor.Tell(new SubscribleProcessor(message.Subscriber));
                        break;
                    default:
                        break;
                }
                Become(Process);
            });
            Receive<VibrationMessage>(message => HandleVibrationMessage(message));
            Receive<ProcessTypeChange>(message => _vibrationProcessActor.Forward(message));
            Receive<FilterChange>(message => _filterActor.Forward(message));
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

        private void HandleTimeOut(ReceiveTimeout message)
        {
            sg.IsConnected = false;
            foreach (var divFre in sg.DivFres)
            {
                //divFre.IsConnected = false;
               // divFre.IsRead = false;
            }
        }

        private void HandleVibrationMessage(VibrationMessage message)
        {
            hasProcessed = true;
            SetSignal(message.DataContract);
            foreach(var linker in _linkList)
            {
                linker.Tell(message);
            }
        }

        private void SetSignal(ChannelDataContract dataContract)
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
            sg.TriggerN = (TriggerType)Enum.Parse(typeof(TriggerType), dataContract.TriggerN.ToString());
            sg.LowDanger = dataContract.LowDanger;
            sg.LowAlert = dataContract.LowAlert;
            sg.LowNormal = dataContract.LowNormal;
            sg.HighNormal = dataContract.HighNormal;
            sg.HighAlert = dataContract.HighAlert;
            sg.HighDanger = dataContract.HighDanger;
            sg.STIME = dataContract.STIME;
            sg.AMS = dataContract.AMS;
            sg.AlarmType = dataContract.AlarmType;
            sg.AlarmGrade = (AlarmGrade)Enum.Parse(typeof(AlarmGrade), dataContract.AlarmGrade.ToString());

            sg.SVType = (SVType)Enum.Parse(typeof(SVType), dataContract.SVType.ToString());
            sg.nType = (nType)Enum.Parse(typeof(nType), dataContract.nType.ToString());
            sg.SensorType = (SensorType)Enum.Parse(typeof(SensorType), dataContract.SensorType.ToString());
            sg.VMSDir = (TPDir)Enum.Parse(typeof(TPDir), dataContract.VMSDir.ToString());
            sg.HP = (HP)Enum.Parse(typeof(HP), dataContract.HP.ToString());
            if (dataContract.RPM.HasValue)
            {
                sg.RPM = dataContract.RPM.Value;
            }
            sg.IsEnp = dataContract.IsEnp;

            if (dataContract.TeethNumber.HasValue)
            {
                sg.TeethNumber = dataContract.TeethNumber.Value;
            }
            if (dataContract.IsUpload.HasValue)
            {
                sg.IsUpload = dataContract.IsUpload.Value;
            }
            if (dataContract.RSlotNum.HasValue)
            {
                sg.RSlotNum = dataContract.RSlotNum.Value;
            }
            if (dataContract.RChaN.HasValue)
            {
                sg.RChaN = dataContract.RChaN.Value;
            }
            if (dataContract.HDRSlotNum.HasValue)
            {
                sg.HDRSlotNum = dataContract.HDRSlotNum.Value;
            }
            if (dataContract.HDChaN.HasValue)
            {
                sg.HDRChaN = dataContract.HDChaN.Value;
            }
            if (dataContract.SlotNum.HasValue)
            {
                sg.SlotNum = dataContract.SlotNum.Value;
            }
            if (dataContract.ChaN.HasValue)
            {
                sg.ChaN = dataContract.ChaN.Value;
            }
            if (dataContract.TSpan.HasValue)
            {
                sg.TSpan = dataContract.TSpan.Value;
            }
            if (dataContract.IntevalTime.HasValue)
            {
                sg.IntevalTime = dataContract.IntevalTime.Value;
            }
            if (dataContract.IsAntiAliase.HasValue)
            {
                sg.IsAntiAliase = dataContract.IsAntiAliase.Value;
            }
            sg.SamplePoint = dataContract.SamplePoint;
            if (sg.TriggerN == TriggerType.Angle)
            {
                sg.SampleFre = sg.RPM * sg.TeethNumber / 60;
            }
            else
            {
                sg.SampleFre = dataContract.SampleFre;
            }
            sg.IP = dataContract.IP;
            sg.Winfun = (Winfun)Enum.Parse(typeof(Winfun), dataContract.Winfun.ToString());
            sg.Unit = (Unit)Enum.Parse(typeof(Unit), dataContract.Unit.ToString());
            sg.OperatingModeUnit = dataContract.OperatingModeUnit;
            sg.OperatingModePara = dataContract.OperatingModePara;
            sg.ComparativePercent = dataContract.ComparativePercent;
            sg.FormulaLowDanger = dataContract.FormulaLowDanger;
            sg.FormulaLowAlert = dataContract.FormulaLowAlert;
            sg.FormulaLowNormal = dataContract.FormulaLowNormal;
            sg.FormulaHighNormal = dataContract.FormulaHighNormal;
            sg.FormulaHighAlert = dataContract.FormulaHighAlert;
            sg.FormulaHighDanger = dataContract.FormulaHighDanger;
            if (dataContract.SampleType.HasValue)
            {
                sg.SampleType = (SampleType)Enum.Parse(typeof(SampleType), dataContract.SampleType.Value.ToString());
            }
            sg.HibernationTime = dataContract.HibernationTime;
            sg.WorkTime = dataContract.WorkTime;
            sg.WirelessMAC = dataContract.WirelessMAC;

            sg.Bytes = dataContract.VData;
            sg.IsConnected = true;

            foreach (var item in sg.DivFres)
            {
                var divFreContract = dataContract.DivFre.Where(o => o.FreDescription == item.Name).SingleOrDefault();
                if (divFreContract != null)
                {

                    item.UploadTime = divFreContract.STIME;
                    item.FreV = divFreContract.FreV;
                    item.AlarmType = divFreContract.AlarmType;
                    item.DivFreType = (DivFreType)Enum.Parse(typeof(DivFreType), divFreContract.DivFreType.ToString());
                    item.Base1Fre = divFreContract.Base1Fre;
                    item.Base1FrePercent = divFreContract.Base1FrePercent;
                    item.Base2Fre = divFreContract.Base2Fre;
                    item.MultiFre = divFreContract.MultiFre;
                    item.DivFreStrategt = divFreContract.DivFreStrategt.Value;
                    item.MaxFreNum = divFreContract.MaxFreNum;
                    item.LowDanger = divFreContract.LowDanger;
                    item.LowAlert = divFreContract.LowAlert;
                    item.LowNormal = divFreContract.LowNormal;
                    item.HighNormal = divFreContract.HighNormal;
                    item.HighAlert = divFreContract.HighAlert;
                    item.HighDanger = divFreContract.HighDanger;
                    item.AlarmGrade = (AlarmGrade)Enum.Parse(typeof(AlarmGrade), divFreContract.AlarmGrade.ToString());
                    item.Phase = divFreContract.Phase;
                    if (divFreContract.RPM.HasValue)
                    {
                        item.RPM = divFreContract.RPM.Value;
                    }
                    if (divFreContract.IntevalTime.HasValue)
                    {
                       // item.IntevalTime = divFreContract.IntevalTime.Value;
                    }
                    if (divFreContract.IsUpload.HasValue)
                    {
                        item.IsUpload = divFreContract.IsUpload.Value;
                    }
                    if (divFreContract.CharacteristicFre.HasValue)
                    {
                        item.CharacteristicFre = divFreContract.CharacteristicFre.Value;
                    }
                    if (divFreContract.DefaultR.HasValue)
                    {
                        item.DefaultR = divFreContract.DefaultR.Value;
                    }
                    item.FreMV = divFreContract.FreMV;
                    item.OperatingModeUnit = divFreContract.OperatingModeUnit;
                    item.OperatingModePara = divFreContract.OperatingModePara;
                    item.ComparativePercent = divFreContract.ComparativePercent;

                    item.FormulaLowDanger = divFreContract.FormulaLowDanger;
                    item.FormulaLowAlert = divFreContract.FormulaLowAlert;
                    item.FormulaLowNormal = divFreContract.FormulaLowNormal;
                    item.FormulaHighNormal = divFreContract.FormulaHighNormal;
                    item.FormulaHighAlert = divFreContract.FormulaHighAlert;
                    item.FormulaHighDanger = divFreContract.FormulaHighDanger;

                   // item.IsConnected = true;
                  //  item.IsRead = true;
                }
                else
                {
                  //  item.IsConnected = false;
                  //  item.IsRead = false;
                }
            }
        }
    }
}

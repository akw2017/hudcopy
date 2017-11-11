using AIC.CoreType;
using AIC.Database;
using AIC.Domain;
using AIC.ServiceInterface;
using Prism.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIC.SignalCache
{
    public class SignalCache : ISignalCache
    {
        private IDictionary<ChannelIdentity, SignalBase> SgDict;

        private readonly IEventAggregator _eventAggregator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPDAService _pdaService;

        public SignalCache(IEventAggregator eventAggregator,IUnitOfWork unitOfWork, IPDAService pdaService)
        {
            _eventAggregator = eventAggregator;
            _unitOfWork = unitOfWork;
            _pdaService = pdaService;
            SgDict = new Dictionary<ChannelIdentity, SignalBase>(new ChannelIdentityComparer());
        }

        public void Initialize()
        {
            CreateSignals();
        }

        private void NotifySignalsAdded(SignalBase signals)
        {

        }

        private void NotifySignalsRemoved(SignalBase signal)
        {

        }

        private void CreateSignals()
        {
            foreach (var channel in _pdaService.GetChannels())
            {
                if (!SgDict.ContainsKey(channel.ChannelId))
                {
                    if(channel is AnalogInChannelModel)
                    {
                        SgDict.Add(channel.ChannelId, new AnalogInSignal(channel.ChannelId));
                    }
                    else if(channel is DigitTachometerChannelModel)
                    {
                        SgDict.Add(channel.ChannelId, new DigitTachometerSignal(channel.ChannelId));
                    }
                    else if (channel is EddyCurrentDisplacementChannelModel)
                    {
                        SgDict.Add(channel.ChannelId, new EddyCurrentDisplacementSignal(channel.ChannelId));
                    }
                    else if (channel is EddyCurrentKeyPhaseChannelModel)
                    {
                        SgDict.Add(channel.ChannelId, new EddyCurrentKeyPhaseSignal(channel.ChannelId));
                    }
                    else if (channel is EddyCurrentTachometerChannelModel)
                    {
                        SgDict.Add(channel.ChannelId, new EddyCurrentTachometerSignal(channel.ChannelId));
                    }
                    else if (channel is IEPEChannelModel)
                    {
                        SgDict.Add(channel.ChannelId, new IEPESignal(channel.ChannelId));
                    }
                    else if (channel is RelayChannelModel)
                    {
                        SgDict.Add(channel.ChannelId, new RelaySignal(channel.ChannelId));
                    }
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"ChannelId已被占用:{channel.ChannelId.ToString()}");
                    sb.AppendLine(SgDict[channel.ChannelId].GetType().ToString());
                    sb.AppendLine("AnalogInSignal");
                    throw new Exception(sb.ToString());
                }
            }
        }

        //private AnalogSignal CreateAnalogSignal(LMAnInfoTableContract anInfo)
        //{
        //    return new AnalogSignal(ChannelIdentity.Create(anInfo.IP, anInfo.SlotNum, anInfo.ChaN).Value);
        //}
        //private DigitalSignal CreateDigitalSignal(LMAnInfoTableContract anInfo)
        //{
        //    return new DigitalSignal(ChannelIdentity.Create(anInfo.IP, anInfo.SlotNum, anInfo.ChaN).Value);
        //}
        //private CompositionSignal CreateCompositionSignal(LMAnInfoTableContract anInfo)
        //{
        //    return new CompositionSignal(ChannelIdentity.Create(anInfo.IP, 0, anInfo.ChaN).Value);
        //}
        //private VibrationSignal CreateVibrationSignal(LMVInfoTableContract vInfo)
        //{
        //    return new VibrationSignal(ChannelIdentity.Create(vInfo.IP, vInfo.SlotNum, vInfo.ChaN).Value);
        //}
        //private VibrationSignal AttachDivFreIfExist(VibrationSignal signal)
        //{
        //    unitOfWork.LMDivFres
        //              .Find(o => o.IP == signal.IP && o.SlotNum == signal.SlotNum && o.ChaN == signal.ChaN)
        //              .ForEach(div => { signal.AddDivFre(new DivFre() { FreDescription = div.FreDescription }); });
        //    return signal;
        //}
        private void AddSignal(SignalBase signal)
        {
            if (SgDict.ContainsKey(signal.ChannelId))
            {
                throw new InvalidOperationException($"信号已存在{signal.ChannelId.IP}.{signal.ChannelId.CardNum}.{signal.ChannelId.ChannelNum}");
            }
            SgDict.Add(signal.ChannelId, signal);
        }

        private void _treeService_TestPointPaired(TestPointTreeModel testPoint)
        {
            if (testPoint == null) return;
            if (!SgDict.ContainsKey(testPoint.ChannelId.Value))
            {
                AlarmSignal sg = null;
                if (testPoint.SignalType == SignalType.Vibration)
                {
                    sg = new VibrationSignal(testPoint.ChannelId.Value);
                }
                else if (testPoint.SignalType == SignalType.Analog)
                {
                    sg = new AnalogSignal(testPoint.ChannelId.Value);
                }
                else if (testPoint.SignalType == SignalType.Digital)
                {
                    sg = new DigitalSignal(testPoint.ChannelId.Value);
                }
                else if (testPoint.SignalType == SignalType.Composition)
                {
                    sg = new CompositionSignal(testPoint.ChannelId.Value);
                }
                if (sg != null)
                {
                    if (testPoint.SignalType == SignalType.Vibration)
                    {
                        foreach (var child in testPoint.Children)
                        {
                            var divfreTM = child as DivFreTreeModel;
                            DivFre divFre = new DivFre();
                            divFre.Name = divfreTM.Name.Value;
                            ((VibrationSignal)sg).AddDivFre(divFre);
                        }
                    }
                    SgDict.Add(testPoint.ChannelId.Value, sg);
                    NotifySignalsAdded(sg);
                }
            }
        }

        private void _treeService_TestPointUnPaired(TestPointTreeModel testPoint)
        {
            if (testPoint == null) return;
            if (SgDict.ContainsKey(testPoint.ChannelId.Value))
            {
                SignalBase signal = null;
                lock (SgDict)
                {
                    signal = SgDict[testPoint.ChannelId.Value];
                    SgDict.Remove(testPoint.ChannelId.Value);
                }
                if (signal != null)
                {
                    NotifySignalsRemoved(signal);
                }
            }
        }

        private void _treeService_DivFreTreeModelAdded(DivFreTreeModel divfreTM)
        {
            if (divfreTM == null) return;
            var testPoint = divfreTM.Parent as TestPointTreeModel;
            if (testPoint == null) return;
            if (testPoint.IsPaired)
            {
                lock (SgDict)
                {
                    if (SgDict.ContainsKey(testPoint.ChannelId.Value))
                    {
                        var sg = SgDict[testPoint.ChannelId.Value];
                        if (sg is VibrationSignal)
                        {
                            var vSg = sg as VibrationSignal;
                            DivFre divFre = new DivFre();
                            divFre.Name = divfreTM.Name.Value;
                            vSg.AddDivFre(divFre);
                        }
                    }
                }
            }
        }

        private void _treeService_DivFreTreeModelRemoved(IEnumerable<DivFreTreeModel> divfreTMs)
        {
            foreach (var divfreTM in divfreTMs)
            {
                if (divfreTM == null) return;
                var testPoint = divfreTM.Parent as TestPointTreeModel;
                if (testPoint == null) return;
                if (testPoint.IsPaired)
                {
                    lock (SgDict)
                    {
                        if (SgDict.ContainsKey(testPoint.ChannelId.Value))
                        {
                            var sg = SgDict[testPoint.ChannelId.Value];
                            if (sg is VibrationSignal)
                            {
                                var vSg = sg as VibrationSignal;
                                DivFre divFre = vSg.DivFres.Where(o => o.Name == divfreTM.Name.Value).SingleOrDefault();
                                if (divFre != null)
                                {
                                    vSg.RemoveDivFre(divFre);
                                }
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<SignalBase> Signals
        {
            get { return SgDict.Values; }
        }

        public SignalBase GetSignal(ChannelIdentity channelID)
        {
            //HashSet<int> hs = new HashSet<int>();

            if (SgDict.ContainsKey(channelID))
            {
                return SgDict[channelID];
            }
            else
            {
                return null;
            }
        }

    }
}

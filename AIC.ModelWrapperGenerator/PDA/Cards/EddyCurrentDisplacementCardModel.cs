using AIC.CoreType;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.ModelWrapperGenerator
{
    public class EddyCurrentDisplacementCardModel : BaseCardModel
    {
        public EddyCurrentDisplacementCardModel(EddyCurrentDisplacementCard card)
            : base(CardIdentity.Create(card.IP, card.CardNum).Value, card.Count.Value)
        {
            Card = card;
            CardName = card.CardName;
            InSignalCategories = card.InSignalCategory != null ? JsonConvert.DeserializeObject<IEnumerable<InSignalCategory>>(card.InSignalCategory) : null;
            InSignalCode = card.InSignalCode != null ? InSignalCategories.Where(o => o.Code == card.InSignalCode.Value).SingleOrDefault() : null;
            SampleFre = card.SampleFre ?? 0;
            SamplePoint = card.SamplePoint ?? 0;
            SampleType = card.SampleType == null ? SampleType.Transient : (SampleType)card.SampleType;
            TriggerChannelNum = card.TriggerChannelNum;
            TriggerCardNum = card.TriggerCardNum;
            UploadIntevalTime = card.UploadIntevalTime ?? 0;
            Cycles = card.Cycles ?? 0;
            Is24V = card.Is24V ?? false;
        }

        public override void AddChannel(BaseChannelModel channel)
        {
            if (channel is EddyCurrentDisplacementChannelModel)
            {
                base.AddChannel(channel);
            }
            else
            {
                throw new Exception("通道类型必须是EddyCurrentDisplacementChannel");
            }
        }

        public EddyCurrentDisplacementCard Card { get; }

        public IEnumerable<InSignalCategory> InSignalCategories { get; }

        private InSignalCategory inSignalCode;
        public InSignalCategory InSignalCode
        {
            get { return inSignalCode; }
            set
            {
                if (SetProperty(ref inSignalCode, value))
                    Card.InSignalCode = inSignalCode != null ? inSignalCode.Code : default(int?);
            }
        }

        private string cardName;
        public string CardName
        {
            get { return cardName; }
            set
            {
                if (SetProperty(ref cardName, value))
                    Card.CardName = value;
            }
        }

        private double sampleFre;
        public double SampleFre
        {
            get { return sampleFre; }
            set
            {
                if (SetProperty(ref sampleFre, value))
                    Card.SampleFre = value;
            }
        }

        private int samplePoint;
        public int SamplePoint
        {
            get { return samplePoint; }
            set
            {
                if (SetProperty(ref samplePoint, value))
                    Card.SamplePoint = value;
            }
        }

        private SampleType sampleType;
        public SampleType SampleType
        {
            get { return sampleType; }
            set
            {
                if (SetProperty(ref sampleType, value))
                    Card.SampleType = (int)value;
            }
        }

        private HP highPass;
        public HP HighPass
        {
            get { return highPass; }
            set
            {
                if (SetProperty(ref highPass, value))
                    Card.HighPass = (int)value;
            }
        }

        private TriggerType triggerType;
        public TriggerType TriggerType
        {
            get { return triggerType; }
            set
            {
                if (SetProperty(ref triggerType, value))
                    Card.TriggerType = (int)value;
            }
        }

        private string triggerChannelNum;
        public string TriggerChannelNum
        {
            get { return triggerChannelNum; }
            set
            {
                if (SetProperty(ref triggerChannelNum, value))
                    Card.TriggerChannelNum = value;
            }
        }

        private string triggerCardNum;
        public string TriggerCardNum
        {
            get { return triggerCardNum; }
            set
            {
                if (SetProperty(ref triggerCardNum, value))
                    Card.TriggerCardNum = value;
            }
        }

        private int uploadIntevalTime;
        public int UploadIntevalTime
        {
            get { return uploadIntevalTime; }
            set
            {
                if (SetProperty(ref uploadIntevalTime, value))
                    Card.UploadIntevalTime = value;
            }
        }

        private int cycles;
        public int Cycles
        {
            get { return cycles; }
            set
            {
                if (SetProperty(ref cycles, value))
                    Card.Cycles = value;
            }
        }

        private bool is24V;
        public bool Is24V
        {
            get { return is24V; }
            set
            {
                if (SetProperty(ref is24V, value))
                    Card.Is24V = value;
            }
        }
    }
}

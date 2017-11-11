using System;
using AIC.Server.SFD.Contract;



namespace AIC.Database
{
    public class UnitOfWorkDemo : IUnitOfWork
    {
        private IRepository<TestPoint> testPoints;

        private IRepository<PDABase> pdas;

        private IRepository<AnalogInCard> analogInCards;
        private IRepository<DigitTachometerCard> digitTachometerCards;
        private IRepository<EddyCurrentDisplacementCard> eddyCurrentDisplacementCards;
        private IRepository<EddyCurrentTachometerCard> eddyCurrentTachometerCards;
        private IRepository<EddyCurrentKeyPhaseCard> eddyCurrentKeyPhaseCards;
        private IRepository<IEPECard> iepeCards;
        private IRepository<RelayCard> relayCards;

        private IRepository<AnalogInChannel> analogInChannels;
        private IRepository<DigitTachometerChannel> digitTachometerChannels;
        private IRepository<EddyCurrentDisplacementChannel> eddyCurrentDisplacementChannels;
        private IRepository<EddyCurrentTachometerChannel> eddyCurrentTachometerChannels;
        private IRepository<EddyCurrentKeyPhaseChannel> eddyCurrentKeyPhaseChannels;
        private IRepository<IEPEChannel> iepeChannels;
        private IRepository<RelayChannel> relayChannels;


        private IRepository<ShaftContract> shafts;
        private IRepository<BearingContract> bearings;
        private IRepository<DeviceContract> devices;
        public UnitOfWorkDemo()
        {
            
        }

        public void Initialize()
        {
            PDAs.Query();

            AnalogInCards.Query();
            DigitTachometerCards.Query();
            EddyCurrentDisplacementCards.Query();
            EddyCurrentTachometerCards.Query();
            EddyCurrentKeyPhaseCards.Query();
            IEPECards.Query();
            RelayCards.Query();

            AnalogInChannels.Query();
            DigitTachometerChannels.Query();
            EddyCurrentDisplacementChannels.Query();
            EddyCurrentTachometerChannels.Query();
            EddyCurrentKeyPhaseChannels.Query();
            IEPEChannels.Query();
            RelayChannels.Query();
        }

        public IRepository<TestPoint> TestPoints => testPoints ?? new TestPointDemoData();

        public IRepository<PDABase> PDAs=> pdas ?? new PDABaseDemoData();

        public IRepository<AnalogInCard> AnalogInCards=> analogInCards ?? new AnalogInCardDemoData();
        public IRepository<DigitTachometerCard> DigitTachometerCards => digitTachometerCards ?? new DigitTachometerCardDemoData();
        public IRepository<EddyCurrentDisplacementCard> EddyCurrentDisplacementCards => eddyCurrentDisplacementCards ?? new EddyCurrentDisplacementCardDemoData();
        public IRepository<EddyCurrentTachometerCard> EddyCurrentTachometerCards => eddyCurrentTachometerCards ?? new EddyCurrentTachometerCardDemoData();
        public IRepository<EddyCurrentKeyPhaseCard> EddyCurrentKeyPhaseCards => eddyCurrentKeyPhaseCards ?? new EddyCurrentKeyPhaseCardDemoData();
        public IRepository<IEPECard> IEPECards => iepeCards ?? new IEPECardDemoData();
        public IRepository<RelayCard> RelayCards => relayCards ?? new RelayCardDemoData();

        public IRepository<AnalogInChannel> AnalogInChannels => analogInChannels ?? new AnalogInChannelDemoData();
        public IRepository<DigitTachometerChannel> DigitTachometerChannels => digitTachometerChannels ?? new DigitTachometerChannelDemoData();
        public IRepository<EddyCurrentDisplacementChannel> EddyCurrentDisplacementChannels => eddyCurrentDisplacementChannels ?? new EddyCurrentDisplacementChannelDemoData();
        public IRepository<EddyCurrentTachometerChannel> EddyCurrentTachometerChannels => eddyCurrentTachometerChannels ?? new EddyCurrentTachometerChannelDemoData();
        public IRepository<EddyCurrentKeyPhaseChannel> EddyCurrentKeyPhaseChannels => eddyCurrentKeyPhaseChannels ?? new EddyCurrentKeyPhaseChannelDemoData();
        public IRepository<IEPEChannel> IEPEChannels => iepeChannels ?? new IEPEChannelDemoData();
        public IRepository<RelayChannel> RelayChannels => relayChannels ?? new RelayChannelDemoData();

        public IRepository<DeviceContract> Devices => devices;
        public IRepository<ShaftContract> Shafts => shafts;
        public IRepository<BearingContract> Bearings => bearings;

    }
}

using AIC.OnlineSystem.Client;

using AIC.Server.SFD.Contract;

namespace AIC.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MasterContext _context;

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
        public UnitOfWork()
        {
            _context = new MasterContext("localhost");
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

        public IRepository<TestPoint> TestPoints => testPoints ?? new Repository<TestPoint>(_context);

        #region MasterTable
        public IRepository<PDABase> PDAs => pdas ?? new Repository<PDABase>(_context);

        public IRepository<AnalogInCard> AnalogInCards => analogInCards ?? new Repository<AnalogInCard>(_context);
        public IRepository<DigitTachometerCard> DigitTachometerCards => digitTachometerCards ?? new Repository<DigitTachometerCard>(_context);
        public IRepository<EddyCurrentDisplacementCard> EddyCurrentDisplacementCards => eddyCurrentDisplacementCards ?? new Repository<EddyCurrentDisplacementCard>(_context);
        public IRepository<EddyCurrentTachometerCard> EddyCurrentTachometerCards => eddyCurrentTachometerCards ?? new Repository<EddyCurrentTachometerCard>(_context);
        public IRepository<EddyCurrentKeyPhaseCard> EddyCurrentKeyPhaseCards => eddyCurrentKeyPhaseCards ?? new Repository<EddyCurrentKeyPhaseCard>(_context);
        public IRepository<IEPECard> IEPECards => iepeCards ?? new Repository<IEPECard>(_context);
        public IRepository<RelayCard> RelayCards => relayCards ?? new Repository<RelayCard>(_context);

        public IRepository<AnalogInChannel> AnalogInChannels => analogInChannels ?? new Repository<AnalogInChannel>(_context);
        public IRepository<DigitTachometerChannel> DigitTachometerChannels => digitTachometerChannels ?? new Repository<DigitTachometerChannel>(_context);
        public IRepository<EddyCurrentDisplacementChannel> EddyCurrentDisplacementChannels => eddyCurrentDisplacementChannels ?? new Repository<EddyCurrentDisplacementChannel>(_context);
        public IRepository<EddyCurrentTachometerChannel> EddyCurrentTachometerChannels => eddyCurrentTachometerChannels ?? new Repository<EddyCurrentTachometerChannel>(_context);
        public IRepository<EddyCurrentKeyPhaseChannel> EddyCurrentKeyPhaseChannels => eddyCurrentKeyPhaseChannels ?? new Repository<EddyCurrentKeyPhaseChannel>(_context);
        public IRepository<IEPEChannel> IEPEChannels => iepeChannels ?? new Repository<IEPEChannel>(_context);
        public IRepository<RelayChannel> RelayChannels => relayChannels ?? new Repository<RelayChannel>(_context);

        public IRepository<DeviceContract> Devices => devices ?? new Repository<DeviceContract>(_context);
        public IRepository<ShaftContract> Shafts => shafts = new Repository<ShaftContract>(_context);
        public IRepository<BearingContract> Bearings => bearings ?? new Repository<BearingContract>(_context);
        #endregion MasterTable
    }
}


using AIC.Server.SFD.Contract;

namespace AIC.Database
{
    public interface IUnitOfWork
    {
        IRepository<TestPoint> TestPoints { get; }

        IRepository<PDABase> PDAs { get; }

        IRepository<AnalogInCard> AnalogInCards { get; }
        IRepository<DigitTachometerCard> DigitTachometerCards { get; }
        IRepository<EddyCurrentDisplacementCard> EddyCurrentDisplacementCards { get; }
        IRepository<EddyCurrentTachometerCard> EddyCurrentTachometerCards { get; }
        IRepository<EddyCurrentKeyPhaseCard> EddyCurrentKeyPhaseCards { get; }
        IRepository<IEPECard> IEPECards { get; }
        IRepository<RelayCard> RelayCards { get; }
        IRepository<AnalogInChannel> AnalogInChannels { get; }

        IRepository<DigitTachometerChannel> DigitTachometerChannels { get; }
        IRepository<EddyCurrentDisplacementChannel> EddyCurrentDisplacementChannels { get; }
        IRepository<EddyCurrentTachometerChannel> EddyCurrentTachometerChannels { get; }
        IRepository<EddyCurrentKeyPhaseChannel> EddyCurrentKeyPhaseChannels { get; }
        IRepository<IEPEChannel> IEPEChannels { get; }
        IRepository<RelayChannel> RelayChannels { get; }

        IRepository<DeviceContract> Devices { get; }
        IRepository<ShaftContract> Shafts { get; }
        IRepository<BearingContract> Bearings { get; }

        void Initialize();
    }
}

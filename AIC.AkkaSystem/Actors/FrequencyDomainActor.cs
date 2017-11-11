using AIC.AkkaSystem.Messages;
using Akka.Actor;
using Arction.Wpf.Charting;

namespace AIC.AkkaSystem.Actors
{
    public class FrequencyDomainActor : ReceiveActor
    {
        private readonly LightningChartUltimate m_chart;
        private readonly IActorRef _chartFrequencyActor;
        private readonly IActorRef _fftActor;
        private readonly IActorRef _vibrationProcessActor; 
        private readonly IActorRef _filterActor;
        private readonly IActorRef _byteToSingleActor;

        public FrequencyDomainActor(LightningChartUltimate chart)
        {
            //Receive<ChannelDataMessage>(data =>HandleChannelData(data));
            //Receive<EnvelopeMessage>(envelope => HandleEnvelope(envelope));
            //Receive<TFFMessage>(tff => HandleTFF(tff));
            //Receive<CepstrumMessage>(cepstrum => HandleCepstrum(cepstrum));
            //Receive<ByteToSingleMessage>(data => HandleByteToSingle(data));
            //Receive<VParmMessage>(data => HandleVParmMessage(data));
            m_chart = chart;

            _chartFrequencyActor = Context.ActorOf(Props.Create(() => new ChartFrequencyActor(m_chart)));

            _fftActor = Context.ActorOf(Props.Create(() => new FFTActor()));
            _fftActor.Tell(new SubscribleProcessor(_chartFrequencyActor));

            _vibrationProcessActor = Context.ActorOf(Props.Create(() => new VibrationProcessActor()));
            _vibrationProcessActor.Tell(new SubscribleProcessor(_fftActor));

            _filterActor = Context.ActorOf(Props.Create(() => new FilterActor()));
            _filterActor.Tell(new SubscribleProcessor(_vibrationProcessActor));

            _byteToSingleActor = Context.ActorOf(Props.Create(() => new ByteToSingleActor()));
            _byteToSingleActor.Tell(new SubscribleProcessor(_filterActor));

            Receive<VibrationMessage>(message => HandleVibrationMessage(message));
           // Receive<FilterActor.FilterChange>(message => _filterActor.Tell(message));
            //Receive<VibrationProcessActor.ProcessTypeChange>(message => _vibrationProcessActor.Tell(message));
        }

        private void HandleVibrationMessage(VibrationMessage message)
        {
            _byteToSingleActor.Tell(message);
        }
    }
}

using AIC.AkkaSystem.Messages;
using AIC.Mathmatics;
using Akka.Actor;

namespace AIC.AkkaSystem.Actors
{
    public class EnvelopeActor: ReceiveActor
    {
        public EnvelopeActor()
        {
            Receive<WaveformMessage>(envelope => HandleEnvelope(envelope));
        }

        private void HandleEnvelope(WaveformMessage envelope)
        {
            var result = Algorithm.Instance.Envelope(envelope.VData, envelope.SamplePoint);
            Sender.Tell(new EnvelopeMessage(result));
        }
    }
}

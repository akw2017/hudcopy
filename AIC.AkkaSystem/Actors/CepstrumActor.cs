using AIC.AkkaSystem.Messages;
using AIC.Mathmatics;
using Akka.Actor;

namespace AIC.AkkaSystem.Actors
{
    public class CepstrumActor : ReceiveActor
    {
        public CepstrumActor()
        {
            Receive<WaveformMessage>(Cepstrum => HandleCepstrum(Cepstrum));
        }

        private void HandleCepstrum(WaveformMessage Cepstrum)
        {
            var result = Algorithm.Instance.Cepstrum(Cepstrum.VData, Cepstrum.SamplePoint);
            Sender.Tell(new CepstrumMessage(result));
        }
    }
}

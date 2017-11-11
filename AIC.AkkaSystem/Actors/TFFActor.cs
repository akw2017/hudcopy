using AIC.AkkaSystem.Messages;
using AIC.Mathmatics;
using Akka.Actor;

namespace AIC.AkkaSystem.Actors
{
    public class TFFActor : ReceiveActor
    {
        public TFFActor()
        {
            Receive<WaveformMessage>(waveform => HandleWaveform(waveform));
        }

        private void HandleWaveform(WaveformMessage waveform)
        {
            var result = Algorithm.Instance.TFF(waveform.VData, waveform.SamplePoint, waveform.SampleFre);
            Sender.Tell(new TFFMessage(result));
        }
    }
}

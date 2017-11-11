using AIC.AkkaSystem.Messages;
using AIC.ServiceInterface;
using Akka.Actor;

namespace AIC.AkkaSystem.Actors
{
    public class RTDataAccessActor: ReceiveActor
    {

        private readonly IRTDataService _rtDataService;
        public RTDataAccessActor(IRTDataService rtDataService)
        {
            _rtDataService = rtDataService;
            Receive<RefreshRTData>(message => GetData(message));
        }

        private void GetData(RefreshRTData message)
        {
            _rtDataService.GetChannelContractsAsync()
                .ContinueWith(task =>
                {
                    return new UpdatedRTData(task.Result);
                }).PipeTo(Sender);
        }
    }
}

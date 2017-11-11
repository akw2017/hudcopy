namespace AIC.AkkaSystem.Messages
{
    public class RefreshRTData
    {
        private RefreshRTData()
        {

        }

        private readonly static RefreshRTData instance = new RefreshRTData();

        public static RefreshRTData Instance { get { return instance; } }
    }
}

namespace AIC.ModelWrapperGenerator
{
    public class EddyCurrentTachometerChannelModel : BaseChannelModel
    {
        private float biasVoltHigh;
        public float BiasVoltHigh
        {
            get { return biasVoltHigh; }
            set { SetProperty(ref biasVoltHigh, value); }
        }

        private float biasVoltLow;
        public float BiasVoltLow
        {
            get { return biasVoltLow; }
            set { SetProperty(ref biasVoltLow, value); }
        }

        private float thresholdVolt;
        public float ThresholdVolt
        {
            get { return thresholdVolt; }
            set { SetProperty(ref thresholdVolt, value); }
        }

        private float hysteresisVolt;
        public float HysteresisVolt
        {
            get { return hysteresisVolt; }
            set { SetProperty(ref hysteresisVolt, value); }
        }

        private int thresholdMode;
        public int ThresholdMode
        {
            get { return thresholdMode; }
            set { SetProperty(ref thresholdMode, value); }
        }

        private float calibration;
        public float Calibration
        {
            get { return calibration; }
            set { SetProperty(ref calibration, value); }
        }
        private bool isNotch;
        public bool IsNotch
        {
            get { return isNotch; }
            set { SetProperty(ref isNotch, value); }
        }
        private float defaultRPM;
        public float DefaultRPM
        {
            get { return defaultRPM; }
            set { SetProperty(ref defaultRPM, value); }
        }

        private float teethNumber;
        public float TeethNumber
        {
            get { return teethNumber; }
            set { SetProperty(ref teethNumber, value); }
        }
        private bool isTwoMAClamp;
        public bool IsTwoMAClamp
        {
            get { return isTwoMAClamp; }
            set { SetProperty(ref isTwoMAClamp, value); }
        }
        private int delayAlarmTime;
        public int DelayAlarmTime
        {
            get { return delayAlarmTime; }
            set { SetProperty(ref delayAlarmTime, value); }
        }
        private int channelType;
        public int ChannelType
        {
            get { return channelType; }
            set { SetProperty(ref channelType, value); }
        }
    }
}

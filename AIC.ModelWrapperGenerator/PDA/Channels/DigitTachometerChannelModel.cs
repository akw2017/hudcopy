namespace AIC.ModelWrapperGenerator
{
    public class DigitTachometerChannelModel : BaseChannelModel
    {
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
    }
}

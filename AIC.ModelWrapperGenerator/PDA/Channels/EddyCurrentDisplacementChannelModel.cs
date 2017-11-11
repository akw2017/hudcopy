namespace AIC.ModelWrapperGenerator
{
    public class EddyCurrentDisplacementChannelModel : BaseChannelModel
    {
        private int tpDir;
        public int TPDir
        {
            get { return tpDir; }
            set { SetProperty(ref tpDir, value); }
        }
        private string resultType;
        public string ResultType
        {
            get { return resultType; }
            set { SetProperty(ref resultType, value); }
        }

        private int resultCode;
        public int ResultCode
        {
            get { return resultCode; }
            set { SetProperty(ref resultCode, value); }
        }

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

        private float sensitivity;
        public float Sensitivity
        {
            get { return sensitivity; }
            set { SetProperty(ref sensitivity, value); }
        }

        private bool isTwoMAClamp;
        public bool IsTwoMAClamp
        {
            get { return isTwoMAClamp; }
            set { SetProperty(ref isTwoMAClamp, value); }
        }
        private int rpmCardNum;
        public int RPMCardNum
        {
            get { return rpmCardNum; }
            set { SetProperty(ref rpmCardNum, value); }
        }

        private int rpmChannelNum;
        public int RPMChannelNum
        {
            get { return rpmChannelNum; }
            set { SetProperty(ref rpmChannelNum, value); }
        }

        private int delayAlarmTime;
        public int DelayAlarmTime
        {
            get { return delayAlarmTime; }
            set { SetProperty(ref delayAlarmTime, value); }
        }
    }
}

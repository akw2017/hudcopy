namespace AIC.ModelWrapperGenerator
{
    public class AnalogInChannelModel : BaseChannelModel
    {
        private float calibration;
        public float Calibration
        {
            get { return calibration; }
            set { SetProperty(ref calibration, value); }
        }

        private float x0;
        public float X0
        {
            get { return x0; }
            set { SetProperty(ref x0, value); }
        }

        private float y0;
        public float Y0
        {
            get { return y0; }
            set { SetProperty(ref y0, value); }
        }

        private float x1;
        public float X1
        {
            get { return x1; }
            set { SetProperty(ref x1, value); }
        }

        private float y1;
        public float Y1
        {
            get { return y1; }
            set { SetProperty(ref y1, value); }
        }

        private bool isEnableFormula;
        public bool IsEnableFormula
        {
            get { return isEnableFormula; }
            set { SetProperty(ref isEnableFormula, value); }
        }
    }
}

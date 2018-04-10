using Prism.Mvvm;

namespace AIC.Core.DiagnosticFilterModels
{
    public class BandPassFilterPara : BindableBase
    {
        public BandPassFilterPara()
        {
            PassbandAttenuationDB = 0.2;
            StopbandAttenuationDB = 60;
            BPPassbandFreLow = 400;
            BPPassbandFreHigh = 600;
            TransitionBandwidth = 100;
        }
        public double passbandAttenuationDB;
        //通带衰减，建议值0.2
        public double PassbandAttenuationDB
        {
            get { return passbandAttenuationDB; }
            set
            {
                passbandAttenuationDB = value;
                OnPropertyChanged("PassbandAttenuationDB");
            }
        }
        //阻带衰减，建议值60
        private double stopbandAttenuationDB;
        public double StopbandAttenuationDB
        {
            get { return stopbandAttenuationDB; }
            set
            {
                stopbandAttenuationDB = value;
                OnPropertyChanged("StopbandAttenuationDB");
            }
        }
        //带通低逼近通带频率
        private double bPPassbandFreLow;
        public double BPPassbandFreLow
        {
            get { return bPPassbandFreLow; }
            set
            {
                bPPassbandFreLow = value;
                OnPropertyChanged("BPPassbandFreLow");
            }
        }
        //带通高逼近通带频率;
        private double bPPassbandFreHigh;
        public double BPPassbandFreHigh
        {
            get { return bPPassbandFreHigh; }
            set
            {
                bPPassbandFreHigh = value;
                OnPropertyChanged("BPPassbandFreHigh");
            }
        }
        //逼近带宽
        private double transitionBandwidth;
        public double TransitionBandwidth
        {
            get { return transitionBandwidth; }
            set
            {
                transitionBandwidth = value;
                OnPropertyChanged("TransitionBandwidth");
            }
        }
        //带通低阻带频率
        public double BPStopbandFreLow => BPPassbandFreLow - TransitionBandwidth;
        //带通高阻带频率
        public double BPStopBandFreHigh => BPPassbandFreHigh + TransitionBandwidth;

        public BandPassFilterPara Clone ()
        {
            return new BandPassFilterPara()
            {
                PassbandAttenuationDB = this.PassbandAttenuationDB,
                StopbandAttenuationDB = this.StopbandAttenuationDB,
                BPPassbandFreLow = this.BPPassbandFreLow,
                BPPassbandFreHigh = this.BPPassbandFreHigh,
            };
        }
    }
}

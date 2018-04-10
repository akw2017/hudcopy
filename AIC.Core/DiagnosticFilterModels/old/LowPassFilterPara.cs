using Prism.Mvvm;

namespace AIC.Core.DiagnosticFilterModels
{
    public class LowPassFilterPara : BindableBase
    {
        public LowPassFilterPara()
        {
            PassbandAttenuationDB = 0.2;
            StopbandAttenuationDB = 60;
            PassbandFre = 400;
            StopbandFre = 600;
        }

        //通带衰减，建议值0.2
        private double passbandAttenuationDB;
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
        //通带频率
        private double passbandFre;
        public double PassbandFre
        {
            get { return passbandFre; }
            set
            {
                passbandFre = value;
                OnPropertyChanged("PassbandFre");
            }
        }
        //阻带频率
        private double stopbandFre;
        public double StopbandFre
        {
            get { return stopbandFre; }
            set
            {
                stopbandFre = value;
                OnPropertyChanged("StopbandFre");
            }
        }

        public LowPassFilterPara Clone()
        {
            return new LowPassFilterPara()
            {
                PassbandAttenuationDB = this.PassbandAttenuationDB,
                StopbandAttenuationDB = this.StopbandAttenuationDB,
                PassbandFre = this.PassbandFre,
                StopbandFre = this.StopbandFre,
            };
        }
    }
}

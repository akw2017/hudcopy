namespace AIC.DiagnosePage.FilterModels
{
    public class HighPassFilterPara
    {
        public HighPassFilterPara()
        {
            PassbandAttenuationDB = 0.2;
            StopbandAttenuationDB = 60;
            PassbandFre = 600;
            StopbandFre = 400;
        }
        //通带衰减，建议值0.2
        public double PassbandAttenuationDB { get; set; }
        //阻带衰减，建议值60
        public double StopbandAttenuationDB { get; set; }
        //通带频率
        public double PassbandFre { get; set; }
        //阻带频率
        public double StopbandFre { get; set; }

        public HighPassFilterPara Clone()
        {
            return new HighPassFilterPara()
            {
                PassbandAttenuationDB = this.PassbandAttenuationDB,
                StopbandAttenuationDB = this.StopbandAttenuationDB,
                PassbandFre = this.PassbandFre,
                StopbandFre = this.StopbandFre,
            };
        }
    }
}



using AIC.Mathmatics;
using AIC.Domain;

namespace AIC.AkkaSystem.Messages
{
    public class VibrationMessage
    {
        public VibrationMessage(ChannelDataContract dataContract)
        {
            DataContract = dataContract;
            VDataBytes = DataContract.VData;
            SamplePoint = DataContract.SamplePoint;
            if (DataContract.TriggerN == 2)
            {
                SampleFre = DataContract.RPM.GetValueOrDefault() * DataContract.TeethNumber.GetValueOrDefault() / 60;
            }
            else
            {
                SampleFre = dataContract.SampleFre;
            }
            //BPFilter = new BandPassFilter();
            //HPFilter = new HighPassFilter();
            //LPFilter = new LowPassFilter();
        }

        public VibrationMessage UpdateVData(double[] vdata)
        {
            return new VibrationMessage(DataContract)
            {
                VData = vdata,
                Frequency = this.Frequency,
                Amplitude = this.Amplitude,
                Phase = this.Phase,
                VParm = this.VParm,
               // FilterType = this.FilterType,
                //BPFilter = this.BPFilter,
                //HPFilter = this.HPFilter,
                //LPFilter = this.LPFilter,
            };
        }

        public VibrationMessage UpdateFFT(double[] frequency, double[] amplitude, double[] phase)
        {
            return new VibrationMessage(DataContract)
            {
                VData = this.VData,
                Frequency = frequency,
                Amplitude = amplitude,
                Phase = phase,
                VParm = this.VParm,
                //FilterType = this.FilterType,
                //BPFilter = this.BPFilter,
                //HPFilter = this.HPFilter,
                //LPFilter = this.LPFilter,
            };
        }

        public VibrationMessage UpdateVParm(VibrationParm vParm)
        {
            return new VibrationMessage(DataContract)
            {
                VData = this.VData,
                Frequency = this.Frequency,
                Amplitude = this.Amplitude,
                Phase = this.Phase,
                VParm = vParm,
                //FilterType = this.FilterType,
                //BPFilter = this.BPFilter,
                //HPFilter = this.HPFilter,
                //LPFilter = this.LPFilter,
            };
        }

        public ChannelDataContract DataContract { get; }
        public byte[] VDataBytes { get; }
        public double[] VData { get; private set; }
        public double[] Frequency { get;private set; }
        public double[] Amplitude { get;private set; }
        public double[] Phase { get;private set; }
        public VibrationParm VParm { get; private set; }
        public int SamplePoint { get; }
        public double SampleFre { get; }
        //public FilterType FilterType { get; private set; }     
        //public BandPassFilter BPFilter { get; private set; }
        //public HighPassFilter HPFilter { get; private set; }
        //public LowPassFilter LPFilter { get; private set; }
        public bool IsCepstrum { get; private set; }
    }  
}


using AIC.CoreType;

namespace AIC.Domain
{
    public class FilterProcessor
    {
        public FilterProcessor()
        {
            FilterType = FilterType.None;
            BPFilter = new BandPassFilter();
            HPFilter = new HighPassFilter();
            LPFilter = new LowPassFilter();
        }

        public FilterType FilterType { get; set; }
        public BandPassFilter BPFilter { get; set; }
        public HighPassFilter HPFilter { get; set; }
        public LowPassFilter LPFilter { get; set; }

        public double[] Filter(double[] input, int samplePoint, double sampleFre, double rpm)
        {
            if (FilterType == FilterType.BandPass)
            {
                return BPFilter.Filter(input, samplePoint, sampleFre, rpm);
            }
            else if (FilterType == FilterType.HighPass)
            {
                return HPFilter.Filter(input, samplePoint, sampleFre, rpm);
            }
            else if (FilterType == FilterType.LowPass)
            {
                return LPFilter.Filter(input, samplePoint, sampleFre, rpm);
            }
            return input;
        }
    }
}

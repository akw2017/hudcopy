using AIC.CoreType;

namespace AIC.Domain
{
    public class VibrationProcessor
    {
        public VibrationProcessor()
        {
            ProcessType = VibrationProcessType.None;
        }

        public VibrationProcessType ProcessType { get; set; }
    }
}

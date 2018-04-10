using System.ComponentModel;

namespace AIC.CoreType
{
    public enum FilterType
    {
        [Description("不滤波")]
        None = 0,
        [Description("高通滤波")]
        HighPass,
        [Description("低通滤波")]
        LowPass,
        [Description("带通滤波")]
        BandPass
    }
}

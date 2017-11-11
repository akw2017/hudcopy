using AIC.Domain;
using AIC.Domain;


namespace AIC.Domain
{
    public partial class LowPassFilterWrapper : ModelWrapper<LowPassFilter>
  {
    public LowPassFilterWrapper(LowPassFilter model) : base(model)
    {
    }

    public System.Double PassbandAttenuationDB
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double PassbandAttenuationDBOriginalValue => GetOriginalValue<System.Double>(nameof(PassbandAttenuationDB));

    public bool PassbandAttenuationDBIsChanged => GetIsChanged(nameof(PassbandAttenuationDB));

    public System.Double StopbandAttenuationDB
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double StopbandAttenuationDBOriginalValue => GetOriginalValue<System.Double>(nameof(StopbandAttenuationDB));

    public bool StopbandAttenuationDBIsChanged => GetIsChanged(nameof(StopbandAttenuationDB));

    public System.Double PassbandFre
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double PassbandFreOriginalValue => GetOriginalValue<System.Double>(nameof(PassbandFre));

    public bool PassbandFreIsChanged => GetIsChanged(nameof(PassbandFre));

    public System.Double StopbandFre
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double StopbandFreOriginalValue => GetOriginalValue<System.Double>(nameof(StopbandFre));

    public bool StopbandFreIsChanged => GetIsChanged(nameof(StopbandFre));
  }
}

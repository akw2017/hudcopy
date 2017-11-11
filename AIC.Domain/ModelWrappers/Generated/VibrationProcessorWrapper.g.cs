namespace AIC.Domain
{
    public partial class VibrationProcessorWrapper : ModelWrapper<VibrationProcessor>
  {
    public VibrationProcessorWrapper(VibrationProcessor model) : base(model)
    {
    }

    public AIC.CoreType.VibrationProcessType ProcessType
    {
      get { return GetValue<AIC.CoreType.VibrationProcessType>(); }
      set { SetValue(value); }
    }

    public AIC.CoreType.VibrationProcessType ProcessTypeOriginalValue => GetOriginalValue<AIC.CoreType.VibrationProcessType>(nameof(ProcessType));

    public bool ProcessTypeIsChanged => GetIsChanged(nameof(ProcessType));
  }
}

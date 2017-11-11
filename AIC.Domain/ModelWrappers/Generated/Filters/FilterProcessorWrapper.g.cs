using System;

namespace AIC.Domain
{
    public partial class FilterProcessorWrapper : ModelWrapper<FilterProcessor>
  {
    public FilterProcessorWrapper(FilterProcessor model) : base(model)
    {
    }

    public AIC.CoreType.FilterType FilterType
    {
      get { return GetValue<AIC.CoreType.FilterType>(); }
      set { SetValue(value); }
    }

    public AIC.CoreType.FilterType FilterTypeOriginalValue => GetOriginalValue<AIC.CoreType.FilterType>(nameof(FilterType));

    public bool FilterTypeIsChanged => GetIsChanged(nameof(FilterType));
 
    public BandPassFilterWrapper BPFilter { get; private set; }
 
    public HighPassFilterWrapper HPFilter { get; private set; }
 
    public LowPassFilterWrapper LPFilter { get; private set; }
    
    protected override void InitializeComplexProperties(FilterProcessor model)
    {
      if (model.BPFilter == null)
      {
        throw new ArgumentException("BPFilter cannot be null");
      }
      BPFilter = new BandPassFilterWrapper(model.BPFilter);
      RegisterComplex(BPFilter);
      if (model.HPFilter == null)
      {
        throw new ArgumentException("HPFilter cannot be null");
      }
      HPFilter = new HighPassFilterWrapper(model.HPFilter);
      RegisterComplex(HPFilter);
      if (model.LPFilter == null)
      {
        throw new ArgumentException("LPFilter cannot be null");
      }
      LPFilter = new LowPassFilterWrapper(model.LPFilter);
      RegisterComplex(LPFilter);
    }
  }
}

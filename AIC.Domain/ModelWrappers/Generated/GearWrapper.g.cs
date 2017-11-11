namespace AIC.Domain
{
    public partial class GearWrapper : ModelWrapper<Gear>
    {
    public GearWrapper(Gear model) : base(model)
    {
    }

    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));

    public bool NameIsChanged => GetIsChanged(nameof(Name));

    public System.Int32 TeethNumber
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 TeethNumberOriginalValue => GetOriginalValue<System.Int32>(nameof(TeethNumber));

    public bool TeethNumberIsChanged => GetIsChanged(nameof(TeethNumber));
  }
}

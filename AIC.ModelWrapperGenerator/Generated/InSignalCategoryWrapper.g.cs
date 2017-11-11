using System;
using System.Linq;
using AIC.ModelWrapperGenerator;

namespace AIC.ModelWrapperGenerator
{
  public partial class InSignalCategoryWrapper : ModelWrapper<InSignalCategory>
  {
    public InSignalCategoryWrapper(InSignalCategory model) : base(model)
    {
    }

    public System.Int32 Code
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 CodeOriginalValue => GetOriginalValue<System.Int32>(nameof(Code));

    public bool CodeIsChanged => GetIsChanged(nameof(Code));

    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));

    public bool NameIsChanged => GetIsChanged(nameof(Name));
  }
}

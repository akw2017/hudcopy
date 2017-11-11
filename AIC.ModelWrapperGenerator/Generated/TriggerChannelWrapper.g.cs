using System;
using System.Linq;
using AIC.ModelWrapperGenerator;

namespace AIC.ModelWrapperGenerator
{
  public partial class TriggerChannelWrapper : ModelWrapper<TriggerChannel>
  {
    public TriggerChannelWrapper(TriggerChannel model) : base(model)
    {
    }

    public System.String IP
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String IPOriginalValue => GetOriginalValue<System.String>(nameof(IP));

    public bool IPIsChanged => GetIsChanged(nameof(IP));

    public System.String CardNum
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String CardNumOriginalValue => GetOriginalValue<System.String>(nameof(CardNum));

    public bool CardNumIsChanged => GetIsChanged(nameof(CardNum));

    public System.String ChannelNum
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String ChannelNumOriginalValue => GetOriginalValue<System.String>(nameof(ChannelNum));

    public bool ChannelNumIsChanged => GetIsChanged(nameof(ChannelNum));

    public System.Boolean IsEmpty
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }

    public System.Boolean IsEmptyOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsEmpty));

    public bool IsEmptyIsChanged => GetIsChanged(nameof(IsEmpty));
  }
}

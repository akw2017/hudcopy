using System;
using System.Linq;
using AIC.ModelWrapperGenerator;

namespace AIC.ModelWrapperGenerator
{
  public partial class ChannelIdentityWrapper : ModelWrapper<ChannelIdentity>
  {
    public ChannelIdentityWrapper(ChannelIdentity model) : base(model)
    {
    }

    public System.String IP
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String IPOriginalValue => GetOriginalValue<System.String>(nameof(IP));

    public bool IPIsChanged => GetIsChanged(nameof(IP));

    public System.Int32 CardNum
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 CardNumOriginalValue => GetOriginalValue<System.Int32>(nameof(CardNum));

    public bool CardNumIsChanged => GetIsChanged(nameof(CardNum));

    public System.Int32 ChannelNum
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 ChannelNumOriginalValue => GetOriginalValue<System.Int32>(nameof(ChannelNum));

    public bool ChannelNumIsChanged => GetIsChanged(nameof(ChannelNum));
  }
}

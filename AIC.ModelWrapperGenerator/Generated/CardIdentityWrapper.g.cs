using System;
using System.Linq;
using AIC.ModelWrapperGenerator;

namespace AIC.ModelWrapperGenerator
{
  public partial class CardIdentityWrapper : ModelWrapper<CardIdentity>
  {
    public CardIdentityWrapper(CardIdentity model) : base(model)
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
  }
}

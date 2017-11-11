using System;
using System.Linq;
using AIC.ModelWrapperGenerator;

namespace AIC.ModelWrapperGenerator
{
  public partial class RelayChannelModelWrapper : ModelWrapper<RelayChannelModel>
  {
    public RelayChannelModelWrapper(RelayChannelModel model) : base(model)
    {
    }

    public System.String Expression
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String ExpressionOriginalValue => GetOriginalValue<System.String>(nameof(Expression));

    public bool ExpressionIsChanged => GetIsChanged(nameof(Expression));

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

    public System.String Unit
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String UnitOriginalValue => GetOriginalValue<System.String>(nameof(Unit));

    public bool UnitIsChanged => GetIsChanged(nameof(Unit));

    public System.Boolean IsBypass
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }

    public System.Boolean IsBypassOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsBypass));

    public bool IsBypassIsChanged => GetIsChanged(nameof(IsBypass));

    public System.Boolean IsUpload
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }

    public System.Boolean IsUploadOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsUpload));

    public bool IsUploadIsChanged => GetIsChanged(nameof(IsUpload));
 
    public ChannelIdentityWrapper ChannelId { get; private set; }
    
    protected override void InitializeComplexProperties(RelayChannelModel model)
    {
      if (model.ChannelId == null)
      {
        throw new ArgumentException("ChannelId cannot be null");
      }
      ChannelId = new ChannelIdentityWrapper(model.ChannelId);
      RegisterComplex(ChannelId);
    }
  }
}

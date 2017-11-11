using System;
using System.Linq;
using AIC.ModelWrapperGenerator;

namespace AIC.ModelWrapperGenerator
{
  public partial class AnalogInChannelModelWrapper : ModelWrapper<AnalogInChannelModel>
  {
    public AnalogInChannelModelWrapper(AnalogInChannelModel model) : base(model)
    {
    }

    public System.Single Calibration
    {
      get { return GetValue<System.Single>(); }
      set { SetValue(value); }
    }

    public System.Single CalibrationOriginalValue => GetOriginalValue<System.Single>(nameof(Calibration));

    public bool CalibrationIsChanged => GetIsChanged(nameof(Calibration));

    public System.Single X0
    {
      get { return GetValue<System.Single>(); }
      set { SetValue(value); }
    }

    public System.Single X0OriginalValue => GetOriginalValue<System.Single>(nameof(X0));

    public bool X0IsChanged => GetIsChanged(nameof(X0));

    public System.Single Y0
    {
      get { return GetValue<System.Single>(); }
      set { SetValue(value); }
    }

    public System.Single Y0OriginalValue => GetOriginalValue<System.Single>(nameof(Y0));

    public bool Y0IsChanged => GetIsChanged(nameof(Y0));

    public System.Single X1
    {
      get { return GetValue<System.Single>(); }
      set { SetValue(value); }
    }

    public System.Single X1OriginalValue => GetOriginalValue<System.Single>(nameof(X1));

    public bool X1IsChanged => GetIsChanged(nameof(X1));

    public System.Single Y1
    {
      get { return GetValue<System.Single>(); }
      set { SetValue(value); }
    }

    public System.Single Y1OriginalValue => GetOriginalValue<System.Single>(nameof(Y1));

    public bool Y1IsChanged => GetIsChanged(nameof(Y1));

    public System.Boolean IsEnableFormula
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }

    public System.Boolean IsEnableFormulaOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsEnableFormula));

    public bool IsEnableFormulaIsChanged => GetIsChanged(nameof(IsEnableFormula));

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
    
    protected override void InitializeComplexProperties(AnalogInChannelModel model)
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

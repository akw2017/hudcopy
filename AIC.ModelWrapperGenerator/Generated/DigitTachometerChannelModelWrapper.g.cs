using System;
using System.Linq;
using AIC.ModelWrapperGenerator;

namespace AIC.ModelWrapperGenerator
{
  public partial class DigitTachometerChannelModelWrapper : ModelWrapper<DigitTachometerChannelModel>
  {
    public DigitTachometerChannelModelWrapper(DigitTachometerChannelModel model) : base(model)
    {
    }

    public System.Single Calibration
    {
      get { return GetValue<System.Single>(); }
      set { SetValue(value); }
    }

    public System.Single CalibrationOriginalValue => GetOriginalValue<System.Single>(nameof(Calibration));

    public bool CalibrationIsChanged => GetIsChanged(nameof(Calibration));

    public System.Boolean IsNotch
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }

    public System.Boolean IsNotchOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsNotch));

    public bool IsNotchIsChanged => GetIsChanged(nameof(IsNotch));

    public System.Single DefaultRPM
    {
      get { return GetValue<System.Single>(); }
      set { SetValue(value); }
    }

    public System.Single DefaultRPMOriginalValue => GetOriginalValue<System.Single>(nameof(DefaultRPM));

    public bool DefaultRPMIsChanged => GetIsChanged(nameof(DefaultRPM));

    public System.Single TeethNumber
    {
      get { return GetValue<System.Single>(); }
      set { SetValue(value); }
    }

    public System.Single TeethNumberOriginalValue => GetOriginalValue<System.Single>(nameof(TeethNumber));

    public bool TeethNumberIsChanged => GetIsChanged(nameof(TeethNumber));

    public System.Boolean IsTwoMAClamp
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }

    public System.Boolean IsTwoMAClampOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsTwoMAClamp));

    public bool IsTwoMAClampIsChanged => GetIsChanged(nameof(IsTwoMAClamp));

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
    
    protected override void InitializeComplexProperties(DigitTachometerChannelModel model)
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

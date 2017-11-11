using System;
using System.Linq;
using AIC.ModelWrapperGenerator;

namespace AIC.ModelWrapperGenerator
{
  public partial class EddyCurrentDisplacementChannelModelWrapper : ModelWrapper<EddyCurrentDisplacementChannelModel>
  {
    public EddyCurrentDisplacementChannelModelWrapper(EddyCurrentDisplacementChannelModel model) : base(model)
    {
    }

    public System.Int32 TPDir
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 TPDirOriginalValue => GetOriginalValue<System.Int32>(nameof(TPDir));

    public bool TPDirIsChanged => GetIsChanged(nameof(TPDir));

    public System.String ResultType
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String ResultTypeOriginalValue => GetOriginalValue<System.String>(nameof(ResultType));

    public bool ResultTypeIsChanged => GetIsChanged(nameof(ResultType));

    public System.Int32 ResultCode
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 ResultCodeOriginalValue => GetOriginalValue<System.Int32>(nameof(ResultCode));

    public bool ResultCodeIsChanged => GetIsChanged(nameof(ResultCode));

    public System.Single BiasVoltHigh
    {
      get { return GetValue<System.Single>(); }
      set { SetValue(value); }
    }

    public System.Single BiasVoltHighOriginalValue => GetOriginalValue<System.Single>(nameof(BiasVoltHigh));

    public bool BiasVoltHighIsChanged => GetIsChanged(nameof(BiasVoltHigh));

    public System.Single BiasVoltLow
    {
      get { return GetValue<System.Single>(); }
      set { SetValue(value); }
    }

    public System.Single BiasVoltLowOriginalValue => GetOriginalValue<System.Single>(nameof(BiasVoltLow));

    public bool BiasVoltLowIsChanged => GetIsChanged(nameof(BiasVoltLow));

    public System.Single Sensitivity
    {
      get { return GetValue<System.Single>(); }
      set { SetValue(value); }
    }

    public System.Single SensitivityOriginalValue => GetOriginalValue<System.Single>(nameof(Sensitivity));

    public bool SensitivityIsChanged => GetIsChanged(nameof(Sensitivity));

    public System.Boolean IsTwoMAClamp
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }

    public System.Boolean IsTwoMAClampOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsTwoMAClamp));

    public bool IsTwoMAClampIsChanged => GetIsChanged(nameof(IsTwoMAClamp));

    public System.Int32 RPMCardNum
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 RPMCardNumOriginalValue => GetOriginalValue<System.Int32>(nameof(RPMCardNum));

    public bool RPMCardNumIsChanged => GetIsChanged(nameof(RPMCardNum));

    public System.Int32 RPMChannelNum
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 RPMChannelNumOriginalValue => GetOriginalValue<System.Int32>(nameof(RPMChannelNum));

    public bool RPMChannelNumIsChanged => GetIsChanged(nameof(RPMChannelNum));

    public System.Int32 DelayAlarmTime
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 DelayAlarmTimeOriginalValue => GetOriginalValue<System.Int32>(nameof(DelayAlarmTime));

    public bool DelayAlarmTimeIsChanged => GetIsChanged(nameof(DelayAlarmTime));

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
    
    protected override void InitializeComplexProperties(EddyCurrentDisplacementChannelModel model)
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

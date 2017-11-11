using System;
using System.Linq;
using AIC.ModelWrapperGenerator;

namespace AIC.ModelWrapperGenerator
{
  public partial class IEPEChannelModelWrapper : ModelWrapper<IEPEChannelModel>
  {
    public IEPEChannelModelWrapper(IEPEChannelModel model) : base(model)
    {
    }

    public System.Int32 TPDir
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 TPDirOriginalValue => GetOriginalValue<System.Int32>(nameof(TPDir));

    public bool TPDirIsChanged => GetIsChanged(nameof(TPDir));

    public System.Int32 SVType
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 SVTypeOriginalValue => GetOriginalValue<System.Int32>(nameof(SVType));

    public bool SVTypeIsChanged => GetIsChanged(nameof(SVType));

    public System.Double BiasVoltHigh
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double BiasVoltHighOriginalValue => GetOriginalValue<System.Double>(nameof(BiasVoltHigh));

    public bool BiasVoltHighIsChanged => GetIsChanged(nameof(BiasVoltHigh));

    public System.Double BiasVoltLow
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double BiasVoltLowOriginalValue => GetOriginalValue<System.Double>(nameof(BiasVoltLow));

    public bool BiasVoltLowIsChanged => GetIsChanged(nameof(BiasVoltLow));

    public System.Double Sensitivity
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double SensitivityOriginalValue => GetOriginalValue<System.Double>(nameof(Sensitivity));

    public bool SensitivityIsChanged => GetIsChanged(nameof(Sensitivity));

    public System.Double VelocityCalibration
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double VelocityCalibrationOriginalValue => GetOriginalValue<System.Double>(nameof(VelocityCalibration));

    public bool VelocityCalibrationIsChanged => GetIsChanged(nameof(VelocityCalibration));

    public System.Double DisplacementCalibration
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double DisplacementCalibrationOriginalValue => GetOriginalValue<System.Double>(nameof(DisplacementCalibration));

    public bool DisplacementCalibrationIsChanged => GetIsChanged(nameof(DisplacementCalibration));

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
 
    public IEPEChannelWrapper Channel { get; private set; }
 
    public ChannelIdentityWrapper ChannelId { get; private set; }
 
    public ChangeTrackingCollection<DivFreWrapper> DivFres { get; private set; }
    
    protected override void InitializeComplexProperties(IEPEChannelModel model)
    {
      if (model.Channel == null)
      {
        throw new ArgumentException("Channel cannot be null");
      }
      Channel = new IEPEChannelWrapper(model.Channel);
      RegisterComplex(Channel);
      if (model.ChannelId == null)
      {
        throw new ArgumentException("ChannelId cannot be null");
      }
      ChannelId = new ChannelIdentityWrapper(model.ChannelId);
      RegisterComplex(ChannelId);
    }

    protected override void InitializeCollectionProperties(IEPEChannelModel model)
    {
      if (model.DivFres == null)
      {
        throw new ArgumentException("DivFres cannot be null");
      }
 
      DivFres = new ChangeTrackingCollection<DivFreWrapper>(
        model.DivFres.Select(e => new DivFreWrapper(e)));
      RegisterCollection(DivFres, model.DivFres);
    }
  }
}

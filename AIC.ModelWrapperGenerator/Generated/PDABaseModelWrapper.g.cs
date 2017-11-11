using System;
using System.Linq;
using AIC.ModelWrapperGenerator;

namespace AIC.ModelWrapperGenerator
{
  public partial class PDABaseModelWrapper : ModelWrapper<PDABaseModel>
  {
    public PDABaseModelWrapper(PDABaseModel model) : base(model)
    {
    }

    public System.String IP
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String IPOriginalValue => GetOriginalValue<System.String>(nameof(IP));

    public bool IPIsChanged => GetIsChanged(nameof(IP));

    public System.Boolean Is4G
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }

    public System.Boolean Is4GOriginalValue => GetOriginalValue<System.Boolean>(nameof(Is4G));

    public bool Is4GIsChanged => GetIsChanged(nameof(Is4G));

    public System.Boolean IsSyn
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }

    public System.Boolean IsSynOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsSyn));

    public bool IsSynIsChanged => GetIsChanged(nameof(IsSyn));

    public System.Boolean IsTotalBypass
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }

    public System.Boolean IsTotalBypassOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsTotalBypass));

    public bool IsTotalBypassIsChanged => GetIsChanged(nameof(IsTotalBypass));

    public System.Boolean IsZipDownload
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }

    public System.Boolean IsZipDownloadOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsZipDownload));

    public bool IsZipDownloadIsChanged => GetIsChanged(nameof(IsZipDownload));

    public System.Boolean IsZipUpload
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }

    public System.Boolean IsZipUploadOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsZipUpload));

    public bool IsZipUploadIsChanged => GetIsChanged(nameof(IsZipUpload));

    public System.Int32 Language
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 LanguageOriginalValue => GetOriginalValue<System.Int32>(nameof(Language));

    public bool LanguageIsChanged => GetIsChanged(nameof(Language));

    public System.String MasterWirelessMAC
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String MasterWirelessMACOriginalValue => GetOriginalValue<System.String>(nameof(MasterWirelessMAC));

    public bool MasterWirelessMACIsChanged => GetIsChanged(nameof(MasterWirelessMAC));

    public System.String PDAAliasName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String PDAAliasNameOriginalValue => GetOriginalValue<System.String>(nameof(PDAAliasName));

    public bool PDAAliasNameIsChanged => GetIsChanged(nameof(PDAAliasName));

    public System.String PDAMAC
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String PDAMACOriginalValue => GetOriginalValue<System.String>(nameof(PDAMAC));

    public bool PDAMACIsChanged => GetIsChanged(nameof(PDAMAC));

    public System.Double SampleFre
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double SampleFreOriginalValue => GetOriginalValue<System.Double>(nameof(SampleFre));

    public bool SampleFreIsChanged => GetIsChanged(nameof(SampleFre));

    public System.Int32 SamplePoint
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 SamplePointOriginalValue => GetOriginalValue<System.Int32>(nameof(SamplePoint));

    public bool SamplePointIsChanged => GetIsChanged(nameof(SamplePoint));

    public AIC.CoreType.SampleType SampleType
    {
      get { return GetValue<AIC.CoreType.SampleType>(); }
      set { SetValue(value); }
    }

    public AIC.CoreType.SampleType SampleTypeOriginalValue => GetOriginalValue<AIC.CoreType.SampleType>(nameof(SampleType));

    public bool SampleTypeIsChanged => GetIsChanged(nameof(SampleType));

    public System.String ServerIP
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String ServerIPOriginalValue => GetOriginalValue<System.String>(nameof(ServerIP));

    public bool ServerIPIsChanged => GetIsChanged(nameof(ServerIP));

    public System.Int32 ServerPort
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 ServerPortOriginalValue => GetOriginalValue<System.Int32>(nameof(ServerPort));

    public bool ServerPortIsChanged => GetIsChanged(nameof(ServerPort));

    public System.Int32 TriggerCardNum
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 TriggerCardNumOriginalValue => GetOriginalValue<System.Int32>(nameof(TriggerCardNum));

    public bool TriggerCardNumIsChanged => GetIsChanged(nameof(TriggerCardNum));

    public System.Int32 TriggerChannelNum
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 TriggerChannelNumOriginalValue => GetOriginalValue<System.Int32>(nameof(TriggerChannelNum));

    public bool TriggerChannelNumIsChanged => GetIsChanged(nameof(TriggerChannelNum));

    public AIC.CoreType.TriggerType TriggerType
    {
      get { return GetValue<AIC.CoreType.TriggerType>(); }
      set { SetValue(value); }
    }

    public AIC.CoreType.TriggerType TriggerTypeOriginalValue => GetOriginalValue<AIC.CoreType.TriggerType>(nameof(TriggerType));

    public bool TriggerTypeIsChanged => GetIsChanged(nameof(TriggerType));

    public System.Int32 UploadMode
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 UploadModeOriginalValue => GetOriginalValue<System.Int32>(nameof(UploadMode));

    public bool UploadModeIsChanged => GetIsChanged(nameof(UploadMode));

    public System.Int32 Count
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 CountOriginalValue => GetOriginalValue<System.Int32>(nameof(Count));

    public bool CountIsChanged => GetIsChanged(nameof(Count));

    public System.Int32 TriggerCount
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 TriggerCountOriginalValue => GetOriginalValue<System.Int32>(nameof(TriggerCount));

    public bool TriggerCountIsChanged => GetIsChanged(nameof(TriggerCount));
 
    public PDABaseWrapper PDA { get; private set; }
 
    public ChangeTrackingCollection<TriggerChannelWrapper> TriggerChannels { get; private set; }
 
    public ChangeTrackingCollection<BaseCardModelWrapper> Cards { get; private set; }
    
    protected override void InitializeComplexProperties(PDABaseModel model)
    {
      if (model.PDA == null)
      {
        throw new ArgumentException("PDA cannot be null");
      }
      PDA = new PDABaseWrapper(model.PDA);
      RegisterComplex(PDA);
    }

    protected override void InitializeCollectionProperties(PDABaseModel model)
    {
      if (model.TriggerChannels == null)
      {
        throw new ArgumentException("TriggerChannels cannot be null");
      }
 
      TriggerChannels = new ChangeTrackingCollection<TriggerChannelWrapper>(
        model.TriggerChannels.Select(e => new TriggerChannelWrapper(e)));
      RegisterCollection(TriggerChannels, model.TriggerChannels);
      if (model.Cards == null)
      {
        throw new ArgumentException("Cards cannot be null");
      }
 
      Cards = new ChangeTrackingCollection<BaseCardModelWrapper>(
        model.Cards.Select(e => new BaseCardModelWrapper(e)));
      RegisterCollection(Cards, model.Cards);
    }
  }
}

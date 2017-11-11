using System;
using System.Linq;
using AIC.ModelWrapperGenerator;

namespace AIC.ModelWrapperGenerator
{
  public partial class EddyCurrentDisplacementCardModelWrapper : ModelWrapper<EddyCurrentDisplacementCardModel>
  {
    public EddyCurrentDisplacementCardModelWrapper(EddyCurrentDisplacementCardModel model) : base(model)
    {
    }

    public System.String CardName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String CardNameOriginalValue => GetOriginalValue<System.String>(nameof(CardName));

    public bool CardNameIsChanged => GetIsChanged(nameof(CardName));

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

    public AIC.CoreType.HP HighPass
    {
      get { return GetValue<AIC.CoreType.HP>(); }
      set { SetValue(value); }
    }

    public AIC.CoreType.HP HighPassOriginalValue => GetOriginalValue<AIC.CoreType.HP>(nameof(HighPass));

    public bool HighPassIsChanged => GetIsChanged(nameof(HighPass));

    public AIC.CoreType.TriggerType TriggerType
    {
      get { return GetValue<AIC.CoreType.TriggerType>(); }
      set { SetValue(value); }
    }

    public AIC.CoreType.TriggerType TriggerTypeOriginalValue => GetOriginalValue<AIC.CoreType.TriggerType>(nameof(TriggerType));

    public bool TriggerTypeIsChanged => GetIsChanged(nameof(TriggerType));

    public System.String TriggerChannelNum
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String TriggerChannelNumOriginalValue => GetOriginalValue<System.String>(nameof(TriggerChannelNum));

    public bool TriggerChannelNumIsChanged => GetIsChanged(nameof(TriggerChannelNum));

    public System.String TriggerCardNum
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String TriggerCardNumOriginalValue => GetOriginalValue<System.String>(nameof(TriggerCardNum));

    public bool TriggerCardNumIsChanged => GetIsChanged(nameof(TriggerCardNum));

    public System.Int32 UploadIntevalTime
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 UploadIntevalTimeOriginalValue => GetOriginalValue<System.Int32>(nameof(UploadIntevalTime));

    public bool UploadIntevalTimeIsChanged => GetIsChanged(nameof(UploadIntevalTime));

    public System.Int32 Cycles
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 CyclesOriginalValue => GetOriginalValue<System.Int32>(nameof(Cycles));

    public bool CyclesIsChanged => GetIsChanged(nameof(Cycles));

    public System.Boolean Is24V
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }

    public System.Boolean Is24VOriginalValue => GetOriginalValue<System.Boolean>(nameof(Is24V));

    public bool Is24VIsChanged => GetIsChanged(nameof(Is24V));

    public System.Int32 Count
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 CountOriginalValue => GetOriginalValue<System.Int32>(nameof(Count));

    public bool CountIsChanged => GetIsChanged(nameof(Count));
 
    public EddyCurrentDisplacementCardWrapper Card { get; private set; }
 
    public InSignalCategoryWrapper InSignalCode { get; private set; }
 
    public CardIdentityWrapper CardId { get; private set; }
 
    public ChangeTrackingCollection<InSignalCategoryWrapper> InSignalCategories { get; private set; }
 
    public ChangeTrackingCollection<BaseChannelModelWrapper> Channels { get; private set; }
    
    protected override void InitializeComplexProperties(EddyCurrentDisplacementCardModel model)
    {
      if (model.Card == null)
      {
        throw new ArgumentException("Card cannot be null");
      }
      Card = new EddyCurrentDisplacementCardWrapper(model.Card);
      RegisterComplex(Card);
      if (model.InSignalCode == null)
      {
        throw new ArgumentException("InSignalCode cannot be null");
      }
      InSignalCode = new InSignalCategoryWrapper(model.InSignalCode);
      RegisterComplex(InSignalCode);
      if (model.CardId == null)
      {
        throw new ArgumentException("CardId cannot be null");
      }
      CardId = new CardIdentityWrapper(model.CardId);
      RegisterComplex(CardId);
    }

    protected override void InitializeCollectionProperties(EddyCurrentDisplacementCardModel model)
    {
      if (model.InSignalCategories == null)
      {
        throw new ArgumentException("InSignalCategories cannot be null");
      }
 
      InSignalCategories = new ChangeTrackingCollection<InSignalCategoryWrapper>(
        model.InSignalCategories.Select(e => new InSignalCategoryWrapper(e)));
      RegisterCollection(InSignalCategories, model.InSignalCategories);
      if (model.Channels == null)
      {
        throw new ArgumentException("Channels cannot be null");
      }
 
      Channels = new ChangeTrackingCollection<BaseChannelModelWrapper>(
        model.Channels.Select(e => new BaseChannelModelWrapper(e)));
      RegisterCollection(Channels, model.Channels);
    }
  }
}

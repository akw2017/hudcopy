using System;
using System.Linq;
using AIC.ModelWrapperGenerator;

namespace AIC.ModelWrapperGenerator
{
  public partial class NullCardWrapper : ModelWrapper<NullCard>
  {
    public NullCardWrapper(NullCard model) : base(model)
    {
    }

    public System.Int32 Count
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 CountOriginalValue => GetOriginalValue<System.Int32>(nameof(Count));

    public bool CountIsChanged => GetIsChanged(nameof(Count));
 
    public CardIdentityWrapper CardId { get; private set; }
 
    public ChangeTrackingCollection<BaseChannelModelWrapper> Channels { get; private set; }
    
    protected override void InitializeComplexProperties(NullCard model)
    {
      if (model.CardId == null)
      {
        throw new ArgumentException("CardId cannot be null");
      }
      CardId = new CardIdentityWrapper(model.CardId);
      RegisterComplex(CardId);
    }

    protected override void InitializeCollectionProperties(NullCard model)
    {
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

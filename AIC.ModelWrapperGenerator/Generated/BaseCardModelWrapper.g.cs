using System;
using System.Linq;
using AIC.ModelWrapperGenerator;

namespace AIC.ModelWrapperGenerator
{
  public partial class BaseCardModelWrapper : ModelWrapper<BaseCardModel>
  {
    public BaseCardModelWrapper(BaseCardModel model) : base(model)
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
 
    public NullCardWrapper Null { get; private set; }
 
    public ChangeTrackingCollection<BaseChannelModelWrapper> Channels { get; private set; }
    
    protected override void InitializeComplexProperties(BaseCardModel model)
    {
      if (model.CardId == null)
      {
        throw new ArgumentException("CardId cannot be null");
      }
      CardId = new CardIdentityWrapper(model.CardId);
      RegisterComplex(CardId);
      if (model.Null == null)
      {
        throw new ArgumentException("Null cannot be null");
      }
      Null = new NullCardWrapper(model.Null);
      RegisterComplex(Null);
    }

    protected override void InitializeCollectionProperties(BaseCardModel model)
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

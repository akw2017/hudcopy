using System.ComponentModel;

namespace AIC.Domain
{
    public interface IValidatableTrackingObject :
    IRevertibleChangeTracking,
    INotifyPropertyChanged
  {
    bool IsValid { get; }
  }
}

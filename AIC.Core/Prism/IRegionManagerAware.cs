

using Prism.Regions;

namespace AIC.Core
{
    public interface IRegionManagerAware
    {
        IRegionManager RegionManager { get; set; }
    }
}

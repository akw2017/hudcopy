using System;
using System.Linq;
using AIC.ModelWrapperGenerator;

namespace AIC.ModelWrapperGenerator
{
  public partial class WrapperGeneratorWrapper : ModelWrapper<WrapperGenerator>
  {
    public WrapperGeneratorWrapper(WrapperGenerator model) : base(model)
    {
    }
  }
}

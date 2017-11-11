using AIC.Core.SignalModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.OnLineDataPage.Controllers
{
    public interface IPropertyController
    {
        DelegateCommand<BaseWaveSignal> EditCommand { get; }
    }
}

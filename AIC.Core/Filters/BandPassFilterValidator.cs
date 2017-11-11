using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Filters
{
    public class BandPassFilterValidator : AbstractValidator<BandPassFilter>
    {
        public BandPassFilterValidator()
        {
            RuleFor(bpf => bpf.TransitionBandwidth).GreaterThan(0).WithMessage("过渡带宽应大于0");
            RuleFor(bpf => bpf.BPStopBandFreLow).GreaterThan(0).WithMessage("低阻带频率应大于0");
            RuleFor(bpf => bpf.BPStopBandFreLow).LessThan(bpf => bpf.BPPassBandFreLow).WithMessage("低阻带频率应小于低逼近通带频率");
            RuleFor(bpf => bpf.BPPassBandFreLow).LessThan(bpf => bpf.BPPassBandFreHigh).WithMessage("低逼近通带频率应小于高逼近通带频率");
            RuleFor(bpf => bpf.BPPassBandFreHigh).LessThan(bpf => bpf.BPStopBandFreHigh).WithMessage("高逼近通带频率应小于高阻带频率");

        }
    }
}

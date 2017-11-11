using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Filters
{
    public class LowPassFilterValidator : AbstractValidator<LowPassFilter>
    {
        public LowPassFilterValidator()
        {
            RuleFor(bpf => bpf.PassbandFre).GreaterThan(0).WithMessage("通带频率应大于0");
            RuleFor(bpf => bpf.PassbandFre).LessThan(bpf => bpf.StopbandFre).WithMessage("通带频率应小于阻带频率");
        }
    }
}

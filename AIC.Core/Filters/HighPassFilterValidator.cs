using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Filters
{
    public class HighPassFilterValidator : AbstractValidator<HighPassFilter>
    {
        public HighPassFilterValidator()
        {
            RuleFor(bpf => bpf.StopbandFre).GreaterThan(0).WithMessage("阻带频率应大于0");
            RuleFor(bpf => bpf.StopbandFre).LessThan(bpf => bpf.PassbandFre).WithMessage("阻带频率应小于通带频率");
        }
    }
}

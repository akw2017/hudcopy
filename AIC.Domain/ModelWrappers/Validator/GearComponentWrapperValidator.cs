using FluentValidation;

namespace AIC.Domain
{

    public class GearComponentWrapperValidator : AbstractValidator<GearComponentWrapper>
    {
        public GearComponentWrapperValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(belt => belt.Name).NotEmpty().WithMessage("名字不能为空").NotNull().WithMessage("名字不能为空");
            RuleFor(belt => belt.Component).NotNull().WithMessage("齿轮类型不能为空");
        }
    }
}

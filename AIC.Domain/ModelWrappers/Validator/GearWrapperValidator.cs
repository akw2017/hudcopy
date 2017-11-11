using FluentValidation;

namespace AIC.Domain
{

    public class GearWrapperValidator : AbstractValidator<GearWrapper>
    {
        public GearWrapperValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            //RuleFor(gear => gear.Name).NotEmpty().WithMessage("名字不能为空").NotNull().WithMessage("名字不能为空");
            RuleFor(gear => gear.TeethNumber).GreaterThan(0).WithMessage("齿数应大于0");
        }
    }
}

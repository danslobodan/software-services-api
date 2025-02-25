using Application.SoftwareLicenses.Commands;
using FluentValidation;

namespace Application.SoftwareLicenses.Validators;

public class CancelSubscriptionValidator : AbstractValidator<CancelSubscription.Command>
{
    public CancelSubscriptionValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}

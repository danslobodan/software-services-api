using Application.SoftwareLicenses.DTOs;
using FluentValidation;

namespace Application.SoftwareLicenses.Validators;

public class PurchasedSoftwareLicenseValidator<T, TDto> : AbstractValidator<T>
    where TDto : PurchasedSoftwareLicenseDto
{
    public PurchasedSoftwareLicenseValidator(Func<T, TDto> selector)
    {
        RuleFor(x => selector(x).Id)
            .NotEmpty().WithMessage("Id is required.");
        RuleFor(x => selector(x).AccountId)
            .NotEmpty().WithMessage("AccountId is required.");
    }
}

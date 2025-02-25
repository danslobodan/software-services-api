using Application.SoftwareLicenses.Commands;
using FluentValidation;

namespace Application.SoftwareServices.Validators;

public class PurchaseSoftwareLicenseValidator : AbstractValidator<PurchaseSoftwareLicense.Command>
{
    public PurchaseSoftwareLicenseValidator()
    {
        RuleFor(x => x.Dto.AccountId)
            .NotEmpty().WithMessage("AccountId is required.");
        RuleFor(x => x.Dto.DurationMonths)
            .NotEmpty().WithMessage("Duration is required.")
            .GreaterThan(0).WithMessage("Number of months must be a positive number.");
        RuleFor(x => x.Dto.Name)
            .NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Dto.Quantity)
            .NotEmpty().WithMessage("Quantity is required.")
            .GreaterThan(0).WithMessage("Quantity must be a positive number.");
        RuleFor(x => x.Dto.SoftwareId)
            .NotEmpty().WithMessage("SoftwareId is required.");
    }
}

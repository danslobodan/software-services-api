using Application.SoftwareServices.Commands;
using FluentValidation;

namespace Application.SoftwareLicenses.Validators;

public class ExtendLicenseValidator : AbstractValidator<ExtendLicense.Command>
{
    public ExtendLicenseValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Dto.DurationMonths)
            .NotEmpty().WithMessage("DurationMonths is required.")
            .GreaterThan(0).WithMessage("Duration is in months and must be 1 or more.");
    }
}

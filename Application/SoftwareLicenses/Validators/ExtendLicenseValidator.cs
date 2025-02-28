using Application.SoftwareLicenses.Commands;
using FluentValidation;

namespace Application.SoftwareLicenses.Validators;

public class ExtendLicenseValidator : AbstractValidator<ExtendLicense.Command>
{
    public ExtendLicenseValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Dto.ValidTo)
            .NotEmpty().WithMessage("ValidTo is required.")
            .GreaterThan(DateTime.Now).WithMessage("The new ValidTo date must be in the future.");
    }
}

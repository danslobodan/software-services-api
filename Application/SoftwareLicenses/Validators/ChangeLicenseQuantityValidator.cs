using System;
using Application.SoftwareServices.Commands;
using Application.SoftwareServices.DTOs;
using FluentValidation;

namespace Application.SoftwareLicenses.Validators;

public class ChangeLicenseQuantityValidator : AbstractValidator<ChangeLicenseQuantity.Command>
{
    public ChangeLicenseQuantityValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Dto.Quantity)
            .NotEmpty().WithMessage("Quantity is required.")
            .GreaterThan(0).WithMessage("Quantity must be a positive number.");
    }
}

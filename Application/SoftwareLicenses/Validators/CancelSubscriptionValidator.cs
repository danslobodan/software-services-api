using Application.SoftwareServices.Commands;
using Application.SoftwareServices.DTOs;

namespace Application.SoftwareLicenses.Validators;

public class CancelSubscriptionValidator 
    : PurchasedSoftwareLicenseValidator<CancelSubscription.Command, CancelSubscriptionDto>
{
    public CancelSubscriptionValidator() : base(x => x.Dto) {}
}

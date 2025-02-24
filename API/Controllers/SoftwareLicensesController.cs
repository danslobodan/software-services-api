using Application.SoftwareLicenses.DTOs;
using Application.SoftwareServices.Commands;
using Application.SoftwareServices.DTOs;
using Application.SoftwareServices.Queries;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SoftwareLicensesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<SoftwareLicense>>> GetLicensesForAccount([FromQuery] string accountId) {
        return HandleResult(await Mediator.Send(new GetSoftwareLicenses.Query{ AccountId = accountId }));
    }

    [HttpPost]
    public async Task<ActionResult<string>> PurchaseLicense(PurchaseSoftwareLicenseDto dto) {
        return HandleResult(await Mediator.Send(new PurchaseSoftwareLicense.Command{ Dto = dto }));
    }

    [HttpPatch("status")]
    public async Task<ActionResult> CancelSubscription(CancelSubscriptionDto dto) {
        return HandleResult(await Mediator.Send(new CancelSubscription.Command{ Dto = dto }));
    }

    [HttpPatch("quantity")]
    public async Task<ActionResult> ChangeQuantity(ChangeLicenseQuantityDto dto) {
        return HandleResult(await Mediator.Send(new ChangeLicenseQuantity.Command{ Dto = dto }));
    }

    [HttpPatch("valid-to")]
    public async Task<ActionResult> ExtendLicense(ExtendLicenseDto dto) {
        return HandleResult(await Mediator.Send(new ExtendLicense.Command { Dto = dto }));
    }
}

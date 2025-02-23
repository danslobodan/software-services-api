using Application.SoftwareServices.Commands;
using Application.SoftwareServices.DTOs;
using Application.SoftwareServices.Queries;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SoftwareLicenseController : BaseApiController
{
    [HttpGet("account/{accountId}")]
    public async Task<ActionResult<List<SoftwareLicense>>> GetLicensesForAccount(string accountId) {
        return HandleResult(await Mediator.Send(new GetSoftwareLicenses.Query{ AccountId = accountId }));
    }

    [HttpPost]
    public async Task<ActionResult<string>> PurchaseLicense(PurchaseSoftwareLicenseDto dto) {
        return HandleResult(await Mediator.Send(new PurchaseSoftwareLicense.Command{ Dto = dto }));
    }

    [HttpPut("/status")]
    public async Task<ActionResult> CancelSubscription(CancelSubscriptionDto dto) {
        return HandleResult(await Mediator.Send(new CancelSubscription.Command{ Dto = dto }));
    }

    [HttpPut("/quantity")]
    public async Task<ActionResult> ChangeQuantity(ChangeLicenseQuantityDto dto) {
        return HandleResult(await Mediator.Send(new ChangeLicenseQuantity.Command{ Dto = dto }));
    }
}

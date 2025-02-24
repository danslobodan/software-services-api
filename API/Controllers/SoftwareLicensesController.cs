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

    [HttpPatch("{id}/cancel")]
    public async Task<ActionResult> CancelSubscription(string Id) {
        return HandleResult(await Mediator.Send(new CancelSubscription.Command{ Id = Id }));
    }

    [HttpPatch("{id}/quantity")]
    public async Task<ActionResult> ChangeQuantity(string Id, ChangeLicenseQuantityDto dto) {
        return HandleResult(await Mediator.Send(new ChangeLicenseQuantity.Command{ Id = Id, Dto = dto }));
    }

    [HttpPatch("{id}/extend")]
    public async Task<ActionResult> ExtendLicense(string Id, ExtendLicenseDto dto) {
        return HandleResult(await Mediator.Send(new ExtendLicense.Command { Id = Id, Dto = dto }));
    }
}

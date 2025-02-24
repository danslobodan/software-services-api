using Application.SoftwareServices.Queries;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountsController : BaseApiController
{
    [HttpGet("{id}/software-licenses")]
    public async Task<ActionResult<List<SoftwareLicense>>> GetSoftwareLicenses(string id) {
        return HandleResult(await Mediator.Send(new GetSoftwareLicenses.Query{ AccountId = id }));
    }
}

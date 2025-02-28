using Application.Accounts.Queries;
using Application.SoftwareLicenses.Queries;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Account>>> GetList() {
        return await Mediator.Send(new GetAccounts.Query{});
    }

    [HttpGet("{id}/software-licenses")]
    public async Task<ActionResult<List<SoftwareLicense>>> GetSoftwareLicenses(string id) {
        return HandleResult(await Mediator.Send(new GetSoftwareLicenses.Query{ AccountId = id }));
    }
}

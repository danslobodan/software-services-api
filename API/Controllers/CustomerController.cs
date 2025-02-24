using Application.Accounts.Queries;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CustomerController : BaseApiController
{
    [HttpGet("{id}/accounts")]
    public async Task<ActionResult<List<Account>>> GetAccountList(string id) {
        return await Mediator.Send(new GetAccountsList.Query{ CustomerId = id });
    }
}

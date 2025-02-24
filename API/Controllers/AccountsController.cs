using Application.Accounts.Queries;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Account>>> GetAccountList([FromQuery] string customerId) {
        return await Mediator.Send(new GetAccountsList.Query{ CustomerId = customerId });
    }
}

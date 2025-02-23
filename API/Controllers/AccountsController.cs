using Application.Accounts.Queries;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountsController : BaseApiController
{
    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<List<Account>>> GetAccountList(string customerId) {
        return await Mediator.Send(new GetAccountsList.Query{ CustomerId = customerId });
    }
}

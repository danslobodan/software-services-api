using Application.Accounts.Queries;
using Application.Customers.Queries;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CustomersController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Customer>>> GetCustomers() {
        return await Mediator.Send(new GetCustomers.Query());
    }

    [HttpGet("{id}/accounts")]
    public async Task<ActionResult<List<Account>>> GetAccountList(string id) {
        return await Mediator.Send(new GetAccountsList.Query{ CustomerId = id });
    }
}

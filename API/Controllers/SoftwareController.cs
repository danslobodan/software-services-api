using Application.Software.DTOs;
using Application.Software.Queries;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SoftwareController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Software>>> GetSoftware([FromQuery] GetSoftwareDto dto) {
        return HandleResult(await Mediator.Send(new GetSoftware.Query{ Dto = dto }));
    }
}

using CcpMock.Models;
using Microsoft.AspNetCore.Mvc;

namespace CcpMock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftwareController : ControllerBase
    {
        [HttpGet]
        public List<Software> GetList([FromQuery] GetSoftwareDto query) {
            var page = query.Page > 0 ? query.Page : 0;
            var pageSize = query.PageSize > 0 ? query.PageSize : 20;

            return [.. new List<Software> {
                new () {
                    Id = "1",
                    Name = "Microsoft Word"
                },
                new () {
                    Id = "2",
                    Name = "Adobe Photoshop",
                },
                new () {
                    Id = "3",
                    Name = "Autodesk 3d Studio Max"
                }
            }.Skip((page - 1) * pageSize)
            .Take(pageSize)];
        }

        [HttpPost]
        public ActionResult OrderSoftware(OrderSoftwareDto dto) {
            return Ok();
        }
    }
}

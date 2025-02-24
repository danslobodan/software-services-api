using CcpMock.Models;
using Microsoft.AspNetCore.Mvc;

namespace CcpMock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftwareController : ControllerBase
    {
        [HttpGet]
        public List<Software> GetList() {
            return [
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
            ];
        }

        [HttpPost]
        public ActionResult OrderSoftware(OrderSoftwareDto dto) {
            return Ok();
        }
    }
}

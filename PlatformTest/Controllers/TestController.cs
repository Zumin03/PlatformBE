using Microsoft.AspNetCore.Mvc;

namespace PlatformTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello Swagger!");
        }
    }
}

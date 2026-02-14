using Microsoft.AspNetCore.Mvc;
using PlatformTest.Model;
using PlatformTest.Service;

namespace PlatformTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstrumentController : ControllerBase
    {
        private readonly IInstrumentService instrumentService;
        private readonly ILogger<InstrumentController> logger;

        public InstrumentController(
            IInstrumentService instrumentService,
            ILogger<InstrumentController> logger)
        {
            this.instrumentService = instrumentService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstrumentDTO>>> GetInstruments()
        {
            var instruments = await instrumentService.GetInstrumentsAsync();
            return Ok(instruments);
        }
    }
}

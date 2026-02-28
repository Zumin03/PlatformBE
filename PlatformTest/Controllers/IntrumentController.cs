using Microsoft.AspNetCore.Mvc;
using PlatformTest.Exceptions;
using PlatformTest.Model;
using PlatformTest.Service;
using System.Diagnostics.Metrics;

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

        [HttpPost("{id}/self-test")]
        public async Task<ActionResult<InstrumentDTO>> SelfTest(string id)
        {
            try
            {
                var result = await instrumentService.RunSelfTest(id);
                return Ok(result);
            }
            catch (InstrumentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

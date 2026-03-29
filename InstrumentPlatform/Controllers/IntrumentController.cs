using InstrumentPlatform.Exceptions;
using InstrumentPlatform.Model;
using InstrumentPlatform.Service;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentPlatform.Controllers
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

        [HttpGet("/instrument/{id}")]
        public async Task<ActionResult<InstrumentDTO>> GetInstrument(string id)
        {
            var instrument = await instrumentService.GetInstrumentAsync(id);

            return Ok(instrument);
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

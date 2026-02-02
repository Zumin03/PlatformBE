using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlatformTest.Exceptions;
using PlatformTest.Model;
using PlatformTest.Service;

namespace PlatformTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService measurementService;
        private readonly ILogger<MeasurementController> logger;

        public MeasurementController(
            IMeasurementService measurementService,
            ILogger<MeasurementController> logger)
        {
            this.measurementService = measurementService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MeasurementRequestDTO request)
        {
            try
            {
                var result = await measurementService.RunMeasurementAsync(request.DeviceId);
                return Ok(result);
            }
            catch (InstrumentNotFoundException ex)
            {
                logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (DeserializationException ex)
            {
                logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeasurementDTO>>> GetMeasurements()
        {
            var measurements = await measurementService.GetMeasurementsAsync();
            return Ok(measurements);
        }
    }
}

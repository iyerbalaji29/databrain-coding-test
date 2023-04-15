using DataBrain.PAYG.Exceptions;
using DataBrain.PAYG.Service.Constants;
using DataBrain.PAYG.Service.Services;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace DataBrain.PAYG.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    /// <summary>
    ///     Contains endpoints that consumes PAYG API for fetching tax calculations.
    /// </summary>
    public class PAYGController : Controller
    {
        private readonly IPAYGService _service;
        private readonly ILogger _logger;
        public PAYGController(IPAYGService service, ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        ///     Executes operation for calculating PAYG Tax for provided earnings and frequency.
        /// </summary>
        /// <param name="earnings">Earnings for the provided period</param>
        /// <param name="frequency">Provides the payment frequency type for the provided earnings, Could be (0)Weekly, (1)Fortnightly, (2)Monthly or (3)FourWeekly</param>
        /// <returns>Calculated PAYG Tax for provided period</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get(float earnings, PaymentFrequency frequency)
        {
            try
            {
                var calculatedTax = _service.GetTax(earnings, frequency);
                return Ok(calculatedTax);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("An unexpected error occurred", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

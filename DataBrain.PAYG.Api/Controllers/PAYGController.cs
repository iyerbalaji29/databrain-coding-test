using DataBrain.PAYG.Exceptions;
using DataBrain.PAYG.Service.Constants;
using DataBrain.PAYG.Service.Services;
using Microsoft.AspNetCore.Mvc;

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
        private readonly ILogger<PAYGController> _logger;
        public PAYGController(IPAYGService service, ILogger<PAYGController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        ///     Executes operation for calculating PAYG Tax for provided earnings and frequency.
        /// </summary>
        /// <param name="earnings">Income earned during the period</param>
        /// <param name="frequency">Payment Frequency for the income earned during the period</param>
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
                _logger.LogError("An unexpected error occurred", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

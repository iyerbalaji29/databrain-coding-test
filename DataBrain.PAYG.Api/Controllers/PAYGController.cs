using DataBrain.PAYG.Service.Constants;
using DataBrain.PAYG.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataBrain.PAYG.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PAYGController : Controller
    {
        private readonly IPAYGService _service;
        public PAYGController(IPAYGService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get(float earnings, PaymentFrequency frequency)
        {
            var calculatedTax = _service.GetTax(earnings, frequency);
            return Ok(calculatedTax);
        }
    }
}

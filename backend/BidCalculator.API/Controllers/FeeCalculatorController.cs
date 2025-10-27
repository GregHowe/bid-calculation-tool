using Microsoft.AspNetCore.Mvc;
using BidCalculator.API.Interfaces;
using BidCalculator.API.Models;

namespace BidCalculator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeeCalculatorController : ControllerBase
    {
        private readonly IFeeCalculator _feeCalculator;

        public FeeCalculatorController(IFeeCalculator feeCalculator)
        {
            _feeCalculator = feeCalculator;
        }

        [HttpPost]
        public ActionResult<FeeResult> Calculate([FromBody] VehicleInput input)
        {
            if (input == null || input.Price <= 0)
                return BadRequest("Invalid input");

            var result = _feeCalculator.CalculateFees(input);
            return Ok(result);
        }
    }
}

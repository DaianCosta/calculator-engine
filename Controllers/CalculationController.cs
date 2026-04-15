using CalculatorEngine.Domain.Models;
using CalculatorEngine.Engine;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorEngine.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class CalculationController : ControllerBase
    {
        private readonly CalculationEngine _engine;

        public CalculationController(CalculationEngine engine)
        {
            _engine = engine;
        }

        [HttpPost]
        public IActionResult Calculate([FromBody] CalculationRequest request)
        {
            var result = _engine.Execute(request.Input, request.Config);

            return Ok(new
            {
                valorFinal = result.CurrentValue,
                memoria = result.Memory
            });
        }
    }

    public class CalculationRequest
    {
        public CalculationInput Input { get; set; }
        public CalculationConfig Config { get; set; }
    }
}

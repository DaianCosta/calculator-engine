using System.Text.Json;
using CalculatorEngine.Domain.Models;

namespace CalculatorEngine.Engine
{

    //Multa
    public class FineStep : ICalculationStep
    {
        public string Type => "multa";

        public void Execute(CalculationContext context, JsonElement parameters)
        {
            var percentual = parameters.GetProperty("percentual").GetDecimal();

            var previous = context.CurrentValue;
            var multa = previous * percentual;

            context.CurrentValue += multa;

            context.Memory.Add(new CalculationMemory
            {
                StepName = "Multa",
                Description = $"{percentual:P}",
                PreviousValue = previous,
                NewValue = context.CurrentValue
            });
        }
    }
}

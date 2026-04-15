namespace CalculatorEngine.Engine
{
    using System.Text.Json;
    using CalculatorEngine.Domain.Models;

    public class ChargesStep : ICalculationStep
    {
        public string Type => "encargos";

        public void Execute(CalculationContext context, JsonElement parameters)
        {
            var valor = parameters.GetProperty("valor").GetDecimal();

            var previous = context.CurrentValue;

            context.CurrentValue += valor;

            context.Memory.Add(new CalculationMemory
            {
                StepName = "Encargos",
                Description = $"Valor fixo",
                PreviousValue = previous,
                NewValue = context.CurrentValue
            });
        }
    }
}

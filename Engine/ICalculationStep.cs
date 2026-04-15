namespace CalculatorEngine.Engine
{
    using System.Text.Json;
    using CalculatorEngine.Domain.Models;

    public interface ICalculationStep
    {
        string Type { get; }
        void Execute(CalculationContext context, JsonElement parameters);
    }
}

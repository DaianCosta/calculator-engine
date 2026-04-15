namespace CalculatorEngine.Domain.Models
{
    using System.Text.Json;

    public class CalculationConfig
    {
        public List<StepConfig> Steps { get; set; }
    }

    public class StepConfig
    {
        public string Type { get; set; }
        public JsonElement Params { get; set; }
    }
}

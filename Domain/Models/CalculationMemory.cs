namespace CalculatorEngine.Domain.Models
{
    public class CalculationMemory
    {
        public string StepName { get; set; }
        public string Description { get; set; }

        public decimal PreviousValue { get; set; }
        public decimal NewValue { get; set; }
        public Dictionary<string, object> Details { get; set; } = new();
    }
}

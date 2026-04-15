namespace CalculatorEngine.Domain.Models
{
    public class CalculationContext
    {
        public CalculationInput Input { get; }
        public decimal CurrentValue { get; set; }

        public List<CalculationMemory> Memory { get; } = new();

        public CalculationContext(CalculationInput input)
        {
            Input = input;
            CurrentValue = input.ValorOriginal;
        }
    }
}

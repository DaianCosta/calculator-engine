namespace CalculatorEngine.Engine
{
    public class StepRegistry
    {
        private readonly Dictionary<string, ICalculationStep> _steps;

        public StepRegistry(IEnumerable<ICalculationStep> steps)
        {
            _steps = steps.ToDictionary(s => s.Type, s => s);
        }

        public ICalculationStep Get(string type)
        {
            if (!_steps.ContainsKey(type))
                throw new Exception($"Step não encontrado: {type}");

            return _steps[type];
        }
    }
}

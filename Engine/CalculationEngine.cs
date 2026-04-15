using CalculatorEngine.Domain.Models;

namespace CalculatorEngine.Engine
{
    public class CalculationEngine
    {
        private readonly StepRegistry _registry;

        public CalculationEngine(StepRegistry registry)
        {
            _registry = registry;
        }

        public CalculationContext Execute(
            CalculationInput input,
            CalculationConfig config)
        {
            var context = new CalculationContext(input);

            foreach (var stepConfig in config.Steps)
            {
                var step = _registry.Get(stepConfig.Type);
                step.Execute(context, stepConfig.Params);
            }

            return context;
        }
    }
}

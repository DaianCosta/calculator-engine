namespace CalculatorEngine.Engine
{
    using System.Text.Json;
    using CalculatorEngine.Domain.Models;

    public class InterestStep : ICalculationStep
    {
        public string Type => "juros";

        public void Execute(CalculationContext context, JsonElement parameters)
        {
            var tipo = parameters.GetProperty("tipo").GetString();

            // 🔥 1. Sem incidência
            if (tipo == "nenhum")
                return;

            var taxa = parameters.GetProperty("taxa").GetDecimal();

            var incidencia = parameters.TryGetProperty("incidencia", out var incProp)
                ? incProp.GetString()
                : "valor_atual";

            var carencia = parameters.TryGetProperty("carenciaDias", out var carProp)
                ? carProp.GetInt32()
                : 0;

            // 🔥 2. Regra de carência
            if (context.Input.DiasAtraso <= carencia)
                return;

            var diasConsiderados = context.Input.DiasAtraso - carencia;
            var meses = diasConsiderados / 30m;

            // 🔥 3. Base de cálculo
            decimal baseCalculo = incidencia == "valor_original"
                ? context.Input.ValorOriginal
                : context.CurrentValue;

            var previous = context.CurrentValue;

            decimal juros = 0;

            // 🔥 4. Tipos de juros
            if (tipo == "simples")
            {
                juros = baseCalculo * taxa * meses;
            }
            else if (tipo == "composto")
            {
                juros = baseCalculo * (decimal)Math.Pow((double)(1 + taxa), (double)meses) - baseCalculo;
            }
            else
            {
                throw new Exception($"Tipo de juros inválido: {tipo}");
            }

            context.CurrentValue += juros;

            context.Memory.Add(new CalculationMemory
            {
                StepName = "Juros",
                Description = $"{tipo} | {taxa:P} | base: {incidencia}",
                PreviousValue = previous,
                NewValue = context.CurrentValue,
                Details = new()
                {
                    ["juros"] = juros,
                    ["meses"] = meses,
                    ["base"] = baseCalculo,
                    ["carencia"] = carencia
                }
            });
        }
    }
}

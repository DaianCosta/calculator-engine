namespace CalculatorEngine.Engine
{
    using System.Text.Json;
    using CalculatorEngine.Domain.Models;

    public class MonetaryCorrectionStep : ICalculationStep
    {
        public string Type => "correcao_monetaria";

        public void Execute(CalculationContext context, JsonElement parameters)
        {
            var previous = context.CurrentValue;

            if (!parameters.TryGetProperty("indices", out var indicesElement))
                throw new Exception("Parâmetro 'indices' é obrigatório");

            decimal fator = 1m;
            var detalhes = new List<object>();

            foreach (var item in indicesElement.EnumerateArray())
            {
                var mes = item.GetProperty("mes").GetString();
                var valor = item.GetProperty("valor").GetDecimal();

                fator *= (1 + valor);

                detalhes.Add(new
                {
                    mes,
                    taxa = valor,
                    fatorParcial = fator
                });
            }

            context.CurrentValue *= fator;

            context.Memory.Add(new CalculationMemory
            {
                StepName = "Correção Monetária",
                Description = "Fator acumulado de índices",
                PreviousValue = previous,
                NewValue = context.CurrentValue,
                Details = new()
                {
                    ["fatorTotal"] = fator,
                    ["indices"] = detalhes
                }
            });
        }
    }
}

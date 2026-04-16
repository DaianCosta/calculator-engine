using System.Text.Json;
using CalculatorEngine.Domain.Models;
using CalculatorEngine.Helpers;

namespace CalculatorEngine.Engine
{
    
    //Correção de juros monetario
    public class MonetaryCorrectionStep : ICalculationStep
    {
        public string Type => "correcao_monetaria";

        public void Execute(CalculationContext context, JsonElement parameters)
        {
            var previous = context.CurrentValue;

            var periods = parameters.GetPeriods();

            decimal fatorTotal = 1m;
            var detalhes = new List<object>();

            foreach (var p in periods)
            {
                var fator = (decimal)Math.Pow(
                    (double)(1 + p.Indice),
                    (double)p.Dias / p.DiasMes
                );

                fatorTotal *= fator;

                detalhes.Add(new
                {
                    mes = p.Mes,
                    indice = p.Indice,
                    dias = p.Dias,
                    diasMes = p.DiasMes,
                    fator
                });
            }

            // ⚠️ regra importante: não aplicar se fator negativo
            if (fatorTotal <= 0)
                return;

            context.CurrentValue *= fatorTotal;

            context.Memory.Add(new CalculationMemory
            {
                StepName = "Correção Monetária",
                Description = "Correção pró-rata por período",
                PreviousValue = previous,
                NewValue = context.CurrentValue,
                Details = new()
                {
                    ["fatorTotal"] = fatorTotal,
                    ["periodos"] = detalhes
                }
            });
        }
    }
}

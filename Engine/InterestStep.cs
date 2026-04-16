using System.Text.Json;
using CalculatorEngine.Domain.Models;
using CalculatorEngine.Helpers;

namespace CalculatorEngine.Engine
{

    //calcular JUROS
    public class InterestStep : ICalculationStep
    {
        public string Type => "juros";

        public void Execute(CalculationContext context, JsonElement parameters)
        {
            var tipo = parameters.GetProperty("tipo").GetString();

            if (tipo == "nenhum")
                return;

            var taxa = parameters.GetProperty("taxa").GetDecimal();
            var periods = parameters.GetPeriods();

            var previous = context.CurrentValue;

            decimal fatorTotal = tipo == "simples" ? 0m : 1m;
            var detalhes = new List<object>();

            foreach (var p in periods)
            {
                var expoente = (double)p.Dias / p.DiasMes;

                if (tipo == "simples")
                {
                    var fator = (decimal)Math.Pow(
                        (double)(1 + taxa),
                        expoente
                    ) - 1;

                    fatorTotal += fator;

                    detalhes.Add(new
                    {
                        mes = p.Mes,
                        fator
                    });
                }
                else if (tipo == "composto")
                {
                    var fator = (decimal)Math.Pow(
                        (double)(1 + taxa),
                        expoente
                    );

                    fatorTotal *= fator;

                    detalhes.Add(new
                    {
                        mes = p.Mes,
                        fator
                    });
                }
            }

            decimal juros;

            if (tipo == "simples")
            {
                juros = context.CurrentValue * fatorTotal;
            }
            else
            {
                juros = context.CurrentValue * (fatorTotal - 1);
            }

            context.CurrentValue += juros;

            context.Memory.Add(new CalculationMemory
            {
                StepName = "Juros",
                Description = tipo,
                PreviousValue = previous,
                NewValue = context.CurrentValue,
                Details = new()
                {
                    ["tipo"] = tipo,
                    ["fatorTotal"] = fatorTotal,
                    ["juros"] = juros,
                    ["periodos"] = detalhes
                }
            });
        }
    }
}

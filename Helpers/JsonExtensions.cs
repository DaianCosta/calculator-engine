namespace CalculatorEngine.Helpers
{
    using System.Text.Json;
    using CalculatorEngine.Domain.Models;

    public static class JsonExtensions
    {
        public static List<Period> GetPeriods(this JsonElement parameters)
        {
            if (!parameters.TryGetProperty("periodos", out var element))
                throw new Exception("Parâmetro 'periodos' é obrigatório");

            var periods = new List<Period>();

            foreach (var item in element.EnumerateArray())
            {
                periods.Add(new Period
                {
                    Mes = item.GetProperty("mes").GetString(),
                    Indice = item.GetProperty("indice").GetDecimal(),
                    Dias = item.GetProperty("dias").GetInt32(),
                    DiasMes = item.GetProperty("diasMes").GetInt32()
                });
            }

            return periods;
        }
    }
}

namespace priorizzeProject.Core.Models;

using priorizzeProject.Core.Models.Enums;

public class CycleValidator
{
    // valida se o ciclo é válido com base no ano atual e no trimestre atual
    public static bool IsValidCycle(CyclesEnum cycle, int year)
    {
        var currentDate = DateTime.Now;
        var currentYear = currentDate.Year;
        var currentMonth = currentDate.Month;

        // Não permite criar ciclos em anos anteriores
        if (year < currentYear)
            return false;

        // Se for ano anterior ao atual, não é válido
        if (year < currentYear)
            return false;

        // Se for o ano atual, verifica qual trimestre está acontecendo
        if (year == currentYear)
        {
            var currentQuarter = GetQuarterFromMonth(currentMonth);
            
            // Só permite criar ciclos a partir do trimestre atual
            return (int)cycle >= (int)currentQuarter;
        }

        // Se for ano futuro, todos os ciclos são válidos
        return true;
    }

    // obtém o trimestre com base no mês e retorna o enum correspondente
    public static CyclesEnum GetQuarterFromMonth(int month)
    {
        return month switch
        {
            1 or 2 or 3 => CyclesEnum.Q1,        // Janeiro, Fevereiro, Março
            4 or 5 or 6 => CyclesEnum.Q2,        // Abril, Maio, Junho
            7 or 8 or 9 => CyclesEnum.Q3,        // Julho, Agosto, Setembro
            10 or 11 or 12 => CyclesEnum.Q4,     // Outubro, Novembro, Dezembro
            _ => throw new ArgumentException($"Mês inválido: {month}")
        };
    }

    // obtém os meses correspondentes a um ciclo específico
    public static int[] GetMonthsFromCycle(CyclesEnum cycle)
    {
        return cycle switch
        {
            CyclesEnum.Q1 => new[] { 1, 2, 3 },      // Janeiro, Fevereiro, Março
            CyclesEnum.Q2 => new[] { 4, 5, 6 },      // Abril, Maio, Junho
            CyclesEnum.Q3 => new[] { 7, 8, 9 },      // Julho, Agosto, Setembro
            CyclesEnum.Q4 => new[] { 10, 11, 12 },   // Outubro, Novembro, Dezembro
            _ => throw new ArgumentException($"Ciclo inválido: {cycle}")
        };
    }

    // valida se um ciclo específico já passou com base no ano atual e no trimestre atual
    public static bool HasCyclePassed(CyclesEnum cycle, int year)
    {
        var currentDate = DateTime.Now;
        var currentYear = currentDate.Year;
        var currentMonth = currentDate.Month;

        // Se o ano é anterior, o ciclo já passou
        if (year < currentYear)
            return true;

        // Se for ano futuro, o ciclo não passou
        if (year > currentYear)
            return false;

        // Se for o ano atual, compara com o trimestre atual
        var currentQuarter = GetQuarterFromMonth(currentMonth);
        return (int)cycle < (int)currentQuarter;
    }
}

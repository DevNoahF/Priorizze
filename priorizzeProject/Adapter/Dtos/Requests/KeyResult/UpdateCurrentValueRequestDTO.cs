namespace priorizzeProject.Adapter.Dtos.Requests;

public sealed class UpdateCurrentValueRequestDTO
{
    public decimal CurrentValue { get; set; }
}

// Feito dessa forma pois a intenção é diferente pois a atualização do valor atual é uma ação específica, e não uma atualização completa do Key Result.
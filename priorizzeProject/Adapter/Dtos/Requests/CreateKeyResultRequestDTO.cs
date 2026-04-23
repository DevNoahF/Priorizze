namespace priorizzeProject.Adapter.Dtos.Requests;

public sealed class CreateKeyResultRequestDTO
{
    public Guid OkrId { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal InitialValue { get; set; }

    public decimal GoalValue { get; set; }

    public decimal CurrentValue { get; set; }

    public string Unit { get; set; } = string.Empty;

    public DateTime LimitDate { get; set; }

    public string? Description { get; set; }
}
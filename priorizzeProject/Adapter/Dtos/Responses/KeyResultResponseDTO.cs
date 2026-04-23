using System.Text.Json.Serialization;

namespace priorizzeProject.Adapter.Dtos.Responses;

public sealed class KeyResultResponseDTO
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("okrId")]
    public Guid OkrId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("initialValue")]
    public decimal InitialValue { get; set; }

    [JsonPropertyName("goalValue")]
    public decimal GoalValue { get; set; }

    [JsonPropertyName("currentValue")]
    public decimal CurrentValue { get; set; }

    [JsonPropertyName("unit")]
    public string Unit { get; set; } = string.Empty;

    [JsonPropertyName("limitDate")]
    public DateTime LimitDate { get; set; }

    [JsonPropertyName("lastUpdated")]
    public DateTime LastUpdated { get; set; }
}
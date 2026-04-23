using System.Text.Json.Serialization;

namespace priorizzeProject.Adapter.Dtos.Responses;

public sealed class JiraSyncConfigResponseDTO
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("userId")]
    public Guid UserId { get; set; }

    [JsonPropertyName("projectKey")]
    public string ProjectKey { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("lastSync")]
    public DateTime? LastSync { get; set; }
}
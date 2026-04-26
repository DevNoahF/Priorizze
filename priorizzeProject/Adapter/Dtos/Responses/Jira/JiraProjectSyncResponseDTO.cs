using System.Text.Json.Serialization;

namespace priorizzeProject.Adapter.Dtos.Responses;

public sealed class JiraProjectSyncResponseDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("jiraId")]
    public string JiraId { get; set; } = string.Empty;

    [JsonPropertyName("projectKey")]
    public string ProjectKey { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("jiraUrl")]
    public string JiraUrl { get; set; } = string.Empty;

    [JsonPropertyName("lastSync")]
    public DateTime? LastSync { get; set; }
}
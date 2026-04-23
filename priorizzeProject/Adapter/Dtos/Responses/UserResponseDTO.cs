using System.Text.Json.Serialization;
using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Adapter.Dtos.Responses;

public sealed class UserResponseDTO
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("role")]
    public UserRole Role { get; set; }

    [JsonPropertyName("squadName")]
    public string? SquadName { get; set; }

    [JsonPropertyName("squadJiraKey")]
    public string? SquadJiraKey { get; set; }

    [JsonPropertyName("jiraTeamId")]
    public string? JiraTeamId { get; set; }

    [JsonPropertyName("jiraAccountId")]
    public string JiraAccountId { get; set; } = string.Empty;

    [JsonPropertyName("jiraEmail")]
    public string JiraEmail { get; set; } = string.Empty;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
}
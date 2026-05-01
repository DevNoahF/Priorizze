using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Adapter.Dtos.Requests;

public sealed class CreateUserRequestDTO
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public string JiraAccountId { get; set; } = string.Empty;

    public string JiraEmail { get; set; } = string.Empty;

    public string JiraApiTokenEnc { get; set; } = string.Empty;

    public string? SquadName { get; set; }

    public string? SquadJiraKey { get; set; }

    public string? JiraTeamId { get; set; }
}
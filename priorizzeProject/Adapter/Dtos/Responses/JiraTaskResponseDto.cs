namespace priorizzeProject.Adapter.Dtos.Responses;

public class JiraTaskResponseDto
{
    public int Id { get; set; }
    public string ExternalKey { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public int SquadId { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public double? StoryPoints { get; set; }
    public string? AssigneeAccountId { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
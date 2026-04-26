namespace priorizzeProject.Adapter.Dtos.Requests;

public class SyncJiraTaskRequestDto
{
    public string ExternalKey { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public int SquadId { get; set; }
    public int? KpiId { get; set; }
    public string IssueType { get; set; } = string.Empty;
    public string StatusName { get; set; } = string.Empty;
    public string PriorityName { get; set; } = string.Empty;
    public double? StoryPoints { get; set; }
    public string? AssigneeAccountId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ResolvedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

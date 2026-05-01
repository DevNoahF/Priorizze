namespace priorizzeProject.Adapter.Dtos.Responses.JiraProjects;

public class JiraProjectsResponseDTO
{
    public int Id { get; set; }
    public required string JiraId { get; set; }
    public required string ProjectKey { get; set; }
    public required string Name { get; set; }
    public required string JiraUrl { get; set; }
    public DateTime? LastSync { get; set; }
}


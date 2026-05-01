namespace priorizzeProject.Adapter.Dtos.Requests.JiraProjects;

public class CreateJiraProjectRequestDTO
{
    public required string JiraId { get; set; }
    public required string ProjectKey { get; set; }
    public required string Name { get; set; }
    public required string JiraUrl { get; set; }
}


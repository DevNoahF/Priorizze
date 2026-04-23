namespace priorizzeProject.Core.Models;

public class JiraProjects
{
    public int Id { get; private set; }
    public required string JiraId { get; set; }
    public required string ProjectKey { get; set; }
    public required string Name { get; set; }
    public required string JiraUrl { get; set; }
    public DateTime? LastSync { get; set; }
}
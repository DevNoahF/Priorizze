namespace priorizzeProject.Adapter.Dtos.Requests;

public class UpdateOkrResquestDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? ManagerId { get; set; }
}
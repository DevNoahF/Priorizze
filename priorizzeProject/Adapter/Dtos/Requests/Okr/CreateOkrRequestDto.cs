namespace priorizzeProject.Adapter.Dtos.Requests;

public class CreateOkrRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CycleId { get; set; }
    public int DirectorId { get; set; }
    public int? ManagerId { get; set; }
}
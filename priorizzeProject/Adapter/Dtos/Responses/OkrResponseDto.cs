using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Adapter.Dtos.Responses;

public class OkrResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CycleId { get; set; }
    public int DirectorId { get; set; }
    public int? ManagerId { get; set; }
    public OkrStatusEnum Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
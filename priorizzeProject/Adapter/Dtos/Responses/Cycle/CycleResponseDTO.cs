using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Adapter.Dtos.Responses.Cycle;

public class CycleResponseDTO
{
    public int Id { get; set; }
    public required CyclesEnum CyclesEnum { get; set; }
    public required int Year { get; set; }
}


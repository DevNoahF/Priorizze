using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Adapter.Dtos.Requests.Cycle;

public class CreateCycleRequestDTO
{
    public required CyclesEnum CyclesEnum { get; set; }
    public required int Year { get; set; }
}


using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Core.Models;

public class Cycle
{
    public int Id { get; private set; }
    public CyclesEnum CyclesEnum { get; set; }
    public int Year { get; set; }
}
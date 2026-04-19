using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Core.Models;

public class MetricsHistory
{
    public int Id { get; private set; }

    public int TargetId { get; set; } 
    
    public TypesEnum TypesEnum { get; set; }

    public int CapturedValue { get;  set; }
    
    public DateTime RecordedAt { get; set; }
}
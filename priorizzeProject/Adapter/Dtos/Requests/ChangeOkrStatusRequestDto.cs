using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Adapter.Dtos.Requests;

public class ChangeOkrStatusRequestDto
{
    public int OkrId { get; set; }
    public OkrStatusEnum NewStatus { get; set; }
}
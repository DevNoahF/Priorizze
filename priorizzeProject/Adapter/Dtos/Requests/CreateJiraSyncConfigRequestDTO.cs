namespace priorizzeProject.Adapter.Dtos.Requests;

public sealed class CreateJiraSyncConfigRequestDTO
{
    public Guid UserId { get; set; }

    public string ProjectKey { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;
}
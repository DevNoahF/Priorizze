using System.Text.Json.Serialization;

namespace priorizzeProject.Adapter.Dtos.Responses;

// mapeamento da resposta da api do jira para o projeto
public class JiraProjectResponseDTO
{
    
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

   
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

   
    [JsonPropertyName("self")]
    public string Self { get; set; } = string.Empty;
}

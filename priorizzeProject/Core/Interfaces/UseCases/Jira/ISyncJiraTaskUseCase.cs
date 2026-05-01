using priorizzeProject.Adapter.Dtos.Requests;
using priorizzeProject.Adapter.Dtos.Responses;

namespace priorizzeProject.Core.Interfaces;

public interface ISyncJiraTaskUseCase
{
    Task<JiraTaskResponseDto?> ExecuteAsync(SyncJiraTaskRequestDto request);
}
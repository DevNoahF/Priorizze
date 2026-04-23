using priorizzeProject.Core.Models;

namespace priorizzeProject.Core.Interfaces;

public interface IJiraSyncConfigUseCase
{
    Task<JiraSyncConfig> ExecuteAsync(
        Guid userId,
        string projectKey,
        string url);

    Task<JiraSyncConfig?> GetByIdAsync(Guid id);

    Task<List<JiraSyncConfig>> GetByUserIdAsync(Guid userId);

    Task<bool> MarkSyncedAsync(Guid id, DateTime syncTime);
}
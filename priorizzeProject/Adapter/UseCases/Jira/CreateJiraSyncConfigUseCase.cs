using priorizzeProject.Core.Interfaces;
using priorizzeProject.Core.Interfaces.Repositories;
using priorizzeProject.Core.Models;

namespace priorizzeProject.Adapter.UseCases;

public class CreateJiraSyncConfigUseCase : IJiraSyncConfigUseCase
{
    private readonly IJiraSyncConfigRepository _jiraSyncConfigRepository;

    public CreateJiraSyncConfigUseCase(IJiraSyncConfigRepository jiraSyncConfigRepository)
    {
        _jiraSyncConfigRepository = jiraSyncConfigRepository;
    }

    public async Task<JiraSyncConfig> ExecuteAsync(
        Guid userId,
        string projectKey,
        string url)
    {
        var syncConfig = new JiraSyncConfig
        {
            UserId = userId,
            ProjectKey = projectKey,
            Url = url
        };

        return await _jiraSyncConfigRepository.AddAsync(syncConfig);
    }

    public async Task<JiraSyncConfig?> GetByIdAsync(Guid id)
    {
        return await _jiraSyncConfigRepository.GetByIdAsync(id);
    }

    public async Task<List<JiraSyncConfig>> GetByUserIdAsync(Guid userId)
    {
        return await _jiraSyncConfigRepository.GetByUserIdAsync(userId);
    }

    public async Task<bool> MarkSyncedAsync(Guid id, DateTime syncTime)
    {
        var syncConfig = await _jiraSyncConfigRepository.GetByIdAsync(id);

        if (syncConfig is null)
        {
            return false;
        }

        syncConfig.LastSync = syncTime;
        await _jiraSyncConfigRepository.UpdateAsync(syncConfig);

        return true;
    }
}

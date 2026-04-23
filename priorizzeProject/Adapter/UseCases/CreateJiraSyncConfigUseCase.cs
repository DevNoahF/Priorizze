using Microsoft.EntityFrameworkCore;
using priorizzeProject.Core.Interfaces;
using priorizzeProject.Adapter.Persistence;
using priorizzeProject.Core.Models;

namespace priorizzeProject.Adapter.UseCases;

public class CreateJiraSyncConfigUseCase : IJiraSyncConfigUseCase
{
    private readonly AppDbContext _dbContext;

    public CreateJiraSyncConfigUseCase(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<JiraSyncConfig> ExecuteAsync(
        Guid userId,
        string projectKey,
        string url)
    {
        var syncConfig = new JiraSyncConfig(userId, projectKey, url);

        _dbContext.JiraSyncConfigs.Add(syncConfig);
        await _dbContext.SaveChangesAsync();

        return syncConfig;
    }

    public async Task<JiraSyncConfig?> GetByIdAsync(Guid id)
    {
        return await _dbContext.JiraSyncConfigs
            .Include(syncConfig => syncConfig.User)
            .FirstOrDefaultAsync(syncConfig => syncConfig.Id == id);
    }

    public async Task<List<JiraSyncConfig>> GetByUserIdAsync(Guid userId)
    {
        return await _dbContext.JiraSyncConfigs
            .Where(syncConfig => syncConfig.UserId == userId)
            .ToListAsync();
    }

    public async Task<bool> MarkSyncedAsync(Guid id, DateTime syncTime)
    {
        var syncConfig = await _dbContext.JiraSyncConfigs.FirstOrDefaultAsync(syncConfig => syncConfig.Id == id);

        if (syncConfig is null)
        {
            return false;
        }

        syncConfig.MarkSynced(syncTime);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
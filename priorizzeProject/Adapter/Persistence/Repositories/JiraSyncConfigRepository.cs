using Microsoft.EntityFrameworkCore;
using priorizzeProject.Core.Interfaces.Repositories;
using priorizzeProject.Core.Models;

namespace priorizzeProject.Adapter.Persistence.Repositories;

public class JiraSyncConfigRepository : IJiraSyncConfigRepository
{
    private readonly AppDbContext _dbContext;

    public JiraSyncConfigRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<JiraSyncConfig> AddAsync(JiraSyncConfig syncConfig)
    {
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

    public async Task<JiraSyncConfig?> GetByIdWithUserAsync(Guid id)
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

    public async Task<JiraSyncConfig> UpdateAsync(JiraSyncConfig syncConfig)
    {
        _dbContext.JiraSyncConfigs.Update(syncConfig);
        await _dbContext.SaveChangesAsync();
        return syncConfig;
    }
}

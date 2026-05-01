using Microsoft.EntityFrameworkCore;
using priorizzeProject.Core.Interfaces.Repositories;
using priorizzeProject.Core.Models;

namespace priorizzeProject.Adapter.Persistence.Repositories;

public class MetricsHistoryRepository : IMetricsHistoryRepository
{
    private readonly AppDbContext _dbContext;

    public MetricsHistoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MetricsHistory> AddAsync(MetricsHistory metricsHistory)
    {
        _dbContext.MetricsHistories.Add(metricsHistory);
        await _dbContext.SaveChangesAsync();
        return metricsHistory;
    }

    public async Task<List<MetricsHistory>> GetByTargetIdAsync(int targetId)
    {
        return await _dbContext.MetricsHistories
            .Where(m => m.TargetId == targetId)
            .OrderByDescending(m => m.RecordedAt)
            .ToListAsync();
    }

    public async Task<MetricsHistory?> GetLatestValueAsync(int targetId)
    {
        return await _dbContext.MetricsHistories
            .Where(value => value.TargetId == targetId)
            .OrderByDescending(value => value.RecordedAt)
            .FirstOrDefaultAsync();
    }
}


using priorizzeProject.Core.Models;

namespace priorizzeProject.Core.Interfaces.Repositories;

public interface IMetricsHistoryRepository
{
    Task<MetricsHistory> AddAsync(MetricsHistory metricsHistory);

    Task<List<MetricsHistory>> GetByTargetIdAsync(int targetId);

    Task<MetricsHistory?> GetLatestValueAsync(int targetId);
}


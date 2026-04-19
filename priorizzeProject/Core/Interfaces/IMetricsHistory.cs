using priorizzeProject.Core.Models;

namespace priorizzeProject.Core.Interfaces;

public interface IMetricsHistory
{
    Task<bool> UpdateKeyResultValueSync(int KeyResultId, decimal newValue);

    Task<List<MetricsHistory>> ExecuteAsync(int targetId, int newValue);

    Task<List<MetricsHistory>> GetMetricsHistoryAsync(int targetId);
}
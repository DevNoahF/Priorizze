namespace priorizzeProject.Adapter.UseCases;

using Microsoft.EntityFrameworkCore;
using priorizzeProject.Core.Models;
using priorizzeProject.Core.Interfaces.Repositories;

public class UpdateKeyResultValueSyncUseCase
{
    private readonly IMetricsHistoryRepository _metricsHistoryRepository;

    public UpdateKeyResultValueSyncUseCase(IMetricsHistoryRepository metricsHistoryRepository)
    {
        _metricsHistoryRepository = metricsHistoryRepository;
    }

    public async Task<bool> ExecuteAsync(int targetId, int newValue)
    {
        try
        {
            var metricsHistory = new MetricsHistory
            {
                TargetId = targetId,
                CapturedValue = newValue,
                RecordedAt = DateTime.Now
            };

            await _metricsHistoryRepository.AddAsync(metricsHistory);
            return true;
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine("Erro ao atualizar banco de dados: " + e);
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro ao executar use case: " + e);
            return false;
        }
    }

    public async Task<List<MetricsHistory>> GetMetricsHistoryAsync(int targetId)
    {
        return await _metricsHistoryRepository.GetByTargetIdAsync(targetId);
    }

    public async Task<MetricsHistory?> GetLatestValueAsync(int targetId)
    {
        return await _metricsHistoryRepository.GetLatestValueAsync(targetId);
    }
}
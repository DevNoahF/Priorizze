namespace priorizzeProject.Adapter.UseCases;

using priorizzeProject.Adapter.Persistence;
using priorizzeProject.Core.Models;
using Microsoft.EntityFrameworkCore;

public class UpdateKeyResultValueSyncUseCase
{
    private readonly AppDbContext _dbContext;

    public UpdateKeyResultValueSyncUseCase(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExecuteAsync(int targetId, int newValue)
    {
        try
        {
            // Criar novo registro de histórico de métricas
            var metricsHistory = new MetricsHistory
            {
                TargetId = targetId,
                CapturedValue = newValue,
                RecordedAt = DateTime.Now
            };

            // Adicionar o novo registro ao contexto
            _dbContext.MetricsHistories.Add(metricsHistory);

            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException e)
        {
            // Tratar erro de atualização do banco de dados
            Console.WriteLine("Erro ao atualizar banco de dados: " + e);
            return false;
        }
        catch (Exception e)
        {
            // Tratar erro genérico
            Console.WriteLine("Erro ao executar use case: " + e);
            return false;
        }
    }

    public async Task<List<MetricsHistory>> GetMetricsHistoryAsync(int targetId)
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
using Microsoft.EntityFrameworkCore;
using priorizzeProject.Core.Interfaces;
using priorizzeProject.Adapter.Persistence;
using priorizzeProject.Core.Models;

namespace priorizzeProject.Adapter.UseCases;

public class CreateKeyResultUseCase : IKeyResultUseCase
{
    private readonly AppDbContext _dbContext;

    public CreateKeyResultUseCase(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<KeyResult> ExecuteAsync(
        Guid okrId,
        string title,
        decimal initialValue,
        decimal goalValue,
        decimal currentValue,
        string unit,
        DateTime limitDate,
        string? description = null)
    {
        var keyResult = new KeyResult
        {
            OkrId = okrId,
            Title = title,
            Description = description,
            InitialValue = initialValue,
            GoalValue = goalValue,
            CurrentValue = currentValue,
            Unit = unit,
            LimitDate = limitDate
        };

        _dbContext.KeyResults.Add(keyResult);
        await _dbContext.SaveChangesAsync();

        return keyResult;
    }

    public async Task<KeyResult?> GetByIdAsync(Guid id)
    {
        return await _dbContext.KeyResults.FirstOrDefaultAsync(keyResult => keyResult.Id == id);
    }

    public async Task<List<KeyResult>> GetByOkrIdAsync(Guid okrId)
    {
        return await _dbContext.KeyResults
            .Where(keyResult => keyResult.OkrId == okrId)
            .ToListAsync();
    }

    public async Task<bool> UpdateCurrentValueAsync(Guid id, decimal currentValue)
    {
        var keyResult = await _dbContext.KeyResults.FirstOrDefaultAsync(keyResult => keyResult.Id == id);

        if (keyResult is null)
        {
            return false;
        }

        keyResult.CurrentValue = currentValue;
        keyResult.LastUpdated = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

        return true;
    }
}

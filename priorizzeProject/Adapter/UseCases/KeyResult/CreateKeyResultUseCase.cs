using priorizzeProject.Core.Interfaces;
using priorizzeProject.Core.Interfaces.Repositories;
using priorizzeProject.Core.Models;

namespace priorizzeProject.Adapter.UseCases;

public class CreateKeyResultUseCase : IKeyResultUseCase
{
    private readonly IKeyResultRepository _keyResultRepository;

    public CreateKeyResultUseCase(IKeyResultRepository keyResultRepository)
    {
        _keyResultRepository = keyResultRepository;
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

        return await _keyResultRepository.AddAsync(keyResult);
    }

    public async Task<KeyResult?> GetByIdAsync(Guid id)
    {
        return await _keyResultRepository.GetByIdAsync(id);
    }

    public async Task<List<KeyResult>> GetByOkrIdAsync(Guid okrId)
    {
        return await _keyResultRepository.GetByOkrIdAsync(okrId);
    }

    public async Task<bool> UpdateCurrentValueAsync(Guid id, decimal currentValue)
    {
        var keyResult = await _keyResultRepository.GetByIdAsync(id);

        if (keyResult is null)
        {
            return false;
        }

        keyResult.CurrentValue = currentValue;
        keyResult.LastUpdated = DateTime.UtcNow;
        await _keyResultRepository.UpdateAsync(keyResult);

        return true;
    }
}

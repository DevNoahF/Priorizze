using priorizzeProject.Core.Models;

namespace priorizzeProject.Core.Interfaces;

public interface IKeyResultUseCase
{
    Task<KeyResult> ExecuteAsync(
        Guid okrId,
        string title,
        decimal initialValue,
        decimal goalValue,
        decimal currentValue,
        string unit,
        DateTime limitDate,
        string? description = null);

    Task<KeyResult?> GetByIdAsync(Guid id);

    Task<List<KeyResult>> GetByOkrIdAsync(Guid okrId);

    Task<bool> UpdateCurrentValueAsync(Guid id, decimal currentValue);
}
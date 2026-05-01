using priorizzeProject.Core.Models;

namespace priorizzeProject.Core.Interfaces.Repositories;

public interface IKeyResultRepository
{
    Task<KeyResult> AddAsync(KeyResult keyResult);

    Task<KeyResult?> GetByIdAsync(Guid id);

    Task<List<KeyResult>> GetByOkrIdAsync(Guid okrId);

    Task<KeyResult> UpdateAsync(KeyResult keyResult);
}

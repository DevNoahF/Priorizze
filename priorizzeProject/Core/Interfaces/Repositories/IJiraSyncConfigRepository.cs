using priorizzeProject.Core.Models;

namespace priorizzeProject.Core.Interfaces.Repositories;

public interface IJiraSyncConfigRepository
{
    Task<JiraSyncConfig> AddAsync(JiraSyncConfig syncConfig);

    Task<JiraSyncConfig?> GetByIdAsync(Guid id);

    Task<JiraSyncConfig?> GetByIdWithUserAsync(Guid id);

    Task<List<JiraSyncConfig>> GetByUserIdAsync(Guid userId);

    Task<JiraSyncConfig> UpdateAsync(JiraSyncConfig syncConfig);
}

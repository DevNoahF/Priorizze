using Microsoft.EntityFrameworkCore;
using priorizzeProject.Core.Interfaces.Repositories;
using priorizzeProject.Core.Models;

namespace priorizzeProject.Adapter.Persistence.Repositories;

public class KeyResultRepository : IKeyResultRepository
{
    private readonly AppDbContext _dbContext;

    public KeyResultRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<KeyResult> AddAsync(KeyResult keyResult)
    {
        _dbContext.KeyResults.Add(keyResult);
        await _dbContext.SaveChangesAsync();
        return keyResult;
    }

    public async Task<KeyResult?> GetByIdAsync(Guid id)
    {
        return await _dbContext.KeyResults
            .FirstOrDefaultAsync(keyResult => keyResult.Id == id);
    }

    public async Task<List<KeyResult>> GetByOkrIdAsync(Guid okrId)
    {
        return await _dbContext.KeyResults
            .Where(keyResult => keyResult.OkrId == okrId)
            .ToListAsync();
    }

    public async Task<KeyResult> UpdateAsync(KeyResult keyResult)
    {
        _dbContext.KeyResults.Update(keyResult);
        await _dbContext.SaveChangesAsync();
        return keyResult;
    }
}

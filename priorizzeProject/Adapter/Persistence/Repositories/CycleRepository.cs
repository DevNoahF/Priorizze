using Microsoft.EntityFrameworkCore;
using priorizzeProject.Core.Interfaces.Repositories;
using priorizzeProject.Core.Models;
using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Adapter.Persistence.Repositories;

public class CycleRepository : ICycleRepository
{
    private readonly AppDbContext _dbContext;

    public CycleRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Cycle> AddAsync(Cycle cycle)
    {
        _dbContext.Cycles.Add(cycle);
        await _dbContext.SaveChangesAsync();
        return cycle;
    }

    public async Task<Cycle?> GetByIdAsync(int id)
    {
        return await _dbContext.Cycles
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Cycle>> GetAllAsync()
    {
        return await _dbContext.Cycles.ToListAsync();
    }

    public async Task<Cycle?> GetByCycleAndYearAsync(CyclesEnum cyclesEnum, int year)
    {
        return await _dbContext.Cycles
            .FirstOrDefaultAsync(c => c.CyclesEnum == cyclesEnum && c.Year == year);
    }

    public async Task<List<Cycle>> QueryAsync(CyclesEnum? cyclesEnum, int? year, int? yearFrom, int? yearTo)
    {
        var query = _dbContext.Cycles.AsQueryable();

        if (cyclesEnum.HasValue)
            query = query.Where(c => c.CyclesEnum == cyclesEnum.Value);

        if (year.HasValue)
        {
            query = query.Where(c => c.Year == year.Value);
        }
        else
        {
            if (yearFrom.HasValue)
                query = query.Where(c => c.Year >= yearFrom.Value);

            if (yearTo.HasValue)
                query = query.Where(c => c.Year <= yearTo.Value);
        }

        return await query
            .OrderBy(c => c.Year)
            .ThenBy(c => c.CyclesEnum)
            .ToListAsync();
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var cycle = await _dbContext.Cycles
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cycle is null)
            return false;

        _dbContext.Cycles.Remove(cycle);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}


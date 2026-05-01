using priorizzeProject.Core.Models;
using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Core.Interfaces.Repositories;

public interface ICycleRepository
{
    Task<Cycle> AddAsync(Cycle cycle);

    Task<Cycle?> GetByIdAsync(int id);

    Task<List<Cycle>> GetAllAsync();

    Task<Cycle?> GetByCycleAndYearAsync(CyclesEnum cyclesEnum, int year);

    Task<List<Cycle>> QueryAsync(CyclesEnum? cyclesEnum, int? year, int? yearFrom, int? yearTo);

    Task<bool> DeleteByIdAsync(int id);
}


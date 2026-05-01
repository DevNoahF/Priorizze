using priorizzeProject.Core.Models;
using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Core.Interfaces;

public interface ICycleUseCase
{
    Task<Cycle> ExecuteAsync(
        CyclesEnum cyclesEnum,
        int year);

    Task<Cycle?> GetByIdAsync(int id);

    Task<List<Cycle>> GetAllAsync();

    Task<Cycle?> GetByCycleAndYearAsync(CyclesEnum cyclesEnum, int year);

    Task<List<Cycle>> QueryAsync(CyclesEnum? cyclesEnum, int? year, int? yearFrom, int? yearTo);

    Task<bool> DeleteAsync(int id);
}

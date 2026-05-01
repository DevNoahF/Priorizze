using priorizzeProject.Core.Interfaces;
using priorizzeProject.Core.Models.Enums;
using priorizzeProject.Core.Interfaces.Repositories;

namespace priorizzeProject.Adapter.UseCases.Cycle;

public class CreateCycleUseCase : ICycleUseCase
{
    private readonly ICycleRepository _cycleRepository;

    public CreateCycleUseCase(ICycleRepository cycleRepository)
    {
        _cycleRepository = cycleRepository;
    }

    public async Task<Core.Models.Cycle> ExecuteAsync(
        CyclesEnum cyclesEnum,
        int year)
    {
        var cycle = new Core.Models.Cycle
        {
            CyclesEnum = cyclesEnum,
            Year = year
        };

        return await _cycleRepository.AddAsync(cycle);
    }

    public async Task<Core.Models.Cycle?> GetByIdAsync(int id)
    {
        return await _cycleRepository.GetByIdAsync(id);
    }

    public async Task<List<Core.Models.Cycle>> GetAllAsync()
    {
        return await _cycleRepository.GetAllAsync();
    }

    public async Task<List<Core.Models.Cycle>> QueryAsync(CyclesEnum? cyclesEnum, int? year, int? yearFrom, int? yearTo)
    {
        return await _cycleRepository.QueryAsync(cyclesEnum, year, yearFrom, yearTo);
    }

    public async Task<Core.Models.Cycle?> GetByCycleAndYearAsync(CyclesEnum cyclesEnum, int year)
    {
        return await _cycleRepository.GetByCycleAndYearAsync(cyclesEnum, year);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _cycleRepository.DeleteByIdAsync(id);
    }
}

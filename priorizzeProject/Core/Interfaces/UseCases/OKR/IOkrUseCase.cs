using priorizzeProject.Adapter.Dtos.Requests;
using priorizzeProject.Adapter.Dtos.Responses;
using priorizzeProject.Core.Models;

namespace priorizzeProject.Core.Interfaces.UseCases.OKR;

public interface IOkrUseCase
{
    Task<OkrResponseDto> CreateAsync(CreateOkrRequestDto request);

    Task<OkrResponseDto> UpdateAsync(int id, UpdateOkrResquestDto request);

    Task<bool> UpdateStatusAsync(int id, int newStatus);

    Task<OkrResponseDto?> GetByIdAsync(int id);

    Task<List<OkrResponseDto>> GetByCycleIdAsync(int cycleId);

    Task<List<OkrResponseDto>> GetByDirectorIdAsync(int directorId);
}

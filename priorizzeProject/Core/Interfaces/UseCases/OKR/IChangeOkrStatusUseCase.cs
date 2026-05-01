using priorizzeProject.Adapter.Dtos.Requests;
using priorizzeProject.Adapter.Dtos.Responses;

namespace priorizzeProject.Core.Interfaces.UseCases.OKR;

public interface IChangeOkrStatusUseCase
{
    Task<OkrResponseDto> ExecuteAsync(ChangeOkrStatusRequestDto request);
}
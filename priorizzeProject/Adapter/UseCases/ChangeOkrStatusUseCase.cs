using Microsoft.EntityFrameworkCore;
using priorizzeProject.Adapter.Dtos.Requests;
using priorizzeProject.Adapter.Dtos.Responses;
using priorizzeProject.Adapter.Persistence;
using priorizzeProject.Core.Interfaces;
using priorizzeProject.Core.Interfaces.UseCases.OKR;
using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Adapter.UseCases;

public class ChangeOkrStatusUseCase : IChangeOkrStatusUseCase
{
    private readonly AppDbContext _dbContext;

    public ChangeOkrStatusUseCase(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OkrResponseDto> ExecuteAsync(ChangeOkrStatusRequestDto request)
    {
        try
        {

            var okr = await _dbContext.Okrs.FindAsync(request.OkrId);

            if (okr == null)
            {
                return null; 
            }
            
            switch (request.NewStatus)
            {
                case OkrStatusEnum.Ativo:

                    if (okr.Status == OkrStatusEnum.Criado)
                    {
                        okr.Status = OkrStatusEnum.Ativo;
                    }
                    break;

                case OkrStatusEnum.Concluido:
                    okr.Status = OkrStatusEnum.Concluido;
                    okr.FinishedAt = DateTime.UtcNow; 
                    break;

                case OkrStatusEnum.Cancelado:
                    okr.Status = OkrStatusEnum.Cancelado;
                    break;
            }


            _dbContext.Okrs.Update(okr);
            await _dbContext.SaveChangesAsync();
            
            return new OkrResponseDto()
            {
                Id = okr.Id,
                Title = okr.Title,
                Status = okr.Status,
            };
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro ao alterar status do OKR: {e.Message}");
            return null;
        }
    }
}
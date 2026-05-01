using priorizzeProject.Adapter.Dtos.Requests;
using priorizzeProject.Adapter.Dtos.Responses;
using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Adapter.UseCases;

using Microsoft.EntityFrameworkCore;
using priorizzeProject.Adapter.Persistence;
using priorizzeProject.Core.Models;

public class CreateOkrUseCase
{
    private readonly AppDbContext _dbContext;

    public CreateOkrUseCase(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OkrResponseDto?> ExecuteAsync(CreateOkrRequestDto request)
    {
        try
        {
            var okr = new OKRs
            {
                Title = request.Title,
                Description = request.Description,
                CycleId = request.CycleId,
                DirectorId = request.DirectorId,
                ManagerId = request.ManagerId,
                Status = OkrStatusEnum.Criado
            };

            _dbContext.Okrs.Add(okr);
            await _dbContext.SaveChangesAsync();

            return new OkrResponseDto
            {
                Id = okr.Id,
                Title = okr.Title,
                Description = okr.Description,
                CycleId = okr.CycleId,
                DirectorId = okr.DirectorId,
                ManagerId = okr.ManagerId,
                Status = okr.Status,
                CreatedAt = okr.CreatedAt
            };
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine("Erro ao atualizar banco de dados: " + e);
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro ao executar use case de criação de OKR: " + e);
            return null;
        }
    }
}
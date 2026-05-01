using Microsoft.EntityFrameworkCore;
using priorizzeProject.Adapter.Dtos.Requests;
using priorizzeProject.Adapter.Dtos.Responses;
using priorizzeProject.Adapter.Persistence;
using priorizzeProject.Core.Models;
// 1. Importante: Adicionamos o namespace onde a interface está
using priorizzeProject.Core.Interfaces; 

namespace priorizzeProject.Adapter.UseCases;

public class SyncJiraTaskUseCase : ISyncJiraTaskUseCase 
{
    private readonly AppDbContext _dbContext;

    public SyncJiraTaskUseCase(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<JiraTaskResponseDto?> ExecuteAsync(SyncJiraTaskRequestDto request)
    {
        try
        {
            var task = await _dbContext.JiraTasks
                .FirstOrDefaultAsync(t => t.ExternalKey == request.ExternalKey);

            if (task == null)
            {
                task = new JiraTasks
                {
                    ExternalKey = request.ExternalKey,
                    Summary = request.Summary,
                    ProjectId = request.ProjectId,
                    SquadId = request.SquadId,
                    KpiId = request.KpiId,
                    IssueType = request.IssueType,
                    StatusName = request.StatusName,
                    PriorityName = request.PriorityName,
                    StoryPoints = request.StoryPoints,
                    AssigneeAccountId = request.AssigneeAccountId,
                    CreatedAt = request.CreatedAt,
                    UpdatedAt = request.UpdatedAt,
                    ResolvedAt = request.ResolvedAt
                };
                _dbContext.JiraTasks.Add(task);
            }
            else
            {
                // Cenário 2: Atualizar tarefa existente com dados novos da API
                task.Summary = request.Summary;
                task.StatusName = request.StatusName;
                task.PriorityName = request.PriorityName;
                task.StoryPoints = request.StoryPoints;
                task.AssigneeAccountId = request.AssigneeAccountId;
                task.UpdatedAt = request.UpdatedAt;
                task.ResolvedAt = request.ResolvedAt;
                // KpiId pode ser atualizado se o vínculo mudar
                task.KpiId = request.KpiId ?? task.KpiId;

                _dbContext.JiraTasks.Update(task);
            }

            await _dbContext.SaveChangesAsync();
            return new JiraTaskResponseDto
            {
                Id = task.Id,
                ExternalKey = task.ExternalKey,
                Summary = task.Summary,
                ProjectId = task.ProjectId,
                SquadId = task.SquadId,
                StatusName = task.StatusName,
                StoryPoints = task.StoryPoints,
                AssigneeAccountId = task.AssigneeAccountId,
                UpdatedAt = task.UpdatedAt
            };
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine("Erro ao atualizar banco de dados no SyncJiraTask: " + e);
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro genérico ao executar SyncJiraTaskUseCase: " + e);
            return null;
        }
    }
}
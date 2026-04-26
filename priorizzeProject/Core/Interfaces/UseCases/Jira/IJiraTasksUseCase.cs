using priorizzeProject.Adapter.Dtos.Requests;
using priorizzeProject.Adapter.Dtos.Responses;

namespace priorizzeProject.Core.Interfaces;

public interface IJiraTasksUseCase
{
    /// Sincroniza uma tarefa vinda do Jira. Se a tarefa (ExternalKey) já existir, atualiza; caso contrário, cria.
    Task<bool> SyncTaskAsync(SyncJiraTaskRequestDto request);

    /// Busca uma tarefa pela chave externa do Jira (ex: PROJ-123).
    Task<JiraTaskResponseDto?> GetByExternalKeyAsync(string externalKey);

    /// Lista as tarefas vinculadas a uma Squad específica (Dashboard do Jira).
    Task<List<JiraTaskResponseDto>> GetBySquadIdAsync(int squadId);
    
    /// Lista as tarefas de um projeto específico.
    Task<List<JiraTaskResponseDto>> GetByProjectIdAsync(int projectId);
    
    /// Vincula uma tarefa do Jira a um KPI/Key Result específico.
    Task<bool> LinkToKpiAsync(int taskId, int kpiId);
}
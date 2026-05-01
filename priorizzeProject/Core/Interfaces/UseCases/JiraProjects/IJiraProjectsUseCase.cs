using priorizzeProject.Core.Models;

namespace priorizzeProject.Core.Interfaces;

public interface IJiraProjectsUseCase
{
    Task<JiraProjects> ExecuteAsync(
        string jiraId,
        string projectKey,
        string name,
        string jiraUrl);

    Task<JiraProjects?> GetByIdAsync(int id);

    Task<List<JiraProjects>> GetAllAsync();

    Task<JiraProjects?> GetByProjectKeyAsync(string projectKey);

    Task<bool> DeleteAsync(int id);
}


using priorizzeProject.Core.Models;

namespace priorizzeProject.Core.Interfaces.Repositories;

public interface IJiraProjectsRepository
{
    Task<JiraProjects> AddAsync(JiraProjects project);

    Task<JiraProjects?> GetByIdAsync(int id);

    Task<List<JiraProjects>> GetAllAsync();

    Task<JiraProjects?> GetByProjectKeyAsync(string projectKey);

    Task<JiraProjects?> GetByJiraIdOrProjectKeyAsync(string jiraId, string projectKey);

    Task<JiraProjects> UpdateAsync(JiraProjects project);

    Task<bool> DeleteByIdAsync(int id);
}


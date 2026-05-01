using priorizzeProject.Core.Interfaces;
using priorizzeProject.Core.Interfaces.Repositories;

namespace priorizzeProject.Adapter.UseCases.JiraProjects;

public class CreateJiraProjectsUseCase : IJiraProjectsUseCase
{
    private readonly IJiraProjectsRepository _jiraProjectsRepository;

    public CreateJiraProjectsUseCase(IJiraProjectsRepository jiraProjectsRepository)
    {
        _jiraProjectsRepository = jiraProjectsRepository;
    }

    public async Task<Core.Models.JiraProjects> ExecuteAsync(
        string jiraId,
        string projectKey,
        string name,
        string jiraUrl)
    {
        var jiraProject = new Core.Models.JiraProjects
        {
            JiraId = jiraId,
            ProjectKey = projectKey,
            Name = name,
            JiraUrl = jiraUrl,
            LastSync = null
        };

        return await _jiraProjectsRepository.AddAsync(jiraProject);
    }

    public async Task<Core.Models.JiraProjects?> GetByIdAsync(int id)
    {
        return await _jiraProjectsRepository.GetByIdAsync(id);
    }

    public async Task<List<Core.Models.JiraProjects>> GetAllAsync()
    {
        return await _jiraProjectsRepository.GetAllAsync();
    }

    public async Task<Core.Models.JiraProjects?> GetByProjectKeyAsync(string projectKey)
    {
        return await _jiraProjectsRepository.GetByProjectKeyAsync(projectKey);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _jiraProjectsRepository.DeleteByIdAsync(id);
    }
}

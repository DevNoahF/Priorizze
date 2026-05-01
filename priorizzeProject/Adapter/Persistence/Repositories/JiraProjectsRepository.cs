using Microsoft.EntityFrameworkCore;
using priorizzeProject.Core.Interfaces.Repositories;
using priorizzeProject.Core.Models;

namespace priorizzeProject.Adapter.Persistence.Repositories;

public class JiraProjectsRepository : IJiraProjectsRepository
{
    private readonly AppDbContext _dbContext;

    public JiraProjectsRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<JiraProjects> AddAsync(JiraProjects project)
    {
        _dbContext.JiraProjects.Add(project);
        await _dbContext.SaveChangesAsync();
        return project;
    }

    public async Task<JiraProjects?> GetByIdAsync(int id)
    {
        return await _dbContext.JiraProjects
            .FirstOrDefaultAsync(jp => jp.Id == id);
    }

    public async Task<List<JiraProjects>> GetAllAsync()
    {
        return await _dbContext.JiraProjects.ToListAsync();
    }

    public async Task<JiraProjects?> GetByProjectKeyAsync(string projectKey)
    {
        return await _dbContext.JiraProjects
            .FirstOrDefaultAsync(jp => jp.ProjectKey == projectKey);
    }

    public async Task<JiraProjects?> GetByJiraIdOrProjectKeyAsync(string jiraId, string projectKey)
    {
        return await _dbContext.JiraProjects
            .FirstOrDefaultAsync(jp => jp.JiraId == jiraId || jp.ProjectKey == projectKey);
    }

    public async Task<JiraProjects> UpdateAsync(JiraProjects project)
    {
        _dbContext.JiraProjects.Update(project);
        await _dbContext.SaveChangesAsync();
        return project;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var jiraProject = await _dbContext.JiraProjects
            .FirstOrDefaultAsync(jp => jp.Id == id);

        if (jiraProject is null)
            return false;

        _dbContext.JiraProjects.Remove(jiraProject);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}


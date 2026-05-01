using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using priorizzeProject.Adapter.Dtos.Responses;
using priorizzeProject.Adapter.Persistence;
using priorizzeProject.Core.Interfaces;
using priorizzeProject.Core.Interfaces.Repositories;

namespace priorizzeProject.Adapter.UseCases;

public class SyncJiraProjectUseCase : IJiraProjectSyncUseCase
{
    private readonly AppDbContext _dbContext;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IJiraProjectsRepository _jiraProjectsRepository;

    public SyncJiraProjectUseCase(
        AppDbContext dbContext,
        IHttpClientFactory httpClientFactory,
        IJiraProjectsRepository jiraProjectsRepository)
    {
        _dbContext = dbContext;
        _httpClientFactory = httpClientFactory;
        _jiraProjectsRepository = jiraProjectsRepository;
    }

    public async Task<Core.Models.JiraProjects> ExecuteAsync(Guid jiraSyncConfigId)
    {
        var syncConfig = await _dbContext.JiraSyncConfigs
            .Include(config => config.User)
            .FirstOrDefaultAsync(config => config.Id == jiraSyncConfigId);

        if (syncConfig is null)
        {
            throw new KeyNotFoundException("Jira sync config not found.");
        }

        if (syncConfig.User is null)
        {
            throw new InvalidOperationException("Jira sync config is not linked to a user.");
        }

        if (string.IsNullOrWhiteSpace(syncConfig.Url) || string.IsNullOrWhiteSpace(syncConfig.ProjectKey))
        {
            throw new InvalidOperationException("Jira sync config is incomplete.");
        }

        if (string.IsNullOrWhiteSpace(syncConfig.User.JiraEmail) || string.IsNullOrWhiteSpace(syncConfig.User.JiraApiTokenEnc))
        {
            throw new InvalidOperationException("User Jira credentials are missing.");
        }

        var projectResponse = await GetProjectFromJiraAsync(syncConfig.Url, syncConfig.ProjectKey, syncConfig.User.JiraEmail, syncConfig.User.JiraApiTokenEnc);
        var syncedAt = DateTime.UtcNow;

        var jiraProject = await _jiraProjectsRepository
            .GetByJiraIdOrProjectKeyAsync(projectResponse.Id, projectResponse.Key);

        if (jiraProject is null)
        {
            jiraProject = new Core.Models.JiraProjects
            {
                JiraId = projectResponse.Id,
                ProjectKey = projectResponse.Key,
                Name = projectResponse.Name,
                JiraUrl = projectResponse.Self,
                LastSync = syncedAt
            };

            jiraProject = await _jiraProjectsRepository.AddAsync(jiraProject);
        }
        else
        {
            jiraProject.JiraId = projectResponse.Id;
            jiraProject.ProjectKey = projectResponse.Key;
            jiraProject.Name = projectResponse.Name;
            jiraProject.JiraUrl = projectResponse.Self;
            jiraProject.LastSync = syncedAt;

            jiraProject = await _jiraProjectsRepository.UpdateAsync(jiraProject);
        }

        syncConfig.LastSync = syncedAt;
        await _dbContext.SaveChangesAsync();

        return jiraProject;
    }

    private async Task<JiraProjectResponseDTO> GetProjectFromJiraAsync(string jiraBaseUrl, string projectKey, string jiraEmail, string jiraApiToken)
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(EnsureTrailingSlash(jiraBaseUrl));
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{jiraEmail}:{jiraApiToken}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);

        var escapedProjectKey = Uri.EscapeDataString(projectKey);

        var response = await client.GetAsync($"rest/api/3/project/{escapedProjectKey}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            response = await client.GetAsync($"rest/api/2/project/{escapedProjectKey}");
        }

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Jira request failed: {(int)response.StatusCode} {response.ReasonPhrase}. {body}");
        }

        var json = await response.Content.ReadAsStringAsync();
        var project = JsonSerializer.Deserialize<JiraProjectResponseDTO>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (project is null)
        {
            throw new InvalidOperationException("Unable to read Jira project response.");
        }

        return project;
    }

    private static string EnsureTrailingSlash(string url) => url.EndsWith('/') ? url : url + '/';
}

using priorizzeProject.Core.Models;

namespace priorizzeProject.Core.Interfaces;

public interface IJiraProjectSyncUseCase
{
    Task<JiraProjects> ExecuteAsync(Guid jiraSyncConfigId);
}
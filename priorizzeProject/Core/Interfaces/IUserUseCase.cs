using priorizzeProject.Core.Models;
using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Core.Interfaces;

public interface IUserUseCase
{
    Task<User> ExecuteAsync(
        string name,
        string email,
        UserRole role,
        string jiraAccountId,
        string jiraEmail,
        string jiraApiTokenEnc,
        string? squadName = null,
        string? squadJiraKey = null,
        string? jiraTeamId = null);

    Task<User?> GetByIdAsync(Guid id);

    Task<List<User>> GetAllAsync();
}
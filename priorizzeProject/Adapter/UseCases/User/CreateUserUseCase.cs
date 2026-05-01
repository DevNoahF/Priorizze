using priorizzeProject.Core.Interfaces;
using priorizzeProject.Core.Interfaces.Repositories;
using priorizzeProject.Core.Models;
using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Adapter.UseCases;

public class CreateUserUseCase : IUserUseCase
{
    private readonly IUserRepository _userRepository;

    public CreateUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> ExecuteAsync(
        string name,
        string email,
        UserRole role,
        string jiraAccountId,
        string jiraEmail,
        string jiraApiTokenEnc,
        string? squadName = null,
        string? squadJiraKey = null,
        string? jiraTeamId = null)
    {
        var user = new User
        {
            Name = name,
            Email = email,
            Role = role,
            JiraAccountId = jiraAccountId,
            JiraEmail = jiraEmail,
            JiraApiTokenEnc = jiraApiTokenEnc,
            SquadName = squadName,
            SquadJiraKey = squadJiraKey,
            JiraTeamId = jiraTeamId
        };

        return await _userRepository.AddAsync(user);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }
}

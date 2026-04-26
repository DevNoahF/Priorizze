using Microsoft.EntityFrameworkCore;
using priorizzeProject.Core.Interfaces;
using priorizzeProject.Adapter.Persistence;
using priorizzeProject.Core.Models;
using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Adapter.UseCases;

public class CreateUserUseCase : IUserUseCase
{
    private readonly AppDbContext _dbContext;

    public CreateUserUseCase(AppDbContext dbContext)
    {
        _dbContext = dbContext;
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
        var user = new User(
            name,
            email,
            role,
            jiraAccountId,
            jiraEmail,
            jiraApiTokenEnc,
            squadName,
            squadJiraKey,
            jiraTeamId);

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users
            .Include(user => user.JiraSyncConfigs)
            .FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _dbContext.Users
            .Include(user => user.JiraSyncConfigs)
            .ToListAsync();
    }
}
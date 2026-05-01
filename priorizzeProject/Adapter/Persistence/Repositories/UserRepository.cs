using Microsoft.EntityFrameworkCore;
using priorizzeProject.Core.Interfaces.Repositories;
using priorizzeProject.Core.Models;

namespace priorizzeProject.Adapter.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> AddAsync(User user)
    {
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

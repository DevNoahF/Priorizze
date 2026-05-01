using priorizzeProject.Core.Models;

namespace priorizzeProject.Core.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> AddAsync(User user);

    Task<User?> GetByIdAsync(Guid id);

    Task<List<User>> GetAllAsync();
}

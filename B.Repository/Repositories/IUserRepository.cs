using A.Domain.Entities;

namespace B.Repository.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);

    Task<User> GetByIdAsync(int id);
}

using A.Domain.Entities;

namespace B.Repository.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user, CancellationToken cancellationToken);

    Task<User> GetByIdAsync(int id, CancellationToken cancellationToken);
}

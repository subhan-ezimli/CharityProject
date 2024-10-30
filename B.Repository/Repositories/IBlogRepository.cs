using A.Domain.Entities;

namespace B.Repository.Repositories;

public interface IBlogRepository
{
    Task AddAsync(Blog blog, CancellationToken cancellationToken);
    Task UpdateAsync(Blog blog);
    Task DeleteAsync(Blog blog, CancellationToken cancellationToken);
    Task<Blog?> GetByIdAsync(int id, CancellationToken cancellationToken);
    IQueryable<Blog> GetAll();
}

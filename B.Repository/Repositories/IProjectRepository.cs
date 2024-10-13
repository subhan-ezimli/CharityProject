using A.Domain.Entities;

namespace B.Repository.Repositories
{
    public interface IProjectRepository
    {
        Task AddAsync(Project project, CancellationToken cancellationToken);
        Task DeleteAsync(Project project, CancellationToken cancellationToken);
        Task UpdateAsync(Project project);
        Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken);
        IQueryable<Project> GetAllAsQueryable();
    }
}

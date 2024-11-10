using A.Domain.Entities;

namespace B.Repository.Repositories
{
    public interface IVolunteerRepository
    {
        Task AddAsync(Volunteer volunteer, CancellationToken cancellationToken);
        Task UpdateAsync(Volunteer volunteer, CancellationToken cancellationToken);
        Task DeleteAsync(Volunteer volunteer, CancellationToken cancellationToken);
        Task<Volunteer?> GetByIdAsync(int id, CancellationToken cancellationToken);
        IQueryable<Volunteer> GetAllAsQueryable();

    }
}

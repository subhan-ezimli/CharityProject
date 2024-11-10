using A.Domain.Entities;

namespace B.Repository.Repositories;

public interface IGalleryRepository
{
    Task AddAsync(Gallery gallery, CancellationToken cancellationToken);
    Task DeleteAsync(Gallery gallery, CancellationToken cancellationToken);
    Task UpdateAsync(Gallery gallery, CancellationToken cancellationToken);
    IQueryable<Gallery> GetAllAsQueryable();
    Task<Gallery?> GetByIdAsync(int id, CancellationToken cancellationToken);

}

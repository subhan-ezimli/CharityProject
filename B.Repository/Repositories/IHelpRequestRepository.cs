using A.Domain.Entities;

namespace B.Repository.Repositories;

public interface IHelpRequestRepository
{
    Task AddAsync(HelpRequest helpRequest, CancellationToken cancellationToken);

    Task DeleteAsync(HelpRequest helpRequest, CancellationToken cancellationToken);

    Task UpdateAsync(HelpRequest helpRequest);

    Task<HelpRequest?> GetByIdAsync(int id, CancellationToken cancellationToken);

    IQueryable<HelpRequest> GetAllAsQueryable();
}

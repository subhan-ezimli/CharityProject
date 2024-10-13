using A.Domain.Entities;
using B.Repository.Repositories;
using D.Dal.SqlServer.Context;

namespace D.Dal.SqlServer.Infrastructure;

public class SqlHelpRequestRepository : IHelpRequestRepository
{
    private readonly AppDbContext _context;
    public SqlHelpRequestRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(HelpRequest helpRequest, CancellationToken cancellationToken)
    {
        helpRequest.CreatedDate = DateTime.Now;
        await _context.HelpRequests.AddAsync(helpRequest, cancellationToken);
    }


    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var helpRequest = await _context.HelpRequests.FindAsync(id, cancellationToken);
        if (helpRequest != null)
        {
            helpRequest.IsDeleted = true;
            _context.HelpRequests.Update(helpRequest);
        }
    }

    public IQueryable<HelpRequest> GetAllAsQueryable()
    {
        return _context.HelpRequests.OrderByDescending(x => x.CreatedDate).AsQueryable();
    }


    public async Task<HelpRequest?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.HelpRequests.FindAsync(id,cancellationToken);
    }

    public async Task UpdateAsync(HelpRequest helpRequest)
    {
        _context.HelpRequests.Update(helpRequest);
    }

}

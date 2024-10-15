using A.Domain.Entities;
using B.Repository.Repositories;
using D.Dal.SqlServer.Context;

namespace D.Dal.SqlServer.Infrastructure;

public class SqlProjectRepository : IProjectRepository
{
    private readonly AppDbContext _context;
    public SqlProjectRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Project project, CancellationToken cancellationToken)
    {
        project.CreatedDate = DateTime.Now;
        await _context.Projects.AddAsync(project, cancellationToken);
    }

    public async Task DeleteAsync(Project project, CancellationToken cancellationToken)
    {
        project.IsDeleted = true;
        _context.Projects.Update(project);
    }

    public IQueryable<Project> GetAllAsQueryable()
    {
        return _context.Projects.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedDate).AsQueryable();
    }

    public async Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Projects.FindAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
    }
}

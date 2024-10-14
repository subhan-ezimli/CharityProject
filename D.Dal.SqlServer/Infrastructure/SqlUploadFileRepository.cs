using A.Domain.Entities;
using B.Repository.Repositories;
using D.Dal.SqlServer.Context;

namespace D.Dal.SqlServer.Infrastructure;

public class SqlUploadFileRepository : IUploadFileRepository
{
    private readonly AppDbContext _context;

    public SqlUploadFileRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UploadFile uploadFile, CancellationToken cancellationToken)
    {
        await _context.UploadFiles.AddAsync(uploadFile, cancellationToken);
    }

    public async Task<UploadFile?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.UploadFiles.FindAsync(id);

    }
}

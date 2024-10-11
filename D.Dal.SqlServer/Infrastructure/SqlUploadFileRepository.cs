using A.Domain.Entities;
using B.Repository.Repositories;
using D.Dal.SqlServer.Context;

namespace D.Dal.SqlServer.Infrastructure;

public class SqlUploadFileRepository : IUploadFileRepository
{
    private readonly AppDbContext _context;

    public SqlUploadFileRepository (AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UploadFile uploadFile)
    {
        await _context.UploadFiles.AddAsync(uploadFile);
    }

    public Task<UploadFile> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}

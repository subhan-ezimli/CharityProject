using B.Repository.Common;
using B.Repository.Repositories;
using D.Dal.SqlServer.Context;
using D.Dal.SqlServer.Infrastructure;

namespace D.Dal.SqlServer.UnitOfWork;

public class SqlUnitOfWork : IUnitOfWork
{

    private readonly AppDbContext _context;

    public SqlUnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public SqlUserRepository _userRepository;
    public SqlUploadFileRepository _uploadFileRepository;

    public IUserRepository UserRepository => _userRepository ??= new SqlUserRepository(_context);
    public IUploadFileRepository UploadFileRepository => _uploadFileRepository ?? new SqlUploadFileRepository(_context);

    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }
}

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
    public SqlHelpRequestRepository _helpRequestRepository;

    public IUserRepository UserRepository => _userRepository ??= new SqlUserRepository(_context);
    public IUploadFileRepository UploadFileRepository => _uploadFileRepository ?? new SqlUploadFileRepository(_context);
    public IHelpRequestRepository HelpRequestRepository => _helpRequestRepository ?? new SqlHelpRequestRepository(_context);

    public async Task<int> SaveChanges(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}

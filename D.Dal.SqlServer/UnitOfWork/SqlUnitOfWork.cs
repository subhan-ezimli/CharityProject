using B.Repository.Common;
using B.Repository.Repositories;
using D.Dal.SqlServer.Context;
using D.Dal.SqlServer.Infrastructure;
using Microsoft.Identity.Client.AuthScheme.PoP;

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
    public IProjectRepository _projectRepository;
    public IBlogRepository _blogRepository;
    public IGalleryRepository _galleryRepository;
    public IVolunteerRepository _volunteerRepository;

    public IUserRepository UserRepository => _userRepository ??= new SqlUserRepository(_context);
    public IUploadFileRepository UploadFileRepository => _uploadFileRepository ?? new SqlUploadFileRepository(_context);
    public IHelpRequestRepository HelpRequestRepository => _helpRequestRepository ?? new SqlHelpRequestRepository(_context);
    public IProjectRepository ProjectRepository => _projectRepository ?? new SqlProjectRepository(_context);
    public IBlogRepository BlogRepository => _blogRepository ?? new BlogRepository(_context);
    public IGalleryRepository GalleryRepository => _galleryRepository ?? new GalleryRepository(_context);
    public IVolunteerRepository VolunteerRepository => _volunteerRepository ?? new SqlVolunteerRepository(_context);

    public async Task<int> SaveChanges(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}

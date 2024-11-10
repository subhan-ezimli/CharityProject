using A.Domain.Entities;
using B.Repository.Repositories;
using D.Dal.SqlServer.Context;
using Microsoft.EntityFrameworkCore;

namespace D.Dal.SqlServer.Infrastructure;

public class GalleryRepository : IGalleryRepository
{
    private AppDbContext _context;

    public GalleryRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Gallery gallery, CancellationToken cancellationToken)
    {
        gallery.IsDeleted = false;
        gallery.CreatedDate = DateTime.Now;

        await _context.Galleries.AddAsync(gallery, cancellationToken);
    }

    public async Task DeleteAsync(Gallery gallery, CancellationToken cancellationToken)
    {
        gallery.IsDeleted = true;
        _context.Galleries.Update(gallery);
    }

    public IQueryable<Gallery> GetAllAsQueryable()
    {
        return _context.Galleries.Where(x => !x.IsDeleted).OrderByDescending(x => x.CreatedDate).AsQueryable();
    }

    public async Task<Gallery?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var gallery = await _context.Galleries.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        return gallery;
    }

    public async Task UpdateAsync(Gallery gallery, CancellationToken cancellationToken)
    {
        _context.Galleries.Update(gallery);
    }
}

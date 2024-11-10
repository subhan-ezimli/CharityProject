using A.Domain.Entities;
using B.Repository.Repositories;
using D.Dal.SqlServer.Context;
using Microsoft.EntityFrameworkCore;

namespace D.Dal.SqlServer.Infrastructure;

public class BlogRepository : IBlogRepository
{
    private readonly AppDbContext _context;

    public BlogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Blog blog, CancellationToken cancellationToken)
    {
        blog.IsDeleted = false;
        blog.CreatedDate = DateTime.Now;
        await _context.Blogs.AddAsync(blog, cancellationToken);
    }

    public async Task DeleteAsync(Blog blog, CancellationToken cancellationToken)
    {
        blog.IsDeleted = true;
        _context.Blogs.Update(blog);
    }

    public IQueryable<Blog> GetAll()
    {
        return _context.Blogs.Where(x => !x.IsDeleted).OrderByDescending(x => x.CreatedDate);
    }

    public async Task<Blog?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        return blog;
    }

    public async Task UpdateAsync(Blog blog)
    {
        _context.Blogs.Update(blog);
    }
}

using A.Domain.Entities;
using B.Repository.Repositories;
using D.Dal.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace D.Dal.SqlServer.Infrastructure;

public class SqlUserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public SqlUserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        user.CreatedDate = DateTime.Now;
        await _context.Users.AddAsync(user, cancellationToken);
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken)
    {
        user.Isdeleted = true;
        _context.Users.Update(user);
    }

    public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.Where(x => x.Email.ToLower() == email.ToLower() && !x.Isdeleted).FirstOrDefaultAsync();
    }

    public IQueryable<User> GetAllAsQueryable()
    {
        return _context.Users.Where(x => !x.Isdeleted).OrderByDescending(x => x.CreatedDate).AsQueryable();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        // return await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
    }
}

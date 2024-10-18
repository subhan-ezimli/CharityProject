using A.Domain.Entities;
using B.Repository.Repositories;
using D.Dal.SqlServer.Context;
using Microsoft.EntityFrameworkCore;

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
        _context.Users.Remove(user);
    }

    public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.Where(x=>x.Email==email).FirstOrDefaultAsync();
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Users.FindAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
    }
}

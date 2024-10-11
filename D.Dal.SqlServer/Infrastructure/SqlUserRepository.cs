using A.Domain.Entities;
using B.Repository.Repositories;
using D.Dal.SqlServer.Context;

namespace D.Dal.SqlServer.Infrastructure;

public class SqlUserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public SqlUserRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(User user)
    {
        user.CreatedDate = DateTime.Now;
        await _context.Users.AddAsync(user);
    }

    public async Task DeleteAsync(User user)
    {
        user.Isdeleted = true;
        _context.Users.Remove(user);
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
    }
}

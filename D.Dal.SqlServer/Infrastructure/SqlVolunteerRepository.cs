using A.Domain.Entities;
using B.Repository.Repositories;
using D.Dal.SqlServer.Context;
using Microsoft.EntityFrameworkCore;

namespace D.Dal.SqlServer.Infrastructure
{
    public class SqlVolunteerRepository : IVolunteerRepository
    {
        private readonly AppDbContext _context;
        public SqlVolunteerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Volunteer volunteer, CancellationToken cancellationToken)
        {
            volunteer.CreatedDate = DateTime.Now;
            await _context.Volunteers.AddAsync(volunteer, cancellationToken);
        }

        public async Task DeleteAsync(Volunteer volunteer, CancellationToken cancellationToken)
        {
            volunteer.Isdeleted = true;
            _context.Volunteers.Update(volunteer);
        }

        public IQueryable<Volunteer> GetAllAsQueryable()
        {
            return _context.Volunteers.Where(x => !x.Isdeleted).OrderByDescending(x => x.CreatedDate).AsQueryable();
        }

        public async Task<Volunteer?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var volunteer = await _context.Volunteers.FirstOrDefaultAsync(x => x.Id == id && !x.Isdeleted);
            return volunteer;
        }

        public async Task UpdateAsync(Volunteer volunteer, CancellationToken cancellationToken)
        {
            _context.Volunteers.Update(volunteer);
        }
    }
}

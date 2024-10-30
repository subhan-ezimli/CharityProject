using A.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace D.Dal.SqlServer.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<UploadFile> UploadFiles { get; set; }
    public DbSet<HelpRequest> HelpRequests { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

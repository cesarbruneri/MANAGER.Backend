using MANAGER.Backend.Core.Domain.Entities.Users;
using MANAGER.Backend.Sql.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace MANAGER.Backend.Sql.Infrastructure.Context;

public class ManagerContext : DbContext
{
    public ManagerContext(DbContextOptions<ManagerContext> opt)
        : base(opt) 
    { 
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}

using MANAGER.Backend.Core.Domain.Entities.UserPermissions;
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

    public DbSet<UserPermission> UserPermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserPermissionEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}

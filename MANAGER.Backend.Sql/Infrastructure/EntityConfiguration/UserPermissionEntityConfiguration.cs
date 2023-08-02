using MANAGER.Backend.Core.Domain.Entities.UserPermissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MANAGER.Backend.Sql.Infrastructure.EntityConfiguration;

public class UserPermissionEntityConfiguration : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.ToTable(nameof(UserPermission));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Role)
            .HasConversion<string>();

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Permissions)
            .HasForeignKey(x => x.UserId);
    }
}

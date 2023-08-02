using MANAGER.Backend.Core.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MANAGER.Backend.Sql.Infrastructure.EntityConfiguration;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(x => x.Password)
            .HasColumnType("varchar(100)");

        builder
            .HasMany(u => u.Permissions)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
    }
}

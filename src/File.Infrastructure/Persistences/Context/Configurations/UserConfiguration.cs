using File.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace File.Infrastructure.Persistences.Context.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__User__3214EC07B6705DDE");

        builder.ToTable("User");

        builder.Property(e => e.CreatedDate).HasColumnType("datetime");
        builder.Property(e => e.Email).HasMaxLength(100);
        builder.Property(e => e.ModifiedDate).HasColumnType("datetime");
        builder.Property(e => e.Name).HasMaxLength(50);
        builder.Property(e => e.PasswordHash).HasMaxLength(255);
        builder.Property(e => e.PasswordSalt).HasMaxLength(255);
    }
}

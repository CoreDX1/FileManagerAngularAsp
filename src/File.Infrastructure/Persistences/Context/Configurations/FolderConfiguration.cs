using File.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace File.Infrastructure.Persistences.Context.Configurations;

public class FolderConfiguration : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Folder__3214EC072F272015");

        builder.ToTable("Folder");

        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.CreateDate).HasColumnType("datetime");
        builder.Property(e => e.DeletedDate).HasColumnType("datetime");
        builder.Property(e => e.Name).HasMaxLength(255);

        builder
            .HasOne(d => d.User)
            .WithMany(p => p.Folders)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__Folder__UserId__5070F446");
    }
}

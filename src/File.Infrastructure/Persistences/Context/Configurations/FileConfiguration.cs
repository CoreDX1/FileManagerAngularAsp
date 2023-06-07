using File.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace File.Infrastructure.Persistences.Context.Configurations;

public class FileConfiguration : IEntityTypeConfiguration<Files>
{
    public void Configure(EntityTypeBuilder<Files> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__File__3214EC07F36BFCE1");

        builder.ToTable("File");

        builder.Property(e => e.CreateDate).HasColumnType("datetime");
        builder.Property(e => e.DeletedDate).HasColumnType("datetime");
        builder.Property(e => e.FileFormat).HasMaxLength(50);
        builder.Property(e => e.Name).HasMaxLength(255);

        builder
            .HasOne(d => d.Folder)
            .WithMany(p => p.Files)
            .HasForeignKey(d => d.FolderId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__File__FolderId__628FA481");

        builder
            .HasOne(d => d.User)
            .WithMany(p => p.Files)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__File__UserId__619B8048");
    }
}

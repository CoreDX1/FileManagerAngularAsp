using Microsoft.EntityFrameworkCore;

namespace File.Domain.Entities;

public partial class FileManagerContext : DbContext
{
    public FileManagerContext() { }

    public FileManagerContext(DbContextOptions<FileManagerContext> options)
        : base(options) { }

    public virtual DbSet<File> Files { get; set; }
    public virtual DbSet<Folder> Folders { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(
            "Server=DESKTOP-5QTNUAM\\SQLEXPRESS;Database=FileManager;TrustServerCertificate=True;User id=sa;Password=index"
        );

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<File>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__File__3214EC07292BAAC5");

            entity.ToTable("File");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.FileFormat).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(255);

            entity
                .HasOne(d => d.Folder)
                .WithMany(p => p.Files)
                .HasForeignKey(d => d.FolderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__File__FolderId__5441852A");

            entity
                .HasOne(d => d.User)
                .WithMany(p => p.Files)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__File__UserId__534D60F1");
        });

        modelBuilder.Entity<Folder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Folder__3214EC072F272015");

            entity.ToTable("Folder");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);

            entity
                .HasOne(d => d.User)
                .WithMany(p => p.Folders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Folder__UserId__5070F446");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC072FEF8747");

            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PasswordSalt).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

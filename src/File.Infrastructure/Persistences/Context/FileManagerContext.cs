using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace File.Domain.Entities;

public partial class FileManagerContext : DbContext
{
    public FileManagerContext(DbContextOptions<FileManagerContext> options)
        : base(options) { }

    public virtual DbSet<Files> Files { get; set; }
    public virtual DbSet<Folder> Folders { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

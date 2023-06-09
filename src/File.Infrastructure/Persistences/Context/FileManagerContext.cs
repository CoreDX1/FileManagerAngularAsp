using System.Reflection;
using File.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace File.Infrastructure.Persistences.Context;

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

    internal object AsNoTracking()
    {
        throw new NotImplementedException();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

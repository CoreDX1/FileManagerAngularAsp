using File.Infrastructure.Persistences.Context;
using File.Infrastructure.Persistences.Interfaces;

namespace File.Infrastructure.Persistences.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly FileManagerContext _context;
    public IFolderRepository FolderRepository { get; }

    public UnitOfWork(IFolderRepository folderRepository, FileManagerContext context)
    {
        this._context = context;
        this.FolderRepository = folderRepository;
    }

    public void Dispose()
    {
        this._context.Dispose();
    }

    public void SaveChanges()
    {
        this._context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await this._context.SaveChangesAsync();
    }
}

using File.Infrastructure.Persistences.Context;
using File.Infrastructure.Persistences.Interfaces;

namespace File.Infrastructure.Persistences.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly FileManagerContext _context;
    public IFolderRepository FolderRepository { get; }
    public IFileRepository FileRepository { get; }
    public IUserRepository UserRepository { get; }

    public UnitOfWork(
        IFolderRepository folderRepository,
        FileManagerContext context,
        IUserRepository userRepository
    )
    {
        this._context = context;
        this.FolderRepository = new FolderRepository(context);
        this.FileRepository = new FileRepository(context);
        this.UserRepository = new UserRepository(context);
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

namespace File.Infrastructure.Persistences.Interfaces;

public interface IUnitOfWork
{
    IFolderRepository FolderRepository { get; }
    IFileRepository FileRepository { get; }
    IUserRepository UserRepository { get; }
    void SaveChanges();
    Task SaveChangesAsync();
}

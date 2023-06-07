namespace File.Infrastructure.Persistences.Interfaces;

public interface IUnitOfWork
{
    IFolderRepository FolderRepository { get; }
    void SaveChanges();
    Task SaveChangesAsync();
}

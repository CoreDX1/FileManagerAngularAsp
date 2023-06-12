using File.Domain.Entities;

namespace File.Infrastructure.Persistences.Interfaces;

public interface IFolderRepository
{
    public Task<Folder> GetByName(Folder folder);
    public Task<bool> Create(Folder folder);
    public Task<bool> Update(Folder folder, string path);
    public Folder GetByPath(string path, string name);
    public Task<bool> DeleteFolder(Folder folder);
}

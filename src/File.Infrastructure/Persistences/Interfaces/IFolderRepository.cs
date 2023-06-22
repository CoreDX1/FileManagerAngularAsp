using File.Domain.Entities;

namespace File.Infrastructure.Persistences.Interfaces;

public interface IFolderRepository
{
    public Task<Folder> GetByName(string name, string path);
    public Task<bool> Create(Folder folder);
    public Task<bool> Update(Folder folder, string path);
    public Folder GetByPath(string path, string name);
    public Task<Folder> DeleteFolder(Folder folder);
    public Task<IEnumerable<Folder>> SearchByContent(string searchQuery);
    public string AccountName(string code);
}

using File.Domain.Entities;

namespace File.Infrastructure.Persistences.Interfaces;

public interface IFolderRepository
{
    public Task<Folder> GetByName(Folder folder);
    public Task<bool> Create(Folder folder);
    public Task<bool> Update(string folder);
}

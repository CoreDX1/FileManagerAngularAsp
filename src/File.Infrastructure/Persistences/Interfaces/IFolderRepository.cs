using File.Domain.Entities;

namespace File.Infrastructure.Persistences.Interfaces;

public interface IFolderRepository
{
    public Task<bool> GetByName(string name);
    public Task<bool> Create(Folder folder);
    public Task<bool> Update(string folder);
}

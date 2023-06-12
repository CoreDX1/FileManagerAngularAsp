using File.Domain.Entities;

namespace File.Infrastructure.Persistences.Interfaces;

public interface IFileRepository
{
    public Task<bool> Create(Files files);
    public Task<bool> Update(string file);
    public Files GetByPath(string path, string name);
    public Task<Files> GetByName(Files folder);
    public Task<bool> Delete(string file);
}

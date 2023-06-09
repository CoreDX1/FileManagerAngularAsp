using File.Domain.Entities;
using File.Infrastructure.Persistences.Context;
using File.Infrastructure.Persistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace File.Infrastructure.Persistences.Repository;

public class FileRepository : IFileRepository
{
    private readonly FileManagerContext _context;

    public FileRepository(FileManagerContext context)
    {
        _context = context;
    }

    public async Task<bool> Create(Files files)
    {
        await this._context.Files.AddAsync(files);
        await this._context.SaveChangesAsync();
        return true;
    }

    public async Task<Files> GetByName(Files folder)
    {
        Files? file = await this._context.Files.FirstOrDefaultAsync(
            x => x.Name == folder.Name && x.Path == folder.Path
        );
        return file!;
    }

    public Files GetByPath(string path, string name)
    {
        Files? file = this._context.Files
            .AsNoTracking()
            .FirstOrDefault(x => x.Name == name && x.Path == path);
        return file!;
    }

    public Task<bool> Update(string file)
    {
        throw new NotImplementedException();
    }
}

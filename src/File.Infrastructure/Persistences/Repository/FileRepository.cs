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

    public async Task<Files> GetByName(Files folder)
    {
        Files? file = await _context.Files.FirstOrDefaultAsync(
            x => x.Name == folder.Name && x.Path == folder.Path
        );
        return file!;
    }

    public Files GetByPath(string path, string name)
    {
        Files? file = _context.Files
            .AsNoTracking()
            .FirstOrDefault(x => x.Name == name && x.Path == path);
        return file!;
    }

    public async Task<bool> Create(Files files)
    {
        await _context.Files.AddAsync(files);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<bool> Delete(string file)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(string file)
    {
        throw new NotImplementedException();
    }
}

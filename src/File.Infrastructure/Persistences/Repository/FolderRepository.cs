using File.Domain.Entities;
using File.Infrastructure.Persistences.Context;
using File.Infrastructure.Persistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace File.Infrastructure.Persistences.Repository;

public class FolderRepository : IFolderRepository
{
    private readonly FileManagerContext _context;
    private readonly string baseDirectory;

    public FolderRepository(FileManagerContext context)
    {
        this._context = context;
        this.baseDirectory = @"C:\Users\Christian\Desktop\File";
    }

    public async Task<bool> Create(Folder folder)
    {
        await _context.Folders.AddAsync(folder);
        await _context.SaveChangesAsync();
        return true;
    }

    public List<Folder> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Folder> GetByName(Folder folder)
    {
        Folder? result = await this._context.Folders.FirstOrDefaultAsync(
            x => x.Name == folder.Name && x.Path == folder.Path
        );
        return result!;
    }

    public Folder GetByPath(string path, string name)
    {
        var result = this._context.Folders
            .AsNoTracking()
            .Where(x => x.Name == name && x.Path == path);
        return result.FirstOrDefault()!;
    }

    public Task<bool> Update(string folder)
    {
        throw new NotImplementedException();
    }
}

using File.Domain.Entities;
using File.Infrastructure.Persistences.Context;
using File.Infrastructure.Persistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace File.Infrastructure.Persistences.Repository;

public class FolderRepository : IFolderRepository
{
    private readonly FileManagerContext _context;

    public FolderRepository(FileManagerContext context)
    {
        _context = context;
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
        Folder? result = this._context.Folders
            .AsNoTracking()
            .FirstOrDefault(x => x.Name == name && x.Path == path);
        return result!;
    }

    public async Task<bool> Create(Folder folder)
    {
        await _context.Folders.AddAsync(folder);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(Folder folder, string path)
    {
        Folder? result = await _context.Folders.FirstAsync(x => x.Path == path);
        result.Name = folder.Name;
        result.Path = folder.Path;
        _context.Update(result);
        var response = await _context.SaveChangesAsync();
        return response > 0;
    }

    public async Task<bool> DeleteFolder(Folder folder)
    {
        Folder? result = await GetByName(folder);
        // Change the info of the folder
        result.DeletedDate = DateTime.Now;
        result.IsDeleted = true;
        _context.Update(result);
        await _context.SaveChangesAsync();
        return true;
    }
}

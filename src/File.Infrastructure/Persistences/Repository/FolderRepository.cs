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

    public async Task<Folder> GetByName(string name, string path)
    {
        Folder? result = await this._context.Folders.FirstOrDefaultAsync(
            x => x.Name == name && x.Path == path
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

    public string AccountName(string code)
    {
        var name = _context.Users.FirstOrDefault(x => x.PasswordSalt == code);
        return name!.Name;
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

    public async Task<Folder> DeleteFolder(Folder folder)
    {
        Folder? result = await GetByName(folder.Name, folder.Path);
        // Change the info of the folder
        result.DeletedDate = DateTime.Now;
        result.IsDeleted = true;
        _context.Update(result);
        await _context.SaveChangesAsync();
        return result;
    }

    public async Task<IEnumerable<Folder>> SearchByContent(string searchQuery)
    {
        var result = await _context.Folders.Where(x => x.Name.Contains(searchQuery)).ToListAsync();
        return result;
    }
}

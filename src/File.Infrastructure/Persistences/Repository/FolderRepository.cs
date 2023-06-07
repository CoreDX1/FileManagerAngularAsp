using File.Domain.Entities;
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

    public async Task<bool> GetByName(string name)
    {
        Folder? result = await this._context.Folders.FirstOrDefaultAsync(x => x.Name == name);
        if (result == null)
            return false;
        return true;
    }

    public Task<bool> Update(Folder folder)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(string folder)
    {
        throw new NotImplementedException();
    }
}

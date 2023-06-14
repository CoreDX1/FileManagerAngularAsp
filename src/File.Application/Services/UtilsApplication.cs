using System.Web;
using File.Domain.Entities;
using File.Infrastructure.Persistences.Interfaces;

namespace File.Application.Services;

public class UtilsApplication
{
    private readonly string baseDirectory;
    private readonly IUnitOfWork _unitOfWork;

    public UtilsApplication(IUnitOfWork unitOfWork)
    {
        baseDirectory = @"C:\Users\Christian\Desktop\File\";
        _unitOfWork = unitOfWork;
    }

    public string DirectoryExists(string path)
    {
        string decodedPath = HttpUtility.UrlDecode(path);
        string newDirectoryPath = Path.Combine(baseDirectory, decodedPath);
        if (!Directory.Exists(newDirectoryPath))
            return null!;
        return newDirectoryPath;
    }

    public long AllDirectorySize(string[] dir, string[] fil)
    {
        long size = 0;
        foreach (var fileInfo in dir)
        {
            var directoryInfo = new DirectoryInfo(fileInfo);
            size += GetDirectorySize(directoryInfo);
        }
        foreach (var file in fil)
        {
            var fileInfo = new FileInfo(file);
            size += fileInfo.Length;
        }
        return size;
    }

    // TODO : Method that used recursively
    public long GetDirectorySize(DirectoryInfo directoryInfo)
    {
        long size = 0;
        var files = directoryInfo.GetFiles();
        foreach (var file in files)
        {
            size += file.Length;
        }
        var subdirectories = directoryInfo.GetDirectories();
        foreach (var subdirectory in subdirectories)
        {
            size += GetDirectorySize(subdirectory);
        }
        return size;
    }

    public Folder ViewFolder(string path, string name)
    {
        var folderInfo = _unitOfWork.FolderRepository.GetByPath(path, name);
        return new Folder()
        {
            Name = folderInfo.Name,
            UserId = folderInfo.UserId,
            Path = folderInfo.Path,
            CreateDate = folderInfo.CreateDate,
        };
    }
}

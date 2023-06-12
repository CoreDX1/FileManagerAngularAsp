using System.Web;
using AutoMapper;
using File.Application.Commons.Base;
using File.Application.DTO.Request.Folder;
using File.Application.DTO.Response.Folder;
using File.Application.Interface;
using File.Domain.Entities;
using File.Infrastructure.Persistences.Interfaces;
using File.Utilities.Static;

namespace File.Application.Services;

public class FolderApplication : IFolderApplication
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly string baseDirectory;

    public FolderApplication(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        baseDirectory = @"C:\Users\Christian\Desktop\";
    }

    public BaseResponse GetRoot(string path)
    {
        var response = new BaseResponse();
        string directoryPath = DirectoryExists(path);

        // TODO : check if directory exists
        if (directoryPath == null)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            return response;
        }

        // TODO : check if directory is empty
        string[] fileNames = Directory.GetFiles(directoryPath);
        string[] directoryNames = Directory.GetDirectories(directoryPath);

        IList<FolderResponseDto> folderTasks = new List<FolderResponseDto>();
        foreach (string dir in directoryNames)
        {
            var folder = ViewFolder(dir, Path.GetFileName(dir));
            folderTasks.Add(folder);
        }

        folderTasks = folderTasks
            .Where(x => x.IsDeleted == false)
            .OrderByDescending(x => x.CreateDate)
            .ToList();

        var rootResponse = new RootResponseDto
        {
            Path = directoryPath,
            TotalSize = AllDirectorySize(directoryNames, fileNames),
            Author = "Christian",
            Directories = folderTasks,
            LastModified = Directory.GetLastWriteTime(directoryPath),
        };

        response.Success = true;
        response.Message = ReplyMessage.MESSAGE_QUERY_SUCCESS;
        response.Data = rootResponse;
        return response;
    }

    private string DirectoryExists(string path)
    {
        string decodedPath = HttpUtility.UrlDecode(path);
        string directoryPath = Path.Combine(baseDirectory, decodedPath.Replace('/', '\\'));
        if (!Directory.Exists(directoryPath))
            return null!;
        return directoryPath;
    }

    private long AllDirectorySize(string[] dir, string[] fil)
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
    private long GetDirectorySize(DirectoryInfo directoryInfo)
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

    private FolderResponseDto ViewFolder(string path, string name)
    {
        var folderInfo = _unitOfWork.FolderRepository.GetByPath(path, name);
        return new FolderResponseDto()
        {
            Name = folderInfo.Name,
            UserId = folderInfo.UserId,
            Path = folderInfo.Path,
            CreateDate = folderInfo.CreateDate,
            DeletedDate = folderInfo.DeletedDate,
            IsDeleted = folderInfo.IsDeleted
        };
    }

    public async Task<BaseResponse> CreateFolder(FolderRequestDto folderRequest)
    {
        var response = new BaseResponse();
        Folder folder = _mapper.Map<Folder>(folderRequest);
        var validate = await GetByName(folderRequest);

        Directory.CreateDirectory(Path.Combine(folder.Path));
        bool create = await _unitOfWork.FolderRepository.Create(folder);
        if (!create && !validate.Success)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_SAVE_ERROR;
        }
        else
        {
            response.Success = true;
            response.Message = ReplyMessage.MESSAGE_SAVE_SUCCESS;
        }
        return response;
    }

    public async Task<BaseResponse> GetByName(FolderRequestDto folderRequest)
    {
        var response = new BaseResponse();
        Folder folder = _mapper.Map<Folder>(folderRequest);
        var result = await _unitOfWork.FolderRepository.GetByName(folder);
        if (result != null)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
        }
        else
        {
            response.Success = true;
            response.Message = ReplyMessage.MESSAGE_QUERY_SUCCESS;
        }
        return response;
    }

    public async Task<BaseResponse> Delete(FolderRequestDto folderRequest)
    {
        var response = new BaseResponse();
        Folder folder = _mapper.Map<Folder>(folderRequest);
        var result = await _unitOfWork.FolderRepository.DeleteFolder(folder);
        if (!result)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_DELETE_ERROR;
        }
        else
        {
            response.Success = true;
            response.Message = ReplyMessage.MESSAGE_DELETE_SUCCESS;
        }
        return response;
    }
}

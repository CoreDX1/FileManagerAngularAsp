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
    private readonly string _baseDirectory;
    private readonly UtilsApplication _utilsApplication;

    public FolderApplication(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        UtilsApplication utilsApplication
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _baseDirectory = @"C:\Users\Christian\Desktop\File\";
        _utilsApplication = utilsApplication;
    }

    public BaseResponse<RootResponseDto> CloneGetRoot(string name)
    {
        var response = new BaseResponse<RootResponseDto>();

        string directoryPath = _utilsApplication.DirectoryExists(name);
        if (directoryPath == null)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            return response;
        }

        string[] fileNames = Directory.GetFiles(directoryPath);
        string[] directoryNames = Directory.GetDirectories(directoryPath);

        IList<Folder> folderTasks = new List<Folder>();
        foreach (string dir in directoryNames)
        {
            var folder = _utilsApplication.ViewFolder(dir, Path.GetFileName(dir));
            folderTasks.Add(folder);
        }

        folderTasks = folderTasks
            .Where(x => x.IsDeleted == false)
            .OrderByDescending(x => x.CreateDate)
            .ToList();

        var rootResponse = new RootResponseDto
        {
            Path = directoryPath,
            TotalSize = _utilsApplication.AllDirectorySize(directoryNames, fileNames),
            Author = name,
            Directories = _mapper.Map<IEnumerable<FolderResponseDto>>(folderTasks),
            LastModified = Directory.GetLastWriteTime(directoryPath),
        };

        return new BaseResponse<RootResponseDto>()
        {
            Success = true,
            Message = ReplyMessage.MESSAGE_QUERY_SUCCESS,
            Data = rootResponse
        };
    }

    public BaseResponse<RootResponseDto> GetRoot(string name, string file)
    {
        var response = new BaseResponse<RootResponseDto>();
        // TODO : check if directory exists == gmail

        string directoryPath = _utilsApplication.DirectoryExists(name);

        if (directoryPath == null)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            return response;
        }

        // TODO : check if directory is empty
        string decodedPath = HttpUtility.UrlDecode(file);
        string newDirectoryPath = Path.Combine(directoryPath, decodedPath.Replace('/', '\\'));

        // TODO : check if directory is empty
        string[] fileNames = Directory.GetFiles(newDirectoryPath);
        string[] directoryNames = Directory.GetDirectories(newDirectoryPath);

        IList<Folder> folderTasks = new List<Folder>();
        foreach (string dir in directoryNames)
        {
            var folder = _utilsApplication.ViewFolder(dir, Path.GetFileName(dir));
            folderTasks.Add(folder);
        }

        folderTasks = folderTasks
            .Where(x => x.IsDeleted == false)
            .OrderByDescending(x => x.CreateDate)
            .ToList();

        var rootResponse = new RootResponseDto
        {
            Path = newDirectoryPath,
            TotalSize = _utilsApplication.AllDirectorySize(directoryNames, fileNames),
            Author = name,
            Directories = _mapper.Map<IEnumerable<FolderResponseDto>>(folderTasks),
            LastModified = Directory.GetLastWriteTime(directoryPath),
        };

        return new BaseResponse<RootResponseDto>()
        {
            Success = true,
            Message = ReplyMessage.MESSAGE_QUERY_SUCCESS,
            Data = rootResponse
        };
    }

    public async Task<BaseResponse<Folder>> CreateFolder(FolderCreateRequestDto folderRequest)
    {
        var response = new BaseResponse<Folder>();
        Folder folder = _mapper.Map<Folder>(folderRequest);
        string comPath = Path.Combine(_baseDirectory, folderRequest.Path!);
        var existingFolder = await GetByName(folderRequest.Name!, comPath);
        if (!existingFolder.Success)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_SUCCESS;
            return response;
        }
        Directory.CreateDirectory(Path.Combine(comPath));
        folder.Path = comPath;
        bool isFolderCreated = await _unitOfWork.FolderRepository.Create(folder);
        if (!isFolderCreated)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_SAVE_ERROR;
        }
        else
        {
            response.Success = true;
            response.Message = ReplyMessage.MESSAGE_SAVE_SUCCESS;
            response.Data = folder;
        }
        return response;
    }

    public async Task<BaseResponse<Folder>> GetByName(string name, string path)
    {
        var response = new BaseResponse<Folder>();
        Folder? result = await _unitOfWork.FolderRepository.GetByName(name, path);
        if (result != null)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
        }
        else
        {
            response.Success = true;
            response.Message = ReplyMessage.MESSAGE_QUERY_SUCCESS;
            response.Data = result;
        }
        return response;
    }

    public async Task<BaseResponse<FolderResponseDto>> Delete(FolderRequestDto folderRequest)
    {
        var response = new BaseResponse<FolderResponseDto>();
        Folder folder = _mapper.Map<Folder>(folderRequest);
        var result = await _unitOfWork.FolderRepository.DeleteFolder(folder);
        if (result == null)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_DELETE_ERROR;
        }
        else
        {
            response.Success = true;
            response.Message = ReplyMessage.MESSAGE_DELETE_SUCCESS;
            response.Data = _mapper.Map<FolderResponseDto>(result);
        }
        return response;
    }

    public async Task<BaseResponse<IEnumerable<Folder>>> SearchByContent(string searchQuery)
    {
        var response = new BaseResponse<IEnumerable<Folder>>();
        var result = await _unitOfWork.FolderRepository.SearchByContent(searchQuery);
        if (result == null)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
        }
        else
        {
            response.Success = true;
            response.Message = ReplyMessage.MESSAGE_QUERY_SUCCESS;
            response.Data = result;
        }
        return response;
    }
}

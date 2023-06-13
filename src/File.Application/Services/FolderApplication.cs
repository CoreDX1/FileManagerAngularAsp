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
    private readonly UtilsApplication _utilsApplication;

    public FolderApplication(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        UtilsApplication utilsApplication
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        baseDirectory = @"C:\Users\Christian\Desktop\";
        _utilsApplication = utilsApplication;
    }

    public BaseResponse<RootResponseDto> GetRoot(string path)
    {
        var response = new BaseResponse<RootResponseDto>();
        string directoryPath = _utilsApplication.DirectoryExists(path);

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
            Author = "Christian",
            Directories = _mapper.Map<IEnumerable<FolderResponseDto>>(folderTasks),
            LastModified = Directory.GetLastWriteTime(directoryPath),
        };

        response.Success = true;
        response.Message = ReplyMessage.MESSAGE_QUERY_SUCCESS;
        response.Data = rootResponse;
        return response;
    }

    public async Task<BaseResponse<Folder>> CreateFolder(FolderCreateRequestDto folderRequest)
    {
        var response = new BaseResponse<Folder>();
        Folder folder = _mapper.Map<Folder>(folderRequest);
        var validate = await GetByName(folderRequest.Name!, folderRequest.Path!);
        if (!validate.Success)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_SUCCESS;
            return response;
        }
        Directory.CreateDirectory(Path.Combine(folder.Path));
        bool create = await _unitOfWork.FolderRepository.Create(folder);
        if (!create)
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

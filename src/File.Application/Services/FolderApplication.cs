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
        this._unitOfWork = unitOfWork;
        this._mapper = mapper;
        this.baseDirectory = @"C:\Users\Christian\Desktop\";
    }

    public RootResponseDto GetRoot(string path)
    {
        var response = new RootResponseDto();
        string decodedPath = HttpUtility.UrlDecode(path);
        string directoryPath = Path.Combine(baseDirectory, decodedPath);
        if (!Directory.Exists(directoryPath))
        {
            response.IsSuccess = false;
            return response;
        }
        string[] fileNames = Directory.GetFiles(directoryPath);
        string[] directoryNames = Directory.GetDirectories(directoryPath);
        response.IsSuccess = true;
        response.Directories = directoryNames.Select(Path.GetFileName).ToList()!;
        response.Files = fileNames.Select(Path.GetFileName).ToList()!;
        return response;
    }

    public async Task<BaseResponse> CreateFolder(FolderRequestDto folderRequest)
    {
        var response = new BaseResponse();
        Folder folder = this._mapper.Map<Folder>(folderRequest);
        var validate = await GetByName(folderRequest);
        if (validate is not null)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_EXISTS;
            return response;
        }
        bool create = await this._unitOfWork.FolderRepository.Create(folder);
        if (!create)
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
        Folder folder = this._mapper.Map<Folder>(folderRequest);
        var result = await this._unitOfWork.FolderRepository.GetByName(folder);
        if (result is null)
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
}

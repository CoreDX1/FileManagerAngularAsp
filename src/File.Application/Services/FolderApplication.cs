using AutoMapper;
using File.Application.Commons.Base;
using File.Application.DTO.Request.Folder;
using File.Application.Interface;
using File.Domain.Entities;
using File.Infrastructure.Persistences.Interfaces;

namespace File.Application.Services;

public class FolderApplication : IFolderApplication
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FolderApplication(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        this._mapper = mapper;
    }

    public async Task<BaseResponse> CreateFolder(FolderRequestDto folderRequest)
    {
        var response = new BaseResponse();
        Folder folder = this._mapper.Map<Folder>(folderRequest);
        bool create = await this._unitOfWork.FolderRepository.Create(folder);
        if (create)
        {
            response.Success = true;
            response.Message = "Success";
        }
        else
        {
            response.Success = false;
            response.Message = "Failed";
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
            response.Message = "La carpeta no se encuentra";
        }
        else
        {
            response.Success = true;
            response.Message = "La carpeta se encuentra";
        }
        return response;
    }
}

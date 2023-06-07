using File.Application.Commons.Base;
using File.Application.DTO.Request.Folder;
using File.Application.Interface;
using File.Infrastructure.Persistences.Interfaces;

namespace File.Application.Services;

public class FolderApplication : IFolderApplication
{
    private readonly IUnitOfWork _unitOfWork;

    public FolderApplication(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse> CreateFolder(FolderRequestDto name)
    {
        var response = new BaseResponse();
        bool create = await this._unitOfWork.FolderRepository.Create(name);
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
}

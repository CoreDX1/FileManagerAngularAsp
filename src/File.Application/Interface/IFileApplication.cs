using File.Application.Commons.Base;
using File.Application.DTO.Request.File;

namespace File.Application.Interface;

public interface IFileApplication
{
    Task<BaseResponse> CreateFile(FileRequestDto fileRequest);
    Task<BaseResponse> GetByName(FileRequestDto fileRequest);
}

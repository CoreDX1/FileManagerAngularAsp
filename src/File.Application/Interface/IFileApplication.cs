using File.Application.Commons.Base;
using File.Application.DTO.Request.File;
using File.Domain.Entities;

namespace File.Application.Interface;

public interface IFileApplication
{
    Task<BaseResponse<Files>> CreateFile(FileRequestDto fileRequest);
    BaseResponse<Files> GetByName(FileRequestDto fileRequest);
}

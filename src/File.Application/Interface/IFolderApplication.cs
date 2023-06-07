using File.Application.Commons.Base;
using File.Application.DTO.Request.Folder;

namespace File.Application.Interface;

public interface IFolderApplication
{
    Task<BaseResponse> CreateFolder(FolderRequestDto name);
}

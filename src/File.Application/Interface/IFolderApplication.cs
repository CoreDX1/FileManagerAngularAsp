using File.Application.Commons.Base;
using File.Application.DTO.Request.Folder;
using File.Application.DTO.Response.Folder;

namespace File.Application.Interface;

public interface IFolderApplication
{
    Task<BaseResponse> CreateFolder(FolderRequestDto folderRequest);
    Task<BaseResponse> GetByName(FolderRequestDto folderRequest);
    RootResponseDto GetRoot(string path);
}

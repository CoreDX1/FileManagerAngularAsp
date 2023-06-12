using File.Application.Commons.Base;
using File.Application.DTO.Request.Folder;

namespace File.Application.Interface;

public interface IFolderApplication
{
    Task<BaseResponse> CreateFolder(FolderRequestDto folderRequest);
    Task<BaseResponse> GetByName(FolderRequestDto folderRequest);
    BaseResponse GetRoot(string path);
    Task<BaseResponse> Delete(FolderRequestDto folderRequest);
}

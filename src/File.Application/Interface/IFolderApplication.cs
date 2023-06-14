using File.Application.Commons.Base;
using File.Application.DTO.Request.Folder;
using File.Application.DTO.Response.Folder;
using File.Domain.Entities;

namespace File.Application.Interface;

public interface IFolderApplication
{
    Task<BaseResponse<Folder>> CreateFolder(FolderCreateRequestDto folderRequest);
    Task<BaseResponse<Folder>> GetByName(string name, string path);
    BaseResponse<RootResponseDto> GetRoot(string user, string file);
    Task<BaseResponse<FolderResponseDto>> Delete(FolderRequestDto folderRequest);
    Task<BaseResponse<IEnumerable<Folder>>> SearchByContent(string searchQuery);
    BaseResponse<RootResponseDto> CloneGetRoot(string name);
}

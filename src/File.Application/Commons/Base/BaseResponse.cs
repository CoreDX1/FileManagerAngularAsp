using File.Application.DTO.Response.File;
using File.Application.DTO.Response.Folder;

namespace File.Application.Commons.Base;

public class BaseResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public List<FileResponseDto>? Files { get; set; }
    public List<FolderResponseDto>? Folders { get; set; }
}

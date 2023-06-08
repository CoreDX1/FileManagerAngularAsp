using File.Application.DTO.Response.File;

namespace File.Application.DTO.Response.Folder;

public class RootResponseDto
{
    public bool IsSuccess { get; set; }
    public IEnumerable<FolderResponseDto>? Directories { get; set; }
    public List<FileResponseDto>? Files { get; set; }
}

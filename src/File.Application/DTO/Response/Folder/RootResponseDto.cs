using File.Application.DTO.Response.File;

namespace File.Application.DTO.Response.Folder;

public class RootResponseDto
{
    public string? Path { get; set; }
    public IEnumerable<FolderResponseDto> Directories { get; set; } = new List<FolderResponseDto>();
    public IEnumerable<FileResponseDto> Files { get; set; } = new List<FileResponseDto>();

    // TODO : Additional information about the root directory
    public long TotalSize { get; set; }
    public DateTime LastModified { get; set; }
    public string? Author { get; set; }

    // TODO : Stadistic about the files and directories
    public int FileCount => Files?.Count() ?? 0;
    public int DirectoryCount => Directories?.Count() ?? 0;
}

namespace File.Application.DTO.Response.Folder;

public class RootResponseDto
{
    public bool IsSuccess { get; set; }
    public List<string>? Directories { get; set; }
    public List<string>? Files { get; set; }
}

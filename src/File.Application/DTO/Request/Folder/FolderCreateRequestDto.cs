namespace File.Application.DTO.Request.Folder;

public class FolderCreateRequestDto
{
    public string? Name { get; set; }
    public string? Path { get; set; }
    public int UserId { get; set; }
}

public record FolderCreateRequestDtoa(string? Name, string? Path, int UserId);

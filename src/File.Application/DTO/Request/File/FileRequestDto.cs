namespace File.Application.DTO.Request.File;

public class FileRequestDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Path { get; set; } = null!;
    public string FileFormat { get; set; } = null!;
    public int UserId { get; set; }
    public int FolderId { get; set; }
    public bool IsDeleted { get; set; }
}

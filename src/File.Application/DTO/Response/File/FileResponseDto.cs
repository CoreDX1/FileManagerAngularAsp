namespace File.Application.DTO.Response.File;

public class FileResponseDto
{
    public string? Name { get; set; }
    public string? Path { get; set; }
    public DateTime CreateDate { get; set; }
    public short Size { get; set; }
    public string? FileFormat { get; set; }
    public int UserId { get; set; }
    public int FolderId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }
}

namespace File.Application.DTO.Response.Folder;

public class FolderResponseDto
{
    public string? Name { get; set; }
    public string? Path { get; set; }
    public short Size { get; set; }
    public DateTime CreateDate { get; set; }
    public int UserId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }
}

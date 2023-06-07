namespace File.Application.DTO.Request.Folder;

public class FolderRequestDto
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

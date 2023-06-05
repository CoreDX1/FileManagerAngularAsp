namespace File.Domain.Entities;

public partial class File
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Path { get; set; } = null!;
    public DateTime CreateDate { get; set; }
    public short Size { get; set; }
    public string FileFormat { get; set; } = null!;
    public int UserId { get; set; }
    public int FolderId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }
    public virtual Folder Folder { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}

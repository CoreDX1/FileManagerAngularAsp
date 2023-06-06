namespace File.Domain.Entities;

public partial class Folder
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Path { get; set; } = null!;
    public short Size { get; set; }
    public DateTime CreateDate { get; set; }
    public int UserId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }
    public virtual ICollection<Files> Files { get; set; } = new List<Files>();
    public virtual User User { get; set; } = null!;
}

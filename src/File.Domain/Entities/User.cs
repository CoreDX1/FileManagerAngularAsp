namespace File.Domain.Entities;

public partial class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<Files> Files { get; set; } = new List<Files>();
    public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();
}

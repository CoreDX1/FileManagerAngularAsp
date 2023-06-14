namespace File.Application.DTO.Response.User;

public class UserRegisterResponseDto
{
    public string Name { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
}

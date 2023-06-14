namespace File.Application.DTO.Request.User;

public class UserRegisterRequestDto
{
    public string Name { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;
    public string Email { get; set; } = null!;
}

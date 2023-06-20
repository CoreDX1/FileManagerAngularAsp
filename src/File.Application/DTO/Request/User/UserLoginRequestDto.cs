namespace File.Application.DTO.Request.User;

public class UserLoginRequestDto
{
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
}

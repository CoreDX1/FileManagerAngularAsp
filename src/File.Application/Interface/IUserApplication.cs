using File.Application.Commons.Base;
using File.Application.DTO.Request.User;
using File.Application.DTO.Response.User;

namespace File.Application.Interface;

public interface IUserApplication
{
    Task<BaseResponse<UserRegisterResponseDto>> AddUser(UserRegisterRequestDto user);
    Task<BaseResponse<string>> LoginUser(UserLoginRequestDto user);
    Task<BaseResponse<UserRegisterResponseDto>> GetUserByEmail(string email);
}

using AutoMapper;
using File.Application.Commons.Base;
using File.Application.DTO.Request.User;
using File.Application.DTO.Response.User;
using File.Application.Interface;
using File.Domain.Entities;
using File.Infrastructure.Persistences.Interfaces;
using File.Utilities.Static;

namespace File.Application.Services;

public class UserApplication : IUserApplication
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserApplication(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<UserRegisterResponseDto>> AddUser(UserRegisterRequestDto user)
    {
        var response = new BaseResponse<UserRegisterResponseDto>();

        var validUser = await GetUserByEmail(user.Email);
        if (!validUser.Success)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_OPERATION_FAILED;
            return response;
        }

        User userEntity = this._mapper.Map<User>(user);
        string path = @"C:\Users\Christian\Desktop\File";

        userEntity.PasswordSalt = Guid.NewGuid().ToString().Replace("-", "");
        var combinatePath = Path.Combine(path, userEntity.PasswordSalt);
        Directory.CreateDirectory(combinatePath);
        bool result = await this._userRepository.AddUser(userEntity);
        if (!result)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_SAVE_ERROR;
        }
        else
        {
            response.Success = true;
            response.Message = ReplyMessage.MESSAGE_SAVE_SUCCESS;
            response.Data = this._mapper.Map<UserRegisterResponseDto>(userEntity);
        }
        return response;
    }

    public async Task<BaseResponse<UserRegisterResponseDto>> GetUserByEmail(string email)
    {
        var response = new BaseResponse<UserRegisterResponseDto>();
        User? user = await this._userRepository.GetUserByEmail(email);
        if (user != null)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
        }
        else
        {
            response.Success = true;
            response.Message = ReplyMessage.MESSAGE_QUERY_SUCCESS;
            response.Data = this._mapper.Map<UserRegisterResponseDto>(user);
        }
        return response;
    }

    public async Task<BaseResponse<string>> LoginUser(UserLoginRequestDto user)
    {
        var response = new BaseResponse<string>();
        User userMapper = this._mapper.Map<User>(user);
        string code = await this._userRepository.LoginUser(userMapper);
        if (code == null)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_AUTH_TYPE;
        }
        else
        {
            response.Success = true;
            response.Message = ReplyMessage.MESSAGE_AUTH_SUCCESS;
            response.Data = code;
        }
        return response;
    }
}

using AutoMapper;
using File.Application.DTO.Request.User;
using File.Application.DTO.Response.User;
using File.Domain.Entities;

namespace File.Application.Mappers.UserMapper;

public class UserMapping : Profile
{
    public UserMapping()
    {
        // * Request
        CreateMap<UserRegisterRequestDto, User>();

        // * Response
        CreateMap<User, UserRegisterResponseDto>();
    }
}

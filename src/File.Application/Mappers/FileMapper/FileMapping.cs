using AutoMapper;
using File.Application.DTO.Request.File;
using File.Domain.Entities;

namespace File.Application.Mappers.FileMapper;

public class FileMapping : Profile
{
    public FileMapping()
    {
        CreateMap<FileRequestDto, Files>();
    }
}

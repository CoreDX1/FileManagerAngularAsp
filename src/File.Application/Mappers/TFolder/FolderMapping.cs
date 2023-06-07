using AutoMapper;
using File.Application.DTO.Request.Folder;
using File.Domain.Entities;

namespace File.Application.Mappers.TFolder;

public class FolderMapping : Profile
{
    public FolderMapping()
    {
        // * Request
        CreateMap<FolderRequestDto, Folder>();
    }
}

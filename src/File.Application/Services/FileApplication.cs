using AutoMapper;
using File.Application.Commons.Base;
using File.Application.DTO.Request.File;
using File.Application.Interface;
using File.Domain.Entities;
using File.Infrastructure.Persistences.Interfaces;
using File.Utilities.Static;

namespace File.Application.Services;

public class FileApplication : IFileApplication
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public FileApplication(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<Files>> CreateFile(FileRequestDto fileRequest)
    {
        var response = new BaseResponse<Files>();
        Files file = _mapper.Map<Files>(fileRequest);
        var valid = GetByName(fileRequest);

        // Create File
        bool files = await _unitOfWork.FileRepository.Create(file);
        if (!files && !valid.Success)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_SAVE_ERROR;
        }
        else
        {
            response.Success = true;
            response.Message = ReplyMessage.MESSAGE_SAVE_SUCCESS;
            response.Data = valid.Data;
        }
        return response;
    }

    public BaseResponse<Files> GetByName(FileRequestDto fileRequest)
    {
        var response = new BaseResponse<Files>();
        Files? file = _mapper.Map<Files>(fileRequest);
        Files files = _unitOfWork.FileRepository.GetByPath(file.Path, file.Name);
        if (files == null)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
        }
        else
        {
            response.Success = true;
            response.Message = ReplyMessage.MESSAGE_QUERY_SUCCESS;
            response.Data = files;
        }
        return response;
    }
}

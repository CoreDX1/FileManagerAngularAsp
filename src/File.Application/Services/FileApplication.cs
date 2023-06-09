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
        this._mapper = mapper;
        this._unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse> CreateFile(FileRequestDto fileRequest)
    {
        var response = new BaseResponse();
        Files file = _mapper.Map<Files>(fileRequest);

        BaseResponse valid = await GetByName(fileRequest);
        if (!valid.Success)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            return response;
        }

        // Create File
        bool files = await _unitOfWork.FileRepository.Create(file);
        if (!files)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_SAVE_ERROR;
        }
        else
        {
            response.Success = true;
            response.Message = ReplyMessage.MESSAGE_SAVE_SUCCESS;
        }
        return response;
    }

    public async Task<BaseResponse> GetByName(FileRequestDto fileRequest)
    {
        var response = new BaseResponse();
        Files? file = _mapper.Map<Files>(fileRequest);
        Files files = await this._unitOfWork.FileRepository.GetByName(file);
        if (files == null)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
        }
        else
        {
            response.Success = true;
            response.Message = ReplyMessage.MESSAGE_QUERY_SUCCESS;
        }
        return response;
    }
}

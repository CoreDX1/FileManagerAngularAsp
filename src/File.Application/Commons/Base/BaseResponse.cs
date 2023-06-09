using File.Application.DTO.Response.File;
using File.Application.DTO.Response.Folder;

namespace File.Application.Commons.Base;

public class BaseResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public RootResponseDto? Data { get; set; }
}

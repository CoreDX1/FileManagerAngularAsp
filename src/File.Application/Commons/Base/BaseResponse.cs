using System.Text.Json.Serialization;
using File.Application.DTO.Response.Folder;

namespace File.Application.Commons.Base;

public class BaseResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RootResponseDto? Data { get; set; }
}

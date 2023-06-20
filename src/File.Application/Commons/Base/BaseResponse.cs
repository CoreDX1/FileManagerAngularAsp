using System.Text.Json.Serialization;

namespace File.Application.Commons.Base;

public class BaseResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }
}

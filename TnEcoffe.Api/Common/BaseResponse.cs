using System.Text.Json.Serialization;

namespace TnEcoffe.Api.Common;

public class BaseResponse
{
    public bool Success { get; set; }

    public string? Message { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ErrorCode { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IDictionary<string, string[]>? Errors { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TraceId { get; init; }

    public static BaseResponse Ok(string? message = null) => new()
    {
        Success = true,
        Message = message,
    };

    public static BaseResponse Fail(
        string errorCode,
        string message,
        IDictionary<string, string[]>? errors = null,
        string? traceId = null) => new()
    {
        Success = false,
        ErrorCode = errorCode,
        Message = message,
        Errors = errors,
        TraceId = traceId,
    };
}

public sealed class BaseResponse<T> : BaseResponse
{
    public T? Data { get; init; }

    public static BaseResponse<T> Ok(T data, string? message = null) => new()
    {
        Success = true,
        Data = data,
        Message = message,
    };
}

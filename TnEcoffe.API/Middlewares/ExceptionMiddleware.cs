using System.Diagnostics;
using TnEcoffe.Application.Common.Exceptions;
using TnEcoffe.Domain.Common.Errors;

namespace TnEcoffe.Api.Middlewares;

// Nơi DUY NHẤT đổi exception → HTTP status + BaseResponse.
public sealed class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
        {
            // Người dùng đóng tab / hủy request: không phải lỗi.
            logger.LogInformation("Request {Path} bị client hủy.", context.Request.Path);
        }
        catch (Exception exception)
        {
            await HandleAsync(context, exception);
        }
    }

    private async Task HandleAsync(HttpContext context, Exception exception)
    {
        var traceId = Activity.Current?.Id ?? context.TraceIdentifier;

        var (statusCode, response) = exception switch
        {
            // Lỗi có chủ đích: lấy loại lỗi → HTTP status, giữ nguyên Code và Message.
            AppException ex => (
                ToStatusCode(ex.Type),
                BaseResponse.Fail(
                    ex.Code,
                    ex.Message,
                    (ex as ValidationException)?.Errors.ToDictionary(),
                    traceId)),

            // Lỗi gọi dịch vụ đối tác (VNPay, MoMo, email...).
            HttpRequestException => (
                StatusCodes.Status502BadGateway,
                BaseResponse.Fail(
                    ErrorCodes.ExternalServiceError,
                    "Không thể kết nối tới dịch vụ đối tác.",
                    traceId: traceId)),

            TimeoutException => (
                StatusCodes.Status504GatewayTimeout,
                BaseResponse.Fail(
                    ErrorCodes.ExternalServiceTimeout,
                    "Dịch vụ đối tác phản hồi quá lâu, vui lòng thử lại.",
                    traceId: traceId)),

            // Lỗi ngoài dự kiến: KHÔNG trả ex.Message ra ngoài.
            _ => (
                StatusCodes.Status500InternalServerError,
                BaseResponse.Fail(
                    ErrorCodes.Internal,
                    "Đã có lỗi xảy ra, vui lòng thử lại sau.",
                    traceId: traceId)),
        };

        if (statusCode >= StatusCodes.Status500InternalServerError)
            logger.LogError(exception, "Lỗi chưa xử lý. TraceId: {TraceId}", traceId);
        else
            logger.LogWarning("{ExceptionType} ({Code}): {Message}. TraceId: {TraceId}",
                exception.GetType().Name, response.ErrorCode, exception.Message, traceId);

        if (context.Response.HasStarted)
            return;

        context.Response.Clear();
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(response);
    }

    // Bảng duy nhất quyết định loại lỗi nào ra HTTP status nào.
    private static int ToStatusCode(ErrorType type) => type switch
    {
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.BadRequest => StatusCodes.Status400BadRequest,
        ErrorType.BusinessRule => StatusCodes.Status400BadRequest,
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Conflict => StatusCodes.Status409Conflict,
        ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
        ErrorType.Forbidden => StatusCodes.Status403Forbidden,
        _ => StatusCodes.Status500InternalServerError,
    };
}
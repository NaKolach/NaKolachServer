using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Diagnostics;

using NaKolachServer.Domain.Auth;
using NaKolachServer.Domain.Routes;
using NaKolachServer.Domain.Users;
using NaKolachServer.Domain.Utils;

namespace NaKolachServer.Presentation.Utils;

internal sealed class BusinessExceptionHandler : IExceptionHandler
{
    private readonly JsonSerializerOptions _options = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
    };

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception ex, CancellationToken cancellationToken)
    {
        if (ex is not BusinessException businessException) return false;

        var responseBody = new ErrorResponse
        {
            Message = ex.Message
        };

        switch (ex)
        {
            case UnauthorizedException:
                responseBody.StatusCode = StatusCodes.Status401Unauthorized;
                httpContext.Response.StatusCode = responseBody.StatusCode;
                break;
            case RefreshTokenActionNotAllowedException:
            case UserActionNotAllowedException:
                responseBody.StatusCode = StatusCodes.Status409Conflict;
                httpContext.Response.StatusCode = responseBody.StatusCode;
                break;
            case UserNotFoundException:
            case RouteNotFoundException:
                responseBody.StatusCode = StatusCodes.Status404NotFound;
                httpContext.Response.StatusCode = responseBody.StatusCode;
                break;
            default:
                return false;
        }

        await httpContext.Response.WriteAsJsonAsync(responseBody, _options, cancellationToken);
        return true;
    }
}
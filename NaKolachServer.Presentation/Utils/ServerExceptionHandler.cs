using Microsoft.AspNetCore.Diagnostics;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace NaKolachServer.Presentation.Utils;

internal sealed class ServerExceptionHandler(ILogger<ServerExceptionHandler> logger) : IExceptionHandler
{
    private readonly JsonSerializerOptions _options = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
    };

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception ex, CancellationToken cancellationToken)
    {
        logger.LogError(ex, "Internal server error");

        var responseBody = new ErrorResponse
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = "Internal server error"
        };

        httpContext.Response.StatusCode = responseBody.StatusCode;

        await httpContext.Response.WriteAsJsonAsync(responseBody, _options, cancellationToken);
        return true;
    }
}

namespace NaKolachServer.Presentation.Utils;

internal sealed class ErrorResponse
{
    public DateTimeOffset Timestamp { get; } = DateTimeOffset.UtcNow;
    public int StatusCode { get; set; }
    public required string Message { get; set; }
    public IDictionary<string, string[]>? Errors { get; set; }
}
